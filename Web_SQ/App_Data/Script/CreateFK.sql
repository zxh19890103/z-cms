ALTER TABLE [dbo].[Nt_Permission_UserLevel_Mapping]
ADD CONSTRAINT [FK_Nt_Permission_UserLevel_Mapping_Nt_Permission]
FOREIGN KEY ([Permission_Id]) REFERENCES [dbo].[Nt_Permission]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[Nt_Permission_UserLevel_Mapping]
ADD CONSTRAINT [FK_Nt_Permission_UserLevel_Mapping_Nt_UserLevel]
FOREIGN KEY ([UserLevel_Id]) REFERENCES [dbo].[Nt_UserLevel]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[Nt_User]
ADD CONSTRAINT [FK_Nt_User_Nt_UserLevel]
FOREIGN KEY ([UserLevel_Id]) REFERENCES [dbo].[Nt_UserLevel]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[Nt_Resume]
ADD CONSTRAINT [FK_Nt_Resume_Nt_Job]
FOREIGN KEY ([Job_Id]) REFERENCES [dbo].[Nt_Job]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[Nt_Order]
ADD CONSTRAINT [FK_Nt_Order_Nt_Member]
FOREIGN KEY ([Member_Id]) REFERENCES [dbo].[Nt_Member]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[Nt_Message]
ADD CONSTRAINT [FK_Nt_Message_Nt_Member]
FOREIGN KEY ([Member_Id]) REFERENCES [dbo].[Nt_Member]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[Nt_MessageReply]
ADD CONSTRAINT [FK_Nt_MessageReply_Nt_Message]
FOREIGN KEY ([Message_Id]) REFERENCES [dbo].[Nt_Message]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[Nt_Member]
ADD CONSTRAINT [FK_Nt_Member_Nt_MemberRole]
FOREIGN KEY ([MemberRole_Id]) REFERENCES [dbo].[Nt_MemberRole]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[Nt_Banner]
ADD CONSTRAINT [FK_Nt_Banner_Nt_Language]
FOREIGN KEY ([Language_Id]) REFERENCES [dbo].[Nt_Language]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[Nt_Book]
ADD CONSTRAINT [FK_Nt_Book_Nt_Language]
FOREIGN KEY ([Language_Id]) REFERENCES [dbo].[Nt_Language]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[Nt_Content]
ADD CONSTRAINT [FK_Nt_Content_Nt_Language]
FOREIGN KEY ([Language_Id]) REFERENCES [dbo].[Nt_Language]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[Nt_Job]
ADD CONSTRAINT [FK_Nt_Job_Nt_Language]
FOREIGN KEY ([Language_Id]) REFERENCES [dbo].[Nt_Language]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[Nt_Link]
ADD CONSTRAINT [FK_Nt_Link_Nt_Language]
FOREIGN KEY ([Language_Id]) REFERENCES [dbo].[Nt_Language]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[Nt_Message]
ADD CONSTRAINT [FK_Nt_Message_Nt_Language]
FOREIGN KEY ([Language_Id]) REFERENCES [dbo].[Nt_Language]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[Nt_News]
ADD CONSTRAINT [FK_Nt_News_Nt_NewsCategory]
FOREIGN KEY ([NewsCategory_Id]) REFERENCES [dbo].[Nt_NewsCategory]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[Nt_NewsCategory]
ADD CONSTRAINT [FK_Nt_NewsCategory_Nt_Language]
FOREIGN KEY ([Language_Id]) REFERENCES [dbo].[Nt_Language]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[Nt_Product]
ADD CONSTRAINT [FK_Nt_Product_Nt_ProductCategory]
FOREIGN KEY ([ProductCategory_Id]) REFERENCES [dbo].[Nt_ProductCategory]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[Nt_ProductCategory]
ADD CONSTRAINT [FK_Nt_ProductCategory_Nt_Language]
FOREIGN KEY ([Language_Id]) REFERENCES [dbo].[Nt_Language]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[Nt_Navigation]
ADD CONSTRAINT [FK_Nt_Navigation_Nt_Language]
FOREIGN KEY ([Language_Id]) REFERENCES [dbo].[Nt_Language]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[Nt_SearchKeyWord]
ADD CONSTRAINT [FK_Nt_SearchKeyWord_Nt_Language]
FOREIGN KEY ([Language_Id]) REFERENCES [dbo].[Nt_Language]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[Nt_SinglePage]
ADD CONSTRAINT [FK_Nt_SinglePage_Nt_Language]
FOREIGN KEY ([Language_Id]) REFERENCES [dbo].[Nt_Language]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[Nt_SpecialPage]
ADD CONSTRAINT [FK_Nt_SpecialPage_Nt_Language]
FOREIGN KEY ([Language_Id]) REFERENCES [dbo].[Nt_Language]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[Nt_Email]
ADD CONSTRAINT [FK_Nt_Email_Nt_EmailAccount]
FOREIGN KEY ([EmailAccountId]) REFERENCES [dbo].[Nt_EmailAccount]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[Nt_Course]
ADD CONSTRAINT [FK_Nt_Course_Nt_CourseCategory]
FOREIGN KEY ([CourseCategory_Id]) REFERENCES [dbo].[Nt_CourseCategory]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[Nt_CourseCategory]
ADD CONSTRAINT [FK_Nt_CourseCategory_Nt_Language]
FOREIGN KEY ([Language_Id]) REFERENCES [dbo].[Nt_Language]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[Nt_ProductField]
ADD CONSTRAINT [FK_Nt_ProductField_Nt_ProductCategory]
FOREIGN KEY ([ProductCategory_Id]) REFERENCES [dbo].[Nt_ProductCategory]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[Nt_BookReply]
ADD CONSTRAINT [FK_Nt_BookReply_Nt_Book]
FOREIGN KEY ([Book_Id]) REFERENCES [dbo].[Nt_Book]([Id])
ON DELETE CASCADE ON UPDATE NO ACTION
GO