using Nt.BLL;
using Nt.Framework;
using Nt.Model.SettingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Order
{
    public class Settings : NtPageForSetting<OrderSettings>
    {
        #region props
        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.OrderSettings;
            }
        }
        #endregion
    }
}
