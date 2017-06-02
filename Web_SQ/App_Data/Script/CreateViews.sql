CREATE VIEW [dbo].[View_Banner]
AS
SELECT *,
(
	SELECT TOP 1 T1.[PictureUrl] 
	FROM [dbo].[Nt_Picture] AS T1
	WHERE T1.Id=T0.Picture_Id
)
AS [PictureUrl]
FROM [dbo].[Nt_Banner] AS T0
GO

CREATE VIEW [dbo].[View_Content]
AS
SELECT *,
(
	SELECT TOP 1 T1.[PictureUrl] 
	FROM [dbo].[Nt_Picture] AS T1
	WHERE T1.Id=T0.Picture_Id
)
AS [PictureUrl]
FROM [dbo].[Nt_Content] AS T0
GO

CREATE VIEW [dbo].[View_Link]
AS
SELECT *,
(
	SELECT TOP 1 T1.[PictureUrl] 
	FROM [dbo].[Nt_Picture] AS T1
	WHERE T1.Id=T0.Picture_Id
)
AS [PictureUrl]
FROM [dbo].[Nt_Link] AS T0
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

CREATE VIEW [dbo].[View_News]
AS
SELECT *,
(
select [Name] From [dbo].[Nt_NewsCategory] as T1
where T1.Id=T0.NewsCategory_Id
)
AS [CategoryName],
(
select [Crumbs] From [dbo].[Nt_NewsCategory] as T1
where T1.Id=T0.NewsCategory_Id
)
AS [CategoryCrumbs]
FROM [dbo].[Nt_News] AS T0
GO

CREATE VIEW [dbo].[View_Member]
AS
SELECT *,
(
select [Name] From [dbo].[Nt_MemberRole] as T1
where T1.Id=T0.MemberRole_Id
)
AS [MemberRoleName]
FROM [dbo].[Nt_Member] AS T0
GO

CREATE VIEW [dbo].[View_User]
AS
SELECT *,
(
select [Name] From [dbo].[Nt_UserLevel] as T1
where T1.Id=T0.UserLevel_Id
)
AS [UserLevelName]
FROM [dbo].[Nt_User] AS T0
GO

CREATE VIEW [dbo].[View_UserPermission]
AS
	SELECT
	T0.Permission_Id As Id,
	T0.UserLevel_Id ,
	T1.Category,
	T1.Name,
	T1.SystemName,
	T1.CategoryName
FROM [dbo].[Nt_Permission_UserLevel_Mapping] AS T0
INNER JOIN [dbo].[Nt_Permission] AS T1
ON T0.Permission_Id=T1.Id
GO

CREATE VIEW [dbo].[View_Course]
AS
SELECT * 
FROM [dbo].[Nt_Course] AS T0
left join 
(Select [Id] as CategoryID, [Crumbs] as CategoryCrumbs,[Name] as CategoryName,[FitPeople],[EduTeachers],[CourseType],[EduAim] 
From [dbo].[Nt_CourseCategory] )as t1
on t0.CourseCategory_Id=t1.CategoryID
GO

CREATE VIEW [dbo].[View_ProductField]
AS
SELECT * ,
(Select Crumbs From Nt_ProductCategory Where ID=T0.ProductCategory_Id) As ProductCategoryCrumbs
FROM [dbo].[Nt_ProductField] AS T0
GO