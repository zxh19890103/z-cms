using Nt.BLL;
using Nt.DAL;
using Nt.Model;
using Nt.Model.SettingModel;
using Nt.Model.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Nt.Web
{
    public class ProductDetail : DetailPageWithCategory<View_Product>
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
        
        DataView _productPictures;
        public DataView ProductPictures
        {
            get
            {
                if (_productPictures == null)
                {
                    ProductService service = new ProductService();
                    DataTable data = service.GetProductPictures(NtID, false);
                    if (data != null)
                    {
                        _productPictures = data.DefaultView;
                    }
                }
                return _productPictures;
            }
        }

        DataView _productParameters;
        public DataView ProductParameters
        {
            get
            {
                if (_productParameters == null)
                {
                    ProductService service = new ProductService();
                    _productParameters = service.GetAdditionalFields(NtID,
                        Model.ProductCategory_Id, false).DefaultView;
                }
                return _productParameters;
            }
        }

        /// <summary>
        /// 获取单张大图路径
        /// </summary>
        /// <returns></returns>
        public string GetBigPictureUrl()
        {
            int pos = Model.ThumbnailUrl.LastIndexOf('_');
            int pos2 = Model.ThumbnailUrl.LastIndexOf('.');
            if (pos == -1)
                return Model.ThumbnailUrl;
            return Model.ThumbnailUrl.Substring(0, pos) + Model.ThumbnailUrl.Substring(pos2);
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
                return NtConfig.PRODUCT;
            }
        }

    }
}
