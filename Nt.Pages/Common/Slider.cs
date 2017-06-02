using Nt.BLL;
using Nt.BLL.Helper;
using Nt.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Nt.Pages.Common
{
    public class Slider : NtPage
    {
        const string DATA_SAVE_PATH = "/App_Data/Txt/slider-data-{0}.txt";

        PictureService _service;

        DataTable _sliders;
        public DataTable Sliders { get { return _sliders; } }
        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            string phy_path = WebHelper.MapPath(string.Format(DATA_SAVE_PATH, WorkingLang.LanguageCode));
            if (IsHttpPost)
            {
                string pictureIds = Request.Form["Picture.Id"];

                File.WriteAllText(phy_path, NtUtility.EnsureNotNull(pictureIds));
                ReLoadByScript("保存成功!");
            }
            else
            {
                if (File.Exists(phy_path))
                {
                    string picids = File.ReadAllText(phy_path);
                    if (!string.IsNullOrEmpty(picids))
                    {
                        _service = new PictureService();
                        _sliders = _service.GetList("Id In (" + picids + ")");
                        foreach (DataRow item in _sliders.Rows)
                        {
                            item["PictureUrl"] = _service.GetPictureUrl(item["PictureUrl"].ToString(), ThumbnailSize,true);
                        }
                    }

                    var xrepeater = Page.Master.FindControl("CPH_Body").FindControl("XRepeater") as Repeater;
                    if (xrepeater != null)
                    {
                        xrepeater.DataSource = _sliders;
                        xrepeater.DataBind();
                    }
                }
            }
        }

        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.SliderManage;
            }
        }
    }
}
