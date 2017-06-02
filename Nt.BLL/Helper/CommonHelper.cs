using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Text.RegularExpressions;
using System.Web;
using System.ComponentModel;
using System.Globalization;
using System.Security.Cryptography;
using System.Data.SqlClient;
using Nt.DAL.Helper;
using System.IO;
using Nt.Model;
using Nt.Model.View;

namespace Nt.BLL.Helper
{
    public class CommonHelper
    {
        #region Utility

        /// <summary>
        /// 根据id删除图片
        /// </summary>
        /// <param name="pictureIds"></param>
        public static void DeletePictureOnFileSystem(string pictureIds)
        {
            if (string.IsNullOrEmpty(pictureIds))
                return;
            SqlDataReader rs = SqlHelper.ExecuteReader(SqlHelper.GetConnection(),
                CommandType.Text,
                string.Format("Select [PictureUrl] From [Nt_Picture] Where [Id] In ({0})", pictureIds));
            while (rs.Read())
            {
                string pictureUrl = rs[0].ToString();
                string fileFullName = WebHelper.MapPath(pictureUrl);
                if (File.Exists(fileFullName))
                    File.Delete(fileFullName);
                //删除缩略图
                var pos = pictureUrl.LastIndexOf('/');
                var pos1 = pictureUrl.LastIndexOf('.');
                var searchPattern = string.Format("{0}_*.jpg",
                    pictureUrl.Substring(pos + 1, pos1 - pos - 1));
                var dirPath = WebHelper.MapPath(PictureService.UPLOAD_FILE_DIR);
                foreach (var f in Directory.GetFiles(dirPath, searchPattern, SearchOption.AllDirectories))
                    File.Delete(f);
            }
            rs.Close();
            rs.Dispose();
        }

        public static void DeleteAnyFileOnFileSystem(string filename)
        {
            var filefullname = WebHelper.MapPath(filename);
            if (File.Exists(filefullname))
                File.Delete(filefullname);
        }

        public static int UpdateStatus(string table, string ids, object status)
        {
            string sql = string.Format("Update [{2}] Set [Status]={0} Where [Id] in ({1}) ", (Int32)status, ids, table);
            return SqlHelper.ExecuteNonQuery(sql);
        }


        public static int UpdateStatus(string table, string ids, string statusCollection)
        {
            string[] arr_id = ids.Split(',');
            string[] arr_status = statusCollection.Split(',');

            StringBuilder sqlBuilder = new StringBuilder();

            for (int i = 0; i < arr_id.Length; i++)
            {
                sqlBuilder.AppendFormat("Update [{2}] Set [Status]={0} Where [Id]={1}\r\n"
                    , arr_id[i], arr_status[i], table);
            }
            return SqlHelper.ExecuteNonQuery(sqlBuilder.ToString());
        }

        public static string GetServiceName<M>()
            where M : BaseViewModel, new()
        {
            int start = 3; //Nt_
            if (typeof(M).GetInterface("IView") != null)
            {
                start = 5;//View_
            }
            return typeof(M).Name.Substring(start) + "Service";
        }

        public static int[] GetInt32ArrayFromStringWithComma(string str)
        {
            if (string.IsNullOrEmpty(str))
                return new int[0];
            string[] arr_str = str.Split(',');
            int[] arr_int = new int[arr_str.Length];
            for (int i = 0; i < arr_str.Length; i++)
                arr_int[i] = Convert.ToInt32(arr_str[i]);
            return arr_int;
        }

        #endregion

    }
}
