using Nt.BLL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Book
{
    public class List : NtPageListWithCatalog<Nt_Book>
    {
        protected override void InitRequiredData()
        {
            PageTitle = "预订/留言列表";
            _service = new BookService();
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

        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.BookManage;
            }
        }
    }
}
