using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.ViewModels.Dashboard
{
    public class ProductStatisticViewModel
    {
        public int TotalBooks { get; set; }
        public int TotalStationerys { get; set; }
        public int TotalReturnThisWeek { get; set; }
        public BestSellingProduct BestSellingBook { get; set; }        

    }

    public class BestSellingProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Solds { get; set; }
    }
    
}
