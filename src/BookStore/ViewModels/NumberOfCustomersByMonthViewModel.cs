using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class NumberOfCustomersByMonthViewModel
    {
        public string Month { get; set; }
        public int MonthValue { get; set; }
        public int Total { get; set; }
    }
}
