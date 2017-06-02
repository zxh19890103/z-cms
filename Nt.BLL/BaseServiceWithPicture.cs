using Nt.BLL.Helper;
using Nt.DAL.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Nt.BLL
{
    public class BaseServiceWithPicture<M> : BaseService<M>
        where M : global::Nt.Model.BaseLocaleModel,new()
    {
        #region Delete

        public override void Delete(int id)
        {
            //删除图片记录
            object raw = this.GetScalar(id, "Picture_Id");
            if (raw != null)
            {
                BLL.Helper.CommonHelper.DeletePictureOnFileSystem(raw.ToString());//删除图片
                _sql.AppendFormat("Delete From [Nt_Picture] Where [Id]={0}\r\n", raw);//删除数据库记录
            }
            base.Delete(id);
        }

        public override void Delete(string ids)
        {
            //删除图片记录
            if (string.IsNullOrEmpty(ids))
                return;

            SqlDataReader rs = SqlHelper.ExecuteReader(SqlHelper.GetConnection(),
                CommandType.Text,
                string.Format("Select Picture_Id From [{0}] Where ID in ({1})", TableName, ids));

            while (rs.Read())
            {
                BLL.Helper.CommonHelper.DeletePictureOnFileSystem(rs[0].ToString());//删除图片
                _sql.AppendFormat("Delete From [Nt_Picture] Where [Id]={0}\r\n", rs[0]);//删除数据库记录
            }
            rs.Close();
            rs.Dispose();
            _sql.AppendFormat("Delete From [{0}] Where ID in ({1}) \r\n", TableName, ids);
            ExecuteSql();
        }

        #endregion
    }
}
