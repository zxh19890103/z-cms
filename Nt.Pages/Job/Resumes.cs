using Nt.BLL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Job
{
    public class Resumes : NtPageForList<Nt_Resume>
    {

        int _jobID;
        public int JobID { get { return _jobID; } }

        protected override void BeginInitPageData()
        {
            if (!Int32.TryParse(Request.QueryString["Job_Id"], out _jobID))
            {
                Goto("List.aspx", "参数错误");
            }
            var service = _service as ResumeService;
            DataSource = service.GetAvailableResume(_jobID);
        }

        public ResumeService Service { get { return _service as ResumeService; } }

        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.JobResumeManage;
            }
        }
    }
}
