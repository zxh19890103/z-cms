<%@ WebHandler Language="C#" Class="BookPostHandler" %>

using System;
using System.Web;
using Nt.Model;
using Nt.Framework;
using Nt.BLL;
using Nt.BLL.Extension;
using Nt.BLL.Mail;
using Nt.Web;
using Nt.Model.SettingModel;
using System.Text.RegularExpressions;

public class BookPostHandler : BaseHandler
{
    BookSettings settings = null;//设置

    protected override void Handle()
    {
        settings = SettingService.GetSettingModel<BookSettings>();
        Nt_Book book = new Nt_Book();
        book.InitDataFromPage();

        HtmlEnCode(book);

        book.Type = 0;//默认分类为0，如果有需要分类，则需要在表单提供分类选择(<select></select>)
        book.Display = false;//默认为不显示
        book.AddDate = DateTime.Now;

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
                try
                {
                    string emailBody = GetEmailBody(book);
                    MailSendService mailsender = new MailSendService();
                    mailsender.SendMail("客户留言:" + book.Title,
                        emailBody,
                        settings.EmailAddressToReceiveEmail, "大连建发建筑设计院");
                }
                catch (Exception ex)
                {
                    Logger.Log(ex.Message);
                }
            }

            service.Insert(book);
            AppendMessage("留言提交成功!");
        }
        catch
        {
            AppendMessage("留言提交失败");
        }
    }

    /// <summary>
    /// 对所有字段进行Server.HtmlEnCode处理
    /// </summary>
    /// <param name="book"></param>
    void HtmlEnCode(Nt_Book book)
    {
        book.Title = Server.HtmlEncode(book.Title);
        book.Company = Server.HtmlEncode(book.Company);
        book.Tel = Server.HtmlEncode(book.Tel);
        book.Name = Server.HtmlEncode(book.Name);
        book.Mobile = Server.HtmlEncode(book.Mobile);
        book.Email = Server.HtmlEncode(book.Email);
        book.Body = Server.HtmlEncode(book.Body);

        //book.ZipCode = Server.HtmlEncode(book.ZipCode);
        //book.PoliticalRole = Server.HtmlEncode(book.PoliticalRole);
        //book.PersonID = Server.HtmlEncode(book.PersonID);
        //book.Grade = Server.HtmlEncode(book.Grade);
        //book.NativePlace = Server.HtmlEncode(book.NativePlace);
        //book.EduDegree = Server.HtmlEncode(book.EduDegree);
        //book.Grade = Server.HtmlEncode(book.Grade);
        //book.Fax = Server.HtmlEncode(book.Fax);
        //book.Nation = Server.HtmlEncode(book.Nation);
        //book.GraduatedFrom = Server.HtmlEncode(book.GraduatedFrom);
    }

    /// <summary>
    /// 获取邮件内容
    /// </summary>
    /// <param name="book"></param>
    /// <returns></returns>
    string GetEmailBody(Nt_Book book)
    {
        string emailBody = "";
        emailBody += "联系人:" + book.Name + "<br/>";
        emailBody += "手机:" + book.Mobile + "<br/>";
        emailBody += "公司:" + book.Company + "<br/>";
        emailBody += "邮箱:" + book.Email + "<br/>";
        emailBody += "电话:" + book.Tel + "<br/>";
        //emailBody += "传真:" + book.Fax + "<br/>";
        //emailBody += "邮编:" + book.ZipCode + "<br/>";
        //emailBody += "政治身份:" + book.PoliticalRole + "<br/>";
        //emailBody += "身份证号:" + book.PersonID + "<br/>";
        //emailBody += "在校成绩:" + book.Grade + "<br/>";
        //emailBody += "民族:" + book.Nation + "<br/>";
        //emailBody += "祖籍:" + book.NativePlace + "<br/>";
        //emailBody += "学校:" + book.GraduatedFrom + "<br/>";
        emailBody += "内容:" + book.Body;
        return emailBody;
    }

    /// <summary>
    /// 验证
    /// </summary>
    /// <param name="book"></param>
    /// <returns></returns>
    bool Validate(Nt_Book book)
    {
        bool flag = true;

        //var cookie = Request.Cookies[ConstStrings.COOKIES_KEY_2_SAVE_CHECKCODE];
        var cookie = System.Web.HttpContext.Current.Session[ConstStrings.COOKIES_KEY_2_SAVE_CHECKCODE];
        if (cookie == null
            || Request.Form["CheckCode"] == null
            || !cookie.ToString().Equals(
            Request.Form["CheckCode"].ToString(),
            StringComparison.OrdinalIgnoreCase))
        {
            AppendMessage("验证码错误!");
            flag = false;
        }

        if (book.Title == "")
        {
            AppendMessage("标题不能为空!");
            flag = false;
        }
        if (book.Body == "")
        {
            AppendMessage("内容不能为空!");
            flag = false;
        }

        if (!NtUtility.IsValidEmail(book.Email))
        {
            AppendMessage("邮箱地址不正确!");
            flag = false;
        }

        if (settings.FiltrateUrl && CommonUtility.ContainsUrl(book.Body))
        {
            AppendMessage("内容不能含有网址!");
            flag = false;
        }

        return flag;

    }

}