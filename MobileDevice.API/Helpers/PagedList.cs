using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MobileDevice.API.Helpers
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = count == 0 ? 0 :(int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source,
            int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
                pageNumber = 1;
            if (pageSize < 0)
                pageSize = 10;
            if (pageSize > 50)
                pageSize = 50;
            if (pageSize == 0)
                pageSize = 500;                

            var count = await source.CountAsync();
            if (pageSize > count)
                pageSize = count;

            if (count > 0) {
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize)
            .ToListAsync();
            return new PagedList<T>(items, count, pageNumber, pageSize);
            }
            else {
                var noItems = await source.ToListAsync();
                return new PagedList<T>(noItems, count, pageNumber, pageSize);
            }
        }
    }
}