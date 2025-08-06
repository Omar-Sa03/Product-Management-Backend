namespace Domain.Common
{
    using System;
    using System.Collections.Generic;

    public class PagedList<T>
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public bool HasPrevious => CurrentPage > 1;

        public bool HasNext => CurrentPage < this.TotalPages;

        public IEnumerable<T> Items { get; set; }

        public PagedList() { }

        public PagedList(IEnumerable<T> items, int count, int? pageNumber, int? pageSize)
        {
            TotalCount = count;
            PageSize = pageSize ?? 1;
            CurrentPage = pageNumber ?? 1;
            TotalPages = pageSize.HasValue && pageSize.Value > 0 ? (int)Math.Ceiling(count / (double)pageSize.Value) : 0;
            Items = items;
        }
    }
}
