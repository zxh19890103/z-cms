using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Nt.Model;
using System.Data.SqlClient;
using Nt.DAL.Helper;
using System.Data;

namespace Nt.BLL
{
    public class MemberRoleService : BaseService<Nt_MemberRole>
    {
        /// <summary>
        /// 获取可用的会员角色(好像需要对名称进行本地化)
        /// </summary>
        /// <returns></returns>
        public List<ListItem> GetAvailableMemberRoles()
        {
            SqlDataReader rs = SqlHelper.ExecuteReader(SqlHelper.GetConnection(),
               CommandType.Text,
               string.Format("Select Id,Name From [{0}] Where Active=1", TableName));
            var list = new List<ListItem>();
            while (rs.Read())
            {
                list.Add(new ListItem(rs[1].ToString(), rs[0].ToString()));
            }
            rs.Close();
            rs.Dispose();
            return list;
        }

    }
}
