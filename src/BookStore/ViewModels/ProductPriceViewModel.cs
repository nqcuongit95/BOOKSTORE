using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{   
    public enum  PriceType : int
    {
        Retail = 1,
        WholeSale
    }

    public class ProductPriceViewModel
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public PriceType PriceType { get; set; }

    }
}
