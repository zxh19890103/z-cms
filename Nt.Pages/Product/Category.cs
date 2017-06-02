using Nt.BLL;
using Nt.DAL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services;

namespace Nt.Pages.Product
{
    public class Category : NtPageForListAsTree<Nt_ProductCategory>
    {

        protected override void InitRequiredData()
        {
            _service = new ProductCategoryService(false);
        }

        protected override void BeginInitPageData()
        {
            DataSource = CommonFactoryAsTree.GetTree("Nt_ProductCategory",
                string.Format("Language_Id={0} And IsDownloadable=0", LanguageID));
            base.BeginInitPageData();
        }

        [WebMethod]
        public static string Migrate(string target, string to)
        {
            return "";
        }


        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.ProductCategoryManage;
            }
        }
    }
}
