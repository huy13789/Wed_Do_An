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
	[Trangthai] bit NULL,
	[MaKH] Integer NOT NULL,
Primary Key  ([MaDH])
) 
go

Create table [THUONGHIEU] (
	[MaTH] Integer IDENTITY(1,1) NOT NULL,
	[TenTH] NVarchar(50) NULL,
	[Mota] NVarchar(400) NULL,
	[QuocgiaSX] NVarchar(50) NULL,
Primary Key  ([MaTH])
) 
go

Create table [KHACHHANG] (
	[MaKH] Integer IDENTITY(1,1)NOT NULL,
	[HoTen] nVarchar(50) NULL,
	[DiachiKH] nVarchar(200) NULL,
	[EmailKH] Varchar(100) NULL,
	[DienthoaiKh] char(11) NULL,
	[tentk] Char(20) NULL,
	[mk] Char(10) NULL,
	[MaRole] Integer NOT NULL,
Primary Key  ([MaKH])
) 
go

Create table [SANPHAM] (
	[MaSP] Integer IDENTITY(1,1) NOT NULL,
	[TenSP] Nvarchar(100) NOT NULL,
	[MotaSP] Nvarchar(400) NULL,
	[HinhanhSP] Varchar(50) NULL,
	[NgaycapnhatSP] Smalldatetime NULL,
	[SoluongSP] Integer NULL,
	[GiaSP] Decimal(18,0) NULL,
	[MaTH] Integer NOT NULL,
	[MaDM] Integer NOT NULL,
Primary Key  ([MaSP],[MaTH],[MaDM])
) 
go

Create table [DANHMUC] (
	[MaDM] Integer IDENTITY(1,1) NOT NULL,
	[TenDM] nVarchar(50) NULL,
	[Mota] nVarchar(400) NULL,
Primary Key  ([MaDM])
) 
go

Create table [CHITIETDONHANG] (
	[MaSP] Integer NOT NULL,
	[MaDH] Integer NOT NULL,
	[Soluong] int NULL,
	[Dongia] decimal(18,0),
	[MaTH] Integer NOT NULL,
	[MaDM] Integer NOT NULL,
Primary Key  ([MaSP],[MaDH],[MaTH],[MaDM])
) 
go

Create table [Role] (
	[MaRole] bit NOT NULL,
	[TenRole] nvarchar(10) NULL,
Primary Key  ([MaRole])
) 
go


Alter table [CHITIETDONHANG] add  foreign key([MaDH]) references [DONHANG] ([MaDH]) 
go
Alter table [SANPHAM] add  foreign key([MaTH]) references [THUONGHIEU] ([MaTH]) 
go
Alter table [DONHANG] add  foreign key([MaKH]) references [KHACHHANG] ([MaKH]) 
go
Alter table [CHITIETDONHANG] add  foreign key([MaSP],[MaTH],[MaDM]) references [SANPHAM] ([MaSP],[MaTH],[MaDM]) 
go
Alter table [SANPHAM] add  foreign key([MaDM]) references [DANHMUC] ([MaDM]) 
go
Alter table [KHACHHANG] add  foreign key([MaRole]) references [Role] ([MaRole]) 
go


Set quoted_identifier on
go

Set quoted_identifier off
go


