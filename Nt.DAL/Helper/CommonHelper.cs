using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Nt.DAL.Helper
{
    public class CommonHelper
    {
        /// <summary>
        /// 根据提供的typecode获取获取默认值
        /// </summary>
        /// <param name="typecode">typecode</param>
        /// <returns></returns>
        public static object GetDefaultValueByTypeCode(TypeCode typecode)
        {
            switch (typecode)
            {
                case TypeCode.Boolean:
                    return false;
                case TypeCode.Byte:
                    return Byte.MinValue;
                case TypeCode.Char:
                    return Char.MinValue;
                case TypeCode.DateTime:
                    return DateTime.Now;
                case TypeCode.DBNull:
                    return DBNull.Value;
                case TypeCode.Decimal:
                    return Decimal.MinValue;
                case TypeCode.Double:
                    return Double.MinValue;
                case TypeCode.Empty:
                    return null;
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                    return 0;
                case TypeCode.Object:
                    return new object();
                case TypeCode.SByte:
                    return SByte.MinValue;
                case TypeCode.Single:
                    return Single.MinValue;
                case TypeCode.String:
                    return string.Empty;
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return 0;
                default:
                    return string.Empty;
            }
        }
                
        public static string ModifyCrumbs(string crumbs)
        {
            if (crumbs == string.Empty)
                return string.Empty;
            if (crumbs.Last() == ',')
                return crumbs.Substring(0, crumbs.LastIndexOf(','));
            else
                return crumbs;
        }

        public static string ArrayToStringWithComma(int[] arr)
        {
            if (arr == null || arr.Length < 1)
                return string.Empty;
            return string.Join(",", arr.Select(x => x.ToString()).ToArray());
        }

        public static void SetValueToProp(PropertyInfo pi, object obj, object value)
        {
            TypeCode code = Type.GetTypeCode(pi.PropertyType);
            if (value == null)
                pi.SetValue(obj, GetDefaultValueByTypeCode(code), null);
            else
            {
                #region  set value converted by code
                switch (code)
                {
                    case TypeCode.Boolean:
                        pi.SetValue(obj, Convert.ToBoolean(value), null);
                        break;
                    case TypeCode.Int32:
                        if (value.ToString() == string.Empty)
                            pi.SetValue(obj, 0, null);
                        else
                            pi.SetValue(obj, Convert.ToInt32(value), null);
                        break;
                    case TypeCode.Int64:
                        if (value.ToString() == string.Empty)
                            pi.SetValue(obj, 0, null);
                        else
                            pi.SetValue(obj, Convert.ToInt64(value), null);
                        break;
                    case TypeCode.DateTime:
                        pi.SetValue(obj, Convert.ToDateTime(value), null);
                        break;
                    case TypeCode.String:
                        pi.SetValue(obj, value.ToString(), null);
                        break;
                    case TypeCode.Double:
                        if (value.ToString() == string.Empty)
                            pi.SetValue(obj, 0, null);
                        else
                            pi.SetValue(obj, Convert.ToDouble(value), null);
                        break;
                    case TypeCode.Single:
                        if (value.ToString() == string.Empty)
                            pi.SetValue(obj, 0, null);
                        else
                            pi.SetValue(obj, Convert.ToSingle(value), null);
                        break;
                    default:
                        pi.SetValue(obj, CommonHelper.GetDefaultValueByTypeCode(code), null);
                        break;
                }
                #endregion
            }
        }
    }
}
