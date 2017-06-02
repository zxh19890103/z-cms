using Nt.BLL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Common
{
    public class MNavigation : NtPageForListAsTree<Nt_Mobile_Navigation>
    {
        protected override void InitRequiredData()
        {
            _service = new MNavigationService();
        }

        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.NavigationManage;
            }
        }
    }
}
