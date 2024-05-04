using System.Reflection;

namespace FinalProyect.Domain.Filters
{
    public class SalesReportFilters
    {
        public string? CustomerFirstName { get; set; } = string.Empty;
        public string? CustomerLastName { get; set; } = string.Empty;
        public string? ProductName { get; set; } = string.Empty;
        public string? ProductCategory { get; set; } = string.Empty;
        public string? SalesPersonFirstName { get; set;} = string.Empty;
        public string? SalesPersonLastName { get; set;} = string.Empty;
        public DateTime StartDate { get; set; } = DateTime.MinValue;
        public DateTime EndDate { get; set; } = DateTime.Now;
        public int Page { get; set; } = 1;
    }
    
}