using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.ViewModels.Dashboard
{
    public class CustomerStatisticsViewModel
    {
        public int TotalCustomers { get; set; }
        public int TotalNewCumstomers { get; set; }
        public string CustomerLiabilities { get; set; }
    }
}
