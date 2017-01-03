using BookStore.Helper;
using BookStore.ViewModels;
using BookStore.ViewModels.Dashboard;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.ViewModels.Sale;

namespace BookStore.Services
{
    public partial class BookStoreData : IBookStoreData
    {
        public async Task<List<RevenueViewModel>> GetRevenueStatistics(int take, TimeEnum time)
        {
            var now = DateTime.Now;
            DateTime start = now, end = now;

            if (time == TimeEnum.Week)
            {
                start = now.Date.AddDays(-7); // prev sunday 00:00
                //end = start.AddDays(7); // next sunday 00:00
                end = now;
            }
            else if (time == TimeEnum.Month)
            {
                //todo: process later
            }

            var query = from receiptVoucher in _context.PhieuThu
                        where receiptVoucher.NgayLap >= start &&
                              receiptVoucher.NgayLap < end
                        group receiptVoucher by receiptVoucher.NgayLap.Date
                        into groupReceipt
                        select new RevenueViewModel
                        {
                            Timeline = groupReceipt.Key.Date.ToString("dd/MM"),
                            Value = groupReceipt.Sum(re => re.TongTien)
                        };

            var result = await query.ToListAsync();

            return result;
        }
        public async Task<List<FeedsViewModel>> GetFeeds(int take)
        {
            //invoice
            var query1 = from item in _context.DonHang
                         orderby item.NgayLap descending
                         select new FeedsViewModel
                         {
                             Id = item.Id,
                             FeedType = FeedType.NewInvoice,
                             Content = Content.NewInvoice,
                             Value = item.TongTien,
                             Icon = "add to cart green",
                             Time = item.NgayLap
                         };
            var lastestInvoice = await query1.Take(take).ToListAsync();

            //customer
            var query2 = from item in _context.KhachHang
                         orderby item.NgayLap descending
                         select new FeedsViewModel
                         {
                             Id = item.Id,
                             FeedType = FeedType.NewCustomer,
                             Content = Content.NewCustomer,
                             Icon = "add user blue",
                             Time = item.NgayLap
                         };
            var lastestCustomer = await query2.Take(take).ToListAsync();

            //return products
            var query3 = from item in _context.PhieuTraHang
                         orderby item.NgayLap descending
                         select new FeedsViewModel
                         {
                             Id = item.Id,
                             FeedType = FeedType.ReturnGoods,
                             Content = Content.ReturnProduct,
                             Icon = "share", //dont know what icon to use right there
                             Time = item.NgayLap
                         };
            var lastestReturnProducts = await query3.Take(take).ToListAsync();

            //receipt voucher
            var query4 = from item in _context.PhieuThu
                         orderby item.NgayLap descending
                         select new FeedsViewModel
                         {
                             Id = item.Id,
                             FeedType = FeedType.ReceiptVoucher,
                             Content = Content.ReceiptVoucher,
                             Icon = "dollar yellow",
                             Value = item.TongTien,
                             Time = item.NgayLap
                         };
            var lastestReceiptVoucher = await query4.Take(take).ToListAsync();

            //imported products
            var query5 = from item in _context.PhieuNhapHang
                         orderby item.NgayLap descending
                         select new FeedsViewModel
                         {
                             Id = item.Id,
                             FeedType = FeedType.ImportedGoods,
                             Content = Content.ImportProduct,
                             Icon = "",
                             Time = item.NgayLap
                         };
            var lastestImportedProducts = await query5.Take(take).ToListAsync();

            var feeds = lastestInvoice.Concat(lastestCustomer)
                                      .Concat(lastestReturnProducts)
                                      .Concat(lastestReturnProducts)
                                      .Concat(lastestReceiptVoucher)
                                      .Concat(lastestImportedProducts)
                                      .OrderByDescending(feed => feed.Time)
                                      .Take(take).ToList();

            for (int i = 0; i < take; i++)
            {
                feeds[i].TimeDescriptor = GetTimeline(feeds[i].Time);
                feeds[i].ValueFormated = feeds[i].Value.ToString("N0") + "đ";
            }

            return feeds;
        }
        //helper function
        public string GetTimeline(DateTime time)
        {
            bool isYesterday = DateTime.Today - time.Date == TimeSpan.FromDays(1);

            if (isYesterday)
            {
                return TimelineDescriptor.Yesterday + time.ToString("HH:mm");
            }
            else if (time.Date == DateTime.Today)
            {
                bool lessThanTwoHours = time.ToUniversalTime().AddHours(2) >= DateTime.UtcNow;
                //just hard code...
                if (lessThanTwoHours)
                {
                    var justNow = DateTime.UtcNow - time.ToUniversalTime();

                    if (justNow.Minutes <= 3 && justNow.Hours == 0)
                    {
                        return TimelineDescriptor.JustNow;
                    }

                    var formated = TimelineDescriptor.Ago;
                    formated += justNow.Hours != 0 ? justNow.Hours + " giờ " : "";
                    formated += justNow.Minutes != 0 ? justNow.Minutes + " phút" : "";

                    return formated;
                }

                return TimelineDescriptor.Today + time.ToString("HH:mm");
            }

            return time.ToString("MM/dd/yyyy HH:mm",
                                CultureInfo.InvariantCulture);
        }
        public string FormatDecimalValue(decimal val)
        {
            return val.ToString("N0") + " đ";
        }
        public async Task<TransactionStatisticsViewModel> GetTransactionStatistics()
        {
            var statistics = new TransactionStatisticsViewModel();

            var totalTransactionValues = await _context.DonHang.SumAsync(i => i.TongTien);
            statistics.TotalTransactionValues = totalTransactionValues.ToString("N0");

            //today transaction
            var todayTransaction = await _context.DonHang.Where(i => i.NgayLap.Date == DateTime.Today)
                                                         .SumAsync(i => i.TongTien);
            statistics.TodayTransactionValues = FormatDecimalValue(todayTransaction);

            //today revenue
            var todayRevenue = await _context.PhieuThu.Where(r => r.NgayLap.Date == DateTime.Today)
                                                      .SumAsync(r => r.TongTien);
            statistics.TodayRevenue = FormatDecimalValue(todayRevenue);

            return statistics;
        }
        public async Task<CustomerStatisticsViewModel> GetCustomerStatistics()
        {
            var statistics = new CustomerStatisticsViewModel();

            //total customer
            var totalCustomer = await _context.KhachHang.CountAsync();
            statistics.TotalCustomers = totalCustomer;

            //number of new customers
            var now = DateTime.Now;
            DateTime start, end;
            start = now.Date.AddDays(-(int)now.DayOfWeek); // prev sunday 00:00
            end = start.AddDays(7); // next sunday 00:00

            var newThisWeek = await _context.KhachHang
                             .CountAsync(c => c.NgayLap >= start && c.NgayLap < end);

            statistics.TotalNewCumstomers = newThisWeek;

            //total liabilities
            //var query = await (from receiptVoucher in _context.PhieuThu
            //                   group receiptVoucher by receiptVoucher.DonHangId).ToListAsync();

            var query = await (from invoice in _context.DonHang
                               join receiptVoucher in _context.PhieuThu
                               on invoice.Id equals receiptVoucher.DonHangId into joined
                               from j in joined.DefaultIfEmpty()
                               group j by invoice.Id).ToListAsync();

            var totalLiabilities = query.Select((r) =>
           {
               var receiptStatistic = r.Aggregate(new ReceiptVoucherStatistics(),
                                                   (acc, c) => acc.Accumulate(c),
                                                   acc => acc.Compute());

               var invoice = _context.DonHang.FirstOrDefault(i => i.Id == r.Key);

               return new
               {
                   Id = r.Key,
                   Liabilities = invoice.TongTien - receiptStatistic.TotalValue
               };

           }).Sum(o => o.Liabilities);

            statistics.CustomerLiabilities = FormatDecimalValue(totalLiabilities);

            return statistics;
        }
        public async Task<ProductStatisticViewModel> GetProductStatistics()
        {
            var statistics = new ProductStatisticViewModel();

            //count product
            var query = from product in _context.HangHoa
                        group product by product.LoaiHangHoaId
                        into grProduct
                        select new
                        {
                            Id = grProduct.Key,
                            Total = grProduct.Count()
                        };

            var countProducts = await query.ToListAsync();
            if (countProducts.ElementAtOrDefault(0) != null)
            {
                statistics.TotalBooks = countProducts[0].Total;
            }

            if (countProducts.ElementAtOrDefault(1) != null)
            {
                statistics.TotalStationerys = countProducts[1].Total;
            }

            //best selling book right now
            var bestSellingBook = await GetBestSellingGoods(1, TimeEnum.Week, ProductType.Book);

            if (bestSellingBook.Count != 0)
            {
                statistics.BestSellingBook = new BestSellingProduct
                {
                    Id = bestSellingBook.FirstOrDefault().Id,
                    Name = bestSellingBook.FirstOrDefault().Name,
                    Solds = bestSellingBook.FirstOrDefault().TotalSold
                };
            }
            else
            {
                statistics.BestSellingBook = new BestSellingProduct
                {
                    Id = -1,
                    Name = "",
                    Solds = 0
                };
            }

            //number of return products: in this week
            var now = DateTime.Now;
            DateTime start, end;
            start = now.Date.AddDays(-(int)now.DayOfWeek); // prev sunday 00:00
            end = start.AddDays(7); // next sunday 00:00

            var numberOfProductsReturn = await (from ret in _context.PhieuTraHang
                                      where ret.NgayLap >= start && ret.NgayLap < end
                                      join detail in _context.ChiTietPhieuTraHang
                                      on ret.Id equals detail.PhieuTraHangId
                                      select detail).SumAsync(d => d.SoLuong);
            statistics.TotalReturnThisWeek = numberOfProductsReturn;

            return statistics;
        }
        public async Task<List<NumberOfCustomersByMonthViewModel>> GetCustomerRegisterStatistics()
        {
            var query = GetAllKhachHang();

            var selectResult = from customer in query
                               group customer by customer.NgayLap.Month
                               into groupCustomer
                               select new NumberOfCustomersByMonthViewModel
                               {
                                   Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(groupCustomer.Key),
                                   Total = groupCustomer.Count(),
                                   MonthValue = groupCustomer.Key
                               } into model
                               orderby model.MonthValue descending
                               select model;

            var result = await selectResult.Take(5).ToListAsync();

            return result.OrderBy(m => m.MonthValue).ToList();
        }
        
    }
}
