using Nt.BLL;
using Nt.DAL;
using Nt.Framework;
using Nt.Model;
using Nt.Model.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Nt.Pages.News
{
    public class Edit : NtPageForEdit<Nt_News>
    {
        #region service
        private NewsCategoryService _categoryService;
        #endregion

        #region Props
        private List<ListItem> _newsCategories = null;
        public List<ListItem> NewsCategories
        {
            get
            {
                return _newsCategories;
            }
        }

        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.NewsEdit;
            }
        }

        #endregion

        #region override

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

        protected override void BeginInitDataToInsert()
        {
            _newsCategories = CommonFactoryAsTree.GetDropDownList("Nt_NewsCategory",
                 string.Format("Language_Id={0}", LanguageID));

            if (_newsCategories == null || _newsCategories.Count < 1)
                Goto("CategoryEdit.aspx", "请先添加新闻类别!");//不可以没有类别  最好有个提示

            int categoryid = IMPOSSIBLE_ID;
            if (Int32.TryParse(Request.QueryString["CategoryId"], out categoryid))
            {
                NtUtility.ListItemSelect(_newsCategories, categoryid);
            }
        }

        protected override void BeginInitDataToUpdate()
        {
            _newsCategories = CommonFactoryAsTree.GetDropDownList("Nt_NewsCategory",
                string.Format("Language_Id={0}", LanguageID));
            if (_newsCategories == null || _newsCategories.Count < 1)
                Goto("CategoryEdit.aspx", "请先添加新闻类别!");//不可以没有类别  最好有个提示
            base.BeginInitDataToUpdate();
        }

        protected override void EndInitDataToUpdate()
        {
            var c = CommonFactory.GetById<Nt_NewsCategory>(Model.NewsCategory_Id);
            if (c == null)
                GotoListPage("新闻类别不存在!");
            NtUtility.ListItemSelect(_newsCategories, Model.NewsCategory_Id);
        }
        
        /// <summary>
        /// 表单验证
        /// </summary>
        protected override bool NtValidateForm()
        {
            return true;
        }

        protected override void InitRequiredData()
        {
            PageTitle = "新闻管理";
            _categoryService = new NewsCategoryService();
        }

        #endregion
    }
}
