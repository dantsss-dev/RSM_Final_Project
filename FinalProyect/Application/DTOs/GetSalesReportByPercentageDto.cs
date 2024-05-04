namespace FinalProyect.Application.DTOs
{
    public class GetSalesReportByPercentageDto
    {
		public string ProductName { get; set; } = string.Empty;
        public string ProductCategory { get; set; } = string.Empty;
		public decimal	TotalSales { get; set; }
		public decimal	PercentageByRegion { get; set; }
		public decimal	PercentageByRegionAndCategory { get; set; }
    }
}