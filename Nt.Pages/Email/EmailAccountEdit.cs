using Nt.BLL;
using Nt.BLL.Mail;
using Nt.DAL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services;

namespace Nt.Pages.Email
{
    public class EmailAccountEdit : NtPageForEdit<Nt_EmailAccount>
    {

        protected override void InitRequiredData()
        {
            ListUrl = "EmailAccount.aspx";
            base.InitRequiredData();
        }

        [WebMethod]
        public static string ChangeEmailAccountPassword(string id, string password)
        {
            EmailAccountService service = new EmailAccountService();
            int i = service.ChangeEmailAccountPassword(id, password);
            return new NtJson(
                  new
                  {
                      error = i == 0 ? 1 : 0,
                      message = i == 0 ? "密码未更改" : "密码更改成功!"
                  }).ToString();
        }

        [WebMethod]
        public static string TrySendMail(string mailAddress, string accountId)
        {
            bool flag = true;
            string message = "Error";
            try
            {
                MailSendService service = new MailSendService();
                EmailAccountService accountService = new EmailAccountService();
                service.EmailAccount = CommonFactory.GetById<Nt_EmailAccount>(Convert.ToInt32(accountId));
                service.SendMail("测试邮件", "大连奈特有限公司测试邮件", mailAddress, "ceshi");
            }
            catch (Exception ex)
            {
                flag = false;
                message = ex.Message;
            }
            return new NtJson(
                  new
                  {
                      error = flag ? 0 : 1,
                      message = flag ? "邮件已发送，请查看！" : message
                  }).ToString();
        }

        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.EmailAccountEdit;
            }
        }
    }
}
