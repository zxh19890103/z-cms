using Nt.DAL.Helper;
using Nt.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Nt.Pages.Seo
{
    public class ColumnsSeo : NtPage
    {
        private DataTable _dataSourse;

        public DataTable DataSource
        {
            get
            {
                if (_dataSourse == null)
                {
                    _dataSourse = SqlHelper.ExecuteDataset(
                        string.Format("Select Name,ID,MetaTitle,MetaKeywords,MetaDescription From [Nt_Navigation] Where Parent=0 And Language_Id=" + LanguageID)).Tables[0];
                }
                return _dataSourse;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            if (IsHttpPost)
            {
                string ids = Request.Form["NavigationID"];
                if (!string.IsNullOrEmpty(ids))
                {
                    string[] arr_ids = ids.Split(',');
                    StringBuilder sqlBuilder = new StringBuilder();
                    SqlParameter[] parameters = new SqlParameter[3 * arr_ids.Length];
                    for (int i = 0; i < arr_ids.Length; i++)
                    {
                        int j = 3 * i;
                        parameters[j] = new SqlParameter("@MetaTitle" + arr_ids[i], Request.Form["MetaTitle" + arr_ids[i]]);
                        parameters[j + 1] = new SqlParameter("@MetaKeywords" + arr_ids[i], Request.Form["MetaKeywords" + arr_ids[i]]);
                        parameters[j + 2] = new SqlParameter("@MetaDescription" + arr_ids[i], Request.Form["MetaDescription" + arr_ids[i]]);
                        sqlBuilder.AppendFormat("Update [Nt_Navigation] Set MetaTitle=@MetaTitle{0},MetaKeywords=@MetaKeywords{0},MetaDescription=@MetaDescription{0} Where ID={0}\r\n", arr_ids[i]);
                    }
                    SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.Text, sqlBuilder.ToString(), parameters);
                    ReLoadByScript("保存成功");
                }
            }
            base.OnLoad(e);
        }

    }
}
