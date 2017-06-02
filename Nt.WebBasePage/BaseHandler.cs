using Nt.BLL;
using Nt.BLL.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Nt.Web
{
    /// <summary>
    /// BaseHandler 的摘要说明
    /// </summary>
    public class BaseHandler : IHttpHandler
    {
        HttpRequest _request;
        HttpResponse _response;
        HttpServerUtility _server;
        LogService _logger;
        public HttpRequest Request { get { return _request; } }
        public HttpResponse Response { get { return _response; } }
        public HttpServerUtility Server { get { return _server; } }
        public LogService Logger { get { return _logger; } }

        protected StringBuilder message;

        public void ProcessRequest(HttpContext context)
        {
            _request = context.Request;
            _response = context.Response;
            _server = context.Server;
            _logger = new LogService();
            message = new StringBuilder();
            Handle();
        }


        public string MapPath(string virtualPath)
        {
            return WebHelper.MapPath(virtualPath);
        }

        protected virtual void Handle()
        {

        }

        protected void AppendMessage(string msg)
        {
            message.Append(msg + @"\n");
        }


        protected void Alert()
        {
            Response.Clear();
            Response.Write("<script type=\"text/javascript\" language='javascript'>");
            Response.Write(string.Format("alert('{0}');", Server.HtmlEncode(message.ToString())));
            Response.Write("</script>");
            Response.End();
        }

        protected void Alert(string returnBUrl)
        {
            Response.Clear();
            Response.Write("<script type=\"text/javascript\">");
            Response.Write(string.Format("alert('{0}');window.location.href='{1}';",
                Server.HtmlEncode(message.ToString()), returnBUrl));
            Response.Write("</script>");
            Response.End();
        }

        protected void Debug(string msg)
        {
            Response.Clear();
            Response.Write(msg);
            Response.End();
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}