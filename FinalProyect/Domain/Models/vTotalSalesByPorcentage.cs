namespace FinalProyect.Domain.Models
{
    public class vTotalSalesByPorcentage
    {
        public string ProductName { get; set; } = string.Empty;
        public string ProductCategory { get; set; } = string.Empty ;
        public decimal TotalSalesProduct { get; set; }
        public decimal PercentageOfTotalSalesInRegion { get; set; }
        public decimal PercentageOfCategoryInRegion { get; set; }
    }
}