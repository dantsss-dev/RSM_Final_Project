using FinalProyect.Application.DTOs;
using FinalProyect.Domain.Filters;
using FinalProyect.Domain.Models;
using FinalProyect.Domain.Pagination;

namespace FinalProyect.Domain.Interface
{
    public interface ISalesReportService
    {
        Task<PagedList<GetSalesReportDto>> GetSalesReport(SalesReportFilters filters); 

        Task<PagedList<GetSalesReportByPercentageDto>> GetSalesReportByPercentage(SalesReportByPercentageFilters filters);
    }
}