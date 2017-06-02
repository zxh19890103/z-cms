using Nt.BLL;
using Nt.BLL.Helper;
using Nt.DAL;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nt.Web
{
    /// <summary>
    /// Config 的摘要说明
    /// </summary>
    public class NtConfig
    {

        #region const page type
        public const int NAVI = 0;
        public const int INDEX = 1;
        public const int NEWS = 2;
        public const int PRODUCT = 3;
        public const int DOWNLOAD = 4;
        public const int COURSE = 5;
        public const int SINGLEPAGE = 6;
        public const int JOB = 7;
        public const int NEWS_LIST = 8;
        public const int PRODUCT_LIST = 9;
        public const int DOWNLOAD_LIST = 10;
        public const int COURSE_LIST = 11;
        public const int JOB_LIST = 12;
        #endregion

        #region Templates Path Root

        /// <summary>
        /// 当前应使用的模板的绝对路径
        /// </summary>
        static string _currentTemplatesPath = string.Empty;
        public static string CurrentTemplatesPath
        {
            get
            {
                if (string.IsNullOrEmpty(_currentTemplatesPath))
                    _currentTemplatesPath = GetTargetTemplatesPath(CurrentLanguage);
                return _currentTemplatesPath;
            }
            set { _currentTemplatesPath = value; }
        }

        /// <summary>
        /// 当前栏目的根目录,带/
        /// </summary>
        static string _currentChannelRootUrl = string.Empty;
        public static string CurrentChannelRootUrl
        {
            get
            {
                int lastIndexOfSlash = WebHelper.Request.Url.AbsolutePath.LastIndexOf('/');
                _currentChannelRootUrl = WebHelper.Request.Url.AbsolutePath.Substring(0, lastIndexOfSlash + 1); ;
                return _currentChannelRootUrl;
            }
            set { _currentChannelRootUrl = value; }
        }

        /// <summary>
        /// 当前Css文件的目录
        /// </summary>
        static string _currentCssRootPath = string.Empty;
        public static string CurrentCssRootPath
        {
            get
            {
                if (string.IsNullOrEmpty(_currentCssRootPath))
                    _currentCssRootPath = string.Format("{0}Content/Css/", CurrentTemplatesPath);
                return _currentCssRootPath;
            }
            set { _currentCssRootPath = value; }
        }

        /// <summary>
        /// 当前Js文件的目录
        /// </summary>
        static string _currentJsRootPath = string.Empty;
        public static string CurrentJsRootPath
        {
            get
            {
                if (string.IsNullOrEmpty(_currentJsRootPath))
                    _currentJsRootPath = string.Format("{0}Script/", CurrentTemplatesPath);
                return _currentJsRootPath;
            }
            set { _currentJsRootPath = value; }
        }

        /// <summary>
        /// 当前图片目录
        /// </summary>
        static string _currentImagesPath = string.Empty;
        public static string CurrentImagesPath
        {
            get
            {
                if (string.IsNullOrEmpty(_currentImagesPath))
                    _currentImagesPath = string.Format("{0}Content/Images/", CurrentTemplatesPath);
                return _currentImagesPath;
            }
            set { _currentImagesPath = value; }
        }

        static int _currentLanguage = 0;
        /// <summary>
        /// 当前语言版本id
        /// </summary>
        public static int CurrentLanguage
        {
            get
            {
                if (_currentLanguage == 0)
                {
                    _currentLanguage = TargetLanguage;
                }
                return _currentLanguage;
            }
            set { _currentLanguage = value; }
        }

        static Nt_Language _currentLanguageModel;
        /// <summary>
        /// 当前语言版本的信息
        /// </summary>
        public static Nt_Language CurrentLanguageModel
        {
            get
            {
                if (_currentLanguageModel == null)
                {
                    _currentLanguageModel = CommonFactory.GetById<Nt_Language>(CurrentLanguage);
                    if (_currentLanguageModel == null)
                        throw new Exception("appSettings配置错误");
                }
                return _currentLanguageModel;
            }
            set { _currentLanguageModel = value; }
        }

        /// <summary>
        /// 目标语言版本id
        /// </summary>
        public static int TargetLanguage
        {
            get
            {
                string currentAbsPath = WebHelper.Request.Url.AbsolutePath.ToLower();
                int pos = currentAbsPath.IndexOf('/');
                int pos1 = currentAbsPath.Substring(pos + 1).IndexOf('/') + 1;
                if (pos > -1)
                {
                    if (pos1 > -1)
                    {
                        string path = currentAbsPath.Substring(pos, pos1 - pos + 1);
                        foreach (var m in LangCodeIDMappings)
                        {
                            if (path.Equals(m.Value))
                                return m.Key;
                        }
                    }
                }
                return LangCodeIDMappings.First().Key;
            }
        }

        /// <summary>
        /// 获取目标模板的根目录
        /// </summary>
        /// <param name="lang">语言id</param>
        /// <returns></returns>
        public static string GetTargetTemplatesPath(int lang)
        {
            foreach (var m in LangCodeIDMappings)
            {
                if (lang == m.Key)
                    return m.Value;
            }
            return "/";
        }

        /// <summary>
        /// 当切换语言版本的时候，调用此方法，
        /// </summary>
        public static void EmptyStaticResources()
        {
            CurrentChannelRootUrl = string.Empty;
            CurrentCssRootPath = string.Empty;
            CurrentImagesPath = string.Empty;
            CurrentJsRootPath = string.Empty;
            CurrentTemplatesPath = string.Empty;
            CurrentLanguage = 0;
            CurrentLanguageModel = null;
        }

        #endregion

        #region  多语言信息
        /// <summary>
        /// 指示是否使用了多语言
        /// </summary>
        public static bool HasMutiLanguage
        {
            get
            {
                return Convert.ToBoolean
                (System.Configuration.ConfigurationManager.AppSettings["mutiLang"]);
            }
        }


        public static string LangCodeIDMappingsConfig
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["lang.templates.path.mappings"]; }
        }

        static Dictionary<int, string> _langCodeIDMappings = null;
        public static Dictionary<int, string> LangCodeIDMappings
        {
            get
            {
                if (_langCodeIDMappings == null)
                {
                    _langCodeIDMappings = new Dictionary<int, string>();
                    string[] mappings = LangCodeIDMappingsConfig.Split(new char[] { ':', ',' });
                    for (int i = 0; i < mappings.Length; )
                    {
                        _langCodeIDMappings[Convert.ToInt32(mappings[i++])] = mappings[i++];
                    }
                }

                return _langCodeIDMappings;
            }
        }
        #endregion

        #region page size config

        public static int NewsListPageSize
        {
            get
            {
                string v = System.Configuration.ConfigurationManager.AppSettings["news.list.pageSize"];
                if (v != null)
                    return Convert.ToInt32(v);
                return 12;
            }
        }

        public static int ProductListPageSize
        {
            get
            {
                string v = System.Configuration.ConfigurationManager.AppSettings["product.list.pageSize"];
                if (v != null)
                    return Convert.ToInt32(v);
                return 12;
            }
        }

        public static int DownloadListPageSize
        {
            get
            {
                string v = System.Configuration.ConfigurationManager.AppSettings["download.list.pageSize"];
                if (v != null)
                    return Convert.ToInt32(v);
                return 12;
            }
        }

        public static int CourseListPageSize
        {
            get
            {
                string v = System.Configuration.ConfigurationManager.AppSettings["course.list.pageSize"];
                if (v != null)
                    return Convert.ToInt32(v);
                return 12;
            }
        }

        public static int JobListPageSize
        {
            get
            {
                string v = System.Configuration.ConfigurationManager.AppSettings["job.list.pageSize"];
                if (v != null)
                    return Convert.ToInt32(v);
                return 12;
            }
        }

        #endregion

        /// <summary>
        /// 手机版网站的域名或根路径
        /// </summary>
        public static string MobileSiteUrl
        {
            get
            {
                string v = System.Configuration.ConfigurationManager.AppSettings["mobile.website.url"];
                return v;
            }
        }

        /// <summary>
        /// SetTop desc,Recommended desc,DisplayOrder desc,AddDate desc
        /// </summary>
        public static string CommonOrderBy { get { return "SetTop desc,Recommended desc,DisplayOrder desc,AddDate desc"; } }

    }
}