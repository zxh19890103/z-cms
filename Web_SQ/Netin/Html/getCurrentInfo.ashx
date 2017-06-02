<%@ WebHandler Language="C#" Class="getCurrentInfo" %>

using System;
using System.Web;

public class getCurrentInfo : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/html";
        context.Response.Write("<html>");
        context.Response.Write("<head>");
        context.Response.Write("<meta http-equiv=\"refresh\" content=\"3\">");
        context.Response.Write("<title>naite</title>");
        context.Response.Write("<head>");
        context.Response.Write("<body>");
        context.Response.Write(Nt.Web.Htmlizer.Instance.Message);
        context.Response.Write("</body>");
        context.Response.Write("</html>");
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}