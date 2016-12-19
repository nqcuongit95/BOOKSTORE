using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class ProductFilterViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal WholeSaleprice { get; set; }
        public int Available { get; set; }
        public int TotalSold { get; set; }
        public string ImageUrl { get; set; }

    }

    public class ProductFilterResults
    {
        public List<ProductFilterViewModel> Results { get; set; }
    }
}
