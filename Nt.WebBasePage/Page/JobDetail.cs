using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Web
{
    public class JobDetail:DetailPage<Nt_Job>
    {
        public void CalculateNextAndPrevious()
        {
            string orderby = "DisplayOrder desc,AddDate desc" + ",ID desc";
            CalculateNextAndPrevious(orderby, string.Empty);
        }

        public override void TryGetModel()
        {
            base.TryGetModel();
            Rating();
        }

        public override int PageType
        {
            get
            {
                return NtConfig.JOB;
            }
        }
    }
}
