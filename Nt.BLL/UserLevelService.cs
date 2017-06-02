using Nt.DAL.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Nt.BLL
{
    public class UserLevelService : BaseService<Nt.Model.Nt_UserLevel>
    {
        public List<ListItem> GetAvailableUserLevel(bool showHidden = true)
        {
            string sql = string.Empty;
            if (showHidden)
                sql = string.Format("Select ID,Name From [{0}] Where ID>={1} ", TableName, NtContext.Current.UserID);
            else
                sql = string.Format("Select ID,Name From [{0}] Where ID>={1} And Active=1 ", TableName, NtContext.Current.UserID);
            SqlDataReader rs = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
            var list = new List<ListItem>();
            while (rs.Read())
            {
                list.Add(new ListItem(rs[1].ToString(), rs[0].ToString()));
            }
            rs.Close();
            rs.Dispose();
            return list;
        }

        /// <summary>
        /// 判断指定的id的用户组级别是否属于超级管理员组
        /// </summary>
        /// <param name="userLevel">管理员组级别</param>
        /// <returns></returns>
        public bool IsAdmin(int userLevel)
        {
            return userLevel == 1;
        }

        public override void Delete(int id)
        {
            if (id == 1 || id == 2)
            {
                throw new Exception("超级管理员组或客户管理员组不准删除!");
            }
            else
            {
                base.Delete(id);
            }
        }

        public override void Delete(string ids)
        {
            int int_id = 0;
            if (!Int32.TryParse(ids, out int_id))
                throw new Exception("参数错误!");
            this.Delete(int_id);
        }

    }
}
