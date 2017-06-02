using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    /// <summary>
    ///those classes which extends this class can be mapped to a table in sql db
    /// </summary>
    public class BaseViewModel :BaseModel
    {
        public int Id { get; set; }
    }
}
