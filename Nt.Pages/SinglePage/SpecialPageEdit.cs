using Nt.BLL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.SinglePage
{
    public class SpecialPageEdit : NtPageForEdit<Nt_SpecialPage>
    {
        #region methods


        protected override void InitRequiredData()
        {
            ListUrl = "SpecialPage.aspx";
            base.InitRequiredData();
        }

        protected override void BeginConfigInsert()
        {
            Model.Note = NtUtility.SubStringWithoutHtml(Model.Note, 1024);
            base.BeginConfigInsert();
        }

        protected override void BeginConfigUpdate()
        {
            Model.Note = NtUtility.SubStringWithoutHtml(Model.Note, 1024);
            base.BeginConfigUpdate();
        }

        #endregion

        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.SpecialPageManage;
            }
        }
    }
}
