using Nt.BLL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.SinglePage
{
    public class SpecialPage : NtPageForList<Nt_SpecialPage>
    {
        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.SpecialPageManage;
            }
        }
    }
}
