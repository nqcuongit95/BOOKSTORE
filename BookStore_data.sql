USE [BOOKSTORE]
GO
SET IDENTITY_INSERT [dbo].[AspNetRoles] ON 

INSERT [dbo].[AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName], [Description]) VALUES (1, N'e24dbb6a-ae2c-465c-82f9-6b9caf5879a7', N'Admin', N'ADMIN', NULL)
SET IDENTITY_INSERT [dbo].[AspNetRoles] OFF
SET IDENTITY_INSERT [dbo].[AspNetUsers] ON 

INSERT [dbo].[AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName], [FullName], [DateCreate]) VALUES (1, 0, N'8af28e6d-2cfb-45a2-953c-e3ed4f0da5b8', NULL, 0, 1, NULL, NULL, N'ADMIN', N'AQAAAAEAACcQAAAAELQ9bRSrbx9jYJN4+R5K16NWP0cpYXcXk15Fz80X/exC/IV/ojyhm3e+43RlE+oePg==', NULL, 0, N'b629b367-358a-4533-ba96-f617df771ecb', 0, N'admin', N'tuat', CAST(0x0000A825013EF61B AS DateTime))
SET IDENTITY_INSERT [dbo].[AspNetUsers] OFF
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (1, 1)
SET IDENTITY_INSERT [dbo].[LoaiKhachHang] ON 

INSERT [dbo].[LoaiKhachHang] ([ID], [TenLoaiKhachHang]) VALUES (1, N'Khách vãng lai')
INSERT [dbo].[LoaiKhachHang] ([ID], [TenLoaiKhachHang]) VALUES (2, N'Khách VIP')
SET IDENTITY_INSERT [dbo].[LoaiKhachHang] OFF
SET IDENTITY_INSERT [dbo].[KhachHang] ON 

INSERT [dbo].[KhachHang] ([ID], [TenKhachHang], [SoDienThoai], [DiaChi], [Email], [LoaiKhachHangID], [NgayLap]) VALUES (1, N'Khách vãng lai', N'', N'', N'', 1, CAST(0x0000A83F015AE8B5 AS DateTime))
INSERT [dbo].[KhachHang] ([ID], [TenKhachHang], [SoDienThoai], [DiaChi], [Email], [LoaiKhachHangID], [NgayLap]) VALUES (2, N'Bùi Như Sang', N'01625437895', N'huyện châu đức tỉnh bà rịa vũng tàu', NULL, 2, CAST(0x0000A83D00000000 AS DateTime))
SET IDENTITY_INSERT [dbo].[KhachHang] OFF
SET IDENTITY_INSERT [dbo].[TrangThai] ON 

INSERT [dbo].[TrangThai] ([ID], [TenTrangThai], [VietTat], [Loai]) VALUES (1, N'Đang sử dụng', N'Use', N'HangHoa')
INSERT [dbo].[TrangThai] ([ID], [TenTrangThai], [VietTat], [Loai]) VALUES (2, N'Ngưng dao dịch', N'NOTUSE', NULL)
INSERT [dbo].[TrangThai] ([ID], [TenTrangThai], [VietTat], [Loai]) VALUES (3, N'Đã thanh toán', N'Paid', N'KhachHang')
INSERT [dbo].[TrangThai] ([ID], [TenTrangThai], [VietTat], [Loai]) VALUES (4, N'Chưa thanh toán', N'Not-Paid', N'KhachHang')
INSERT [dbo].[TrangThai] ([ID], [TenTrangThai], [VietTat], [Loai]) VALUES (5, N'Thanh toán một phần', N'Semi-Paid', N'KhachHang')
SET IDENTITY_INSERT [dbo].[TrangThai] OFF
SET IDENTITY_INSERT [dbo].[DonHang] ON 

INSERT [dbo].[DonHang] ([ID], [NgayLap], [KhachHangID], [TrangThaiID], [TongTien], [NhanVienID], [ChietKhau]) VALUES (2, CAST(0x0000A83F015EEBB2 AS DateTime), 1, 3, 10000.0000, 1, 0.0000)
INSERT [dbo].[DonHang] ([ID], [NgayLap], [KhachHangID], [TrangThaiID], [TongTien], [NhanVienID], [ChietKhau]) VALUES (3, CAST(0x0000A83F01615280 AS DateTime), 1, 3, 10000.0000, 1, 0.0000)
INSERT [dbo].[DonHang] ([ID], [NgayLap], [KhachHangID], [TrangThaiID], [TongTien], [NhanVienID], [ChietKhau]) VALUES (4, CAST(0x0000A83F01616121 AS DateTime), 1, 4, 17500.0000, 1, 0.0000)
INSERT [dbo].[DonHang] ([ID], [NgayLap], [KhachHangID], [TrangThaiID], [TongTien], [NhanVienID], [ChietKhau]) VALUES (5, CAST(0x0000A83F016211A6 AS DateTime), 1, 5, 7500.0000, 1, 0.0000)
INSERT [dbo].[DonHang] ([ID], [NgayLap], [KhachHangID], [TrangThaiID], [TongTien], [NhanVienID], [ChietKhau]) VALUES (6, CAST(0x0000A83F01623E2C AS DateTime), 1, 5, 7500.0000, 1, 0.0000)
INSERT [dbo].[DonHang] ([ID], [NgayLap], [KhachHangID], [TrangThaiID], [TongTien], [NhanVienID], [ChietKhau]) VALUES (7, CAST(0x0000A83F01644A6F AS DateTime), 2, 3, 17500.0000, 1, 0.0000)
INSERT [dbo].[DonHang] ([ID], [NgayLap], [KhachHangID], [TrangThaiID], [TongTien], [NhanVienID], [ChietKhau]) VALUES (8, CAST(0x0000A83F016814A0 AS DateTime), 2, 5, 10000.0000, 1, 0.0000)
SET IDENTITY_INSERT [dbo].[DonHang] OFF
SET IDENTITY_INSERT [dbo].[LoaiPhieu] ON 

INSERT [dbo].[LoaiPhieu] ([ID], [TenLoaiPhieu], [Loai]) VALUES (1, N'Phiếu chi', N'Phieuchi')
INSERT [dbo].[LoaiPhieu] ([ID], [TenLoaiPhieu], [Loai]) VALUES (2, N'Phiếu thu', N'Phieuthu')
SET IDENTITY_INSERT [dbo].[LoaiPhieu] OFF
SET IDENTITY_INSERT [dbo].[NhaCungCap] ON 

INSERT [dbo].[NhaCungCap] ([ID], [TenNhaCungCap], [NgayLap]) VALUES (1, N'Nhà cung câp 1', CAST(0xD43A0B00 AS Date))
INSERT [dbo].[NhaCungCap] ([ID], [TenNhaCungCap], [NgayLap]) VALUES (2, N'Nhà cung câp 2', CAST(0xD53A0B00 AS Date))
INSERT [dbo].[NhaCungCap] ([ID], [TenNhaCungCap], [NgayLap]) VALUES (3, N'Nhà cung câp 3', CAST(0xD63A0B00 AS Date))
INSERT [dbo].[NhaCungCap] ([ID], [TenNhaCungCap], [NgayLap]) VALUES (4, N'Nhà cung câp 4', CAST(0xD73A0B00 AS Date))
INSERT [dbo].[NhaCungCap] ([ID], [TenNhaCungCap], [NgayLap]) VALUES (5, N'Nhà cung câp 5', CAST(0xD83A0B00 AS Date))
INSERT [dbo].[NhaCungCap] ([ID], [TenNhaCungCap], [NgayLap]) VALUES (6, N'Nhà cung câp 6', CAST(0xD93A0B00 AS Date))
INSERT [dbo].[NhaCungCap] ([ID], [TenNhaCungCap], [NgayLap]) VALUES (7, N'Nhà cung câp 7', CAST(0xDA3A0B00 AS Date))
INSERT [dbo].[NhaCungCap] ([ID], [TenNhaCungCap], [NgayLap]) VALUES (8, N'Nhà cung câp 8', CAST(0xDB3A0B00 AS Date))
INSERT [dbo].[NhaCungCap] ([ID], [TenNhaCungCap], [NgayLap]) VALUES (9, N'Nhà cung câp 9', CAST(0xDC3A0B00 AS Date))
INSERT [dbo].[NhaCungCap] ([ID], [TenNhaCungCap], [NgayLap]) VALUES (10, N'Nhà cung câp 10', CAST(0xDD3A0B00 AS Date))
INSERT [dbo].[NhaCungCap] ([ID], [TenNhaCungCap], [NgayLap]) VALUES (11, N'Nhà cung câp 11', CAST(0xDE3A0B00 AS Date))
INSERT [dbo].[NhaCungCap] ([ID], [TenNhaCungCap], [NgayLap]) VALUES (12, N'Nhà cung câp 12', CAST(0xDF3A0B00 AS Date))
INSERT [dbo].[NhaCungCap] ([ID], [TenNhaCungCap], [NgayLap]) VALUES (13, N'Nhà cung câp 13', CAST(0xE03A0B00 AS Date))
INSERT [dbo].[NhaCungCap] ([ID], [TenNhaCungCap], [NgayLap]) VALUES (14, N'Nhà cung câp 14', CAST(0xE13A0B00 AS Date))
INSERT [dbo].[NhaCungCap] ([ID], [TenNhaCungCap], [NgayLap]) VALUES (15, N'Nhà cung câp 15', CAST(0xE23A0B00 AS Date))
INSERT [dbo].[NhaCungCap] ([ID], [TenNhaCungCap], [NgayLap]) VALUES (16, N'Nhà cung câp 16', CAST(0xE33A0B00 AS Date))
INSERT [dbo].[NhaCungCap] ([ID], [TenNhaCungCap], [NgayLap]) VALUES (17, N'Nhà cung câp 17', CAST(0xE43A0B00 AS Date))
INSERT [dbo].[NhaCungCap] ([ID], [TenNhaCungCap], [NgayLap]) VALUES (18, N'Nhà cung câp 18', CAST(0xE53A0B00 AS Date))
INSERT [dbo].[NhaCungCap] ([ID], [TenNhaCungCap], [NgayLap]) VALUES (19, N'Nhà cung câp 19', CAST(0xE63A0B00 AS Date))
INSERT [dbo].[NhaCungCap] ([ID], [TenNhaCungCap], [NgayLap]) VALUES (20, N'Nhà cung câp 20', CAST(0xE73A0B00 AS Date))
INSERT [dbo].[NhaCungCap] ([ID], [TenNhaCungCap], [NgayLap]) VALUES (21, N'Nhà cung câp 21', CAST(0xE83A0B00 AS Date))
INSERT [dbo].[NhaCungCap] ([ID], [TenNhaCungCap], [NgayLap]) VALUES (22, N'Nhà cung câp 22', CAST(0xE93A0B00 AS Date))
INSERT [dbo].[NhaCungCap] ([ID], [TenNhaCungCap], [NgayLap]) VALUES (23, N'Nhà cung câp 23', CAST(0xEA3A0B00 AS Date))
INSERT [dbo].[NhaCungCap] ([ID], [TenNhaCungCap], [NgayLap]) VALUES (24, N'Nhà cung câp 24', CAST(0xEB3A0B00 AS Date))
INSERT [dbo].[NhaCungCap] ([ID], [TenNhaCungCap], [NgayLap]) VALUES (25, N'Nhà cung câp 25', CAST(0xEC3A0B00 AS Date))
INSERT [dbo].[NhaCungCap] ([ID], [TenNhaCungCap], [NgayLap]) VALUES (26, N'Nhà cung câp 26', CAST(0xED3A0B00 AS Date))
INSERT [dbo].[NhaCungCap] ([ID], [TenNhaCungCap], [NgayLap]) VALUES (27, N'Nhà cung câp 27', CAST(0xEE3A0B00 AS Date))
INSERT [dbo].[NhaCungCap] ([ID], [TenNhaCungCap], [NgayLap]) VALUES (28, N'Nhà cung câp 28', CAST(0xEF3A0B00 AS Date))
INSERT [dbo].[NhaCungCap] ([ID], [TenNhaCungCap], [NgayLap]) VALUES (29, N'Nhà cung câp 29', CAST(0xF03A0B00 AS Date))
INSERT [dbo].[NhaCungCap] ([ID], [TenNhaCungCap], [NgayLap]) VALUES (30, N'Nhà cung câp 30', CAST(0xF13A0B00 AS Date))
SET IDENTITY_INSERT [dbo].[NhaCungCap] OFF
SET IDENTITY_INSERT [dbo].[PhieuThu] ON 

INSERT [dbo].[PhieuThu] ([ID], [NgayLap], [DonHangID], [PhieuTraNhapHangID], [NhanVienID], [TongTien], [LoaiPhieuID], [KhachHangId], [GhiChu], [NhaCungCapID]) VALUES (2, CAST(0x0000A83F015EEC1D AS DateTime), 2, NULL, 1, 10000.0000, 2, 1, NULL, NULL)
INSERT [dbo].[PhieuThu] ([ID], [NgayLap], [DonHangID], [PhieuTraNhapHangID], [NhanVienID], [TongTien], [LoaiPhieuID], [KhachHangId], [GhiChu], [NhaCungCapID]) VALUES (3, CAST(0x0000A83F01615298 AS DateTime), 3, NULL, 1, 4500.0000, 2, 1, NULL, NULL)
INSERT [dbo].[PhieuThu] ([ID], [NgayLap], [DonHangID], [PhieuTraNhapHangID], [NhanVienID], [TongTien], [LoaiPhieuID], [KhachHangId], [GhiChu], [NhaCungCapID]) VALUES (4, CAST(0x0000A83F0161F16A AS DateTime), 3, NULL, 1, 5500.0000, 2, 1, NULL, NULL)
INSERT [dbo].[PhieuThu] ([ID], [NgayLap], [DonHangID], [PhieuTraNhapHangID], [NhanVienID], [TongTien], [LoaiPhieuID], [KhachHangId], [GhiChu], [NhaCungCapID]) VALUES (5, CAST(0x0000A83F016211BE AS DateTime), 5, NULL, 1, 5000.0000, 2, 1, NULL, NULL)
INSERT [dbo].[PhieuThu] ([ID], [NgayLap], [DonHangID], [PhieuTraNhapHangID], [NhanVienID], [TongTien], [LoaiPhieuID], [KhachHangId], [GhiChu], [NhaCungCapID]) VALUES (6, CAST(0x0000A83F01623E41 AS DateTime), 6, NULL, 1, 6000.0000, 2, 1, NULL, NULL)
INSERT [dbo].[PhieuThu] ([ID], [NgayLap], [DonHangID], [PhieuTraNhapHangID], [NhanVienID], [TongTien], [LoaiPhieuID], [KhachHangId], [GhiChu], [NhaCungCapID]) VALUES (7, CAST(0x0000A83F01644A9A AS DateTime), 7, NULL, 1, 17500.0000, 2, 2, NULL, NULL)
INSERT [dbo].[PhieuThu] ([ID], [NgayLap], [DonHangID], [PhieuTraNhapHangID], [NhanVienID], [TongTien], [LoaiPhieuID], [KhachHangId], [GhiChu], [NhaCungCapID]) VALUES (8, CAST(0x0000A83F016814CC AS DateTime), 8, NULL, 1, 9000.0000, 2, 2, NULL, NULL)
SET IDENTITY_INSERT [dbo].[PhieuThu] OFF
SET IDENTITY_INSERT [dbo].[LoaiHangHoa] ON 

INSERT [dbo].[LoaiHangHoa] ([ID], [TenLoaiHangHoa]) VALUES (1, N'Sách')
INSERT [dbo].[LoaiHangHoa] ([ID], [TenLoaiHangHoa]) VALUES (2, N'Văn phòng phẩm')
SET IDENTITY_INSERT [dbo].[LoaiHangHoa] OFF
SET IDENTITY_INSERT [dbo].[NhanHieu] ON 

INSERT [dbo].[NhanHieu] ([ID], [TenNhanHieu]) VALUES (1, N'Nhãn hiệu 1')
INSERT [dbo].[NhanHieu] ([ID], [TenNhanHieu]) VALUES (2, N'Nhãn hiệu 2')
INSERT [dbo].[NhanHieu] ([ID], [TenNhanHieu]) VALUES (3, N'Nhãn hiệu 3')
INSERT [dbo].[NhanHieu] ([ID], [TenNhanHieu]) VALUES (4, N'Nhãn hiệu 4')
INSERT [dbo].[NhanHieu] ([ID], [TenNhanHieu]) VALUES (5, N'Nhãn hiệu 5')
INSERT [dbo].[NhanHieu] ([ID], [TenNhanHieu]) VALUES (6, N'Nhãn hiệu 6')
INSERT [dbo].[NhanHieu] ([ID], [TenNhanHieu]) VALUES (7, N'Nhãn hiệu 7')
INSERT [dbo].[NhanHieu] ([ID], [TenNhanHieu]) VALUES (8, N'Nhãn hiệu 8')
INSERT [dbo].[NhanHieu] ([ID], [TenNhanHieu]) VALUES (9, N'Nhãn hiệu 9')
INSERT [dbo].[NhanHieu] ([ID], [TenNhanHieu]) VALUES (10, N'Nhãn hiệu 10')
INSERT [dbo].[NhanHieu] ([ID], [TenNhanHieu]) VALUES (11, N'Nhãn hiệu 11')
INSERT [dbo].[NhanHieu] ([ID], [TenNhanHieu]) VALUES (12, N'Nhãn hiệu 12')
INSERT [dbo].[NhanHieu] ([ID], [TenNhanHieu]) VALUES (13, N'Nhãn hiệu 13')
INSERT [dbo].[NhanHieu] ([ID], [TenNhanHieu]) VALUES (14, N'Nhãn hiệu 14')
INSERT [dbo].[NhanHieu] ([ID], [TenNhanHieu]) VALUES (15, N'Nhãn hiệu 15')
INSERT [dbo].[NhanHieu] ([ID], [TenNhanHieu]) VALUES (16, N'Nhãn hiệu 16')
INSERT [dbo].[NhanHieu] ([ID], [TenNhanHieu]) VALUES (17, N'Nhãn hiệu 17')
INSERT [dbo].[NhanHieu] ([ID], [TenNhanHieu]) VALUES (18, N'Nhãn hiệu 18')
INSERT [dbo].[NhanHieu] ([ID], [TenNhanHieu]) VALUES (19, N'Nhãn hiệu 19')
INSERT [dbo].[NhanHieu] ([ID], [TenNhanHieu]) VALUES (20, N'Nhãn hiệu 20')
INSERT [dbo].[NhanHieu] ([ID], [TenNhanHieu]) VALUES (21, N'Nhãn hiệu 21')
INSERT [dbo].[NhanHieu] ([ID], [TenNhanHieu]) VALUES (22, N'Nhãn hiệu 22')
INSERT [dbo].[NhanHieu] ([ID], [TenNhanHieu]) VALUES (23, N'Nhãn hiệu 23')
INSERT [dbo].[NhanHieu] ([ID], [TenNhanHieu]) VALUES (24, N'Nhãn hiệu 24')
INSERT [dbo].[NhanHieu] ([ID], [TenNhanHieu]) VALUES (25, N'Nhãn hiệu 25')
INSERT [dbo].[NhanHieu] ([ID], [TenNhanHieu]) VALUES (26, N'Nhãn hiệu 26')
INSERT [dbo].[NhanHieu] ([ID], [TenNhanHieu]) VALUES (27, N'Nhãn hiệu 27')
INSERT [dbo].[NhanHieu] ([ID], [TenNhanHieu]) VALUES (28, N'Nhãn hiệu 28')
INSERT [dbo].[NhanHieu] ([ID], [TenNhanHieu]) VALUES (29, N'Nhãn hiệu 29')
INSERT [dbo].[NhanHieu] ([ID], [TenNhanHieu]) VALUES (30, N'Nhãn hiệu 30')
SET IDENTITY_INSERT [dbo].[NhanHieu] OFF
SET IDENTITY_INSERT [dbo].[HangHoa] ON 

INSERT [dbo].[HangHoa] ([ID], [NgayLap], [TenHangHoa], [LoaiHangHoaId], [TonKho], [NhaCungCapID], [NhanHieuID], [GiaKhoiTao], [GiaNhap], [GiaBanSi], [GiaBanLe], [TrangThaiID], [DaBan], [ImageUrl]) VALUES (1, CAST(0xDB3A0B00 AS Date), N'Hàng hóa 1', 1, 987, 1, 1, 1000.0000, 1500.0000, 2000.0000, 2500.0000, 1, 113, NULL)
INSERT [dbo].[HangHoa] ([ID], [NgayLap], [TenHangHoa], [LoaiHangHoaId], [TonKho], [NhaCungCapID], [NhanHieuID], [GiaKhoiTao], [GiaNhap], [GiaBanSi], [GiaBanLe], [TrangThaiID], [DaBan], [ImageUrl]) VALUES (2, CAST(0xDC3A0B00 AS Date), N'Hàng hóa 2', 1, 988, 2, 2, 1001.0000, 1500.0000, 2000.0000, 2500.0000, 1, 113, NULL)
INSERT [dbo].[HangHoa] ([ID], [NgayLap], [TenHangHoa], [LoaiHangHoaId], [TonKho], [NhaCungCapID], [NhanHieuID], [GiaKhoiTao], [GiaNhap], [GiaBanSi], [GiaBanLe], [TrangThaiID], [DaBan], [ImageUrl]) VALUES (3, CAST(0xDD3A0B00 AS Date), N'Hàng hóa 3', 2, 1002, 3, 3, 1002.0000, 1500.0000, 2000.0000, 2500.0000, 1, 101, NULL)
INSERT [dbo].[HangHoa] ([ID], [NgayLap], [TenHangHoa], [LoaiHangHoaId], [TonKho], [NhaCungCapID], [NhanHieuID], [GiaKhoiTao], [GiaNhap], [GiaBanSi], [GiaBanLe], [TrangThaiID], [DaBan], [ImageUrl]) VALUES (4, CAST(0xDE3A0B00 AS Date), N'Hàng hóa 4', 1, 997, 4, 4, 1003.0000, 1500.0000, 2000.0000, 2500.0000, 1, 108, NULL)
INSERT [dbo].[HangHoa] ([ID], [NgayLap], [TenHangHoa], [LoaiHangHoaId], [TonKho], [NhaCungCapID], [NhanHieuID], [GiaKhoiTao], [GiaNhap], [GiaBanSi], [GiaBanLe], [TrangThaiID], [DaBan], [ImageUrl]) VALUES (5, CAST(0xDF3A0B00 AS Date), N'Hàng hóa 5', 1, 1004, 5, 5, 1004.0000, 1500.0000, 2000.0000, 2500.0000, 1, 103, NULL)
INSERT [dbo].[HangHoa] ([ID], [NgayLap], [TenHangHoa], [LoaiHangHoaId], [TonKho], [NhaCungCapID], [NhanHieuID], [GiaKhoiTao], [GiaNhap], [GiaBanSi], [GiaBanLe], [TrangThaiID], [DaBan], [ImageUrl]) VALUES (6, CAST(0xE03A0B00 AS Date), N'Hàng hóa 6', 2, 1005, 6, 6, 1005.0000, 1500.0000, 2000.0000, 2500.0000, 1, 104, NULL)
INSERT [dbo].[HangHoa] ([ID], [NgayLap], [TenHangHoa], [LoaiHangHoaId], [TonKho], [NhaCungCapID], [NhanHieuID], [GiaKhoiTao], [GiaNhap], [GiaBanSi], [GiaBanLe], [TrangThaiID], [DaBan], [ImageUrl]) VALUES (7, CAST(0xE13A0B00 AS Date), N'Hàng hóa 7', 1, 1006, 7, 7, 1006.0000, 1500.0000, 2000.0000, 2500.0000, 1, 105, NULL)
INSERT [dbo].[HangHoa] ([ID], [NgayLap], [TenHangHoa], [LoaiHangHoaId], [TonKho], [NhaCungCapID], [NhanHieuID], [GiaKhoiTao], [GiaNhap], [GiaBanSi], [GiaBanLe], [TrangThaiID], [DaBan], [ImageUrl]) VALUES (8, CAST(0xE23A0B00 AS Date), N'Hàng hóa 8', 1, 1007, 8, 8, 1007.0000, 1500.0000, 2000.0000, 2500.0000, 1, 106, NULL)
INSERT [dbo].[HangHoa] ([ID], [NgayLap], [TenHangHoa], [LoaiHangHoaId], [TonKho], [NhaCungCapID], [NhanHieuID], [GiaKhoiTao], [GiaNhap], [GiaBanSi], [GiaBanLe], [TrangThaiID], [DaBan], [ImageUrl]) VALUES (9, CAST(0xE33A0B00 AS Date), N'Hàng hóa 9', 2, 1008, 9, 9, 1008.0000, 1500.0000, 2000.0000, 2500.0000, 1, 107, NULL)
INSERT [dbo].[HangHoa] ([ID], [NgayLap], [TenHangHoa], [LoaiHangHoaId], [TonKho], [NhaCungCapID], [NhanHieuID], [GiaKhoiTao], [GiaNhap], [GiaBanSi], [GiaBanLe], [TrangThaiID], [DaBan], [ImageUrl]) VALUES (10, CAST(0xE43A0B00 AS Date), N'Hàng hóa 10', 1, 1009, 10, 10, 1009.0000, 1500.0000, 2000.0000, 2500.0000, 1, 108, NULL)
INSERT [dbo].[HangHoa] ([ID], [NgayLap], [TenHangHoa], [LoaiHangHoaId], [TonKho], [NhaCungCapID], [NhanHieuID], [GiaKhoiTao], [GiaNhap], [GiaBanSi], [GiaBanLe], [TrangThaiID], [DaBan], [ImageUrl]) VALUES (11, CAST(0xE53A0B00 AS Date), N'Hàng hóa 11', 1, 1010, 11, 11, 1010.0000, 1500.0000, 2000.0000, 2500.0000, 1, 109, NULL)
INSERT [dbo].[HangHoa] ([ID], [NgayLap], [TenHangHoa], [LoaiHangHoaId], [TonKho], [NhaCungCapID], [NhanHieuID], [GiaKhoiTao], [GiaNhap], [GiaBanSi], [GiaBanLe], [TrangThaiID], [DaBan], [ImageUrl]) VALUES (12, CAST(0xE63A0B00 AS Date), N'Hàng hóa 12', 2, 1011, 12, 12, 1011.0000, 1500.0000, 2000.0000, 2500.0000, 1, 110, NULL)
INSERT [dbo].[HangHoa] ([ID], [NgayLap], [TenHangHoa], [LoaiHangHoaId], [TonKho], [NhaCungCapID], [NhanHieuID], [GiaKhoiTao], [GiaNhap], [GiaBanSi], [GiaBanLe], [TrangThaiID], [DaBan], [ImageUrl]) VALUES (13, CAST(0xE73A0B00 AS Date), N'Hàng hóa 13', 1, 1012, 13, 13, 1012.0000, 1500.0000, 2000.0000, 2500.0000, 1, 111, NULL)
INSERT [dbo].[HangHoa] ([ID], [NgayLap], [TenHangHoa], [LoaiHangHoaId], [TonKho], [NhaCungCapID], [NhanHieuID], [GiaKhoiTao], [GiaNhap], [GiaBanSi], [GiaBanLe], [TrangThaiID], [DaBan], [ImageUrl]) VALUES (14, CAST(0xE83A0B00 AS Date), N'Hàng hóa 14', 2, 1013, 14, 14, 1013.0000, 1500.0000, 2000.0000, 2500.0000, 1, 112, NULL)
INSERT [dbo].[HangHoa] ([ID], [NgayLap], [TenHangHoa], [LoaiHangHoaId], [TonKho], [NhaCungCapID], [NhanHieuID], [GiaKhoiTao], [GiaNhap], [GiaBanSi], [GiaBanLe], [TrangThaiID], [DaBan], [ImageUrl]) VALUES (15, CAST(0xE93A0B00 AS Date), N'Hàng hóa 15', 1, 1014, 15, 15, 1014.0000, 1500.0000, 2000.0000, 2500.0000, 1, 113, NULL)
INSERT [dbo].[HangHoa] ([ID], [NgayLap], [TenHangHoa], [LoaiHangHoaId], [TonKho], [NhaCungCapID], [NhanHieuID], [GiaKhoiTao], [GiaNhap], [GiaBanSi], [GiaBanLe], [TrangThaiID], [DaBan], [ImageUrl]) VALUES (16, CAST(0xEA3A0B00 AS Date), N'Hàng hóa 16', 2, 1015, 16, 16, 1015.0000, 1500.0000, 2000.0000, 2500.0000, 1, 114, NULL)
INSERT [dbo].[HangHoa] ([ID], [NgayLap], [TenHangHoa], [LoaiHangHoaId], [TonKho], [NhaCungCapID], [NhanHieuID], [GiaKhoiTao], [GiaNhap], [GiaBanSi], [GiaBanLe], [TrangThaiID], [DaBan], [ImageUrl]) VALUES (17, CAST(0xEB3A0B00 AS Date), N'Hàng hóa 17', 1, 1016, 17, 17, 1016.0000, 1500.0000, 2000.0000, 2500.0000, 1, 115, NULL)
INSERT [dbo].[HangHoa] ([ID], [NgayLap], [TenHangHoa], [LoaiHangHoaId], [TonKho], [NhaCungCapID], [NhanHieuID], [GiaKhoiTao], [GiaNhap], [GiaBanSi], [GiaBanLe], [TrangThaiID], [DaBan], [ImageUrl]) VALUES (18, CAST(0xEC3A0B00 AS Date), N'Hàng hóa 18', 1, 1017, 18, 18, 1017.0000, 1500.0000, 2000.0000, 2500.0000, 1, 116, NULL)
INSERT [dbo].[HangHoa] ([ID], [NgayLap], [TenHangHoa], [LoaiHangHoaId], [TonKho], [NhaCungCapID], [NhanHieuID], [GiaKhoiTao], [GiaNhap], [GiaBanSi], [GiaBanLe], [TrangThaiID], [DaBan], [ImageUrl]) VALUES (19, CAST(0xED3A0B00 AS Date), N'Hàng hóa 19', 2, 1018, 19, 19, 1018.0000, 1500.0000, 2000.0000, 2500.0000, 1, 117, NULL)
INSERT [dbo].[HangHoa] ([ID], [NgayLap], [TenHangHoa], [LoaiHangHoaId], [TonKho], [NhaCungCapID], [NhanHieuID], [GiaKhoiTao], [GiaNhap], [GiaBanSi], [GiaBanLe], [TrangThaiID], [DaBan], [ImageUrl]) VALUES (20, CAST(0xEE3A0B00 AS Date), N'Hàng hóa 20', 1, 1019, 20, 20, 1019.0000, 1500.0000, 2000.0000, 2500.0000, 1, 118, NULL)
INSERT [dbo].[HangHoa] ([ID], [NgayLap], [TenHangHoa], [LoaiHangHoaId], [TonKho], [NhaCungCapID], [NhanHieuID], [GiaKhoiTao], [GiaNhap], [GiaBanSi], [GiaBanLe], [TrangThaiID], [DaBan], [ImageUrl]) VALUES (21, CAST(0x9A3D0B00 AS Date), N'asdasd', 1, 2000, 5, 5, 2000.0000, 2000.0000, 2000.0000, 2000.0000, 1, 0, NULL)
INSERT [dbo].[HangHoa] ([ID], [NgayLap], [TenHangHoa], [LoaiHangHoaId], [TonKho], [NhaCungCapID], [NhanHieuID], [GiaKhoiTao], [GiaNhap], [GiaBanSi], [GiaBanLe], [TrangThaiID], [DaBan], [ImageUrl]) VALUES (22, CAST(0x9A3D0B00 AS Date), N'asdasd', 1, 10000, 5, 5, 10000.0000, 10000.0000, 10000.0000, 10000.0000, 1, 0, NULL)
SET IDENTITY_INSERT [dbo].[HangHoa] OFF
SET IDENTITY_INSERT [dbo].[ChiTietDonHang] ON 

INSERT [dbo].[ChiTietDonHang] ([ID], [DonHangID], [HangHoaID], [SoLuong], [GiaBan]) VALUES (3, 2, 1, 1, 2500.0000)
INSERT [dbo].[ChiTietDonHang] ([ID], [DonHangID], [HangHoaID], [SoLuong], [GiaBan]) VALUES (4, 2, 2, 2, 2500.0000)
INSERT [dbo].[ChiTietDonHang] ([ID], [DonHangID], [HangHoaID], [SoLuong], [GiaBan]) VALUES (5, 2, 4, 1, 2500.0000)
INSERT [dbo].[ChiTietDonHang] ([ID], [DonHangID], [HangHoaID], [SoLuong], [GiaBan]) VALUES (6, 3, 2, 1, 2500.0000)
INSERT [dbo].[ChiTietDonHang] ([ID], [DonHangID], [HangHoaID], [SoLuong], [GiaBan]) VALUES (7, 3, 1, 2, 2500.0000)
INSERT [dbo].[ChiTietDonHang] ([ID], [DonHangID], [HangHoaID], [SoLuong], [GiaBan]) VALUES (8, 3, 4, 1, 2500.0000)
INSERT [dbo].[ChiTietDonHang] ([ID], [DonHangID], [HangHoaID], [SoLuong], [GiaBan]) VALUES (9, 4, 1, 1, 2500.0000)
INSERT [dbo].[ChiTietDonHang] ([ID], [DonHangID], [HangHoaID], [SoLuong], [GiaBan]) VALUES (10, 4, 2, 4, 2500.0000)
INSERT [dbo].[ChiTietDonHang] ([ID], [DonHangID], [HangHoaID], [SoLuong], [GiaBan]) VALUES (11, 4, 4, 2, 2500.0000)
INSERT [dbo].[ChiTietDonHang] ([ID], [DonHangID], [HangHoaID], [SoLuong], [GiaBan]) VALUES (12, 5, 1, 1, 2500.0000)
INSERT [dbo].[ChiTietDonHang] ([ID], [DonHangID], [HangHoaID], [SoLuong], [GiaBan]) VALUES (13, 5, 2, 1, 2500.0000)
INSERT [dbo].[ChiTietDonHang] ([ID], [DonHangID], [HangHoaID], [SoLuong], [GiaBan]) VALUES (14, 5, 4, 1, 2500.0000)
INSERT [dbo].[ChiTietDonHang] ([ID], [DonHangID], [HangHoaID], [SoLuong], [GiaBan]) VALUES (15, 6, 2, 1, 2500.0000)
INSERT [dbo].[ChiTietDonHang] ([ID], [DonHangID], [HangHoaID], [SoLuong], [GiaBan]) VALUES (16, 6, 1, 1, 2500.0000)
INSERT [dbo].[ChiTietDonHang] ([ID], [DonHangID], [HangHoaID], [SoLuong], [GiaBan]) VALUES (17, 6, 4, 1, 2500.0000)
INSERT [dbo].[ChiTietDonHang] ([ID], [DonHangID], [HangHoaID], [SoLuong], [GiaBan]) VALUES (18, 7, 2, 2, 2500.0000)
INSERT [dbo].[ChiTietDonHang] ([ID], [DonHangID], [HangHoaID], [SoLuong], [GiaBan]) VALUES (19, 7, 1, 5, 2500.0000)
INSERT [dbo].[ChiTietDonHang] ([ID], [DonHangID], [HangHoaID], [SoLuong], [GiaBan]) VALUES (20, 8, 1, 2, 2500.0000)
INSERT [dbo].[ChiTietDonHang] ([ID], [DonHangID], [HangHoaID], [SoLuong], [GiaBan]) VALUES (21, 8, 2, 2, 2500.0000)
SET IDENTITY_INSERT [dbo].[ChiTietDonHang] OFF
