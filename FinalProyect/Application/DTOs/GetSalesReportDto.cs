namespace FinalProyect.Application.DTOs
{
    public class GetSalesReportDto
    {
        public int SalesOrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string? CustomerName { get; set; }
        public string? ProductName { get; set; }
        public string? ProductCategory { get; set; }
        public decimal UnitPrice { get; set; }
        public int OrderQty { get; set; }
        public decimal LineTotal { get; set; }
        public string? SalesPersonName { get; set;}
        public string? ShippingAddress { get; set; }
        public string? BillingAddress { get; set; }
    }
}