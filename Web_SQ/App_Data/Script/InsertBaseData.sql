SET IDENTITY_INSERT [dbo].[Nt_Language] ON
INSERT INTO  [dbo].[Nt_Language]([Id],[LanguageCode],[ResxPath],[Published],[DisplayOrder],[Name])
VALUES(1,'cn','',1,0,'简体中文')
SET IDENTITY_INSERT [dbo].[Nt_Language] OFF

SET IDENTITY_INSERT [dbo].[Nt_UserLevel] ON
INSERT INTO [dbo].[Nt_UserLevel]([Id],[Name],[SystemName],[Description],[Active])
VALUES(1,'超级管理员','Administrator','No Description',1)
INSERT INTO [dbo].[Nt_UserLevel]([Id],[Name],[SystemName],[Description],[Active])
VALUES(2,'客户管理员','CustomAdmin','No Description',1)
SET IDENTITY_INSERT [dbo].[Nt_UserLevel] OFF

SET IDENTITY_INSERT [dbo].[Nt_User] ON
INSERT INTO [dbo].[Nt_User]([Id],[UserName],[Password],[UserLevel_Id],[Description],[Active],[AddUser],[AddDate])
VALUES(1,'naite2014','a5d9a2e4773c4b4424de67c5decd0e29',1,'I Am A  super Admin',1,0,GETDATE())
INSERT INTO [dbo].[Nt_User]([Id],[UserName],[Password],[UserLevel_Id],[Description],[Active],[AddUser],[AddDate])
VALUES(2,'admin','21232f297a57a5a743894a0e4a801fc3',2,'I Am An Admin',1,1,GETDATE())
SET IDENTITY_INSERT [dbo].[Nt_User] OFF

SET IDENTITY_INSERT [dbo].[Nt_MemberRole] ON
INSERT INTO [dbo].[Nt_MemberRole]([Id],[Name],[SystemName],[Description],[Active])
VALUES(1,'已注册','Registered','Members Who Are Registered.',1)
SET IDENTITY_INSERT [dbo].[Nt_MemberRole] OFF

SET IDENTITY_INSERT [dbo].[Nt_Member] ON
INSERT INTO [dbo].[Nt_Member]([Id],[LoginName],[Password],[RealName],[Sex],[Company],[Address],[ZipCode],[MobilePhone],[Phone],[Fax],[Email],[MemberRole_Id],[Active],[Note],[LoginTimes],[LastLoginDate],[LastLoginIP],[AddDate])
VALUES(1,'michael','37fb5d842ec5bed2dc025affd81cf816','张星海',1,'大连奈特网络科技有限公司','大连市中山区鲁迅路安乐街锦联大厦1308','116600','189...','0411...','0411...','121...@qq.com',1,1,'',0,GETDATE(),'192.168.18.24',GETDATE())
SET IDENTITY_INSERT [dbo].[Nt_Member] OFF