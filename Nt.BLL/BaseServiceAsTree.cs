using Nt.BLL.Helper;
using Nt.DAL;
using Nt.DAL.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Nt.BLL
{
    public class BaseServiceAsTree<M> : BaseService<M>
        where M : global::Nt.Model.BaseTreeModel, new()
    {
        public override int Insert(M m)
        {
            return CommonFactoryAsTree.Insert(m);
        }

        public override void Update(M m)
        {
            base.Update(m, new string[] { "Crumbs", "Depth" });
        }

        public override DataTable GetList()
        {
            string filter = GetFilter();
            return CommonFactoryAsTree.GetTree(TableName, filter);
        }

        public override void Delete(int id)
        {
            CommonFactoryAsTree.Delete(TableName, id);
        }

        public override void Delete(string id)
        {
            int int_id = 0;
            if (!Int32.TryParse(id, out int_id))
            {
                throw new Exception("参数错误");
            }
            Delete(int_id);
        }

        public virtual void CopyAllDataTo(int[] targetLangs, string which, string[] replaceItems)
        {
            if (targetLangs == null || targetLangs.Length < 1)
                return;

            DataTable tree = GetList();
            _sql.Append("IF OBJECT_ID(N'[dbo].[TempTable]', 'U') IS NOT NULL \r\n DROP TABLE [dbo].[TempTable];");
            ExecuteSql();
            _sql.Append("create table TempTable(depth int,name varchar(255),displayOrder int,display bit)");
            ExecuteSql();
            foreach (DataRow r in tree.Rows)
                _sql.AppendFormat("insert into TempTable(depth,name,displayOrder,display)values({0},'{1}',{2},'{3}')\r\n"
                    , r["depth"], r["name"], r["displayorder"], r["display"]);

            ExecuteSql();

            string rawcopyScript = File.ReadAllText(WebHelper.MapPath(
                string.Format("/App_Data/Script/copy.tree.{0}.sql", which)));
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

        /// <summary>
        /// 批量转移
        /// </summary>
        /// <param name="from">id，从</param>
        /// <param name="to">id，到</param>
        public void TreeMigrate(int from, int to)
        {
            string tab = TableName;
            if (from == to
                || to == Convert.ToInt32(
                SqlHelper.ExecuteScalar("Select Parent From " + tab + " Where id=" + from)))
                throw new Exception("无效转移!");
            string phy_path = WebHelper.MapPath("/App_Data/Script/tree.migration.sql");
            if (!File.Exists(phy_path))
                throw new Exception("不存在必需的脚本文件!");
            string sql = File.ReadAllText(phy_path);
            sql = sql
                .Replace("{tab}", tab)
                .Replace("{targetID}", from.ToString())
                .Replace("{toParent}", to.ToString());
            foreach (string block in
                sql.Split(new string[] { "GO" }, StringSplitOptions.RemoveEmptyEntries))
            {
                ExecuteNonQuery(block);
            }
        }

        /// <summary>
        /// 有问题
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        bool IsParametersValid(int from, int to)
        {
            string sql = "declare @count int,@res int,@from int,@to int;\r\n" +
                                "set @res=1;set @from=" + from + ";set @to=" + to + ";\r\n" +
                                "set @count=(select COUNT(0) from " + TableName + " where ID=@from);\r\n" +
                                "if @count>0\r\n" +
                                "begin\r\n" +
                                "if @to>0\r\n" +
                                "begin\r\n" +
                                "set @count=(select COUNT(0) from " + TableName + " where ID=@to);\r\n" +
                                "if @count>0\r\n" +
                                "set @res=PATINDEX('%," + to + ",%',(select crumbs from " + TableName + " where id=@from));\r\n" +
                                "end\r\n" +
                                "else\r\n" +
                                "begin\r\n" +
                                "set @res=(select parent from " + TableName + " where id=@from);\r\n" +
                                "if @res=0\r\n" +
                                "set @res=1;\r\n" +
                                "end\r\n" +
                                "end\r\n" +
                                "select @res;";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(sql)) == 0;
        }

    }
}
