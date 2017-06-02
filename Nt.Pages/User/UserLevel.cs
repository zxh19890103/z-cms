using Nt.BLL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.User
{
    public class UserLevel : NtPageForList<Nt_UserLevel>
    {
        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.UserLevelManage;
            }
        }

        protected override void BeginInitPageData()
        {
            var service = _service as UserLevelService;
            DataSource = service.GetList(string.Format(" Id>={0}", UserID));
            base.BeginInitPageData();
        }

    }
}
