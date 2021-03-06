﻿using Nt.BLL;
using Nt.DAL.Helper;
using Nt.Framework;
using Nt.Model;
using Nt.Model.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services;

namespace Nt.Pages.Member
{
    public class List: NtPageForList<View_Member>
    {
        protected override void InitRequiredData()
        {
            NeedPagerize = true;
        }
        
        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.MemberManage;
            }
        }

        #region WebService

        [WebMethod]
        public static string ResetPassword(string id)
        {
            int int_id = 0;
            if (!Int32.TryParse(id, out int_id))
            {
                return new NtJson(new
                {
                    error = 0,
                    message = "参数错误"
                }).ToString();
            }

            string sql = "Update [Nt_Member] Set [Password]='670b14728ad9902aecba32e22fa4f6bd' Where [Id]=" + id;
            int i = SqlHelper.ExecuteNonQuery(sql);
            return new NtJson(new
            {
                error = i == 0 ? 1 : 0,
                message = i == 0 ? "重置失败!" : "密码已经重置为000000"
            }).ToString();
        }

        #endregion
    }
}
