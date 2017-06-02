using Nt.BLL;
using Nt.Framework;
using Nt.Model.SettingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Book
{
    public class AdminNotice : NtPageForSetting<BookAdminNotice>
    {
        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.BookAdminNotice;
            }
        }

        protected override void InitRequiredData()
        {
            PageTitle = "管理员公告";
            base.InitRequiredData();
        }
    }
}
