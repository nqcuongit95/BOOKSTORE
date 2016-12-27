using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class InvoiceDetailsViewModel
    {
        public int ID { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalValues { get; set; }
        public string TotalValuesFormated { get; set; }
    }
}
