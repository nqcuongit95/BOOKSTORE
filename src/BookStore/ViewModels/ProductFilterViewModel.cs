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
        public decimal Price { get; set; }
        public int Available { get; set; }
        public string ImageUrl { get; set; }

    }

    public class ProductFilterResult
    {
        public List<ProductFilterViewModel> Results { get; set; }
    }
}
