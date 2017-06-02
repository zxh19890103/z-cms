<%@ WebHandler Language="C#" Class="MessagePostHandler" %>


using System;
using System.Web;
using Nt.Model;
using Nt.Framework;
using Nt.BLL;
using Nt.BLL.Extension;
using Nt.BLL.Mail;
using Nt.Web;
using Nt.Model.SettingModel;

public class MessagePostHandler : BaseHandler
{
    MessageSettings settings = null;//设置

    protected override void Handle()
    {
        settings = SettingService.GetSettingModel<MessageSettings>();
        Nt_Message message = new Nt_Message();
        message.InitDataFromPage();

        if (Validate(message))
        {
            PostMessage(message);
        }
        string redirectUrl = NtConfig.CurrentTemplatesPath;
        if (string.IsNullOrEmpty(Request.QueryString["redirectUrl"]))
        {
            redirectUrl = Request.QueryString["redirectUrl"];
        }
        Alert(redirectUrl);
    }

    /// <summary>
    /// 提交留言
    /// </summary>
    /// <param name="message">message</param>
    void PostMessage(Nt_Message message)
    {
        try
        {

            MessageService service = new MessageService();

            if (settings.EnableSendEmail)
            {
                MailSendService mailsender = new MailSendService();
                mailsender.SendMail("游客留言:" + message.Title, message.Body, settings.EmailAddressToReceiveEmail, "michael");
            }

            if (settings.FiltrateSensitiveWords)
            {
                message.Body = CommonUtility.FilterSensitiveWords(message.Body, settings.SensitiveWords);
            }

            message.Language_Id = NtContext.Current.LanguageID;
            service.Insert(message);
            AppendMessage("留言提交成功!");
        }
        catch
        {
            AppendMessage("留言提交失败!");
        }
    }

    /// <summary>
    /// 验证
    /// </summary>
    /// <param name="message">message</param>
    /// <returns></returns>
    bool Validate(Nt_Message message)
    {
        bool flag = true;

        int memberID;//会员ID
        if (!Int32.TryParse(Request.QueryString["mId"], out memberID))
        {
            AppendMessage("参数错误!");
            flag = false;
        }

        MemberService memberService = new MemberService();
        if (memberService.GetRecordsCount(" it.Id=" + memberID) < 1)
        {
            AppendMessage("无会员信息!");
            flag = false;
        }
        
        if (settings.EnableCheckCode)
        {
            var cookie = Request.Cookies[ConstStrings.COOKIES_KEY_2_SAVE_CHECKCODE];
            if (cookie == null
                || Request.Form["CheckCode"] == null
                || !cookie.Value.Equals(Request.Form["CheckCode"].ToString(), StringComparison.OrdinalIgnoreCase))
            {
                AppendMessage("验证码错误!");
                flag = false;
            }
        }

        if (message.Title == "")
        {
            AppendMessage("标题不能为空!");
            flag = false;
        }

        if (message.Body == "")
        {
            AppendMessage("内容不能为空!");
            flag = false;
        }

        if (settings.FiltrateUrl && CommonUtility.ContainsUrl(message.Body))
        {
            AppendMessage("内容不能含有网址!");
            flag = false;
        }

        return flag;

    }

}