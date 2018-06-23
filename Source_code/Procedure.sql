USE QLPM4
GO
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

Create procedure [Search by Keyword]
@keyword as varchar(max)
AS
SELECT * 
FROM BenhNhan 
WHERE dbo.fChuyenCoDauThanhKhongDau(HoVaTen) LIKE  N'%'+@keyword+'%';

[Search by Keyword] 'Nguyen Van Trua'

select  dbo.fChuyenCoDauThanhKhongDau(HoVaTen) from BenhNhan
Create procedure [Delete BenhNhan]
@key as int
AS
DELETE 
     FROM   BenhNhan
     WHERE  
     BenhNhan.MaBN = @key

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
        -- SET @inputVar = replace(@inputVar,' ','-')
        RETURN @inputVar
    END

