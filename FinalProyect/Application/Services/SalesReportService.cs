namespace FinalProyect.Application.Services
{
    using Azure;
    using FinalProyect.Application.DTOs;
    using FinalProyect.Domain.Filters;
    using FinalProyect.Domain.Interface;
    using FinalProyect.Domain.Models;
    using FinalProyect.Domain.Pagination;

    public class SalesReportService : ISalesReportService
    {
        private readonly ISalesReportRepository _repository;
        public SalesReportService(ISalesReportRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedList<GetSalesReportDto>> GetSalesReport(SalesReportFilters filters)
        {
            var pagedSalesReport = new PagedList<GetSalesReportDto>()
            {
                Page = filters.Page,
            };
            var (totalCount, salesReports) = await _repository.GetSalesReportQueryWithFiltering(filters);
            pagedSalesReport.TotalCount = totalCount;

            foreach(var saleReport in salesReports)
            {
                GetSalesReportDto dto = new()
                {
                    SalesOrderId = saleReport.SalesOrderId,
                    OrderDate = saleReport.OrderDate,
                    CustomerName = $"{saleReport.CustomerFirstName} {saleReport.CustomerLastName}",
                    ProductName = saleReport.ProductName,
                    ProductCategory = saleReport.ProductCategory,
                    UnitPrice = saleReport.UnitPrice,
                    OrderQty = saleReport.OrderQty,
                    LineTotal = saleReport.LineTotal,
                    SalesPersonName = $"{saleReport.SalesPersonFirstName} {saleReport.SalesPersonLastName}",
                    ShippingAddress = saleReport.ShippingAddress,
                    BillingAddress = saleReport.BillingAddress
                };
                pagedSalesReport.Items.Add(dto);
            }
            
            return pagedSalesReport;
        }

        public async Task<PagedList<GetSalesReportByPercentageDto>>  GetSalesReportByPercentage(SalesReportByPercentageFilters filters)
        {
            var pagedSalesReportByPercentage = new PagedList<GetSalesReportByPercentageDto>
            {
                Page = filters.Page
            };
            var (totalCount, salesReportsByPercentage) = await _repository.GetSalesReportByPercentage(filters);
            pagedSalesReportByPercentage.TotalCount = totalCount;

            foreach(var salesReportByPercentage in salesReportsByPercentage)
            {
                GetSalesReportByPercentageDto dto = new()
                {
                    ProductName = salesReportByPercentage.ProductName,
                    ProductCategory = salesReportByPercentage.ProductCategory,
                    TotalSales = salesReportByPercentage.TotalSalesProduct,
                    PercentageByRegion = salesReportByPercentage.PercentageOfTotalSalesInRegion,
                    PercentageByRegionAndCategory = salesReportByPercentage.PercentageOfCategoryInRegion,
                };
                pagedSalesReportByPercentage.Items.Add(dto);
            }

            return pagedSalesReportByPercentage;


        }
    }
}