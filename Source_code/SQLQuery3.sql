CREATE DATABASE QLPM4
USE QLPM4
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
	MaBN 			INT IDENTITY PRIMARY KEY NOT NULL,
	HoVaTen		NVARCHAR(50) NOT NULL,
	GioiTinh	NVARCHAR(3) NOT NULL,
	NamSinh		INT NOT NULL,
	DiaChi		NVARCHAR(100) NOT NULL,
)
CREATE TABLE Thuoc(
	MaThuoc 			INT IDENTITY PRIMARY KEY NOT NULL,
	TenThuoc	NVARCHAR(50) NOT NULL,--30loai
	DonVi		NVARCHAR(4) NOT NULL,--2 loai
	DonGia		INT NOT NULL DEFAULT 0,
)
CREATE TABLE PhieuKB(
	MaPKB 			INT IDENTITY  PRIMARY KEY NOT NULL,
	MaBN		INT NOT NULL,
	LoaiBenh	NVARCHAR(50) NOT NULL,
	TrieuChung	NVARCHAR(100) NOT NULL,--4cach
	NgayKham	DATE NOT NULL,
)
CREATE TABLE CHITIETPKB(
	MaCTPKB 			INT IDENTITY  PRIMARY KEY NOT NULL,

	MaPKB		INT NOT NULL,
	MaThuoc		INT NOT NULL,
	SLThuoc		INT NOT NULL,
	CachDung	NVARCHAR(100) NOT NULL,
	
)
CREATE TABLE HoaDon(
	MaHD 			INT IDENTITY PRIMARY KEY NOT NULL,

	MaPKB		INT NOT NULL,
	TienKham	FLOAT NOT NULL DEFAULT 0,
	TienThuoc	FLOAT NOT NULL DEFAULT 0,
	TongTien	FLOAT NOT NULL DEFAULT 0,
	
)

--FOREIGN KEY
ALTER TABLE PhieuKB
ADD CONSTRAINT FK1
FOREIGN KEY (MaBN)
REFERENCES BenhNhan (MaBN);

ALTER TABLE CHITIETPKB
ADD CONSTRAINT FK2
FOREIGN KEY (MaPKB) REFERENCES PHIEUKB(MaPKB);

ALTER TABLE CHITIETPKB
ADD CONSTRAINT FK3
FOREIGN KEY (MaThuoc) REFERENCES THUOC(MaThuoc);

ALTER TABLE HoaDon
ADD CONSTRAINT FK4
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
INSERT INTO BenhNhan VALUES(N'Phạm Hoài Phương',	N'Nam',1997,N'Thủ Đức')
INSERT INTO BenhNhan VALUES(N'Trần Nam Bàng',	N'Nam',1997,N'Bình Thạnh')
INSERT INTO BenhNhan VALUES(N'Lê Vũ Trùng Dương',N'Nam',1997,N'Bình Tân')
--Thuoc
INSERT INTO Thuoc VALUES( N'Adsjndjs name', N'Viên', 69000)
INSERT INTO Thuoc VALUES( N'Đau Chân', N'Viên', 19000)
INSERT INTO Thuoc VALUES( N'Đau Bụng', N'Viên', 40000)

--Phieu Kham Benh
INSERT INTO PhieuKB VALUES('1',N'Tim mạch', N'Huyết áp tăng','2018/04/19')
INSERT INTO PhieuKB VALUES('2',N'Dạ Dày', N'Đau Bụng','2018/04/25')
INSERT INTO PhieuKB VALUES('3',N'Tim mạch', N'Huyết áp tăng','2018/04/31')

--CTPhieuKB
INSERT INTO CHITIETPKB VALUES('1','1',10, N'Uống')
INSERT INTO CHITIETPKB VALUES('2','2',5, N'Uống')
INSERT INTO CHITIETPKB VALUES('3','2',5, N'Uống')


--HoaDon
INSERT INTO HoaDon VALUES('1',30000,0,30000)
INSERT INTO HoaDon VALUES('2',10500,0,15000)