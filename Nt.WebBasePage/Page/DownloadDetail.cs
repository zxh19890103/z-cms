using Nt.BLL;
using Nt.DAL;
using Nt.Model;
using Nt.Model.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Nt.Web
{
    public class DownloadDetail : DetailPageWithCategory<View_Product>
    {
        DataView _productPictures;
        public DataView ProductPictures
        {
            get
            {
                if (_productPictures == null)
                {
                    ProductService service = new ProductService();
                    _productPictures = service.GetProductPictures(NtID, false).DefaultView;
                }
                return _productPictures;
            }
        }

        /// <summary>
        /// 获取单张大图路径
        /// </summary>
        /// <returns></returns>
        public string GetBigPictureUrl()
        {
            int pos = Model.ThumbnailUrl.LastIndexOf('_');
            if (pos == -1)
                return Model.ThumbnailUrl;
            return Model.ThumbnailUrl.Substring(0, pos);
        }

        public override void Seo()
        {
            PageTitle = Model.Title;
            Description = Model.MetaDescription;
            Keywords = Model.MetaKeyWords;
        }

        public override void TryGetModel()
        {
            base.TryGetModel();
            Rating();
            SortID = Model.ProductCategory_Id;
            Crumbs = CommonFactoryAsTree.GetCrumbs<Nt_ProductCategory>(Model.ProductCategory_Id);
            Model.Short = CommonUtility.TextAreaToHtml(Model.Short);
        }
        
        public override int PageType
        {
            get
            {
                return NtConfig.DOWNLOAD;
            }
        }
    }
}
