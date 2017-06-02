using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nt.Model;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Nt.BLL.Helper;
using Nt.DAL.Helper;
using System.IO;

namespace Nt.BLL
{
    public class ProductCategoryService : BaseServiceAsTree<Nt_ProductCategory>
    {
        #region Fields

        bool _isDownloadable;

        #endregion

        #region Props

        public bool IsDownloadable
        {
            get { return _isDownloadable; }
            set { _isDownloadable = value; }
        }

        #endregion

        #region ctor

        public ProductCategoryService(bool isDownloadable)
        {
            _isDownloadable = isDownloadable;
        }

        public ProductCategoryService()
        {

        }

        public override string BaseFilter
        {
            get
            {
                return string.Format(" IsDownloadable={0} ",
                    IsDownloadable ? 1 : 0);
            }
        }
        #endregion
        
        #region Get Local

        public int[] GetAllIds()
        {
            string sql = string.Format(
                "Select Id From Nt_ProductCategory Where Language_Id={0} And IsDownloadable={1}",
                NtContext.Current.LanguageID, IsDownloadable ? 1 : 0);
            SqlDataReader rs = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
            string ids = string.Empty;
            while (rs.Read())
            {
                if (ids == string.Empty)
                    ids += rs[0].ToString();
                else
                    ids += "," + rs[0].ToString();
            }
            rs.Close();
            rs.Dispose();
            if (ids == string.Empty)
                return new int[0];
            return BLL.Helper.CommonHelper.GetInt32ArrayFromStringWithComma(ids);
        }

        /// <summary>
        /// get all sub category's id array
        /// </summary>
        /// <param name="parent">parent id</param>
        /// <returns></returns>
        public int[] GetSubCategoryIDs(int parent)
        {
            string sql = string.Format(
               "Select Id From Nt_ProductCategory Where Crumbs like '%,{0},%' And ID<>{0} ",
               parent);
            SqlDataReader rs = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text, sql);
            string ids = string.Empty;
            while (rs.Read())
            {
                if (ids == string.Empty)
                    ids += rs[0].ToString();
                else
                    ids += "," + rs[0].ToString();
            }
            rs.Close();
            rs.Dispose();
            if (ids == string.Empty)
                return new int[0];
            return ids.Split(',').Cast<int>().ToArray();
        }
        #endregion
    }
}
