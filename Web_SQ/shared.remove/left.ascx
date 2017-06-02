<%@ Control Language="C#" ClassName="left" Inherits="Nt.Web.BaseUserControl" %>

<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="Nt.DAL.Helper" %>
<%@ Import Namespace="System.Data" %>

<script runat="server">

    protected bool IsDropdownStyle = true;
    DataTable CategoryD = null;
    protected int Sid = 0;
    protected string EnglishTitle;
    protected string Title;

    void OutNewsLeft()
    {
        var page = Page as Nt.Web.IPageWithCategory;
        CategoryD = page.Categories;

        DataRow[] data = CategoryD.Select("[Parent]=" + Sid);

        foreach (DataRow r in data)
        {
            if (!IsDropdownStyle)
                Response.Write("<a href=\"?sortid=" + r["id"] + "\">");
            Response.Write("<div class=\"subNav\">" + r["Name"].ToString().GetSubStr(18) + "</div>");
            if (!IsDropdownStyle)
                Response.Write("</a>");
            if (IsDropdownStyle)
            {
                string sql = string.Format
            ("select id,title from view_news where NewsCategory_Id={0} order by displayorder desc,adddate desc", r["id"]);
                using (SqlDataReader rs = SqlHelper.ExecuteReader(
                    SqlHelper.GetConnection(), CommandType.Text, sql))
                {
                    int c = 0;
                    while (rs.Read())
                    {
                        if (c == 0)
                            Response.Write("<ul class=\"navContent\" >");
                        Response.Write("<li title=\"");
                        Response.Write(rs[1]);
                        Response.Write("\"><a href=\"newsdetail.aspx?id=");
                        Response.Write(rs[0]);
                        Response.Write("\"><span>+</span>");
                        Response.Write(rs[1].ToString().GetSubStr(16));
                        Response.Write("</a></li>");
                        c++;
                    }
                    if (c > 0)
                        Response.Write("</ul>");
                }
            }
        }
    }

    protected void OutAboutusLeft()
    {
        Response.Write("<a href=\"/about/page.aspx?id=1\"><div class=\"subNav\">公司介绍</div></a>");
        Response.Write("<a href=\"/about/page.aspx?id=2\"><div class=\"subNav\">组织架构</div></a>");
        Response.Write("<a href=\"/about/product.aspx\"><div class=\"subNav\">士奇团队</div></a>");
        Response.Write("<a href=\"/about/page.aspx?id=3\"><div class=\"subNav\">企业文化</div></a>");
        Response.Write("<a href=\"/about/page.aspx?id=4\"><div class=\"subNav\">投资者关系</div></a>");
        Response.Write("<a href=\"/about/page.aspx?id=5\"><div class=\"subNav \">联系我们</div></a>");
    }

    protected void OutResearchLeft()
    {
        Response.Write("<a href=\"/research/news.aspx?sortid=16\"><div class=\"subNav\">研究报告</div></a>");
        Response.Write("<a href=\"/research/product.aspx?sortid=2\"><div class=\"subNav\">研究院</div></a>");
        Response.Write("<a href=\"/research/news.aspx?sortid=16\"><div class=\"subNav\">政策法规</div></a>");
        Response.Write("<a href=\"/research/product.aspx?sortid=3\"><div class=\"subNav\">合作机构</div></a>");
    }
    
    void Begin()
    {
        Response.Write("<div class=\"nav_left fl\">");
        Response.Write("<div class=\"nav_show\">");
        Response.Write("	<h2>");
        Response.Write(Title);
        Response.Write("</h2>");
        Response.Write("	<p>");
        Response.Write(EnglishTitle);
        Response.Write("</p>");
        Response.Write("</div>");
        Response.Write("<div class=\"subNavBox\" id=\"subNavBox\">");
    }

    protected void RenderPartial()
    {
        switch (CurrentPage.ChannelNo)
        {
            case 2:
                Title = "关于士奇咨询";
                EnglishTitle = "About us";
                Begin();
                OutAboutusLeft();
                break;
            case 3:
                Title = "士奇新闻中心";
                EnglishTitle = "News Center";
                Sid = 1;
                Begin();
                OutNewsLeft();
                break;
            case 4:
                Title = "士奇管理咨询";
                EnglishTitle = "Management";
                Sid = 6;
                Begin();
                OutNewsLeft();
                break;
            case 5:
                Title = "士奇投资咨询";
                EnglishTitle = "Investment";
                Sid = 11;
                Begin();
                OutNewsLeft();
                break;
            case 6:
                Title = "士奇研究中心";
                EnglishTitle = "Research Center";
                Begin();
                OutResearchLeft();
                break;
            case 7:
                Title = "士奇咨询案例";
                EnglishTitle = "Cases";
                Sid = 18;
                Begin();
                OutNewsLeft();
                break;
            default:
                break;
        }

        Response.Write("<script>$('.subNav:eq(5)').css('border-bottom-color', '#fff');");
        Response.Write("</");
        Response.Write("script>");
        Response.Write("<img src=\"../images/img_line.png\" width=\"212\" height=\"4\" /></div></div>");
    }
</script>

<%RenderPartial(); %>
