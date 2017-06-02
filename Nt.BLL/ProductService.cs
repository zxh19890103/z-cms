using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nt.Model;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Nt.BLL.Helper;
using System.Data.Common;
using Nt.DAL;
using Nt.DAL.Helper;

namespace Nt.BLL
{
    public class ProductService : BaseService<Nt_Product>
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

        public override string BaseFilter
        {
            get
            {
                return string.Format(" IsDownloadable={0} ",
                    IsDownloadable ? 1 : 0);
            }
        }

        #endregion

        #region Octor
        public ProductService(bool isDownloadable)
        {
            _isDownloadable = isDownloadable;
        }

        public ProductService()
        {

        }

        #endregion

        #region Get

        public DataTable GetProductPictures(int id, bool showHidden)
        {
            object obj = GetScalar(id, "PictureIds");
            if (obj == null)
                return null;
            string picture_ids = obj.ToString();
            if (picture_ids == string.Empty)
                return null;
            if (showHidden)
                return CommonFactory.GetList("Nt_Picture",
                    string.Format("ID in ({0})", picture_ids), "DisplayOrder Desc");
            else
                return CommonFactory.GetList("Nt_Picture",
                    string.Format("ID in ({0}) And Display=1 ", picture_ids), "DisplayOrder Desc");
        }

        /// <summary>
        /// 获取产品的添加字段
        /// </summary>
        /// <param name="id">产品id</param>
        /// <param name="categoryID">当前产品的目录ID</param>
        /// <returns></returns>
        public DataTable GetAdditionalFields(int id, int categoryID, bool showHidden)
        {
            string crumbs = SqlHelper.ExecuteScalar(
                "Select Crumbs From Nt_ProductCategory Where ID=" + categoryID)
                .ToString();
            crumbs = DAL.Helper.CommonHelper.ModifyCrumbs(crumbs);

            string sql = "select  id as field_Id,* " +
            "from [Nt_ProductField] as t0 left JOIN (SELECT Product_Id,Value,ProductField_Id  FROM [dbo].[Nt_ProductFieldValue] where Product_Id=" + id +
            ") as t1 " +
            "on t0.Id=t1.ProductField_Id Where t0.ProductCategory_Id in (" + crumbs + ") ";

            if (!showHidden)
                sql += " And t0.Display=1 ";
            sql += "Order by t0.DisplayOrder desc";

            DataTable fields = SqlHelper.ExecuteDataset(sql).Tables[0];

            return fields;
        }

        #endregion

        #region Delete

        public override void Delete(int id)
        {
            string picture_ids = GetScalar(id, "PictureIds").ToString();
            if (!string.IsNullOrEmpty(picture_ids))
            {
                BLL.Helper.CommonHelper.DeletePictureOnFileSystem(picture_ids);
            }
            AppendSqlToDeleteProductAssociation(picture_ids, "Nt_Picture");
            _sql.AppendFormat("Delete From [Nt_ProductFieldValue] Where [Product_Id]={0}", id);
            base.Delete(id);
        }

        public override void Delete(string ids)
        {
            if (ids == string.Empty)
                return;

            SqlDataReader rs = SqlHelper.ExecuteReader(SqlHelper.GetConnection(), CommandType.Text,
                string.Format("Select PictureIds From Nt_Product Where ID in ({0})", ids));
            while (rs.Read())
            {
                string pictureIds = rs[0].ToString();
                BLL.Helper.CommonHelper.DeletePictureOnFileSystem(pictureIds);
                AppendSqlToDeleteProductAssociation(pictureIds, "Nt_Picture");
            }
            _sql.AppendFormat("Delete From [Nt_ProductFieldValue] Where [Product_Id] in ({0})", ids);
            base.Delete(ids);
        }

        private void AppendSqlToDeleteProductAssociation(string ids, string tableName)
        {
            if (ids == string.Empty)
                return;
            _sql.AppendFormat("Delete From [{1}] Where [Id] In ({0})\r\n", ids, tableName);
        }

        #endregion

        #region Ajax Batch Migration

        public int BatchMigrate(string ids, int to)
        {
            if (string.IsNullOrEmpty(ids))
                throw new Exception("没有可供操作的项.");
            string sql = string.Format("Update [Nt_Product] Set [ProductCategory_Id]={0} Where [Id] In ({1})", to, ids);
            return SqlHelper.ExecuteNonQuery(sql);
        }

        #endregion

    }
}
