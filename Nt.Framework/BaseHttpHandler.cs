using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using LitJson;
using Nt.BLL;

namespace Nt.Framework
{
    public class BaseHttpHandler : IHttpHandler
    {
        private Hashtable responseJson;
        private HttpRequest request;
        private HttpResponse response;

        protected HttpRequest Request { get { return request; } }
        protected HttpResponse Response { get { return response; } }
        protected Hashtable ResponseJson { get { return responseJson; } }
        
        public int SinglePictureThumnailSize
        {
            get
            {
                return Convert.ToInt32(
                    global::System.Configuration.ConfigurationManager.AppSettings["admin-uc-picture-thumbnailSize"]);
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            request = context.Request;
            response = context.Response;
            responseJson = new Hashtable();
            Handle();
            Response.Write(JsonMapper.ToJson(responseJson));
            Response.End();
        }

        protected virtual void Handle() { }

        #region error success

        protected void Error(string message)
        {
            responseJson["error"] = 1;
            responseJson["message"] = message;
            Response.Write(JsonMapper.ToJson(responseJson));
            Response.End();
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
