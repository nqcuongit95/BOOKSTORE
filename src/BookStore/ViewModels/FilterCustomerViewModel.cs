using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class FilterCustomerViewModel
    {
        public string Name { get; set; }
        public string Phone { get; set; }

    }

    public class CustomerResults
    {
        public List<FilterCustomerViewModel> Results { get; set; }
    }

}
