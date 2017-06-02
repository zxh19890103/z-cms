using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using Nt.Model;
using Nt.BLL;
using Nt.BLL.Helper;
using System.Configuration;
using System.Web.Services;
using Nt.DAL.Helper;
using Nt.Model.SettingModel;

namespace Nt.Framework
{
    public class NtPage : Page
    {

        public const int IMPOSSIBLE_ID = 0;

        #region Service
        PermissionService _permissionService = null;
        LogService _logger = null;
        #endregion

        #region Public Properties

        public bool IsHttpPost { get { return Request.HttpMethod == "POST"; } }
        public string LocalUrl { get { return Request.Url.PathAndQuery; } }
        public string LocalPath { get { return Request.Url.AbsolutePath; } }

        private string _pageTitle = string.Empty;
        public string PageTitle
        {
            get
            {
                if (_pageTitle == string.Empty)
                    return ConfigurationManager.AppSettings["admin-base-title"];
                return _pageTitle + "-" + ConfigurationManager.AppSettings["admin-base-title"];
            }
            set { _pageTitle = value; }
        }

        public NtContext NtWorkingContext { get { return NtContext.Current; } }
        public Nt_Language WorkingLang { get { return NtContext.Current.CurrentLanguage; } }
        public int LanguageID { get { return NtContext.Current.LanguageID; } }
        public Nt_User WorkingUser { get { return NtContext.Current.CurrentUser; } }
        public int UserID { get { return NtContext.Current.UserID; } }
        public bool IsAdministrator { get { return NtContext.Current.IsAdministrator; } }

        public PermissionService PermissionService { get { return _permissionService; } }
        public LogService Logger { get { return _logger; } }

        /// <summary>
        /// 列表页缩略图大小(限高)
        /// </summary>
        public int ThumbnailSize { get { return Convert.ToInt32(ConfigurationManager.AppSettings["admin-thumbnailSize"]); } }


        public virtual PermissionRecord CurrentPermissionRecord
        {
            get { return null; }
        }

        WebsiteInfoSettings _websiteSettings;
        public WebsiteInfoSettings WebsiteSettings
        {
            get
            {
                if (_websiteSettings == null)
                    _websiteSettings = SettingService.GetSettingModel<WebsiteInfoSettings>();
                return _websiteSettings;
            }
        }

        #endregion

        #region Utility

        private StringBuilder _script = null;

        /// <summary>
        /// 页面重载
        /// </summary>
        public void ReLoad()
        {
            Response.Clear();
            Response.Redirect(LocalUrl, true);
        }

        /// <summary>
        /// 用js脚本重新载入当前页面
        /// </summary>
        /// <param name="msg">消息</param>
        public void ReLoadByScript(string message = "")
        {
            if (message != "")
                RegisterJavaScript(string.Format("ntAlert('{0}',function(){{window.location.href='" + LocalUrl + "';}});", message.Replace("'", "’")));
            else
                RegisterJavaScript("window.location.href='" + LocalUrl + "';");
        }

        /// <summary>
        ///向页面注册js脚本
        /// </summary>
        /// <param name="scriptBody"></param>
        public void RegisterJavaScript(string scriptBody)
        {
            _script.Append(scriptBody);
            _script.Append("\r\n");
        }

        /// <summary>
        /// 弹出提示信息后关闭窗口
        /// </summary>
        /// <param name="message">提示信息</param>
        public void CloseWindow(string message)
        {
            Response.Clear();
            Response.AddHeader("Content-Type", "text/html;charset=utf-8");
            Response.AddHeader("CacheControl", "Private");
            Response.Write(string.Format("<!DOCTYPE html><html><head><title>{0}</title>", message));
            Response.Write("<link href=\"/Netin/Content/Css/admin.common.css\" rel=\"stylesheet\" />");
            Response.Write("<link href=\"/Netin/Content/Css/fileuploader.css\" rel=\"stylesheet\" />");
            Response.Write("<script src=\"/Netin/Scripts/jquery-1.7.2.min.js\" type=\"text/javascript\"></script>");
            Response.Write("<script src=\"/Netin/Scripts/admin.common.js\" type=\"text/javascript\"></script>");
            Response.Write("<script src=\"/Netin/Scripts/admin.main.js\" type=\"text/javascript\"></script>");
            Response.Write("</head><body>");
            Response.Write(string.Format("<script type=\"text/javascript\">$(function(){{ntAlert('{0}',function(){{window.close();}});}})\r\n </script>",
                message.Replace("'", "’")));
            Response.Write("</body></html>");
            Response.End();
        }

        /// <summary>
        /// 弹窗
        /// </summary>
        /// <param name="message">消息</param>
        public void Alert(string message)
        {
            RegisterJavaScript(string.Format("ntAlert('{0}');", message.Replace("'", "’")));
        }

        /// <summary>
        /// 弹窗后回到历史
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="history">历史-1，-2，...</param>
        public void Alert(string message, int history)
        {
            if (history > 0)
            {
                Alert(message);
                return;
            }
            RegisterJavaScript(string.Format("ntAlert('{0}',function(){{this.window.history.go({1});}});", message.Replace("'", "’"), history));
        }

        /// <summary>
        /// 停止当前请求并弹出警告框最后跳转到指定页面
        /// </summary>
        /// <param name="url">如果不指定则返回历史</param>
        /// <param name="message">消息</param>
        public void Goto(string url, string message, int delay)
        {
            Response.Clear();
            Response.AddHeader("Content-Type", "text/html;charset=utf-8");
            Response.AddHeader("CacheControl", "Private");
            Response.Write(string.Format("<!DOCTYPE html><html><head><title>{0}</title>", message));
            Response.Write("<link href=\"/Netin/Content/Css/admin.common.css\" rel=\"stylesheet\" />");
            Response.Write("<link href=\"/Netin/Content/Css/fileuploader.css\" rel=\"stylesheet\" />");
            Response.Write("<script src=\"/Netin/Scripts/jquery-1.7.2.min.js\" type=\"text/javascript\"></script>");
            Response.Write("<script src=\"/Netin/Scripts/admin.common.js\" type=\"text/javascript\"></script>");
            Response.Write("<script src=\"/Netin/Scripts/admin.main.js\" type=\"text/javascript\"></script>");
            Response.Write("</head><body>");
            string redirect = string.Empty;
            if (string.IsNullOrEmpty(url))
                redirect = "if(this.mainFrame){this.mainFrame.history.go(-1);}else{this.window.history.go(-1);}\r\n";
            else
                redirect = "if(this.mainFrame){this.mainFrame.location.href='" + url + "';}else{this.window.location.href='" + url + "';}\r\n";
            string html = string.Format("<script type=\"text/javascript\">$(function(){{ntAlert('{0}',function(){{{1}}});\r\n}});</script>"
                , message.Replace("'", "’"), redirect);
            Response.Write(html);
            Response.Write("</body></html>");
            Response.End();
        }

        public void Goto(string url, string message)
        {
            string aurl = url;
            if (!url.StartsWith("/"))
                aurl = string.Format("/netin/{0}/{1}", CurrentPermissionRecord.Category, url);
            Goto(aurl, message, 3000);
        }


        #endregion

        #region override

        protected override void OnPreRenderComplete(EventArgs e)
        {
            if (Page.Master == null)
                return;
            if (_script.Length < 1)
                return;
            System.Web.UI.HtmlControls.HtmlGenericControl div = null;
            div = Page.Master.FindControl("ScriptArea") as System.Web.UI.HtmlControls.HtmlGenericControl;
            div.InnerHtml = string.Format("<script type=\"text/javascript\">$(function(){{{0}}});</script>", _script.ToString());
            base.OnPreRenderComplete(e);
        }

        protected override void OnPreInit(EventArgs e)
        {
            /*如果未登陆则跳转到login.aspx*/
            if (!NtWorkingContext.Logined())
            {
                Response.Clear();
                Response.AddHeader("Content-Type", "text/html;charset=utf-8");
                Response.AddHeader("CacheControl", "Private");
                Response.Write("<script type=\"text/javascript\">parent.location.href='/Netin/Login.aspx';</script>");
                Response.End();
            }
            _script = new StringBuilder();
            _permissionService = new PermissionService();
            if (CurrentPermissionRecord != null)
            {
                if (!_permissionService.Authorize(CurrentPermissionRecord))
                {
                    Response.Redirect("/Netin/NotAuthorized.aspx", true);
                }
            }
            _logger = new LogService();
            base.OnPreInit(e);
        }

        #endregion

        #region Web Service

        /// <summary>
        /// Logout
        /// </summary>
        [WebMethod]
        public static void Logout()
        {
            NtContext.Current.SessionEnd();
        }

        /// <summary>
        /// 切换语言版本
        /// </summary>
        /// <param name="languageID"></param>
        /// <returns></returns>
        [WebMethod]
        public static string SwitchLanguage(string languageID)
        {
            NtContext.Current.CookieLanguageID(Convert.ToInt32(languageID));
            NtContext.Current.ClearCache();//清除缓存
            return string.Empty;
        }

        [WebMethod]
        public static string SaveNote(string id, string note, string tableName)
        {
            note = NtUtility.SubStringWithoutHtml(note, 1024);
            string sql = string.Format("Update [{0}] Set [Note]='{1}' Where [Id]={2}", tableName, note.Replace("'", "''"), id);
            int i = SqlHelper.ExecuteNonQuery(sql);
            return new NtJson(
                new
                {
                    error = i == 0 ? 1 : 0,
                    message = i == 0 ? "没有更新任何数据" : "保存成功!"
                }).ToString();
        }

        [WebMethod]
        public static string SaveOrderReply(string id, string reply)
        {
            reply = NtUtility.RemoveHTML(reply);
            string sql = string.Format("Update [Nt_Order] Set [ReplyContent]='{0}',[ReplyDate]='{2}' Where [Id]={1}"
                , reply.Replace("'", "''"), id, DateTime.Now.ToString("yyyy-MM-dd"));
            int i = SqlHelper.ExecuteNonQuery(sql);
            return new NtJson(
                new
                {
                    error = i == 0 ? 1 : 0,
                    message = i == 0 ? "没有更新任何数据" : "订单回复保存成功!"
                }).ToString();
        }

        [WebMethod]
        public static string SaveResumeReply(string id, string reply)
        {
            reply = NtUtility.RemoveHTML(reply);
            string sql = string.Format("Update [Nt_Resume] Set [ReplyContent]='{0}',[ReplyDate]='{2}' Where [Id]={1}"
                , reply.Replace("'", "''"), id, DateTime.Now.ToString("yyyy-MM-dd"));
            int i = SqlHelper.ExecuteNonQuery(sql);
            return new NtJson(
                new
                {
                    error = i == 0 ? 1 : 0,
                    message = i == 0 ? "没有更新任何数据" : "简历回复保存成功!"
                }).ToString();
        }

        [WebMethod]
        public static string SetEnumValue(string tab, string field, string id, string value)
        {
            string sql = string.Format("Update [{0}] Set [{1}]={2} Where  [ID]={3}",
                tab, field, value, id);
            int i = SqlHelper.ExecuteNonQuery(sql);
            return new NtJson(
                new
                {
                    error = i == 0 ? 1 : 0,
                    message = i == 0 ? "没有更新任何数据" : "更新成功!"
                }).ToString();
        }

        #endregion

    }
}
