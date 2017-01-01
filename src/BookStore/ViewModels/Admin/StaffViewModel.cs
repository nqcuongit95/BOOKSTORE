using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.ViewModels.Admin
{
    public class StaffViewModel
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public DateTime? DateCreate { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string Phone { get; set; }

    }
}
