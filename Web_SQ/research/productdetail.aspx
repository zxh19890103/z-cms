<%@ Page Language="C#" Inherits="Nt.Web.ProductDetail" %>

<%@ Register Src="~/shared/top.ascx" TagPrefix="uc1" TagName="top" %>
<%@ Register Src="~/shared/left.ascx" TagPrefix="uc1" TagName="left" %>
<%@ Register Src="~/shared/foot.ascx" TagPrefix="uc1" TagName="foot" %>



<%
    ChannelNo = 6;
	 TryGetModel();
	 CalculateNextAndPrevious();   
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
 <title><%=PageTitle %></title>
    <meta name="keywords" content="<%=Keywords %>" />
    <meta name="description" content="<%=Description %>" />    
   

<!-- tab -->
  <SCRIPT type=text/javascript>
    function selectTag(showContent,selfObj){
	// 操作标签
	var tag = document.getElementById("tags").getElementsByTagName("li");
	var taglength = tag.length;
	for(i=0; i<taglength; i++){
		tag[i].className = "";
	}
	selfObj.parentNode.className = "selectTag";
	// 操作内容
	for(i=0; j=document.getElementById("tagContent"+i); i++){
		j.style.display = "none";
	}
	document.getElementById(showContent).style.display = "block";
}
</SCRIPT>
<!-- tab end -->

 <!--#include file="../html.inc/head.htm"-->
</head>
<body>
 <uc1:top runat="server" ID="top" />
<div class="content">
	<div class="content_in">
    	<uc1:left runat="server" ID="left" />
        
        <div class="content_right">
        	<div class="position">
           		  <a href="/">首 页</a>   > <a href="/Research/">研究中心</a>   >   <%WriteCrumbs();%>
               </div>
            <a class="gotoback" href="javascript:window.history.go(-1);">返回</a>
            <div class="Research_Show_title">
            	<h1><%=Model.Title%></h1>
               
            </div>
           		 <div class="Research_Show_img">
     				
                      <div class="main_tag">
         
				<%  
                                if (ProductPictures != null && ProductPictures.Count > 0)
                                {
                                    Response.Write("<div id=\"tagContent\">");
                                    int k = 0;
                                    foreach (DataRowView item in ProductPictures)
                                    {
                            %>
		   <div class="tagContent<%if (k == 0)
                                                    { %> selectTag<%} %>" id="tagContent<%=k%>">
                                <img alt="<%=item["SeoAlt"] %>" src="<%=item["PictureUrl"] %>" title="<%=item["Title"] %>"
                                    width="467" height="337" />
			</div>
			   <%  k = k + 1;
                                   } %>
			</div>					   
										
			
           <% 
                                        Response.Write("<ul id=\"tags\">");
                                        k = 0;
                                        foreach (DataRowView item in ProductPictures)
                                        { %>
										
										<li class="selectTag"><a onclick="selectTag('tagContent<%=k%>',this)" href="javascript:void(0)">
                                <img alt="<%=item["SeoAlt"] %>" src="<%=item["PictureUrl"] %>" title="<%=item["Title"] %>"
                                    width="96" height="69" /></a> </li>
		   <%   k = k + 1;
                                   }
                                   Response.Write("</ul>");
                                   Response.Write("</div>");
                                    }
                            %>
        
                    
                 </div>
				 <script>
					$('#tags li img:odd').css('padding-right','0px');
				 
				 </script>
				 
                   <div class="clear"></div>
                   
                   
               	 <div id="editor_content">
                    	 <div class="Research_show_content_t">详细信息</div>
						<div class="clear" style="height:16px; width:100%;"></div>
                      <%=Model.Body%>
					  
    	
                	 </div>
                     
                     
<div class="clear" style="height:70px; width:100%;"></div>
          
          <div class="team_show_page">
          		<a href="?id=<%=PreID %>"> 上一篇：<%=PreTitle.GetSubStr(20)  %></a>丨
       			<a href="?id=<%=NextID %>">下一篇： <%=NextTitle.GetSubStr(20)  %></a>
          </div>  
   
            
        </div>
    </div>
</div>
 <uc1:foot runat="server" ID="foot" />
</body>
</html>
