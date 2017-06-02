using Nt.BLL;
using Nt.DAL;
using Nt.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Nt.Pages.Download
{
    public class CopyCategory : NtPage
    {
        protected override void OnLoad(EventArgs e)
        {
            if (IsHttpPost)
            {
                string langids = Request.Form[ConstStrings.COPY_TREE_TARGET_LANGUAGES];
                if (!string.IsNullOrEmpty(langids))
                {
                    int[] arr_ids = NtUtility.SeparateToIntArray(',', langids);
                    ProductCategoryService service = new ProductCategoryService();
                    service.CopyAllDataTo(arr_ids, "download", null);
                    Goto("Category.aspx", "下载目录拷贝成功!");
                }
                else
                {
                    Alert("参数错误!");
                }
            }

            base.OnLoad(e);
        }

        DataTable _availableLanguages;
        public DataTable AvailableLanguages
        {
            get
            {
                if (_availableLanguages == null)
                {
                    _availableLanguages = CommonFactory.GetList("Nt_Language");
                }
                return _availableLanguages;
            }
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
