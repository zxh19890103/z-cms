using Nt.BLL;
using Nt.BLL.Helper;
using Nt.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services;

namespace Nt.Pages.Seo
{
    public class SiteMap : NtPage
    {
        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.SitemapManage;
            }
        }

        #region WebService
        /// <summary>
        /// sitemap
        /// </summary>
        /// <param name="type">1:baidu,2:google</param>
        /// <returns></returns>
        [WebMethod]
        public static string GenSitemap(string type, string isHtml)
        {
            SitemapHelper helper = new SitemapHelper();
            helper.IsHtml = Convert.ToBoolean(isHtml);
            try
            {
                switch (type)
                {
                    case "1":
                        helper.SitemapType = SitemapType.Baidu;
                        helper.SiteMapName = "sitemap.xml";
                        helper.GenerateSitemap();
                        break;
                    case "2":
                        helper.SitemapType = SitemapType.Google;
                        helper.SiteMapName = "sitemap-google.xml";
                        helper.GenerateSitemap();
                        break;
                    default:
                        helper.GenerateSitemap();
                        break;
                }
            }
            catch (Exception ex)
            {
                return new NtJson(new
                {
                    error = 1,
                    message = ex.Message
                }).ToString();
            }

            string posturl = "";
            switch (helper.SitemapType)
            {
                case SitemapType.Baidu:
                    posturl = "http://zhanzhang.baidu.com/";
                    break;
                case SitemapType.Google:
                    posturl = "http://www.google.cn/webmasters/";
                    break;
                default:
                    posturl = "javascript:alert('没有提交网址');";
                    break;
            }

            return new NtJson(new
            {
                error = 0,
                message = "yeah!",
                countOfFound = helper.CountOfFound,
                sitemapPath = WebHelper.CurrentRootUrl + helper.VirtualDir2SaveXml + helper.SiteMapName,
                postUrl = posturl
            }).ToString();

        }
        #endregion
    }
}
