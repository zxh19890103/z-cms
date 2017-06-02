using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Web
{
    public class Job : ListPage<Nt_Job>
    {
        public override bool TryGetList()
        {
            HandlePageSize("job");
            return base.TryGetList();
        }

        protected override void InitCommonData()
        {
            OrderBy = "DisplayOrder desc,AddDate desc";
            base.InitCommonData();
        }
        
        public override int PageType
        {
            get
            {
                return NtConfig.JOB_LIST;
            }
        }
    }
}
