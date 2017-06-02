using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nt.Model;
using System.Data.SqlClient;
using Nt.BLL.Helper;
using System.Data;
using Nt.DAL.Helper;

namespace Nt.BLL
{
    public class UserService : BaseService<Nt_User>
    {

        public override void Update(Nt_User m)
        {
            base.Update(m, new string[] { "Password", "AddUser" });
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="newPassword"></param>
        public void ChangePassword(int userID, string newPassword)
        {
            string sql = string.Format("Update [Nt_User] Set Password='{0}' Where Id={1}", newPassword, userID);
            SqlHelper.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 寻找userid的所有隶属用户的id
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string GetAllSubUser(int userID)
        {
            var all = GetList();
            StringBuilder holder = new StringBuilder();
            FindSubUser(all, userID, holder);
            if (holder.Length > 1)
                holder.Remove(0, 1);//去掉前面的一个逗号
            return holder.ToString();
        }

        void FindSubUser(DataTable all, object userID, StringBuilder holder)
        {
            holder.Append("," + userID);
            foreach (DataRow r in all.Select("AddUser=" + userID))
            {
                FindSubUser(all, r["ID"], holder);
            }
        }

        /// <summary>
        /// 登录检查
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool Login(string userName, string password, out int userID)
        {
            var dxname = userName.ToUpper();
            object raw = SqlHelper.ExecuteScalar(
                string.Format("Select ID From [{0}] Where (Upper(UserName))='{1}' And [Password]='{2}' ",
                TableName, dxname, password));
            userID = NtContext.IMPOSSIBLE_ID;
            if (raw == null)
                return false;
            userID = Convert.ToInt32(raw);
            return true;
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
                string.Format("Select Count(0) From [{0}] Where (Upper(UserName))<>'{2}' And (Upper(UserName))='{1}' ",
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
                string.Format("Select Count(0) From [{0}] Where (Upper(UserName))='{1}'",
                TableName, dxuserName)));
            return one > 0;
        }
        
        public override void Delete(int id)
        {
            if (id == 1 || id == 2)
            {
                throw new Exception("超级管理员或Admin不允许删除!");
            }
            base.Delete(id);
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
