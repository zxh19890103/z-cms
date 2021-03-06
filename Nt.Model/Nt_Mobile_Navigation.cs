﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class Nt_Mobile_Navigation:BaseTreeModel,ILocaleModel
    {
        public int Language_Id { get; set; }
        public string Path { get; set; }
        public string AnchorTarget { get; set; }
        public bool Display { get; set; }
        public int DisplayOrder { get; set; }
        public string MetaKeyWords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
    }
}
