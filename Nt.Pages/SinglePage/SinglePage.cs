using Nt.BLL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.SinglePage
{
    public class SinglePage : NtPageForList<Nt_SinglePage>
    {
        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.SinglePageManage;
            }
        }
    }
}
