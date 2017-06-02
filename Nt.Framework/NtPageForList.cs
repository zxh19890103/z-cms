using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nt.BLL;
using Nt.BLL.Caching;
using Nt.BLL.Helper;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Services;
using System.Data;
using Nt.Model;
using Nt.DAL.Helper;

namespace Nt.Framework
{
    public class NtPageForList<M> : NtPage, INtPageForList
         where M : BaseViewModel, new()
    {
        #region Service
        protected BaseService<M> _service = null;
        #endregion

        #region Props

        NtPager _pager = null;
        /// <summary>
        /// Pager
        /// </summary>
        public NtPager Pager
        {
            get { return _pager; }
        }

        protected DataTable _dataSource = null;
        public DataTable DataSource
        {
            get { return _dataSource; }
            set { _dataSource = value; }
        }

        protected bool _needPagerize = false;
        /// <summary>
        /// 是否需要分页
        /// </summary>
        public bool NeedPagerize
        {
            get { return _needPagerize; }
            set
            {
                if (value && _pager == null)
                    _pager = new NtPager();
                _needPagerize = value;
            }
        }

        Repeater _repeater = null;
        public Repeater Repeater
        {
            get { return _repeater; }
            set { _repeater = value; }
        }
        
        #endregion

        #region WebService ReOrder
        /// <summary>
        /// 重新排序
        /// </summary>
        /// <param name="ids">操作的对象的id</param>
        /// <param name="orders">序号</param>
        /// <returns></returns>
        [WebMethod]
        public static string ReOrder(string ids, string orders)
        {
            LitJson.JsonData json = new LitJson.JsonData();
            if (string.IsNullOrEmpty(ids))
            {
                json["error"] = 1;
                json["message"] = "没有可操作的项!";
                return LitJson.JsonMapper.ToJson(json);
            }
            string tab = typeof(M).Name;
            string[] arr_ids = ids.Split(',');
            string[] arr_orders = orders.Split(',');
            string sqlbuilder = string.Empty;
            for (int i = 0; i < arr_ids.Length; i++)
            {
                sqlbuilder += string.Format("Update {0} Set DisplayOrder={1} Where Id={2}\r\n",
                    tab, arr_orders[i], arr_ids[i]);
            }
            if (sqlbuilder != "")
                SqlHelper.ExecuteNonQuery(sqlbuilder);

            json["error"] = 0;
            json["message"] = "排序成功!";
            return LitJson.JsonMapper.ToJson(json);
        }

        #endregion

        #region Batch Delete

        [WebMethod]
        public static string BatchDelete(string ids)
        {
            LitJson.JsonData json = new LitJson.JsonData();
            if (string.IsNullOrEmpty(ids))
            {
                json["error"] = 1;
                json["message"] = "没有可操作的项!";
                return LitJson.JsonMapper.ToJson(json);
            }
            try
            {
                IService service = Nt.BLL.NtEngine.GetServiceJustForDel<M>();
                service.Delete(ids);
                json["error"] = 0;
                json["message"] = "删除成功";
                return LitJson.JsonMapper.ToJson(json);
            }
            catch (Exception ex)
            {
                json["error"] = 1;
                json["message"] = ex.Message;
                return LitJson.JsonMapper.ToJson(json);
            }

        }

        /// <summary>
        /// Batch 批量操作
        /// </summary>
        /// <param name="ids">操作的对象的id</param>
        /// <param name="type">0:settop,1:recommend,2:display,3:hot,4:active,5:published</param>
        /// <returns>json字符串(error,message)</returns>
        [WebMethod]
        public static string Batch(string ids, string type)
        {
            LitJson.JsonData json = new LitJson.JsonData();
            if (string.IsNullOrEmpty(ids))
            {
                json["error"] = 1;
                json["message"] = "没有可操作的项!";
                return LitJson.JsonMapper.ToJson(json);
            }
            string tab = typeof(M).Name;
            string sql = string.Empty;
            json["error"] = 0;
            switch (type)
            {
                case "0":
                    sql = string.Format("Update {0} Set SetTop=1-SetTop Where id in ({1})", tab, ids);
                    break;
                case "1":
                    sql = string.Format("Update {0} Set Recommended=1-Recommended Where id in ({1})", tab, ids);
                    break;
                case "2":
                    sql = string.Format("Update {0} Set display=1-display Where id in ({1})", tab, ids);
                    break;
                case "3":
                    sql = string.Format("Update {0} Set hot=1-hot Where id in ({1})", tab, ids);
                    break;
                case "4":
                    sql = string.Format("Update {0} Set active=1-active Where id in ({1})", tab, ids);
                    break;
                case "5":
                    sql = string.Format("Update {0} Set published=1-published Where id in ({1})", tab, ids);
                    break;
                default:
                    json["error"] = 1;
                    json["message"] = "参数错误";
                    break;
            }
            if (!string.IsNullOrEmpty(sql))
                SqlHelper.ExecuteNonQuery(sql);
            json["message"] = "设置成功";
            return LitJson.JsonMapper.ToJson(json);
        }


        #endregion

        #region Init
        /// <summary>
        /// 初始化列表页的数据
        /// </summary>
        protected virtual void InitPageData()
        {
            if (NeedPagerize && !Pager.Initialized)
                Pager.TotalRecords = _service.GetRecordsCount();
            if (_dataSource == null)
                _dataSource = NeedPagerize ? _service.GetList(Pager.PageIndex, Pager.PageSize)
                    : _service.GetList();
            if (Repeater == null)
            {
                Repeater = Page.Master.FindControl("CPH_Body").FindControl("XRepeater") as Repeater;//找到默认的Repeater控件
            }

            if (Repeater != null && _dataSource != null)
            {
                Repeater.DataSource = _dataSource;
                Repeater.DataBind();
            }
        }

        #endregion

        #region virtual function

        protected virtual void InitRequiredData() { }
        protected virtual void BeginInitPageData() { }
        protected virtual void EndInitPageData() { }

        #endregion

        /// <summary>
        /// 当不想使用默认的列表数据提供时，可以重写此方法，并且不执行父类的此方法
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitRequiredData();
            if (_service == null)
                _service = NtEngine.GetService<M>();
            BeginInitPageData();
            InitPageData();
            EndInitPageData();
        }

    }
}
