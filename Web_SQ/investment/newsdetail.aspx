<%@ Page Language="C#" Inherits="Nt.Web.NewsDetail" %>

<%@ Register Src="~/shared/foot.ascx" TagPrefix="uc1" TagName="foot" %>
<%@ Register Src="~/shared/top.ascx" TagPrefix="uc1" TagName="top" %>
<%@ Register Src="~/shared/left.ascx" TagPrefix="uc1" TagName="left" %>


<%
    ChannelNo = 5;
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
</head>


<body>

    <uc1:top runat="server" ID="top" />

    <div class="content">
        <div class="content_in">
            
            <uc1:left runat="server" ID="left" />

            <div class="content_right">
                <div class="position">
                    <a href="/">首 页</a>   > 
                    <%WriteCrumbs();%>
                </div>
                <a class="gotoback" href="javascript:window.history.go(-1);">返回</a>
           
		   <div class="Research_short">
            	<h1><%=Model.Title %></h1>
                <h2><%=Model.Title2 %></h2>
                <span></span>
                <p><%=Model.Title3 %></p>
                <strong><%=Model.Title4 %></strong>
               <!-- <img src="/images/team_img.png" width="306" height="83" />-->
			   <%=Model.NewsPicture %>
			   
            </div>

                <div id="editor_content">
                     <div class="content_in_target">
                 		<h2> <%=Model.Title %></h2>
                        <%=Model.Body %>
                    </div>
                    <div class="clear" style="width: 100%; height: 50px;"></div>
                    
                </div>
            </div>
        </div>
    </div>


    <uc1:foot runat="server" ID="foot" />


</body>
</html>

