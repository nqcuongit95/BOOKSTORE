using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.ViewModels.Dashboard
{
    public class TransactionStatisticsViewModel
    {
        public string TotalTransactionValues { get; set; }
        public string TodayTransactionValues { get; set; }
        public string TodayRevenue { get; set; }
        public int TodayTotalInvoices { get; set; }

    }
}
