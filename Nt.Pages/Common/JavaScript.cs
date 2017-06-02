using Nt.BLL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Common
{
    public class JavaScript : NtPageForList<Nt_JavaScript>
    {
        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.JsManage;
            }
        }

        protected override void InitRequiredData()
        {
            base.InitRequiredData();
        }
    }
}
