using Nt.BLL;
using Nt.Framework;
using Nt.Model.SettingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Book
{
    public class Settings : NtPageForSetting<BookSettings>
    {
        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.BookSettings;
            }
        }

        protected override void InitRequiredData()
        {
            PageTitle = "预订设置";
            base.InitRequiredData();
        }
    }
}
