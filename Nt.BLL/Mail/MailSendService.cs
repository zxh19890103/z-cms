using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nt.Model;
using System.Net.Mail;
using System.Net;
using System.Data;
using Nt.DAL;

namespace Nt.BLL.Mail
{
    public class MailSendService
    {
        #region service
        EmailService _emailService;
        EmailAccountService _emailAccountService;
        #endregion

        #region Props

        private Nt_EmailAccount _account;
        public Nt_EmailAccount EmailAccount
        {
            get { return _account; }
            set { _account = value; }
        }

        #endregion

        #region ctor

        public MailSendService()
        {
            _emailService = new EmailService();
            _emailAccountService = new EmailAccountService();
        }

        #endregion

        #region methods

        public void SendMail(string subject, string body, string to, string toName)
        {
            if (_account == null)
                InitEmailAccountFromDB();

            MailMessage message = new MailMessage();
            message.Subject = subject;
            message.To.Add(new MailAddress(to, toName));
            message.From = new MailAddress(_account.Email, _account.DisplayName);
            message.Body = body;
            message.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.UseDefaultCredentials = _account.UseDefaultCredentials;
            smtpClient.Host = _account.Host;
            smtpClient.Port = _account.Port;
            smtpClient.EnableSsl = _account.EnableSsl;
            if (_account.UseDefaultCredentials)
                smtpClient.Credentials = CredentialCache.DefaultNetworkCredentials;
            else
                smtpClient.Credentials = new NetworkCredential(_account.UserName, _account.Password);
            try
            {
                smtpClient.Send(message);
            }
            catch (SmtpFailedRecipientsException) { throw; }
            catch (SmtpException) { throw; }
            catch (Exception) { throw; }
            //send a mail
            Nt_Email email = new Nt_Email()
            {
                AddDate = DateTime.Now,
                Bcc = "",
                Body = body,
                CC = "",
                EmailAccountId = _account.Id,
                From = _account.Email,
                FromName = _account.DisplayName
                ,
                Priority = -1,
                SentDate = DateTime.Now,
                SentTries = 0,
                Subject = subject,
                To = to,
                ToName = toName
            };
            _emailService.Insert(email);
        }

        void InitEmailAccountFromDB()
        {
            _account = new Nt_EmailAccount();
            DataTable accounts = CommonFactory.GetList("Nt_EmailAccount", "", "IsDefault");
            if (accounts.Rows.Count < 1)
                throw new Exception("数据库中没有任何邮箱账号，请先添加!");
            _account.DisplayName = accounts.Rows[0]["DisplayName"].ToString();
            _account.Email = accounts.Rows[0]["Email"].ToString();
            _account.EnableSsl = Convert.ToBoolean(accounts.Rows[0]["EnableSsl"]);
            _account.Host = accounts.Rows[0]["Host"].ToString();
            _account.Id = Convert.ToInt32(accounts.Rows[0]["Id"]);
            _account.IsDefault = Convert.ToBoolean(accounts.Rows[0]["IsDefault"]);
            _account.Password = accounts.Rows[0]["Password"].ToString();
            _account.Port = Convert.ToInt32(accounts.Rows[0]["Port"]);
            _account.UseDefaultCredentials = Convert.ToBoolean(accounts.Rows[0]["UseDefaultCredentials"]);
            _account.UserName = accounts.Rows[0]["UserName"].ToString();
            accounts.Dispose();
        }

        #endregion
    }
}
