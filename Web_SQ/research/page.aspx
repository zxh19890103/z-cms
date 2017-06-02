<%@ Page Language="C#" Inherits="Nt.Web.SinglePage" %>

<%@ Register Src="~/shared/top.ascx" TagPrefix="uc1" TagName="top" %>
<%@ Register Src="~/shared/left.ascx" TagPrefix="uc1" TagName="left" %>
<%@ Register Src="~/shared/foot.ascx" TagPrefix="uc1" TagName="foot" %>

<%
UseNavigationAsSeo = true;
    ChannelNo = 6;
	NtID =6;
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
                    <a href="/">首 页</a>   > <a href="/research/">研究中心</a>   >  <a href="/research/product.aspx?sortid=2">研究团队</a>   > <%=Model.Title %>
                </div>
                <a class="gotoback" href="javascript:history.go(-1);">返回</a>
				<div class="Research_short">
<h1>研究院介绍 </h1>
<h2>Our Institute</h2>
<span></span>
<p>奉行着严谨认真，每一个结论都经过不断的验证</p>
<strong>A rigorous, each conclusion through continuous validation</strong>
       </div>  
				
				
				
                <div id="editor_content">
                        <%=Model.Body %>
                </div>
            </div>
        </div>
    </div>

    <uc1:foot runat="server" ID="foot" />

</body>
</html>

