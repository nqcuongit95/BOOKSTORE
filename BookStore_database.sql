USE [master]
GO
/****** Object:  Database [BOOKSTORE]    Script Date: 2017-12-04 22:16:54 ******/
CREATE DATABASE [BOOKSTORE]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BOOKSTORE', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\BOOKSTORE.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'BOOKSTORE_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\BOOKSTORE_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [BOOKSTORE] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BOOKSTORE].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BOOKSTORE] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BOOKSTORE] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BOOKSTORE] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BOOKSTORE] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BOOKSTORE] SET ARITHABORT OFF 
GO
ALTER DATABASE [BOOKSTORE] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BOOKSTORE] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [BOOKSTORE] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BOOKSTORE] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BOOKSTORE] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BOOKSTORE] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BOOKSTORE] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BOOKSTORE] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BOOKSTORE] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BOOKSTORE] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BOOKSTORE] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BOOKSTORE] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BOOKSTORE] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BOOKSTORE] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BOOKSTORE] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BOOKSTORE] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BOOKSTORE] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BOOKSTORE] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BOOKSTORE] SET RECOVERY FULL 
GO
ALTER DATABASE [BOOKSTORE] SET  MULTI_USER 
GO
ALTER DATABASE [BOOKSTORE] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BOOKSTORE] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BOOKSTORE] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BOOKSTORE] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [BOOKSTORE]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 2017-12-04 22:16:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 2017-12-04 22:16:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[Description] [nvarchar](90) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 2017-12-04 22:16:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 2017-12-04 22:16:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 2017-12-04 22:16:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 2017-12-04 22:16:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[FullName] [nvarchar](60) NOT NULL,
	[DateCreate] [datetime] NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 2017-12-04 22:16:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [int] NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ChiTietDonHang]    Script Date: 2017-12-04 22:16:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietDonHang](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DonHangID] [int] NOT NULL,
	[HangHoaID] [int] NOT NULL,
	[SoLuong] [int] NOT NULL,
	[GiaBan] [money] NOT NULL,
 CONSTRAINT [PK_ChiTietDonHang] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ChiTietHangHoa]    Script Date: 2017-12-04 22:16:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietHangHoa](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[HangHoaID] [int] NOT NULL,
	[ThuocTinh] [nvarchar](50) NOT NULL,
	[GiaTri] [nvarchar](100) NULL,
 CONSTRAINT [PK_ChiTietHangHoa] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ChiTietPhieuKiemKho]    Script Date: 2017-12-04 22:16:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietPhieuKiemKho](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PhieuKiemKhoID] [int] NOT NULL,
	[HangHoaID] [int] NOT NULL,
	[TonKho] [int] NOT NULL,
	[TonKhoThucTe] [int] NOT NULL,
	[LyDo] [nvarchar](100) NULL,
 CONSTRAINT [PK_ChiTietPhieuKiemKho] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ChiTietPhieuNhapHang]    Script Date: 2017-12-04 22:16:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietPhieuNhapHang](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PhieuNhapHangID] [int] NOT NULL,
	[HangHoaID] [int] NOT NULL,
	[SoLuong] [int] NOT NULL,
	[GiaNhap] [money] NOT NULL,
 CONSTRAINT [PK_ChiTietPhieuNhapHang] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ChiTietPhieuTraHang]    Script Date: 2017-12-04 22:16:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietPhieuTraHang](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PhieuTraHangID] [int] NOT NULL,
	[ChiTietDonHangID] [int] NOT NULL,
	[SoLuong] [int] NOT NULL,
	[GiaTra] [money] NOT NULL,
 CONSTRAINT [PK_ChiTietPhieuTraHang] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ChiTietPhieuTraNhapHang]    Script Date: 2017-12-04 22:16:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietPhieuTraNhapHang](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PhieuTraNhapHangID] [int] NOT NULL,
	[ChiTietPhieuNhapHangID] [int] NOT NULL,
	[SoLuong] [int] NOT NULL,
	[GiaTra] [money] NOT NULL,
 CONSTRAINT [PK_ChiTietPhieuTraNhapHang] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DonHang]    Script Date: 2017-12-04 22:16:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonHang](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NgayLap] [datetime] NOT NULL,
	[KhachHangID] [int] NOT NULL,
	[TrangThaiID] [int] NOT NULL,
	[TongTien] [money] NOT NULL,
	[NhanVienID] [int] NOT NULL,
	[ChietKhau] [money] NULL,
 CONSTRAINT [PK_DonHang] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HangHoa]    Script Date: 2017-12-04 22:16:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HangHoa](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NgayLap] [date] NULL,
	[TenHangHoa] [nvarchar](50) NOT NULL,
	[LoaiHangHoaId] [int] NOT NULL,
	[TonKho] [int] NOT NULL,
	[NhaCungCapID] [int] NOT NULL,
	[NhanHieuID] [int] NOT NULL,
	[GiaKhoiTao] [money] NOT NULL,
	[GiaNhap] [money] NULL,
	[GiaBanSi] [money] NULL,
	[GiaBanLe] [money] NULL,
	[TrangThaiID] [int] NOT NULL,
	[DaBan] [int] NULL,
	[ImageUrl] [nvarchar](1000) NULL,
 CONSTRAINT [PK_HANGHOA] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[KhachHang]    Script Date: 2017-12-04 22:16:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[KhachHang](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TenKhachHang] [nvarchar](50) NOT NULL,
	[SoDienThoai] [varchar](20) NOT NULL,
	[DiaChi] [nvarchar](60) NULL,
	[Email] [varchar](50) NULL,
	[LoaiKhachHangID] [int] NOT NULL,
	[NgayLap] [datetime] NOT NULL,
 CONSTRAINT [PK_KhachHang] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LoaiHangHoa]    Script Date: 2017-12-04 22:16:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiHangHoa](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TenLoaiHangHoa] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_LoaiHangHoa] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LoaiKhachHang]    Script Date: 2017-12-04 22:16:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiKhachHang](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TenLoaiKhachHang] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_LoaiKhachHang] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LoaiPhieu]    Script Date: 2017-12-04 22:16:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LoaiPhieu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TenLoaiPhieu] [nvarchar](80) NOT NULL,
	[Loai] [varchar](10) NOT NULL,
 CONSTRAINT [PK_LoaiPhieu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NhaCungCap]    Script Date: 2017-12-04 22:16:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhaCungCap](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TenNhaCungCap] [nvarchar](50) NOT NULL,
	[NgayLap] [date] NOT NULL,
 CONSTRAINT [PK_NhaCungCap] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NhanHieu]    Script Date: 2017-12-04 22:16:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhanHieu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TenNhanHieu] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_NhanHieu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PhieuChi]    Script Date: 2017-12-04 22:16:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuChi](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NhanVienID] [int] NOT NULL,
	[NgayLap] [datetime] NOT NULL,
	[PhieuTraHangID] [int] NULL,
	[PhieuNhapHangID] [int] NULL,
	[TongTien] [money] NOT NULL,
	[LoaiPhieuID] [int] NOT NULL,
	[GhiChu] [nvarchar](100) NULL,
	[KhachHangID] [int] NULL,
	[NhaCungCapID] [int] NULL,
 CONSTRAINT [PK_PhieuChi] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PhieuKiemKho]    Script Date: 2017-12-04 22:16:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuKiemKho](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NgayLap] [datetime] NOT NULL,
	[NhanVienID] [int] NOT NULL,
	[GhiChu] [nvarchar](100) NULL,
 CONSTRAINT [PK_PhieuKiemKho] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PhieuNhanHang]    Script Date: 2017-12-04 22:16:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuNhanHang](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PhieuNhapHangID] [int] NOT NULL,
	[NgayLap] [datetime] NOT NULL,
	[NhanVienID] [int] NOT NULL,
	[TongTien] [money] NOT NULL,
	[GhiChu] [nvarchar](100) NULL,
 CONSTRAINT [PK_PhieuNhanHang] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PhieuNhapHang]    Script Date: 2017-12-04 22:16:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuNhapHang](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NgayLap] [datetime] NOT NULL,
	[TongTien] [money] NOT NULL,
	[NhanVienID] [int] NOT NULL,
	[TrangThaiID] [int] NOT NULL,
	[NhaCungCapID] [int] NOT NULL,
	[GhiChu] [nvarchar](100) NULL,
 CONSTRAINT [PK_PhieuNhapHang] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PhieuThu]    Script Date: 2017-12-04 22:16:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuThu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NgayLap] [datetime] NOT NULL,
	[DonHangID] [int] NULL,
	[PhieuTraNhapHangID] [int] NULL,
	[NhanVienID] [int] NOT NULL,
	[TongTien] [money] NOT NULL,
	[LoaiPhieuID] [int] NOT NULL,
	[KhachHangId] [int] NULL,
	[GhiChu] [nvarchar](100) NULL,
	[NhaCungCapID] [int] NULL,
 CONSTRAINT [PK_PhieuThu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PhieuTraHang]    Script Date: 2017-12-04 22:16:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuTraHang](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NgayLap] [datetime] NOT NULL,
	[KhachHangID] [int] NOT NULL,
	[DonHangID] [int] NOT NULL,
	[GhiChu] [nvarchar](100) NULL,
	[TongTien] [money] NOT NULL,
	[NhanVienID] [int] NOT NULL,
 CONSTRAINT [PK_PhieuTraHang] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PhieuTraNhapHang]    Script Date: 2017-12-04 22:16:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuTraNhapHang](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NgayLap] [datetime] NOT NULL,
	[NhanVienID] [int] NOT NULL,
	[PhieuNhapHangID] [int] NOT NULL,
	[TongTien] [money] NOT NULL,
	[GhiChu] [nvarchar](100) NULL,
 CONSTRAINT [PK_PhieuTraNhapHang] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ThuocTinhHangHoa]    Script Date: 2017-12-04 22:16:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThuocTinhHangHoa](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LoaiHangHoaID] [int] NOT NULL,
	[TenThuocTinh] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ThuocTinhHangHoa] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TrangThai]    Script Date: 2017-12-04 22:16:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TrangThai](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TenTrangThai] [nvarchar](100) NOT NULL,
	[VietTat] [varchar](20) NOT NULL,
	[Loai] [varchar](20) NULL,
 CONSTRAINT [PK_TrangThai] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[ChiTietDonHang]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietDonHang_DonHang] FOREIGN KEY([DonHangID])
REFERENCES [dbo].[DonHang] ([ID])
GO
ALTER TABLE [dbo].[ChiTietDonHang] CHECK CONSTRAINT [FK_ChiTietDonHang_DonHang]
GO
ALTER TABLE [dbo].[ChiTietDonHang]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietDonHang_HangHoa] FOREIGN KEY([HangHoaID])
REFERENCES [dbo].[HangHoa] ([ID])
GO
ALTER TABLE [dbo].[ChiTietDonHang] CHECK CONSTRAINT [FK_ChiTietDonHang_HangHoa]
GO
ALTER TABLE [dbo].[ChiTietHangHoa]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietHangHoa_HangHoa] FOREIGN KEY([HangHoaID])
REFERENCES [dbo].[HangHoa] ([ID])
GO
ALTER TABLE [dbo].[ChiTietHangHoa] CHECK CONSTRAINT [FK_ChiTietHangHoa_HangHoa]
GO
ALTER TABLE [dbo].[ChiTietPhieuKiemKho]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietPhieuKiemKho_HangHoa] FOREIGN KEY([HangHoaID])
REFERENCES [dbo].[HangHoa] ([ID])
GO
ALTER TABLE [dbo].[ChiTietPhieuKiemKho] CHECK CONSTRAINT [FK_ChiTietPhieuKiemKho_HangHoa]
GO
ALTER TABLE [dbo].[ChiTietPhieuKiemKho]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietPhieuKiemKho_PhieuKiemKho] FOREIGN KEY([PhieuKiemKhoID])
REFERENCES [dbo].[PhieuKiemKho] ([ID])
GO
ALTER TABLE [dbo].[ChiTietPhieuKiemKho] CHECK CONSTRAINT [FK_ChiTietPhieuKiemKho_PhieuKiemKho]
GO
ALTER TABLE [dbo].[ChiTietPhieuNhapHang]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietPhieuNhapHang_HangHoa] FOREIGN KEY([HangHoaID])
REFERENCES [dbo].[HangHoa] ([ID])
GO
ALTER TABLE [dbo].[ChiTietPhieuNhapHang] CHECK CONSTRAINT [FK_ChiTietPhieuNhapHang_HangHoa]
GO
ALTER TABLE [dbo].[ChiTietPhieuNhapHang]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietPhieuNhapHang_PhieuNhapHang] FOREIGN KEY([PhieuNhapHangID])
REFERENCES [dbo].[PhieuNhapHang] ([ID])
GO
ALTER TABLE [dbo].[ChiTietPhieuNhapHang] CHECK CONSTRAINT [FK_ChiTietPhieuNhapHang_PhieuNhapHang]
GO
ALTER TABLE [dbo].[ChiTietPhieuTraHang]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietPhieuTraHang_PhieuTraHang] FOREIGN KEY([PhieuTraHangID])
REFERENCES [dbo].[PhieuTraHang] ([ID])
GO
ALTER TABLE [dbo].[ChiTietPhieuTraHang] CHECK CONSTRAINT [FK_ChiTietPhieuTraHang_PhieuTraHang]
GO
ALTER TABLE [dbo].[ChiTietPhieuTraNhapHang]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietPhieuTraNhapHang_ChiTietPhieuNhapHang] FOREIGN KEY([ChiTietPhieuNhapHangID])
REFERENCES [dbo].[ChiTietPhieuNhapHang] ([ID])
GO
ALTER TABLE [dbo].[ChiTietPhieuTraNhapHang] CHECK CONSTRAINT [FK_ChiTietPhieuTraNhapHang_ChiTietPhieuNhapHang]
GO
ALTER TABLE [dbo].[ChiTietPhieuTraNhapHang]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietPhieuTraNhapHang_PhieuTraNhapHang] FOREIGN KEY([PhieuTraNhapHangID])
REFERENCES [dbo].[PhieuTraNhapHang] ([ID])
GO
ALTER TABLE [dbo].[ChiTietPhieuTraNhapHang] CHECK CONSTRAINT [FK_ChiTietPhieuTraNhapHang_PhieuTraNhapHang]
GO
ALTER TABLE [dbo].[DonHang]  WITH CHECK ADD  CONSTRAINT [FK_DonHang_AspNetUsers] FOREIGN KEY([NhanVienID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[DonHang] CHECK CONSTRAINT [FK_DonHang_AspNetUsers]
GO
ALTER TABLE [dbo].[DonHang]  WITH CHECK ADD  CONSTRAINT [FK_DonHang_KhachHang] FOREIGN KEY([KhachHangID])
REFERENCES [dbo].[KhachHang] ([ID])
GO
ALTER TABLE [dbo].[DonHang] CHECK CONSTRAINT [FK_DonHang_KhachHang]
GO
ALTER TABLE [dbo].[DonHang]  WITH CHECK ADD  CONSTRAINT [FK_DonHang_TrangThai] FOREIGN KEY([TrangThaiID])
REFERENCES [dbo].[TrangThai] ([ID])
GO
ALTER TABLE [dbo].[DonHang] CHECK CONSTRAINT [FK_DonHang_TrangThai]
GO
ALTER TABLE [dbo].[HangHoa]  WITH CHECK ADD  CONSTRAINT [FK_HangHoa_LoaiHangHoa] FOREIGN KEY([LoaiHangHoaId])
REFERENCES [dbo].[LoaiHangHoa] ([ID])
GO
ALTER TABLE [dbo].[HangHoa] CHECK CONSTRAINT [FK_HangHoa_LoaiHangHoa]
GO
ALTER TABLE [dbo].[HangHoa]  WITH CHECK ADD  CONSTRAINT [FK_HangHoa_NhaCungCap] FOREIGN KEY([NhaCungCapID])
REFERENCES [dbo].[NhaCungCap] ([ID])
GO
ALTER TABLE [dbo].[HangHoa] CHECK CONSTRAINT [FK_HangHoa_NhaCungCap]
GO
ALTER TABLE [dbo].[HangHoa]  WITH CHECK ADD  CONSTRAINT [FK_HangHoa_NhanHieu] FOREIGN KEY([NhanHieuID])
REFERENCES [dbo].[NhanHieu] ([ID])
GO
ALTER TABLE [dbo].[HangHoa] CHECK CONSTRAINT [FK_HangHoa_NhanHieu]
GO
ALTER TABLE [dbo].[HangHoa]  WITH CHECK ADD  CONSTRAINT [FK_HangHoa_TrangThai] FOREIGN KEY([TrangThaiID])
REFERENCES [dbo].[TrangThai] ([ID])
GO
ALTER TABLE [dbo].[HangHoa] CHECK CONSTRAINT [FK_HangHoa_TrangThai]
GO
ALTER TABLE [dbo].[KhachHang]  WITH CHECK ADD  CONSTRAINT [FK_KhachHang_LoaiKhachHang] FOREIGN KEY([LoaiKhachHangID])
REFERENCES [dbo].[LoaiKhachHang] ([ID])
GO
ALTER TABLE [dbo].[KhachHang] CHECK CONSTRAINT [FK_KhachHang_LoaiKhachHang]
GO
ALTER TABLE [dbo].[PhieuChi]  WITH CHECK ADD  CONSTRAINT [FK_PhieuChi_AspNetUsers] FOREIGN KEY([NhanVienID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[PhieuChi] CHECK CONSTRAINT [FK_PhieuChi_AspNetUsers]
GO
ALTER TABLE [dbo].[PhieuChi]  WITH CHECK ADD  CONSTRAINT [FK_PhieuChi_KhachHang] FOREIGN KEY([KhachHangID])
REFERENCES [dbo].[KhachHang] ([ID])
GO
ALTER TABLE [dbo].[PhieuChi] CHECK CONSTRAINT [FK_PhieuChi_KhachHang]
GO
ALTER TABLE [dbo].[PhieuChi]  WITH CHECK ADD  CONSTRAINT [FK_PhieuChi_LoaiPhieu] FOREIGN KEY([LoaiPhieuID])
REFERENCES [dbo].[LoaiPhieu] ([ID])
GO
ALTER TABLE [dbo].[PhieuChi] CHECK CONSTRAINT [FK_PhieuChi_LoaiPhieu]
GO
ALTER TABLE [dbo].[PhieuChi]  WITH CHECK ADD  CONSTRAINT [FK_PhieuChi_NhaCungCap] FOREIGN KEY([NhaCungCapID])
REFERENCES [dbo].[NhaCungCap] ([ID])
GO
ALTER TABLE [dbo].[PhieuChi] CHECK CONSTRAINT [FK_PhieuChi_NhaCungCap]
GO
ALTER TABLE [dbo].[PhieuChi]  WITH CHECK ADD  CONSTRAINT [FK_PhieuChi_PhieuNhapHang] FOREIGN KEY([PhieuNhapHangID])
REFERENCES [dbo].[PhieuNhapHang] ([ID])
GO
ALTER TABLE [dbo].[PhieuChi] CHECK CONSTRAINT [FK_PhieuChi_PhieuNhapHang]
GO
ALTER TABLE [dbo].[PhieuChi]  WITH CHECK ADD  CONSTRAINT [FK_PhieuChi_PhieuTraHang] FOREIGN KEY([PhieuTraHangID])
REFERENCES [dbo].[PhieuTraHang] ([ID])
GO
ALTER TABLE [dbo].[PhieuChi] CHECK CONSTRAINT [FK_PhieuChi_PhieuTraHang]
GO
ALTER TABLE [dbo].[PhieuKiemKho]  WITH CHECK ADD  CONSTRAINT [FK_PhieuKiemKho_AspNetUsers] FOREIGN KEY([NhanVienID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[PhieuKiemKho] CHECK CONSTRAINT [FK_PhieuKiemKho_AspNetUsers]
GO
ALTER TABLE [dbo].[PhieuNhanHang]  WITH CHECK ADD  CONSTRAINT [FK_PhieuNhanHang_AspNetUsers] FOREIGN KEY([NhanVienID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[PhieuNhanHang] CHECK CONSTRAINT [FK_PhieuNhanHang_AspNetUsers]
GO
ALTER TABLE [dbo].[PhieuNhanHang]  WITH CHECK ADD  CONSTRAINT [FK_PhieuNhanHang_PhieuNhapHang] FOREIGN KEY([PhieuNhapHangID])
REFERENCES [dbo].[PhieuNhapHang] ([ID])
GO
ALTER TABLE [dbo].[PhieuNhanHang] CHECK CONSTRAINT [FK_PhieuNhanHang_PhieuNhapHang]
GO
ALTER TABLE [dbo].[PhieuNhapHang]  WITH CHECK ADD  CONSTRAINT [FK_PhieuNhapHang_AspNetUsers] FOREIGN KEY([NhanVienID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[PhieuNhapHang] CHECK CONSTRAINT [FK_PhieuNhapHang_AspNetUsers]
GO
ALTER TABLE [dbo].[PhieuNhapHang]  WITH CHECK ADD  CONSTRAINT [FK_PhieuNhapHang_NhaCungCap] FOREIGN KEY([NhaCungCapID])
REFERENCES [dbo].[NhaCungCap] ([ID])
GO
ALTER TABLE [dbo].[PhieuNhapHang] CHECK CONSTRAINT [FK_PhieuNhapHang_NhaCungCap]
GO
ALTER TABLE [dbo].[PhieuNhapHang]  WITH CHECK ADD  CONSTRAINT [FK_PhieuNhapHang_TrangThai] FOREIGN KEY([TrangThaiID])
REFERENCES [dbo].[TrangThai] ([ID])
GO
ALTER TABLE [dbo].[PhieuNhapHang] CHECK CONSTRAINT [FK_PhieuNhapHang_TrangThai]
GO
ALTER TABLE [dbo].[PhieuThu]  WITH CHECK ADD  CONSTRAINT [FK_PhieuThu_AspNetUsers] FOREIGN KEY([NhanVienID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[PhieuThu] CHECK CONSTRAINT [FK_PhieuThu_AspNetUsers]
GO
ALTER TABLE [dbo].[PhieuThu]  WITH CHECK ADD  CONSTRAINT [FK_PhieuThu_DonHang] FOREIGN KEY([DonHangID])
REFERENCES [dbo].[DonHang] ([ID])
GO
ALTER TABLE [dbo].[PhieuThu] CHECK CONSTRAINT [FK_PhieuThu_DonHang]
GO
ALTER TABLE [dbo].[PhieuThu]  WITH CHECK ADD  CONSTRAINT [FK_PhieuThu_KhachHang] FOREIGN KEY([KhachHangId])
REFERENCES [dbo].[KhachHang] ([ID])
GO
ALTER TABLE [dbo].[PhieuThu] CHECK CONSTRAINT [FK_PhieuThu_KhachHang]
GO
ALTER TABLE [dbo].[PhieuThu]  WITH CHECK ADD  CONSTRAINT [FK_PhieuThu_LoaiPhieu] FOREIGN KEY([LoaiPhieuID])
REFERENCES [dbo].[LoaiPhieu] ([ID])
GO
ALTER TABLE [dbo].[PhieuThu] CHECK CONSTRAINT [FK_PhieuThu_LoaiPhieu]
GO
ALTER TABLE [dbo].[PhieuThu]  WITH CHECK ADD  CONSTRAINT [FK_PhieuThu_NhaCungCap] FOREIGN KEY([NhaCungCapID])
REFERENCES [dbo].[NhaCungCap] ([ID])
GO
ALTER TABLE [dbo].[PhieuThu] CHECK CONSTRAINT [FK_PhieuThu_NhaCungCap]
GO
ALTER TABLE [dbo].[PhieuThu]  WITH CHECK ADD  CONSTRAINT [FK_PhieuThu_PhieuTraNhapHang] FOREIGN KEY([PhieuTraNhapHangID])
REFERENCES [dbo].[PhieuTraNhapHang] ([ID])
GO
ALTER TABLE [dbo].[PhieuThu] CHECK CONSTRAINT [FK_PhieuThu_PhieuTraNhapHang]
GO
ALTER TABLE [dbo].[PhieuTraHang]  WITH CHECK ADD  CONSTRAINT [FK_PhieuTraHang_AspNetUsers] FOREIGN KEY([NhanVienID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[PhieuTraHang] CHECK CONSTRAINT [FK_PhieuTraHang_AspNetUsers]
GO
ALTER TABLE [dbo].[PhieuTraHang]  WITH CHECK ADD  CONSTRAINT [FK_PhieuTraHang_DonHang] FOREIGN KEY([DonHangID])
REFERENCES [dbo].[DonHang] ([ID])
GO
ALTER TABLE [dbo].[PhieuTraHang] CHECK CONSTRAINT [FK_PhieuTraHang_DonHang]
GO
ALTER TABLE [dbo].[PhieuTraHang]  WITH CHECK ADD  CONSTRAINT [FK_PhieuTraHang_KhachHang] FOREIGN KEY([KhachHangID])
REFERENCES [dbo].[KhachHang] ([ID])
GO
ALTER TABLE [dbo].[PhieuTraHang] CHECK CONSTRAINT [FK_PhieuTraHang_KhachHang]
GO
ALTER TABLE [dbo].[PhieuTraNhapHang]  WITH CHECK ADD  CONSTRAINT [FK_PhieuTraNhapHang_AspNetUsers] FOREIGN KEY([NhanVienID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[PhieuTraNhapHang] CHECK CONSTRAINT [FK_PhieuTraNhapHang_AspNetUsers]
GO
ALTER TABLE [dbo].[PhieuTraNhapHang]  WITH CHECK ADD  CONSTRAINT [FK_PhieuTraNhapHang_PhieuNhapHang] FOREIGN KEY([PhieuNhapHangID])
REFERENCES [dbo].[PhieuNhapHang] ([ID])
GO
ALTER TABLE [dbo].[PhieuTraNhapHang] CHECK CONSTRAINT [FK_PhieuTraNhapHang_PhieuNhapHang]
GO
ALTER TABLE [dbo].[ThuocTinhHangHoa]  WITH CHECK ADD  CONSTRAINT [FK_ThuocTinhHangHoa_LoaiHangHoa] FOREIGN KEY([LoaiHangHoaID])
REFERENCES [dbo].[LoaiHangHoa] ([ID])
GO
ALTER TABLE [dbo].[ThuocTinhHangHoa] CHECK CONSTRAINT [FK_ThuocTinhHangHoa_LoaiHangHoa]
GO
USE [master]
GO
ALTER DATABASE [BOOKSTORE] SET  READ_WRITE 
GO
