declare @parent int
declare @baseCrumbs varchar(255)
declare @currentID int
declare @currentDepth int;
declare @lastDepth int;
declare @len int,@res varchar(255);
declare @p1 int,@p2 int;

declare @name varchar(255),@displayorder int,@display bit;

set @parent=0;
set @baseCrumbs='0,'
set @lastDepth=0;

declare mycursor cursor for
select * from TempTable
open mycursor

fetch next from mycursor into @currentDepth,@name,@displayorder,@display
while(@@FETCH_STATUS=0)
begin
	if(@currentDepth>@lastDepth)
	begin
         set @lastDepth=@currentDepth;
         set @parent=@currentID
         set @baseCrumbs=(Select Crumbs From [Nt_NewsCategory] where id=@parent)
	end
	else if(@currentDepth<@lastDepth)
	begin
         set @lastDepth=@currentDepth
		 if(@currentDepth=0)
		 begin
			set @parent=0
			set @baseCrumbs='0,'
		 end
		 else
			begin
				set @p1=charindex(',',REVERSE(@baseCrumbs),2)
				set @p2=charindex(',',REVERSE(@baseCrumbs),@p1+1)
				set @res=SUBSTRING(@baseCrumbs,LEN(@baseCrumbs)-@p2+2,@p2-@p1-1)
				set @parent=cast(@res as int);
				set @baseCrumbs=substring(@baseCrumbs,0,LEN(@baseCrumbs)-@p1+2)    --'0,'
			end
	end
	insert into [Nt_NewsCategory] (name,parent,depth,crumbs,
clickrate,Display,DisplayOrder,Language_Id)values(@name,@parent,
@currentDepth,'',0,@display,@displayorder,{lang})
	set @currentID=(select @@IDENTITY)
	update [Nt_NewsCategory] 
	set Crumbs=@baseCrumbs+cast(@currentID as varchar)+',' where [Id]=@currentID
	fetch next from mycursor into @currentDepth,@name,@displayorder,@display
end
close mycursor 
deallocate mycursor