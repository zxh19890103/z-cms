using Nt.BLL;
using Nt.Framework;
using Nt.Model.SettingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Message
{
    public class Settings : NtPageForSetting<MessageSettings>
    {
        #region props
        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.MessageSettings;
            }
        }
        #endregion
    }
}
