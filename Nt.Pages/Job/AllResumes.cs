using Nt.BLL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Job
{
    public class AllResumes : NtPageForList<Nt_Resume>
    {
        public ResumeService Service { get { return _service as ResumeService; } }


        protected override void InitPageData()
        {
            NeedPagerize = true;
            base.InitPageData();
        }

        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.JobResumeManage;
            }
        }
    }
}
