namespace FinalProyect.Domain.Models
{
    public class vProductSales
    {
		public int	ProductId { get; set; }
		public string ProductName { get; set; } = string.Empty;
        public string ProductCategory { get; set; } = string.Empty;
		public decimal	LineTotal { get; set; }
        public int? TerritoryId { get; set; }
        public DateTime OrderDate {get; set;} 
    }
}