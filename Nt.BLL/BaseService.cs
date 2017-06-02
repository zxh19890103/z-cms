using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using Nt.Model;
using Nt.BLL.Helper;
using Nt.BLL.Extension;
using Nt.DAL.Helper;
using Nt.DAL;

namespace Nt.BLL
{
    public class BaseService<M> : IService
        where M : global::Nt.Model.BaseViewModel, new()
    {

        #region StringBuilder For Sql
        protected StringBuilder _sql = null;
        #endregion

        #region octor

        public BaseService()
        {
            _sql = new StringBuilder();
        }
        #endregion

        #region Props

        private string _tableName = string.Empty;
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get
            {
                if (string.IsNullOrEmpty(_tableName))
                {
                    _tableName = typeof(M).Name;
                }
                return _tableName;
            }
        }

        public virtual string BaseFilter
        {
            get { return string.Empty; }
        }

        bool _isLocale;
        public bool IsLocale
        {
            get
            {
                var t = typeof(M).GetInterface("ILocaleModel");
                _isLocale = t != null;
                return _isLocale;
            }
        }

        int _languageID = 0;
        /// <summary>
        /// 使用的语言版本id
        /// </summary>
        public int LanguageID
        {
            get
            {
                if (_languageID == 0)
                    return NtContext.Current.LanguageID;
                else
                    return _languageID;
            }
            set { _languageID = value; }
        }

        #endregion

        #region Get Local

        protected string GetFilter(params string[] where)
        {
            string filter = string.Empty;
            if (BaseFilter != string.Empty)
                filter += BaseFilter;
            if (IsLocale)
            {
                filter += string.IsNullOrEmpty(filter) ? string.Empty : " And ";
                filter += " Language_Id=" + LanguageID;// this model is localable
            }
            if (where != null && where.Length > 0)
            {
                foreach (var item in where)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        filter += string.IsNullOrEmpty(filter) ? string.Empty : " And ";
                        filter += item;
                    }
                }
            }
            return filter;
        }

        /// <summary>
        /// 获取一个表中某记录某字段的值
        /// </summary>
        /// <param name="id">记录的id值</param>
        /// <param name="fieldName">字段名(t)</param>
        /// <returns>object</returns>
        public virtual object GetScalar(int id, string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName))
                return null;
            string filter = "id=" + id;
            string sql = string.Format("Select [{1}] From [{0}] Where {2}", TableName, fieldName, filter);
            return SqlHelper.ExecuteScalar(sql);
        }


        public virtual M GetByID(int id)
        {
            M m = CommonFactory.GetById<M>(id);
            return m;
        }


        /// <summary>
        /// 获取记录的条数  locale
        /// </summary>
        /// <returns>int</returns>
        public virtual int GetRecordsCount()
        {
            return CommonFactory.GetRecordCount(TableName, GetFilter());
        }

        public virtual int GetRecordsCount(string filter)
        {
            string _filter = GetFilter(filter);
            return CommonFactory.GetRecordCount(TableName, _filter);
        }

        public virtual DataTable GetList()
        {
            string _filter = GetFilter();
            if (string.IsNullOrEmpty(_filter))
                return CommonFactory.GetList(TableName);
            else
                return CommonFactory.GetList(TableName, _filter);
        }

        public virtual DataTable GetList(string filter)
        {
            string _filter = GetFilter(filter);
            return CommonFactory.GetList(TableName, _filter);
        }

        public virtual DataTable GetList(string filter, string orderby)
        {
            string _filter = GetFilter(filter);
            return CommonFactory.GetList(TableName, _filter, orderby);
        }

        public virtual DataTable GetList(int pageIndex, int pageSize)
        {
            string _filter = GetFilter();
            if (string.IsNullOrEmpty(_filter))
                return CommonFactory.GetList(TableName, pageIndex, pageSize);
            return CommonFactory.GetList(TableName, pageIndex, pageSize, _filter);
        }

        public virtual DataTable GetList(int pageIndex, int pageSize, string filter)
        {
            string _filter = GetFilter(filter);
            return CommonFactory.GetList(TableName, pageIndex, pageSize, _filter);
        }

        public virtual DataTable GetList(string orderby, int pageIndex, int pageSize)
        {
            string _filter = GetFilter();
            if (string.IsNullOrEmpty(_filter))
                return CommonFactory.GetList(TableName, orderby, pageIndex, pageSize);
            return CommonFactory.GetList(TableName, _filter, orderby, pageIndex, pageSize);
        }

        public virtual DataTable GetList(int pageIndex, int pageSize, string orderby, string filter)
        {
            string _filter = GetFilter(filter);
            return CommonFactory.GetList(TableName, _filter, orderby, pageIndex, pageSize);
        }

        #endregion

        #region Update Or Insert

        public virtual void Update(M m)
        {
            this.Update(m, null);
        }

        /// <summary>
        ///根据id 更新一条记录
        /// </summary>
        /// <param name="id">ID</param>
        public virtual void Update(M m, string[] exclude)
        {
            CommonFactory.Update(m, exclude);
        }

        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <returns></returns>
        public virtual int Insert(M m)
        {
            return CommonFactory.Insert(m);
        }

        #endregion

        #region Execute Sql
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public virtual int ExecuteNonQuery(string sql, params SqlParameter[] parameters)
        {
            return SqlHelper.ExecuteNonQuery(SqlHelper.GetConnSting(), CommandType.Text, sql, parameters);
        }

        /// <summary>
        /// 返回结果集中的第一行第一列
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public virtual object ExecuteScalar(string sql, params SqlParameter[] parameters)
        {
            return SqlHelper.ExecuteScalar(SqlHelper.GetConnSting(), CommandType.Text, sql, parameters);
        }
        #endregion

        #region ClearBuilder ExecuteSql
        /// <summary>
        /// 清空SQL
        /// </summary>
        private void ClearBuilder()
        {
            _sql.Remove(0, _sql.Length);
        }

        /// <summary>
        /// 执行语句
        /// </summary>
        public virtual int ExecuteSql()
        {
            var sql = _sql.ToString();
            if (_sql == null || _sql.Length < 1)
                return 0;
            int n = SqlHelper.ExecuteNonQuery(sql);
            ClearBuilder();
            return n;
        }

        #endregion

        #region Delete
        /// <summary>
        /// 根据id删除一个记录
        /// </summary>
        /// <param name="id">键值id</param>
        /// <returns>如果删除成功则返回true，否则返回false</returns>
        public virtual void Delete(int id)
        {
            if (id == 0)
                throw new Exception("参数错误");
            _sql.AppendFormat("Delete From [{0}] Where [Id]={1}\r\n", TableName, id);
            ExecuteSql();
        }

        /// <summary>
        /// 根据ids删除若干纪录
        /// </summary>
        /// <param name="ids">形似1,2,3,4</param>
        public virtual void Delete(string ids)
        {
            if (string.IsNullOrEmpty(ids))
                throw new Exception("没有可操作的项");
            _sql.AppendFormat("Delete From [{0}] Where [Id] in ({1})\r\n", TableName, ids);
            ExecuteSql();
        }

        #endregion

        #region Ajax ReOrder Set Bool

        public virtual int ReOrder(string ids, string orders)
        {
            if (string.IsNullOrEmpty(ids))
                throw new Exception("没有可操作的项.");
            string[] arr_ids = ids.Split(',');
            string[] arr_orders = orders.Split(',');
            if (arr_ids.Length != arr_orders.Length)
                throw new Exception("指定的id和排序值数量上不匹配.");
            StringBuilder sql = new StringBuilder();
            for (var i = 0; i < arr_ids.Length; i++)
                sql.AppendFormat("Update [{2}] Set [DisplayOrder]={0} Where [Id]={1}\r\n",
                    arr_orders[i], arr_ids[i], TableName);
            return SqlHelper.ExecuteNonQuery(sql.ToString());
        }

        public virtual int SetTop(string ids)
        {
            string sql = string.Format("Update [{1}] Set [SetTop]=1-[SetTop] Where [Id] In ({0})",
                ids, TableName);
            return SqlHelper.ExecuteNonQuery(sql);
        }

        public virtual int SetRecommend(string ids)
        {
            string sql = string.Format("Update [{1}] Set [Recommended]=1-[Recommended] Where [Id] In ({0})",
                ids, TableName);
            return SqlHelper.ExecuteNonQuery(sql);
        }

        public virtual int SetDisplay(string ids)
        {
            string sql = string.Format("Update [{1}] Set [Display]=1-[Display] Where [Id] In ({0})",
                ids, TableName);
            return SqlHelper.ExecuteNonQuery(sql);
        }

        public virtual int SetHot(string ids)
        {
            string sql = string.Format("Update [{1}] Set [Hot]=1-[Hot] Where [Id] In ({0})",
                ids, TableName);
            return SqlHelper.ExecuteNonQuery(sql);
        }

        public virtual int SetActive(string ids)
        {
            string sql = string.Format("Update [{1}] Set [Active]=1-[Active] Where [Id] In ({0})",
                ids, TableName);
            return SqlHelper.ExecuteNonQuery(sql);
        }

        public virtual int SetPublished(string ids)
        {
            string sql = string.Format("Update [{1}] Set [Published]=1-[Published] Where [Id] In ({0})",
                ids, TableName);
            return SqlHelper.ExecuteNonQuery(sql);
        }

        #endregion


        public void ExecuteScriptOnFile(string filename)
        {
            var phy_filename = WebHelper.MapPath(filename);
            if (!File.Exists(phy_filename))
                throw new Exception(string.Format("没有发现路径为{0}的脚本文件！", filename));
            StreamReader reader = null;
            try
            {
                reader = File.OpenText(phy_filename);
                string line = string.Empty;
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine().Trim();
                    if (line.ToUpper() == "GO")
                    {
                        ExecuteSql();
                    }
                    else
                    {
                        _sql.Append(line);
                        _sql.Append("\r\n");
                    }
                }
                ExecuteSql();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                reader.Close();
                reader.Dispose();
            }
        }

    }
}
