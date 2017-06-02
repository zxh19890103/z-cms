using Nt.BLL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Seo
{
    public class FriendLinkEdit : NtPageForEdit<Nt_Link>
    {

        public int PictureID { get { return Model.Picture_Id; } }

        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.FriendLinkManage;
            }
        }

        /// <summary>
        /// 一个值，指示友情链接是否含有图片
        /// </summary>
        public bool WithImage
        {
            get
            {
                var setting = global::System.Configuration.ConfigurationManager.AppSettings["admin-friendlink-with-image"];
                if (!string.IsNullOrEmpty(setting))
                {
                    return Convert.ToBoolean(setting);
                }
                return false;
            }
        }
        
        protected override void InitRequiredData()
        {
            ListUrl = "FriendLink.aspx";
            base.InitRequiredData();
        }
    }
}
