using System;
using System.Collections.Generic;
using System.Linq;

namespace EgressProject.API.Models.Utils
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        
        public PagedList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            TotalCount = source.Count();
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);

            var items = source.Skip((CurrentPage - 1) * PageSize)
                                .Take(PageSize)
                                .ToList();

            AddRange(items);
        } 

        public PagedList(IEnumerable<T> data, int currentPage, int totalPages, int pageSize, int totalCount)
        {
            CurrentPage = currentPage;
            TotalPages = totalPages;
            PageSize = pageSize;
            TotalCount = totalCount;

            AddRange(data);
        }       
    }
}