using Nt.BLL;
using Nt.Framework;
using Nt.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Message
{
    public class CatalogSettings : NtPage
    {
        #region service
        MessageService _service;
        #endregion

        #region props
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

        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.MessageCatalogManage;
            }
        }

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _service = new MessageService();
            if (IsHttpPost)
            {
                _service.SaveCatalog(Request.Form["TypeId"], Request.Form["Name"]);
                ReLoadByScript("保存成功!");
            }
        }
    }
}
