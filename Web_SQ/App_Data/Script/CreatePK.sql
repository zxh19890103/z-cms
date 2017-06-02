-- Creating primary key on [Id] in table 'Nt_Banner'
ALTER TABLE [dbo].[Nt_Banner]
ADD CONSTRAINT [PK_Nt_Banner]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Nt_Book'
ALTER TABLE [dbo].[Nt_Book]
ADD CONSTRAINT [PK_Nt_Book]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Nt_Content'
ALTER TABLE [dbo].[Nt_Content]
ADD CONSTRAINT [PK_Nt_Content]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Nt_Email'
ALTER TABLE [dbo].[Nt_Email]
ADD CONSTRAINT [PK_Nt_Email]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Nt_EmailAccount'
ALTER TABLE [dbo].[Nt_EmailAccount]
ADD CONSTRAINT [PK_Nt_EmailAccount]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Nt_JavaScript'
ALTER TABLE [dbo].[Nt_JavaScript]
ADD CONSTRAINT [PK_Nt_JavaScript]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Nt_Job'
ALTER TABLE [dbo].[Nt_Job]
ADD CONSTRAINT [PK_Nt_Job]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Nt_Language'
ALTER TABLE [dbo].[Nt_Language]
ADD CONSTRAINT [PK_Nt_Language]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Nt_Link'
ALTER TABLE [dbo].[Nt_Link]
ADD CONSTRAINT [PK_Nt_Link]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Nt_Log'
ALTER TABLE [dbo].[Nt_Log]
ADD CONSTRAINT [PK_Nt_Log]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Nt_Member'
ALTER TABLE [dbo].[Nt_Member]
ADD CONSTRAINT [PK_Nt_Member]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Nt_MemberRole'
ALTER TABLE [dbo].[Nt_MemberRole]
ADD CONSTRAINT [PK_Nt_MemberRole]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Nt_Message'
ALTER TABLE [dbo].[Nt_Message]
ADD CONSTRAINT [PK_Nt_Message]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Nt_MessageReply'
ALTER TABLE [dbo].[Nt_MessageReply]
ADD CONSTRAINT [PK_Nt_MessageReply]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Nt_Navigation'
ALTER TABLE [dbo].[Nt_Navigation]
ADD CONSTRAINT [PK_Nt_Navigation]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Nt_News'
ALTER TABLE [dbo].[Nt_News]
ADD CONSTRAINT [PK_Nt_News]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Nt_NewsCategory'
ALTER TABLE [dbo].[Nt_NewsCategory]
ADD CONSTRAINT [PK_Nt_NewsCategory]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Nt_Order'
ALTER TABLE [dbo].[Nt_Order]
ADD CONSTRAINT [PK_Nt_Order]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Nt_Permission'
ALTER TABLE [dbo].[Nt_Permission]
ADD CONSTRAINT [PK_Nt_Permission]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Permission_Id], [UserLevel_Id] in table 'Nt_Permission_UserLevel_Mapping'
ALTER TABLE [dbo].[Nt_Permission_UserLevel_Mapping]
ADD CONSTRAINT [PK_Nt_Permission_UserLevel_Mapping]
    PRIMARY KEY CLUSTERED ([Permission_Id], [UserLevel_Id] ASC);
GO

-- Creating primary key on [Id] in table 'Nt_Picture'
ALTER TABLE [dbo].[Nt_Picture]
ADD CONSTRAINT [PK_Nt_Picture]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Nt_Product'
ALTER TABLE [dbo].[Nt_Product]
ADD CONSTRAINT [PK_Nt_Product]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Nt_ProductCategory'
ALTER TABLE [dbo].[Nt_ProductCategory]
ADD CONSTRAINT [PK_Nt_ProductCategory]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Nt_Resume'
ALTER TABLE [dbo].[Nt_Resume]
ADD CONSTRAINT [PK_Nt_Resume]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Nt_SearchKeyWord'
ALTER TABLE [dbo].[Nt_SearchKeyWord]
ADD CONSTRAINT [PK_Nt_SearchKeyWord]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Nt_SinglePage'
ALTER TABLE [dbo].[Nt_SinglePage]
ADD CONSTRAINT [PK_Nt_SinglePage]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Nt_SpecialPage'
ALTER TABLE [dbo].[Nt_SpecialPage]
ADD CONSTRAINT [PK_Nt_SpecialPage]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Nt_User'
ALTER TABLE [dbo].[Nt_User]
ADD CONSTRAINT [PK_Nt_User]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Nt_UserLevel'
ALTER TABLE [dbo].[Nt_UserLevel]
ADD CONSTRAINT [PK_Nt_UserLevel]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

ALTER TABLE [dbo].[Nt_Course]
ADD CONSTRAINT [PK_Nt_Course]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Nt_CourseCategory'
ALTER TABLE [dbo].[Nt_CourseCategory]
ADD CONSTRAINT [PK_Nt_CourseCategory]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

ALTER TABLE [dbo].[Nt_ProductField]
ADD CONSTRAINT [PK_Nt_ProductField]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

ALTER TABLE [dbo].[Nt_ProductFieldValue]
ADD CONSTRAINT [PK_Nt_ProductFieldValue]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

ALTER TABLE [dbo].[Nt_BookReply]
ADD CONSTRAINT [PK_Nt_BookReply]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO