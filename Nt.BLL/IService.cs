using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Nt.BLL
{
    public interface IService
    {
        void Delete(int id);
        void Delete(string ids);        
    }
}
