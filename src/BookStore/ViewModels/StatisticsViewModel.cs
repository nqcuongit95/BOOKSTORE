using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class StatisticsViewModel
    {
        [DisplayName("Tổng Giá Trị Giao Dịch")]
        public decimal TotalTransactionValues { get; set; }

        [DisplayName("Tổng Giá Trị Tồn Kho")]
        public decimal ToTalInventoryValues { get; set; }

        [DisplayName("Khách Hàng")]
        public int TotalCustomers { get; set; }

        [DisplayName("Hàng Hóa")]
        public int ToTalGoods { get; set; }
    }
}
