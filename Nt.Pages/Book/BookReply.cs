using Nt.BLL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Book
{
    public class BookReply : NtPageForList<Nt_BookReply>
    {
        #region methods

        protected override void InitRequiredData()
        {
            PageTitle = "预订/留言回复管理";
        }

        private int _bookID;
        public int BookID { get { return _bookID; } }

        protected override void BeginInitPageData()
        {
            if (!Int32.TryParse(Request.QueryString["Book_Id"], out _bookID))
            {
                CloseWindow("参数错误");
            }
            BookService service = new BookService();
            DataSource = service.GetAllReply(_bookID);
        }

        #endregion

        #region props
        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.BookReplyManage;
            }
        }
        #endregion
    }
}
