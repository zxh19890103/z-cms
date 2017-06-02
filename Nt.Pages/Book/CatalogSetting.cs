using Nt.BLL;
using Nt.Framework;
using Nt.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Book
{
    public class CatalogSetting : NtPage
    {
        #region service

        BookService _service;

        #endregion

        List<SimpleCatalog> _dataSource;

        public List<SimpleCatalog> DataSource
        {
            get
            {
                if (_dataSource == null)
                {
                    _dataSource = _service.GetCatalogFromXml();
                }
                return _dataSource;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            PageTitle = "预订分类管理";
            _service = new BookService();
            if (IsHttpPost)
            {
                _service.SaveCatalog(Request.Form["TypeId"], Request.Form["Name"]);
                ReLoadByScript("保存成功!");
            }
        }

        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.BookCatalogManage;
            }
        }
    }
}
