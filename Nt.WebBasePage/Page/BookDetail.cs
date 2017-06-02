using Nt.BLL;
using Nt.DAL.Helper;
using Nt.Model;
using Nt.Model.Common;
using Nt.Model.SettingModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Nt.Web
{
    /// <summary>
    /// BookPage 的摘要说明
    /// </summary>
    public class BookDetail : DetailPage<Nt_Book>
    {
        #region Settings
        BookSettings _settings;
        public BookSettings Settings
        {
            get
            {
                if (_settings == null)
                    _settings = SettingService.GetSettingModel<BookSettings>();
                return _settings;
            }
        }

        BookAdminNotice _adminNotice;
        public BookAdminNotice AdminNotice
        {
            get
            {
                if (_adminNotice == null)
                    _adminNotice = SettingService.GetSettingModel<BookAdminNotice>();
                return _adminNotice;
            }
        }

        #endregion

        /// <summary>
        /// 是否有分类
        /// </summary>
        public bool HasCatalog
        {
            get
            {
                return TypeNames.Count > 0;
            }
        }

        List<SimpleCatalog> _typeNames;
        /// <summary>
        /// 分类
        /// </summary>
        public List<SimpleCatalog> TypeNames
        {
            get
            {
                if (_typeNames == null)
                {
                    BookService service = Service as BookService;
                    service.LanguageCode = NtConfig.CurrentLanguageModel.LanguageCode;
                    _typeNames = service.GetCatalogFromXml();
                }
                return _typeNames;
            }
        }

        /// <summary>
        /// 向页面渲染预订分类数据
        /// </summary>
        public void RenderTypeSelector(string wrapperTag, string li, object style)
        {
            var data = TypeNames;
            if (HasCatalog)
            {
                WriteBeginTag(wrapperTag, style);
                foreach (var item in data)
                {
                    Response.Write(li.Replace("{value}", item.Id.ToString()).Replace("{text}", item.Name));
                }
                WriteEndTag(wrapperTag);
            }
        }

        /// <summary>
        /// 获取回复
        /// </summary>
        /// <returns></returns>
        public DataTable GetReplies()
        {
            BookService service = _service as BookService;
            return service.GetAailableReply(NtID);
        }

    }
}