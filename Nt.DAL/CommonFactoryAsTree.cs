using Nt.DAL.Helper;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Nt.DAL
{
    public class CommonFactoryAsTree
    {
        /// <summary>
        /// insert a model to db
        /// </summary>
        /// <param name="m">the model</param>
        /// <returns>the id it returns</returns>
        public static int Insert(BaseTreeModel m)
        {
            int id = CommonFactory.Insert(m);
            string table = m.GetType().Name;
            string sql = string.Empty;
            if (m.Parent == 0)
                sql = string.Format("Update [{0}] Set Depth=0,[Crumbs]='0,{1},' Where id={1}", table, id);
            else
            {
                string sql4parentCrumbs = string.Format("Select Crumbs From [{0}] Where [Id]={1}", table, m.Parent);
                string sql4parentDepth = string.Format("Select Depth From [{0}] Where [Id]={1}", table, m.Parent);
                sql = string.Format(
                    "Update [{0}] Set Depth=({3})+1,Crumbs=({1})+'{2},' Where [Id]={2}",
                    table, sql4parentCrumbs, id, sql4parentDepth);
            }
            SqlHelper.ExecuteNonQuery(sql);
            return id;
        }

        /// <summary>
        /// get category selector order by crumbs,thus its hierarchical structure will be obvious
        /// </summary>
        /// <param name="table">table's name</param>
        /// <returns>listitem list</returns>
        public static List<ListItem> GetDropDownList(string table, string filter)
        {
            var list = new List<ListItem>();
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(filter))
                sql = string.Format(
                "Select Name,Crumbs,Id,Display,[Parent] From {1} Where {0} Order by DisplayOrder",
                filter, table);
            else
                sql = string.Format(
                "Select Name,Crumbs,Id,Display,[Parent] From {0} Order by DisplayOrder",
                table);
            DataTable source = SqlHelper.ExecuteDataset(sql).Tables[0];
            FindSubList(source, 0, list);
            source.Dispose();
            return list;
        }

        static void FindSubList(DataTable source, int pid, List<ListItem> addTo)
        {
            foreach (DataRow r in source.Select("[Parent]=" + pid))
            {
                if (!Convert.ToBoolean(r[3]))
                    continue;
                var crumbs = r[1].ToString();
                addTo.Add(new ListItem(GetFullName(source, crumbs), r[2].ToString()));
                FindSubList(source, Convert.ToInt32(r[2]), addTo);
            }
        }


        /// <summary>
        /// get category name by id
        /// </summary>
        /// <param name="table">table's name</param>
        /// <param name="id">id</param>
        /// <returns>string</returns>
        public static string GetFullName(string table, int id)
        {
            if (id == 0)
                return "根级";
            object crumbs = SqlHelper.ExecuteScalar(string.Format("Select Crumbs From {0} Where id={1}", table, id));
            if (crumbs == null)
                return "??";
            return GetFullName(table, crumbs.ToString());
        }


        public static string GetFullName(string table, string crumbs)
        {
            if (string.IsNullOrEmpty(crumbs))
                return "crumbs=Empty";
            string ids = CommonHelper.ModifyCrumbs(crumbs.ToString());
            string[] arr_ids = ids.Split(',');
            DataTable data = CommonFactory.GetList(table, string.Format("Id in ({0})", ids), "");
            for (int i = 0; i < arr_ids.Length; i++)
            {
                if (arr_ids[i] == "0")
                    arr_ids[i] = "根级";
                else
                {
                    foreach (DataRow r in data.Rows)
                    {
                        if (r["Id"].ToString() == arr_ids[i])
                        {
                            arr_ids[i] = r["Name"].ToString();
                            break;
                        }
                    }
                }
            }
            data.Dispose();
            return string.Join("->", arr_ids);
        }

        /// <summary>
        /// just for the method GetList
        /// </summary>
        /// <param name="data"></param>
        /// <param name="crumbs"></param>
        /// <returns></returns>
        static string GetFullName(DataTable data, string crumbs)
        {
            if (string.IsNullOrEmpty(crumbs))
                return "None Name";
            string[] arr_ids = CommonHelper.ModifyCrumbs(crumbs).Split(',');
            for (int i = 0; i < arr_ids.Length; i++)
            {
                if (arr_ids[i] == "0")
                    arr_ids[i] = "根级";
                else
                {
                    foreach (DataRow r in data.Rows)
                    {
                        if (r["Id"].ToString() == arr_ids[i])
                        {
                            arr_ids[i] = r["Name"].ToString();
                            break;
                        }
                    }
                }
            }
            return string.Join("->", arr_ids);
        }


        public static DataTable GetTree(string table, string filter)
        {
            DataTable source = CommonFactory.GetList(table, filter, "DisplayOrder");
            DataTable clone = source.Clone();
            int currentPid = 0;
            FindSubTree(source, currentPid, clone);
            source.Dispose();
            return clone;
        }

        static void FindSubTree(DataTable source, int pid, DataTable cloneTo)
        {
            foreach (DataRow r in source.Select("[Parent]=" + pid))
            {
                r["Crumbs"] = GetFullName(source, r["Crumbs"].ToString());
                cloneTo.ImportRow(r);
                FindSubTree(source, Convert.ToInt32(r["ID"]), cloneTo);
            }
        }

        /// <summary>
        /// 获取面包屑数据
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="crumbs">面包屑ids</param>
        /// <returns></returns>
        public static List<ListItem> GetCrumbs(string table, string crumbs)
        {
            if (string.IsNullOrEmpty(crumbs))
                return null;
            crumbs = CommonHelper.ModifyCrumbs(crumbs);
            string sql = string.Format("Select Id,Name From {0} Where Id in ({1})", table, crumbs);
            DataTable query = SqlHelper.ExecuteDataset(sql).Tables[0];
            List<ListItem> list = new List<ListItem>();
            foreach (DataRow item in query.Rows)
            {
                list.Add(new ListItem(item[1].ToString(), item[0].ToString()));
            }
            return list;
        }

        public static List<ListItem> GetCrumbs<M>(int categoryID)
            where M:global::Nt.Model.BaseTreeModel,new()
        {
            M m = CommonFactory.GetById<M>(categoryID);
            if (m == null)
                return null;
            string crumbs = m.Crumbs;
            string table = m.GetType().Name;
            return GetCrumbs(table, crumbs);
        }


        /// <summary>
        /// delete by id
        /// </summary>
        /// <param name="table">table's name</param>
        /// <param name="id">id</param>
        /// <returns>number of row deleted</returns>
        public static int Delete(string table, int id)
        {
            string sql = string.Format("Delete From {0} Where Crumbs like '%,{1},%' ", table, id);
            return SqlHelper.ExecuteNonQuery(sql);
        }

    }
}
