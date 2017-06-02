/*
migrate the data from old database to new database
{new} means the name of the new database
{old} means the name of the old database
*/
use master
go

delete from {new}.[dbo].[Nt_Language]
delete from {new}.[dbo].[Nt_banner]
delete from {new}.[dbo].[Nt_Product]
delete from {new}.[dbo].[Nt_News]
delete from {new}.[dbo].[Nt_Course]
delete from {new}.[dbo].[Nt_Job]
delete from {new}.[dbo].[Nt_SinglePage]
delete from {new}.[dbo].[Nt_JavaScript]
delete from {new}.[dbo].[Nt_Content]
delete from {new}.[dbo].[Nt_link]
delete from {new}.[dbo].[Nt_Picture]
delete from {new}.[dbo].[Nt_Navigation]
delete from {new}.[dbo].[Nt_Picture]

--language begin
SET IDENTITY_INSERT {new}.[dbo].[Nt_Language] ON

insert into {new}.[dbo].[Nt_Language]
(
[Id]
,[LanguageCode]
,[ResxPath]
,[Published]
,[DisplayOrder]
,[Name]
)
select 
[Id]
,[LanguageCode]
,[ResxPath]
,[Published]
,[DisplayOrder]
,[Name]
from {old}.dbo.[Nt_Language]

SET IDENTITY_INSERT {new}.[dbo].[Nt_Language] off
go
--language end


--banner begin
SET IDENTITY_INSERT {new}.[dbo].[Nt_banner] ON

insert into {new}.[dbo].[Nt_banner]
(
[Id]
,[Language_Id]
,[Picture_Id]
,[Url]
,[Title]
,[Text]
,[Display]
,[DisplayOrder]
,[Note]
)
select 
[Id]
,[Language_Id]
,[Picture_Id]
,[Url]
,[Title]
,[Text]
,[Display]
,[DisplayOrder]
,[Note]
from {old}.dbo.[Nt_banner]

SET IDENTITY_INSERT {new}.[dbo].[Nt_banner] off
go
--banner end


--content begin
SET IDENTITY_INSERT {new}.[dbo].[Nt_content] ON

insert into {new}.[dbo].[Nt_content]
(
[Id],
[Language_Id],
[Title],
[Text],
[Note],
[Display],
[Picture_Id]
)
select 
[Id],
[Language_Id],
[Title],
[Text],
[Note],
[Display],
0 as [Picture_Id]
from {old}.dbo.[Nt_content]

SET IDENTITY_INSERT {new}.[dbo].[Nt_content] off
go
--content end


--javascript begin
SET IDENTITY_INSERT {new}.[dbo].[Nt_javascript] ON

insert into {new}.[dbo].[Nt_javascript]
(
[Id]
,[Script]
,[Display]
,[DisplayOrder]
,[Note]
)
select 
[Id]
,[Script]
,[Display]
,[DisplayOrder]
,[Note]
from {old}.dbo.[Nt_javascript]

SET IDENTITY_INSERT {new}.[dbo].[Nt_javascript] off
go
--javascript end

--link begin
SET IDENTITY_INSERT {new}.[dbo].[Nt_link] ON

insert into {new}.[dbo].[Nt_link]
(
[Id]
,[Language_Id]
,[Url]
,[Picture_Id]
,[Text]
,[ClickRate]
,[Display]
,[DisplayOrder]
,[AddDate]
,[Note]
)
select 
[Id]
,[Language_Id]
,[Url]
,[Picture_Id]
,[Text]
,[ClickRate]
,[Display]
,[DisplayOrder]
,[AddDate]
,[Note]
from {old}.dbo.[Nt_link]

SET IDENTITY_INSERT {new}.[dbo].[Nt_link] off
go
--link end

--picture begin
SET IDENTITY_INSERT {new}.[dbo].[Nt_picture] ON

insert into {new}.[dbo].[Nt_picture]
(
[Id]
,[SeoAlt]
,[PictureUrl]
,[Display]
,[DisplayOrder]
,[Title]
,[Text]
)
select 
[Id]
,[SeoAlt]
,[PictureUrl]
,[Display]
,[DisplayOrder]
,[Title]
,[Text]
from {old}.dbo.[Nt_picture]

SET IDENTITY_INSERT {new}.[dbo].[Nt_picture] off
go
--picture end

--singlepage begin
SET IDENTITY_INSERT {new}.[dbo].[Nt_singlepage] ON

insert into {new}.[dbo].[Nt_singlepage]
(
[Id]
,[Language_Id]
,[Title]
,[Short]
,[Body]
,[HtmlPath]
,[FirstPicture]
,[MetaKeyWords]
,[MetaDescription]
,[Display]
)
select 
[Id]
,[Language_Id]
,[Title]
,[Short]
,[Body]
,[HtmlPath]
,[FirstPicture]
,[MetaKeyWords]
,[MetaDescription]
,[Display]
from {old}.dbo.[Nt_singlepage]

SET IDENTITY_INSERT {new}.[dbo].[Nt_singlepage] off
go
--singlepage end


--job begin
SET IDENTITY_INSERT {new}.[dbo].[Nt_job] ON

insert into {new}.[dbo].[Nt_job]
(
[Id]
,[Language_Id]
,[JobName]
,[RecruitCount]
,[Salary]
,[Duties]
,[Requirements]
,[Hr]
,[Phone]
,[Email]
,[ClickRate]
,[WorkPlace]
,[StartDate]
,[EndDate]
,[Display]
,[DisplayOrder]
,[AddDate]
,[Note]
,[HtmlPath]
)
select 
[Id]
,[Language_Id]
,[JobName]
,[RecruitCount]
,[Salary]
,[Duties]
,[Requirements]
,[Hr]
,[Phone]
,[Email]
,[ClickRate]
,[WorkPlace]
,[StartDate]
,[EndDate]
,[Display]
,[DisplayOrder]
,[AddDate]
,[Note]
,[HtmlPath]
from {old}.dbo.[Nt_job]

SET IDENTITY_INSERT {new}.[dbo].[Nt_job] off
go
--job end

--navigation begin
SET IDENTITY_INSERT {new}.[dbo].[Nt_Navigation] ON

insert into {new}.[dbo].[Nt_Navigation]
(
[Id]
,[Name]
,[Display]
,[DisplayOrder]
,[Path]
,[AnchorTarget]
,[HtmlPath]
,[Parent]
,[Depth]
,[Crumbs]
,[Language_Id]
,[MetaTitle]
,[MetaKeywords]
,[MetaDescription]
)
select 
[Id]
,[Name]
,[Display]
,[DisplayOrder]
,[Path]
,[AnchorTarget]
,[HtmlPath]
,[Parent]
,0 as [Depth]
,[Crumbs]
,[Language_Id]
,[Name] as [MetaTitle]
,[MetaKeywords]
,[MetaDescription]
from {old}.dbo.[Nt_Navigation]

SET IDENTITY_INSERT {new}.[dbo].[Nt_Navigation] off
go

declare @depth int,@id int;
declare mycursor cursor 
for 
select depth,id from {new}.[dbo].[Nt_Navigation]
open mycursor
fetch next from mycursor into @depth,@id
while @@FETCH_STATUS=0
begin
	update {new}.[dbo].[Nt_Navigation]
	set Depth=@depth+1 where Parent=@id;
	fetch from mycursor into @depth,@id
end
close mycursor
deallocate mycursor

--navigation end



--news category begin---
SET IDENTITY_INSERT {new}.[dbo].[Nt_NewsCategory] ON

insert into {new}.[dbo].[Nt_NewsCategory] (id,depth,parent,clickrate,crumbs,display,displayorder,name,language_id)
select id,0 as depth,parentcategoryid as parent,clickrate,crumbs,display,displayorder,name,language_id
from {old}.dbo.Nt_NewsCategory

SET IDENTITY_INSERT {new}.[dbo].[Nt_NewsCategory] off
go

declare @depth int,@id int;
declare mycursor cursor 
for 
select depth,id from {new}.[dbo].[Nt_NewsCategory]
open mycursor
fetch next from mycursor into @depth,@id
while @@FETCH_STATUS=0
begin
	update {new}.[dbo].[Nt_NewsCategory]
	set Depth=@depth+1 where Parent=@id;
	fetch from mycursor into @depth,@id
end
close mycursor
deallocate mycursor

---news category end--

--news begin--
SET IDENTITY_INSERT {new}.[dbo].[Nt_News] ON

insert into {new}.[dbo].[Nt_News] 
([Id]
  ,[Title]
  ,[Language_Id]
  ,[Author]
  ,[Source]
  ,[Short]
  ,[Body]
  ,[ClickRate]
  ,[SetTop]
  ,[Recommended]
  ,[HtmlPath]
  ,[FirstPicture]
  ,[MetaKeyWords]
  ,[MetaDescription]
  ,[EditDate]
  ,[AddDate]
  ,[Display]
  ,[DisplayOrder]
  ,[NewsCategory_Id]
)
select 
[Id]
,[Title]
,[Language_Id]
,[Author]
,[Source]
,[Short]
,[Body]
,[ClickRate]
,[SetTop]
,[Recommended]
,[HtmlPath]
,[FirstPicture]
,[MetaKeyWords]
,[MetaDescription]
,[EditDate]
,[AddDate]
,[Display]
,[DisplayOrder]
,[NewsCategory_Id]
from {old}.dbo.Nt_News

SET IDENTITY_INSERT {new}.[dbo].[Nt_News] off
go
--news end--

--product categroy
SET IDENTITY_INSERT {new}.[dbo].[Nt_ProductCategory] ON

insert into {new}.[dbo].[Nt_ProductCategory] (
id,depth,parent,clickrate,crumbs,display,displayorder,name,IsDownloadable,language_id)
select 
id,0 as depth,parentcategoryid as parent,clickrate,crumbs,display,displayorder,name,IsDownloadable,language_id
from {old}.dbo.Nt_ProductCategory

SET IDENTITY_INSERT {new}.[dbo].Nt_ProductCategory off
go

declare @depth int,@id int;
declare mycursor cursor 
for 
select depth,id from {new}.[dbo].Nt_ProductCategory
open mycursor
fetch next from mycursor into @depth,@id
while @@FETCH_STATUS=0
begin
	update {new}.[dbo].Nt_ProductCategory
	set Depth=@depth+1 where Parent=@id;
	fetch from mycursor into @depth,@id
end
close mycursor
deallocate mycursor

--product
SET IDENTITY_INSERT {new}.[dbo].[Nt_Product] ON

insert into {new}.[dbo].[Nt_Product] 
([Id]
,[Language_Id]
,[Title]
,[Short]
,[Body]
,[ClickRate]
,[SetTop]
,[Recommended]
,[HtmlPath]
,[MetaKeyWords]
,[MetaDescription]
,[EditDate]
,[AddDate]
,[Display]
,[DisplayOrder]
,[DownloadedRate]
,[FileSize]
,[FileUrl]
,[IsDownloadable]
,[ProductCategory_Id]
,[PictureIds]
,[ThumbnailUrl]
,[ThumbnailID]
)
select 
[Id]
,[Language_Id]
,[Title]
,[Short]
,[Body]
,[ClickRate]
,[SetTop]
,[Recommended]
,[HtmlPath]
,[MetaKeyWords]
,[MetaDescription]
,[EditDate]
,[AddDate]
,[Display]
,[DisplayOrder]
,[DownloadedRate]
,[FileSize]
,[FileUrl]
,[IsDownloadable]
,[ProductCategory_Id]
,[PictureIds]
,(select PictureUrl from {old}.dbo.Nt_Picture as t where t.Id=Thumbnail) as [ThumbnailUrl]
,[Thumbnail] as [ThumbnailID]
from {old}.dbo.Nt_Product

SET IDENTITY_INSERT {new}.[dbo].Nt_Product off
go

---product end---

--course category begin---
SET IDENTITY_INSERT {new}.[dbo].[Nt_courseCategory] ON

insert into {new}.[dbo].[Nt_courseCategory] 
(
[Id]
,[Depth]
,[Parent]
,[ClickRate]
,[Crumbs]
,[Display]
,[DisplayOrder]
,[Name]
,[FitPeople]
,[EduTeachers]
,[CourseType]
,[EduAim]
,[Language_Id]
)
select 
[Id]
,0 as [Depth]
,[ParentCategoryId] as parent
,[ClickRate]
,[Crumbs]
,[Display]
,[DisplayOrder]
,[Name]
,[FitPeople]
,[EduTeachers]
,[CourseIntro] as [CourseType]
,[EduAim]
,[Language_Id]
from {old}.dbo.[Nt_courseCategory]

SET IDENTITY_INSERT {new}.[dbo].[Nt_courseCategory] off
go

declare @depth int,@id int;
declare mycursor cursor 
for 
select depth,id from {new}.[dbo].[Nt_courseCategory]
open mycursor
fetch next from mycursor into @depth,@id
while @@FETCH_STATUS=0
begin
	update {new}.[dbo].[Nt_courseCategory]
	set Depth=@depth+1 where Parent=@id;
	fetch from mycursor into @depth,@id
end
close mycursor
deallocate mycursor

---course category end--

--course begin--
SET IDENTITY_INSERT {new}.[dbo].[Nt_course] ON

insert into {new}.[dbo].[Nt_course] 
([Id]
,[Title]
,[Language_Id]
,[Short]
,[Body]
,[ClickRate]
,[SetTop]
,[Recommended]
,[HtmlPath]
,[MetaKeyWords]
,[MetaDescription]
,[EditDate]
,[AddDate]
,[Display]
,[DisplayOrder]
,[CourseCategory_Id]
,[CourseStartDate]
,[CourseDuration]
,[CourseTimeSpan]
,[CourseTeachers]
,[CourseBooks]
,[CourseType]
)
select 
[Id]
,[Title]
,[Language_Id]
,[Short]
,[Body]
,[ClickRate]
,[SetTop]
,[Recommended]
,[HtmlPath]
,[MetaKeyWords]
,[MetaDescription]
,[EditDate]
,[AddDate]
,[Display]
,[DisplayOrder]
,[CourseCategory_Id]
,[CourseStartDate]
,[CourseDuration]
,[CourseTimeSpan]
,[CourseTeachers]
,[CourseBooks]
,[CourseTarget]
from {old}.dbo.[Nt_course]

SET IDENTITY_INSERT {new}.[dbo].[Nt_course] off
go
--course end--