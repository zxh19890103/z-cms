using Nt.BLL;
using Nt.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.System
{
    public class SystemDescription : NtPage
    {
        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.SystemDescription;
            }
        }
    }
}
