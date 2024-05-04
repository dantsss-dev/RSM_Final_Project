using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

namespace FinalProyect.Domain.Pagination
{
    public class PagedList<T>()
    {
        public List<T> Items { get; set; } = [] ;
        public int Page { get; set;}  = 1;
        public int TotalCount { get; set;}
        public bool HasNextPage => Page * 17 < TotalCount;
        public bool HasPreviousPage => Page > 1;

    }
}