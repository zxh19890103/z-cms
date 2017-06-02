using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Text;
using Nt.Model;
using Nt.BLL.Caching;
using Nt.BLL.Helper;
using Nt.DAL;

namespace Nt.BLL
{
    public sealed class NtContext
    {
        #region singleton
        private NtContext() { }

        static NtContext _current = null;
        static readonly object padlock = new object();

        public static NtContext Current
        {
            get
            {
                lock (padlock)
                {
                    if (_current == null)
                    {
                        _current = new NtContext();
                    }
                    return _current;
                }
            }
        }
        #endregion

        #region const
        public const int IMPOSSIBLE_ID = -8964;
        public const string COOKIE_KEY_OF_LANGUAGE = "nt-current-language-id";
        public const string SESSION_KEY_OF_USER = "nt-user-id";
        public const string SESSION_KEY_OF_MEMBER = "nt-member-id";
        public const string CACHE_KEY_OF_CURRENT_USER = "nt-cache-current-user-{0}";
        public const string CACHE_KEY_OF_CURRENT_LANGUAGE = "nt-cache-current-langauge-{0}";
        public const string CACHE_KEY_OF_CURRENT_MEMBER = "nt-cache-current-member-{0}";
        #endregion

        #region UserID LanguageID MemberID

        public int UserID
        {
            get
            {
                return CurrentUser.Id;
            }
        }

        public int LanguageID
        {
            get
            {
                return CurrentLanguage.Id;
            }
        }

        public int MemberID
        {
            get
            {
                return CurrentMember.Id;
            }
        }

        #endregion

        #region Session variables and cookie current language version

        public void SessionUser(int userID)
        {
            WebHelper.Session[SESSION_KEY_OF_USER] = userID;
        }

        /// <summary>
        /// CookieLanguageID
        /// </summary>
        /// <param name="languageID"></param>
        /// <param name="Error">您的浏览器版本不支持Cookies功能，请先开启此功能</param>
        public void CookieLanguageID(int languageID)
        {
            if (!WebHelper.Request.Browser.Cookies)
                throw new Exception("您的浏览器版本不支持Cookies功能，请先开启此功能.");
            var cookie = new HttpCookie(COOKIE_KEY_OF_LANGUAGE
                , languageID.ToString());
            cookie.Expires = DateTime.Now.AddDays(30);
            WebHelper.Response.SetCookie(cookie);
        }

        #endregion

        #region CurrentLanguage  CurrentUser CurrentMember

        public Nt_Language CurrentLanguage { get { return TryGetCurrentLanguage(); } }

        public Nt_User CurrentUser { get { return TryGetCurrentUser(); } }

        public Nt_Member CurrentMember { get { return TryGetCurrentMember(); } }

        #endregion

        #region IsAdministrator,IsLanguageDefault

        /// <summary>
        /// 一个bool型的值，指示当前用户是否是超级管理员
        /// </summary>
        public bool IsAdministrator { get { return CurrentUser.UserLevel_Id == 1; } }

        //public bool IsLanguageDefault { get { return CurrentLanguage.Id == 1; } }

        #endregion

        /// <summary>
        /// 当用户退出时
        /// </summary>
        public void SessionEnd()
        {
            ClearCache();
            WebHelper.Session.Contents.Remove(SESSION_KEY_OF_USER);
            WebHelper.Session.Contents.Remove(SESSION_KEY_OF_MEMBER);
        }

        /// <summary>
        /// 一个值，指示当前访问后台页面的是否是已经登录的用户
        /// </summary>
        /// <returns></returns>
        public bool Logined()
        {
            return TryGetCurrentUser() != null;
        }

        public void ClearCache()
        {
            string key = string.Format(CACHE_KEY_OF_CURRENT_LANGUAGE, WebHelper.Request.Cookies[COOKIE_KEY_OF_LANGUAGE]);
            NtCachingManager.Current.Remove(key);
            key = string.Format(CACHE_KEY_OF_CURRENT_USER, WebHelper.Session[SESSION_KEY_OF_USER]);
            NtCachingManager.Current.Remove(key);
            key = string.Format(CACHE_KEY_OF_CURRENT_MEMBER, WebHelper.Session[SESSION_KEY_OF_MEMBER]);
            NtCachingManager.Current.Remove(key);
        }

        /// <summary>
        /// 获取当前登录的用户
        /// </summary>
        /// <returns></returns>
        Nt_User TryGetCurrentUser()
        {
            int userid = IMPOSSIBLE_ID;
            var t = WebHelper.Session[SESSION_KEY_OF_USER];
            if (t == null)
                return null;
            userid = Convert.ToInt32(t);
            string key = string.Format(CACHE_KEY_OF_CURRENT_USER, userid);
            if (NtCachingManager.Current.HasCached(key))
                return NtCachingManager.Current.Get<Nt_User>(key);

            Nt_User currentUser = CommonFactory.GetById<Nt_User>(userid);

            if (currentUser == null)
                return null;

            NtCachingManager.Current.Cache(
                   key,
                   currentUser
                   );
            return currentUser;
        }

        /// <summary>
        /// 获取当前语言版本
        /// </summary>
        Nt_Language TryGetCurrentLanguage()
        {
            int langid = IMPOSSIBLE_ID;
            var t = WebHelper.Request.Cookies[COOKIE_KEY_OF_LANGUAGE];

            if (t != null)
                langid = Convert.ToInt32(t.Value);

            Nt_Language currentLanguage = null;
            string key = string.Empty;

            if (langid != IMPOSSIBLE_ID)
            {
                key = string.Format(CACHE_KEY_OF_CURRENT_LANGUAGE, langid);
                if (NtCachingManager.Current.HasCached(key))
                    return NtCachingManager.Current.Get<Nt_Language>(key);

                currentLanguage = CommonFactory.GetById<Nt_Language>(langid);
                if (currentLanguage != null)
                    return currentLanguage;
            }

            currentLanguage = CommonFactory.GetFirstOrDefault<Nt_Language>();

            if (currentLanguage == null)
                throw new Exception("数据库中不存在任何语言信息!");

            key = string.Format(CACHE_KEY_OF_CURRENT_LANGUAGE, currentLanguage.Id);

            NtCachingManager.Current.Cache(
               key,
               currentLanguage
               );

            CookieLanguageID(currentLanguage.Id);

            return currentLanguage;
        }

        /// <summary>
        /// 获取当前在线会员
        /// </summary>
        Nt_Member TryGetCurrentMember()
        {
            int mid = IMPOSSIBLE_ID;
            var t = WebHelper.Session[SESSION_KEY_OF_MEMBER];
            if (t == null)
                return null;

            mid = Convert.ToInt32(t);
            string key = string.Format(CACHE_KEY_OF_CURRENT_MEMBER, mid);
            if (NtCachingManager.Current.HasCached(key))
                return NtCachingManager.Current.Get<Nt_Member>(key);

            Nt_Member currentMember = CommonFactory.GetById<Nt_Member>(mid);

            if (currentMember == null)
                return null;

            NtCachingManager.Current.Cache(
                   key,
                   currentMember
                   );
            return currentMember;
        }
        
    }
}
