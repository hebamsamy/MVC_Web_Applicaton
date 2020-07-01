/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2017 (14.0.2027)
    Source Database Engine Edition : Microsoft SQL Server Enterprise Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2017
    Target Database Engine Edition : Microsoft SQL Server Standard Edition
    Target Database Engine Type : Standalone SQL Server
*/

USE [dbMyOnlineShopping]
GO
/****** Object:  StoredProcedure [dbo].[GetBySearch]    Script Date: 4/3/2020 8:11:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetBySearch] 
	@search nvarchar(max)=null
AS
BEGIN
    select * from [dbo].[Products] p
	left join [dbo].[Categories] c on p.CategoryID = c.ID
	where 
	p.[Name] like CASE WHEN @search is not null then '%'+@search+'%' else p.[Name] end
	OR
	c.[Name] like CASE WHEN @search is not null then '%'+@search+'%' else c.[Name] end
END
