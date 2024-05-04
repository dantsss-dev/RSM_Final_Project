namespace FinalProyect.Domain.Interface
{
    using FinalProyect.Domain.Filters;
    using FinalProyect.Domain.Models;

    public interface ISalesReportRepository
    {
        Task<Tuple<int,List<vSalesReport>>> GetSalesReportQueryWithFiltering(SalesReportFilters filters);

        Task<Tuple<int,List<vTotalSalesByPorcentage>>> GetSalesReportByPercentage(SalesReportByPercentageFilters filters);
    }    
}