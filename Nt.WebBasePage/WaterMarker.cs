using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nt.BLL;
using Nt.Model.SettingModel;
using System.Data.SqlClient;
using Nt.DAL.Helper;
using System.Data;
using System.IO;
using Nt.BLL.Helper;

namespace Nt.Web
{
    public class WaterMarker
    {

        #region singleton
        private static WaterMarker _instance = new WaterMarker();
        static readonly object padlock = new object();
        public static WaterMarker Instance
        {
            get
            {
                return _instance;
            }
        }
        #endregion

        #region fields

        private bool _isRunning = false;
        private int _interval = 5;//请求的时间间隔,秒
        private int _counter = 0;
        private int _collectCircle = 50;//回收周期
        private int _total = 0;

        #endregion

        #region props

        PictureService _picService;
        ProductSettings _productSettings;

        /// <summary>
        /// 清理无效图片
        /// </summary>
        public bool ClearInvalidPicture { get; set; }

        string _idsThatNeedToDel = string.Empty;
        /// <summary>
        /// 需要删除的图片id
        /// </summary>
        public string IdsThatNeedToDel { get { return _idsThatNeedToDel; } }

        /// <summary>
        /// 计数
        /// </summary>
        public int Total { get { return _total; } }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 请求的时间间隔(秒)
        /// </summary>
        public int Interval
        {
            get { return _interval; }
            set { _interval = value; }
        }

        /// <summary>
        /// 是否正在进行静态化
        /// </summary>
        public bool IsRunning
        {
            get { return _isRunning; }
        }

        /// <summary>
        /// 回收周期
        /// </summary>
        public int CollectCircle
        {
            get { return _collectCircle; }
            set { _collectCircle = value; }
        }

        int _action = 1;
        /// <summary>
        /// 操作
        /// 1=生成缩略图
        /// 2=生成水印图
        /// 3=删除缩略图
        /// 4=删除水印图
        /// </summary>
        public int Action
        {
            get { return _action; }
            set
            {
                if (_isRunning)
                    return;
                _action = value;
            }
        }

        string _languageCode;
        /// <summary>
        /// 语言
        /// </summary>
        public string LanguageCode
        {
            get
            {
                return _languageCode;
            }
            set
            {
                if (_isRunning) return;
                _languageCode = value;
            }
        }

        #endregion

        #region methods

        public void Run()
        {
            if (_isRunning)
            {
                Message = "程序正在运行，请稍后再试！";
                return;
            }

            if (string.IsNullOrEmpty(LanguageCode))
            {
                Message = "请提供语言版本符号！";
                return;
            }

            switch (Action)
            {
                case 1:
                    new System.Threading.Thread(GenerateThumbnail).Start();
                    break;
                case 2:
                    new System.Threading.Thread(GenerateWaterMark).Start();
                    break;
                case 3:
                    new System.Threading.Thread(DelAllThumbnail).Start();
                    break;
                case 4:
                    new System.Threading.Thread(DelAllWaterMark).Start();
                    break;
                default:
                    Message = "无效操作!";
                    break;
            }
        }

        void Initialize()
        {
            Message = "空闲";
            _isRunning = true;
            _counter = 0;
            _total = 0;
            _picService = new PictureService();
            _productSettings = SettingService.GetSettingModel<ProductSettings>(LanguageCode);
        }

        void End()
        {
            _counter = 0;
            _isRunning = false;
            _picService = null;
            _productSettings = null;
            if (!string.IsNullOrEmpty(_idsThatNeedToDel))
                SqlHelper.ExecuteNonQuery(
                    string.Format("delete from nt_picture where id in ({0})", _idsThatNeedToDel));
            _idsThatNeedToDel = string.Empty;
        }

        /// <summary>
        /// 生成缩略图
        /// </summary>
        public void GenerateThumbnail()
        {
            Initialize();

            #region 验证

            if (!_productSettings.EnableThumbnail
                && !_productSettings.EnableThumbOnHomePage
                && !_productSettings.EnableThumbOnDetailPage)
            {
                Message = "没有开启生成缩略图";
                End();
                return;
            }

            if (_productSettings.EnableThumbnail)
            {
                if (_productSettings.ThumbnailWidth < 40)
                {
                    Message = "列表页缩略图宽度不得小于40";
                    End();
                    return;
                }
                if (_productSettings.ThumbnailHeight < 30)
                {
                    Message = "列表页缩略图高度不得小于30";
                    End();
                    return;
                }
            }

            if (_productSettings.EnableThumbOnHomePage)
            {
                if (_productSettings.ThumbOnHomePageWidth < 40)
                {
                    Message = "首页缩略图宽度不得小于40";
                    End();
                    return;
                }
                if (_productSettings.ThumbOnHomePageHeight < 30)
                {
                    Message = "首页缩略图高度不得小于30";
                    End();
                    return;
                }
            }

            if (_productSettings.EnableThumbOnDetailPage)
            {
                if (_productSettings.ThumbOnDetailPageWidth < 40)
                {
                    Message = "产品详细页缩略图宽度不得小于40";
                    End();
                    return;
                }
                if (_productSettings.ThumbOnDetailPageHeight < 30)
                {
                    Message = "产品详细页缩略图高度不得小于30";
                    End();
                    return;
                }
            }

            #endregion

            Queue<string> que = new Queue<string>();
            using (SqlDataReader rs = SqlHelper.ExecuteReader(
                SqlHelper.GetConnection(), CommandType.Text, "select id,pictureurl from nt_picture"
                ))
            {
                while (rs.Read())
                {
                    que.Enqueue(rs[0].ToString());//id
                    que.Enqueue(rs[1].ToString());//pictureurl
                }
            }

            int i = 0;
            int count = que.Count / 2;

            while (count > i)
            {
                string id = que.Dequeue();
                string pictureUrl = que.Dequeue();
                string generatedUrl = string.Empty;

                //生成列表缩略图
                if (_productSettings.EnableThumbnail)
                {
                    generatedUrl = _picService.GetPictureUrl(
                        pictureUrl,
                        _productSettings.ThumbnailWidth,
                        _productSettings.ThumbnailHeight,
                        _productSettings.ThumbnailMode);
                    if (!PictureService.NO_IMAGE.Equals(generatedUrl, StringComparison.OrdinalIgnoreCase))
                    {
                        _counter++;
                        _total++;
                        Message = string.Format("生成列表缩略图{0}", generatedUrl);
                    }
                }

                if (_productSettings.EnableThumbOnHomePage)
                {
                    generatedUrl = _picService.GetPictureUrl(
                        pictureUrl,
                        _productSettings.ThumbOnHomePageWidth,
                        _productSettings.ThumbOnHomePageHeight,
                        _productSettings.ThumbOnHomePageMode);
                    if (!PictureService.NO_IMAGE.Equals(generatedUrl, StringComparison.OrdinalIgnoreCase))
                    {
                        _counter++;
                        _total++;
                        Message = string.Format("生成首页缩略图{0}", generatedUrl);
                    }
                }

                if (_counter >= _collectCircle)
                {
                    System.GC.Collect();
                    _counter = 0;
                }

                i++;
            }
            Message = string.Format(
                "缩略图生成完毕，一共生成{0}张图片!", _total);
            End();
        }

        /// <summary>
        /// 生成水印
        /// </summary>
        public void GenerateWaterMark()
        {

            Initialize();

            #region 验证

            if (!_productSettings.EnableImgMark
                && !_productSettings.EnableTextMark)
            {
                Message = "没有开启图片水印或文字水印功能!";
                End();
                return;
            }

            string markImg = _picService.GetPictureUrl(_productSettings.Picture_Id);
            if (_productSettings.EnableImgMark
                && markImg.Equals(PictureService.NO_IMAGE, StringComparison.OrdinalIgnoreCase))
            {
                Message = "没有提供水印图片!";
                End();
                return;
            }

            if (_productSettings.EnableTextMark
                && string.IsNullOrEmpty(_productSettings.TextMark))
            {
                Message = "水印字符串不能为空!";
                End();
                return;
            }

            #endregion

            Queue<string> que = new Queue<string>();

            using (SqlDataReader rs = SqlHelper.ExecuteReader(
                SqlHelper.GetConnection(), CommandType.Text, "select id,pictureurl from nt_picture"
                ))
            {
                while (rs.Read())
                {
                    que.Enqueue(rs[0].ToString());//id
                    que.Enqueue(rs[1].ToString());//pictureurl
                }
            }

            int i = 0;
            int count = que.Count / 2;
            while (count > i)
            {
                string id = que.Dequeue();
                string pictureUrl = que.Dequeue();
                string generatedUrl = string.Empty;

                if (_productSettings.EnableImgMark
                    && _productSettings.EnableTextMark)
                {
                    generatedUrl = _picService.GetPictureUrl(pictureUrl
                        , _productSettings.PictureUrl
                        , _productSettings.TextMark
                       , _productSettings.ImgMarkPosition
                       , _productSettings.ImgMarkAlpha);
                    if (!PictureService.NO_IMAGE.Equals(generatedUrl, StringComparison.OrdinalIgnoreCase))
                    {
                        _counter++;
                        _total++;
                        Message = string.Format("生成图片及文字水印图{0}", generatedUrl);
                    }
                }
                else if (_productSettings.EnableImgMark)
                {
                    generatedUrl = _picService.GetPictureUrl(pictureUrl
                       , _productSettings.PictureUrl
                       , string.Empty
                       , _productSettings.ImgMarkPosition
                       , _productSettings.ImgMarkAlpha);
                    if (!PictureService.NO_IMAGE.Equals(generatedUrl, StringComparison.OrdinalIgnoreCase))
                    {
                        _counter++;
                        _total++;
                        Message = string.Format("生成图片水印图{0}", generatedUrl);
                    }
                }
                else if (_productSettings.EnableTextMark)
                {
                    generatedUrl = _picService.GetPictureUrl(
                        pictureUrl,
                        _productSettings.TextMark,
                        _productSettings.FontFamily,
                        _productSettings.WidthOfTextBox,
                        _productSettings.HeightOfTextBox,
                        _productSettings.Position);
                    if (!PictureService.NO_IMAGE.Equals(generatedUrl, StringComparison.OrdinalIgnoreCase))
                    {
                        _counter++;
                        _total++;
                        Message = string.Format("生成文字水印图{0}", generatedUrl);
                    }
                }

                if (_counter >= _collectCircle)
                {
                    _counter = 0;
                    System.GC.Collect();
                }
                i++;
            }
            End();
        }

        /// <summary>
        /// 删除所有缩略图
        /// </summary>
        public void DelAllThumbnail()
        {
            _isRunning = true;
            _total = 0;
            _picService = new PictureService();
            string dirPathOnDisk = WebHelper.MapPath("/upload/Product-Pictures/");
            string[] searchPatternPart1 = new string[] { "H", "W", "HW", "CUT", "CUTA" };
            string[] searchPatternPart2 = _picService.AllowedPicturesUploadFormats.Split('|');
            string searchPattern = string.Empty;
            foreach (string part1 in searchPatternPart1)
            {
                foreach (string part2 in searchPatternPart2)
                {
                    searchPattern = string.Format("*_*{0}{1}", part1, part2);
                    foreach (string f in
                    Directory.GetFiles(dirPathOnDisk,
                    searchPattern,
                    SearchOption.AllDirectories))
                    {
                        File.Delete(f);
                        _total++;
                    }
                }
            }
            Message = string.Format("成功删除{0}个缩略图文件!", _total);
            _picService = null;
            _isRunning = false;
        }

        /// <summary>
        /// 删除水印图
        /// </summary>
        public void DelAllWaterMark()
        {
            _isRunning = true;
            _total = 0;
            _picService = new PictureService();
            string dirPathOnDisk = WebHelper.MapPath("/upload/Product-Pictures/");
            string[] searchPatternPart1 = new string[] { "text", "mark" };
            string[] searchPatternPart2 = _picService.AllowedPicturesUploadFormats.Split('|');
            string searchPattern = string.Empty;
            foreach (string part1 in searchPatternPart1)
            {
                foreach (string part2 in searchPatternPart2)
                {
                    searchPattern = string.Format("*_{0}{1}", part1, part2);
                    foreach (string f in Directory.GetFiles(
                        dirPathOnDisk, searchPattern, SearchOption.AllDirectories))
                    {
                        File.Delete(f);
                        _total++;
                    }
                }
            }
            Message = string.Format("成功删除{0}个水印图文件!", _total);
            _picService = null;
            _isRunning = false;
        }

        #endregion

    }
}
