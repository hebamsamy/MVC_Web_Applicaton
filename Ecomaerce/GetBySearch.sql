CREATE PROCEDURE [dbo].[Procedure]
	@param1 int = 0,
	@param2 int
AS
	SELECT @param1, @param2
RETURN 0



GO
/****** Object:  StoredProcedure [dbo].[GetBySearch]    Script Date: 02/04/2020 03:47:08 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetBySearch]
	@search nvarchar(max)=null
AS
BEGIN
	SELECT *from [dbo].[Tbl_Product] P
	left join [dbo].[Tbl_Category] C on p.CategoryId=c.CategoryId
	where
	P.ProductName LIKE CASE WHEN @search is not null then  '%'+@search+'%' else P.ProductName end
	OR
	C.CategoryName LIKE CASE WHEN @search is not null then  '%'+@search+'%' else C.CategoryName end
END
