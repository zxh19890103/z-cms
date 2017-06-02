-- Creating table 'Nt_Banner'
CREATE TABLE [dbo].[Nt_Banner] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Language_Id] int  NOT NULL,
    [Picture_Id] int  NOT NULL,
    [Url] varchar(512)  NOT NULL,
    [Title] varchar(255)  NOT NULL,
    [Text] varchar(1024)  NOT NULL,
    [Display] bit  NOT NULL,
    [DisplayOrder] int  NOT NULL,
    [Note] varchar(1024)  NOT NULL
);
GO

-- Creating table 'Nt_Book'
CREATE TABLE [dbo].[Nt_Book] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Language_Id] int  NOT NULL,
    [Title] varchar(512)  NOT NULL,
    [Name] varchar(255)  NOT NULL,
    [Tel] varchar(125)  NOT NULL,
    [Gender] bit  NOT NULL,
    [NativePlace] varchar(255)  NOT NULL,
    [Nation] varchar(255)  NOT NULL,
    [PersonID] varchar(255)  NOT NULL,
    [EduDegree] varchar(255)  NOT NULL,
    [ZipCode] varchar(255)  NOT NULL,
    [PoliticalRole] varchar(255)  NULL,
    [Address] varchar(255)  NOT NULL,
    [GraduatedFrom] varchar(512)  NOT NULL,
    [Grade] varchar(255)  NOT NULL,
    [BirthDate] datetime  NOT NULL,
    [Mobile] varchar(125)  NOT NULL,
    [Email] varchar(255)  NOT NULL,
    [Company] varchar(255)  NOT NULL,
    [Body] varchar(max)  NOT NULL,
    [Display] bit  NOT NULL,
    [DisplayOrder] int  NOT NULL,
    [AddDate] datetime  NOT NULL,
    [Note] varchar(1024)  NOT NULL,
    [Type] int  NOT NULL,
	[Fax]  varchar(512) not null,
    [CustomerNumber] int not null default(0)
);
GO

-- Creating table 'Nt_Content'
CREATE TABLE [dbo].[Nt_Content] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Language_Id] int  NOT NULL,
    [Title] varchar(255)  NOT NULL,
    [Text] varchar(max)  NOT NULL,
    [Note] varchar(1024)  NOT NULL,
    [Display] bit  NOT NULL,
	[Picture_Id] int not null
);
GO

-- Creating table 'Nt_Email'
CREATE TABLE [dbo].[Nt_Email] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Priority] int  NOT NULL,
    [From] varchar(500)  NOT NULL,
    [FromName] varchar(500)  NOT NULL,
    [To] varchar(500)  NOT NULL,
    [ToName] varchar(500)  NOT NULL,
    [CC] varchar(500)  NOT NULL,
    [Bcc] varchar(500)  NOT NULL,
    [Subject] varchar(1000)  NOT NULL,
    [Body] varchar(max)  NOT NULL,
    [AddDate] datetime  NOT NULL,
    [SentTries] int  NOT NULL,
    [SentDate] datetime  NOT NULL,
    [EmailAccountId] int  NOT NULL
);
GO

-- Creating table 'Nt_EmailAccount'
CREATE TABLE [dbo].[Nt_EmailAccount] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Email] varchar(255)  NOT NULL,
    [DisplayName] varchar(255)  NOT NULL,
    [Host] varchar(255)  NOT NULL,
    [Port] int  NOT NULL,
    [UserName] varchar(255)  NOT NULL,
    [Password] varchar(255)  NOT NULL,
    [EnableSsl] bit  NOT NULL,
    [UseDefaultCredentials] bit  NOT NULL,
    [IsDefault] bit  NOT NULL
);
GO

-- Creating table 'Nt_JavaScript'
CREATE TABLE [dbo].[Nt_JavaScript] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Script] varchar(512)  NOT NULL,
    [Display] bit  NOT NULL,
    [DisplayOrder] int  NOT NULL,
    [Note] varchar(1024)  NOT NULL
);
GO

-- Creating table 'Nt_Job'
CREATE TABLE [dbo].[Nt_Job] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Language_Id] int  NOT NULL,
    [JobName] varchar(512)  NOT NULL,
    [RecruitCount] int  NOT NULL,
    [Salary] varchar(1024)  NOT NULL,
    [Duties] varchar(max)  NOT NULL,
    [Requirements] varchar(max)  NOT NULL,
    [Hr] varchar(255)  NOT NULL,
    [Phone] varchar(255)  NOT NULL,
    [Email] varchar(255)  NOT NULL,
    [ClickRate] int  NOT NULL,
    [WorkPlace] varchar(1024)  NOT NULL,
    [StartDate] datetime  NOT NULL,
    [EndDate] datetime  NOT NULL,
    [Display] bit  NOT NULL,
    [DisplayOrder] int  NOT NULL,
    [AddDate] datetime  NOT NULL,
    [Note] varchar(1024)  NOT NULL,
    [HtmlPath] varchar(255)  NOT NULL,
);
GO

-- Creating table 'Nt_Language'
CREATE TABLE [dbo].[Nt_Language] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LanguageCode] varchar(255)  NOT NULL,
    [ResxPath] varchar(255)  NOT NULL,
    [Published] bit  NOT NULL,
    [DisplayOrder] int  NOT NULL,
    [Name] varchar(255)  NOT NULL
);
GO

-- Creating table 'Nt_Link'
CREATE TABLE [dbo].[Nt_Link] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Language_Id] int  NOT NULL,
    [Url] varchar(255)  NOT NULL,
    [Picture_Id] int  NOT NULL,
    [Text] varchar(512)  NOT NULL,
    [ClickRate] int  NOT NULL,
    [Display] bit  NOT NULL,
    [DisplayOrder] int  NOT NULL,
    [AddDate] datetime  NOT NULL,
    [Note] varchar(1024)  NOT NULL
);
GO

-- Creating table 'Nt_Log'
CREATE TABLE [dbo].[Nt_Log] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserID] int  NOT NULL,
    [LoginIP] varchar(255)  NOT NULL,
    [Description] varchar(max)  NOT NULL,
    [AddDate] datetime  NOT NULL
);
GO

-- Creating table 'Nt_Member'
CREATE TABLE [dbo].[Nt_Member] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LoginName] varchar(255)  NOT NULL,
    [Password] varchar(255)  NOT NULL,
    [RealName] varchar(255)  NOT NULL,
    [Sex] bit  NOT NULL,
    [Company] varchar(512)  NOT NULL,
    [Address] varchar(512)  NOT NULL,
    [ZipCode] varchar(255)  NOT NULL,
    [MobilePhone] varchar(255)  NOT NULL,
    [Phone] varchar(255)  NOT NULL,
    [Fax] varchar(255)  NOT NULL,
    [Email] varchar(255)  NOT NULL,
    [Active] bit  NOT NULL,
    [Note] varchar(1024)  NOT NULL,
    [LoginTimes] int  NOT NULL,
    [LastLoginDate] datetime  NOT NULL,
    [LastLoginIP] varchar(255)  NOT NULL,
    [AddDate] datetime  NOT NULL,
    [MemberRole_Id] int  NOT NULL
);
GO

-- Creating table 'Nt_MemberRole'
CREATE TABLE [dbo].[Nt_MemberRole] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] varchar(1024)  NOT NULL,
    [Active] bit  NOT NULL,
    [Name] varchar(255)  NOT NULL,
    [SystemName] varchar(max)  NOT NULL
);
GO

-- Creating table 'Nt_Message'
CREATE TABLE [dbo].[Nt_Message] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Language_Id] int  NOT NULL,
    [Title] varchar(512)  NOT NULL,
    [Body] varchar(max)  NOT NULL,
    [LinkMan] varchar(255)  NOT NULL,
    [Status] int  NOT NULL,
    [SetTop] bit  NOT NULL,
    [Display] bit  NOT NULL,
    [DisplayOrder] int  NOT NULL,
    [AddDate] datetime  NOT NULL,
    [Note] varchar(1024)  NOT NULL,
    [Member_Id] int  NOT NULL,
    [Type] int  NOT NULL
);
GO

-- Creating table 'Nt_MessageReply'
CREATE TABLE [dbo].[Nt_MessageReply] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Body] varchar(max)  NOT NULL,
    [ReplyMan] varchar(255)  NOT NULL,
    [ReplyDate] datetime  NOT NULL,
    [Display] bit  NOT NULL,
    [DisplayOrder] int  NOT NULL,
    [AddDate] datetime  NOT NULL,
    [Note] varchar(1024)  NOT NULL,
    [Message_Id] int  NOT NULL
);
GO

-- Creating table 'Nt_Navigation'
CREATE TABLE [dbo].[Nt_Navigation] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(255)  NOT NULL,
    [Display] bit  NOT NULL,
    [DisplayOrder] int  NOT NULL,
    [Path] varchar(1024)  NOT NULL,
    [AnchorTarget] varchar(255)  NOT NULL,
    [HtmlPath] varchar(1024)  NOT NULL,
    [Parent] int  NOT NULL,
	[Depth] int Not null,
    [Crumbs] varchar(max)  NOT NULL,
    [Language_Id] int  NOT NULL,
	[MetaTitle] varchar(1024) not null,
    [MetaKeywords] varchar(1024)  NOT NULL,
    [MetaDescription] varchar(1024)  NOT NULL
);
GO

-- Creating table 'Nt_Navigation'
CREATE TABLE [dbo].[Nt_Mobile_Navigation] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(255)  NOT NULL,
    [Display] bit  NOT NULL,
    [DisplayOrder] int  NOT NULL,
    [Path] varchar(1024)  NOT NULL,
    [AnchorTarget] varchar(255)  NOT NULL,
    [Parent] int  NOT NULL,
	[Depth] int Not null,
    [Crumbs] varchar(max)  NOT NULL,
    [Language_Id] int  NOT NULL,
	[MetaTitle] varchar(1024) not null,
    [MetaKeywords] varchar(1024)  NOT NULL,
    [MetaDescription] varchar(1024)  NOT NULL
);
GO

-- Creating table 'Nt_News'
CREATE TABLE [dbo].[Nt_News] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] varchar(512)  NOT NULL,
    [Language_Id] int  NOT NULL,
    [Author] varchar(255)  NOT NULL,
    [Source] varchar(512)  NOT NULL,
    [Short] varchar(max)  NOT NULL,
    [Body] varchar(max)  NOT NULL,
    [ClickRate] int  NOT NULL,
    [SetTop] bit  NOT NULL,
    [Recommended] bit  NOT NULL,
    [HtmlPath] varchar(255)  NOT NULL,
    [FirstPicture] varchar(255)  NOT NULL,
    [MetaKeyWords] varchar(1024)  NOT NULL,
    [MetaDescription] varchar(1024)  NOT NULL,
    [EditDate] datetime  NOT NULL,
    [AddDate] datetime  NOT NULL,
    [Display] bit  NOT NULL,
    [DisplayOrder] int  NOT NULL,
    [NewsCategory_Id] int  NOT NULL,
);
GO

-- Creating table 'Nt_NewsCategory'
CREATE TABLE [dbo].[Nt_NewsCategory] (
    [Id] int IDENTITY(1,1) NOT NULL,
	[Depth] int  NOT NULL,
    [Parent] int  NOT NULL,
    [ClickRate] int  NOT NULL,
    [Crumbs] varchar(255)  NOT NULL,
    [Display] bit  NOT NULL,
    [DisplayOrder] int  NOT NULL,
    [Name] varchar(255)  NOT NULL,
    [Language_Id] int  NOT NULL
);
GO

-- Creating table 'Nt_Order'
CREATE TABLE [dbo].[Nt_Order] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [OrderCode] varchar(255)  NOT NULL,
    [Title] varchar(512)  NOT NULL,
    [LinkMan] varchar(255)  NOT NULL,
    [ReplyContent] varchar(max)  NOT NULL,
    [ReplyDate] datetime  NOT NULL,
    [Status] int  NOT NULL,
    [EditDate] datetime  NOT NULL,
    [AddDate] datetime  NOT NULL,
    [Note] varchar(1024)  NOT NULL,
    [Member_Id] int  NOT NULL
);
GO

-- Creating table 'Nt_Permission'
CREATE TABLE [dbo].[Nt_Permission] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Category] varchar(255)  NOT NULL,
    [Name] varchar(255)  NOT NULL,
    [SystemName] varchar(255)  NOT NULL,
    [CategoryName] varchar(max)  NOT NULL
);
GO

-- Creating table 'Nt_Permission_UserLevel_Mapping'
CREATE TABLE [dbo].[Nt_Permission_UserLevel_Mapping] (
    [Permission_Id] int  NOT NULL,
    [UserLevel_Id] int  NOT NULL
);
GO

-- Creating table 'Nt_Picture'
CREATE TABLE [dbo].[Nt_Picture] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SeoAlt] varchar(512)  NOT NULL,
    [PictureUrl] varchar(255)  NOT NULL,
    [Display] bit  NOT NULL,
    [DisplayOrder] int  NOT NULL,
    [Title] varchar(255)  NOT NULL,
    [Text] varchar(1024)  NOT NULL
);
GO

-- Creating table 'Nt_Product'
CREATE TABLE [dbo].[Nt_Product] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Language_Id] int  NOT NULL,
    [Title] varchar(512)  NOT NULL,
    [Short] varchar(max)  NOT NULL,
    [Body] varchar(max)  NOT NULL,
    [ClickRate] int  NOT NULL,
    [SetTop] bit  NOT NULL,
    [Recommended] bit  NOT NULL,
    [HtmlPath] varchar(255)  NOT NULL,
    [MetaKeyWords] varchar(1024)  NOT NULL,
    [MetaDescription] varchar(1024)  NOT NULL,
    [EditDate] datetime  NOT NULL,
    [AddDate] datetime  NOT NULL,
    [Display] bit  NOT NULL,
    [DisplayOrder] int  NOT NULL,
    [DownloadedRate] int  NOT NULL,
    [FileSize] bigint  NOT NULL,
    [FileUrl] varchar(255)  NOT NULL,
    [IsDownloadable] bit  NOT NULL,
    [ProductCategory_Id] int NOT NULL,
    [PictureIds] varchar(1024) NOT NULL,
	[ThumbnailUrl] varchar(512) not null,
	[ThumbnailID] int not null default(0),
);
GO

-- Creating table 'Nt_ProductCategory'
CREATE TABLE [dbo].[Nt_ProductCategory] (
    [Id] int IDENTITY(1,1) NOT NULL,
	[Depth] int  NOT NULL,
    [Parent] int  NOT NULL,
    [ClickRate] int  NOT NULL,
    [Crumbs] varchar(255)  NOT NULL,
    [Display] bit  NOT NULL,
    [DisplayOrder] int  NOT NULL,
    [Name] varchar(255)  NOT NULL,
    [IsDownloadable] bit  NOT NULL,
    [Language_Id] int  NOT NULL
);
GO

-- Creating table 'Nt_Resume'
CREATE TABLE [dbo].[Nt_Resume] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(255)  NOT NULL,
    [Gender] bit  NOT NULL,
    [Height] varchar(255)  NOT NULL,
    [BirthDay] datetime  NOT NULL,
    [HomeAddress] varchar(512)  NOT NULL,
    [MarritalStatus] int  NOT NULL,
    [GraduatedFrom] varchar(512)  NOT NULL,
    [GraduatedDate] datetime  NOT NULL,
    [Address] varchar(512)  NOT NULL,
    [MobilePhone] varchar(255)  NOT NULL,
    [Phone] varchar(255)  NOT NULL,
    [Email] varchar(255)  NOT NULL,
    [EduDegree] int  NOT NULL,
    [Work_History] varchar(max)  NOT NULL,
    [Salary] varchar(1024)  NOT NULL,
    [Major] varchar(1024)  NOT NULL,
    [Proffession] varchar(max)  NOT NULL,
    [ZipCode] varchar(255)  NOT NULL,
    [Status] int  NOT NULL,
    [ReplyContent] varchar(max)  NOT NULL,
    [ReplyDate] datetime  NOT NULL,
    [AttachedObject] varchar(255)  NOT NULL,
    [EditDate] datetime  NOT NULL,
    [AddDate] datetime  NOT NULL,
    [Note] varchar(1024)  NOT NULL,
    [Job_Id] int  NOT NULL
);
GO

-- Creating table 'Nt_SearchKeyWord'
CREATE TABLE [dbo].[Nt_SearchKeyWord] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Language_Id] int  NOT NULL,
    [KeyWord] varchar(512)  NOT NULL,
    [Display] bit  NOT NULL,
    [DisplayOrder] int  NOT NULL,
    [Note] varchar(1024)  NOT NULL
);
GO

-- Creating table 'Nt_SinglePage'
CREATE TABLE [dbo].[Nt_SinglePage] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Language_Id] int  NOT NULL,
    [Title] varchar(512)  NOT NULL,
    [Short] varchar(max)  NOT NULL,
    [Body] varchar(max)  NOT NULL,
    [HtmlPath] varchar(255)  NOT NULL,
    [FirstPicture] varchar(255)  NOT NULL,
    [MetaKeyWords] varchar(1024)  NOT NULL,
    [MetaDescription] varchar(1024)  NOT NULL,
    [Display] bit  NOT NULL
);
GO

-- Creating table 'Nt_SpecialPage'
CREATE TABLE [dbo].[Nt_SpecialPage] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Language_Id] int  NOT NULL,
    [Path] varchar(1024)  NOT NULL,
    [Display] bit  NOT NULL,
    [DisplayOrder] int  NOT NULL,
    [Note] varchar(1024)  NOT NULL
);
GO

-- Creating table 'Nt_User'
CREATE TABLE [dbo].[Nt_User] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserName] varchar(255)  NOT NULL,
    [Password] varchar(255)  NOT NULL,
    [Description] varchar(1024)  NOT NULL,
    [Active] bit  NOT NULL,
    [AddUser] int  NOT NULL,
    [AddDate] datetime  NOT NULL,
    [UserLevel_Id] int  NOT NULL
);
GO

-- Creating table 'Nt_UserLevel'
CREATE TABLE [dbo].[Nt_UserLevel] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] varchar(1024)  NOT NULL,
    [Active] bit  NOT NULL,
    [Name] varchar(255)  NOT NULL,
    [SystemName] varchar(max)  NOT NULL
);
GO

-- Creating table 'Nt_News'
CREATE TABLE [dbo].[Nt_Course] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] varchar(512)  NOT NULL,
    [Language_Id] int  NOT NULL,
    [Short] varchar(max)  NOT NULL,
    [Body] varchar(max)  NOT NULL,
    [ClickRate] int  NOT NULL,
    [SetTop] bit  NOT NULL,
    [Recommended] bit  NOT NULL,
    [HtmlPath] varchar(255)  NOT NULL,
    [MetaKeyWords] varchar(1024)  NOT NULL,
    [MetaDescription] varchar(1024)  NOT NULL,
    [EditDate] datetime  NOT NULL,
    [AddDate] datetime  NOT NULL,
    [Display] bit  NOT NULL,
    [DisplayOrder] int  NOT NULL,
    [CourseCategory_Id] int  NOT NULL,
	[CourseStartDate] datetime Not Null,
	[CourseDuration] varchar(1024) Not Null,
	[CourseTimeSpan] varchar(1024) Not Null,
	[CourseTeachers] varchar(1024) Not Null,
	[CourseBooks] varchar(1024) Not null,
	[CourseTarget] varchar(max) Not null
);
GO

-- Creating table 'Nt_NewsCategory'
CREATE TABLE [dbo].[Nt_CourseCategory] (
    [Id] int IDENTITY(1,1) NOT NULL,
	[Depth] int  NOT NULL,
    [Parent] int  NOT NULL,
    [ClickRate] int  NOT NULL,
    [Crumbs] varchar(255)  NOT NULL,
    [Display] bit  NOT NULL,
    [DisplayOrder] int  NOT NULL,
    [Name] varchar(255)  NOT NULL,
	[FitPeople] varchar(1024) Not NULL,
	[EduTeachers] varchar(1024) Not NULL,
	[CourseType] varchar(1024) Not NULL,
	[EduAim] varchar(1024) Not NULL,
    [Language_Id] int  NOT NULL
);
GO

--create table 'Nt_ProductField'
CREATE TABLE [dbo].[Nt_ProductField] (
	[Id] int identity(1,1) not null,
	[ProductCategory_Id] int NOT NULL,
	[Name] varchar(max) not null,
	[Display] bit not null,
	[DisplayOrder] int not null
);
GO

--create table 'Nt_ProductFieldValue'
CREATE TABLE [dbo].[Nt_ProductFieldValue] (
	[Id] int identity(1,1) not null,
	[Product_Id] int not null,
	[ProductField_Id] int not null,
	[Value] varchar(max) not null
);
GO

--create table 'Nt_BookReply'
CREATE TABLE [dbo].[Nt_BookReply] (
	[Id] int identity(1,1) not null,
	[ReplyContent] varchar(max) null,
    [Book_Id] int not null,
    [ReplyDate] datetime null,
    [ReplyMan] varchar(512) null,
	[Display] bit null,
	[DisplayOrder] int null
);
GO

