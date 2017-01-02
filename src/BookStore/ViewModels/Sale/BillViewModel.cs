using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.ViewModels.Sale
{
    public class BillViewModel
    {
        public string ID { get; set; }
        public CustomerViewModel Customer { get; set; }
        public string Staff { get; set; }        
        public decimal TotalValue { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalValueAfterDiscount { get; set; }
        public decimal CustomerPaid { get; set; }
        public decimal CustomerChange { get; set; }
        public DateTime DateCreate { get; set; }
        public List<ProductBuyingDetailsViewModel> ProductDetail { get; set; }
    }

    public class CustomerViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
