using Nt.BLL;
using Nt.Framework;
using Nt.Model.SettingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Member
{
    public class RegisterDeclare : NtPageForSetting<RegisterDeclaration>
    {
        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.MemberRegisterDeclare;
            }
        }

    }
}
