using Nt.BLL;
using Nt.DAL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Course
{
    public class CategoryEdit : NtPageForEditAsTree<Nt_CourseCategory>
    {
        #region Properties
        public string ParentName { get; set; }
        int _parentID = 0;
        public int ParentID { get { return _parentID; } set { _parentID = value; } }

        #endregion

        #region override

        protected override bool NtValidateForm()
        {
            return true;
        }

        protected override void InitRequiredData()
        {
            ListUrl = "Category.aspx";
            base.InitRequiredData();
        }

        protected override void BeginConfigUpdate()
        {
            Model.EduAim = NtUtility.SubStringWithoutHtml(Model.EduAim, 1024);
            Model.EduTeachers = NtUtility.SubStringWithoutHtml(Model.EduTeachers, 1024);
            Model.FitPeople = NtUtility.SubStringWithoutHtml(Model.FitPeople, 1024);
            base.BeginConfigUpdate();
        }

        protected override void BeginConfigInsert()
        {
            Model.EduAim = NtUtility.SubStringWithoutHtml(Model.EduAim, 1024);
            Model.EduTeachers = NtUtility.SubStringWithoutHtml(Model.EduTeachers, 1024);
            Model.FitPeople = NtUtility.SubStringWithoutHtml(Model.FitPeople, 1024);
            base.BeginConfigInsert();
        }

        protected override void EndInitDataToUpdate()
        {
            ParentName = CommonFactoryAsTree.GetFullName("Nt_CourseCategory", Model.Parent);
            ParentID = Model.Parent;
        }

        protected override void BeginInitDataToInsert()
        {
            Int32.TryParse(Request.QueryString["PId"], out _parentID);
            ParentName = CommonFactoryAsTree.GetFullName("Nt_CourseCategory", ParentID);
            base.BeginInitDataToInsert();
        }

        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.CourseCategoryManage;
            }
        }
        #endregion
    }
}
