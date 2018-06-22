CREATE DATABASE QLPM4
USE QLPM4
SET DATEFORMAT DMY
/*
BENHNHAN(mabn, hoten, gioitinh, namsinh, diachi)
PHIEUKB(mapkb, mabn, mahd, mabenh, trieuchung, ngaykham)
HOADON(mahd, mapkb, tienthuoc, tienkham, tongtien)
THUOC(mathuoc, tenthuoc, donvi, dongia, cachdung)
CTPHIEUKB(mactpkb, mapkb, mathuoc, slthuoc, mabenh)
BENH(mabenh, tenbenh)
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
	mabn		NVARCHAR(4) PRIMARY KEY NOT NULL,
	mapkb		NVARCHAR(4) NOT NULL,
	hoten		NVARCHAR(50) NOT NULL,
	gioitinh	NVARCHAR(3) NOT NULL,
	namsinh		INT NOT NULL,
	diachi		NVARCHAR(100) NOT NULL,
)
CREATE TABLE Benh(
	id INT IDENTITY,
	mabenh		NVARCHAR(4) PRIMARY KEY NOT NULL,
	tenbenh		NVARCHAR(50) NOT NULL,
)
CREATE TABLE Thuoc(
	id 			INT IDENTITY,
	mathuoc		NVARCHAR(4) PRIMARY KEY NOT NULL,
	tenthuoc	NVARCHAR(50) NOT NULL,--30loai
	cachdung	NVARCHAR(100) NOT NULL,
	donvi		NVARCHAR(50) NOT NULL,--2 loai
	dongia		FLOAT NOT NULL DEFAULT 0,
)
CREATE TABLE PhieuKB(
	id 			INT IDENTITY,
	mapkb		NVARCHAR(4) PRIMARY KEY NOT NULL,
	mabn		NVARCHAR(4) NOT NULL,
	mabenh		NVARCHAR(4) NOT NULL,
	mahd		NVARCHAR(4) NOT NULL,
	trieuchung	NVARCHAR(100) NOT NULL,--4cach
	ngaykham	DATE,
	FOREIGN KEY (mabenh) REFERENCES Benh(mabenh),
	FOREIGN KEY (mabn) REFERENCES BenhNhan(mabn),
)
CREATE TABLE HoaDon(
	id 			INT IDENTITY,
	mahd		NVARCHAR(4) PRIMARY KEY NOT NULL,
	mapkb		NVARCHAR(4) NOT NULL,
	tienkham	FLOAT NOT NULL DEFAULT 0,
	tienthuoc	FLOAT NOT NULL DEFAULT 0,
	tongtien	FLOAT NOT NULL DEFAULT 0,
	FOREIGN KEY (mapkb) REFERENCES PhieuKB(mapkb),
)
CREATE TABLE CTPhieuKB(
	id 			INT IDENTITY,
	mactpkb		NVARCHAR(4) PRIMARY KEY NOT NULL,
	mapkb		NVARCHAR(4) NOT NULL,
	mathuoc		NVARCHAR(4) NOT NULL,
	slthuoc		INT,
	mabenh		NVARCHAR(4) NOT NULL,
	FOREIGN KEY (mapkb) REFERENCES PhieuKB(mapkb),
	FOREIGN KEY (mathuoc) REFERENCES Thuoc(mathuoc),
	FOREIGN KEY (mabenh) REFERENCES Benh(mabenh),
)
 
--CONSTRAINT------------------------------------------------------------------------------
ALTER TABLE BenhNhan
ADD  CONSTRAINT cont1
CHECK(gioitinh IN (N'Nam',N'Nữ'))

ALTER TABLE Thuoc
ADD  CONSTRAINT Cont2
CHECK(donvi IN (N'Chai',N'Viên'))

ALTER TABLE PhieuKB
ADD  CONSTRAINT Cont3
FOREIGN KEY (mahd) REFERENCES HoaDon(mahd)
--ADD DATA--------------------------------------------------------------------------------
--Benh Nhan
INSERT INTO BenhNhan VALUES('BN01',N'Phạm Hoài Phương',	N'Nam',1997,N'Thủ Đức')
INSERT INTO BenhNhan VALUES('BN02',N'Trần Nam Bàng',	N'Nam',1997,N'Bình Thạnh')
INSERT INTO BenhNhan VALUES('BN03',N'Lê Vũ Trùng Dương',N'Nam',1997,N'Bình Tân')
--Thuoc
INSERT INTO Thuoc VALUES('Th01',N'',N'Uống',N'Viên',69000)
--Benh
INSERT INTO Benh VALUES('B01',N'Sốt')
INSERT INTO Benh VALUES('B02',N'Dị ứng')
INSERT INTO Benh VALUES('B03',N'Đau dạ dày')
INSERT INTO Benh VALUES('B04',N'Viêm họng')
--Phieu Kham Benh
INSERT INTO PhieuKB VALUES('PK01','BN04','B01','HD01', N'Buồn nôn','19/4/2018', 30000)
--CTPhieuKB
INSERT INTO CTPhieuKB VALUES('CT01','PK01','Th01',10, N'',N'',6000)
--HoaDon
INSERT INTO HoaDon VALUES('HD01','PK01',30000,0,30000)