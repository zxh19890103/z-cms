using Nt.BLL;
using Nt.DAL;
using Nt.Model;
using Nt.Model.SettingModel;
using Nt.Model.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace Nt.Web
{
    public class Product : ListPageWithCategory<View_Product>
    {

        ProductSettings _settings;
        /// <summary>
        /// 产品设置
        /// </summary>
        public ProductSettings Settings
        {
            get
            {
                if (_settings == null)
                    _settings = SettingService.GetSettingModel<ProductSettings>();
                return _settings;
            }
        }

        PictureService _pictureService;
        
        public override bool TryGetList()
        {
            AddFilter(" IsDownloadable=0 ");
            HandlePageSize("product");
            Crumbs = CommonFactoryAsTree.GetCrumbs<Nt_ProductCategory>(SortID);
            bool res = base.TryGetList();
            if (res && Settings.EnableThumbnail)
            {
                _pictureService = new PictureService();
                foreach (DataRow r in DataList.Rows)
                {
                    r["ThumbnailUrl"] = _pictureService
                        .GetThumbnailUrl(r["ThumbnailUrl"].ToString(),
                        Settings.ThumbnailHeight, Settings.ThumbnailWidth, Settings.ThumbnailMode);
                }
            }
            return res;
        }

        public override int PageType
        {
            get
            {
                return NtConfig.PRODUCT_LIST;
            }
        }
    }
}
