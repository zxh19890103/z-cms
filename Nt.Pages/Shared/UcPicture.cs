using Nt.BLL;
using Nt.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Shared
{
    public class UcPicture : NtUserControl
    {
        PictureService _service;
        int _picture_Id;


        public int UcPictureThumnailSize
        {
            get
            {
                return Convert.ToInt32(
                    global::System.Configuration.ConfigurationManager.AppSettings["admin-uc-picture-thumbnailSize"]);
            }
        }

        public int Picture_Id
        {
            get { return _picture_Id; }
            set
            {
                _service = new PictureService();
                _pictureUrl = _service.GetPictureUrl(value);
                _pictureUrl = _service.GetPictureUrl(_pictureUrl, UcPictureThumnailSize,true);
                if (string.IsNullOrEmpty(_pictureUrl))
                    _pictureUrl = PictureService.NO_IMAGE;
                _picture_Id = value;
            }
        }

        string _pictureUrl = PictureService.NO_IMAGE;
        public string PictureUrl
        {
            get { return _pictureUrl; }
        }
    }
}
