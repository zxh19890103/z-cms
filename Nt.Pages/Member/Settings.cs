using Nt.BLL;
using Nt.Framework;
using Nt.Model.SettingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Member
{
    public class Settings : NtPageForSetting<MemberSettings>
    {
        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.MemberSettings;
            }
        }
    }
}
