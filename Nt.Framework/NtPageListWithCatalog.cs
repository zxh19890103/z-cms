using Nt.Model;
using Nt.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Framework
{
    public class NtPageListWithCatalog<M>:NtPageForList<M>
        where M :BaseLocaleModel, new()
    {
        #region Type Names

        List<SimpleCatalog> _typeNames;
        /// <summary>
        /// 类别名数据
        /// </summary>
        public List<SimpleCatalog> TypeNames
        {
            get
            {
                if (_typeNames == null)
                {
                    var service = _service as BLL.BaseServiceWithCatalog<M>;
                    _typeNames = service.GetCatalogFromXml();
                }
                return _typeNames;
            }
        }

        /// <summary>
        /// type 必须可以转为数值
        /// </summary>
        /// <param name="type">id</param>
        /// <returns></returns>
        public string GetCatalogName(object type)
        {
            int int_type = Convert.ToInt32(type);
            var zzz = TypeNames.FirstOrDefault(x => x.Id == int_type);
            return zzz == null ? "Unknown" : zzz.Name;
        }

        #endregion
    }
}
