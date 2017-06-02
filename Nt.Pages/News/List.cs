using Nt.BLL;
using Nt.DAL;
using Nt.Framework;
using Nt.Model.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace Nt.Pages.News
{
    public class List : NtPageForList<View_News>
    {
        #region service
        private NewsCategoryService _categoryService;
        #endregion

        #region props

        List<ListItem> _availableNewsCategories = null;
        public List<ListItem> AvailableNewsCategories
        {
            get
            {
                if (_availableNewsCategories == null)
                    _availableNewsCategories = CommonFactoryAsTree.GetDropDownList("Nt_NewsCategory",
                        string.Format("Language_Id={0}", LanguageID));
                return _availableNewsCategories;
            }
        }

        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.NewsManage;
            }
        }

        #endregion

        #region override

        protected override void BeginInitPageData()
        {
            TrySearch();
        }

        void TrySearch()
        {
            string searchTitle = Request.QueryString[ConstStrings.SEARCH_TITLE];
            string searchCategory = Request.QueryString[ConstStrings.SEARCH_CATEGORY];
            string filter = string.Empty;
            if (!string.IsNullOrEmpty(searchTitle))
                filter += " Title like '%" + searchTitle + "%' ";
            if (!string.IsNullOrEmpty(searchCategory) && searchCategory != "0")
            {
                if (string.IsNullOrEmpty(filter))
                    filter += " CategoryCrumbs like '%," + searchCategory + ",%' ";
                else
                    filter += " And CategoryCrumbs like '%," + searchCategory + ",%' ";
            }
            if (!string.IsNullOrEmpty(filter))
            {
                Pager.TotalRecords = _service.GetRecordsCount(filter);
                DataSource = _service.GetList(Pager.PageIndex, Pager.PageSize, "DisplayOrder desc", filter);
            }
            else
            {
                Pager.TotalRecords = _service.GetRecordsCount();
                DataSource = _service.GetList("DisplayOrder desc", Pager.PageIndex, Pager.PageSize);
            }
        }

        protected override void InitRequiredData()
        {
            _categoryService = new NewsCategoryService();
            NeedPagerize = true;
        }

        #endregion

        #region webservice

        [WebMethod]
        public static string BatchMigrate(string ids, string to)
        {
            LitJson.JsonData json = new LitJson.JsonData();
            if (string.IsNullOrEmpty(ids))
            {
                json["error"] = 1;
                json["message"] = "没有可操作的项!";
                return LitJson.JsonMapper.ToJson(json);
            }
            NewsService t_serivce = new NewsService();
            t_serivce.BatchMigrate(ids, Convert.ToInt32(to));

            json["error"] = 0;
            json["message"] = "转移成功!";
            return LitJson.JsonMapper.ToJson(json);
        }

        #endregion
    }
}
