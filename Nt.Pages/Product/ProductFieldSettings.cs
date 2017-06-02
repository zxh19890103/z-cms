using Nt.BLL;
using Nt.DAL;
using Nt.DAL.Helper;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Nt.Pages.Product
{
    public class ProductFieldSettings : NtPageForList<Nt_ProductField>
    {
        int _productCategory_Id = 0;
        public int ProductCategoryId { get { return _productCategory_Id; } }
        public List<ListItem> Categories { get; set; }

        protected override void InitRequiredData()
        {
            base.InitRequiredData();

            _service = new BaseService<Nt_ProductField>();

            Categories = CommonFactoryAsTree.GetDropDownList("Nt_ProductCategory",
                        string.Format("Language_Id={0} And IsDownloadable=0", LanguageID));

            if (Categories == null || Categories.Count < 1)
                Goto("CategoryEdit.aspx", "请先添加产品类别");

            if (!Int32.TryParse(Request.QueryString["ProductCategory_Id"], out _productCategory_Id))
            {
                _productCategory_Id = Convert.ToInt32(Categories[0].Value);
            }

            NtUtility.ListItemSelect(Categories, _productCategory_Id);


            string crumbs = SqlHelper.ExecuteScalar(
                "Select Crumbs From Nt_ProductCategory Where ID=" + ProductCategoryId)
                .ToString();
            crumbs = CommonHelper.ModifyCrumbs(crumbs);
            DataSource = _service.GetList(string.Format(
                " ProductCategory_Id in ({0}) ", crumbs));
        }

        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.ProductFieldsManage;
            }
        }

    }
}
