using Nt.BLL;
using Nt.DAL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Product
{
    public class CategoryEdit : NtPageForEditAsTree<Nt_ProductCategory>
    {

        #region Properties

        public string ParentName { get; set; }
        int _parentID = 0;
        public int ParentID { get { return _parentID; } set { _parentID = value; } }


        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.ProductCategoryManage;
            }
        }


        #endregion

        #region override
        /// <summary>
        /// 对表单的验证
        /// </summary>
        protected override bool NtValidateForm()
        {
            return true;
        }

        protected override void BeginInitDataToInsert()
        {
            Int32.TryParse(Request.QueryString["PId"], out _parentID);
            ParentName = CommonFactoryAsTree.GetFullName("Nt_ProductCategory", ParentID);
        }

        /// <summary>
        /// 在BeginInitDataToInsert和BeginInitDataToUpdate中不能使用Model
        /// </summary>
        protected override void EndInitDataToUpdate()
        {
            ParentName = CommonFactoryAsTree.GetFullName("Nt_ProductCategory", Model.Parent);
            ParentID = Model.Parent;
        }

        protected override void InitRequiredData()
        {
            ListUrl = "Category.aspx";
            _service = new ProductCategoryService(false);
        }

        #endregion

    }
}
