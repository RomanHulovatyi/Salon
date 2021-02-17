using System;

namespace Salon.BLL.ViewModels
{
    public class PageModel
    {
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }
        public PageModel(int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }
        public bool HasNextPage
        {
            get
            {
                return (PageNumber < TotalPages);
            }
        }
        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }
    }
}
