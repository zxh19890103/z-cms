using Nt.BLL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Common
{
    public class BannerEdit : NtPageForEdit<Nt_Banner>
    {

        protected override void InitRequiredData()
        {
            ListUrl = "Banner.aspx";
            base.InitRequiredData();
        }

        protected override bool NtValidateForm()
        {
            return true;
        }

        protected override void BeginConfigInsert()
        {
            Model.Note = NtUtility.SubStringWithoutHtml(Model.Note, 1024);
            Model.Text = NtUtility.SubStringWithoutHtml(Model.Text, 1024);
            base.BeginConfigInsert();
        }

        protected override void BeginConfigUpdate()
        {
            base.BeginConfigUpdate();
            Model.Note = NtUtility.SubStringWithoutHtml(Model.Note, 1024);
            Model.Text = NtUtility.SubStringWithoutHtml(Model.Text, 1024);
        }

        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.BannerManage;
            }
        }
    }
}
