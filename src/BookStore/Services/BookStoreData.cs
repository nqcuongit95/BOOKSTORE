using BookStore.Entities;
using BookStore.Helper;
using BookStore.Models;
using BookStore.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.ViewModels.Dashboard;
using BookStore.ViewModels.Customer;

namespace BookStore.Services
{
    public partial class BookStoreData : IBookStoreData
    {
        private BOOKSTOREContext _context;

        public BookStoreData(BOOKSTOREContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public int CreateCustomer(KhachHang customer)
        {
            _context.Add(customer);
            _context.SaveChanges();

            return customer.Id;
        }

        public IQueryable<CustomerInfoViewModel> GetAllKhachHang()
        {
            var query = from customer in _context.KhachHang
                        join type in _context.LoaiKhachHang
                        on customer.LoaiKhachHangId equals type.Id
                        select new CustomerInfoViewModel
                        {
                            ID = customer.Id,
                            TenKhachHang = customer.TenKhachHang,
                            SoDienThoai = customer.SoDienThoai,
                            Email = customer.Email,
                            TenLoaiKhachHang = type.TenLoaiKhachHang,
                            NgayLap = customer.NgayLap,
                            DiaChi = customer.DiaChi
                        };

            return query;
        }

        public IEnumerable<LoaiKhachHang> GetAllLoaiKhachHang()
        {
            return _context.LoaiKhachHang;
        }

        public KhachHang GetKhachHang(int id)
        {
            return _context.KhachHang.First(c => c.Id == id);
        }

        public CustomerInfoViewModel GetKhachHangInfo(int id)
        {
            var query = from customer in _context.KhachHang
                        where customer.Id == id
                        join type in _context.LoaiKhachHang
                        on customer.LoaiKhachHangId equals type.Id
                        select new CustomerInfoViewModel
                        {
                            ID = customer.Id,
                            TenKhachHang = customer.TenKhachHang,
                            SoDienThoai = customer.SoDienThoai,
                            Email = customer.Email,
                            TenLoaiKhachHang = type.TenLoaiKhachHang,
                            NgayLap = customer.NgayLap,
                            DiaChi = customer.DiaChi
                        };

            return query.First();
        }

        public string GetTenLoaiKhachHang(int id)
        {
            return _context.LoaiKhachHang.First(i => i.Id == id).TenLoaiKhachHang;
        }

        public async Task<CustomerTransactionsViewModel> GetCustomerTransactionsDetails(int id)
        {
            var model = new CustomerTransactionsViewModel();

            model.Name = _context.KhachHang.First(c => c.Id == id).TenKhachHang;

            var query = from invoice in _context.DonHang
                        join status in _context.TrangThai
                        on invoice.TrangThaiId equals status.Id
                        where invoice.KhachHangId == id
                        select new InvoiceDetailsViewModel
                        {
                            ID = invoice.Id,
                            Date = invoice.NgayLap,
                            Status = status.TenTrangThai,
                            TotalValues = invoice.TongTien,
                            TotalValuesFormated = FormatDecimalValue(invoice.TongTien)
                        };

            var invoices = await query.ToListAsync();
            var totalInvoices = await query.CountAsync();
            var totalValues = await query.SumAsync(inv => inv.TotalValues);

            model.Invoices = query.OrderByDescending(i=>i.Date);
            model.TotalInvoices = totalInvoices;
            model.TotalValues = totalValues;
            model.TotalValuesFormated = totalValues.ToString("N0");        

            return model;
        }

        public async Task<StatisticsViewModel> GetStatisticsInformation()
        {
            var model = new StatisticsViewModel();
            var totalCustomers = await _context.KhachHang.CountAsync();
            var totalGoods = await _context.HangHoa.CountAsync();
            var totalTransactionValues = await _context.DonHang.SumAsync(i => i.TongTien);

            model.TotalCustomers = totalCustomers;
            model.ToTalGoods = totalGoods;
            model.TotalTransactionValues = totalTransactionValues;

            return model;
        }

        public async Task<List<Role>> GetListRoles()
        {
            return await _context.Roles.ToListAsync();
        }
        public async Task<List<Staff>> GetListStaffs()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<CustomerFilterResults> FindCustomer(string value)
        {
            if (value.All(char.IsDigit))
            {
                var query2 = from customer in _context.KhachHang
                             where customer.SoDienThoai.Contains(value)
                             select new CustomerFilterViewModel
                             {
                                 Id = customer.Id,
                                 Name = customer.TenKhachHang,
                                 Phone = customer.SoDienThoai,
                                 Address = customer.DiaChi

                             };
                var results2 = await query2.ToListAsync();
                return new CustomerFilterResults { Results = results2 };
            }

            var query3 = from customer in _context.KhachHang
                         where customer.TenKhachHang.Contains(value)
                         select new CustomerFilterViewModel
                         {
                             Id = customer.Id,
                             Name = customer.TenKhachHang,
                             Phone = customer.SoDienThoai,
                             Address = customer.DiaChi
                         };

            var results3 = await query3.ToListAsync();
            return new CustomerFilterResults { Results = results3 };

        }

        public async Task<ProductFilterResults> FindProduct(string val)
        {

            if (val.All(char.IsDigit))
            {
                var query1 = from product in _context.HangHoa
                             where product.Id.ToString().Contains(val)
                             select new ProductFilterViewModel
                             {
                                 Id = product.Id,
                                 Name = product.TenHangHoa,
                                 RetailPrice = product.GiaBanLe != null ? product.GiaBanLe.Value : 0,
                                 WholeSaleprice = product.GiaBanSi != null ? product.GiaBanSi.Value : 0,
                                 Available = product.TonKho

                             };
                var result1 = await query1.ToListAsync();
                return new ProductFilterResults { Results = result1 };
            }

            var query = from product in _context.HangHoa
                        where product.TenHangHoa.Contains(val)
                        select new ProductFilterViewModel
                        {
                            Id = product.Id,
                            Name = product.TenHangHoa,
                            RetailPrice = product.GiaBanLe != null ? product.GiaBanLe.Value : 0,
                            WholeSaleprice = product.GiaBanSi != null ? product.GiaBanSi.Value : 0,
                            Available = product.TonKho
                        };

            var result2 = await query.ToListAsync();

            return new ProductFilterResults { Results = result2 };
        }

        public async Task<ProductPriceViewModel> GetPrice(int id, int type)
        {
            var query = from product in _context.HangHoa
                        where product.Id == id
                        select new ProductPriceViewModel
                        {
                            Id = product.Id,
                            Price = type == (int)PriceType.Retail ?
                            product.GiaBanLe != null ? product.GiaBanLe.Value : 0 :
                            product.GiaBanSi != null ? product.GiaBanSi.Value : 0,
                            PriceType = (PriceType)type
                        };

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> AddInvoice(InvoiceViewModel invoice,
            List<ProductBuyingDetailsViewModel> productDetails)
        {
            try
            {
                //check if product details is valid
                foreach (var details in productDetails)
                {
                    var product = await _context.HangHoa.Where(p => p.Id == details.ProductId).FirstOrDefaultAsync();

                    if (product == null || (details.Count > product.TonKho))
                    {
                        return false;
                    }
                }

                //add invoice
                var userId = (from user in _context.Staff
                              where user.UserName == invoice.Staff
                              select user).First().Id;

                //hard code id, edit later
                var statusId = invoice.CustomerPaid >= invoice.TotalValue ? 2 : 3;

                var invoice_ = new DonHang
                {
                    KhachHangId = invoice.CustomerId,
                    NhanVienId = userId,
                    NgayLap = DateTime.Now,
                    TongTien = invoice.TotalValue,
                    TrangThaiId = statusId
                };

                await _context.DonHang.AddAsync(invoice_);

                //add related product
                foreach (var product in productDetails)
                {
                    var detail = new ChiTietDonHang
                    {
                        //DonHangId = invoice_.Id,
                        HangHoaId = product.ProductId,
                        SoLuong = product.Count,
                        GiaBan = product.Price
                    };

                    invoice_.ChiTietDonHang.Add(detail);

                    //also update product count
                    var boughtProduct = await (from bought in _context.HangHoa
                                               where bought.Id == product.ProductId
                                               select bought).FirstAsync();

                    boughtProduct.TonKho -= product.Count;

                    //update number of products sold
                    boughtProduct.DaBan += product.Count;

                }

                //finally, add receipt voucher
                if (invoice.CustomerPaid > 0)
                {
                    var receiptVoucher = new PhieuThu
                    {
                        NgayLap = DateTime.Now,
                        NhanVienId = invoice_.NhanVienId,
                        TongTien = invoice.CustomerPaid >= invoice.TotalValue ?
                                       invoice.TotalValue : invoice.CustomerPaid,
                        LoaiPhieuId = 2 //hard code for tesing, edit later
                    };
                    invoice_.PhieuThu.Add(receiptVoucher);
                }
                //commit transaction
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<List<ProductFilterViewModel>>
            GetBestSellingGoods(int take, TimeEnum time, ProductType type)
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
                //start and end day of this month
                start = new DateTime(now.Year, now.Month, 1);
                end = start.AddMonths(1).AddDays(-1);
            }

            var query = from invoice in _context.DonHang
                        where invoice.NgayLap >= start && invoice.NgayLap < end
                        join detail in _context.ChiTietDonHang
                        on invoice.Id equals detail.DonHangId
                        group detail by detail.HangHoaId;


            var invoicesInAWeek = await query.ToListAsync();

            var query2 = invoicesInAWeek.Select((g) =>
            {
                var statistic = g.Aggregate(new ProductStatistics(),
                                            (acc, c) => acc.Accumulate(c),
                                            acc => acc.Compute());

                var product = (from pro in _context.HangHoa
                               where pro.Id == g.Key
                               select pro).First();

                return new ProductFilterViewModel
                {
                    Id = g.Key,
                    ProductTypeId = product.LoaiHangHoaId,
                    TotalSold = statistic.TotalSold,
                    Name = product.TenHangHoa,
                    Available = product.TonKho,
                    RetailPrice = product.GiaBanLe != null ? product.GiaBanLe.Value : 0,
                    WholeSaleprice = product.GiaBanSi != null ? product.GiaBanSi.Value : 0
                };
            }).OrderByDescending(i => i.TotalSold);

            List<ProductFilterViewModel> results;

            if (type == ProductType.Both)
            {
                results = query2.Take(take).ToList();
                return results;
            }

            results = query2.Where(f => f.ProductTypeId == (int)type).Take(take).ToList();

            return results;
        }

        public async Task<CustomerFilterViewModel> GetCustomerById(int id)
        {
            var result = await (from customer in _context.KhachHang
                                where customer.Id == id
                                select new CustomerFilterViewModel
                                {
                                    Id = customer.Id,
                                    Name = customer.TenKhachHang,
                                    Address = customer.DiaChi,
                                    Phone = customer.SoDienThoai
                                }).FirstOrDefaultAsync();

            return result;
        }

        public async Task<CustomerLiabilitesViewModel> GetCustomerLiabilites(int id)
        {
            //var query = from customer in _context.KhachHang
            //            where customer.Id == id
            //            join invoice in _context.DonHang
            //            on customer.Id equals invoice.KhachHangId into b
            //            join receipt in _context.PhieuThu
            //            on receipt

            var model = new CustomerLiabilitesViewModel();

            return model;
        }
    }
}
