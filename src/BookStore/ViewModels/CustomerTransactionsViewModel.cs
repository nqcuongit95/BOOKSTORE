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
        public string TotalValuesFormated { get; set; }
        public IQueryable<InvoiceDetailsViewModel> Invoices { get; set; }
        public PaginatedList<InvoiceDetailsViewModel> InvoicesPage { get; set; }
    }
}
