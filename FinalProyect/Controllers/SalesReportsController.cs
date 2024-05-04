using System.Net.Mime;
using FinalProyect.Application.DTOs;
using FinalProyect.Domain.Filters;
using FinalProyect.Domain.Interface;
using FinalProyect.Domain.Pagination;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;

namespace FinalProyect.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesReportsController : ControllerBase
    {
        private readonly ISalesReportService _service;
        private readonly IRedisCacheService _cache;
        private readonly IGeneratePdfService _pdf;
        public SalesReportsController(ISalesReportService service, IRedisCacheService cache, IGeneratePdfService pdf)
        {
            _service = service;
            _cache = cache;
            _pdf = pdf;
        }

        [HttpGet("getGeneralSalesReport")]
        public async Task<IActionResult> Get([FromQuery] SalesReportFilters filters)
        {
            string recordKey = $"GSR_{filters.CustomerFirstName?.Trim()}{filters.CustomerLastName?.Trim()}{filters.ProductName?.Trim()}{filters.ProductCategory?.Trim()}{filters.SalesPersonFirstName?.Trim()}{filters.SalesPersonLastName?.Trim()}{filters.StartDate}{filters.EndDate}{filters.Page}";

            var cacheData = await _cache.GetRecordAsync<PagedList<GetSalesReportDto>>(recordKey);
            if(cacheData is null)
            {
                var salesReport = await _service.GetSalesReport(filters);

                await _cache.SetRecordAsync(recordKey, salesReport, null, null);

                return Ok(salesReport);
            }
            return Ok(cacheData);
        }

        [HttpGet("getSalesReportByPercentage")]
        public async Task<IActionResult> GetSalesByPercentage([FromQuery] SalesReportByPercentageFilters filters)
        {
            string recordKey = $"SRbP_{filters.ProductCategory?.Trim()}{filters.Page}";

            var cacheData = await _cache.GetRecordAsync<PagedList<GetSalesReportByPercentageDto>>(recordKey);
            if(cacheData is null)
            {
                var salesReportByPercentage = await _service.GetSalesReportByPercentage(filters);

                await _cache.SetRecordAsync(recordKey, salesReportByPercentage, null, null);

                return Ok(salesReportByPercentage);
            }
            return Ok(cacheData);
        }

        [HttpGet("getGeneralSalesReportPdf")]
        public async Task<IResult> GenerateGeneralSalesPdf([FromQuery] SalesReportFilters filters)
        {
            string recordKey = $"GSR_{filters.CustomerFirstName?.Trim()}{filters.CustomerLastName?.Trim()}{filters.ProductName?.Trim()}{filters.ProductCategory?.Trim()}{filters.SalesPersonFirstName?.Trim()}{filters.SalesPersonLastName?.Trim()}{filters.StartDate}{filters.EndDate}{filters.Page}";

            Console.WriteLine("there is no problem with page "+ filters.Page);
            var cacheData = await _cache.GetRecordAsync<PagedList<GetSalesReportDto>>(recordKey);
            if(cacheData is null)
            {
                cacheData = await _service.GetSalesReport(filters);
                
            }

            var document = _pdf.GeneratePdfQuest(cacheData);

            var pdf = document.GeneratePdf();
            return Results.File(pdf, "application/pdf", "GeneralSales.pdf"); 
        }

        [HttpGet("getSalesReportPercentagePdf")]
        public async Task<IResult> GenerateSalesPercentagePdf([FromQuery] SalesReportByPercentageFilters filters)
        {
            string recordKey = $"SRbP_{filters.ProductCategory?.Trim()}{filters.Page}";

            Console.WriteLine("there is no problem with page "+ filters.Page);
            var cacheData = await _cache.GetRecordAsync<PagedList<GetSalesReportByPercentageDto>>(recordKey);
            if(cacheData is null)
            {
                cacheData = await _service.GetSalesReportByPercentage(filters);
                
            }

            var document = _pdf.GeneratePdfQuest(cacheData);

            var pdf = document.GeneratePdf();
            return Results.File(pdf, "application/pdf", "SalesPercentage.pdf"); 
        }

    }
}