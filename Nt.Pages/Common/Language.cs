using Nt.BLL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Common
{
    public class Language : NtPageForList<Nt_Language>
    {
        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.LanaguageManage;
            }
        }

        protected override void InitRequiredData()
        {
            base.InitRequiredData();
        }
    }
}
