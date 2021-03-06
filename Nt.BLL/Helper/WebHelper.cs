﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.SessionState;

namespace Nt.BLL.Helper
{
    public partial class WebHelper
    {
        /// <summary>
        /// Maps a virtual path to a physical disk path.
        /// </summary>
        /// <param name="path">The path to map. E.g. "~/bin"</param>
        /// <returns>The physical path. E.g. "c:\inetpub\wwwroot\bin"</returns>
        public static string MapPath(string path)
        {
            if (HostingEnvironment.IsHosted)
            {
                //hosted
                return HostingEnvironment.MapPath(path);
            }
            else
            {
                //not hosted. For example, run in unit tests
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                path = path.Replace("~/", "").TrimStart('/').Replace('/', '\\');
                return Path.Combine(baseDirectory, path);
            }
        }

        /// <summary>
        /// 获取当前ip
        /// </summary>
        /// <returns></returns>
        public static string GetIP()
        {
            string userIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (userIP == null || userIP == "")
            {
                userIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return userIP;
        }

        /// <summary>
        /// 当前域名
        /// </summary>
        public static string CurrentRootUrl
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    bool isDefaultPort = Request.Url.IsDefaultPort;
                    if (isDefaultPort)
                        return Request.Url.GetLeftPart(UriPartial.Scheme) + Request.Url.Host;
                    else
                        return Request.Url.GetLeftPart(UriPartial.Scheme) + Request.Url.Host + ":" + Request.Url.Port;
                }
                else
                {
                    return System.Configuration.ConfigurationManager.AppSettings["domain.name"];
                }
            }
        }

        #region Http

        static HttpRequest _request;

        /// <summary>
        /// 当前HttpRequest
        /// </summary>
        public static HttpRequest Request
        {
            get
            {
                if (HttpContext.Current != null)
                    return HttpContext.Current.Request;
                return _request;
            }
            set { _request = value; }
        }

        static HttpResponse _response;
        /// <summary>
        /// 当前HttpResponse
        /// </summary>
        public static HttpResponse Response
        {
            get
            {
                if (HttpContext.Current != null)
                    return HttpContext.Current.Response;
                return _response;
            }
            set { _response = value; }
        }

        /// <summary>
        /// 当前HttpSessionState
        /// </summary>
        public static HttpSessionState Session
        {
            get { return HttpContext.Current.Session; }
        }

        private static HttpServerUtility _server;
        /// <summary>
        ///  当前HttpServerUtility
        /// </summary>
        public static HttpServerUtility Server
        {
            get
            {
                if (HttpContext.Current != null)
                    return HttpContext.Current.Server;
                return _server;
            }
            set { _server = value; }
        }

        #endregion

    }
}
