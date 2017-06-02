using Nt.BLL;
using Nt.DAL;
using Nt.Framework;
using Nt.Model.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Nt.Pages.Course
{
    public class List : NtPageForList<View_Course>
    {
        #region props

        List<ListItem> _availableCourseCategories = null;
        public List<ListItem> AvailableCourseCategories
        {
            get
            {
                if (_availableCourseCategories == null)
                    _availableCourseCategories =
                        CommonFactoryAsTree.GetDropDownList("Nt_CourseCategory",
                        string.Format("Language_Id={0}", LanguageID));
                return _availableCourseCategories;
            }
        }

        #endregion

        #region override

        protected override void InitRequiredData()
        {
            NeedPagerize = true;
        }

        protected override void InitPageData()
        {
            Pager.TotalRecords = _service.GetRecordsCount();
            DataSource = _service.GetList("DisplayOrder desc", Pager.PageIndex, Pager.PageSize);
            base.InitPageData();
        }

        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.CourseManage;
            }
        }

        #endregion
    }
}
