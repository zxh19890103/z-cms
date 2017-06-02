using Nt.Model;
using Nt.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Framework
{
    /// <summary>
    /// all model BaseServiceWithCatalog can use are baselocalemodel 
    /// </summary>
    /// <typeparam name="M"></typeparam>
    public class NtPageEditWithCatalog<M>:NtPageForEdit<M>
        where M:BaseLocaleModel, new()
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
        /// 获取分类名
        /// </summary>
        /// <param name="type">id</param>
        /// <returns></returns>
        public string GetCatalogName(int type)
        {
            var zzz = TypeNames.FirstOrDefault(x => x.Id == type);
            return zzz == null ? "Unknown" : zzz.Name;
        }

        #endregion
    }
}
