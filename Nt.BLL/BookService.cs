using Nt.BLL.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Nt.Model;
using System.Data;
using Nt.DAL;

namespace Nt.BLL
{
    public class BookService : BaseServiceWithCatalog<Nt_Book>
    {
        /// <summary>
        /// 获取所单条留言所有的回复
        /// </summary>
        /// <param name="id">留言ID</param>
        /// <returns></returns>
        public DataTable GetAllReply(int id)
        {
            return CommonFactory
                .GetList("Nt_BookReply", string.Format("Book_Id={0}", id), "DisplayOrder desc,ReplyDate desc");
        }

        /// <summary>
        /// 获取留言回复并过滤隐藏项
        /// </summary>
        /// <param name="id">留言ID</param>
        /// <returns></returns>
        public DataTable GetAailableReply(int id)
        {
            return CommonFactory
                .GetList("Nt_BookReply", string.Format("Book_Id={0} And Display=1 ", id), "DisplayOrder desc,ReplyDate desc");
        }

    }
}
