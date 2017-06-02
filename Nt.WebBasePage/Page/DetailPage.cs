using Nt.BLL;
using Nt.BLL.Helper;
using Nt.DAL.Helper;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace Nt.Web
{
    /// <summary>
    /// DetailPage 的摘要说明
    /// </summary>
    public class DetailPage<M> : BasePage
        where M : BaseViewModel, new()
    {
        #region props

        int _ntID = 0;
        /// <summary>
        /// id
        /// </summary>
        public int NtID
        {
            get
            {
                if (_ntID == 0)
                {
                    if (!Int32.TryParse(Request.QueryString["ID"], out _ntID))
                    {
                        GotoErrorPage("参数错误!");
                    }
                }
                return _ntID;
            }
            set { _ntID = value; }
        }

        M _m;
        /// <summary>
        /// 实体
        /// </summary>
        public M Model
        {
            get
            {
                return _m;
            }
        }

        protected BaseService<M> _service;
        /// <summary>
        /// 服务工具
        /// </summary>
        public BaseService<M> Service
        {
            get
            {
                if (_service == null)
                {
                    _service = NtEngine.GetService<M>();
                    _service.LanguageID = NtConfig.CurrentLanguage;
                }
                return _service;
            }
            set { _service = value; }
        }

        string _tableName;
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get
            {
                if (string.IsNullOrEmpty(_tableName))
                {
                    if (typeof(M).GetInterface("IView") != null)
                    {
                        _tableName = "Nt_" + typeof(M).Name.Substring(5);
                    }
                    else
                    {
                        _tableName = _service.TableName;
                    }
                }
                return _tableName;
            }
        }

        string _nextTitle = "No More...";
        /// <summary>
        /// 下一篇title
        /// </summary>
        public virtual string NextTitle { get { return _nextTitle; } set { _nextTitle = value; } }
        int _nextId = 0;
        /// <summary>
        /// 下一篇id
        /// </summary>
        public virtual int NextID { get { return _nextId; } set { _nextId = value; } }

        string _preTitle = "No More...";
        /// <summary>
        /// 上一篇title
        /// </summary>
        public virtual string PreTitle { get { return _preTitle; } set { _preTitle = value; } }

        int _preId = 0;
        /// <summary>
        /// 上一篇id
        /// </summary>
        public virtual int PreID { get { return _preId; } set { _preId = value; } }

        /// <summary>
        /// 相应列表页的页面类型
        /// </summary>
        public int ListPageType
        {
            get
            {
                switch (PageType)
                {
                    case NtConfig.DOWNLOAD:
                        return NtConfig.DOWNLOAD_LIST;
                    case NtConfig.NEWS:
                        return NtConfig.NEWS_LIST;
                    case NtConfig.PRODUCT:
                        return NtConfig.PRODUCT_LIST;
                    case NtConfig.COURSE:
                        return NtConfig.COURSE_LIST;
                    case NtConfig.JOB:
                        return NtConfig.JOB_LIST;
                    default:
                        throw new Exception("没有此页面类型!");
                }
            }
        }

        public override string HtmlPageUrl
        {
            get
            {
                return string.Format("/html/{0}/{1}.html", PageType, NtID);
            }
        }

        #endregion

        #region 上一页和下一页

        internal void CalculateNextAndPrevious(string listOrderby, string filter)
        {
            string sql = "select id,title from " +
                 _service.TableName +
                 (string.IsNullOrEmpty(filter) ? "" : " where " + filter) +
                 (string.IsNullOrEmpty(listOrderby) ? "" : " order by " + listOrderby);
            using (SqlDataReader r = SqlHelper.ExecuteReader(
                SqlHelper.GetConnection(),
                CommandType.Text,
               sql
                ))
            {
                int id = 0;
                string title = string.Empty;
                while (r.Read())
                {
                    id = r.GetInt32(0);
                    title = r.GetString(1);
                    if (NtID == id)
                    {
                        if (r.Read())
                        {
                            NextID = r.GetInt32(0);
                            NextTitle = r.GetString(1);
                        }
                        break;
                    }
                    PreID = id;
                    PreTitle = title;
                }
            }
        }

        #endregion

        #region methods
        /// <summary>
        /// 点击率增加1
        /// </summary>
        /// <param name="table">表名</param>
        public void Rating()
        {
            SqlHelper.ExecuteNonQuery(string.Format
            ("Update [{1}] Set [ClickRate]=[ClickRate]+1 Where [ID]={0}", NtID, TableName));
        }

        /// <summary>
        /// 处理seo
        /// </summary>
        public virtual void Seo() { }

        public virtual void TryGetModel()
        {
            if (_m == null)
            {
                _m = Service.GetByID(NtID);
                if (_m == null)
                {
                    GotoErrorPage("未发现指定记录!");
                }
                Seo();
            }
        }

        #endregion
    }
}