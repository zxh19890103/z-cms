using Nt.BLL;
using Nt.BLL.Helper;
using Nt.DAL.Helper;
using Nt.Framework;
using Nt.Model.SettingModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Services;
using System.Xml;

namespace Nt.Pages.Seo
{
    public class WebsiteLink : NtPage
    {
        const string ROOT_NAME = "WebsiteLinks";
        const string VIRTUAL_PATH = "/App_Data/xml/websitelink-{0}.xml";

        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.WebsiteLinksManage;
            }
        }

        static string VirtualPath
        {
            get
            {
                return string.Format(VIRTUAL_PATH,
                    Nt.BLL.NtContext.Current.CurrentLanguage.LanguageCode);
            }
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (IsHttpPost)
            {
                SaveDataFromPage();
            }
            PageTitle = "站内链接";
        }

        List<WebsiteLinkItem> _dataSource = null;
        public List<WebsiteLinkItem> DataSource
        {
            get
            {
                if (_dataSource == null)
                    _dataSource = GetDataFromXml();
                return _dataSource;
            }
        }

        /// <summary>
        /// 从页面获取数据并保存
        /// </summary>
        void SaveDataFromPage()
        {
            string[] words = Request.Form["Word"] == null ? new string[0] : Request.Form["Word"].Split(',');
            string[] urls = Request.Form["Url"] == null ? new string[0] : Request.Form["Url"].Split(',');

            List<WebsiteLinkItem> data = new List<WebsiteLinkItem>();
            for (int i = 0; i < words.Length; i++)
            {
                data.Add(new WebsiteLinkItem() { Word = words[i], Url = urls[i], Id = i });
            }
            SaveDataToXml(data);
            ReLoadByScript("保存成功!");
        }

        /// <summary>
        /// 保存到Xml
        /// </summary>
        /// <param name="data">数据</param>
        void SaveDataToXml(List<WebsiteLinkItem> data)
        {
            string path = MapPath(VirtualPath);
            XmlDocument xdoc = new XmlDocument();
            XmlElement root;
            if (!File.Exists(path))
            {
                XmlDeclaration declaration = xdoc.CreateXmlDeclaration("1.0", "utf-8", null);
                xdoc.AppendChild(declaration);
                root = xdoc.CreateElement(ROOT_NAME);
                xdoc.AppendChild(root);
            }
            else
            {
                xdoc.Load(path);
                root = xdoc.DocumentElement;
                root.RemoveAll();
            }

            if (data != null && data.Count > 0)
            {
                foreach (var item in data)
                {
                    XmlAttribute attr0 = xdoc.CreateAttribute("Word");
                    attr0.Value = item.Word;
                    XmlAttribute attr1 = xdoc.CreateAttribute("Url");
                    attr1.Value = item.Url;
                    XmlAttribute attr2 = xdoc.CreateAttribute("Id");
                    attr2.Value = item.Id.ToString();
                    XmlElement node = xdoc.CreateElement("Add");
                    node.Attributes.Append(attr0);
                    node.Attributes.Append(attr1);
                    node.Attributes.Append(attr2);
                    root.AppendChild(node);
                }
            }

            xdoc.Save(path);
        }

        /// <summary>
        /// 由Xml文档提取数据
        /// </summary>
        /// <returns></returns>
        List<WebsiteLinkItem> GetDataFromXml()
        {
            string path = MapPath(VirtualPath);
            if (!File.Exists(path))
            {
                return new List<WebsiteLinkItem>();
            }
            else
            {
                List<WebsiteLinkItem> data = new List<WebsiteLinkItem>();
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(path);
                XmlNode root = xdoc.SelectSingleNode(ROOT_NAME);
                foreach (XmlNode item in root.ChildNodes)
                {
                    data.Add(new WebsiteLinkItem()
                    {
                        Word = item.Attributes["Word"].Value,
                        Url = item.Attributes["Url"].Value,
                        Id = Convert.ToInt32(item.Attributes["Id"].Value)
                    });
                }
                return data;
            }
        }


        #region WebService


        /// <summary>
        /// 站内链接
        /// </summary>
        /// <param name="way">add-0添加所有，add-{num}：添加指定，cancel-0：取消所有，cancel-{num}：取消指定</param>
        /// <returns></returns>
        [WebMethod]
        public static string HandleWebsiteLinks(string way)
        {

            List<WebsiteLinkItem> data = new List<WebsiteLinkItem>();

            //get data from xml file
            string path = WebHelper.MapPath(VirtualPath);
            if (File.Exists(path))
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(path);
                XmlNode root = xdoc.SelectSingleNode(ROOT_NAME);
                foreach (XmlNode item in root.ChildNodes)
                {
                    data.Add(new WebsiteLinkItem()
                    {
                        Word = item.Attributes["Word"].Value,
                        Url = item.Attributes["Url"].Value,
                        Id = Convert.ToInt32(item.Attributes["Id"].Value)
                    });
                }
            }

            if (data == null || data.Count < 1)
                return new NtJson(new { error = 0, message = "无链接..." }).ToString();
            try
            {
                int i = 0, j = 0, z = 0;
                string message = "";
                string[] method = way.Split('-');
                int id = Convert.ToInt32(method[1]);
                switch (method[0])
                {
                    case "add":
                        i = AddWebLink(data, "Nt_News", id);
                        j = AddWebLink(data, "Nt_Product", id);
                        z = AddWebLink(data, "Nt_SinglePage", id);
                        message = string.Format("已经添加{0}处站内链接.", i + j + z);
                        break;
                    case "cancel":
                        CancelWebLink(data, "Nt_News", id);
                        CancelWebLink(data, "Nt_Product", id);
                        CancelWebLink(data, "Nt_SinglePage", id);
                        message = "已经取消全部站内链接.";
                        break;
                    default:
                        message = "未作任何处理.";
                        break;
                }
                return new NtJson(new { error = 0, message = message }).ToString();

            }
            catch (Exception ex)
            {
                return new NtJson(new { error = 1, message = ex.Message }).ToString();
            }

        }

        /// <summary>
        /// 取消所有的关键词链接
        /// </summary>
        /// <param name="links"></param>
        /// <param name="tab"></param>
        /// <returns></returns>
        static int CancelWebLink(List<WebsiteLinkItem> links, string tab, int id)
        {
            using (SqlConnection conn = SqlHelper.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                string sql = string.Format("Select Id,Body From {0} Where Language_Id={1}", tab, Nt.BLL.NtContext.Current.LanguageID);
                cmd.CommandText = sql;
                DataTable data = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
                int counter = 0;
                string oldvalue = string.Empty;
                string newvalue = string.Empty;
                foreach (DataRow r in data.Rows)
                {
                    string body = r[1].ToString();
                    if (body == "")
                        continue;
                    foreach (var item in links)
                    {
                        if (id == 2014 || id == item.Id)
                        {
                            oldvalue = string.Format("<a class=\"nt-website-link\" href=\"{0}\">{1}</a>", item.Url, item.Word);
                            newvalue = item.Word;
                            body = body.Replace(oldvalue, newvalue);
                        }
                    }
                    r["Body"] = body;
                }
                adapter.UpdateCommand =
                    new SqlCommand(
                        "UPDATE [" + tab + "] Set Body=@Body Where ID=@ID;", conn);
                adapter.UpdateCommand.Parameters.Add("@Body",
                   SqlDbType.VarChar, int.MaxValue, "Body");
                adapter.UpdateCommand.Parameters.Add("@ID",
                   SqlDbType.Int, 4, "ID");
                adapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.None;
                adapter.Update(data);
                return counter;
            }
        }

        /// <summary>
        /// 添加所有指定的链接到表tab
        /// </summary>
        /// <param name="links"></param>
        /// <param name="tab"></param>
        /// <returns></returns>
        static int AddWebLink(List<WebsiteLinkItem> links, string tab, int id)
        {
            using (SqlConnection conn = SqlHelper.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                string sql = string.Format("Select Id,Body From {0} Where Language_Id={1}", tab, Nt.BLL.NtContext.Current.LanguageID);
                cmd.CommandText = sql;
                DataTable data = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
                int counter = 0;
                foreach (DataRow r in data.Rows)
                {
                    string body = r[1].ToString();
                    if (body == "")
                        continue;
                    foreach (var item in links)
                    {
                        if (id == 2014 || id == item.Id)
                        {
                            int c = 0;
                            body = WrapLink(body, item.Word, item.Url, out c);
                            counter += c;
                        }
                    }
                    r["Body"] = body;
                }
                adapter.UpdateCommand =
                    new SqlCommand(
                        "UPDATE [" + tab + "] Set Body=@Body Where ID=@ID;", conn);
                adapter.UpdateCommand.Parameters.Add("@Body",
                   SqlDbType.VarChar, int.MaxValue, "Body");
                adapter.UpdateCommand.Parameters.Add("@ID",
                   SqlDbType.Int, 4, "ID");
                adapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.None;
                adapter.Update(data);
                return counter;
            }
        }

        /// <summary>
        /// 给指定的字符串中的关键词添加指定的链接
        /// </summary>
        /// <param name="input"></param>
        /// <param name="word"></param>
        /// <param name="href"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        static string WrapLink(string input, string word, string href, out int c)
        {
            c = 0;
            if (input.Length == 0
                || word.Length == 0
                || input.Length < word.Length)
                return input;

            StringBuilder sb = new StringBuilder(input);
            int p = 0;//当前位置，起始索引
            int next = 0;//下一个进行匹配的起始索引
            int len = word.Length;//链接词的长度
            int maxIndex = 0;//最大索引值
            string tagBegin = string.Format("<a class=\"nt-website-link\" href=\"{0}\">", href, word);//开始标签
            int tagBeginLen = tagBegin.Length;//开始标签的长度
            string tagEnd = "</a>";//结束标签
            int tagEndLen = 4;//结束标签的长度
            int j = 0;
            bool success = true;//一个值，指示当前匹配是否成功
            bool breakable = false;//一个值，指示是否该退出循环
            while (true)
            {
                maxIndex = sb.Length - 1;
                success = true;
                j = 0;
                for (int i = 0; i < len; i++)
                {
                    if (i + p > maxIndex)//如果指定超出字符串的最大索引，则指示可以退出循环
                        breakable = true;
                    else
                    {
                        if (sb[i + p] != word[j])
                        {
                            success = false;
                            break;
                        }
                        j++;
                    }
                }
                if (breakable) break;

                if (success)
                {
                    next = p + len;
                    //进一步确认当前匹配是否有效
                    if (next + 3 <= maxIndex
                       && sb[next] == '<'
                       && sb[next + 1] == '/'
                       && sb[next + 2] == 'a'
                       && sb[next + 3] == '>')
                    {
                        next += tagEndLen;
                    }
                    else
                    {
                        sb.Insert(p, tagBegin);
                        next += tagBeginLen;
                        sb.Insert(next, tagEnd);
                        next += tagEndLen;
                        c++;
                    }
                }
                else
                {
                    next = p + 1;
                }
                p = next;
            }
            return sb.ToString();
        }

        #endregion
    }
}
