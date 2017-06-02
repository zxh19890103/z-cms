using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nt.Model;
using Nt.Model.SettingModel;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using Nt.BLL.Helper;
using Nt.BLL.Extension;

namespace Nt.BLL
{
    public class SettingService<S>
         where S : BaseSettingModel, new()
    {
        /// <summary>
        /// 0---语言版本
        /// </summary>
        private const string SETTING_DIR_PATTERN = "/App_Data/Settings/{0}/";
        private const string ROOT_NAME = "Setting";

        string _xmlpath = string.Empty;
        string _xdocName = string.Empty;
        string _xdocDir = string.Empty;

        public string LanguageCode
        {
            get { return NtContext.Current.CurrentLanguage.LanguageCode; }
        }

        public SettingService()
        {
            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            var className = typeof(S).Name;
            _xdocName = className;//将末尾的Model5个字符去掉
            _xdocDir = string.Format(SETTING_DIR_PATTERN,
                LanguageCode);
            var phyDirPath = WebHelper.MapPath(_xdocDir);
            if (!Directory.Exists(phyDirPath))
                Directory.CreateDirectory(phyDirPath);
            _xmlpath = WebHelper.MapPath(
                string.Format("{0}{1}.xml", _xdocDir, _xdocName));
        }

        /// <summary>
        /// 从Xml获取S
        /// </summary>
        /// <returns></returns>
        public S ResolveSetting()
        {
            if (File.Exists(_xmlpath))
                return GetSettingFromXml();
            else
                CreateSettingXml();
            var s = new S();
            s.InitData();
            return s;
        }

        /// <summary>
        /// 保存修改
        /// </summary>
        /// <param name="s"></param>
        public void SaveSetting(S s)
        {
            var xdoc = new XmlDocument();
            xdoc.Load(_xmlpath);
            XmlNode root = xdoc.SelectSingleNode(ROOT_NAME);
            object value;
            XmlNode node;
            foreach (var p in s.GetType().GetProperties())
            {
                value = p.GetValue(s, null);
                node = root[p.Name];
                node.InnerText = value.ToString();
            }
            xdoc.Save(_xmlpath);
        }

        /// <summary>
        /// 从Xml文档中获取S的属性值
        /// </summary>
        /// <returns></returns>
        private S GetSettingFromXml()
        {
            S s = new S();
            var xdoc = new XmlDocument();
            xdoc.Load(_xmlpath);
            XmlNode root = xdoc.SelectSingleNode(ROOT_NAME);
            string value;
            foreach (var p in s.GetType().GetProperties())
            {
                XmlNode n = root.SelectSingleNode(p.Name);
                if (n == null)
                    value = DAL.Helper.CommonHelper.GetDefaultValueByTypeCode(Type.GetTypeCode(p.PropertyType)).ToString();
                else
                    value = root.SelectSingleNode(p.Name).InnerText;
                Nt.DAL.Helper.CommonHelper.SetValueToProp(p, s, value);
            }
            return s;
        }

        /// <summary>
        /// 根据S的属性结构创建Xml文档
        /// </summary>
        public void CreateSettingXml()
        {
            var xdoc = new XmlDocument();
            XmlDeclaration declaration = xdoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xdoc.AppendChild(declaration);
            XmlElement root = xdoc.CreateElement(ROOT_NAME);
            xdoc.AppendChild(root);
            foreach (var p in typeof(S).GetProperties())
            {
                XmlElement e = xdoc.CreateElement(p.Name);
                e.InnerText = Nt.DAL.Helper.CommonHelper
                    .GetDefaultValueByTypeCode(Type.GetTypeCode(p.PropertyType))
                    .ToString();
                root.AppendChild(e);
            }
            xdoc.Save(_xmlpath);
        }
    }


    public class SettingService
    {
        public static S GetSettingModel<S>(string languageCode)
            where S : BaseSettingModel, new()
        {
            var className = typeof(S).Name;
            var xdocName = className;
            var filename = WebHelper.MapPath(
                string.Format("/App_Data/Settings/{0}/{1}.xml", languageCode, xdocName));
            if (!File.Exists(filename))
            {
                return new S();
            }
            var xdoc = new XmlDocument();
            xdoc.Load(filename);
            var s = new S();
            string value = string.Empty;
            XmlNode root = xdoc.SelectSingleNode("Setting");
            foreach (var p in s.GetType().GetProperties())
            {
                XmlNode n = root.SelectSingleNode(p.Name);
                if (n == null)
                    value = DAL.Helper.CommonHelper.GetDefaultValueByTypeCode(Type.GetTypeCode(p.PropertyType)).ToString();
                else
                    value = root.SelectSingleNode(p.Name).InnerText;
                Nt.DAL.Helper.CommonHelper.SetValueToProp(p, s, value);
            }
            return s;
        }

        public static S GetSettingModel<S>()
            where S : BaseSettingModel, new()
        {
            return SettingService.GetSettingModel<S>(
                NtContext.Current.CurrentLanguage.LanguageCode);
        }
    }

}
