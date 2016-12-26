using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class ReceiptVoucherViewModel
    {
        public DateTime DateCreate { get; set; }
        public int InvoiceId { get; set; }
        public int StaffId { get; set; }
        public decimal Value { get; set; }
        public int ReceiptTypeId { get; set; }

    }
}
