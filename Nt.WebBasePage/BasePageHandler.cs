using Nt.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Nt.Web
{
    public class BasePageHandler : Page
    {
        protected StringBuilder message;
        LogService _logger;
        public LogService Logger { get { return _logger; } }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            _logger = new LogService();
            message = new StringBuilder();
            Handle();
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
            if (message.Length == 0)
            {
                Response.Redirect(returnBUrl, true);
            }
            Response.Clear();
            Response.Write("<script type=\"text/javascript\">");
            Response.Write(string.Format("alert('{0}');window.location.href='{1}';",
                Server.HtmlEncode(message.ToString()), returnBUrl));
            Response.Write("</script>");
            Response.End();
        }


    }
}
