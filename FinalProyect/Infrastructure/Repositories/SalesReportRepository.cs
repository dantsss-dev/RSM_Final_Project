using FinalProyect.Domain.Filters;
using FinalProyect.Domain.Interface;
using FinalProyect.Domain.Models;
using FinalProyect.Domain.Pagination;
using Microsoft.EntityFrameworkCore;

namespace FinalProyect.Infrastructure.Repositories
{
    public class SalesReportRepository : ISalesReportRepository
    {
        private readonly AdvWorksDbContext _context;

        public SalesReportRepository(AdvWorksDbContext context)
        {
            _context = context;
        }

        public async Task<Tuple<int,List<vSalesReport>>> GetSalesReportQueryWithFiltering(SalesReportFilters filters)
        {
            var query = from soh in _context.SalesOrderHeaders
                            join sod in _context.SalesOrderDetails on soh.SalesOrderId equals sod.SalesOrderId
                            join pro in _context.Products on sod.ProductId equals pro.ProductId
                            join psc in _context.ProductSubcategories on pro.ProductSubcategoryId equals psc.ProductSubcategoryId
                            join pc  in _context.ProductCategories on psc.ProductCategoryId equals pc.ProductCategoryId
                            join sp  in _context.People on soh.SalesPersonId equals sp.BusinessEntityId
                            join c   in _context.Customers on soh.CustomerId equals c.CustomerId
                            join cp  in _context.People on c.PersonId equals cp.BusinessEntityId
                            join sta in _context.Addresses on soh.ShipToAddressId equals sta.AddressId
                            join bta in _context.Addresses on soh.BillToAddressId equals bta.AddressId
                            select new vSalesReport
                            {
                                SalesOrderId = soh.SalesOrderId,
                                OrderDate = soh.OrderDate,
                                CustomerId = soh.CustomerId,
                                CustomerFirstName = cp.FirstName,
                                CustomerLastName = cp.LastName,
                                ProductId = sod.ProductId,
                                ProductName = pro.Name,
                                ProductCategory = pc.Name,
                                UnitPrice = sod.UnitPrice,
                                OrderQty = sod.OrderQty,
                                LineTotal = sod.LineTotal,
                                SalesPersonId = soh.SalesPersonId,
                                SalesPersonFirstName = sp.FirstName,
                                SalesPersonLastName = sp.LastName,
                                ShippingAddress = sta.AddressLine1,
                                BillingAddress = bta.AddressLine1
                            };

           
            if(!string.IsNullOrEmpty(filters.CustomerFirstName))
            {
                query = query.Where(x => x.CustomerFirstName.Contains(filters.CustomerFirstName));
            }
            if(!string.IsNullOrEmpty(filters.CustomerLastName))
            {
                query = query.Where(x => x.CustomerLastName.Contains(filters.CustomerLastName));
            } 
            if(!string.IsNullOrEmpty(filters.SalesPersonFirstName))
            {
                query = query.Where(x => x.SalesPersonFirstName.Contains(filters.SalesPersonFirstName));
            }   
            if(!string.IsNullOrEmpty(filters.SalesPersonLastName))
            {
                query = query.Where(x => x.SalesPersonLastName.Contains(filters.SalesPersonLastName));
            }
            if(!string.IsNullOrEmpty(filters.ProductName))
            {
                query = query.Where(x => x.ProductName.Contains(filters.ProductName));
            } 
            if(!string.IsNullOrEmpty(filters.ProductCategory))
            {
                query = query.Where(x => x.ProductCategory.Contains(filters.ProductCategory));
            }
            var filtering = query.Where(x => x.OrderDate >= filters.StartDate && x.OrderDate <= filters.EndDate);  

            var totalCount = await filtering.CountAsync();

            var reportSale = await filtering.Skip((filters.Page -1) * 17).Take(17).AsNoTracking().ToListAsync();

            return Tuple.Create(totalCount, reportSale);
        }
        public async Task<Tuple<int,List<vTotalSalesByPorcentage>>> GetSalesReportByPercentage(SalesReportByPercentageFilters filters)
        {
            var query = from soh in _context.SalesOrderHeaders
            join sod in _context.SalesOrderDetails on soh.SalesOrderId equals sod.SalesOrderId
            join pro in _context.Products on sod.ProductId equals pro.ProductId
            join psc in _context.ProductSubcategories on pro.ProductSubcategoryId equals psc.ProductSubcategoryId
            join pc in _context.ProductCategories on psc.ProductCategoryId equals pc.ProductCategoryId
            select new vProductSales
            {
                ProductId = sod.ProductId,
                ProductName = pro.Name,
                ProductCategory = pc.Name,
                LineTotal = sod.LineTotal,
                TerritoryId = soh.TerritoryId,
                OrderDate = soh.OrderDate,
            };

var totalSalesProductRegion = from p in query
                              group p by new { p.ProductCategory, p.TerritoryId } into g
                              select new
                              {
                                  ProductCategory = g.Key.ProductCategory,
                                  TerritoryId = g.Key.TerritoryId,
                                  TotalSalesProductRegion = g.Sum(p => p.LineTotal)
                              };

var totalSalesCategoryInRegion = from p in totalSalesProductRegion
                                group p by new { p.ProductCategory } into g
                                select new
                                {
                                    ProductCategory = g.Key.ProductCategory,
                                    TotalSalesCategoryInRegion = g.Sum(p => p.TotalSalesProductRegion)
                                };

var totalSalesWorldwide = query.Sum(p => p.LineTotal);

var finalResult = from tsc in totalSalesCategoryInRegion
                  select new vTotalSalesByPorcentage
                  {
                      ProductCategory = tsc.ProductCategory,
                      PercentageOfTotalSalesInRegion = Math.Round(totalSalesProductRegion.Sum(x => x.TotalSalesProductRegion) / totalSalesWorldwide * 100, 4),
                      PercentageOfCategoryInRegion = Math.Round(tsc.TotalSalesCategoryInRegion / totalSalesWorldwide * 100, 4)
                  } into result
                  orderby result.PercentageOfTotalSalesInRegion descending
                  select result;

var totalCount = await finalResult.CountAsync();
if (!string.IsNullOrEmpty(filters.ProductCategory))
{
    finalResult = (IOrderedQueryable<vTotalSalesByPorcentage>)finalResult.Where(p => p.ProductCategory.Contains(filters.ProductCategory));
}
var SalesReportByPercentage = await finalResult.Skip((filters.Page - 1) * 17).Take(17).AsNoTracking().ToListAsync();
                
                return Tuple.Create(totalCount, SalesReportByPercentage);
        }
    }
}