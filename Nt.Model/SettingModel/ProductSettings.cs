using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.SettingModel
{
    public class ProductSettings : BaseSettingModel
    {
        /// <summary>
        /// 开启列表页缩略图
        /// </summary>
        public bool EnableThumbnail { get; set; }
        /// <summary>
        /// 列表页缩略图宽
        /// </summary>
        public int ThumbnailWidth { get; set; }
        /// <summary>
        /// 列表页缩略图高
        /// </summary>
        public int ThumbnailHeight { get; set; }

        /// <summary>
        /// 列表页缩略图处理模式
        /// </summary>
        public string ThumbnailMode { get; set; }

        /// <summary>
        /// 开启首页缩略图
        /// </summary>
        public bool EnableThumbOnHomePage { get; set; }
        /// <summary>
        /// 首页缩略图宽
        /// </summary>
        public int ThumbOnHomePageWidth { get; set; }
        /// <summary>
        /// 首页缩略图高
        /// </summary>
        public int ThumbOnHomePageHeight { get; set; }
        /// <summary>
        /// 首页缩略图处理模式
        /// </summary>
        public string ThumbOnHomePageMode { get; set; }

        /// <summary>
        /// 开启产品详细页缩略图
        /// </summary>
        public bool EnableThumbOnDetailPage { get; set; }
        /// <summary>
        /// 首页缩略图宽
        /// </summary>
        public int ThumbOnDetailPageWidth { get; set; }
        /// <summary>
        /// 首页缩略图高
        /// </summary>
        public int ThumbOnDetailPageHeight { get; set; }
        /// <summary>
        /// 首页缩略图处理模式
        /// </summary>
        public string ThumbOnDetailPageMode { get; set; }


        /*文字水印*/
        public bool EnableTextMark { get; set; }
        public string TextMark { get; set; }
        public int Position { get; set; }
        public string FontFamily { get; set; }
        public int WidthOfTextBox { get; set; }
        public int HeightOfTextBox { get; set; }
        
        /*图片水印*/
        public bool EnableImgMark { get; set; }
        public float ImgMarkAlpha { get; set; }
        public int ImgMarkPosition { get; set; }

        public string PictureUrl { get; set; }
        public int Picture_Id { get; set; }
    }
}
