using Nt.BLL;
using Nt.Framework;
using Nt.Model;
using Nt.Model.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Nt.Pages.Common
{
    public class Content:NtPageForList<View_Content>
    {

        PictureService _pictureService = null;

        protected override void BeginInitPageData()
        {
            var data = _service.GetList();
            foreach (DataRow item in data.Rows)
            {
                item["PictureUrl"] = _pictureService.GetPictureUrl(item["PictureUrl"].ToString(), ThumbnailSize, true);
            }
            DataSource = data;
        }

        protected override void InitRequiredData()
        {
            _service = new BaseService<View_Content>();
            _pictureService = new PictureService();
        }

        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.ContentManage;
            }
        }
    }
}
