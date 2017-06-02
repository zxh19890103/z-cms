using Nt.BLL;
using Nt.BLL.MD5;
using Nt.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Nt.Pages
{
    public class Login : Page
    {
        #region Utitliy

        void Alert(string message)
        {
            if (string.IsNullOrEmpty(message))
                return;
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "nt.login.alert",
                        "alert(\"" + message + "\");",
                        true
                         );
        }

        #endregion


        protected override void OnLoad(EventArgs e)
        {
            if (NtContext.Current.Logined())
            {
                Response.Redirect("Default.aspx", true);
            }

            if (IsPostBack || Request.HttpMethod == "POST")
            {
                var checkCode = Request.Form["CheckCode"];
                if (Request.Cookies[ConstStrings.COOKIES_KEY_2_SAVE_CHECKCODE] == null)
                {
                    Alert("未知错误!");
                    return;
                }
                if (checkCode.ToUpper()
                    != Request.Cookies[ConstStrings.COOKIES_KEY_2_SAVE_CHECKCODE].Value)
                {
                    Alert("验证码错误！");
                    Request.Cookies.Remove(ConstStrings.COOKIES_KEY_2_SAVE_CHECKCODE);
                    return;
                }

                var userName = Request.Form["UserName"].Trim();
                var password = Request.Form["Password"].Trim();

                var service = new UserService();
                var md5Pass = Md5Service.getMd5Hash(password);
                int userID = 0;
                if (service.Login(userName, md5Pass, out userID))
                {
                    NtContext.Current.SessionUser(userID);
                    Response.Redirect("Default.aspx", true);
                }
                else
                {
                    Alert("登录失败");
                }
                base.OnLoad(e);
            }
        }
    }
}
