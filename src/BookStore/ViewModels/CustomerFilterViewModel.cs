using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class CustomerFilterViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

    }

    public class CustomerFilterResults
    {
        public List<CustomerFilterViewModel> Results { get; set; }
    }

}
