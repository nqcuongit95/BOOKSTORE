using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BookStore.Models
{
    public partial class BOOKSTOREContext : DbContext
    {
        public virtual DbSet<Chitietdonhang> Chitietdonhang { get; set; }
        public virtual DbSet<Chitietphieunhap> Chitietphieunhap { get; set; }
        public virtual DbSet<Chitietphieutra> Chitietphieutra { get; set; }
        public virtual DbSet<Donhang> Donhang { get; set; }
        public virtual DbSet<Hanghoa> Hanghoa { get; set; }
        public virtual DbSet<Hinhthucvanchuyen> Hinhthucvanchuyen { get; set; }
        public virtual DbSet<Khachhang> Khachhang { get; set; }
        public virtual DbSet<Nhacungcap> Nhacungcap { get; set; }
        public virtual DbSet<Nhasanxuat> Nhasanxuat { get; set; }
        public virtual DbSet<Nhaxuatban> Nhaxuatban { get; set; }
        public virtual DbSet<Phieunhaphang> Phieunhaphang { get; set; }
        public virtual DbSet<Phieutrahang> Phieutrahang { get; set; }
        public virtual DbSet<Quocgia> Quocgia { get; set; }
        public virtual DbSet<Sach> Sach { get; set; }
        public virtual DbSet<Tacgia> Tacgia { get; set; }
        public virtual DbSet<Theloaisach> Theloaisach { get; set; }
        public virtual DbSet<Trangthai> Trangthai { get; set; }
        public virtual DbSet<Vanphongpham> Vanphongpham { get; set; }        

        public BOOKSTOREContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chitietdonhang>(entity =>
            {
                entity.HasKey(e => e.MaCtdh)
                    .HasName("PK_CHITIETDONHANG");

                entity.ToTable("CHITIETDONHANG");

                entity.Property(e => e.MaCtdh)
                    .HasColumnName("MaCTDH")
                    .ValueGeneratedNever();

                entity.Property(e => e.GiaBan).HasColumnType("money");

                entity.HasOne(d => d.MaDonHangNavigation)
                    .WithMany(p => p.Chitietdonhang)
                    .HasForeignKey(d => d.MaDonHang)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CHITIETDONHANG_DONHANG");

                entity.HasOne(d => d.MaHangHoaNavigation)
                    .WithMany(p => p.Chitietdonhang)
                    .HasForeignKey(d => d.MaHangHoa)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CHITIETDONHANG_HANGHOA");
            });

            modelBuilder.Entity<Chitietphieunhap>(entity =>
            {
                entity.HasKey(e => e.MaCtpn)
                    .HasName("PK_CHITIETPHIEUNHAP");

                entity.ToTable("CHITIETPHIEUNHAP");

                entity.Property(e => e.MaCtpn)
                    .HasColumnName("MaCTPN")
                    .ValueGeneratedNever();

                entity.Property(e => e.GiaNhap).HasColumnType("money");

                entity.HasOne(d => d.MaHangHoaNavigation)
                    .WithMany(p => p.Chitietphieunhap)
                    .HasForeignKey(d => d.MaHangHoa)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CHITIETPHIEUNHAP_HANGHOA");

                entity.HasOne(d => d.MaPhieuNhapNavigation)
                    .WithMany(p => p.Chitietphieunhap)
                    .HasForeignKey(d => d.MaPhieuNhap)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CHITIETPHIEUNHAP_PHIEUNHAPHANG");
            });

            modelBuilder.Entity<Chitietphieutra>(entity =>
            {
                entity.HasKey(e => e.MaCtpt)
                    .HasName("PK_CHITIETPHIEUTRA");

                entity.ToTable("CHITIETPHIEUTRA");

                entity.Property(e => e.MaCtpt)
                    .HasColumnName("MaCTPT")
                    .ValueGeneratedNever();

                entity.Property(e => e.MaCtdh).HasColumnName("MaCTDH");

                entity.HasOne(d => d.MaCtdhNavigation)
                    .WithMany(p => p.Chitietphieutra)
                    .HasForeignKey(d => d.MaCtdh)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CHITIETPHIEUTRA_CHITIETDONHANG");

                entity.HasOne(d => d.MaPhieuTraNavigation)
                    .WithMany(p => p.Chitietphieutra)
                    .HasForeignKey(d => d.MaPhieuTra)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CHITIETPHIEUTRA_PHIEUTRAHANG");
            });

            modelBuilder.Entity<Donhang>(entity =>
            {
                entity.HasKey(e => e.MaDonHang)
                    .HasName("PK_DONHANG");

                entity.ToTable("DONHANG");

                entity.Property(e => e.MaDonHang).ValueGeneratedNever();

                entity.Property(e => e.NgayGiaoHang).HasColumnType("datetime");

                entity.Property(e => e.NgayLap).HasColumnType("datetime");

                entity.Property(e => e.TongTien).HasColumnType("money");

                entity.HasOne(d => d.MaKhachHangNavigation)
                    .WithMany(p => p.Donhang)
                    .HasForeignKey(d => d.MaKhachHang)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_DONHANG_KHACHHANG");

                entity.HasOne(d => d.MaTrangThaiNavigation)
                    .WithMany(p => p.Donhang)
                    .HasForeignKey(d => d.MaTrangThai)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_DONHANG_TRANGTHAI");

                entity.HasOne(d => d.MaVanChuyenNavigation)
                    .WithMany(p => p.Donhang)
                    .HasForeignKey(d => d.MaVanChuyen)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_DONHANG_HINHTHUCVANCHUYEN");
            });

            modelBuilder.Entity<Hanghoa>(entity =>
            {
                entity.HasKey(e => e.MaHangHoa)
                    .HasName("PK_HANGHOA");

                entity.ToTable("HANGHOA");

                entity.Property(e => e.MaHangHoa).ValueGeneratedNever();

                entity.Property(e => e.GiaBan).HasColumnType("money");

                entity.Property(e => e.GiaNhap).HasColumnType("money");

                entity.HasOne(d => d.MaHangHoaNavigation)
                    .WithOne(p => p.Hanghoa)
                    .HasForeignKey<Hanghoa>(d => d.MaHangHoa)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_HANGHOA_SACH");

                entity.HasOne(d => d.MaHangHoa1)
                    .WithOne(p => p.Hanghoa)
                    .HasForeignKey<Hanghoa>(d => d.MaHangHoa)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_HANGHOA_VANPHONGPHAM");

                entity.HasOne(d => d.MaTrangThaiNavigation)
                    .WithMany(p => p.Hanghoa)
                    .HasForeignKey(d => d.MaTrangThai)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_HANGHOA_TRANGTHAI");
            });

            modelBuilder.Entity<Hinhthucvanchuyen>(entity =>
            {
                entity.HasKey(e => e.MaVanChuyen)
                    .HasName("PK_HINHTHUCVANCHUYEN");

                entity.ToTable("HINHTHUCVANCHUYEN");

                entity.Property(e => e.MaVanChuyen).ValueGeneratedNever();

                entity.Property(e => e.HinhThucVanChuyen)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Khachhang>(entity =>
            {
                entity.HasKey(e => e.MaKhachHang)
                    .HasName("PK_KHACHHANG");

                entity.ToTable("KHACHHANG");

                entity.Property(e => e.MaKhachHang).ValueGeneratedNever();

                entity.Property(e => e.DiaChi).HasMaxLength(50);

                entity.Property(e => e.Email).HasColumnType("varchar(30)");

                entity.Property(e => e.SoDienThoai)
                    .IsRequired()
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.TenKhachHang)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Nhacungcap>(entity =>
            {
                entity.HasKey(e => e.MaNcc)
                    .HasName("PK_NHACUNGCAP");

                entity.ToTable("NHACUNGCAP");

                entity.Property(e => e.MaNcc)
                    .HasColumnName("MaNCC")
                    .ValueGeneratedNever();

                entity.Property(e => e.Email).HasColumnType("varchar(30)");

                entity.Property(e => e.SoDienThoai)
                    .IsRequired()
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.TenNcc)
                    .IsRequired()
                    .HasColumnName("TenNCC")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Nhasanxuat>(entity =>
            {
                entity.HasKey(e => e.MaNsx)
                    .HasName("PK_NHASANXUAT");

                entity.ToTable("NHASANXUAT");

                entity.Property(e => e.MaNsx)
                    .HasColumnName("MaNSX")
                    .ValueGeneratedNever();

                entity.Property(e => e.TenNsx)
                    .HasColumnName("TenNSX")
                    .HasColumnType("varchar(50)");

                entity.HasOne(d => d.MaQuocGiaNavigation)
                    .WithMany(p => p.Nhasanxuat)
                    .HasForeignKey(d => d.MaQuocGia)
                    .HasConstraintName("FK_NHASANXUAT_QUOCGIA");
            });

            modelBuilder.Entity<Nhaxuatban>(entity =>
            {
                entity.HasKey(e => e.MaNxb)
                    .HasName("PK_NHAXUATBAN");

                entity.ToTable("NHAXUATBAN");

                entity.Property(e => e.MaNxb)
                    .HasColumnName("MaNXB")
                    .ValueGeneratedNever();

                entity.Property(e => e.TenNxb)
                    .IsRequired()
                    .HasColumnName("TenNXB")
                    .HasMaxLength(50);

                entity.HasOne(d => d.MaQuocGiaNavigation)
                    .WithMany(p => p.Nhaxuatban)
                    .HasForeignKey(d => d.MaQuocGia)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_NHAXUATBAN_QUOCGIA");
            });

            modelBuilder.Entity<Phieunhaphang>(entity =>
            {
                entity.HasKey(e => e.MaPhieuNhap)
                    .HasName("PK_PHIEUNHAPHANG");

                entity.ToTable("PHIEUNHAPHANG");

                entity.Property(e => e.MaPhieuNhap).ValueGeneratedNever();

                entity.Property(e => e.MaNcc).HasColumnName("MaNCC");

                entity.Property(e => e.NgayNhap).HasColumnType("datetime");

                entity.HasOne(d => d.MaNccNavigation)
                    .WithMany(p => p.Phieunhaphang)
                    .HasForeignKey(d => d.MaNcc)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PHIEUNHAPHANG_NHACUNGCAP");
            });

            modelBuilder.Entity<Phieutrahang>(entity =>
            {
                entity.HasKey(e => e.MaPhieuTra)
                    .HasName("PK_PHIEUTRAHANG");

                entity.ToTable("PHIEUTRAHANG");

                entity.Property(e => e.MaPhieuTra).ValueGeneratedNever();

                entity.Property(e => e.GhiChu).HasMaxLength(80);

                entity.Property(e => e.NgayTra).HasColumnType("datetime");

                entity.HasOne(d => d.MaDonHangNavigation)
                    .WithMany(p => p.Phieutrahang)
                    .HasForeignKey(d => d.MaDonHang)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PHIEUTRAHANG_DONHANG");

                entity.HasOne(d => d.MaKhachHangNavigation)
                    .WithMany(p => p.Phieutrahang)
                    .HasForeignKey(d => d.MaKhachHang)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PHIEUTRAHANG_KHACHHANG");
            });

            modelBuilder.Entity<Quocgia>(entity =>
            {
                entity.HasKey(e => e.MaQuocGia)
                    .HasName("PK_QUOCGIA");

                entity.ToTable("QUOCGIA");

                entity.Property(e => e.MaQuocGia).ValueGeneratedNever();

                entity.Property(e => e.TenQuocGia).HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<Sach>(entity =>
            {
                entity.HasKey(e => e.MaHangHoa)
                    .HasName("PK_SACH");

                entity.ToTable("SACH");

                entity.Property(e => e.MaHangHoa).ValueGeneratedNever();

                entity.Property(e => e.Isbn)
                    .HasColumnName("ISBN")
                    .HasColumnType("varchar(17)");

                entity.Property(e => e.MaNxb).HasColumnName("MaNXB");

                entity.Property(e => e.MoTa).HasMaxLength(80);

                entity.Property(e => e.NgayXuatBan).HasColumnType("datetime");

                entity.Property(e => e.TenSach)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.MaLoaiSachNavigation)
                    .WithMany(p => p.Sach)
                    .HasForeignKey(d => d.MaLoaiSach)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SACH_THELOAISACH");

                entity.HasOne(d => d.MaNxbNavigation)
                    .WithMany(p => p.Sach)
                    .HasForeignKey(d => d.MaNxb)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SACH_NHAXUATBAN");

                entity.HasOne(d => d.MaTacGiaNavigation)
                    .WithMany(p => p.Sach)
                    .HasForeignKey(d => d.MaTacGia)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SACH_TACGIA");
            });

            modelBuilder.Entity<Tacgia>(entity =>
            {
                entity.HasKey(e => e.MaTacGia)
                    .HasName("PK_TACGIA");

                entity.ToTable("TACGIA");

                entity.Property(e => e.MaTacGia).ValueGeneratedNever();

                entity.Property(e => e.TenTacGia)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.MaQuocGiaNavigation)
                    .WithMany(p => p.Tacgia)
                    .HasForeignKey(d => d.MaQuocGia)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_TACGIA_QUOCGIA");
            });

            modelBuilder.Entity<Theloaisach>(entity =>
            {
                entity.HasKey(e => e.MaLoaiSach)
                    .HasName("PK_THELOAISACH");

                entity.ToTable("THELOAISACH");

                entity.Property(e => e.MaLoaiSach).ValueGeneratedNever();

                entity.Property(e => e.TenLoaiSach)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Trangthai>(entity =>
            {
                entity.HasKey(e => e.MaTrangThai)
                    .HasName("PK_TRANGTHAI");

                entity.ToTable("TRANGTHAI");

                entity.Property(e => e.MaTrangThai).ValueGeneratedNever();

                entity.Property(e => e.TenTrangThai)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Vanphongpham>(entity =>
            {
                entity.HasKey(e => e.MaHangHoa)
                    .HasName("PK_VANPHONGPHAM");

                entity.ToTable("VANPHONGPHAM");

                entity.Property(e => e.MaHangHoa).ValueGeneratedNever();

                entity.Property(e => e.MaNsx).HasColumnName("MaNSX");

                entity.Property(e => e.MoTa).HasMaxLength(50);

                entity.Property(e => e.Ten)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.MaNsxNavigation)
                    .WithMany(p => p.Vanphongpham)
                    .HasForeignKey(d => d.MaNsx)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_VANPHONGPHAM_NHASANXUAT");
            });
        }
    }
}