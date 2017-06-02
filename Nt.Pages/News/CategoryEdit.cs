using Nt.BLL;
using Nt.DAL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.News
{
    public class CategoryEdit : NtPageForEditAsTree<Nt_NewsCategory>
    {

        #region Properties
        public string ParentName { get; set; }
        int _parentID = 0;
        public int ParentID { get { return _parentID; } set { _parentID = value; } }

        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.NewsCategoryManage;
            }
        }

        #endregion

        #region override

        protected override bool NtValidateForm()
        {
            return true;
        }

        protected override void BeginConfigInsert()
        {
            base.BeginConfigInsert();
        }


        protected override void BeginConfigUpdate()
        {
            base.BeginConfigUpdate();
        }

        protected override void EndInitDataToUpdate()
        {
            ParentName = CommonFactoryAsTree.GetFullName("Nt_NewsCategory", Model.Parent);
            ParentID = Model.Parent;
        }

        protected override void BeginInitDataToInsert()
        {
            Int32.TryParse(Request.QueryString["PId"], out _parentID);
            ParentName = CommonFactoryAsTree.GetFullName("Nt_NewsCategory", ParentID);
        }

        protected override void InitRequiredData()
        {
            ListUrl = "Category.aspx";
            _service = new NewsCategoryService();
        }
        #endregion
    }
}
