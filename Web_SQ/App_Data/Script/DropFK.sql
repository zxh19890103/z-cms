IF OBJECT_ID(N'[dbo].[FK_Nt_Permission_UserLevel_Mapping_Nt_Permission]','F') IS NOT NULL
	ALTER TABLE [dbo].[Nt_Permission_UserLevel_Mapping] DROP CONSTRAINT [FK_Nt_Permission_UserLevel_Mapping_Nt_Permission];
GO
IF OBJECT_ID(N'[dbo].[FK_Nt_Permission_UserLevel_Mapping_Nt_UserLevel]','F') IS NOT NULL
	ALTER TABLE [dbo].[Nt_Permission_UserLevel_Mapping] DROP CONSTRAINT [FK_Nt_Permission_UserLevel_Mapping_Nt_UserLevel];
GO
IF OBJECT_ID(N'[dbo].[FK_Nt_User_Nt_UserLevel]','F') IS NOT NULL
	ALTER TABLE [dbo].[Nt_User] DROP CONSTRAINT [FK_Nt_User_Nt_UserLevel];
GO
IF OBJECT_ID(N'[dbo].[FK_Nt_Resume_Nt_Job]','F') IS NOT NULL
	ALTER TABLE [dbo].[Nt_Resume] DROP CONSTRAINT [FK_Nt_Resume_Nt_Job];
GO
IF OBJECT_ID(N'[dbo].[FK_Nt_Order_Nt_Member]','F') IS NOT NULL
	ALTER TABLE [dbo].[Nt_Order] DROP CONSTRAINT [FK_Nt_Order_Nt_Member];
GO
IF OBJECT_ID(N'[dbo].[FK_Nt_Message_Nt_Member]','F') IS NOT NULL
	ALTER TABLE [dbo].[Nt_Message] DROP CONSTRAINT [FK_Nt_Message_Nt_Member];
GO
IF OBJECT_ID(N'[dbo].[FK_Nt_MessageReply_Nt_Message]','F') IS NOT NULL
	ALTER TABLE [dbo].[Nt_MessageReply] DROP CONSTRAINT [FK_Nt_MessageReply_Nt_Message];
GO
IF OBJECT_ID(N'[dbo].[FK_Nt_Member_Nt_MemberRole]','F') IS NOT NULL
	ALTER TABLE [dbo].[Nt_Member] DROP CONSTRAINT [FK_Nt_Member_Nt_MemberRole];
GO
IF OBJECT_ID(N'[dbo].[FK_Nt_Banner_Nt_Language]','F') IS NOT NULL
	ALTER TABLE [dbo].[Nt_Banner] DROP CONSTRAINT [FK_Nt_Banner_Nt_Language];
GO
IF OBJECT_ID(N'[dbo].[FK_Nt_Book_Nt_Language]','F') IS NOT NULL
	ALTER TABLE [dbo].[Nt_Book] DROP CONSTRAINT [FK_Nt_Book_Nt_Language];
GO
IF OBJECT_ID(N'[dbo].[FK_Nt_Content_Nt_Language]','F') IS NOT NULL
	ALTER TABLE [dbo].[Nt_Content] DROP CONSTRAINT [FK_Nt_Content_Nt_Language];
GO
IF OBJECT_ID(N'[dbo].[FK_Nt_Job_Nt_Language]','F') IS NOT NULL
	ALTER TABLE [dbo].[Nt_Job] DROP CONSTRAINT [FK_Nt_Job_Nt_Language];
GO
IF OBJECT_ID(N'[dbo].[FK_Nt_Link_Nt_Language]','F') IS NOT NULL
	ALTER TABLE [dbo].[Nt_Link] DROP CONSTRAINT [FK_Nt_Link_Nt_Language];
GO
IF OBJECT_ID(N'[dbo].[FK_Nt_Message_Nt_Language]','F') IS NOT NULL
	ALTER TABLE [dbo].[Nt_Message] DROP CONSTRAINT [FK_Nt_Message_Nt_Language];
GO
IF OBJECT_ID(N'[dbo].[FK_Nt_News_Nt_NewsCategory]','F') IS NOT NULL
	ALTER TABLE [dbo].[Nt_News] DROP CONSTRAINT [FK_Nt_News_Nt_NewsCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_Nt_NewsCategory_Nt_Language]','F') IS NOT NULL
	ALTER TABLE [dbo].[Nt_NewsCategory] DROP CONSTRAINT [FK_Nt_NewsCategory_Nt_Language];
GO
IF OBJECT_ID(N'[dbo].[FK_Nt_Product_Nt_ProductCategory]','F') IS NOT NULL
	ALTER TABLE [dbo].[Nt_Product] DROP CONSTRAINT [FK_Nt_Product_Nt_ProductCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_Nt_ProductCategory_Nt_Language]','F') IS NOT NULL
	ALTER TABLE [dbo].[Nt_ProductCategory] DROP CONSTRAINT [FK_Nt_ProductCategory_Nt_Language];
GO
IF OBJECT_ID(N'[dbo].[FK_Nt_SearchKeyWord_Nt_Language]','F') IS NOT NULL
	ALTER TABLE [dbo].[Nt_SearchKeyWord] DROP CONSTRAINT [FK_Nt_SearchKeyWord_Nt_Language];
GO
IF OBJECT_ID(N'[dbo].[FK_Nt_SinglePage_Nt_Language]','F') IS NOT NULL
	ALTER TABLE [dbo].[Nt_SinglePage] DROP CONSTRAINT [FK_Nt_SinglePage_Nt_Language];
GO
IF OBJECT_ID(N'[dbo].[FK_Nt_SpecialPage_Nt_Language]','F') IS NOT NULL
	ALTER TABLE [dbo].[Nt_SpecialPage] DROP CONSTRAINT [FK_Nt_SpecialPage_Nt_Language];
GO
IF OBJECT_ID(N'[dbo].[FK_Nt_Navigation_Nt_Language]','F') IS NOT NULL
	ALTER TABLE [dbo].[Nt_Navigation] DROP CONSTRAINT [FK_Nt_Navigation_Nt_Language];
GO
IF OBJECT_ID(N'[dbo].[FK_Nt_Email_Nt_EmailAccount]','F') IS NOT NULL
	ALTER TABLE [dbo].[Nt_Email] DROP CONSTRAINT [FK_Nt_Email_Nt_EmailAccount];
GO
IF OBJECT_ID(N'[dbo].[FK_Nt_Course_Nt_CourseCategory]','F') IS NOT NULL
	ALTER TABLE [dbo].[Nt_Course] DROP CONSTRAINT [FK_Nt_Course_Nt_CourseCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_Nt_Course_Nt_Language]','F') IS NOT NULL
	ALTER TABLE [dbo].[Nt_Course] DROP CONSTRAINT [FK_Nt_Course_Nt_Language];
GO
IF OBJECT_ID(N'[dbo].[FK_Nt_CourseCategory_Nt_Language]','F') IS NOT NULL
	ALTER TABLE [dbo].[Nt_CourseCategory] DROP CONSTRAINT [FK_Nt_CourseCategory_Nt_Language];
GO
IF OBJECT_ID(N'[dbo].[FK_Nt_ProductField_Nt_ProductCategory]','F') IS NOT NULL
	ALTER TABLE [dbo].[Nt_ProductField] DROP CONSTRAINT [FK_Nt_ProductField_Nt_ProductCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_Nt_BookReply_Nt_Book]','F') IS NOT NULL
	ALTER TABLE [dbo].[Nt_BookReply] DROP CONSTRAINT [FK_Nt_BookReply_Nt_Book];
GO