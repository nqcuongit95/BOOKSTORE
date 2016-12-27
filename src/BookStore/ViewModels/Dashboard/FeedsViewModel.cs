using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.ViewModels.Dashboard
{
    public enum FeedType
    {
        NewInvoice = 1,
        NewCustomer,
        ReceiptVoucher,
        ImportedGoods,
        ReturnGoods
    }



    public class FeedsViewModel
    {
        public FeedType FeedType { get; set; }
        public DateTime Time { get; set; }
        public string TimeDescriptor { get; set; }
        public string Content { get; set; }
        public decimal Value { get; set; }
        public string ValueFormated { get; set; }
        public string Icon { get; set; }
        public int Id { get; set; }

    }
}
