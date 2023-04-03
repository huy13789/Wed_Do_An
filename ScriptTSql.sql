/*
Created		09/03/2023
Modified		23/03/2023
Project		
Model		
Company		
Author		
Version		
Database		MS SQL 7 
*/


Create table [DONHANG] (
	[MaDH] Integer IDENTITY(1,1) NOT NULL,
	[NgayDH] Datetime NULL,
	[Ngaygiao] Datetime NULL,
	[Trangthai] Bit NULL,
	[MaKH] Integer NOT NULL,
Primary Key  ([MaDH])
) 
go

Create table [THUONGHIEU] (
	[MaTH] Integer IDENTITY(1,1) NOT NULL,
	[TenTH] Nvarchar(50) NULL,
	[Mota] Nvarchar(max) NULL,
	[QuocgiaSX] Nchar(50) NULL,
Primary Key  ([MaTH])
) 
go

Create table [KHACHHANG] (
	[MaKH] Integer IDENTITY(1,1) NOT NULL,
	[HoTen] NVarchar(50) NULL,
	[DiachiKH] Nvarchar(200) NULL,
	[EmailKH] Varchar(100) NULL,
	[DienthoaiKh] Varchar(50) NULL,
	[tentk] Char(20) NULL,
	[mk] Char(50) NULL,
	[MaRole] Integer NOT NULL,
Primary Key  ([MaKH])
) 
go

Create table [SANPHAM] (
	[MaSP] Integer IDENTITY(1,1) NOT NULL,
	[TenSP] Nvarchar(100) NOT NULL,
	[MotaSP] Nvarchar(max) NULL,
	[HinhanhSP] Varchar(50) NULL,
	[NgaycapnhatSP] Smalldatetime NULL,
	[SoluongSP] Integer NULL,
	[GiaSP] Decimal(18,0) NULL,
	[MaDM] Integer NOT NULL,
	[MaTH] Integer NOT NULL,
Primary Key  ([MaSP])
) 
go

Create table [DANHMUC] (
	[MaDM] Integer IDENTITY(1,1) NOT NULL,
	[TenDM] Nvarchar(50) NULL,
	[Mota] nVarchar(max) NULL,
Primary Key  ([MaDM])
) 
go

Create table [CHITIETDONHANG] (
	[MaDH] Integer IDENTITY(1,1) NOT NULL,
	[Soluong] int NULL,
	[Dongia] Decimal(18,0) NULL,
	[MaSP] Integer NOT NULL,
Primary Key  ([MaDH],[MaSP])
) 
go

Create table [Role] (
	[MaRole] Integer NOT NULL,
	[TenRole] Char(10) NULL,
Primary Key  ([MaRole])
) 
go


Alter table [CHITIETDONHANG] add  foreign key([MaDH]) references [DONHANG] ([MaDH]) 
go
Alter table [SANPHAM] add  foreign key([MaTH]) references [THUONGHIEU] ([MaTH]) 
go
Alter table [DONHANG] add  foreign key([MaKH]) references [KHACHHANG] ([MaKH]) 
go
Alter table [CHITIETDONHANG] add  foreign key([MaSP]) references [SANPHAM] ([MaSP]) 
go
Alter table [SANPHAM] add  foreign key([MaDM]) references [DANHMUC] ([MaDM]) 
go
Alter table [KHACHHANG] add  foreign key([MaRole]) references [Role] ([MaRole]) 
go


Set quoted_identifier on
go

Set quoted_identifier off
go


