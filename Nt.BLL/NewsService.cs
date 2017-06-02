using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nt.Model;
using System.Web.UI.WebControls;
using System.Data;
using Nt.BLL.Helper;
using Nt.DAL.Helper;

namespace Nt.BLL
{
    public class NewsService : BaseService<Nt_News>
    {
        public NewsService()
        {

        }

        #region Ajax Batch Migration

        public int BatchMigrate(string ids, int to)
        {
            if (string.IsNullOrEmpty(ids))
                throw new Exception("没有可供操作的项.");
            string sql = string.Format("Update [Nt_News] Set [NewsCategory_Id]={0} Where [Id] In ({1})", to, ids);
            return SqlHelper.ExecuteNonQuery(sql);
        }

        #endregion
    }
}
