using Nt.BLL;
using Nt.Framework;
using Nt.Model.SettingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Common
{
    public class WebsiteSettings:NtPageForSetting<WebsiteInfoSettings>
    {
        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.WebSiteSettings;
            }
        }

        protected override void InitRequiredData()
        {
            base.InitRequiredData();
        }
    }
}
