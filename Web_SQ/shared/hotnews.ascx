<%@ Control Language="C#" ClassName="hotnews" Inherits="Nt.Web.BaseUserControl" %>

<!--用于新闻列表页面-->
<div class="content_in_hot_news">
    <div class="news_hot_title">
        <p>最新消息</p>
        <a class="hot_news_per" id="but_up"><</a>
        <a class="hot_news_next" id="but_down">></a>
    </div>
    <div id="scrollDiv">
        <ul class="hot_news_list">
            <!--循环-->
            <%
                var data = Nt.DAL.CommonFactory2.GetList<View_News>("CategoryCrumbs like '%,1,%' ", "displayorder desc");
				
                
                foreach (var item in data)
                {
                    if (item.FirstPicture.ToString() == ""
                      && ConfigurationManager.AppSettings["news.first.picture.url.if.not.found"] != null)
                    {
                       item.FirstPicture = ConfigurationManager.AppSettings["news.first.picture.url.if.not.found"];
                    }
                    Response.Write("<li>");
                    Response.Write("	<div class=\"hot_news_content\">");
                    Response.Write("		<a href=\"/news/newsdetail.aspx?id="+item.Id+"\">");
                    Response.Write("			<img src=\""+item.FirstPicture+"\" width=\"194\" height=\"120\" />");
                    Response.Write("			<h2>"+item.Title+"</h2>");
                    Response.Write("			<span>"+CommonUtility.DateTimeFormat(item.AddDate,"yyyy-MM-dd")+"</span>");
                    Response.Write("			<p>");
                    Response.Write(item.Short.ToString().GetSubStr(120));
                    Response.Write("			</p>");
                    Response.Write("		</a>");
                    Response.Write("	</div>");
                    Response.Write("</li>");
                }              
                
            %>
            <!--循环结束-->
        </ul>
    </div>

</div>
