﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Nt.Model
{
    public class Nt_SpecialPage : BaseLocaleModel
    {
        public string Path { get; set; }
        public bool Display { get; set; }
        public int DisplayOrder { get; set; }
        public string Note { get; set; }
    }
}
