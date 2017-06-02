<%@ Page Language="C#" Inherits="Nt.Web.ProductDetail" %>

<%@ Register Src="~/shared/top.ascx" TagPrefix="uc1" TagName="top" %>
<%@ Register Src="~/shared/left.ascx" TagPrefix="uc1" TagName="left" %>
<%@ Register Src="~/shared/foot.ascx" TagPrefix="uc1" TagName="foot" %>



<%
    ChannelNo = 2;
    SortID = 1;
    TryGetModel();
	 CalculateNextAndPrevious();
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title><%=PageTitle %></title>
    <meta name="keywords" content="<%=Keywords %>" />
    <meta name="description" content="<%=Description %>" />    
    <!--#include file="../html.inc/head.htm"-->


<body>
 <uc1:top runat="server" ID="top" />

<div class="content">
	<div class="content_in">
		<uc1:left runat="server" ID="left" />
        <div class="content_right">
        	<div class="position">
           		<a href="/">首 页</a>   > <a href="/about/">关于士奇咨询</a>   >  <%WriteCrumbs();%>
               </div>          
            <a class="gotoback" href="javascript:window.history.go(-1);" >返回</a>
            
           		 <div class="team_Show">
                 	<div class="team_Show_img">
                        <img src="<%=Model.ThumbnailUrl%>" width="242" height="265" />
                     	<p> <%=Model.ManNameTeam%></p>
                       <span>职务：<%=Model.Poster%></span>
                    </div>	
                 </div>
                 
                	 
                   	 <div id="editor_content">
                    	 <div class="team_show_content_t">详细介绍</div>
           				     <%=Model.Body%>
    	
                	 </div>
                     
                     
<div class="clear" style="height:70px; width:100%;"></div>
          
          <div class="team_show_page">
          		<a href="?id=<%=PreID %>"> 上一篇：<%=PreTitle.GetSubStr(20) %></a>丨
       			<a href="?id=<%=NextID %>">下一篇： <%=NextTitle.GetSubStr(20) %></a>
          </div>  
            
        </div>
    </div>
</div>
<uc1:foot runat="server" ID="foot" />
</body>
</html>
