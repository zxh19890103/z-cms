using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Web
{
    public class SinglePage : DetailPage<Nt_SinglePage>
    {
        public override void Seo()
        {
            PageTitle = Model.Title;
            Description = Model.MetaDescription;
            Keywords = Model.MetaKeyWords;
        }

        /// <summary>
        /// 设置默认的ID参数，如果通过Request.QueryString获取不到该值
        /// </summary>
        public void SetDefaultIDIfRequestFailed(int id)
        {
            if (string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                NtID = id;
            }
        }

        public override int PageType
        {
            get
            {
                return NtConfig.SINGLEPAGE;
            }
        }
    }
}
