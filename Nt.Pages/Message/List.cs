using Nt.BLL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Message
{
    public class List : NtPageListWithCatalog<Nt_Message>
    {
        #region methods
        protected override void InitRequiredData()
        {
            NeedPagerize = true;
        }

        protected override void BeginInitPageData()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["type"]))
            {
                string filter = string.Empty;
                filter = "type=" + Request.QueryString["type"];
                Pager.TotalRecords = _service.GetRecordsCount(filter);
                DataSource = _service.GetList(Pager.PageIndex, Pager.PageSize, "DisplayOrder desc", filter);
            }
        }

        #endregion

        #region props
        public MessageService Service { get { return _service as MessageService; } }
        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.MessageManage;
            }
        }
        #endregion
    }
}
