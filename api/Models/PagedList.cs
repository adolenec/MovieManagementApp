using System;
namespace api
{
    public class PagedList<T> where T : class
    {
        public IList<T> Items { get; set; } = null!;
        public int TotalCount { get; set; }

        public PagedList(IList<T> items, int totalCount)
        {
            Items = items;
            TotalCount = totalCount;
        }
    }
}

