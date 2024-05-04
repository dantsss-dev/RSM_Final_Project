using FinalProyect.Application.DTOs;
using FinalProyect.Domain.Pagination;
using QuestPDF.Fluent;

namespace FinalProyect.Domain.Interface
{
    public interface IGeneratePdfService
    {
        Document GeneratePdfQuest(PagedList<GetSalesReportDto> salesReport);
        Document GeneratePdfQuest(PagedList<GetSalesReportByPercentageDto> salesReport);
    }
}