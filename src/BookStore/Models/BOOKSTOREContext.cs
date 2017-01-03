using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BookStore.Entities;

namespace BookStore.Models
{
    public partial class BOOKSTOREContext : IdentityDbContext<Staff, Role, int>
    {
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<ChiTietDonHang> ChiTietDonHang { get; set; }
        public virtual DbSet<ChiTietHangHoa> ChiTietHangHoa { get; set; }
        public virtual DbSet<ChiTietPhieuKiemKho> ChiTietPhieuKiemKho { get; set; }
        public virtual DbSet<ChiTietPhieuNhapHang> ChiTietPhieuNhapHang { get; set; }
        public virtual DbSet<ChiTietPhieuTraHang> ChiTietPhieuTraHang { get; set; }
        public virtual DbSet<ChiTietPhieuTraNhapHang> ChiTietPhieuTraNhapHang { get; set; }
        public virtual DbSet<DonHang> DonHang { get; set; }
        public virtual DbSet<HangHoa> HangHoa { get; set; }
        public virtual DbSet<KhachHang> KhachHang { get; set; }
        public virtual DbSet<LoaiHangHoa> LoaiHangHoa { get; set; }
        public virtual DbSet<LoaiKhachHang> LoaiKhachHang { get; set; }
        public virtual DbSet<LoaiPhieu> LoaiPhieu { get; set; }
        public virtual DbSet<NhaCungCap> NhaCungCap { get; set; }
        public virtual DbSet<NhanHieu> NhanHieu { get; set; }
        public virtual DbSet<PhieuChi> PhieuChi { get; set; }
        public virtual DbSet<PhieuKiemKho> PhieuKiemKho { get; set; }
        public virtual DbSet<PhieuNhanHang> PhieuNhanHang { get; set; }
        public virtual DbSet<PhieuNhapHang> PhieuNhapHang { get; set; }
        public virtual DbSet<PhieuThu> PhieuThu { get; set; }
        public virtual DbSet<PhieuTraHang> PhieuTraHang { get; set; }
        public virtual DbSet<PhieuTraNhapHang> PhieuTraNhapHang { get; set; }
        public virtual DbSet<ThuocTinhHangHoa> ThuocTinhHangHoa { get; set; }
        public virtual DbSet<TrangThai> TrangThai { get; set; }

        public BOOKSTOREContext(DbContextOptions<BOOKSTOREContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ChiTietDonHang>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DonHangId).HasColumnName("DonHangID");

                entity.Property(e => e.GiaBan).HasColumnType("money");

                entity.Property(e => e.HangHoaId).HasColumnName("HangHoaID");

                entity.HasOne(d => d.DonHang)
                    .WithMany(p => p.ChiTietDonHang)
                    .HasForeignKey(d => d.DonHangId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ChiTietDonHang_DonHang");

                entity.HasOne(d => d.HangHoa)
                    .WithMany(p => p.ChiTietDonHang)
                    .HasForeignKey(d => d.HangHoaId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ChiTietDonHang_HangHoa");
            });

            modelBuilder.Entity<ChiTietHangHoa>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.GiaTri).HasMaxLength(100);

                entity.Property(e => e.HangHoaId).HasColumnName("HangHoaID");

                entity.Property(e => e.ThuocTinh)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.HangHoa)
                    .WithMany(p => p.ChiTietHangHoa)
                    .HasForeignKey(d => d.HangHoaId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ChiTietHangHoa_HangHoa");
            });

            modelBuilder.Entity<ChiTietPhieuKiemKho>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.HangHoaId).HasColumnName("HangHoaID");

                entity.Property(e => e.LyDo).HasMaxLength(100);

                entity.Property(e => e.PhieuKiemKhoId).HasColumnName("PhieuKiemKhoID");

                entity.HasOne(d => d.HangHoa)
                    .WithMany(p => p.ChiTietPhieuKiemKho)
                    .HasForeignKey(d => d.HangHoaId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ChiTietPhieuKiemKho_HangHoa");

                entity.HasOne(d => d.PhieuKiemKho)
                    .WithMany(p => p.ChiTietPhieuKiemKho)
                    .HasForeignKey(d => d.PhieuKiemKhoId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ChiTietPhieuKiemKho_PhieuKiemKho");
            });

            modelBuilder.Entity<ChiTietPhieuNhapHang>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.GiaNhap).HasColumnType("money");

                entity.Property(e => e.HangHoaId).HasColumnName("HangHoaID");

                entity.Property(e => e.PhieuNhapHangId).HasColumnName("PhieuNhapHangID");

                entity.HasOne(d => d.HangHoa)
                    .WithMany(p => p.ChiTietPhieuNhapHang)
                    .HasForeignKey(d => d.HangHoaId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ChiTietPhieuNhapHang_HangHoa");

                entity.HasOne(d => d.PhieuNhapHang)
                    .WithMany(p => p.ChiTietPhieuNhapHang)
                    .HasForeignKey(d => d.PhieuNhapHangId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ChiTietPhieuNhapHang_PhieuNhapHang");
            });

            modelBuilder.Entity<ChiTietPhieuTraHang>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ChiTietDonHangId).HasColumnName("ChiTietDonHangID");

                entity.Property(e => e.GiaTra).HasColumnType("money");

                entity.Property(e => e.PhieuTraHangId).HasColumnName("PhieuTraHangID");

                entity.HasOne(d => d.PhieuTraHang)
                    .WithMany(p => p.ChiTietPhieuTraHang)
                    .HasForeignKey(d => d.PhieuTraHangId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ChiTietPhieuTraHang_PhieuTraHang");
            });

            modelBuilder.Entity<ChiTietPhieuTraNhapHang>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ChiTietPhieuNhapHangId).HasColumnName("ChiTietPhieuNhapHangID");

                entity.Property(e => e.GiaTra).HasColumnType("money");

                entity.Property(e => e.PhieuTraNhapHangId).HasColumnName("PhieuTraNhapHangID");

                entity.HasOne(d => d.ChiTietPhieuNhapHang)
                    .WithMany(p => p.ChiTietPhieuTraNhapHang)
                    .HasForeignKey(d => d.ChiTietPhieuNhapHangId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ChiTietPhieuTraNhapHang_ChiTietPhieuNhapHang");

                entity.HasOne(d => d.PhieuTraNhapHang)
                    .WithMany(p => p.ChiTietPhieuTraNhapHang)
                    .HasForeignKey(d => d.PhieuTraNhapHangId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ChiTietPhieuTraNhapHang_PhieuTraNhapHang");
            });

            modelBuilder.Entity<DonHang>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.KhachHangId).HasColumnName("KhachHangID");

                entity.Property(e => e.NgayLap).HasColumnType("datetime");

                entity.Property(e => e.NhanVienId).HasColumnName("NhanVienID");

                entity.Property(e => e.TongTien).HasColumnType("money");

                entity.Property(e => e.TrangThaiId).HasColumnName("TrangThaiID");

                entity.HasOne(d => d.KhachHang)
                    .WithMany(p => p.DonHang)
                    .HasForeignKey(d => d.KhachHangId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_DonHang_KhachHang");

                entity.HasOne(d => d.NhanVien)
                    .WithMany(p => p.DonHang)
                    .HasForeignKey(d => d.NhanVienId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_DonHang_Staff");

                entity.HasOne(d => d.TrangThai)
                    .WithMany(p => p.DonHang)
                    .HasForeignKey(d => d.TrangThaiId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_DonHang_TrangThai");
            });

            modelBuilder.Entity<HangHoa>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.GiaBanLe).HasColumnType("money");

                entity.Property(e => e.GiaBanSi).HasColumnType("money");

                entity.Property(e => e.GiaKhoiTao).HasColumnType("money");

                entity.Property(e => e.GiaNhap).HasColumnType("money");

                entity.Property(e => e.NgayLap).HasColumnType("date");

                entity.Property(e => e.NhaCungCapId).HasColumnName("NhaCungCapID");

                entity.Property(e => e.NhanHieuId).HasColumnName("NhanHieuID");

                entity.Property(e => e.TenHangHoa)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.TrangThaiId).HasColumnName("TrangThaiID");

                entity.HasOne(d => d.LoaiHangHoa)
                    .WithMany(p => p.HangHoa)
                    .HasForeignKey(d => d.LoaiHangHoaId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_HangHoa_LoaiHangHoa");

                entity.HasOne(d => d.NhaCungCap)
                    .WithMany(p => p.HangHoa)
                    .HasForeignKey(d => d.NhaCungCapId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_HangHoa_NhaCungCap");

                entity.HasOne(d => d.NhanHieu)
                    .WithMany(p => p.HangHoa)
                    .HasForeignKey(d => d.NhanHieuId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_HangHoa_NhanHieu");

                entity.HasOne(d => d.TrangThai)
                    .WithMany(p => p.HangHoa)
                    .HasForeignKey(d => d.TrangThaiId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_HangHoa_TrangThai");
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DiaChi).HasMaxLength(60);

                entity.Property(e => e.Email).HasColumnType("varchar(50)");

                entity.Property(e => e.LoaiKhachHangId).HasColumnName("LoaiKhachHangID");

                entity.Property(e => e.NgayLap).HasColumnType("datetime");

                entity.Property(e => e.SoDienThoai)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.TenKhachHang)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.LoaiKhachHang)
                    .WithMany(p => p.KhachHang)
                    .HasForeignKey(d => d.LoaiKhachHangId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_KhachHang_LoaiKhachHang");
            });

            modelBuilder.Entity<LoaiHangHoa>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.TenLoaiHangHoa)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<LoaiKhachHang>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.TenLoaiKhachHang)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<LoaiPhieu>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Loai)
                    .IsRequired()
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.TenLoaiPhieu)
                    .IsRequired()
                    .HasMaxLength(80);
            });

            modelBuilder.Entity<NhaCungCap>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.NgayLap).HasColumnType("date");

                entity.Property(e => e.TenNhaCungCap)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<NhanHieu>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.TenNhanHieu)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PhieuChi>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.GhiChu).HasMaxLength(100);

                entity.Property(e => e.LoaiPhieuId).HasColumnName("LoaiPhieuID");

                entity.Property(e => e.NgayLap).HasColumnType("datetime");

                entity.Property(e => e.NhanVienId).HasColumnName("NhanVienID");

                entity.Property(e => e.PhieuNhapHangId).HasColumnName("PhieuNhapHangID");

                entity.Property(e => e.PhieuTraHangId).HasColumnName("PhieuTraHangID");

                entity.Property(e => e.TongTien).HasColumnType("money");

                entity.HasOne(d => d.LoaiPhieu)
                    .WithMany(p => p.PhieuChi)
                    .HasForeignKey(d => d.LoaiPhieuId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PhieuChi_LoaiPhieu");

                entity.HasOne(d => d.NhanVien)
                    .WithMany(p => p.PhieuChi)
                    .HasForeignKey(d => d.NhanVienId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PhieuChi_Staff");

                entity.HasOne(d => d.PhieuNhapHang)
                    .WithMany(p => p.PhieuChi)
                    .HasForeignKey(d => d.PhieuNhapHangId)
                    .HasConstraintName("FK_PhieuChi_PhieuNhapHang");

                entity.HasOne(d => d.PhieuTraHang)
                    .WithMany(p => p.PhieuChi)
                    .HasForeignKey(d => d.PhieuTraHangId)
                    .HasConstraintName("FK_PhieuChi_PhieuTraHang");
            });

            modelBuilder.Entity<PhieuKiemKho>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.GhiChu).HasMaxLength(100);

                entity.Property(e => e.NgayLap).HasColumnType("datetime");

                entity.Property(e => e.NhanVienId).HasColumnName("NhanVienID");

                entity.HasOne(d => d.NhanVien)
                    .WithMany(p => p.PhieuKiemKho)
                    .HasForeignKey(d => d.NhanVienId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PhieuKiemKho_Staff");
            });

            modelBuilder.Entity<PhieuNhanHang>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.GhiChu).HasMaxLength(100);

                entity.Property(e => e.NgayLap).HasColumnType("datetime");

                entity.Property(e => e.NhanVienId).HasColumnName("NhanVienID");

                entity.Property(e => e.PhieuNhapHangId).HasColumnName("PhieuNhapHangID");

                entity.Property(e => e.TongTien).HasColumnType("money");

                entity.HasOne(d => d.NhanVien)
                    .WithMany(p => p.PhieuNhanHang)
                    .HasForeignKey(d => d.NhanVienId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PhieuNhanHang_Staff");

                entity.HasOne(d => d.PhieuNhapHang)
                    .WithMany(p => p.PhieuNhanHang)
                    .HasForeignKey(d => d.PhieuNhapHangId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PhieuNhanHang_PhieuNhapHang");
            });

            modelBuilder.Entity<PhieuNhapHang>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.GhiChu).HasMaxLength(100);

                entity.Property(e => e.NgayLap).HasColumnType("datetime");

                entity.Property(e => e.NhaCungCapId).HasColumnName("NhaCungCapID");

                entity.Property(e => e.NhanVienId).HasColumnName("NhanVienID");

                entity.Property(e => e.TongTien).HasColumnType("money");

                entity.Property(e => e.TrangThaiId).HasColumnName("TrangThaiID");

                entity.HasOne(d => d.NhaCungCap)
                    .WithMany(p => p.PhieuNhapHang)
                    .HasForeignKey(d => d.NhaCungCapId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PhieuNhapHang_NhaCungCap");

                entity.HasOne(d => d.NhanVien)
                    .WithMany(p => p.PhieuNhapHang)
                    .HasForeignKey(d => d.NhanVienId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PhieuNhapHang_Staff");

                entity.HasOne(d => d.TrangThai)
                    .WithMany(p => p.PhieuNhapHang)
                    .HasForeignKey(d => d.TrangThaiId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PhieuNhapHang_TrangThai");
            });

            modelBuilder.Entity<PhieuThu>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DonHangId).HasColumnName("DonHangID");

                entity.Property(e => e.GhiChu).HasMaxLength(100);

                entity.Property(e => e.LoaiPhieuId).HasColumnName("LoaiPhieuID");

                entity.Property(e => e.NgayLap).HasColumnType("datetime");

                entity.Property(e => e.NhanVienId).HasColumnName("NhanVienID");

                entity.Property(e => e.PhieuTraNhapHangId).HasColumnName("PhieuTraNhapHangID");

                entity.Property(e => e.TongTien).HasColumnType("money");

                entity.HasOne(d => d.DonHang)
                    .WithMany(p => p.PhieuThu)
                    .HasForeignKey(d => d.DonHangId)
                    .HasConstraintName("FK_PhieuThu_DonHang");

                entity.HasOne(d => d.KhachHang)
                    .WithMany(p => p.PhieuThu)
                    .HasForeignKey(d => d.KhachHangId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PhieuThu_KhachHang");

                entity.HasOne(d => d.LoaiPhieu)
                    .WithMany(p => p.PhieuThu)
                    .HasForeignKey(d => d.LoaiPhieuId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PhieuThu_LoaiPhieu");

                entity.HasOne(d => d.NhanVien)
                    .WithMany(p => p.PhieuThu)
                    .HasForeignKey(d => d.NhanVienId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PhieuThu_Staff");

                entity.HasOne(d => d.PhieuTraNhapHang)
                    .WithMany(p => p.PhieuThu)
                    .HasForeignKey(d => d.PhieuTraNhapHangId)
                    .HasConstraintName("FK_PhieuThu_PhieuTraNhapHang");
            });

            modelBuilder.Entity<PhieuTraHang>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DonHangId).HasColumnName("DonHangID");

                entity.Property(e => e.GhiChu).HasMaxLength(100);

                entity.Property(e => e.KhachHangId).HasColumnName("KhachHangID");

                entity.Property(e => e.NgayLap).HasColumnType("datetime");

                entity.Property(e => e.NhanVienId).HasColumnName("NhanVienID");

                entity.Property(e => e.TongTien).HasColumnType("money");

                entity.HasOne(d => d.DonHang)
                    .WithMany(p => p.PhieuTraHang)
                    .HasForeignKey(d => d.DonHangId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PhieuTraHang_DonHang");

                entity.HasOne(d => d.KhachHang)
                    .WithMany(p => p.PhieuTraHang)
                    .HasForeignKey(d => d.KhachHangId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PhieuTraHang_KhachHang");

                entity.HasOne(d => d.NhanVien)
                    .WithMany(p => p.PhieuTraHang)
                    .HasForeignKey(d => d.NhanVienId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PhieuTraHang_Staff");
            });

            modelBuilder.Entity<PhieuTraNhapHang>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.GhiChu).HasMaxLength(100);

                entity.Property(e => e.NgayLap).HasColumnType("datetime");

                entity.Property(e => e.NhanVienId).HasColumnName("NhanVienID");

                entity.Property(e => e.PhieuNhapHangId).HasColumnName("PhieuNhapHangID");

                entity.Property(e => e.TongTien).HasColumnType("money");

                entity.HasOne(d => d.NhanVien)
                    .WithMany(p => p.PhieuTraNhapHang)
                    .HasForeignKey(d => d.NhanVienId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PhieuTraNhapHang_Staff");

                entity.HasOne(d => d.PhieuNhapHang)
                    .WithMany(p => p.PhieuTraNhapHang)
                    .HasForeignKey(d => d.PhieuNhapHangId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PhieuTraNhapHang_PhieuNhapHang");
            });

            modelBuilder.Entity<ThuocTinhHangHoa>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LoaiHangHoaId).HasColumnName("LoaiHangHoaID");

                entity.Property(e => e.TenThuocTinh)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.LoaiHangHoa)
                    .WithMany(p => p.ThuocTinhHangHoa)
                    .HasForeignKey(d => d.LoaiHangHoaId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ThuocTinhHangHoa_LoaiHangHoa");
            });

            modelBuilder.Entity<TrangThai>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Loai).HasColumnType("varchar(20)");

                entity.Property(e => e.TenTrangThai)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.VietTat)
                    .IsRequired()
                    .HasColumnType("varchar(20)");
            });
        }
    }
}