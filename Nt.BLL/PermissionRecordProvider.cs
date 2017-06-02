using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.BLL
{
    public class PermissionRecord
    {
        public int Id { get; set; }
        public string SystemName { get; set; }
        public string Category { get; set; }
        public string CategoryName { get; set; }
        public string Name { get; set; }
    }

    public class PermissionRecordProvider
    {

        #region Common
        public static readonly PermissionRecord WebSiteSettings = new PermissionRecord() { Id = 1, Name = "网站设置", SystemName = "WebSiteSettings", Category = "Common", CategoryName = "通用设置" };
        public static readonly PermissionRecord NavigationManage = new PermissionRecord() { Id = 2, Name = "导航管理", SystemName = "NavigationManage", Category = "Common", CategoryName = "通用设置" };
        public static readonly PermissionRecord LogManage = new PermissionRecord() { Id = 3, Name = "后台登陆日志", SystemName = "LogManage", Category = "Common", CategoryName = "通用设置" };
        public static readonly PermissionRecord BannerManage = new PermissionRecord() { Id = 4, Name = "Banner图管理", SystemName = "BannerManage", Category = "Common", CategoryName = "通用设置" };
        public static readonly PermissionRecord LanaguageManage = new PermissionRecord() { Id = 5, Name = "语言版本管理", SystemName = "LanaguageManage", Category = "Common", CategoryName = "通用设置" };
        public static readonly PermissionRecord JsManage = new PermissionRecord() { Id = 6, Name = "脚本管理", SystemName = "JsManage", Category = "Common", CategoryName = "通用设置" };
        public static readonly PermissionRecord ContentManage = new PermissionRecord() { Id = 7, Name = "内容管理", SystemName = "ContentManage", Category = "Common", CategoryName = "通用设置" };
        public static readonly PermissionRecord SliderManage = new PermissionRecord() { Id = 8, Name = "幻灯片管理", SystemName = "SliderManage", Category = "Common", CategoryName = "通用设置" };
        #endregion

        #region User
        public static readonly PermissionRecord UserLevelManage = new PermissionRecord() { Id = 9, Name = "管理员角色管理", SystemName = "UserLevelManage", Category = "User", CategoryName = "后台管理员管理" };
        public static readonly PermissionRecord UserManage = new PermissionRecord() { Id = 10, Name = "管理员管理", SystemName = "UserManage", Category = "User", CategoryName = "后台管理员管理" };
        public static readonly PermissionRecord UserLevelAuthorization = new PermissionRecord() { Id = 11, Name = "授权管理", SystemName = "UserLevelAuthorization", Category = "User", CategoryName = "后台管理员管理" };
        public static readonly PermissionRecord UserChangePwd = new PermissionRecord() { Id = 12, Name = "密码修改", SystemName = "UserChangePwd", Category = "User", CategoryName = "后台管理员管理" };
        #endregion

        #region SinglePage
        public static readonly PermissionRecord SinglePageManage = new PermissionRecord() { Id = 13, Name = "二级页面管理", SystemName = "SinglePageManage", Category = "SinglePage", CategoryName = "单页面管理" };
        public static readonly PermissionRecord SpecialPageManage = new PermissionRecord() { Id = 14, Name = "专题页管理", SystemName = "SpecialPageManage", Category = "SinglePage", CategoryName = "单页面管理" };
        #endregion

        #region News
        public static readonly PermissionRecord NewsCategoryManage = new PermissionRecord() { Id = 15, Name = "新闻目录管理", SystemName = "NewsCategoryManage", Category = "News", CategoryName = "新闻管理" };
        public static readonly PermissionRecord NewsManage = new PermissionRecord() { Id = 16, Name = "新闻列表", SystemName = "NewsManage", Category = "News", CategoryName = "新闻管理" };
        public static readonly PermissionRecord NewsEdit = new PermissionRecord() { Id = 17, Name = "添加新闻", SystemName = "NewsEdit", Category = "News", CategoryName = "新闻管理" };
        public static readonly PermissionRecord NewsSettings = new PermissionRecord() { Id = 18, Name = "设置", SystemName = "NewsSettings", Category = "News", CategoryName = "新闻管理" };
        #endregion

        #region Product
        public static readonly PermissionRecord ProductCategoryManage = new PermissionRecord() { Id = 19, Name = "产品目录管理", SystemName = "ProductCategoryManage", Category = "Product", CategoryName = "产品管理" };
        public static readonly PermissionRecord ProductManage = new PermissionRecord() { Id = 20, Name = "产品列表", SystemName = "ProductManage", Category = "Product", CategoryName = "产品管理" };
        public static readonly PermissionRecord ProductEdit = new PermissionRecord() { Id = 21, Name = "添加产品", SystemName = "ProductEdit", Category = "Product", CategoryName = "产品管理" };
        public static readonly PermissionRecord ProductSettings = new PermissionRecord() { Id = 22, Name = "设置", SystemName = "ProductSettings", Category = "Product", CategoryName = "产品管理" };
        #endregion

        #region Download
        public static readonly PermissionRecord DownloadCategoryManage = new PermissionRecord() { Id = 23, Name = "下载目录管理", SystemName = "DownloadCategoryManage", Category = "Download", CategoryName = "下载管理" };
        public static readonly PermissionRecord DownloadManage = new PermissionRecord() { Id = 24, Name = "下载列表", SystemName = "DownloadManage", Category = "Download", CategoryName = "下载管理" };
        public static readonly PermissionRecord DownloadEdit = new PermissionRecord() { Id = 25, Name = "添加下载", SystemName = "DownloadEdit", Category = "Download", CategoryName = "下载管理" };
        public static readonly PermissionRecord DownloadSettings = new PermissionRecord() { Id = 26, Name = "设置", SystemName = "DownloadSettings", Category = "Download", CategoryName = "下载管理" };
        #endregion

        #region Member
        public static readonly PermissionRecord MemberManage = new PermissionRecord() { Id = 27, Name = "会员列表", SystemName = "MemberManage", Category = "Member", CategoryName = "会员管理" };
        public static readonly PermissionRecord MemberEdit = new PermissionRecord() { Id = 28, Name = "添加会员", SystemName = "MemberEdit", Category = "Member", CategoryName = "会员管理" };
        public static readonly PermissionRecord MemberRoleManage = new PermissionRecord() { Id = 29, Name = "会员角色管理", SystemName = "MemberRoleManage", Category = "Member", CategoryName = "会员管理" };
        public static readonly PermissionRecord MemberRegisterDeclare = new PermissionRecord() { Id = 30, Name = "注册声明", SystemName = "MemberRegisterDeclare", Category = "Member", CategoryName = "会员管理" };
        public static readonly PermissionRecord MemberSettings = new PermissionRecord() { Id = 31, Name = "设置", SystemName = "MemberSettings", Category = "Member", CategoryName = "会员管理" };
        public static readonly PermissionRecord MemberChangePwd = new PermissionRecord() { Id = 32, Name = "修改密码", SystemName = "MemberChangePwd", Category = "Member", CategoryName = "会员管理" };
        #endregion

        #region Order
        public static readonly PermissionRecord OrderManage = new PermissionRecord() { Id = 33, Name = "订单列表", SystemName = "OrderManage", Category = "Order", CategoryName = "订单管理" };
        public static readonly PermissionRecord OrderEdit = new PermissionRecord() { Id = 34, Name = "添加订单", SystemName = "OrderEdit", Category = "Order", CategoryName = "订单管理" };
        public static readonly PermissionRecord OrderSettings = new PermissionRecord() { Id = 35, Name = "设置", SystemName = "OrderSettings", Category = "Order", CategoryName = "订单管理" };
        #endregion

        #region Book
        public static readonly PermissionRecord BookManage = new PermissionRecord() { Id = 36, Name = "预订列表", SystemName = "BookManage", Category = "Book", CategoryName = "预订管理" };
        public static readonly PermissionRecord BookCatalogManage = new PermissionRecord() { Id = 37, Name = "类别管理", SystemName = "BookCatalogManage", Category = "Book", CategoryName = "预订管理" };
        public static readonly PermissionRecord BookSettings = new PermissionRecord() { Id = 38, Name = "设置", SystemName = "BookSettings", Category = "Book", CategoryName = "预订管理" };
        #endregion

        #region Job
        public static readonly PermissionRecord JobManage = new PermissionRecord() { Id = 39, Name = "职位列表", SystemName = "JobManage", Category = "Job", CategoryName = "招聘管理" };
        public static readonly PermissionRecord JobEdit = new PermissionRecord() { Id = 40, Name = "添加职位", SystemName = "JobEdit", Category = "Job", CategoryName = "招聘管理" };
        public static readonly PermissionRecord JobResumeManage = new PermissionRecord() { Id = 41, Name = "简历管理", SystemName = "JobResumeManage", Category = "Job", CategoryName = "招聘管理" };
        public static readonly PermissionRecord JobSettings = new PermissionRecord() { Id = 42, Name = "设置", SystemName = "JobSettings", Category = "Job", CategoryName = "招聘管理" };
        #endregion

        #region Message
        public static readonly PermissionRecord MessageManage = new PermissionRecord() { Id = 43, Name = "留言列表", SystemName = "MessageManage", Category = "Message", CategoryName = "会员留言管理" };
        public static readonly PermissionRecord MessageAdminNotice = new PermissionRecord() { Id = 44, Name = "管理员公告", SystemName = "MessageAdminNotice", Category = "Message", CategoryName = "会员留言管理" };
        public static readonly PermissionRecord MessageCatalogManage = new PermissionRecord() { Id = 45, Name = "类别管理", SystemName = "MessageCatalogManage", Category = "Message", CategoryName = "会员留言管理" };
        public static readonly PermissionRecord MessageReplyManage = new PermissionRecord() { Id = 46, Name = "留言回复", SystemName = "MessageReplyManage", Category = "Message", CategoryName = "会员留言管理" };
        public static readonly PermissionRecord MessageSettings = new PermissionRecord() { Id = 47, Name = "设置", SystemName = "MessageSettings", Category = "Message", CategoryName = "会员留言管理" };
        #endregion

        #region Email
        public static readonly PermissionRecord EmailAccountManage = new PermissionRecord() { Id = 48, Name = "邮箱账号管理", SystemName = "EmailAccountManage", Category = "Email", CategoryName = "邮件管理" };
        public static readonly PermissionRecord EmailAccountEdit = new PermissionRecord() { Id = 49, Name = "添加邮箱账号", SystemName = "EmailAccountEdit", Category = "Email", CategoryName = "邮件管理" };
        public static readonly PermissionRecord EmailManage = new PermissionRecord() { Id = 50, Name = "邮件管理", SystemName = "EmailManage", Category = "Email", CategoryName = "邮件管理" };
        public static readonly PermissionRecord EmailSettings = new PermissionRecord() { Id = 51, Name = "设置", SystemName = "EmailSettings", Category = "Email", CategoryName = "邮件管理" };
        #endregion

        #region Seo
        public static readonly PermissionRecord WebsiteLinksManage = new PermissionRecord() { Id = 52, Name = "站内链接", SystemName = "WebsiteLinksManage", Category = "Seo", CategoryName = "Seo优化管理" };
        public static readonly PermissionRecord SitemapManage = new PermissionRecord() { Id = 53, Name = "搜索引擎优化", SystemName = "SeoSitemapManage", Category = "Seo", CategoryName = "Seo优化管理" };
        public static readonly PermissionRecord FriendLinkManage = new PermissionRecord() { Id = 54, Name = "友情链接", SystemName = "FriendLinkManage", Category = "Seo", CategoryName = "Seo优化管理" };
        public static readonly PermissionRecord SearchWordsManage = new PermissionRecord() { Id = 55, Name = "搜索关键词", SystemName = "SearchWordsManage", Category = "Seo", CategoryName = "Seo优化管理" };
        public static readonly PermissionRecord HtmliztionManage = new PermissionRecord() { Id = 56, Name = "静态生成", SystemName = "HtmliztionManage", Category = "Seo", CategoryName = "Seo优化管理" };
        #endregion

        #region System
        public static readonly PermissionRecord SystemDescription = new PermissionRecord() { Id = 57, Name = "系统说明", SystemName = "SystemDescription", Category = "System", CategoryName = "系统" };
        #endregion

        #region Course
        public static readonly PermissionRecord CourseCategoryManage = new PermissionRecord() { Id = 58, Name = "班级分类管理", SystemName = "CourseCategoryManage", Category = "Course", CategoryName = "课程管理" };
        public static readonly PermissionRecord CourseManage = new PermissionRecord() { Id = 59, Name = "课程列表", SystemName = "CourseManage", Category = "Course", CategoryName = "课程管理" };
        public static readonly PermissionRecord CourseEdit = new PermissionRecord() { Id = 60, Name = "添加课程", SystemName = "CourseEdit", Category = "Course", CategoryName = "课程管理" };
        #endregion

        #region Added
        public static readonly PermissionRecord ProductFieldsManage = new PermissionRecord() { Id = 61, Name = "字段管理", SystemName = "ProductFieldsManage", Category = "Product", CategoryName = "产品管理" };
        public static readonly PermissionRecord AdvertManage = new PermissionRecord() { Id = 62, Name = "广告管理", SystemName = "AdvertManage", Category = "Common", CategoryName = "通用设置" };
        public static readonly PermissionRecord BookReplyManage = new PermissionRecord() { Id = 63, Name = "预订.留言管理", SystemName = "BookReplyManage", Category = "Book", CategoryName = "预订管理" };
        public static readonly PermissionRecord BookAdminNotice = new PermissionRecord() { Id = 64, Name = "管理员公告", SystemName = "BookAdminNotice", Category = "Book", CategoryName = "预订管理" };
        #endregion

        #region DB
        public static readonly PermissionRecord DBManage = new PermissionRecord() { Id = 65, Name = "数据库管理", SystemName = "DBManage", Category = "DB", CategoryName = "数据库管理" };
        #endregion


        public static string[] AllPermissionCategory
        {
            get
            {
                return new string[]{"Common","User","SinglePage","News",
                    "Product","Download","Member","Order","Book",
                    "Job","Message","Email","Seo","System","Course",
                "DB"};
            }
        }

        /// <summary>
        /// all permissions records
        /// </summary>
        public static PermissionRecord[] AllPermissionRecords
        {
            get
            {
                return new PermissionRecord[]{
           WebSiteSettings,NavigationManage,LogManage,BannerManage,LanaguageManage,JsManage,ContentManage,SliderManage,AdvertManage,
           UserLevelManage,UserManage,UserLevelAuthorization,UserChangePwd,
           SinglePageManage,SpecialPageManage,
           NewsCategoryManage,NewsManage,NewsEdit,NewsSettings,
           ProductCategoryManage,ProductManage,ProductEdit,ProductSettings,ProductFieldsManage,
           DownloadCategoryManage,DownloadEdit,DownloadManage,DownloadSettings,
           MemberManage,MemberEdit,MemberRoleManage,MemberRegisterDeclare,MemberSettings,MemberChangePwd,
           OrderManage,OrderEdit,OrderSettings,
           BookManage,BookCatalogManage,BookSettings,BookReplyManage,BookAdminNotice,
           JobManage,JobEdit,JobResumeManage,JobSettings,
           MessageManage,MessageAdminNotice,MessageCatalogManage,MessageReplyManage,MessageSettings,
           EmailAccountManage,EmailAccountEdit,EmailManage,EmailSettings,
           WebsiteLinksManage,SitemapManage,FriendLinkManage,SearchWordsManage,HtmliztionManage,
           SystemDescription,
           CourseCategoryManage, CourseEdit, CourseManage,
           DBManage
            };
            }
        }

        /// <summary>
        /// to define  the permission that all common administrator owns
        /// </summary>
        public static PermissionRecord[] AdminDefaultPermissionRecords
        {
            get
            {
                return new PermissionRecord[]{
           WebSiteSettings,LogManage,BannerManage,SliderManage,AdvertManage,
           UserLevelManage,UserManage,UserLevelAuthorization,UserChangePwd,
           NewsCategoryManage,NewsManage,NewsEdit,
           ProductCategoryManage,ProductManage,ProductEdit,
           WebsiteLinksManage,SitemapManage,FriendLinkManage,SearchWordsManage,HtmliztionManage,
           SystemDescription
            };
            }
        }


    }
}
