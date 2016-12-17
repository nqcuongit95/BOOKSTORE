using BookStore.Models;
using BookStore.ViewModels;
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

        public int TaoDonHang(DonHang donhang)
        {
            _context.Add(donhang);
            _context.SaveChanges();

            return donhang.Id;
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
        public IQueryable<DonHangViewModel> GetAllPhieuChi()
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
    }
}
