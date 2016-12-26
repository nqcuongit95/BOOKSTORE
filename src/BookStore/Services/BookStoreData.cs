using BookStore.Entities;
using BookStore.Models;
using BookStore.ViewModels;
using BookStore.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class BookStoreData : IBookStoreData
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
                            TotalValues = invoice.TongTien
                        };

            var invoices = await query.ToListAsync();
            var totalInvoices = await query.CountAsync();
            var totalValues = await query.SumAsync(inv => inv.TotalValues);

            model.Invoices = invoices;
            model.TotalInvoices = totalInvoices;
            model.TotalValues = totalValues;

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
                var query1 = from customer in _context.KhachHang
                             where customer.SoDienThoai.Contains(value)
                             select new CustomerFilterViewModel
                             {
                                 Id = customer.Id,
                                 Name = customer.TenKhachHang,
                                 Phone = customer.SoDienThoai,
                                 Address = customer.DiaChi

                             };
                var results1 = await query1.ToListAsync();
                return new CustomerFilterResults { Results = results1 };
            }

            var query2 = from customer in _context.KhachHang
                         where customer.TenKhachHang.Contains(value)
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
                        DonHangId = invoice_.Id,
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

                }

                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public IQueryable<DonHangViewModel> GetAllDonHang()
        {
            var query = from donhang in _context.DonHang
                        join kh in _context.KhachHang
                        on donhang.KhachHangId equals kh.Id
                        join trangthai in _context.TrangThai
                        on donhang.TrangThaiId equals trangthai.Id
                        select new DonHangViewModel
                        {
                            ID = donhang.Id,
                            TenKhachHang = kh.TenKhachHang,
                            NgayLap = donhang.NgayLap,
                            KhachHangId = kh.Id,
                            TrangThaiId = trangthai.Id,
                            TenTrangThai = trangthai.TenTrangThai,
                            TongTien = donhang.TongTien,

                        };

            return query;
        }

        public IQueryable<LoaiPhieu> GetAllLoaiPhieu()
        {
            var query = from donhang in _context.DonHang
                        join kh in _context.KhachHang
                        on donhang.KhachHangId equals kh.Id
                        join trangthai in _context.TrangThai
                        on donhang.TrangThaiId equals trangthai.Id
                        select new LoaiPhieu
                        {
                            //ID = donhang.Id,
                            //TenKhachHang = kh.TenKhachHang,
                            //NgayLap = donhang.NgayLap,
                            //KhachHangId = kh.Id,
                            //TrangThaiId = trangthai.Id,
                            //TenTrangThai = trangthai.TenTrangThai,
                            //TongTien = donhang.TongTien,

                        };

            return query;
        }
        public IQueryable<PhieuTraNhapHang> GetAllPhieuTraNhapHang()
        {
            var query = from donhang in _context.DonHang
                        join kh in _context.KhachHang
                        on donhang.KhachHangId equals kh.Id
                        join trangthai in _context.TrangThai
                        on donhang.TrangThaiId equals trangthai.Id
                        select new PhieuTraNhapHang
                        {
                            //ID = donhang.Id,
                            //TenKhachHang = kh.TenKhachHang,
                            //NgayLap = donhang.NgayLap,
                            //KhachHangId = kh.Id,
                            //TrangThaiId = trangthai.Id,
                            //TenTrangThai = trangthai.TenTrangThai,
                            //TongTien = donhang.TongTien,

                        };

            return query;
        }
        public int TaoDonHang(DonHang donhang)
        {
            _context.Add(donhang);
            _context.SaveChanges();

            return donhang.Id;
        }

        public int TaoPhieuThu(PhieuThu phieuthu)
        {
            _context.Add(phieuthu);
            _context.SaveChanges();

            return phieuthu.Id;
        }

        public int TaoPhieuChi(PhieuChi phieuchi)
        {
            _context.Add(phieuchi);
            _context.SaveChanges();

            return phieuchi.Id;
        }

        public IQueryable<PhieuThuViewModel> GetAllPhieuThu()
        {
            var query = from phieuthu in _context.PhieuThu
                        join donhang in _context.DonHang
                        on phieuthu.DonHangId equals donhang.Id
                        join phieunhap in _context.PhieuTraNhapHang
                        on phieuthu.PhieuTraNhapHangId equals phieunhap.Id
                        join loaiphieu in _context.LoaiPhieu
                        on phieuthu.LoaiPhieuId equals loaiphieu.Id
                        join khachhang in _context.KhachHang
                        on donhang.KhachHangId equals khachhang.Id
                        select new PhieuThuViewModel
                        {
                            ID = phieuthu.Id,
                           NgayLap = phieuthu.NgayLap,
                           DonHangId = donhang.Id,
                           PhieuNhapHangId = phieuthu.PhieuTraNhapHangId,
                           TongTien = phieuthu.TongTien,
                           LoaiPhieuId = phieuthu.LoaiPhieuId,
                           TenLoaiPhieu = loaiphieu.TenLoaiPhieu,
                        };
            return query;
        }
        public IQueryable<PhieuChiViewModel> GetAllPhieuChi()
        {
            var query = from phieuchi in _context.PhieuChi
                        join trahang in _context.PhieuTraHang
                        on phieuchi.PhieuTraHangId equals trahang.Id
                        join phieunhap in _context.PhieuNhapHang
                        on phieuchi.PhieuNhapHangId equals phieunhap.Id
                        join loaiphieu in _context.LoaiPhieu
                        on phieuchi.LoaiPhieuId equals loaiphieu.Id
                        join khachhang in _context.KhachHang
                        on trahang.KhachHangId equals khachhang.Id
                        select new PhieuChiViewModel
                        {
                            ID = phieuchi.Id,
                            NgayLap = phieuchi.NgayLap,
                            PhieuNhapHangId = phieuchi.PhieuNhapHangId,
                            TongTien = phieuchi.TongTien,
                            LoaiPhieuId = phieuchi.LoaiPhieuId,
                            TenLoaiPhieu = loaiphieu.TenLoaiPhieu,
                        };
            return query;
        }
    }
}
