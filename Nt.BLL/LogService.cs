using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nt.Model;
using Nt.BLL.Helper;

namespace Nt.BLL
{
    public class LogService : BaseService<Nt_Log>
    {

        public int UserID { get; set; }

        public void Log(string description)
        {
            Nt_Log log = new Nt_Log();
            log.UserID = UserID;
            log.LoginIP = WebHelper.GetIP();
            log.AddDate = DateTime.Now;
            log.Description = description;
            Insert(log);
        }

    }
}
