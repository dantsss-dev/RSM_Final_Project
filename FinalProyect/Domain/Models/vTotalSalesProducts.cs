namespace FinalProyect.Domain.Models
{
    public class vTotalSalesProducts
    {
        public string ProductName { get; set; } = string.Empty;
        public string ProductCategory { get; set; } = string.Empty;
        public int? TerritoryId { get; set; }
        public decimal TotalSalesProduct { get; set; } = 0;
        public decimal TotalSalesCategory { get; set; }
    }
}