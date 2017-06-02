using Nt.BLL.Helper;
using Nt.DAL.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// MyUtility 的摘要说明
/// </summary>
public class MyUtility
{
    #region singleton

    static MyUtility _current = null;
    static readonly object padlock = new object();

    public static MyUtility C
    {
        get
        {
            lock (padlock)
            {
                if (_current == null)
                {
                    _current = new MyUtility();
                }
                return _current;
            }
        }
    }
    #endregion

    private string cacheName;

    DataTable newsCatalog;

    /// <summary>
    /// {0}=parent
    /// </summary>
    public string CacheNamePattern { get; set; }

    public MyUtility()
    {

    }


    public void SaveImgFromHttp()
    {
    }



    public void OutNewsClasses(int parent, string template)
    {
        OutNewsClasses(parent, template, null);
    }

    /// <summary>
    /// OutNewsClasses
    /// </summary>
    /// <param name="parent">parent id</param>
    /// <param name="template">use {\w+}</param>
    public void OutNewsClasses(int parent, string template, Action noRecord)
    {
        cacheName = string.Format("/html.inc/{0}.htm", CacheNamePattern.Replace("{0}", parent.ToString()));

        //response html
        if (File.Exists(WebHelper.MapPath(cacheName)))
        {
            HttpContext.Current.Response.Write(File.ReadAllText(WebHelper.MapPath(cacheName)));
            return;
        }

        if (newsCatalog == null)
        {
            using (var conn = SqlHelper.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandTimeout = 60;
                cmd.CommandText = string.Format("select * from nt_newscategory where display=1 order by displayorder");
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                newsCatalog = new DataTable();
                adp.Fill(newsCatalog);
                adp.Dispose();
                conn.Close();
            }
        }

        if (newsCatalog.Rows.Count == 0 && noRecord != null)
            noRecord();

        MatchCollection mats = Regex.Matches(template, @"{\w+}");

        string copy = string.Empty;

        StreamWriter sw = new System.IO.StreamWriter(WebHelper.MapPath(cacheName), false, System.Text.Encoding.UTF8);

        foreach (DataRow r in newsCatalog.Select("[parent]=" + parent))
        {
            copy = template;
            foreach (Match mat in mats)
            {
                copy = copy.Replace(
                    mat.Value,
                    r[mat.Value.Substring(1, mat.Length - 2)].ToString());
            }

            HttpContext.Current.Response.Write(copy);//output html here
            sw.Write(copy);
        }

        sw.Flush();
        sw.Close();
        sw.Dispose();

        CacheNamePattern = string.Empty;
        cacheName = string.Empty;
    }

}