<%@ Control Language="C#" ClassName="left" Inherits="Nt.Web.BaseUserControl" %>

<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="Nt.DAL.Helper" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.IO" %>

<script runat="server">

    protected int Sid = 0;
    protected string EnglishTitle;
    protected string Title;
    protected bool ListNewsItem;//if show the list of news items while drop down
    StreamWriter sw;

    void WriteHtml(string s)
    {
        Response.Write(s);
        sw.Write(s);
    }

    void WriteHtml(object obj)
    {
        Response.Write(obj);
        sw.Write(obj);
    }

    /// <summary>
    /// 层级栏目输出
    /// </summary>
    void OutNewsLeft()
    {
        using (var conn = SqlHelper.GetConnection())
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandTimeout = 60;
            cmd.CommandText = string.Format(
                "select id,parent,depth,[name] from Nt_NewsCategory where crumbs like '%,{0},%' and id<>{0} order by displayorder;\r\n select id,title as name,crumbs,newscategory_id from nt_news left join (select id as cid,[name] as cname,crumbs from nt_newscategory) as t0 on  nt_news.newscategory_id=t0.cid  where crumbs like '%,{0},%' and id<>{0} order by displayorder desc,adddate desc", Sid);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet data = new DataSet();
            adp.Fill(data);

            foreach (DataRow r in data.Tables[0].Select("depth=1"))
            {

                DataRow[] subContent;
                if (ListNewsItem)
                    subContent = data.Tables[1].Select("newscategory_id=" + r["id"]);
                else
                    subContent = data.Tables[0].Select("[parent]=" + r["id"]);

                //contains content
                if (subContent.Length > 0)
                {
                    WriteHtml("<div class=\"subNav");
                    WriteHtml("\">");
                    WriteHtml(r["Name"].ToString().GetSubStr(18));
                    WriteHtml("</div>");

                    WriteHtml("<ul class=\"navContent\" >");
                    foreach (DataRow r0 in subContent)
                    {
                        WriteHtml("<li title=\"");
                        WriteHtml(r0["name"]);
                        if (ListNewsItem)
                            WriteHtml("\"><a href=\"newsdetail.aspx?id=");
                        else
                            WriteHtml("\"><a href=\"news.aspx?sortid=");
                        WriteHtml(r0["id"]);
                        WriteHtml("&pid=");
                        WriteHtml(r["id"]);
                        WriteHtml("\"><span>+</span>");
                        WriteHtml(r0["name"].ToString().GetSubStr(16));
                        WriteHtml("</a></li>");
                    }

                    WriteHtml("</ul>");
                }
                else
                {
                    //no content
                    if (!ListNewsItem)
                    {
                        WriteHtml("<a href=\"news.aspx?sortid=");
                        WriteHtml(r["id"]);
                        WriteHtml("\">");
                    }

                    WriteHtml("<div class=\"subNav");
                    WriteHtml("\">");
                    WriteHtml(r["Name"].ToString().GetSubStr(18));
                    WriteHtml("</div>");

                    if (!ListNewsItem)
                        WriteHtml("</a>");

                }
            }

            data.Dispose();
            adp.Dispose();
            cmd.Dispose();
            conn.Close();
        }
    }

    /// <summary>
    /// 开始
    /// </summary>
    void Begin()
    {
        WriteHtml("<div class=\"nav_left fl\">");
        WriteHtml("<div class=\"nav_show\">");
        WriteHtml("	<h2>");
        WriteHtml(Title);
        WriteHtml("</h2>");
        WriteHtml("	<p>");
        WriteHtml(EnglishTitle);
        WriteHtml("</p>");
        WriteHtml("</div>");
        WriteHtml("<div class=\"subNavBox\" id=\"subNavBox\">");
    }

    /// <summary>
    /// 结束
    /// </summary>
    void End()
    {
        WriteHtml("<script>$('.subNav:eq(5)').css('border-bottom-color', '#fff');");
        WriteHtml("</");
        WriteHtml("script>");
        WriteHtml("<img src=\"../images/img_line.png\" width=\"212\" height=\"4\" /></div></div>");
    }

    protected void RenderPartial(string path)
    {
        sw = new System.IO.StreamWriter(Server.MapPath(path), false, System.Text.Encoding.UTF8);

        switch (CurrentPage.ChannelNo)
        {
            case 2:
                Title = "关于士奇咨询";
                EnglishTitle = "About us";
                Begin();
                WriteHtml("<a href=\"/about/page.aspx?id=1\"><div class=\"subNav\">公司介绍</div></a>");
                WriteHtml("<a href=\"/about/page.aspx?id=2\"><div class=\"subNav\">组织架构</div></a>");
                WriteHtml("<a href=\"/about/product.aspx\"><div class=\"subNav\">士奇团队</div></a>");
                WriteHtml("<a href=\"/about/page.aspx?id=3\"><div class=\"subNav\">企业文化</div></a>");
                WriteHtml("<a href=\"/about/page.aspx?id=4\"><div class=\"subNav\">投资者关系</div></a>");
                WriteHtml("<a href=\"/about/page.aspx?id=5\"><div class=\"subNav \">联系我们</div></a>");
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
                ListNewsItem = true;
                Begin();
                OutNewsLeft();
                break;
            case 5:
                Title = "士奇投资咨询";
                EnglishTitle = "Investment";
                Sid = 11;
                ListNewsItem = true;
                Begin();
                OutNewsLeft();
                break;
            case 6:
                Title = "士奇研究中心";
                EnglishTitle = "Research Center";
                Sid = 15;
                Begin();
                WriteHtml("<div class=\"subNav\">研究团队</div>");
                WriteHtml("<ul class=\"navContent\" >");
                WriteHtml("<li><a href=\"/research/page.aspx?Id=6\"><span>+</span>研究院介绍 </a> </li>");
                WriteHtml("<li><a href=\"/research/product.aspx\"><span>+</span>研究团队 </a> </li>");
                WriteHtml("</ul>");
                OutNewsLeft();
                WriteHtml("<a href=\"/research/hezuojigou.aspx\"><div class=\"subNav\">合作机构</div></a>");
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

        End();
        sw.Flush();
        sw.Close();
        sw.Dispose();
    }
</script>

<%
    string p = string.Format("/html.inc/left-cno={0}.html", CurrentPage.ChannelNo);
    if (File.Exists(Server.MapPath(p)))
        CurrentPage.Include(p);
    else
        RenderPartial(p);
%>