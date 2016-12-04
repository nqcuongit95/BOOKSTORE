using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class CustomerTransactionsViewModel
    {
        public string Name { get; set; }
        public int TotalInvoices { get; set; }
        public decimal TotalValues { get; set; }
        public List<InvoiceDetailsViewModel> Invoices { get; set; }
    }
}
