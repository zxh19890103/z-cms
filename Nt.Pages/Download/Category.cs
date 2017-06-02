using Nt.BLL;
using Nt.DAL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Download
{
    public class Category : NtPageForListAsTree<Nt_ProductCategory>
    {

        protected override void InitRequiredData()
        {
            _service = new ProductCategoryService(true);
        }

        protected override void BeginInitPageData()
        {
            DataSource = CommonFactoryAsTree.GetTree("Nt_ProductCategory",
                string.Format("Language_Id={0} And IsDownloadable=1", LanguageID));
            base.BeginInitPageData();
        }


        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.DownloadCategoryManage;
            }
        }
    }
}
