using Nt.BLL.Helper;
using Nt.Model.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Nt.BLL
{
    /// <summary>
    /// 多语言问题尚未调好
    /// </summary>
    /// <typeparam name="M"></typeparam>
    public class BaseServiceWithCatalog<M> : BaseService<M>
        where M : global::Nt.Model.BaseLocaleModel, new()
    {
        const string VIRTUAL_PATH_FOR_CATALOGS = "/App_Data/Types/types-{1}-{0}.xml";
        const string ROOT_NAME = "Types";

        public string VirtualPath4Catalogs
        {
            get
            {
                return string.Format(VIRTUAL_PATH_FOR_CATALOGS,
                    LanguageCode,
                    typeof(M).Name.Replace("Nt_", ""));
            }
        }

        string _langCode;
        /// <summary>
        /// 当前语言版本的符号
        /// </summary>
        public string LanguageCode
        {
            get
            {
                if (string.IsNullOrEmpty(_langCode))
                {
                    return NtContext.Current.CurrentLanguage.LanguageCode;
                }
                return _langCode;
            }
            set { _langCode = value; }
        }


        /// <summary>
        /// 将数据保存在Xml文档里
        /// </summary>
        /// <param name="names">分类名</param>
        public void SaveCatalog(string typeIds, string names)
        {
            string path = WebHelper.MapPath(VirtualPath4Catalogs);
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

            string[] ns = names == null ? new string[0] : names.Split(',');
            string[] ids = typeIds == null ? new string[0] : typeIds.Split(',');

            for (int i = 0; i < ns.Length; i++)
            {
                XmlAttribute attr0 = xdoc.CreateAttribute("Value");
                attr0.Value = ids[i] == string.Empty ? "0" : ids[i];
                XmlAttribute attr1 = xdoc.CreateAttribute("Name");
                attr1.Value = ns[i];
                XmlElement node = xdoc.CreateElement("Add");
                node.Attributes.Append(attr0);
                node.Attributes.Append(attr1);
                root.AppendChild(node);
            }
            xdoc.Save(path);
        }

        /// <summary>
        /// 由Xml文档获取Catalog数据
        /// </summary>
        /// <returns></returns>
        public List<SimpleCatalog> GetCatalogFromXml()
        {
            string path = WebHelper.MapPath(VirtualPath4Catalogs);
            if (!File.Exists(path))
            {
                return new List<SimpleCatalog>();
            }
            else
            {
                List<SimpleCatalog> data = new List<SimpleCatalog>();
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(path);
                XmlNode root = xdoc.SelectSingleNode(ROOT_NAME);
                foreach (XmlNode item in root.ChildNodes)
                {
                    data.Add(
                        new SimpleCatalog() { Id = Convert.ToInt32(item.Attributes["Value"].Value), Name = item.Attributes["Name"].Value }
                    );
                }
                return data;
            }
        }

    }
}
