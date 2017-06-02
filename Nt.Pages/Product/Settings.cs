using Nt.BLL;
using Nt.Framework;
using Nt.Model.SettingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Pages.Product
{
    public class Settings : NtPageForSetting<ProductSettings>
    {
        public int PictureID { get { return Model.Picture_Id; } }

        public readonly string[] FontFamilyProvider = new string[] { "Arial", "System", "仿宋", "黑体", "楷体", "Times New Roman", "微软雅黑", "宋体" };

        public readonly string[] PositionProvider = new string[] { "左上", "居上", "右上", "居中", "左下", "居下", "右下" };

        public readonly string[] ThumnailModeProvider = new string[] { "HW", "H", "W", "CUT", "CUTA" };
        public readonly string[] ThumnailModeDescriptionProvider = new string[] { "指定宽高(可能变形)", "指定高缩小", "指定宽缩小", "按指定宽高剪切", "指定高宽裁减（不变形）自定义" };

        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.ProductSettings;
            }
        }

    }
}
