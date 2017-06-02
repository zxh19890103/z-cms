using Nt.BLL;
using Nt.DAL;
using Nt.DAL.Helper;
using Nt.Framework;
using Nt.Model;
using Nt.Model.SettingModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Nt.Pages.Download
{
    public class Edit : NtPageForEdit<Nt_Product>
    {
        #region service
        ProductCategoryService _categoryService;
        PictureService _pictureService;
        #endregion

        #region Props

        private List<ListItem> _productCategories = null;
        public List<ListItem> ProductCategories
        {
            get
            {
                return _productCategories;
            }
        }

        ProductSettings _settings;
        /// <summary>
        /// 产品设置
        /// </summary>
        public ProductSettings Settings
        {
            get
            {
                if (_settings == null)
                    _settings = SettingService.GetSettingModel<ProductSettings>();
                return _settings;
            }
        }

        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.DownloadEdit;
            }
        }


        #endregion

        #region override

        protected override void BeginConfigInsert()
        {
            Model.MetaKeyWords = NtUtility.SubStringWithoutHtml(Model.MetaKeyWords, 1024);
            Model.MetaDescription = NtUtility.SubStringWithoutHtml(Model.MetaDescription, 1024);
            Model.Short = NtUtility.SubStringWithoutHtml(Model.Short, 1024);
            FindThumbnail();
        }

        protected override void EndConfigInsert()
        {
            base.EndConfigInsert();
            //SaveFields();
        }

        protected override void BeginConfigUpdate()
        {
            Model.MetaKeyWords = NtUtility.SubStringWithoutHtml(Model.MetaKeyWords, 1024);
            Model.MetaDescription = NtUtility.SubStringWithoutHtml(Model.MetaDescription, 1024);
            Model.Short = NtUtility.SubStringWithoutHtml(Model.Short, 1024);
            FindThumbnail();
            //SaveFields();
        }

        protected override void BeginInitDataToInsert()
        {
            _productCategories = CommonFactoryAsTree.GetDropDownList("Nt_ProductCategory",
                string.Format("Language_Id={0} And IsDownloadable=1", LanguageID));
            if (_productCategories == null || _productCategories.Count < 1)
                Goto("CategoryEdit.aspx", "请先添加下载类别");

            int categoryid = IMPOSSIBLE_ID;
            if (Int32.TryParse(Request.QueryString["CategoryId"], out categoryid))
                NtUtility.ListItemSelect(_productCategories, categoryid);
            //GenerateFieldsScript();//生成字段名数据json数组
        }

        protected override void EndInitDataToUpdate()
        {
            var service = _service as ProductService;
            var c = CommonFactory.GetById<Nt_ProductCategory>(Model.ProductCategory_Id);
            if (c == null)
                GotoListPage("下载类别不存在!");
            _productCategories = new List<ListItem>();
            _productCategories.Add(new ListItem(
                CommonFactoryAsTree.GetFullName("Nt_ProductCategory", c.Crumbs), c.Id.ToString()));
        }

        DataTable _productPictures;
        /// <summary>
        /// 产品图片
        /// </summary>
        public DataTable ProductPictures
        {
            get
            {
                if (_productPictures == null)
                {
                    var service = _service as ProductService;
                    _productPictures = service.GetProductPictures(NtID, true);
                    if (_productPictures != null)
                    {
                        foreach (DataRow item in _productPictures.Rows)
                        {
                            item["PictureUrl"] = _pictureService.GetPictureUrl(item["PictureUrl"].ToString(),
                                ThumbnailSize, true);
                        }
                    }
                }
                return _productPictures;
            }
        }


        DataTable _productFields;
        /// <summary>
        ///  产品字段
        /// </summary>
        public DataTable ProductFields
        {
            get
            {
                if (_productFields == null)
                {
                    _productFields = GetProductFields(true);
                }
                return _productFields;
            }
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
            _service = new ProductService(true);
            _categoryService = new ProductCategoryService(true);
            _pictureService = new PictureService();
        }

        /// <summary>
        /// 保存字段值
        /// </summary>
        void SaveFields()
        {
            string fids = Request.Form["ProductField_Id"];
            if (string.IsNullOrEmpty(fids))
                return;
            string[] arr_ids = fids.Split(',');
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.AppendFormat("Delete From Nt_ProductFieldValue Where Product_Id={0}", NtID);//delete old bindings
            for (int i = 0; i < arr_ids.Length; i++)
            {
                sqlBuilder.AppendFormat("Insert into Nt_ProductFieldValue(Product_Id,ProductField_Id,Value)values({0},{1},'{2}')\r\n",
                        NtID, arr_ids[i], NtUtility.EnsureNotNull(Request.Form["FieldValue" + arr_ids[i]]).Replace("'", "''"));
            }
            SqlHelper.ExecuteNonQuery(sqlBuilder.ToString());
        }


        /*
            *  [
            *{c:1,fc:3,fids:[],fn:['','','',...]},
            *{c:1,fc:3,fids:[],fn:['','','',...]}
            * ]
        */
        void GenerateFieldsScript()
        {
            int[] categories = _categoryService.GetAllIds();
            DataTable data = CommonFactory.GetList("Nt_ProductField", "", "DisplayOrder desc");
            StringBuilder scriptBuilder = new StringBuilder();
            scriptBuilder.Append("\r\n window.ntfns=[");
            foreach (int i in categories)
            {
                DataRow[] rs = data.Select("ProductCategory_Id=" + i);
                int fieldCount = rs.Length;
                string[] ids = new string[fieldCount];
                string[] fieldNames = new string[fieldCount];
                for (int j = 0; j < fieldCount; j++)
                {
                    fieldNames[j] = rs[j]["Name"].ToString();
                    ids[j] = rs[j]["Id"].ToString();
                }
                scriptBuilder.AppendFormat("{{c:{0},fc:{1},fids:[{2}],fns:['{3}']}},",
                    i, fieldCount, String.Join(",", ids), String.Join("','", fieldNames));
            }
            scriptBuilder.Remove(scriptBuilder.Length - 1, 1);//remove the last comma
            scriptBuilder.Append("]\r\n");
            RegisterJavaScript(scriptBuilder.ToString());
            data.Dispose();
        }

        /// <summary>
        /// 按照图片排序找出第一张产品图
        /// </summary>
        void FindThumbnail()
        {
            if (string.IsNullOrEmpty(Request.Form["Picture.Id"]))
            {
                Model.PictureIds = "";
                return;
            }

            int int_thumbnailId = 0;
            string[] pic_ids = Request.Form["Picture.Id"].Split(',');

            string thumbnailId = Request.Form["SetThumbnail"];
            if (!string.IsNullOrEmpty(thumbnailId))//如果指定了缩略图
            {
                int_thumbnailId = Convert.ToInt32(thumbnailId);
            }
            else                                                       //否则按排序
            {
                string[] pic_orders = Request.Form["Picture.DisplayOrder"].Split(',');
                int order = IMPOSSIBLE_ID;
                int current = 0;
                int target = 0;
                for (int i = 0; i < pic_orders.Length; i++)
                {
                    if (pic_orders[i] == string.Empty)
                        current = 0;
                    else
                        current = Convert.ToInt32(pic_orders[i]);
                    if (current > order)
                    {
                        order = current;
                        target = i;
                    }
                }
                int_thumbnailId = Convert.ToInt32(pic_ids[target]);
            }

            Model.ThumbnailUrl = _pictureService.GetPictureUrl(int_thumbnailId);
            Model.ThumbnailID = int_thumbnailId;
            string url = Model.ThumbnailUrl;

            //列表页缩略图
            if (Settings.EnableThumbnail)
            {
                switch (Settings.ThumbnailMode)
                {
                    case "H":
                        _pictureService.GetPictureUrl(url, Settings.ThumbnailHeight, true);
                        break;
                    case "W":
                        _pictureService.GetPictureUrl(url, Settings.ThumbnailWidth, false);
                        break;
                    case "HW":
                    case "CUT":
                        _pictureService.GetPictureUrl(url, Settings.ThumbnailWidth,
                           Settings.ThumbnailHeight, Settings.ThumbnailMode);
                        break;
                    default:
                        break;
                }
            }

            //首页缩略图
            if (Settings.EnableThumbOnHomePage)
            {
                switch (Settings.ThumbOnHomePageMode)
                {
                    case "H":
                        _pictureService.GetPictureUrl(url, Settings.ThumbOnHomePageHeight, true);
                        break;
                    case "W":
                        _pictureService.GetPictureUrl(url, Settings.ThumbOnHomePageWidth, false);
                        break;
                    case "HW":
                    case "CUT":
                        _pictureService.GetPictureUrl(url, Settings.ThumbOnHomePageWidth,
                           Settings.ThumbOnHomePageHeight, Settings.ThumbOnHomePageMode);
                        break;
                    default:
                        break;
                }
            }

            //后台列表页缩略图
            _pictureService.GetPictureUrl(url, ThumbnailSize, true);
        }


        DataTable GetProductFields(bool showHidden)
        {
            int categoryID = 0;
            if (EnsureEdit)
                categoryID = Model.ProductCategory_Id;
            else
            {
                if (Int32.TryParse(Request.QueryString["CategoryId"], out categoryID)) { }
                else { categoryID = Convert.ToInt32(ProductCategories[0].Value); }
            }
            string sql = "select  id as field_Id,* " +
            "from [Nt_ProductField] as t0 left JOIN (SELECT Product_Id,Value,ProductField_Id  FROM [dbo].[Nt_ProductFieldValue] where Product_Id=" + NtID +
            ") as t1 " +
            "on t0.Id=t1.ProductField_Id Where t0.ProductCategory_Id=" + categoryID;

            if (!showHidden)
                sql += " And t0.Display=1 ";
            sql += "Order by t0.DisplayOrder desc";

            DataTable fields = SqlHelper.ExecuteDataset(sql).Tables[0];
            return fields;
        }

        #endregion

    }
}
