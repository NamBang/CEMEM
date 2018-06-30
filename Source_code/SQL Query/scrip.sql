CREATE DATABASE QLPM
USE QLPM
SET DATEFORMAT DMY

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
	DiaChi		NVARCHAR(100) NOT NULL
)
CREATE TABLE Thuoc(
	MaThuoc 			INT IDENTITY PRIMARY KEY NOT NULL,
	TenThuoc	NVARCHAR(50) NOT NULL,--30loai
	DonVi		NVARCHAR(4) NOT NULL,--2 loai
	DonGia		INT NOT NULL
)
CREATE TABLE PhieuKB(
	MaPKB 			INT IDENTITY  PRIMARY KEY NOT NULL,
	MaBN		INT NOT NULL,
	LoaiBenh	NVARCHAR(50) NOT NULL,
	TrieuChung	NVARCHAR(100) NOT NULL,--4cach
	NgayKham	DATE NOT NULL
)
CREATE TABLE CHITIETPKB(
	MaCTPKB 			INT IDENTITY  PRIMARY KEY NOT NULL,

	MaPKB		INT NOT NULL,
	MaThuoc		INT NOT NULL,
	SLThuoc		INT NOT NULL DEFAULT 1,
	CachDung	NVARCHAR(100) NOT NULL
	
)
CREATE TABLE HoaDon(
	MaHD 			INT IDENTITY PRIMARY KEY NOT NULL,

	MaPKB		INT NOT NULL,
	TienKham	FLOAT NOT NULL,
	TienThuoc	FLOAT NOT NULL,
	TongTien	FLOAT NOT NULL
	
)
CREATE TABLE QuyetDinh(
	id 			INT IDENTITY,
	SLBenhNhan	int NOT NULL,
	TienKham	int NOT NULL
	
)

--FOREIGN KEY
ALTER TABLE PhieuKB
ADD CONSTRAINT FK1
FOREIGN KEY (MaBN)
REFERENCES BenhNhan (MaBN) ON DELETE CASCADE;

ALTER TABLE CHITIETPKB
ADD CONSTRAINT FK2
FOREIGN KEY (MaPKB) REFERENCES PHIEUKB(MaPKB) ON DELETE CASCADE;

ALTER TABLE CHITIETPKB
ADD CONSTRAINT FK3
FOREIGN KEY (MaThuoc) REFERENCES THUOC(MaThuoc) ON DELETE CASCADE;

ALTER TABLE HoaDon
ADD CONSTRAINT FK4
FOREIGN KEY (MaPKB) REFERENCES PHIEUKB(MaPKB) ON DELETE CASCADE;
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
INSERT INTO PhieuKB VALUES('3',N'Tim mạch', N'Huyết áp tăng','2018/04/30')

--CTPhieuKB
INSERT INTO CHITIETPKB VALUES('1','1',10, N'Uống')
INSERT INTO CHITIETPKB VALUES('2','2',5, N'Uống')
INSERT INTO CHITIETPKB VALUES('3','2',5, N'Uống')


--HoaDon
INSERT INTO HoaDon VALUES('1',30000,0,30000)
INSERT INTO HoaDon VALUES('2',10500,0,15000)

--Tai Khoan
INSERT INTO TaiKhoan VALUES('1',N'Test','1','2')
INSERT INTO TaiKhoan VALUES(N'admin',N'CEEM',N'admin','1')
--QuyetDinh
INSERT INTO QuyetDinh VALUES('30','30000')

-----------------PROCEDURE------------------------------------
--Sales by Month
Create procedure [Sales by Month]
@month	int
AS
select	Row_number() over(order by HoaDon.TongTien) STT,
		Day(PhieuKB.NgayKham) as Ngay,
		(select COUNT(*) from PhieuKB,BenhNhan where PhieuKB.MABN = BenhNhan.MaBN and MONTH(PhieuKB.NgayKham) = @month) as SoBenhNhan,
		HoaDon.TongTien as DoanhThu,
		(SELECT CAST(HoaDon.TongTien AS float) / 
		CAST((select SUM(TongTien) from PhieuKB,HoaDon 
		where PhieuKB.MaPKB = HoaDon.MaPKB and MONTH(PhieuKB.NgayKham) = @month ) AS float)) as TiLe
from PhieuKB,HoaDon
where PhieuKB.MaPKB = HoaDon.MaPKB and Month(PhieuKB.NgayKham) = @month
--Sales by Month Drug
Create procedure [Sales by Month Drug]
@moth int
AS
select Row_number() over(order by Thuoc.MaThuoc) STT,
		Thuoc.TenThuoc TenThuoc,
		Thuoc.DonVi DonVi,
		B.SoLuong SoLuong,
		B.SLDung SLDung
		from (select C.MaThuoc,sum(C.SLThuoc) SoLuong,count(C.MaThuoc) SLDung from (select Thuoc.MaThuoc,CHITIETPKB.SLThuoc from(select * from PhieuKB
			where MONTH(PhieuKB.NgayKham) = @moth) A,CHITIETPKB,Thuoc
			where A.MaPKB = CHITIETPKB.MaPKB and CHITIETPKB.MaThuoc = Thuoc.MaThuoc)  C
			Group by C.MaThuoc) B, Thuoc
		where B.MaThuoc = Thuoc.MaThuoc
---Search by Keyword
Create procedure [Search by Keyword]
@keyword as varchar(max)
AS
SELECT * 
FROM BenhNhan 
WHERE dbo.fChuyenCoDauThanhKhongDau(HoVaTen) LIKE  N'%'+@keyword+'%'
---[Search by Keyword Drug]
Create procedure [Search by Keyword Drug]
@keyword as varchar(max)
AS
SELECT * 
FROM Thuoc 
WHERE dbo.fChuyenCoDauThanhKhongDau(TenThuoc) LIKE  N'%'+@keyword+'%'
---Search by Keyword Account
Create procedure [Search by Keyword Account]
@keyword as varchar(max)
AS
SELECT * 
FROM TaiKhoan 
WHERE dbo.fChuyenCoDauThanhKhongDau(tenhienthi) LIKE  N'%'+@keyword+'%'
---Delete BenhNhan
Create procedure [Delete BenhNhan]
@key as int
AS
DELETE 
     FROM   BenhNhan
     WHERE  
     BenhNhan.MaBN = @key
---Delete Thuoc
Create procedure [Delete Thuoc]
@key as int
AS
DELETE 
     FROM   Thuoc
     WHERE  
     Thuoc.MaThuoc = @key
--Delete TaiKhoan
Create procedure [Delete TaiKhoan]
@key as varchar(max)
AS
DELETE 
     FROM   TaiKhoan
     WHERE  
     TaiKhoan.username = @key
--fChuyenCoDauThanhKhongDau
CREATE FUNCTION [dbo].[fChuyenCoDauThanhKhongDau](@inputVar NVARCHAR(MAX) )
    RETURNS NVARCHAR(MAX)
    AS
    BEGIN    
        IF (@inputVar IS NULL OR @inputVar = '')  RETURN ''
       
        DECLARE @RT NVARCHAR(MAX)
        DECLARE @SIGN_CHARS NCHAR(256)
        DECLARE @UNSIGN_CHARS NCHAR (256)
     
        SET @SIGN_CHARS = N'ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệếìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵýĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ' + NCHAR(272) + NCHAR(208)
        SET @UNSIGN_CHARS = N'aadeoouaaaaaaaaaaaaaaaeeeeeeeeeeiiiiiooooooooooooooouuuuuuuuuuyyyyyAADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOUUUUUUUUUUYYYYYDD'
     
        DECLARE @COUNTER int
        DECLARE @COUNTER1 int
       
        SET @COUNTER = 1
        WHILE (@COUNTER <= LEN(@inputVar))
        BEGIN  
            SET @COUNTER1 = 1
            WHILE (@COUNTER1 <= LEN(@SIGN_CHARS) + 1)
            BEGIN
                IF UNICODE(SUBSTRING(@SIGN_CHARS, @COUNTER1,1)) = UNICODE(SUBSTRING(@inputVar,@COUNTER ,1))
                BEGIN          
                    IF @COUNTER = 1
                        SET @inputVar = SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@inputVar, @COUNTER+1,LEN(@inputVar)-1)      
                    ELSE
                        SET @inputVar = SUBSTRING(@inputVar, 1, @COUNTER-1) +SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@inputVar, @COUNTER+1,LEN(@inputVar)- @COUNTER)
                    BREAK
                END
                SET @COUNTER1 = @COUNTER1 +1
            END
            SET @COUNTER = @COUNTER +1
        END
        RETURN @inputVar
    END
--Search LoaiTK
Create procedure [Search LoaiTK]
@key as varchar(max)
AS
select TYPE LoaiTK,tenhienthi TenHienThi from TaiKhoan
where TaiKhoan.username = @key
--Search by keyword Bill
Create procedure [Search by keyword Bill]
@key as int
as
select BenhNhan.HoVaTen,NgayKham from BenhNhan,PhieuKB
where PhieuKB.MaBN = BenhNhan.MaBN and BenhNhan.MaBN = @key
--Search by keyword Bill
create procedure [Get Build]
@date as date, @maBN as int
as
select  Row_number() over(order by Thuoc.MaThuoc) STT,Thuoc.TenThuoc,Thuoc.DonGia,SLThuoc,(Thuoc.DonGia * B.SLThuoc) as Tien,B.MaPKB from (select CHITIETPKB.MaThuoc,CHITIETPKB.SLThuoc,CHITIETPKB.MaPKB from (select MaPKB from PhieuKB
where Day(PhieuKB.NgayKham) = Day(@date) and MONTH(PhieuKB.NgayKham) = MONTH(@date) and YEAR(PhieuKB.NgayKham) = YEAR(@date) and PhieuKB.MaBN = @maBN) A,CHITIETPKB
where A.MaPKB = CHITIETPKB.MaPKB) B,Thuoc
where B.MaThuoc = Thuoc.MaThuoc
--Sale by Bill
create procedure [Sale by Bill]
@key as int
as
select Row_number() over(order by PhieuKB.NgayKham) STT,NgayKham,TienKham,TienThuoc,TongTien from PhieuKB,HoaDon
where PhieuKB.MaBN = @key and PhieuKB.MaPKB = HoaDon.MaPKB
--Select QuyDinh
create procedure [Select QuyDinh]
as
select QuyetDinh.TienKham,QuyetDinh.SLBenhNhan from QuyetDinh
--Sales by TenThuoc
Create procedure [Sales by TenThuoc]
as
select TenThuoc,MaThuoc from Thuoc
--Final add PhieuKham
Create procedure [Final add PhieuKham]
as
SELECT MAX(MaPKB) AS MaPKB
FROM PhieuKB
--Search Danh Sach CTPhieuKham by MaPKB
Create procedure [Search Danh Sach CTPhieuKham by MaPKB]
@key as int
as
select TenThuoc,SLThuoc,CachDung,MaCTPKB from (select MaThuoc, SLThuoc, CachDung,MaCTPKB from PhieuKB,CHITIETPKB 
where PhieuKB.MaPKB = CHITIETPKB.MaPKB and PhieuKB.MaPKB = @key) A, Thuoc
where A.MaThuoc = Thuoc.MaThuoc
---Delete ChiTietPhieuKham
Create procedure [Delete ChiTietPhieuKham]
@key as int
AS
DELETE 
     FROM   CHITIETPKB
     WHERE  
     CHITIETPKB.MaCTPKB = @key
--Final add CTPhieuKham
Create procedure [Final add CTPhieuKham]
as
SELECT MAX(MaCTPKB) AS MaCTPKB
FROM CHITIETPKB

Create procedure [Sales TenBenhNhan]
@key as int
as
Select HoVaTen from BenhNhan
where BenhNhan.MaBN = @key