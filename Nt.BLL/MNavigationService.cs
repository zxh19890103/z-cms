using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nt.Model;
using System.Data;
using System.Data.SqlClient;
using Nt.BLL.Helper;
using System.Web.UI.WebControls;
using System.Data.Common;
using Nt.DAL;
using System.IO;

namespace Nt.BLL
{
    public class MNavigationService : BaseServiceAsTree<Nt_Mobile_Navigation>
    {
        public void CopyAllDataTo(int[] targetLangs, string[] replaceItems)
        {
            if (targetLangs == null || targetLangs.Length < 1)
                return;

            DataTable tree = GetList();
            _sql.Append("IF OBJECT_ID(N'[dbo].[TempTable]', 'U') IS NOT NULL \r\n DROP TABLE [dbo].[TempTable];");
            ExecuteSql();
            _sql.Append("create table TempTable(depth int,name varchar(255),displayOrder int,display bit,[path] varchar(512))");
            ExecuteSql();
            foreach (DataRow r in tree.Rows)
                _sql.AppendFormat("insert into TempTable(depth,name,displayOrder,display,[path])values({0},'{1}',{2},'{3}','{4}')\r\n"
                    , r["depth"], r["name"], r["displayorder"], r["display"], r["Path"]);

            ExecuteSql();

            string rawcopyScript = File.ReadAllText(WebHelper.MapPath("/App_Data/Script/copy.tree.mnavigation.sql"));
            foreach (int l in targetLangs)
            {
                if (l == NtContext.Current.LanguageID)
                    throw new Exception("A Same Language ID Included In Target Language Ids array!");
                string copyScript = string.Empty;
                copyScript = rawcopyScript.Replace("{lang}", l.ToString());//replace string {lang} to specified language id
                if (replaceItems != null)
                {
                    if (replaceItems.Length % 2 != 0)
                        throw new Exception("Parameter replaceItems is not valid!");

                    int i = 0;
                    for (; i < replaceItems.Length; )
                    {
                        copyScript = copyScript.
                            Replace("{" + replaceItems[i++] + "}", replaceItems[i++]);
                    }
                }

                ExecuteNonQuery(copyScript, null);
            }
            _sql.Append("drop table TempTable");
            ExecuteSql();
        }
    }
}
