using Nt.BLL;
using Nt.Framework;
using Nt.Model.SettingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.News
{
    public class Settings : NtPageForSetting<NewsSettings>
    {
        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.NewsSettings;
            }
        }
    }
}
