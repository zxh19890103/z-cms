using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nt.Model;
using Nt.BLL.Helper;
using Nt.DAL.Helper;
using System.Data.SqlClient;
using System.Data;
using Nt.DAL;

namespace Nt.BLL
{
    public class PermissionService : BaseService<Nt_Permission>
    {

        DataTable _permissionsByUserLevel = null;

        public DataTable PermissionsByUserLevel
        {
            get
            {
                if (_permissionsByUserLevel == null)
                {
                    _permissionsByUserLevel = CommonFactory.GetList("View_UserPermission",
                        string.Format("UserLevel_Id={0}", NtContext.Current.CurrentUser.UserLevel_Id));
                }
                return _permissionsByUserLevel;
            }
        }

        /// <summary>
        /// 判断指定的许可是否被授权给当前用户
        /// </summary>
        /// <param name="systemName">指定的许可的系统名</param>
        /// <returns>Bool</returns>
        public bool Authorize(string systemName)
        {
            if (NtContext.Current.IsAdministrator)
                return true;
            int b = Convert.ToInt32(
                SqlHelper.ExecuteScalar(string.Format
            ("Select Count(0) From View_UserPermission Where Upper(SystemName)='{0}'", systemName.ToUpper())));
            return b > 0;
        }

        public bool Authorize(PermissionRecord record)
        {
            if (NtContext.Current.IsAdministrator)
                return true;
            if (record == null)
                return false;
            int b = Convert.ToInt32(SqlHelper.ExecuteScalar(string.Format(
                "Select Count(0) From [{0}] Where ID={1}", TableName, record.Id)));
            return b > 0;
        }

        public int[] GetPermissionIdsByUserLevel(int userLevel)
        {
            string sql = string.Format(
               "Select Id From View_UserPermission Where UserLevel_Id={0}",
               userLevel);
            SqlDataReader rs = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
            string ids = string.Empty;
            while (rs.Read())
            {
                if (ids == string.Empty)
                    ids += rs[0].ToString();
                else
                    ids += "," + rs[0].ToString();
            }
            if (ids == string.Empty)
                return new int[0];
            return BLL.Helper.CommonHelper.GetInt32ArrayFromStringWithComma(ids);
        }

        public int SaveAuthorized(int level, string permissions)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("Delete From [Nt_Permission_UserLevel_Mapping] Where [UserLevel_Id]={0}\r\n", level);
            if (string.IsNullOrEmpty(permissions))
                return 0;
            foreach (var i in permissions.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                sql.AppendFormat("INSERT INTO [Nt_Permission_UserLevel_Mapping] (Permission_Id,UserLevel_Id)VALUES({0},{1})\r\n", i, level);
            return SqlHelper.ExecuteNonQuery(sql.ToString());
        }
    }
}
