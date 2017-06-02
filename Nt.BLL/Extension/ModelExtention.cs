using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nt.Model.SettingModel;
using Nt.BLL.Helper;
using Nt.Model;
using Nt.DAL.Helper;
using Nt.Model.View;

namespace Nt.BLL.Extension
{
    public static class ModelExtention
    {
        /// <summary>
        /// 子页面获取model的数据
        /// </summary>
        /// <param name="m">model</param>
        public static void InitDataFromPage(this BaseModel m)
        {
            var properties = m.GetType().GetProperties();
            foreach (var item in properties)
            {
                var value = System.Web.HttpContext.Current.Request[item.Name];
                DAL.Helper.CommonHelper.SetValueToProp(item, m, value);
            }
        }

        /// <summary>
        /// 初始化Model数据
        /// </summary>
        /// <param name="m">model</param>
        public static void InitData(this BaseModel m)
        {
            var properties = m.GetType().GetProperties();
            foreach (var item in properties)
            {
                TypeCode code = Type.GetTypeCode(item.PropertyType);
                item.SetValue(m,
                    DAL.Helper.CommonHelper.GetDefaultValueByTypeCode(code), null);
            }
        }

    }
}
