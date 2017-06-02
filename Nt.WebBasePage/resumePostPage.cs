using Nt.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Web
{
    public class resumePostPage:BasePageHandler
    {

        Dictionary<string, string> _fieldNames;
        /// <summary>
        /// 字段名
        /// </summary>
        Dictionary<string, string> FieldNames
        {
            get
            {
                if (_fieldNames == null)
                {
                    _fieldNames = new Dictionary<string, string>();
                    _fieldNames.Add("Title", "标题");
                    _fieldNames.Add("Name", "联系人");
                    _fieldNames.Add("Mobile", "手机");
                    _fieldNames.Add("Company", "公司");
                    _fieldNames.Add("Email", "邮箱");
                    _fieldNames.Add("Fax", "传真");
                    _fieldNames.Add("Address", "住址");
                    _fieldNames.Add("Tel", "电话");
                    _fieldNames.Add("ZipCode", "邮编");
                    _fieldNames.Add("PoliticalRole", "政治身份");
                    _fieldNames.Add("PersonID", "身份证号");
                    _fieldNames.Add("Grade", "在校成绩");
                    _fieldNames.Add("Nation", "民族");
                    _fieldNames.Add("NativePlace", "祖籍");
                    _fieldNames.Add("GraduatedFrom", "学校");
                    _fieldNames.Add("Body", "内容");
                }
                return _fieldNames;
            }
        }


        protected override void Handle()
        {
            string redirectUrl = Request.QueryString["redirectUrl"];
            if (string.IsNullOrEmpty(redirectUrl) ||
                redirectUrl.ToLower().StartsWith("http://"))
            {
                redirectUrl = NtConfig.CurrentTemplatesPath;
            }
            Alert(redirectUrl);
        }

    }
}
