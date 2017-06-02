using Nt.BLL;
using Nt.DAL;
using Nt.Framework;
using Nt.Model.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace Nt.Pages.Product
{
    public class List : NtPageForList<View_Product>
    {
        #region service
        PictureService _pictureService;
        ProductCategoryService _categoryService;
        #endregion

        #region props

        List<ListItem> _availableProductCategories = null;
        public List<ListItem> AvailableProductCategories
        {
            get
            {
                if (_availableProductCategories == null)
                    _availableProductCategories = CommonFactoryAsTree.GetDropDownList("Nt_ProductCategory",
                        string.Format("Language_Id={0} And IsDownloadable=0", LanguageID));
                return _availableProductCategories;
            }
        }

        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.ProductManage;
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
            string filter = " IsDownloadable=0 ";
            if (!string.IsNullOrEmpty(searchTitle))
                filter += " And Title like '%" + searchTitle + "%' ";
            if (!string.IsNullOrEmpty(searchCategory) && searchCategory != "0")
            {
                filter += " And CategoryCrumbs like '%," + searchCategory + ",%' ";
            }

            Pager.TotalRecords = _service.GetRecordsCount(filter);
            DataTable data= _service.GetList(Pager.PageIndex, Pager.PageSize,"DisplayOrder desc", filter);

            foreach (DataRow r in data.Rows)
            {
                r["ThumbnailUrl"] = _pictureService.GetThumbnailUrl(r["ThumbnailUrl"].ToString(),
                    ThumbnailSize, true);
            }

            DataSource = data;
        }

        protected override void InitRequiredData()
        {
            _pictureService = new PictureService();
            _categoryService = new ProductCategoryService(false);
            NeedPagerize = true;
        }

        #endregion

        #region WebService

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

            ProductService t_serivce = new ProductService();
            t_serivce.BatchMigrate(ids, Convert.ToInt32(to));

            json["error"] = 0;
            json["message"] = "转移成功!";
            return LitJson.JsonMapper.ToJson(json);
        }

        #endregion

    }
}
