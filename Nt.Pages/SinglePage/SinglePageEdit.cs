using Nt.BLL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.SinglePage
{
    public class SinglePageEdit : NtPageForEdit<Nt_SinglePage>
    {
        #region methods


        protected override void InitRequiredData()
        {
            ListUrl = "SinglePage.aspx";
            base.InitRequiredData();
        }

        protected override void BeginConfigInsert()
        {
            Model.FirstPicture = NtUtility.GetImageUrl(Model.Body);
            Model.MetaDescription = NtUtility.SubStringWithoutHtml(Model.MetaDescription, 1024);
            Model.MetaKeyWords = NtUtility.SubStringWithoutHtml(Model.MetaKeyWords, 1024);
            Model.Short = NtUtility.SubStringWithoutHtml(Model.Short, 1024);
        }

        protected override void BeginConfigUpdate()
        {
            Model.FirstPicture = NtUtility.GetImageUrl(Model.Body);
            Model.MetaDescription = NtUtility.SubStringWithoutHtml(Model.MetaDescription, 1024);
            Model.MetaKeyWords = NtUtility.SubStringWithoutHtml(Model.MetaKeyWords, 1024);
            Model.Short = NtUtility.SubStringWithoutHtml(Model.Short, 1024);
        }

        #endregion

        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.SinglePageManage;
            }
        }
    }
}
