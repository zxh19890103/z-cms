using Nt.BLL;
using Nt.Framework;
using Nt.Model;
using Nt.Model.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Nt.Pages.Seo
{
    public class FriendLink : NtPageForList<View_Link>
    {
        PictureService _pictureService;

        protected override void InitRequiredData()
        {
            _pictureService = new PictureService();
        }

        protected override void BeginInitPageData()
        {
            var data = _service.GetList();
            if (WithImage)
            {
                foreach (DataRow r in data.Rows)
                {
                    r["PictureUrl"] = _pictureService.GetPictureUrl(r["PictureUrl"].ToString(),
                        ThumbnailSize, true);
                }
            }
            DataSource = data;
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


        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.FriendLinkManage;
            }
        }
    }
}
