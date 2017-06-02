using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Nt.Model.SettingModel;

namespace Nt.Web
{
    /// <summary>
    /// BaseUserControl 的摘要说明
    /// </summary>
    public class BaseUserControl : UserControl
    {
        WebsiteInfoSettings _websiteInfo;
        public WebsiteInfoSettings WebsiteInfo
        {
            get
            {
                BasePage page = Page as BasePage;
                if (page == null)
                {
                    _websiteInfo = new WebsiteInfoSettings();
                }
                else
                {
                    _websiteInfo = page.WebsiteInfo;
                }
                return _websiteInfo;
            }
        }

        public BasePage CurrentPage { get { return Page as BasePage; } }

        protected override void Render(HtmlTextWriter writer)
        {
            if (this.GetType().Name.ToLower().Contains("top")
                && !string.IsNullOrEmpty(NtConfig.MobileSiteUrl))
            {
                writer.Write("<script type=\"text/javascript\">");
                writer.Write("var mobileAgent = new Array(\"iphone\", \"ipod\", \"ipad\", \"android\", \"mobile\", \"blackberry\", \"webos\", \"incognito\", \"webmate\", \"bada\", \"nokia\", \"lg\", \"ucweb\", \"skyfire\");");
                writer.Write("var browser = navigator.userAgent.toLowerCase();");
                writer.Write("var isMobile = false;");
                writer.Write("for (var i = 0; i < mobileAgent.length; i++) {");
                writer.Write("if (browser.indexOf(mobileAgent[i]) != -1) {");
                writer.Write("isMobile = true;");
                writer.Write("window.location.href = '" + NtConfig.MobileSiteUrl + "';");
                writer.Write("break;}}");
                writer.Write("</script>");
            }
            base.Render(writer);
        }

    }
}