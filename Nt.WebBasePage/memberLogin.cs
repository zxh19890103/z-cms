using Nt.DAL.Helper;
using Nt.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Web
{
    public class memberLogin : BasePageHandler
    {
        protected override void Handle()
        {
            bool isLogin = Request.QueryString["loginout"] == null;//是登录
            if (!isLogin)
            {
                Session.Contents.Remove(Nt.BLL.NtContext.SESSION_KEY_OF_MEMBER);
            }
            else
            {
                if (Nt.BLL.NtContext.Current.CurrentMember == null)
                {
                    var cookie = Session[ConstStrings.SESSION_KEY_2_SAVE_CHECKCODE];
                    if (cookie == null
                        || Request.Form["CheckCode"] == null
                        || !cookie.ToString().Equals(
                        Request.Form["CheckCode"].ToString(),
                        StringComparison.OrdinalIgnoreCase))
                    {
                        Session.Contents.Remove(ConstStrings.SESSION_KEY_2_SAVE_CHECKCODE);
                        AppendMessage("验证码错误!");
                    }
                    else
                    {
                        Nt.BLL.MemberService service = new BLL.MemberService();
                        string loginName = NtUtility.EnsureNotNull(Request.Form["LoginName"]);
                        string pass = NtUtility.EnsureNotNull(Request.Form["Password"]);
                        pass = Nt.BLL.MD5.Md5Service.getMd5Hash(pass);
                        int mid = 0;
                        if (service.Login(loginName, pass, out mid))
                        {
                            Session[Nt.BLL.NtContext.SESSION_KEY_OF_MEMBER] = mid;
                            var m = Nt.BLL.NtContext.Current.CurrentMember;
                            if (m != null)
                            {
                                SqlHelper.ExecuteNonQuery(
                                    "update nt_member set logintimes=logintimes+1,lastLogindate=getdate(),lastloginIp='" +
                                    Nt.BLL.Helper.WebHelper.GetIP() + "' where id=" + m.Id);
                            }
                        }
                    }
                }
            }

            string redirectUrl = Request.QueryString["redirectUrl"];
            if (string.IsNullOrEmpty(redirectUrl) ||
                redirectUrl.ToLower().StartsWith("http://"))
            {
                redirectUrl = NtConfig.CurrentTemplatesPath;
            }
            Alert(redirectUrl);
        }
    }
}
