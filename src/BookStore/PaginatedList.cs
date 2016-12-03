using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookStore
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int FirstShowedPage { get; set; }
        public int LastShowedPage { get; set; }
        public int NumberOfDisplayPages { get; set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            AddRange(items);
        }

        public PaginatedList(List<T> items, int count,
                             int pageIndex, int pageSize,
                             int numberOfDisplayPages,
                             int? firstShowedPage, int? lastShowedPage)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            NumberOfDisplayPages = numberOfDisplayPages;
            FirstShowedPage = firstShowedPage.GetValueOrDefault();
            LastShowedPage = lastShowedPage.GetValueOrDefault();

            CalculateDisplayPages();

            AddRange(items);
        }

        public void CalculateDisplayPages()
        {            
            if (PageIndex == 1)
            {
                if (TotalPages >= NumberOfDisplayPages)
                {
                    FirstShowedPage = PageIndex;
                    LastShowedPage = NumberOfDisplayPages;                    
                }
                else
                {
                    FirstShowedPage = PageIndex;
                    LastShowedPage = TotalPages;
                }
                return;
            }

            if (PageIndex == LastShowedPage)
            {
                if (LastShowedPage == TotalPages)
                {
                    return;
                }

                if (TotalPages >= NumberOfDisplayPages)
                {
                    var remain = TotalPages - PageIndex;
                    var accommodate = NumberOfDisplayPages - 1;

                    if (remain >= accommodate)
                    {
                        FirstShowedPage = LastShowedPage - 1;
                        LastShowedPage = LastShowedPage + 3;
                    }
                    else
                    {
                        FirstShowedPage = LastShowedPage - remain;
                        LastShowedPage = TotalPages;
                    }
                    
                }
                else
                {
                    FirstShowedPage = 1;
                    LastShowedPage = TotalPages;
                }

                return;
            }

            if (PageIndex == FirstShowedPage)
            {
                if (PageIndex < NumberOfDisplayPages)
                {
                    FirstShowedPage = 1;
                    LastShowedPage = NumberOfDisplayPages;
                }
                else
                {
                    LastShowedPage = FirstShowedPage + 1;
                    FirstShowedPage = LastShowedPage - NumberOfDisplayPages + 1;
                }                
                return;
            }
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
                return (PageIndex < TotalPages);
            }
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex,
                                                               int pageSize,
                                                               int numberOfDisplayPages,
                                                               int? firstShowedPage,
                                                               int? lastShowedPage)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize,
                                        numberOfDisplayPages,
                                        firstShowedPage, lastShowedPage);
        }
    }
}
