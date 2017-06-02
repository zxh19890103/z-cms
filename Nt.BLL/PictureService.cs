using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Drawing;
using Nt.Model;
using System.Data;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using Nt.BLL.Helper;
using Nt.DAL.Helper;
using System.Configuration;
using System.Text.RegularExpressions;


namespace Nt.BLL
{
    public class PictureService : BaseService<Nt_Picture>
    {
        public const string UPLOAD_FILE_DIR = "/Upload/Product-Pictures/";
        public const string NO_IMAGE = "/Upload/Product-Pictures/no-image.gif";
        public const string ALLOWED_PIC_FORMAT = ".jpg|.bmp|.png|.gif|.jpeg";

        #region props

        /// <summary>
        /// 上传的图片的上限
        /// </summary>
        public int MaxLength
        {
            get
            {
                if (ConfigurationManager.AppSettings["max.length.of.upload"] != null)
                {
                    return Convert.ToInt32(
                        ConfigurationManager.AppSettings["max.length.of.upload"]
                        );
                }
                return 1 * 1024 * 1024;//1M
            }
        }

        /// <summary>
        /// 上传根目录
        /// </summary>
        public string UploadFileRootDir
        {
            get
            {
                if (ConfigurationManager.AppSettings["upload.product.pictures.directory.path"] != null)
                {
                    return ConfigurationManager.AppSettings["upload.product.pictures.directory.path"];
                }
                return UPLOAD_FILE_DIR;
            }
        }

        /// <summary>
        /// 允许的上传格式
        /// </summary>
        public string AllowedPicturesUploadFormats
        {
            get
            {
                if (ConfigurationManager.AppSettings["upload.product.pictures.formats"] != null)
                {
                    return ConfigurationManager.AppSettings["upload.product.pictures.formats"];
                }
                return ALLOWED_PIC_FORMAT;
            }
        }

        #endregion

        #region Db Access

        public string GetPictureUrl(int id)
        {
            object obj = GetScalar(id, "PictureUrl");
            if (obj != null)
                return obj.ToString();
            return NO_IMAGE;
        }

        public void SetPictureToSpecifiedValue(string ids, string value)
        {
            if (string.IsNullOrEmpty(ids))
                return;
            string sql = string.Format("Update [Nt_Picture] Set PictureUrl='{0}' Where Id in ({1})", value, ids);
            SqlHelper.ExecuteNonQuery(sql);
        }

        public void SetPictureUrlToNull(object ids)
        {
            SetPictureToSpecifiedValue(ids.ToString(), NO_IMAGE);
        }

        /// <summary>
        /// 将指定id的记录的PictureUrl设置为指定的值
        /// </summary>
        /// <param name="id">记录id</param>
        /// <param name="url">图片绝对Url</param>
        public void SetPictureUrl(object ids, string url)
        {
            SetPictureToSpecifiedValue(ids.ToString(), url);
        }

        #endregion

        /// <summary>
        /// 上传Ajax图片
        /// </summary>
        /// <returns>返回文件的路劲</returns>
        public string AsyncUpload()
        {
            var request = System.Web.HttpContext.Current.Request;
            int maxLength = MaxLength;
            Stream stream = null;
            var fileName = "";
            HttpPostedFile httpPostedFile = request.Files["imgFile"];
            if (httpPostedFile == null)
                throw new ArgumentException("没有载入的文件");
            if (httpPostedFile.ContentLength > maxLength)
                throw new Exception("图片大小不能超过" + maxLength / 1024 + "K");
            stream = httpPostedFile.InputStream;
            fileName = Path.GetFileName(httpPostedFile.FileName);
            var fileExtension = Path.GetExtension(fileName).ToLower();
            if (!AllowedPicturesUploadFormats.Split('|')
                .Contains(fileExtension))
                throw new Exception(string.Format(
                    "不允许上传格式除{0}以外的图片", AllowedPicturesUploadFormats));
            fileExtension = fileExtension.ToLowerInvariant();
            string newFileName = GetNewFileName(fileExtension);
            string currentDir = UploadFileRootDir + DateTime.Now.ToString("yyyyMM") + "/";
            string dirpath = WebHelper.MapPath(currentDir);
            if (!Directory.Exists(dirpath))
                Directory.CreateDirectory(dirpath);
            string filePath = dirpath + newFileName;
            httpPostedFile.SaveAs(filePath);
            return currentDir + newFileName;
        }

        #region GetPictureUrl
        /// <summary>
        /// 根据给定的originalImagePath获取指定高度的图片的路径
        /// </summary>
        /// <param name="originalImagePath">相对地址</param>
        /// <param name="height">指定高度，匡宽度按比例</param>
        /// <returns>图片路径</returns>
        public string GetPictureUrl(string originalImagePath, int size, bool byHeight)
        {
            int width = Int32.MaxValue;
            int height = Int32.MaxValue;
            string mode = byHeight ? "H" : "W";
            if (byHeight)
                height = size;
            else
                width = size;
            return GetPictureUrl(originalImagePath, width, height, mode);
        }

        /// <summary>
        /// 缩略图
        /// </summary>
        /// <param name="originalImagePath">原图的路径</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <param name="mode">HW  CUT H W</param>
        /// <returns></returns>
        public string GetPictureUrl(string originalImagePath, int width, int height, string mode)
        {
            if (string.IsNullOrEmpty(originalImagePath)
                || originalImagePath.Equals(NO_IMAGE, StringComparison.OrdinalIgnoreCase))
                return NO_IMAGE;
            string phy_originalImagePath = WebHelper.MapPath(originalImagePath);
            if (!File.Exists(phy_originalImagePath))
                return NO_IMAGE;

            string thumbnailPath = GetNewThumbnailPath(originalImagePath, height, width, mode);
            string phy_thumbnailPath = WebHelper.MapPath(thumbnailPath);

            if (!File.Exists(phy_thumbnailPath))
                MakeThumbnail(phy_originalImagePath, phy_thumbnailPath, width, height, mode);
            return thumbnailPath;
        }

        /// <summary>
        /// 图片水印
        /// </summary>
        /// <param name="originalImagePath">原始图片</param>
        /// <param name="rDstImgPath">水印图</param>
        /// <param name="pos">水印图的位置(10-70:10)</param>
        /// <param name="alpha">不透明度</param>
        /// <returns></returns>
        public string GetPictureUrl(string originalImagePath, string markImgPath, int pos, float alpha)
        {
            return GetPictureUrl(originalImagePath, markImgPath, string.Empty, pos, alpha);
        }

        /// <summary>
        /// 图片和文字水印
        /// </summary>
        /// <param name="originalImagePath">原始图片</param>
        /// <param name="rDstImgPath">水印图</param>
        /// <param name="text">水印文字</param>
        /// <param name="pos">水印图的位置(10-70:10)</param>
        /// <param name="alpha">不透明度</param>
        /// <returns></returns>
        public string GetPictureUrl(string originalImagePath, string markImgPath, string text, int pos, float alpha)
        {
            if (string.IsNullOrEmpty(originalImagePath)
               || originalImagePath.Equals(NO_IMAGE, StringComparison.OrdinalIgnoreCase))
                return NO_IMAGE;

            string phy_originalImagePath = WebHelper.MapPath(originalImagePath);
            if (!File.Exists(phy_originalImagePath))
                return NO_IMAGE;

            string phy_markImgPath = WebHelper.MapPath(markImgPath);
            if (!File.Exists(phy_markImgPath))//水印图不存在
                return originalImagePath;

            string markPath = GetNewMarkImgPath(originalImagePath);
            string phy_markPath = WebHelper.MapPath(markPath);
            if (!File.Exists(phy_markPath))
                BuildWatermark(
                    phy_originalImagePath,
                    phy_markImgPath,
                    text,
                    phy_markPath,
                    pos,
                    alpha);
            return markPath;
        }

        /// <summary>
        /// 文字水印
        /// </summary>
        /// <param name="originalImagePath">原始图片</param>
        /// <param name="text">水印文字</param>
        /// <param name="fontFamily">字体</param>
        /// <param name="width">文本框的宽</param>
        /// <param name="height">文本框的高度</param>
        /// <param name="pos">位置，1:左上,2:居上,3:右上,4:居中,5:左下,6:居下,7:右下,默认为居中</param>
        /// <returns></returns>
        public string GetPictureUrl(string originalImagePath, string text, string fontFamily, int width, int height, int pos)
        {
            if (string.IsNullOrEmpty(originalImagePath)
                || originalImagePath.Equals(NO_IMAGE, StringComparison.OrdinalIgnoreCase))
                return NO_IMAGE;
            string phy_originalImagePath = WebHelper.MapPath(originalImagePath);
            if (!File.Exists(phy_originalImagePath))
                return NO_IMAGE;

            if (string.IsNullOrEmpty(text))
                return originalImagePath;

            string markPath = GetNewMarkTextPath(originalImagePath);
            string phy_markPath = WebHelper.MapPath(markPath);

            if (!File.Exists(phy_markPath))
                BuildWatermarkText(
                    phy_originalImagePath,
                    phy_markPath,
                    text,
                    fontFamily,
                    width,
                    height,
                    pos
                    );

            return markPath;
        }

        #endregion

        #region 获取文件名

        /// <summary>
        /// 获取新的文件名
        /// </summary>
        /// <param name="fileExtention">文件后缀名，不带.</param>
        /// <returns></returns>
        private string GetNewFileName(string fileExtention)
        {
            string name = DateTime.Now.ToString("yyyyMMddhhmmss");
            return string.Format("{0}{1}", name, fileExtention);
        }

        /// <summary>
        /// 按指定高度获取图片缩略图的路径
        /// </summary>
        /// <param name="originalImagePath">原始图片的路径（相对）</param>
        /// <param name="height">高度，匡宽度按比例</param>
        /// <returns>缩略图的路径</returns>
        string GetNewThumbnailPath(string originalImagePath, int size, bool byHeight)
        {
            int pos = originalImagePath.LastIndexOf('.');
            string fileExtention = originalImagePath.Substring(pos);
            string thumbnailPath = string.Format("{0}_{1}{3}{2}",
                originalImagePath.Substring(0, pos), size, fileExtention, byHeight ? "H" : "W");
            return thumbnailPath;
        }

        /// <summary>
        /// 以上两个方法的综合
        /// </summary>
        /// <returns></returns>
        string GetNewThumbnailPath(string originalImagePath, int height, int width, string mode)
        {
            switch (mode)
            {
                case "H":
                    return GetNewThumbnailPath(originalImagePath, height, true);
                case "W":
                    return GetNewThumbnailPath(originalImagePath, width, false);
                case "HW":
                case "CUT":
                case "CUTA":
                    int pos = originalImagePath.LastIndexOf('.');
                    string fileExtention = originalImagePath.Substring(pos);
                    string thumbnailPath = string.Format("{0}_{1}x{2}{3}{4}",
                        originalImagePath.Substring(0, pos), width, height, mode, fileExtention);
                    return thumbnailPath;
                default:
                    throw new Exception(mode + "不是合法的裁剪模式.");
            }
        }

        /// <summary>
        /// 获取缩略图Url
        /// </summary>
        /// <param name="originalImagePath">原图绝对路径</param>
        /// <param name="size">大小</param>
        /// <param name="byHeight">是否按高</param>
        /// <returns></returns>
        public string GetThumbnailUrl(string originalImagePath, int size, bool byHeight)
        {
            if (string.IsNullOrEmpty(originalImagePath) 
                || originalImagePath.Equals(NO_IMAGE,StringComparison.OrdinalIgnoreCase))
            {
                return NO_IMAGE;
            }
            string path = GetNewThumbnailPath(originalImagePath, size, byHeight);
            if (!File.Exists(WebHelper.MapPath(path)))
                return originalImagePath;
            return path;
        }

        /// <summary>
        ///  获取缩略图Url
        /// </summary>
        /// <param name="originalImagePath">原图绝对路径</param>
        /// <param name="height">高</param>
        /// <param name="width">宽</param>
        /// <param name="mode">模式</param>
        /// <returns></returns>
        public string GetThumbnailUrl(string originalImagePath, int height, int width, string mode)
        {
            if (string.IsNullOrEmpty(originalImagePath)
                || originalImagePath == NO_IMAGE)
            {
                return NO_IMAGE;
            }
            string path = GetNewThumbnailPath(originalImagePath, height, width, mode);
            if (!File.Exists(WebHelper.MapPath(path)))
                return originalImagePath;
            return path;
        }

        /// <summary>
        /// 获取打上水印的图片路径(相对)
        /// </summary>
        /// <param name="originalImagePath">原图的路径（相对）</param>
        /// <returns></returns>
        public string GetNewMarkImgPath(string originalImagePath)
        {
            int pos = originalImagePath.LastIndexOf('.');
            string fileExtention = originalImagePath.Substring(pos);
            string thumbnailPath = string.Format("{0}_{1}{2}", originalImagePath.Substring(0, pos), "mark", fileExtention);
            return thumbnailPath;
        }

        public string GetMarkImgUrl(string originalImagePath)
        {
            if (string.IsNullOrEmpty(originalImagePath)
                || originalImagePath.Equals(NO_IMAGE, StringComparison.OrdinalIgnoreCase))
                return NO_IMAGE;
            int pos = originalImagePath.LastIndexOf('.');
            string fileExtention = originalImagePath.Substring(pos);
            string markPath = string.Format("{0}_{1}{2}", originalImagePath.Substring(0, pos), "mark", fileExtention);
            if (!File.Exists(WebHelper.MapPath(markPath)))
                return originalImagePath;
            return markPath;
        }

        /// <summary>
        /// 文字水印
        /// </summary>
        /// <param name="originalImagePath">原图的路径（相对）</param>
        /// <returns></returns>
        public string GetNewMarkTextPath(string originalImagePath)
        {
            int pos = originalImagePath.LastIndexOf('.');
            string fileExtention = originalImagePath.Substring(pos);
            string thumbnailPath = string.Format("{0}_{1}{2}", originalImagePath.Substring(0, pos), "text", fileExtention);
            return thumbnailPath;
        }

        public string GetMarkTextUrl(string originalImagePath)
        {
            if (string.IsNullOrEmpty(originalImagePath)
              || originalImagePath.Equals(NO_IMAGE, StringComparison.OrdinalIgnoreCase))
                return NO_IMAGE;
            int pos = originalImagePath.LastIndexOf('.');
            string fileExtention = originalImagePath.Substring(pos);
            string markPath = string.Format("{0}_{1}{2}", originalImagePath.Substring(0, pos), "text", fileExtention);
            if (!File.Exists(WebHelper.MapPath(markPath)))
                return originalImagePath;
            return markPath;
        }

        #endregion

        #region Delete

        /// <summary>
        /// 删除一个已经存在的文件,以及其所有缩略图
        /// </summary>
        /// <param name="filename">文件的相对路径或绝对路径</param>
        public void DeleteExistedPicture(string pictureUrl)
        {
            if (!string.IsNullOrEmpty(pictureUrl)
                && pictureUrl.Equals(NO_IMAGE, StringComparison.OrdinalIgnoreCase))
                return;
            string pictureFullName = WebHelper.MapPath(pictureUrl);
            if (!File.Exists(pictureFullName))
                return;

            //删除原图
            File.Delete(pictureFullName);
            //删除缩略图
            var pos = pictureUrl.LastIndexOf('/');
            var pos1 = pictureUrl.LastIndexOf('.');
            var pictureUrlWithoutExtention = pictureUrl.Substring(pos + 1, pos1 - pos - 1);
            var searchPattern = string.Format("{0}_*.{1}", pictureUrlWithoutExtention, pictureUrl.Substring(pos1 + 1));
            var dirPath = WebHelper.MapPath(UPLOAD_FILE_DIR);
            foreach (var f in Directory.GetFiles(dirPath, searchPattern, SearchOption.AllDirectories))
                File.Delete(f);
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="id">记录id</param>
        public override void Delete(int id)
        {
            //删除图片
            string pictureUrl = GetPictureUrl(id);
            DeleteExistedPicture(pictureUrl);
            base.Delete(id);
        }

        /// <summary>
        /// 删除一些记录
        /// </summary>
        /// <param name="ids">记录的id集合</param>
        public override void Delete(string ids)
        {
            if (string.IsNullOrEmpty(ids))
                return;
            string sql = string.Format("Select [PictureUrl] From [Nt_Picture] Where [Id] In ({0})", ids);
            DataTable data = SqlHelper.ExecuteDataset(sql).Tables[0];
            foreach (DataRow row in data.Rows)
            {
                var url = row[0].ToString();
                DeleteExistedPicture(url);
            }
            data.Dispose();
            base.Delete(ids);
        }

        /// <summary>
        /// 根据id删除对应的图片
        /// </summary>
        /// <param name="id">记录id</param>
        public void DeletePicture(int id)
        {
            var pictureUrl = GetPictureUrl(id);
            DeleteExistedPicture(pictureUrl);
            SetPictureUrlToNull(id);
        }

        /// <summary>
        ///  根据ids删除对应的图片
        /// </summary>
        /// <param name="ids">逗号隔开</param>
        public void DeletePicture(string ids)
        {
            if (string.IsNullOrEmpty(ids))
                throw new Exception("参数错误.");
            string sql = string.Format("Select [PictureUrl] From [Nt_Picture] Where [Id] In ({0})", ids);
            DataTable data = SqlHelper.ExecuteDataset(sql).Tables[0];
            foreach (DataRow row in data.Rows)
            {
                var url = row[0].ToString();
                if (!string.IsNullOrEmpty(url))
                    DeleteExistedPicture(url);
            }
            SetPictureUrlToNull(ids);
            data.Dispose();
        }

        #endregion

        #region utility
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式 HW,W,H,Cut</param>
        public void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            Image originalImage = Image.FromFile(originalImagePath);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）
                    break;
                case "W"://指定宽，高按比例
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "CUT"://指定高宽裁减
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                case "CUTA"://指定高宽裁减（不变形）自定义
                    if (ow <= towidth && oh <= toheight)
                    {
                        x = -(towidth - ow) / 2;
                        y = -(toheight - oh) / 2;
                        ow = towidth;
                        oh = toheight;
                    }
                    else
                    {
                        if (ow > oh)//宽大于高 
                        {
                            x = 0;
                            y = -(ow - oh) / 2;
                            oh = ow;
                        }
                        else//高大于宽 
                        {
                            y = 0;
                            x = -(oh - ow) / 2;
                            ow = oh;
                        }
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以白色背景色填充
            g.Clear(Color.White);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
             new Rectangle(x, y, ow, oh),
             GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }

        /// <summary>
        /// Creating a Watermarked Photograph with GDI+ for .NET    
        /// </summary>
        /// <param name="rSrcImgPath">原始图片的物理路径</param>
        /// <param name="rMarkImgPath">水印图片的物理路径</param>
        /// <param name="rMarkText">水印文字（不显示水印文字设为空串）</param>
        /// <param name="rDstImgPath">输出合成后的图片的物理路径</param>
        /// <param name="pos">图片水印的位置，1:左上,2:居上,3:右上,4:居中,5:左下,6:居下,7:右下,默认为居中</param>
        /// <param name="alpha">图片水印的不透明度</param>
        private void BuildWatermark(string rSrcImgPath, string rMarkImgPath,
            string rMarkText, string rDstImgPath
            , int pos,
            float alpha)
        {
            #region prepare

            //以下（代码）从一个指定文件创建了一个Image 对象，然后为它的 Width 和 Height定义变量。      
            //这些长度待会被用来建立一个以24 bits 每像素的格式作为颜色数据的Bitmap对象。      
            Image imgPhoto = Image.FromFile(rSrcImgPath);
            int phWidth = imgPhoto.Width;
            int phHeight = imgPhoto.Height;
            Bitmap bmPhoto = new Bitmap(phWidth, phHeight, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(72, 72);
            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            //这个代码载入水印图片，水印图片已经被保存为一个BMP文件，以绿色(A=0,R=0,G=255,B=0)作为背景颜色。      
            //再一次，会为它的Width 和Height定义一个变量
            Image imgWatermark = new Bitmap(rMarkImgPath);
            int wmWidth = imgWatermark.Width;
            int wmHeight = imgWatermark.Height;
            //这个代码以100%它的原始大小绘制imgPhoto 到Graphics 对象的（x=0,y=0）位置。      
            //以后所有的绘图都将发生在原来照片的顶部。      
            grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
            grPhoto.DrawImage(
                 imgPhoto,
                 new Rectangle(0, 0, phWidth, phHeight),
                 0,
                 0,
                 phWidth,
                 phHeight,
                 GraphicsUnit.Pixel);

            #endregion

            #region draw text
            if (!string.IsNullOrEmpty(rMarkText))
            {
                //为了最大化版权信息的大小，我们将测试7种不同的字体大小来决定我们能为我们的照片宽度使用的可能的最大大小。      
                //为了有效地完成这个，我们将定义一个整型数组，接着遍历这些整型值测量不同大小的版权字符串。      
                //一旦我们决定了可能的最大大小，我们就退出循环，绘制文本      
                int[] sizes = new int[] { 16, 14, 12, 10, 8, 6, 4 };
                Font crFont = null;
                SizeF crSize = new SizeF();
                for (int i = 0; i < 7; i++)
                {
                    crFont = new Font("arial", sizes[i],
                          FontStyle.Bold);
                    crSize = grPhoto.MeasureString(rMarkText,
                          crFont);
                    if ((ushort)crSize.Width < (ushort)phWidth)
                        break;
                }
                //因为所有的照片都有各种各样的高度，所以就决定了从图象底部开始的5%的位置开始。      
                //使用rMarkText字符串的高度来决定绘制字符串合适的Y坐标轴。      
                //通过计算图像的中心来决定X轴，然后定义一个StringFormat 对象，设置StringAlignment 为Center。      
                int yPixlesFromBottom = (int)(phHeight * .05);
                float yPosFromBottom = ((phHeight -
                     yPixlesFromBottom) - (crSize.Height / 2));
                float xCenterOfImg = (phWidth / 2);
                StringFormat StrFormat = new StringFormat();
                StrFormat.Alignment = StringAlignment.Center;
                //现在我们已经有了所有所需的位置坐标来使用60%黑色的一个Color(alpha值153)创建一个SolidBrush 。      
                //在偏离右边1像素，底部1像素的合适位置绘制版权字符串。      
                //这段偏离将用来创建阴影效果。使用Brush重复这样一个过程，在前一个绘制的文本顶部绘制同样的文本。      
                SolidBrush semiTransBrush2 =
                     new SolidBrush(Color.FromArgb(153, 0, 0, 0));
                grPhoto.DrawString(rMarkText,
                     crFont,
                     semiTransBrush2,
                     new PointF(xCenterOfImg + 1, yPosFromBottom + 1),
                     StrFormat);
                SolidBrush semiTransBrush = new SolidBrush(
                     Color.FromArgb(153, 255, 255, 255));
                grPhoto.DrawString(rMarkText,
                     crFont,
                     semiTransBrush,
                     new PointF(xCenterOfImg, yPosFromBottom),
                     StrFormat);
            }
            #endregion

            #region draw image
            //根据前面修改后的照片创建一个Bitmap。把这个Bitmap载入到一个新的Graphic对象。      
            Bitmap bmWatermark = new Bitmap(bmPhoto);
            bmWatermark.SetResolution(
                 imgPhoto.HorizontalResolution,
                 imgPhoto.VerticalResolution);
            Graphics grWatermark =
                 Graphics.FromImage(bmWatermark);
            //通过定义一个ImageAttributes 对象并设置它的两个属性，我们就是实现了两个颜色的处理，以达到半透明的水印效果。      
            //处理水印图象的第一步是把背景图案变为透明的(Alpha=0, R=0, G=0, B=0)。我们使用一个Colormap 和定义一个RemapTable来做这个。      
            //就像前面展示的，我的水印被定义为100%绿色背景，我们将搜到这个颜色，然后取代为透明。      
            ImageAttributes imageAttributes =
                 new ImageAttributes();
            ColorMap colorMap = new ColorMap();
            colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
            colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
            ColorMap[] remapTable = { colorMap };
            //第二个颜色处理用来改变水印的不透明性。      
            //通过应用包含提供了坐标的RGBA空间的5x5矩阵来做这个。      
            //通过设定第三行、第三列为0.3f我们就达到了一个不透明的水平。结果是水印会轻微地显示在图象底下一些。      

            imageAttributes.SetRemapTable(remapTable,
                 ColorAdjustType.Bitmap);
            float[][] colorMatrixElements = {       
                                                 new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},      
                                                 new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},      
                                                 new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},      
                                                 new float[] {0.0f,  0.0f,  0.0f,  alpha, 0.0f},      
                                                 new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}      
                                            };
            ColorMatrix wmColorMatrix = new
                 ColorMatrix(colorMatrixElements);
            imageAttributes.SetColorMatrix(wmColorMatrix,
                 ColorMatrixFlag.Default,
                 ColorAdjustType.Bitmap);
            //随着两个颜色处理加入到imageAttributes 对象，我们现在就能在照片右手边上绘制水印了。      
            //我们会偏离10像素到底部，10像素到左边。      
            int markWidth;
            int markHeight;
            //mark比原来的图宽      
            if (phWidth <= wmWidth)
            {
                markWidth = phWidth - 10;
                markHeight = (markWidth * wmHeight) / wmWidth;
            }
            else if (phHeight <= wmHeight)
            {
                markHeight = phHeight - 10;
                markWidth = (markHeight * wmWidth) / wmHeight;
            }
            else
            {
                markWidth = wmWidth;
                markHeight = wmHeight;
            }

            #region position of water mark

            int xPosOfWm = (phWidth - markWidth) / 2;
            int yPosOfWm = (phHeight - markHeight) / 2;

            switch (pos)
            {
                case 1:
                    xPosOfWm = 10;
                    yPosOfWm = 10;
                    break;
                case 2:
                    yPosOfWm = 10;
                    break;
                case 3:
                    xPosOfWm = (phWidth - markWidth) - 10;
                    yPosOfWm = 10;
                    break;
                case 40:
                    break;
                case 5:
                    xPosOfWm = 10;
                    yPosOfWm = (phHeight - markHeight) - 10;
                    break;
                case 6:
                    yPosOfWm = (phHeight - markHeight) - 10;
                    break;
                case 7:
                    xPosOfWm = (phWidth - markWidth) - 10;
                    yPosOfWm = (phHeight - markHeight) - 10;
                    break;
                default:
                    break;
            }
            #endregion

            grWatermark.DrawImage(imgWatermark,
                 new Rectangle(xPosOfWm, yPosOfWm, markWidth,
                 markHeight),
                 0,
                 0,
                 wmWidth,
                 wmHeight,
                 GraphicsUnit.Pixel,
                 imageAttributes);
            //最后的步骤将是使用新的Bitmap取代原来的Image。 销毁两个Graphic对象，然后把Image 保存到文件系统。      

            #endregion

            imgPhoto = bmWatermark;
            grPhoto.Dispose();
            grWatermark.Dispose();
            imgPhoto.Save(rDstImgPath, ImageFormat.Jpeg);
            imgPhoto.Dispose();
            imgWatermark.Dispose();
        }

        /// <summary>
        /// 文字水印
        /// </summary>
        /// <param name="srcPath">原图路径（物理路径）</param>
        /// <param name="targetPath">目标图片路径(物理路径)</param>
        /// <param name="watermarkText">文字</param>
        /// <param name="fontFamily">字体</param>
        /// <param name="width">文本框的宽度</param>
        /// <param name="height">文本框的高度</param>
        /// <param name="pos">位置，1:左上,2:居上,3:右上,4:居中,5:左下,6:居下,7:右下,默认为居中</param>
        public void BuildWatermarkText(string srcPath, string targetPath, string watermarkText, string fontFamily, int width, int height, int pos)
        {
            Image img = Image.FromFile(srcPath);
            Graphics picture = Graphics.FromImage(img);

            // 确定水印文字的字体大小
            int[] sizes = new int[] { 32, 30, 28, 26, 24, 22, 20, 18, 16, 14, 12, 10, 8, 6, 4 };
            Font crFont = null;
            SizeF crSize = new SizeF();
            for (int i = 0; i < sizes.Length; i++)
            {
                crFont = new Font(fontFamily, sizes[i], FontStyle.Bold);
                crSize = picture.MeasureString(watermarkText, crFont);
                if ((ushort)crSize.Width < (ushort)width)
                    break;
            }

            // 生成水印图片（将文字写到图片中）
            Bitmap floatBmp = new Bitmap((int)crSize.Width + 3,
                  (int)crSize.Height + 3, PixelFormat.Format32bppArgb);
            Graphics fg = Graphics.FromImage(floatBmp);
            PointF pt = new PointF(0, 0);
            // 画阴影文字
            Brush TransparentBrush0 = new SolidBrush(Color.FromArgb(255, Color.Black));
            Brush TransparentBrush1 = new SolidBrush(Color.FromArgb(255, Color.Black));
            fg.DrawString(watermarkText, crFont, TransparentBrush0, pt.X, pt.Y + 1);
            fg.DrawString(watermarkText, crFont, TransparentBrush0, pt.X + 1, pt.Y);
            fg.DrawString(watermarkText, crFont, TransparentBrush1, pt.X + 1, pt.Y + 1);
            fg.DrawString(watermarkText, crFont, TransparentBrush1, pt.X, pt.Y + 2);
            fg.DrawString(watermarkText, crFont, TransparentBrush1, pt.X + 2, pt.Y);
            TransparentBrush0.Dispose();
            TransparentBrush1.Dispose();
            // 画文字
            fg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            fg.DrawString(watermarkText,
             crFont, new SolidBrush(Color.White),
             pt.X, pt.Y, StringFormat.GenericDefault);
            // 保存刚才的操作
            fg.Save();
            fg.Dispose();

            #region Calculate Position

            int fw = floatBmp.Width;
            int fh = floatBmp.Height;
            int sw = img.Width;
            int sh = img.Height;

            int x = 0, y = 0;
            switch (pos)
            {
                case 1:
                    x = 5;
                    y = 5;
                    break;
                case 2:
                    x = (sw - fw) / 2 <= 0 ? 5 : (sw - fw) / 2;
                    y = 5;
                    break;
                case 3:
                    x = sw - fw;
                    y = 5;
                    break;
                case 4:
                    x = (sw - fw) / 2 <= 0 ? 5 : (sw - fw) / 2;
                    y = (sh - fh) / 2 <= 0 ? 5 : (sh - fh) / 2;
                    break;
                case 5:
                    x = 5;
                    y = sh - fh;
                    break;
                case 6:
                    x = (sw - fw) / 2 <= 0 ? 5 : (sw - fw) / 2;
                    y = sh - fh;
                    break;
                case 7:
                    x = sw - fw;
                    y = sh - fh;
                    break;
                default:
                    x = (sw - fw) / 2 <= 0 ? 5 : (sw - fw) / 2;
                    y = (sh - fh) / 2 <= 0 ? 5 : (sh - fh) / 2;
                    break;
            }

            #endregion

            picture.DrawImage(floatBmp, new Point(x, y));
            picture.Dispose();
            img.Save(targetPath, ImageFormat.Jpeg);
            img.Dispose();
        }

        #endregion
    }
}
