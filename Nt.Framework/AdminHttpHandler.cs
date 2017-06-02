using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nt.BLL;
using Nt.BLL.Extension;
using System.Web;
using System.Collections;
using LitJson;
using Nt.Model;
using Nt.DAL;

namespace Nt.Framework
{
    public class AdminHttpHandler<M> : IHttpHandler
        where M : BaseViewModel, new()
    {
        protected BaseService<M> _service;

        protected Hashtable responseJson;

        public const string DELETE = "DELETE";//删
        public const string GET = "GET";//查
        public const string PUT = "PUT";//增
        public const string POST = "POST";//改

        public const int IMPOSSIBLE_ID = 0;

        public const string METHOD_QUERY_NAME = "method";
        public const string ID_QUERY_NAME = "Id";

        private HttpRequest request;
        private HttpResponse response;

        protected HttpRequest Request { get { return request; } }
        protected HttpResponse Response { get { return response; } }

        public void ProcessRequest(HttpContext context)
        {
            request = context.Request;
            response = context.Response;
            responseJson = new Hashtable();
            _service = new BaseService<M>();
            Handle();
        }

        /// <summary>
        /// 需要进行的操作
        /// </summary>
        public string Method
        {
            get
            {
                return request[METHOD_QUERY_NAME];
            }
        }

        string _tableName = string.Empty;
        public string TableName
        {
            get
            {
                if (_tableName == string.Empty)
                    _tableName = typeof(M).Name;
                return _tableName;
            }
        }

        /// <summary>
        /// id
        /// </summary>
        private int _id = NtContext.IMPOSSIBLE_ID;
        public int NtID
        {
            get
            {
                if (_id == NtContext.IMPOSSIBLE_ID)
                {
                    if (!Int32.TryParse(request[ID_QUERY_NAME], out _id))
                        _id = NtContext.IMPOSSIBLE_ID;
                }
                return _id;
            }
        }

        void Handle()
        {
            Success("");
            switch (Method)
            {
                case PUT:
                    Insert();
                    break;
                case DELETE:
                    Del();
                    break;
                case GET:
                    Get();
                    break;
                case POST:
                    Save();
                    break;
                default:
                    Error("没有进行任何操作!");
                    break;
            }
            Response.Write(JsonMapper.ToJson(responseJson));
            Response.End();
        }

        protected virtual void Get()
        {
            if (NtID == NtContext.IMPOSSIBLE_ID)
            {
                Error("参数错误!");
                return;
            }
            var m = CommonFactory.GetById<M>(NtID);
            if (m != null)
            {
                responseJson["model"] = (new NtJson(m)).Json;
            }
            else
                Error("没有发现您所查询记录!");
        }

        protected virtual void Save()
        {
            if (NtID == NtContext.IMPOSSIBLE_ID)
            {
                Error("参数错误!");
                return;
            }
            var m = new M();
            m.InitDataFromPage();
            CommonFactory.Update(m);
            Success("修改成功!");
        }

        protected virtual void Insert()
        {
            var m = new M();
            m.InitDataFromPage();
            var id = CommonFactory.Insert(m);
            responseJson["Id"] = id;
            Success("添加成功!");
        }

        protected virtual void Del()
        {
            if (NtID == NtContext.IMPOSSIBLE_ID)
            {
                Error("参数错误!");
                return;
            }
            CommonFactory.Delete(TableName, NtID);
            Success("删除成功!");
        }

        #region error success

        protected void Error(string message)
        {
            responseJson["error"] = 1;
            responseJson["message"] = message;
        }

        protected void Success(string message)
        {
            responseJson["error"] = 0;
            responseJson["message"] = message;
        }

        #endregion

        public bool IsReusable { get { return true; } }
    }
}
