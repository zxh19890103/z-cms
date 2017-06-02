using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nt.Model;
using System.Web.UI.WebControls;

namespace Nt.BLL
{
    public class LanguageService : BaseService<Nt_Language>
    {
        public override void Delete(int id)
        {
            if (id == 1)
            {
                throw new Exception("简体中文作为默认的语言版本不允许被删除!");
            }
            base.Delete(id);
        }

        public override void Delete(string ids)
        {
            int int_id = 0;
            if (!Int32.TryParse(ids, out int_id))
                throw new Exception("参数错误!");
            this.Delete(int_id);
        }
    }
}
