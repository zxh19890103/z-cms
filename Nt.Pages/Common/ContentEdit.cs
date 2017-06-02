using Nt.BLL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Common
{
    public class ContentEdit : NtPageForEdit<Nt_Content>
    {
        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.ContentManage;
            }
        }

        protected override void InitRequiredData()
        {
            ListUrl = "Content.aspx";
            base.InitRequiredData();
        }
    }
}
