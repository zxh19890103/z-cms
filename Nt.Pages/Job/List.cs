using Nt.BLL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Job
{
    public class List : NtPageListWithCatalog<Nt_Job>
    {
        protected override void InitRequiredData()
        {
            NeedPagerize = true;
        }

        protected override void InitPageData()
        {
            DataSource = _service.GetList("DisplayOrder desc", Pager.PageIndex, Pager.PageSize);
            base.InitPageData();
        }


        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.JobManage;
            }
        }
    }
}
