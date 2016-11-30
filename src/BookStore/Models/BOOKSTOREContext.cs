using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BookStore.Models
{
    public partial class BOOKSTOREContext : DbContext
    {
        public virtual DbSet<ChiTietDonHang> ChiTietDonHang { get; set; }
        public virtual DbSet<ChiTietHangHoa> ChiTietHangHoa { get; set; }
        public virtual DbSet<ChiTietPhieuKiemKho> ChiTietPhieuKiemKho { get; set; }
        public virtual DbSet<ChiTietPhieuNhapHang> ChiTietPhieuNhapHang { get; set; }
        public virtual DbSet<ChiTietPhieuTraHang> ChiTietPhieuTraHang { get; set; }
        public virtual DbSet<DonHang> DonHang { get; set; }
        public virtual DbSet<HangHoa> HangHoa { get; set; }
        public virtual DbSet<KhachHang> KhachHang { get; set; }
        public virtual DbSet<LoaiHangHoa> LoaiHangHoa { get; set; }
        public virtual DbSet<LoaiKhachHang> LoaiKhachHang { get; set; }
        public virtual DbSet<NhaCungCap> NhaCungCap { get; set; }
        public virtual DbSet<NhanHieu> NhanHieu { get; set; }
        public virtual DbSet<PhieuKiemKho> PhieuKiemKho { get; set; }
        public virtual DbSet<PhieuNhapHang> PhieuNhapHang { get; set; }
        public virtual DbSet<PhieuTraHang> PhieuTraHang { get; set; }
        public virtual DbSet<ThuocTinhHangHoa> ThuocTinhHangHoa { get; set; }
        public virtual DbSet<TrangThai> TrangThai { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");

            IConfigurationRoot connectionStringConfig = builder.Build();

            optionsBuilder.UseSqlServer(connectionStringConfig.GetConnectionString("BookStore"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

                entity.Property(e => e.NhaCungCapId).HasColumnName("NhaCungCapID");

                entity.Property(e => e.PhieuNhapHangId).HasColumnName("PhieuNhapHangID");

                entity.HasOne(d => d.HangHoa)
                    .WithMany(p => p.ChiTietPhieuNhapHang)
                    .HasForeignKey(d => d.HangHoaId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ChiTietPhieuNhapHang_HangHoa");

                entity.HasOne(d => d.NhaCungCap)
                    .WithMany(p => p.ChiTietPhieuNhapHang)
                    .HasForeignKey(d => d.NhaCungCapId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ChiTietPhieuNhapHang_NhaCungCap");

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

            modelBuilder.Entity<DonHang>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.KhachHangId).HasColumnName("KhachHangID");

                entity.Property(e => e.NgayLap).HasColumnType("datetime");

                entity.Property(e => e.TongTien).HasColumnType("money");

                entity.Property(e => e.TrangThaiId).HasColumnName("TrangThaiID");

                entity.HasOne(d => d.KhachHang)
                    .WithMany(p => p.DonHang)
                    .HasForeignKey(d => d.KhachHangId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_DonHang_KhachHang");

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

                entity.Property(e => e.NgayTao).HasColumnType("date");

                entity.Property(e => e.NhaCungCapId).HasColumnName("NhaCungCapID");

                entity.Property(e => e.NhanHieuId).HasColumnName("NhanHieuID");

                entity.Property(e => e.TenHangHoa)
                    .IsRequired()
                    .HasMaxLength(50);

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

            modelBuilder.Entity<PhieuKiemKho>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.NgayLap).HasColumnType("datetime");
            });

            modelBuilder.Entity<PhieuNhapHang>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.NgayLap).HasColumnType("datetime");

                entity.Property(e => e.TongTien).HasColumnType("money");
            });

            modelBuilder.Entity<PhieuTraHang>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DonHangId).HasColumnName("DonHangID");

                entity.Property(e => e.GhiChu).HasMaxLength(100);

                entity.Property(e => e.KhachHangId).HasColumnName("KhachHangID");

                entity.Property(e => e.NgayLap).HasColumnType("datetime");

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

                entity.Property(e => e.TenTrangThai)
                    .IsRequired()
                    .HasMaxLength(100);
            });
        }
    }
}