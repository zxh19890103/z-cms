alter table nt_product add ThumbnailID int not null default(0);
GO
drop view view_product;
GO
CREATE VIEW [dbo].[View_Product]
AS
SELECT *,
(
select [Name] From [dbo].[Nt_ProductCategory] as T1
where T1.Id=T0.ProductCategory_Id
)
AS [CategoryName],
(
select [Crumbs] From [dbo].[Nt_ProductCategory] as T1
where T1.Id=T0.ProductCategory_Id
)
AS [CategoryCrumbs]
FROM [dbo].[Nt_Product] AS T0
GO

update Nt_Permission set category=substring(category,1,len(category)-6) where category like '%Manage';