using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Nt.Framework
{
    public static class HtmlHelper
    {
        public static string Input(bool show, string name, object value, object props)
        {
            if (!show)
                return string.Empty;
            StringBuilder html = new StringBuilder();
            html.AppendFormat("<input type=\"text\" class=\"input-text no-comma\" name=\"{0}\" value=\"{1}\"", name, value);
            AppendAttrs(html, props);
            html.AppendFormat("/>");
            return html.ToString();
        }

        public static string BoolLabel(string label0, string label1, object current, object props)
        {
            StringBuilder html = new StringBuilder();
            var b = Convert.ToBoolean(current);
            string text = b ? label0 : label1;
            int val = b ? 1 : 0;
            html.AppendFormat("<label stringResources=\"{0}|{1}\" current=\"{2}\"", label0, label1, val);
            if (!b)
                html.AppendFormat(" style=\"color:red;\"");
            AppendAttrs(html, props);
            html.AppendFormat(">{0}</label>", text);
            return html.ToString();
        }

        public static string DropdownList(IList<ListItem> data, object props)
        {
            if (data == null)
                return string.Empty;
            StringBuilder html = new StringBuilder();
            html.Append("<select");
            AppendAttrs(html, props);
            html.Append(">");
            foreach (var item in data)
            {
                if (item.Selected)
                    html.AppendFormat("<option selected=\"selected\" value=\"{0}\">{1}</option>", item.Value, item.Text);
                else
                    html.AppendFormat("<option value=\"{0}\">{1}</option>", item.Value, item.Text);
            }
            html.Append("</select>");
            return html.ToString();
        }

        public static string CheckBox(string text, object isChecked, string name, object props)
        {
            StringBuilder html = new StringBuilder();
            var id = "ck" + NtUtility.GenRandNum();
            html.AppendFormat("<input class=\"input-bool input-state-tracking\"  type=\"checkbox\" id=\"{0}\"", id);
            AppendAttrs(html, props);
            var b = Convert.ToBoolean(isChecked);
            if (b)
                html.Append(" checked=\"checked\"");
            html.Append("/>");
            html.AppendFormat("<input for=\"{1}\" type=\"hidden\" name=\"{0}\"", name, id);
            if (b)
                html.AppendFormat(" value=\"True\"/>");
            else
                html.AppendFormat(" value=\"False\"/>");
            if (!string.IsNullOrEmpty(text))
                html.AppendFormat("<label for=\"{1}\">{0}</label>", text, id);
            return html.ToString();
        }

        public static string CheckBox(object isChecked, string name, object props)
        {
            return CheckBox(string.Empty, isChecked, name, props);
        }

        /// <summary>
        /// 一个拥有name,value的checkbox
        /// </summary>
        /// <param name="name">名字</param>
        /// <param name="value">值</param>
        /// <param name="isChecked">是否选中</param>
        /// <returns></returns>
        public static string CheckBox(object isChecked, string name, object value, object text)
        {
            StringBuilder html = new StringBuilder();
            var id = "ck-" + NtUtility.GenRandNum();
            html.AppendFormat("<input type=\"checkbox\" id=\"{2}\" name=\"{0}\" value=\"{1}\"", name, value, id);
            if (Convert.ToBoolean(isChecked))
                html.Append(" checked=\"checked\"");
            html.Append("/>");
            html.AppendFormat("<label for=\"{0}\">{1}</label>", id, text);
            return html.ToString();
        }

        public static string CheckBoxList(IList<ListItem> data, string name, object props)
        {
            if (data == null)
                return string.Empty;
            StringBuilder html = new StringBuilder();
            html.Append("<div");
            AppendAttrs(html, props);
            html.Append("><ul>");
            foreach (var item in data)
            {
                html.Append("<li>");
                html.AppendFormat("<input type=\"checkbox\" value=\"{1}\" name=\"{0}\"", name, item.Value);
                if (item.Selected)
                {
                    html.AppendFormat(" checked=\"checked\"");
                }
                html.Append("/>");
                html.AppendFormat("<label>{0}</label>", item.Text);
                html.Append("</li>");
            }
            html.Append("</ul></div>");
            return html.ToString();
        }

        public static string RadioBoxList(IList<ListItem> data, string name)
        {
            if (data == null)
                return string.Empty;
            StringBuilder html = new StringBuilder();
            foreach (var item in data)
            {
                html.AppendFormat("<input type=\"radio\" value=\"{0}\" name=\"{1}\"", item.Value, name);
                if (item.Selected)
                {
                    html.AppendFormat(" checked=\"checked\"");
                }
                html.Append("/>");
                html.AppendFormat("<label>{0}</label>", item.Text);
            }
            return html.ToString();
        }

        public static string RadioBoxList(string text0, string text1, object currentSelected, string name)
        {
            IList<ListItem> data = new List<ListItem>();
            data.Add(new ListItem(text0, "True"));
            data.Add(new ListItem(text1, "False"));
            if (Convert.ToBoolean(currentSelected))
                data[0].Selected = true;
            else
                data[1].Selected = true;
            return RadioBoxList(data, name);
        }

        private static void AppendAttrs(StringBuilder html, object props)
        {
            foreach (var item in props.GetType().GetProperties())
            {
                if (item.Name.StartsWith("_"))
                    html.AppendFormat(" {0}=\"{1}\"", item.Name.Remove(0, 1), item.GetValue(props, null));
                else
                    html.AppendFormat(" {0}=\"{1}\"", item.Name, item.GetValue(props, null));
            }
        }
    }
}
