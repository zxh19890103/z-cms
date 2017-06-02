using Nt.BLL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Common
{
    public class Log : NtPageForList<Nt_Log>
    {
        protected override void InitRequiredData()
        {
            NeedPagerize = true;
        }

        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.LogManage;
            }
        }
    }
}
