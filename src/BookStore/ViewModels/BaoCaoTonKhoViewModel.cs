using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class BaoCaoTonKhoViewModel
    {
        public List<HangHoa> HangHoa { get; set; }
        public int TotalInventory { get; set; }
        public decimal TotalValueInventory { get; set; }
    }
}