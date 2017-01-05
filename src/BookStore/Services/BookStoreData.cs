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
using BookStore.ViewModels.Sale;

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
        public IEnumerable<LoaiPhieu> GetAllLoaiPhieuChi()
        {
            var query = from loaiphieu in _context.LoaiPhieu
                        where loaiphieu.Loai.Contains("PC")
                        select loaiphieu;
            return query;
        }
        public IEnumerable<LoaiPhieu> GetAllLoaiPhieuThu()
        {
            var query = from loaiphieu in _context.LoaiPhieu
                        where loaiphieu.Loai.Contains("PTH")
                        select loaiphieu;
            return query;
        }
        public IEnumerable<PhieuTraNhapHang> GetAllPhieuTraNhapHang()
        {
            var query = from phieu in _context.PhieuTraNhapHang
                        select phieu;

            return _context.PhieuTraNhapHang;
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
                            StatusAcronym = status.VietTat,
                            TotalValues = invoice.TongTien,
                            TotalValuesFormated = FormatDecimalValue(invoice.TongTien)
                        };

            var invoices = await query.ToListAsync();
            var totalInvoices = await query.CountAsync();
            var totalValues = await query.SumAsync(inv => inv.TotalValues);

            model.Invoices = query.OrderByDescending(i => i.Date);
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
        public IQueryable<Staff> GetListStaffQueryable()
        {
            return _context.Users;
        }
        public CustomerInfoViewModel findCustomerById(int customerId)
        {
            var query = from khachhang in _context.KhachHang
                        join loai in _context.LoaiKhachHang
                        on khachhang.LoaiKhachHangId equals loai.Id
                        where khachhang.Id == customerId
                        select new CustomerInfoViewModel{
                            ID = khachhang.Id,
                            TenKhachHang = khachhang.TenKhachHang,
                            SoDienThoai = khachhang.SoDienThoai,
                            DiaChi = khachhang.DiaChi,
                            Email = khachhang.Email,
                            TenLoaiKhachHang = loai.TenLoaiKhachHang,
                            NgayLap = khachhang.NgayLap
                        };
            return query.First();
        }
        public DonHangViewModel findDonHangById(int donhangID)
        {
            var re = from donhang in _context.DonHang
                     join khachhang in _context.KhachHang
                     on donhang.KhachHangId equals khachhang.Id
                     join trangthai in _context.TrangThai
                     on donhang.TrangThaiId equals trangthai.Id
                     join nv in _context.Staff
                     on donhang.NhanVienId equals nv.Id
                     where donhang.Id == donhangID
                     select new DonHangViewModel
                     {
                         ID = donhang.Id,
                         TongTien = donhang.TongTien,
                         NgayLap = donhang.NgayLap,
                         KhachHangId = khachhang.Id,
                         TenKhachHang = khachhang.TenKhachHang,
                         TrangThaiId = trangthai.Id,
                         TenTrangThai = trangthai.TenTrangThai,
                         NhanVienId = nv.Id,
                         TenNhanVien = nv.FullName
                     };
            return re.First();
        }
        public PhieuThuViewModel findPhieuThu(int phieuID)
        {
            var re = from phieuthu in _context.PhieuThu
                     join loaiphieu in _context.LoaiPhieu
                     on phieuthu.LoaiPhieuId equals loaiphieu.Id
                     join nv in _context.Staff
                     on phieuthu.NhanVienId equals nv.Id
                     where phieuthu.Id == phieuID
                     select new PhieuThuViewModel
                     {
                         ID = phieuthu.Id,
                         NgayLap = phieuthu.NgayLap,
                         DonHangId = phieuthu.DonHangId,
                         PhieuNhapHangId = phieuthu.PhieuTraNhapHangId,
                         NhanVienId = phieuthu.NhanVienId,
                         TongTien = phieuthu.TongTien,
                         LoaiPhieuId = phieuthu.LoaiPhieuId,
                         TenNhanVien = nv.FullName,
                         TenLoaiPhieu = loaiphieu.TenLoaiPhieu,
                         KhachHangId = phieuthu.KhachHangId
                     };
            return re.First();
        }

        public int? findPhieuTraByDonHang(int donhangID)
        {
            var query = _context.PhieuTraHang.Where(m => m.DonHangId == donhangID).FirstOrDefault();
            if (query == null)
            {
                return null;
            }
            else
            {
                return query.Id;
            }
        }
        public IQueryable<DonHang> GetDonHangWithOutPtra()
        {
            //var query = (from donhang in _context.DonHang
            //             join phieu in _context.PhieuTraHang
            //             on donhang.Id equals phieu.DonHangId
            //             where 
            //             select donhang).DefaultIfEmpty();
            var query = _context.DonHang
                .Include(m => m.KhachHang)
                .Include(m => m.TrangThai)
                .Include(m => m.PhieuTraHang)
                .Where(m => m.PhieuTraHang.Count == 0);
            return query;
        }
        public TraHangViewModel findPhieuTra(int phieuID)
        {
            var re = from phieutra in _context.PhieuTraHang
                     join nv in _context.Staff
                     on phieutra.NhanVienId equals nv.Id
                     join khachhang in _context.KhachHang
                     on phieutra.KhachHangId equals khachhang.Id
                     join donhang in _context.DonHang
                     on phieutra.DonHangId equals donhang.Id
                     where phieutra.Id == phieuID
                     select new TraHangViewModel
                     {
                         ID = phieutra.Id,
                         NgayLap = phieutra.NgayLap,
                         KhachHangId = khachhang.Id,
                         TenKhachHang = khachhang.TenKhachHang,
                         DonHangId = donhang.Id,
                         TongTien = phieutra.TongTien,
                         NhanVienId = nv.Id,
                         TenNhanVien = nv.FullName
                         
                     };
            return re.First();
        }

        public PhieuChiViewModel findPhieuChi(int phieuID)
        {
            var re = from phieuchi in _context.PhieuChi
                     join loaiphieu in _context.LoaiPhieu
                     on phieuchi.LoaiPhieuId equals loaiphieu.Id
                     join nv in _context.Staff
                     on phieuchi.NhanVienId equals nv.Id
                     where phieuchi.Id == phieuID
                     select new PhieuChiViewModel
                     {
                         ID = phieuchi.Id,
                         NgayLap = phieuchi.NgayLap,
                         PhieuTraHangId = phieuchi.PhieuTraHangId,
                         PhieuNhapHangId = phieuchi.PhieuNhapHangId,
                         NhanVienId = phieuchi.NhanVienId,
                         TongTien = phieuchi.TongTien,
                         LoaiPhieuId = phieuchi.LoaiPhieuId,
                         TenNhanVien = nv.FullName,
                         TenLoaiPhieu = loaiphieu.TenLoaiPhieu
                     };
            return re.First();
        }
        public KhachHang findCustomerByDonhang(int donhangId)
        {
            var query = from khachhang in _context.KhachHang
                        join don in _context.DonHang
                        on khachhang.Id equals don.KhachHangId
                        where don.Id == donhangId
                        select khachhang;
            return query.First();
        }
        public KhachHang findCustomerByPhieuTra(int donhangId)
        {
            var query = from khachhang in _context.KhachHang
                        join phieu in _context.PhieuTraHang
                        on khachhang.Id equals phieu.KhachHangId
                        where phieu.Id == donhangId
                        select khachhang;
            return query.First();
        }
        public NhaCungCap findProviderByPhieuTra(int phieuID)
        {
            var query = from ncc in _context.NhaCungCap
                        join phieu in _context.PhieuNhapHang
                        on ncc.Id equals phieu.NhaCungCapId
                        join phieutra in _context.PhieuTraNhapHang
                        on phieu.Id equals phieutra.PhieuNhapHangId
                        where phieutra.Id == phieuID
                        select ncc;
            return query.First();
        }
        public NhaCungCap findProviderByPhieuNhap(int phieuID)
        {
            var query = from ncc in _context.NhaCungCap
                        join phieu in _context.PhieuNhapHang
                        on ncc.Id equals phieu.NhaCungCapId
                        where phieu.Id == phieuID
                        select ncc;
            return query.First();
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
        public async Task<ProviderFilterResults> FindProvider(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                var query = from customer in _context.NhaCungCap
                            select new ProviderViewModel
                            {
                                Id = customer.Id,
                                Name = customer.TenNhaCungCap,
                            };
                var queryRe = await query.ToListAsync();
                return new ProviderFilterResults { Results = queryRe };
            }
            if (value.All(char.IsDigit))
            {
                var query1 = from customer in _context.NhaCungCap
                             where customer.Id.ToString().Contains(value)
                             select new ProviderViewModel
                             {
                                 Id = customer.Id,
                                 Name = customer.TenNhaCungCap,
                             };
                var results1 = await query1.ToListAsync();
                return new ProviderFilterResults { Results = results1 };
            }

            var query2 = from customer in _context.NhaCungCap
                         where customer.TenNhaCungCap.Contains(value)
                         select new ProviderViewModel
                         {
                             Id = customer.Id,
                             Name = customer.TenNhaCungCap,
                         };

            var results2 = await query2.ToListAsync();
            return new ProviderFilterResults { Results = results2 };

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
                                 Available = product.TonKho,
                                 ImageUrl = product.ImageUrl

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
                            Available = product.TonKho,
                            ImageUrl = product.ImageUrl
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

        public async Task<Tuple<bool, int>> AddInvoice(InvoiceViewModel invoice,
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
                        return new Tuple<bool, int>(false, -1);
                    }
                }

                //add invoice
                var userId = (from user in _context.Staff
                              where user.UserName == invoice.Staff
                              select user).First().Id;

                //hard code id, edit later
                int statusId = 0;
                
                if (invoice.CustomerPaid >= invoice.TotalValue)
                {
                    statusId = await GetCustomerTransactionStatus("Paid", "KhachHang");
                }
                else if(invoice.CustomerPaid <= 0)
                {
                    statusId = await GetCustomerTransactionStatus("Not-Paid", "KhachHang");
                }
                else 
                {
                    statusId = await GetCustomerTransactionStatus("Semi-Paid", "KhachHang");
                }

                var invoice_ = new DonHang
                {
                    KhachHangId = invoice.CustomerId,
                    NhanVienId = userId,
                    NgayLap = DateTime.Now,
                    TongTien = invoice.TotalValue,
                    TrangThaiId = statusId,
                    ChietKhau = invoice.Discount.GetValueOrDefault()
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
                        LoaiPhieuId = 2, //hard code for tesing, edit later
                        KhachHangId = invoice.CustomerId
                    };
                    invoice_.PhieuThu.Add(receiptVoucher);
                }
                //commit transaction
                _context.SaveChanges();

                return new Tuple<bool, int>(true, invoice_.Id);
            }
            catch (Exception e)
            {
                var error = e;
                return new Tuple<bool, int>(false, -1); ;
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
                    WholeSaleprice = product.GiaBanSi != null ? product.GiaBanSi.Value : 0,
                    ImageUrl = product.ImageUrl
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

        public  CustomerLiabilitesViewModel GetCustomerLiabilites(int id)
        {
            var model = new CustomerLiabilitesViewModel();

            var invoices = from invoice in _context.DonHang
                           where invoice.KhachHangId == id
                           select new DebtViewModel
                           {
                               Id = invoice.Id,
                               DateCreate = invoice.NgayLap,
                               Note = "Đơn hàng",
                               Staff = invoice.NhanVien.FullName,
                               Value = invoice.TongTien
                           };

            var receiptVouchers = from receipt in _context.PhieuThu
                                  where receipt.KhachHangId == id
                                  select new DebtViewModel
                                  {
                                      Id = receipt.Id,
                                      DateCreate = receipt.NgayLap,
                                      Note = "Phiếu thu",
                                      Staff = receipt.NhanVien.FullName,
                                      Value = -receipt.TongTien
                                  };

            var result = invoices.Concat(receiptVouchers);

            model.sourceDebts = result;

            return model;
        }

        //public IQueryable<DonHangViewModel> GetAllDonHang()
        //{
        //    var query = from donhang in _context.DonHang
        //                join kh in _context.KhachHang
        //                on donhang.KhachHangId equals kh.Id
        //                join trangthai in _context.TrangThai
        //                on donhang.TrangThaiId equals trangthai.Id
        //                select new DonHangViewModel
        //                {
        //                    ID = donhang.Id,
        //                    TenKhachHang = kh.TenKhachHang,
        //                    NgayLap = donhang.NgayLap,
        //                    KhachHangId = kh.Id,
        //                    TrangThaiId = trangthai.Id,
        //                    TenTrangThai = trangthai.TenTrangThai,
        //                    TongTien = donhang.TongTien,

        //                };

        //    return query;
        //}

        public IQueryable<DonHang> GetAllDonHang()
        {
            var query = _context.DonHang.Include(m => m.KhachHang)
                .Include(m => m.TrangThai);
            return query;
        }


        public int TaoDonHang(DonHang donhang)
        {
            _context.Add(donhang);
            _context.SaveChanges();

            return donhang.Id;
        }

        public void  UpdateDonHang(int? id)
        {
            decimal totalValue = 0;
            var donhang = _context.DonHang.Where(m => m.Id == id).FirstOrDefault();
            var listPhieuthu = _context.PhieuThu.Where(m => m.DonHangId == donhang.Id).ToList();
            for (int i = 0; i < listPhieuthu.Count; i++)
            {
                totalValue += listPhieuthu[i].TongTien;
            }
            if(totalValue >= donhang.TongTien)
            {
                donhang.TrangThaiId = 2;
            }
            else
            {
                donhang.TrangThaiId = 3;
            }
            _context.SaveChanges();
        }
        public void CapnhatDonhang(int? id, decimal tienthu)
        {
            var donhang = _context.DonHang.Where(m => m.Id == id).FirstOrDefault();
            if (tienthu >= donhang.TongTien)
            {
                donhang.TrangThaiId = 12; //da thanh toan
            }
            if (tienthu < donhang.TongTien)
            {
                donhang.TrangThaiId = 11;   //thanh toan mot phan
            }
            _context.SaveChanges();
        }
        public int findPhieuTraByCustomer(int? customerID)
        {
            var donhangList = from phieutra in _context.PhieuTraHang
                              join khachhang in _context.KhachHang
                              on phieutra.KhachHangId equals khachhang.Id
                              where customerID == khachhang.Id
                              orderby phieutra.NgayLap descending
                              select phieutra;
            var result = donhangList.FirstOrDefault();
            return result.Id;

        }
        public int findPhieuNhapByCustomer(int? customerID)
        {
            var donhangList = from phieunhap in _context.PhieuNhapHang
                              join ncc in _context.NhaCungCap
                              on phieunhap.NhaCungCapId equals ncc.Id
                              where customerID == ncc.Id
                              orderby phieunhap.NgayLap descending
                              select phieunhap;
            var result = donhangList.FirstOrDefault();
            return result.Id;
        }
        public int findDonHangByCustomer(int? customerID)
        {
            var donhangList = from donhang in _context.DonHang
                              join khachhang in _context.KhachHang
                              on donhang.KhachHangId equals khachhang.Id
                              where customerID == khachhang.Id
                              orderby donhang.NgayLap descending
                              select donhang;
            var result = donhangList.FirstOrDefault();
            return result.Id;
        }
        public int findPhieuTraNhapHang(int? providerID)
        {
            var donhangList = from phieu in _context.PhieuTraNhapHang
                              join phieunhap in _context.PhieuNhapHang
                              on phieu.PhieuNhapHangId equals phieunhap.Id
                              join ncc in _context.NhaCungCap
                              on phieunhap.NhaCungCapId equals ncc.Id
                              where providerID == ncc.Id
                              orderby phieu.NgayLap descending
                              select phieu;
            var result = donhangList.FirstOrDefault();
            return result.Id;
        }
        public int findUserId(String name)
        {
            var user = _context.Staff.Where(m => m.UserName == name).FirstOrDefault();
            return user.Id;
        }
        public void TaoPhieuThu(PhieuThu phieuthu)
        {
            _context.PhieuThu.Add(phieuthu);
            _context.SaveChanges();
        }
        public void TaoPhieuTraHang(PhieuTraHang pth)
        {
            _context.PhieuTraHang.Add(pth);
            _context.SaveChanges();
        }
        public void TaoCTPhieuTraHang(ChiTietPhieuTraHang pth)
        {
            _context.ChiTietPhieuTraHang.Add(pth);
            _context.SaveChanges();
        }
        public PhieuTraHang findNewPhieuTraHang()
        {
            var query = from phieu in _context.PhieuTraHang
                        orderby phieu.NgayLap descending
                        select phieu;
            return query.First();
        }
        public void TaoPhieuChi(PhieuChi phieuchi)
        {
            _context.PhieuChi.Add(phieuchi);
            _context.SaveChanges();
        }
        public IQueryable<PhieuThu> findPhieuThuByDonHang(int donHangID)
        {
            var query = from phieu in _context.PhieuThu
                        where phieu.DonHangId == donHangID
                        select phieu;
            return query;
        }
        public IQueryable<CTDonHang> GetCTDonHang(int donhangID)
        {
            var re = from cthang in _context.ChiTietDonHang
                     join hanghoa in _context.HangHoa
                     on cthang.HangHoaId equals hanghoa.Id
                     where cthang.DonHangId == donhangID
                     select new CTDonHang
                     {
                         ID = cthang.Id,
                         DonHangId = donhangID,
                         HangHoaId = hanghoa.Id,
                         TenHangHoa = hanghoa.TenHangHoa,
                         SoLuong = cthang.SoLuong,
                         GiaBan = cthang.GiaBan
                     };
            return re;
        }
        public IQueryable<TraHangDetailViewModel> GetCTTraHang(int phieutraID)
        {
            var re = from cthang in _context.ChiTietPhieuTraHang
                     join ctdonhag in _context.ChiTietDonHang
                     on cthang.ChiTietDonHangId equals ctdonhag.Id
                     join hanghoa in _context.HangHoa
                     on ctdonhag.HangHoaId equals hanghoa.Id
                     where cthang.PhieuTraHangId == phieutraID
                     select new TraHangDetailViewModel
                     {
                         ID = cthang.Id,
                         TraHangId = cthang.PhieuTraHangId,
                         HangHoaId = hanghoa.Id,
                         TenHangHoa = hanghoa.TenHangHoa,
                         SoLuong = cthang.SoLuong,
                         GiaTra = cthang.GiaTra
                     };
            return re;
        }
        public IQueryable<PhieuThuViewModel> GetAllPhieuThu()
        {
            var re = from phieuthu in _context.PhieuThu
                     join loaiphieu in _context.LoaiPhieu
                     on phieuthu.LoaiPhieuId equals loaiphieu.Id
                     join nv in _context.Staff
                     on phieuthu.NhanVienId equals nv.Id
                     select new PhieuThuViewModel
                     {
                         ID = phieuthu.Id,
                         NgayLap = phieuthu.NgayLap,
                         DonHangId = phieuthu.DonHangId,
                         PhieuNhapHangId = phieuthu.PhieuTraNhapHangId,
                         NhanVienId = phieuthu.NhanVienId,
                         TongTien = phieuthu.TongTien,
                         LoaiPhieuId = phieuthu.LoaiPhieuId,
                         TenNhanVien = nv.FullName,
                         TenLoaiPhieu = loaiphieu.TenLoaiPhieu,
                         KhachHangId = phieuthu.KhachHangId
                     };
            return re;
        }
        public IQueryable<PhieuChiViewModel> GetAllPhieuChi()
        {
            var re = from phieuchi in _context.PhieuChi
                     join loaiphieu in _context.LoaiPhieu
                     on phieuchi.LoaiPhieuId equals loaiphieu.Id
                     join nv in _context.Staff
                     on phieuchi.NhanVienId equals nv.Id
                     select new PhieuChiViewModel
                     {
                         ID = phieuchi.Id,
                         NgayLap = phieuchi.NgayLap,
                         PhieuTraHangId = phieuchi.PhieuTraHangId,
                         PhieuNhapHangId = phieuchi.PhieuNhapHangId,
                         NhanVienId = phieuchi.NhanVienId,
                         TongTien = phieuchi.TongTien,
                         LoaiPhieuId = phieuchi.LoaiPhieuId,
                         TenNhanVien = nv.FullName,
                         TenLoaiPhieu = loaiphieu.TenLoaiPhieu
                     };
            return re;
        }
        public IQueryable<TraHangViewModel> GetAllPhieuTraHang()
        {
            var re = from phieu in _context.PhieuTraHang
                     join khachhang in _context.KhachHang
                     on phieu.KhachHangId equals khachhang.Id
                     join donhang in _context.DonHang
                     on phieu.DonHangId equals donhang.Id
                     join nv in _context.Staff
                     on phieu.NhanVienId equals nv.Id
                     select new TraHangViewModel
                     {
                        ID = phieu.Id,
                        NgayLap = phieu.NgayLap,
                        KhachHangId = khachhang.Id,
                        TenKhachHang = khachhang.TenKhachHang,
                        DonHangId = donhang.Id,
                        TongTien = phieu.TongTien,
                        NhanVienId = nv.Id,
                        TenNhanVien = nv.FullName,
                        GhiChu = phieu.GhiChu
                     };
            return re;
        }

        public async Task<Staff> GetStaffByUserName(string userName)
        {
            return await _context.Staff
                .SingleOrDefaultAsync(m => m.UserName == userName);
        }

        public async Task<BillViewModel> GetBill(int id)
        {
            var bill = await _context.DonHang.Where(i => i.Id == id).FirstOrDefaultAsync();

            var billDetails = await _context.ChiTietDonHang.Where(i => i.DonHangId == id)
                                                           .Select(d => new ProductBuyingDetailsViewModel
                                                           {
                                                               ProductName = d.HangHoa.TenHangHoa,
                                                               Count = d.SoLuong,
                                                               Price = d.GiaBan,
                                                               TotalValue = d.SoLuong * d.GiaBan
                                                           }).ToListAsync();

            var receiptVoucher = await _context.PhieuThu.Where(i => i.DonHangId == id)
                                                        .OrderByDescending(u => u.NgayLap)
                                                        .FirstOrDefaultAsync();

            var staff = await _context.Staff.Where(s => s.Id == bill.NhanVienId).FirstAsync();
            var customer = await _context.KhachHang.Where(c => c.Id == bill.KhachHangId).FirstAsync();

            var result = new BillViewModel
            {
                ID = bill.Id.ToString(),
                Staff = staff.FullName,
                DateCreate = DateTime.Now,
                TotalValue = bill.TongTien,
                Discount = bill.ChietKhau.GetValueOrDefault(),
                TotalValueAfterDiscount = bill.TongTien - bill.ChietKhau.GetValueOrDefault(),
                ProductDetail = billDetails
            };

            result.Customer = new CustomerViewModel
            {
                ID = customer.Id,
                Name = customer.TenKhachHang,
                Address = customer.DiaChi,
                Phone = customer.SoDienThoai
            };

            return result;
        }

        public async Task<int> GetCustomerTransactionStatus(string shortcut, string type)
        {            
            var query = await _context.TrangThai
                .Where(t => t.VietTat == shortcut && t.Loai == type).FirstAsync();

           return query.Id;                     
        }
    }
}
