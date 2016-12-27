using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Helper
{
    public class ProductStatistics
    {
        public ProductStatistics()
        {
            TotalSold = 0;
        }

        public ProductStatistics Accumulate(ChiTietDonHang detail)
        {
            TotalSold += detail.SoLuong;

            return this;
        }        

        public ProductStatistics Compute()
        {
            return this;
        }

        public int TotalSold { get; set; }
    }
}
