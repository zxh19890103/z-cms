<%@ Page Language="C#" Inherits="Nt.Web.SinglePage" %>

<%@ Register Src="~/shared/top.ascx" TagPrefix="uc1" TagName="top" %>
<%@ Register Src="~/shared/left.ascx" TagPrefix="uc1" TagName="left" %>
<%@ Register Src="~/shared/foot.ascx" TagPrefix="uc1" TagName="foot" %>

<%
    ChannelNo = 2;
    SetDefaultIDIfRequestFailed(1);
    TryGetModel();
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
                    <a href="#">首 页</a>   > <a href="#">公司介绍</a>   >   <%=Model.Title %>
                </div>
                <a class="gotoback" href="#">返回</a>
                <div id="editor_content">
                    <div class="content_in_company">
                        <%=Model.Body %>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <uc1:foot runat="server" ID="foot" />

</body>
</html>

