using Nt.BLL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Job
{
    public class Edit:NtPageEditWithCatalog<Nt_Job>
    {
        protected override void BeginConfigInsert()
        {
            base.BeginConfigInsert();
            Model.Note = NtUtility.SubStringWithoutHtml(Model.Note, 1024);
            Model.Requirements = NtUtility.SubStringWithoutHtml(Model.Requirements, 1024);
            Model.Duties = NtUtility.SubStringWithoutHtml(Model.Duties, 1024);
        }
        
        protected override void BeginConfigUpdate()
        {
            Model.Note = NtUtility.SubStringWithoutHtml(Model.Note, 1024);
            Model.Requirements = NtUtility.SubStringWithoutHtml(Model.Requirements, 1024);
            Model.Duties = NtUtility.SubStringWithoutHtml(Model.Duties, 1024);
            base.BeginConfigUpdate();
        }

        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.JobEdit;
            }
        }
    }
}
