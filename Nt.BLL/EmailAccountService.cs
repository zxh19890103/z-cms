using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nt.Model;
using Nt.BLL.Helper;
using Nt.DAL.Helper;

namespace Nt.BLL
{
    public class EmailAccountService : BaseService<Nt_EmailAccount>
    {
        /// <summary>
        ///设置默认使用的邮箱账号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int SetDefualtEmailAccount(string id)
        {
            string sql = string.Format(
                "Update [Nt_EmailAccount] Set [IsDefault]=1 Where [Id]={0}\r\n Update [Nt_EmailAccount] Set [IsDefault]=0 Where [Id]<>{0}\r\n", id);
            return SqlHelper.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 更改邮箱账号密码
        /// </summary>
        /// <param name="id">邮箱账号id</param>
        /// <param name="password">新密码</param>
        /// <returns></returns>
        public int ChangeEmailAccountPassword(string id, string password)
        {
            string sql = string.Format(
               "Update [Nt_EmailAccount] Set [Password]='{1}' Where [Id]={0}", id, password);
            return SqlHelper.ExecuteNonQuery(sql);
        }

    }
}
