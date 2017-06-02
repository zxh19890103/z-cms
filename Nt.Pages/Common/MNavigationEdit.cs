using Nt.BLL;
using Nt.DAL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Common
{
    public class MNavigationEdit : NtPageForEditAsTree<Nt_Mobile_Navigation>
    {
        public string ParentName { get; set; }
        int _parentID = 0;
        public int ParentID { get { return _parentID; } set { _parentID = value; } }

        public string[] AnchorTargetProvider { get { return new string[] { "_blank", "_self", "_top", "_parent" }; } }

        protected override void InitRequiredData()
        {
            _service = new MNavigationService();
            ListUrl = "MNavigation.aspx";
        }

        protected override void BeginInitDataToInsert()
        {
            Int32.TryParse(Request.QueryString["PId"], out _parentID);
            ParentName = CommonFactoryAsTree.GetFullName("Nt_Mobile_Navigation", ParentID);
        }

        protected override void BeginConfigInsert()
        {
            Model.MetaDescription = NtUtility.SubStringWithoutHtml(Model.MetaDescription, 1024);
            Model.MetaKeyWords = NtUtility.SubStringWithoutHtml(Model.MetaKeyWords, 1024);
            base.BeginConfigInsert();
        }

        protected override void BeginConfigUpdate()
        {
            Model.MetaDescription = NtUtility.SubStringWithoutHtml(Model.MetaDescription, 1024);
            Model.MetaKeyWords = NtUtility.SubStringWithoutHtml(Model.MetaKeyWords, 1024);
            base.BeginConfigUpdate();
        }

        protected override void EndInitDataToUpdate()
        {
            ParentName = CommonFactoryAsTree.GetFullName("Nt_Mobile_Navigation", Model.Parent);
            ParentID = Model.Parent;
        }

        protected override bool NtValidateForm()
        {
            if (Model.Name == string.Empty)
            {
                Alert("导航名称不能为空!", -1);
                return false;
            }
            return true;
        }
        
        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.NavigationManage;
            }
        }
    }
}
