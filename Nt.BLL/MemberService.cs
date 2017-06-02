using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nt.Model;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using Nt.BLL.Helper;
using Nt.DAL.Helper;
using System.Data;

namespace Nt.BLL
{
    public class MemberService : BaseService<Nt_Member>
    {
        public override void Update(Nt_Member m)
        {
            base.Update(m, new string[] { "Password", "LastLoginIP", "AddUser", "LoginTimes" });
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="password"></param>
        public void ChangePassword(int memberID, string password)
        {
            string sql = string.Format("Update [{2}] Set [Password]='{0}' Where [Id]={1}"
                , password, memberID, TableName);
            SqlHelper.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 获取可用的会员
        /// </summary>
        /// <returns></returns>
        public List<ListItem> GetAvailableMembers()
        {
            SqlDataReader rs = SqlHelper.ExecuteReader(SqlHelper.GetConnection(),
               CommandType.Text,
               string.Format("Select Id,RealName From [{0}] Where Active=1", TableName));
            List<ListItem> list = new List<ListItem>();
            while (rs.Read())
            {
                list.Add(new ListItem(rs[1].ToString(), rs[0].ToString()));
            }
            rs.Close();
            rs.Dispose();
            return list;
        }

        /// <summary>
        /// 指示用户名是否存在
        /// </summary>
        /// <param name="userName">用户名不准修改</param>
        /// <returns></returns>
        public bool LoginNameExisting(string loginName, string oldone)
        {
            var dxuserName = loginName.ToUpper();
            var dxold = oldone.ToUpper();
            int one = Convert.ToInt32(
                SqlHelper.ExecuteScalar(
                string.Format("Select Count(0) From [{0}] Where (Upper(LoginName))<>'{2}' And (Upper(LoginName))='{1}'",
                TableName, dxuserName, dxold)));
            return one > 0;
        }

        /// <summary>
        /// 指示用户名是否存在
        /// </summary>
        /// <param name="userName">用户名不准修改</param>
        /// <returns></returns>
        public bool LoginNameExisting(string loginName)
        {
            var dxuserName = loginName.ToUpper().Replace("'", "''");
            int one = Convert.ToInt32(
                SqlHelper.ExecuteScalar(
                string.Format("Select Count(0) From [{0}] Where (Upper(LoginName))='{1}'",
                TableName, dxuserName)));
            return one > 0;
        }

        /// <summary>
        /// 登录检查
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool Login(string loginName, string password, out int mid)
        {
            var dxname = loginName.ToUpper();
            object raw = SqlHelper.ExecuteScalar(
                string.Format("Select ID From [{0}] Where (Upper(LoginName))='{1}' And [Password]='{2}' ",
                TableName, dxname, password));
            mid = NtContext.IMPOSSIBLE_ID;
            if (raw == null)
                return false;
            mid = Convert.ToInt32(raw);
            return true;
        }

    }

}
