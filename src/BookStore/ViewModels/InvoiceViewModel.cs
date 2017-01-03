using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class InvoiceViewModel
    {
        public int CustomerId { get; set; }
        public string Staff { get; set; }
        public int StatusId { get; set; }
        public decimal TotalValue { get; set; }
        public decimal CustomerPaid { get; set; }
        public DateTime DateCreate { get; set; }
        public decimal? Discount { get; set; }

    }
}
