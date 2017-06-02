using Nt.DAL.Helper;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Nt.DAL
{
    public class CommonFactory
    {
        #region get local
        /// <summary>
        /// get list from db
        /// </summary>
        /// <param name="table">table's name</param>
        /// <param name="filter">condition to filterate data</param>
        /// <param name="orderby">order</param>
        /// <returns>DataTable</returns>
        public static DataTable GetList(string table, string filter, string orderby)
        {
            string sql = string.Format("Select * From [{0}]", table);
            if (!string.IsNullOrEmpty(filter))
            {
                if (!string.IsNullOrEmpty(orderby))
                    sql = string.Format("Select * From [{0}] Where {1} Order by {2} ", table, filter, orderby);
                else
                    sql = string.Format("Select * From [{0}] Where {1} ", table, filter);
            }
            else
            {
                if (!string.IsNullOrEmpty(orderby))
                    sql = string.Format("Select * From [{0}] Order by {1} ", table, orderby);
                else
                    sql = string.Format("Select * From [{0}] ", table);
            }
            DataTable data = SqlHelper.ExecuteDataset(sql).Tables[0];
            return data;
        }

        /// <summary>
        /// get all data
        /// </summary>
        /// <param name="table">table's name</param>
        /// <returns></returns>
        public static DataTable GetList(string table)
        {
            string sql = string.Format("Select * From [{0}]", table);
            DataTable data = SqlHelper.ExecuteDataset(sql).Tables[0];
            return data;
        }

        /// <summary>
        /// get data by filter
        /// </summary>
        /// <param name="table">table's name</param>
        /// <param name="filter">filter</param>
        /// <returns></returns>
        public static DataTable GetList(string table, string filter)
        {
            string sql = string.Format("Select * From [{0}] Where {1} ", table, filter);
            DataTable data = SqlHelper.ExecuteDataset(sql).Tables[0];
            return data;
        }

        #region get pagerized list from db
        /// <summary>
        /// get pagerized list from db
        /// </summary>
        /// <param name="table">table's name</param>
        /// <param name="filter">condition to filterate data</param>
        /// <param name="orderby">order</param>
        /// <param name="pageindex">index of page</param>
        /// <param name="pagesize">the  number of record displaying on each page</param>
        /// <returns>DataTable</returns>
        public static DataTable GetList(string table, string filter, string orderby, int pageindex, int pagesize)
        {
            string sql = string.Empty;
            if (pageindex == 1)
            {
                sql = string.Format("Select Top {0} * From [{1}] Where {2} Order by {3},ID desc", pagesize, table, filter, orderby);
            }
            else
            {
                string subSelect = string.Format("Select Top {0} ID From [{1}] Where {2} Order By {3},ID desc",
                    (pageindex - 1) * pagesize, table, filter, orderby);
                sql = string.Format("Select Top {0} * From [{1}] Where {2} And ID Not in ({3}) Order by {4},ID desc", pagesize, table, filter, subSelect, orderby);
            }
            DataTable data = SqlHelper.ExecuteDataset(sql).Tables[0];
            return data;
        }

        /// <summary>
        /// get pagerized list from db
        /// </summary>
        /// <param name="table">table's name</param>
        /// <param name="orderby">order</param>
        /// <param name="pageindex">index of page</param>
        /// <param name="pagesize">the  number of record displaying on each page</param>
        /// <returns>DataTable</returns>
        public static DataTable GetList(string table, string orderby, int pageindex, int pagesize)
        {
            string sql = string.Empty;
            if (pageindex == 1)
            {
                sql = string.Format("Select Top {0} * From [{1}] Order by {2},ID desc ", pagesize, table, orderby);
            }
            else
            {
                string subSelect = string.Format("Select Top {0} ID From [{1}] Order By {2},ID desc",
                    (pageindex - 1) * pagesize, table, orderby);
                sql = string.Format("Select Top {0} * From [{1}] Where ID Not in ({2}) Order by {3},ID desc",
                    pagesize, table, subSelect, orderby);
            }
            DataTable data = SqlHelper.ExecuteDataset(sql).Tables[0];
            return data;
        }

        /// <summary>
        ///  get pagerized list from db
        /// </summary>
        /// <param name="table">table's name</param>
        /// <param name="pageindex">index of current page</param>
        /// <param name="pagesize">the  number of record displaying on each page<</param>
        /// <param name="filter">condition to filterate data</param>
        /// <returns>DataTable</returns>
        public static DataTable GetList(string table, int pageindex, int pagesize, string filter)
        {
            string sql = string.Empty;
            if (pageindex == 1)
            {
                sql = string.Format("Select Top {0} * From [{1}] Where {2} Order by id desc ", pagesize, table, filter);
            }
            else
            {
                string subSelect = string.Format("Select Top {0} ID From [{1}] Where {2} Order by id desc",
                    (pageindex - 1) * pagesize, table, filter);
                sql = string.Format("Select Top {0} * From [{1}] Where {2} And ID Not in ({3}) Order by id desc",
                    pagesize, table, filter, subSelect);
            }
            DataTable data = SqlHelper.ExecuteDataset(sql).Tables[0];
            return data;
        }

        /// <summary>
        /// get pagerized list from db
        /// </summary>
        /// <param name="table">table's name</param>
        /// <param name="pageindex">index of current page</param>
        /// <param name="pagesize">the  number of record displaying on each page</param>
        /// <returns>DataTable</returns>
        public static DataTable GetList(string table, int pageindex, int pagesize)
        {
            string sql = string.Empty;
            if (pageindex == 1)
            {
                sql = string.Format("Select Top {0} * From [{1}] Order by id desc", pagesize, table);
            }
            else
            {
                string subSelect = string.Format("Select Top {0} ID From [{1}] Order by id desc",
                    (pageindex - 1) * pagesize, table);
                sql = string.Format("Select Top {0} * From [{1}] Where ID Not in ({2}) Order by id desc",
                    pagesize, table, subSelect);
            }
            DataTable data = SqlHelper.ExecuteDataset(sql).Tables[0];
            return data;
        }

        #endregion

        #endregion

        /// <summary>
        /// get the number  of data from db with filter
        /// </summary>
        /// <param name="table">table's name</param>
        /// <param name="filter">condition to filterate the data</param>
        /// <returns>the number of query result</returns>
        public static int GetRecordCount(string table, string filter)
        {
            string sql = string.Format("Select Count(*) From [{0}]", table);
            if (!string.IsNullOrEmpty(filter))
            {
                sql = string.Format("Select Count(*) From [{0}] Where {1}", table, filter);
            }
            return Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
        }

        #region update or insert

        public static void Update(BaseViewModel m)
        {
            Update(m, null);
        }

        /// <summary>
        /// use this function to update a model which is BaseViewModel
        /// </summary>
        /// <param name="m">model</param>
        /// <param name="exclude">fields should not be updated</param>
        public static void Update(BaseViewModel m, string[] exclude)
        {
            var id = m.Id;
            var properties = m.GetType().GetProperties();
            string table = m.GetType().Name;
            StringBuilder sql = new StringBuilder(string.Format("UPDATE [{0}] SET"
                , table));
            SqlParameter[] parameters = new SqlParameter[properties.Length];
            int i = 0;
            foreach (var item in properties)
            {
                TypeCode code = Type.GetTypeCode(item.PropertyType);
                if (item.Name == "Id")
                    continue;

                //the exclude means these specified fields should not be updated
                //so we continue
                if (exclude != null
                    && exclude.Length > 0
                    && exclude.Any(x =>
                        x.Equals(item.Name, StringComparison.OrdinalIgnoreCase)))
                    continue;

                if (i == 0)
                    sql.AppendFormat(" [{0}]=@{0}", item.Name);
                else
                    sql.AppendFormat(",[{0}]=@{0}", item.Name);
                var value = item.GetValue(m, null);
                if (value == null)
                {
                    parameters[i++] = new SqlParameter("@" + item.Name,
                        CommonHelper.GetDefaultValueByTypeCode(code));
                }
                else
                    parameters[i++] = new SqlParameter("@" + item.Name, value);
            }
            sql.AppendFormat(" Where [Id]={0}", id);
            SqlHelper.ExecuteNonQuery(SqlHelper.GetConnSting(), CommandType.Text, sql.ToString(), parameters);
        }

        /// <summary>
        /// use this function to insert a model to db
        /// this model should be BaseViewModel or its subclass
        /// </summary>
        /// <param name="m">this model</param>
        /// <returns>this new id it will be</returns>
        public static int Insert(BaseViewModel m)
        {
            var properties = m.GetType().GetProperties();
            string table = m.GetType().Name;
            StringBuilder sql = new StringBuilder(string.Format("INSERT INTO [{0}]("
                , table));
            StringBuilder sql_values = new StringBuilder();
            SqlParameter[] parameters = new SqlParameter[properties.Length];
            int i = 0;
            foreach (var item in properties)
            {
                TypeCode code = Type.GetTypeCode(item.PropertyType);
                if (item.Name == "Id")
                    continue;
                if (i == 0)
                {
                    sql.AppendFormat(" [{0}]", item.Name);
                    sql_values.Append("@" + item.Name);
                }
                else
                {
                    sql.AppendFormat(",[{0}]", item.Name);
                    sql_values.Append(",@" + item.Name);
                }
                var value = item.GetValue(m, null);
                if (value == null)
                {
                    parameters[i++] = new SqlParameter("@" + item.Name,
                        CommonHelper.GetDefaultValueByTypeCode(code));
                }
                else
                    parameters[i++] = new SqlParameter("@" + item.Name, value);
            }
            sql.AppendFormat(")Values({0})", sql_values.ToString());
            sql.Append("\r\n Select @@IDENTITY");
            var result = SqlHelper.ExecuteScalar(SqlHelper.GetConnection(),
                CommandType.Text, sql.ToString(), parameters);
            if (result == null)
                throw new Exception("result == null");
            else
                return Convert.ToInt32(result);
        }

        #endregion

        #region Delete

        /// <summary>
        /// delete one record
        /// </summary>
        /// <param name="table">table's name</param>
        /// <param name="id">specified id which you want to delete</param>
        /// <returns>the number of rows which is deleted successfully</returns>
        public static int Delete(string table, int id)
        {
            string sql = string.Format("delete From [{0}] Where id={1}", table, id);
            return SqlHelper.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// delete many record
        /// </summary>
        /// <param name="table">table's name</param>
        /// <param name="ids">specified ids(separated by comma) which you want to delete</param>
        /// <returns>the number of rows which is deleted successfully</returns>
        public static int Delete(string table, string ids)
        {
            string sql = string.Format("Delete From [{0}] Where id in ({1}) ", table, ids);
            return SqlHelper.ExecuteNonQuery(sql);
        }

        public static int Delete(string table, string filter, bool byFilter)
        {
            if (!byFilter)
                Delete(table, filter);
            string sql = string.Format("Delete From [{0}] Where {1} ", table, filter);
            return SqlHelper.ExecuteNonQuery(sql);
        }

        #endregion

        /// <summary>
        /// get on model by id
        /// </summary>
        /// <typeparam name="M">type which is BaseViewModel</typeparam>
        /// <param name="id">the specified id</param>
        /// <returns>Model</returns>
        public static M GetById<M>(int id)
            where M : BaseModel, new()
        {
            using (SqlConnection conn = SqlHelper.GetConnection())
            {
                conn.Open();
                string table = typeof(M).Name;
                var properties = typeof(M).GetProperties();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = string.Format("Select * From {0} Where Id={1} ", table, id);
                SqlDataReader rs = cmd.ExecuteReader();
                M m = null;
                if (rs.Read())
                {
                    m = new M();
                    foreach (var item in properties)
                        CommonHelper.SetValueToProp(item, m, rs[item.Name]);
                }
                return m;
            }
        }

        public static M GetFirstOrDefault<M>()
            where M : BaseModel, new()
        {
            return GetFirstOrDefault<M>(null, null);
        }

        /// <summary>
        /// 获取默认的第一个
        /// </summary>
        /// <typeparam name="M"></typeparam>
        /// <param name="orderby">排序</param>
        /// <returns></returns>
        public static M GetFirstOrDefault<M>(string filter, string orderby)
            where M : BaseModel, new()
        {
            using (SqlConnection conn = SqlHelper.GetConnection())
            {
                conn.Open();
                string table = typeof(M).Name;
                var properties = typeof(M).GetProperties();
                SqlCommand cmd = conn.CreateCommand();
                string sql = "";
                if (string.IsNullOrEmpty(orderby) && string.IsNullOrEmpty(filter))
                    sql = string.Format("Select * From {0} ", table);
                else
                {
                    if (string.IsNullOrEmpty(orderby))
                        sql = string.Format("Select * From {0} where {1}", table, filter);
                    else
                    {
                        if (string.IsNullOrEmpty(filter))
                            sql = string.Format("Select * From {0}  order by {1} ", table, orderby);
                        else
                            sql = string.Format("Select * From {0} where {2}  order by {1} ", table, orderby, filter);
                    }
                }
                cmd.CommandText = sql;
                SqlDataReader rs = cmd.ExecuteReader();
                M m = null;
                if (rs.Read())
                {
                    m = new M();
                    foreach (var item in properties)
                        CommonHelper.SetValueToProp(item, m, rs[item.Name]);
                }
                return m;
            }
        }
    }
}
