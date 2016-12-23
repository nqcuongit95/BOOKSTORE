using BookStore.Helper;
using BookStore.ViewModels.Dashboard;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                start = now.Date.AddDays(-(int)now.DayOfWeek); // prev sunday 00:00
                end = start.AddDays(7); // next sunday 00:00
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
                             Value= item.TongTien,
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
                    var justNow =  DateTime.UtcNow - time.ToUniversalTime();

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
    }
}
