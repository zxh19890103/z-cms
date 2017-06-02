using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nt.Model;
using Nt.Framework;
using Nt.BLL.Extension;
using Nt.BLL;
using System.Text.RegularExpressions;

namespace Nt.Web
{
    public class addMemberPage : BasePageHandler
    {

        MemberService service;

        Dictionary<string, string> _fieldNames;
        /// <summary>
        /// 字段名
        /// </summary>
        Dictionary<string, string> FieldNames
        {
            get
            {
                if (_fieldNames == null)
                {
                    _fieldNames = new Dictionary<string, string>();
                    _fieldNames.Add("LoginName", "登录名");
                    _fieldNames.Add("Password", "密码");
                    _fieldNames.Add("RealName", "真实姓名");
                    _fieldNames.Add("Compnay", "公司");
                    _fieldNames.Add("Address", "住址");
                    _fieldNames.Add("ZipCode", "邮编");
                    _fieldNames.Add("MobilePhone", "手机");
                    _fieldNames.Add("Phone", "电话");
                    _fieldNames.Add("Fax", "传真");
                    _fieldNames.Add("Email", "邮箱");
                }
                return _fieldNames;
            }
        }

        protected override void Handle()
        {
            service = new MemberService();
            Nt_Member m = new Nt_Member();
            m.InitDataFromPage();

            m.Active = false;
            if (m.MemberRole_Id == 0)
                m.MemberRole_Id = 1;

            if (Validate(m))
            {
                PostMessage(m);
            }

            string redirectUrl = Request.QueryString["redirectUrl"];
            if (string.IsNullOrEmpty(redirectUrl) ||
                redirectUrl.ToLower().StartsWith("http://"))
            {
                redirectUrl = NtConfig.CurrentTemplatesPath;
            }
            Alert(redirectUrl);
        }

        /// <summary>
        /// 提交留言
        /// </summary>
        /// <param name="book"></param>
        void PostMessage(Nt_Member m)
        {
            try
            {
                m.Password = Nt.BLL.MD5.Md5Service.getMd5Hash(m.Password);
                service.Insert(m);
                AppendMessage("注册成功!");
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
                AppendMessage("注册失败!");
            }
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        bool Validate(Nt_Member m)
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
                return false;
            }

            Type type = m.GetType();
            foreach (var p in type.GetProperties())
            {
                string name = p.Name;
                if (Request.Form[name] != null && FieldNames.ContainsKey(name))
                {
                    if (Request.Form[name] == "")
                    {
                        AppendMessage(FieldNames[name] + "不能为空!");
                        return false;
                    }
                    string value = Server.HtmlEncode(Request.Form[name]);
                    p.SetValue(m, value, null);
                }
            }

            if (!Regex.IsMatch(m.LoginName, "^[a-zA-Z][a-zA-Z0-9]*$"))
            {
                AppendMessage("登陆名错误，以字母开头，4-20位字母或数字!");
                return false;
            }

            if (!m.Password.Equals(
                NtUtility.EnsureNotNull(Request["Password.Again"])))
            {
                AppendMessage("两次密码输入不一致!");
                return false;
            }

            if (m.Password.Length < 6)
            {
                AppendMessage("密码不应少于6个字符!");
                return false;
            }

            if (!NtUtility.IsValidEmail(m.Email))
            {
                AppendMessage("邮箱地址不正确!");
                return false;
            }

            if (service.LoginNameExisting(m.LoginName))
            {
                AppendMessage("该用户名已经存在!");
                return false;
            }

            return true;
        }


    }
}
