CREATE DATABASE QLPM
USE QLPM
SET DATEFORMAT DMY
/* GREENHOUSE130618
BENHNHAN(MaBenhNhan, HoTen, GioiTinh, NamSinh, DiaChi)
PHIEUKB(MaPKB, NgayKham, MaBenhNhan, TrieuChung, LoaiBenh)
THAMSO(SoBenhNhanMax, SLBenh, SLThuoc, SLDonVi, SLCachDung, MucTienKham)
CHITIETPKB(MaCTPKB, MaPKB, MaThuoc, SLThuoc, CachDung)
THUOC(MaThuoc, TenThuoc, MaDVThuoc, DonGia)
HOADON(MaHoaDon, MaPKB, TienKham, TienThuoc, TongTien)
DOANHTHU(MaDoanhThu, Ngay, Thang, SoBenhNhan, DoanhThu, TyLe)
THONGKETHUOC(MaTKThuoc, Thang, MaThuoc, SLThuoc, SoLanDung)
*/
CREATE TABLE TaiKhoan(
	id 			INT IDENTITY,
	username	NVARCHAR(50) PRIMARY KEY NOT NULL,
	tenhienthi	NVARCHAR(50) NOT NULL,
	password	NVARCHAR(50) NOT NULL,
	TYPE INT NOT NULL
)  
CREATE TABLE BenhNhan(
	id 			INT IDENTITY,
	MaBenhNhan	NVARCHAR(4) PRIMARY KEY NOT NULL,
	HoTen		NVARCHAR(50) NOT NULL,
	GioiTinh	NVARCHAR(3) NOT NULL,
	NamSinh		INT NOT NULL,
	DiaChi		NVARCHAR(100) NOT NULL,
)
CREATE TABLE Thuoc(
	id 			INT IDENTITY,
	MaThuoc		NVARCHAR(4) PRIMARY KEY NOT NULL,
	TenThuoc	NVARCHAR(50) NOT NULL,--30loai
	DonVi		NVARCHAR(4) NOT NULL,--2 loai
	DonGia		FLOAT NOT NULL DEFAULT 0,
)
CREATE TABLE PhieuKB(
	id 			INT IDENTITY,
	MaPKB		NVARCHAR(4) PRIMARY KEY NOT NULL,
	MaBenhNhan	NVARCHAR(4) NOT NULL,
	LoaiBenh	NVARCHAR(50) NOT NULL,
	TrieuChung	NVARCHAR(100) NOT NULL,--4cach
	NgayKham	DATE NOT NULL,
)
CREATE TABLE CHITIETPKB(
	id 			INT IDENTITY,
	MaCTPKB		NVARCHAR(4) PRIMARY KEY NOT NULL,
	MaPKB		NVARCHAR(4) NOT NULL,
	MaThuoc		NVARCHAR(4) NOT NULL,
	SLThuoc		INT NOT NULL,
	CachDung	NVARCHAR(100) NOT NULL,
)
CREATE TABLE HoaDon(
	id 			INT IDENTITY,
	MaHoaDon		NVARCHAR(4) PRIMARY KEY NOT NULL,
	MaPKB		NVARCHAR(4) NOT NULL,
	TienKham	FLOAT NOT NULL DEFAULT 0,
	TienThuoc	FLOAT NOT NULL DEFAULT 0,
	TongTien	FLOAT NOT NULL DEFAULT 0,
	FOREIGN KEY (mapkb) REFERENCES PhieuKB(mapkb),
)
CREATE TABLE DOANHTHU(
	id 			INT IDENTITY,
	MaDoanhThu	NVARCHAR(4) PRIMARY KEY NOT NULL,
	Ngay		DATE,
	Thang		INT,
	SoBenhNhan
	DoanhThu
	TyLe
	FOREIGN KEY (mapkb) REFERENCES PhieuKB(mapkb),
	FOREIGN KEY (mathuoc) REFERENCES Thuoc(mathuoc),
	FOREIGN KEY (MaBenh) REFERENCES Benh(MaBenh),
)
CREATE TABLE TKTHUOC(
	id 			INT IDENTITY,
	MaTKThuoc 	NVARCHAR(4) PRIMARY KEY NOT NULL,
	Thang		INT, 
	MaThuoc		NVARCHAR(4),
	SLThuoc		INT, 
	SoLanDung	INT,
)
--FOREIGN KEY
ALTER TABLE PhieuKB
ADD CONSTRAINT FK1
FOREIGN KEY (MaBenhNhan)
REFERENCES BenhNhan (MaBenhNhan);

ALTER TABLE CHITIETPKB
ADD CONSTRAINT FK2
FOREIGN KEY (MaPKB) REFERENCES PHIEUKB(MaPKB);

ALTER TABLE CHITIETPKB
ADD CONSTRAINT FK3
FOREIGN KEY (MaThuoc) REFERENCES THUOC(MaThuoc);

ALTER TABLE HoaDon
ADD CONSTRAINT FK3
FOREIGN KEY (MaPKB) REFERENCES PHIEUKB(MaPKB);
--CONSTRAINT------------------------------------------------------------------------------
ALTER TABLE BenhNhan
ADD  CONSTRAINT cont1
CHECK(GioiTinh IN (N'Nam',N'Nữ'))

ALTER TABLE Thuoc
ADD  CONSTRAINT Cont2
CHECK(DonVi IN (N'Chai',N'Viên'))

--ADD DATA--------------------------------------------------------------------------------
--Benh Nhan
INSERT INTO BenhNhan VALUES('BN01',N'Phạm Hoài Phương',	N'Nam',1997,N'Thủ Đức')
INSERT INTO BenhNhan VALUES('BN02',N'Trần Nam Bàng',	N'Nam',1997,N'Bình Thạnh')
INSERT INTO BenhNhan VALUES('BN03',N'Lê Vũ Trùng Dương',N'Nam',1997,N'Bình Tân')
--Thuoc
INSERT INTO Thuoc VALUES('Th01', N'Adsjndjs name', N'Viên', 69000)
--Phieu Kham Benh
INSERT INTO PhieuKB VALUES('PK01','BN04',N'Tim mạch', N'Huyết áp tăng','19/4/2018')
--CTPhieuKB
INSERT INTO CTPhieuKB VALUES('CT01','PK01','Th01',10, N'Uống')
--HoaDon
INSERT INTO HoaDon VALUES('HD01','PK01',30000,0,30000)