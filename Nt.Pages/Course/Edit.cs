using Nt.BLL;
using Nt.DAL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Nt.Pages.Course
{
    public class Edit:NtPageForEdit<Nt_Course>
    {
        #region Props
        private List<ListItem> _courseCategories = null;
        public List<ListItem> CourseCategories
        {
            get
            {
                return _courseCategories;
            }
        }


        #endregion

        #region override

        protected override void BeginConfigInsert()
        {
            Model.MetaDescription = NtUtility.SubStringWithoutHtml(Model.MetaDescription, 1024);
            Model.MetaKeywords = NtUtility.SubStringWithoutHtml(Model.MetaKeywords, 1024);
            Model.Short = NtUtility.SubStringWithoutHtml(Model.Short, 1024);
        }

        protected override void BeginConfigUpdate()
        {
            Model.MetaDescription = NtUtility.SubStringWithoutHtml(Model.MetaDescription, 1024);
            Model.MetaKeywords = NtUtility.SubStringWithoutHtml(Model.MetaKeywords, 1024);
            Model.Short = NtUtility.SubStringWithoutHtml(Model.Short, 1024);
        }

        protected override void BeginInitDataToInsert()
        {
            _courseCategories = CommonFactoryAsTree.GetDropDownList("Nt_CourseCategory",
               "Language_Id=" + LanguageID);
            if (_courseCategories == null || _courseCategories.Count < 1)
                Goto("CategoryEdit.aspx", "请先添加班级类别!");//不可以没有类别  最好有个提示

            int categoryid = IMPOSSIBLE_ID;
            if (Int32.TryParse(Request.QueryString["CategoryId"], out categoryid))
            {
                NtUtility.ListItemSelect(_courseCategories, categoryid);
            }
        }

        protected override void EndInitDataToUpdate()
        {
            _courseCategories = CommonFactoryAsTree.GetDropDownList("Nt_CourseCategory",
                "Language_Id=" + LanguageID);
            if (_courseCategories == null || _courseCategories.Count < 1)
                Goto("CategoryEdit.aspx", "请先添加班级类别!");//不可以没有类别  最好有个提示
            NtUtility.ListItemSelect(_courseCategories, Model.CourseCategory_Id);
        }

        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.CourseEdit;
            }
        }

        #endregion

    }
}
