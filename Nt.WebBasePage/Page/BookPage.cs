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
    public class BookPage : ListPage<Nt_Book>
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

        public bool HasCatalog
        {
            get
            {
                return TypeNames.Count > 0;
            }
        }

        List<SimpleCatalog> _typeNames;
        public List<SimpleCatalog> TypeNames
        {
            get
            {
                if (_typeNames == null)
                {
                    BookService service = Service as BookService;
                    _typeNames = service.GetCatalogFromXml();
                }
                return _typeNames;
            }
        }

        string sql4reply;

        int _currentBookID = 0;
        int _bookCounter = 0;

        SqlDataReader _replyReader;

        public DataRow CurrentBook { get { return DataList.Rows[_bookCounter - 1]; } }

        public SqlDataReader ReplyReader { get { return _replyReader; } }

        public int CurrentBookID { get { return _currentBookID; } }

        /// <summary>
        /// 下一个留言
        /// </summary>
        /// <returns></returns>
        public bool NextBook()
        {
            bool hasNext = DataList.Rows.Count > _bookCounter;
            if (hasNext)
            {
                _bookCounter++;
                sql4reply = string.Format(
                    "Select * From Nt_BookReply Where Display=1 And Book_Id={0} Order by DisplayOrder desc,ReplyDate desc",
                    CurrentBook["ID"]);
                _replyReader = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql4reply);
                _currentBookID = Convert.ToInt32(CurrentBook["ID"]);
            }
            else
            {
                DataList.Dispose();
                _replyReader.Close();
                _replyReader.Dispose();
            }

            return hasNext;
        }

        /// <summary>
        /// 下一个回复
        /// </summary>
        /// <returns>如果有返回true</returns>
        public bool NextBookReply()
        {
            bool hasNext = ReplyReader.Read();
            return hasNext;
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

        protected override void InitCommonData()
        {
            OrderBy = " DisplayOrder desc ";
            base.InitCommonData();
        }

    }
}