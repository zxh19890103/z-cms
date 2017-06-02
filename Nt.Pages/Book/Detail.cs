using Nt.BLL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Book
{
    public class Detail:NtPageEditWithCatalog<Nt_Book>
    {
        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.BookManage;
            }
        }

        protected override void InitRequiredData()
        {
            PageTitle = "预订详细";
            base.InitRequiredData();
        }
    }
}
