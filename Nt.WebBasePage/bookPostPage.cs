using Nt.BLL;
using Nt.BLL.Mail;
using Nt.Framework;
using Nt.Model;
using Nt.Model.SettingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Nt.BLL.Extension;

namespace Nt.Web
{
    public class bookPostPage : BasePageHandler
    {

        BookSettings settings = null;//设置
        string emailBody = string.Empty;

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
                    _fieldNames.Add("Title", "标题");
                    _fieldNames.Add("Name", "联系人");
                    _fieldNames.Add("Mobile", "手机");
                    _fieldNames.Add("Company", "公司");
                    _fieldNames.Add("Email", "邮箱");
                    _fieldNames.Add("Fax", "传真");
                    _fieldNames.Add("Address", "住址");
                    _fieldNames.Add("Tel", "电话");
                    _fieldNames.Add("ZipCode", "邮编");
                    _fieldNames.Add("PoliticalRole", "政治身份");
                    _fieldNames.Add("PersonID", "身份证号");
                    _fieldNames.Add("Grade", "在校成绩");
                    _fieldNames.Add("Nation", "民族");
                    _fieldNames.Add("NativePlace", "祖籍");
                    _fieldNames.Add("GraduatedFrom", "学校");
                    _fieldNames.Add("Body", "内容");
                }
                return _fieldNames;
            }
        }


        protected override void Handle()
        {
            settings = SettingService.GetSettingModel<BookSettings>();
            Nt_Book book = new Nt_Book();
            book.InitDataFromPage();            
            book.Display = false;//默认为不显示
            book.AddDate = DateTime.Now;
            if (book.Language_Id == 0)
                book.Language_Id = NtConfig.CurrentLanguage;

            if (Validate(book))
            {
                PostMessage(book);
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
        void PostMessage(Nt_Book book)
        {
            try
            {
                BookService service = new BookService();

                if (settings.FiltrateSensitiveWords)
                {
                    book.Body = CommonUtility.FilterSensitiveWords(book.Body, settings.SensitiveWords);
                }

                if (settings.EnableSendEmail)
                {
                    MailSendService mailsender = new MailSendService();
                    mailsender.SendMail("客户留言:" + (string.IsNullOrEmpty(book.Title) ? "无标题" : book.Title),
                        emailBody,
                        settings.EmailAddressToReceiveEmail, settings.EmailToName);
                }

                service.Insert(book);
                AppendMessage("留言提交成功!");
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
                AppendMessage("留言提交失败");
            }
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        bool Validate(Nt_Book book)
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

            Type type = book.GetType();
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
                    p.SetValue(book, value, null);
                    if (settings.EnableSendEmail)
                    {
                        emailBody += string.Format("{0}:{1}<br/>", FieldNames[name], value);
                    }
                }
            }

            if (!NtUtility.IsValidEmail(book.Email))
            {
                AppendMessage("邮箱地址不正确!");
                return false;
            }

            if (settings.FiltrateUrl && CommonUtility.ContainsUrl(book.Body))
            {
                AppendMessage("内容不能含有网址!");
                return false;
            }

            return true;

        }
    }
}
