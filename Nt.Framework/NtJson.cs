using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;
using System.Collections;
using Nt.Model;

namespace Nt.Framework
{
    public class NtJson
    {
        Hashtable hash;
        public NtJson(object data)
        {
            hash = new Hashtable();
            foreach (var item in data.GetType().GetProperties())
            {
                hash[item.Name] = item.GetValue(data, null);
            }
        }

        public NtJson(BaseViewModel data)
        {
            hash = new Hashtable();
            foreach (var item in data.GetType().GetProperties())
            {
                var code = Type.GetTypeCode(item.PropertyType);
                hash[item.Name] = item.GetValue(data, null);
            }
        }

        public Hashtable Json
        {
            get { return hash; }
        }

        public override string ToString()
        {
            return JsonMapper.ToJson(hash);
        }
    }
}
