using System.Reflection;

namespace FinalProyect.Domain.Filters
{
    public class SalesReportByPercentageFilters
    {
        public string? ProductCategory { get; set; } = string.Empty;
        public int Page { get; set; } = 1;
    }
    
}