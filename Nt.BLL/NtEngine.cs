using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Nt.BLL
{
    public class NtEngine
    {
        public static BaseService<E> GetService<E>(object[] parameters)
            where E : Nt.Model.BaseViewModel, new()
        {
            Type t = typeof(E);
            string entityName = t.Name;
            int start = 3;
            if (entityName.StartsWith("View_"))
                start = 5;
            entityName = entityName.Substring(start);
            string serviceName = "Nt.BLL." + entityName + "Service";
            Assembly assembly = Assembly.Load("Nt.BLL");
            try
            {
                Type type = assembly.GetType(serviceName);
                ConstructorInfo constructor = type.GetConstructor(new Type[0]);
                var service = constructor.Invoke(parameters) as BaseService<E>;
                if (service == null)
                    return new BaseService<E>();
                else
                    return service;
            }
            catch
            {
                return new BaseService<E>();
            }
        }


        public static IService GetServiceJustForDel<E>()
          where E : Nt.Model.BaseViewModel, new()
        {
            Type t = typeof(E);
            string entityName = t.Name;
            int start = 5;
            entityName = entityName.Substring(start);
            string serviceName = "Nt.BLL." + entityName + "Service";
            Assembly assembly = Assembly.Load("Nt.BLL");

            try
            {
                Type type = assembly.GetType(serviceName);
                ConstructorInfo constructor = type.GetConstructor(new Type[0]);
                var obj = constructor.Invoke(new object[0]);
                return obj as IService;
            }
            catch
            {

            }
            return new BaseService<E>();
        }




        public static BaseService<E> GetService<E>()
           where E : Nt.Model.BaseViewModel, new()
        {
            return NtEngine.GetService<E>(new object[0]);
        }
    }
}
