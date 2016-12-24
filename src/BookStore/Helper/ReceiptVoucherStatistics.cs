using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Helper
{
    public class ReceiptVoucherStatistics
    {
        public decimal TotalValue { get; set; }

        public ReceiptVoucherStatistics()
        {
            TotalValue = 0;
        }

        public ReceiptVoucherStatistics Accumulate(PhieuThu receiptVoucher)
        {
            TotalValue += receiptVoucher == null ? 0 : receiptVoucher.TongTien;
            return this;
        }        

        public ReceiptVoucherStatistics Compute()
        {
            //some aggregate calculation go here
            return this;
        }
    }
}
