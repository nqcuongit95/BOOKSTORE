using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookStore
{
    public class Paginated
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public Paginated(int pageIndex, int totalPages)
        {
            PageIndex = pageIndex;
            TotalPages = totalPages;
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex > 0) && (PageIndex < TotalPages);
            }
        }
    }

    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        private PaginatedList(List<T> items, int count, int pageIndex, int totalPages)
        {
            PageIndex = pageIndex;
            TotalPages = totalPages;

            if (PageIndex > TotalPages)
                PageIndex = TotalPages;

            this.AddRange(items);
        }

        public static async Task<PaginatedList<T>> CreateAsync(
            IQueryable<T> source, int pageIndex, int pageSize)
        {
            if (pageSize < 1)
                pageSize = 10;

            var count = await source.CountAsync();
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);

            if (pageIndex < 1)
                pageIndex = 1;

            if (totalPages < 1)
                totalPages = 1;

            if (pageIndex > totalPages)
            {
                if (totalPages > 0)
                    pageIndex = totalPages;
                else
                    pageIndex = 1;
            }

            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedList<T>(items, count, pageIndex, totalPages);
        }
    }
}
