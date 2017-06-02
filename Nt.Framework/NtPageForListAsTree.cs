using Nt.BLL;
using Nt.BLL.Helper;
using Nt.DAL;
using Nt.DAL.Helper;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace Nt.Framework
{
    public class NtPageForListAsTree<M> : NtPageForList<M>
         where M : BaseTreeModel, new()
    {
        protected override void InitPageData()
        {
            var service = _service as BaseServiceAsTree<M>;
            if (_dataSource == null)
                _dataSource = service.GetList();
            if (Repeater == null)
            {
                Repeater = Page.Master.FindControl("CPH_Body").FindControl("XRepeater") as Repeater;//找到默认的Repeater控件
            }

            if (Repeater != null && _dataSource != null)
            {
                Repeater.DataSource = _dataSource;
                Repeater.DataBind();
            }
        }

        /// <summary>
        /// 获取所有的分类列表作为选项
        /// </summary>
        /// <returns></returns>
        public List<ListItem> GetAllRecordsSelections()
        {
            var data = CommonFactoryAsTree
                .GetDropDownList(_service.TableName, "Display=1");
            data.Insert(0, new ListItem("根级", "0"));
            return data;
        }

        /// <summary>
        /// 输出用于做类别迁移的类别列表数据
        /// </summary>
        /// <param name="from">从</param>
        /// <param name="type">0:news,1:product,2:download,3:course</param>
        /// <returns></returns>
        [WebMethod]
        public static string OutputSelections(string from, string type)
        {
            string tab = typeof(M).Name;
            string filter = string.Format("Display=1 And Language_Id={1} And crumbs not like '%,{0},%' ",
                from, NtContext.Current.LanguageID);
            switch (type)
            {
                case "0":
                case "3":
                    break;
                case "2":
                    filter += " And IsDownloadable=1 ";
                    break;
                case "1":
                    filter += " And IsDownloadable=0 ";
                    break;
                default:
                    return "参数错误";
            }
            var data = CommonFactoryAsTree
                .GetDropDownList(tab, filter);
            data.Insert(0, new ListItem("根级", "0"));
            string html = "";
            html += "<ul>";
            foreach (var item in data)
            {
                html += string.Format("<li><input type=\"hidden\" value=\"{0}\"/>{1}</li>",
                    item.Value, item.Text);
            }
            html += "</ul>";
            return html;
        }

        /// <summary>
        /// 类别迁移
        /// </summary>
        /// <param name="from">自</param>
        /// <param name="to">到</param>
        /// <returns></returns>
        [WebMethod]
        public static string TreeMigrate(string from, string to)
        {
            BaseServiceAsTree<M> service = NtEngine.GetService<M>() as BaseServiceAsTree<M>;
            try
            {
                int int_from = 0;
                int int_to = 0;
                if ((Int32.TryParse(from, out int_from)
                    && Int32.TryParse(to, out int_to))
                    && int_from > 0)
                {
                    service.TreeMigrate(int_from, int_to);
                }
                else
                {
                    return
                        new NtJson(
                            new { error = 1, message = "参数错误!" }
                            ).ToString();
                }
            }
            catch (Exception ex)
            {
                return
                   new NtJson(
                       new { error = 1, message = ex.Message }
                       ).ToString();
            }
            return
                new NtJson(
                    new { error = 0, message = "转移成功!" }
                    ).ToString();
        }

    }
}
