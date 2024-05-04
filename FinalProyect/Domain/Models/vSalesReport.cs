namespace FinalProyect.Domain.Models
{
    public class vSalesReport
    {
        public int SalesOrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public string CustomerFirstName { get; set; } = string.Empty;
        public string CustomerLastName { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductCategory { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int OrderQty { get; set; }
        public decimal LineTotal { get; set; }
        public int? SalesPersonId { get; set; }
        public string SalesPersonFirstName { get; set;} = string.Empty;
        public string SalesPersonLastName { get; set;} = string.Empty;
        public string? ShippingAddress { get; set; }
        public string? BillingAddress { get; set; }
    }
}