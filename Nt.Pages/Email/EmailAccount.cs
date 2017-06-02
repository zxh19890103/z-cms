using Nt.BLL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services;

namespace Nt.Pages.Email
{
    public class EmailAccount : NtPageForList<Nt_EmailAccount>
    {
        [WebMethod]
        public static string SetDefualtEmailAccount(string id)
        {
            EmailAccountService service = new EmailAccountService();
            int i = service.SetDefualtEmailAccount(id);
            return new NtJson(
                    new
                    {
                        error = i == 0 ? 1 : 0,
                        message = i == 0 ? "没有更新任何数据" : "设置成功!"
                    }).ToString();
        }

        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.EmailAccountManage;
            }
        }

        protected override void InitRequiredData()
        {
            PageTitle = "邮件账号管理";
            base.InitRequiredData();
        }
    }
}
