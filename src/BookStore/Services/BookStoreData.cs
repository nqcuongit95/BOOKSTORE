using BookStore.Entities;
using BookStore.Models;
using BookStore.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
