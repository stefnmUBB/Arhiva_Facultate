using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Licenta.Commons.Utils
{
    public static class Reflection
    {
        public static List<Type> AppTypes;

        private static Dictionary<string, Type> TypeFromFullName;

        static Reflection()
        {
            AppTypes = (from a in AppDomain.CurrentDomain.GetAssemblies()
                        from t in a.GetTypes()
                        select t).Distinct().ToList();            

            TypeFromFullName = AppTypes.Where(_ => !_.FullName.StartsWith("System."))
                .Peek(_ => Debug.WriteLine(_))
                .GroupBy(_ => _.FullName)
                .ToDictionary(_ => _.Key, _ => _.First());
        }

        public static Type GetTypeFromFullName(string name) 
            => TypeFromFullName.TryGetValue(name, out var type) ? type : null;

        public static IEnumerable<Type> GetAllTypesHavingAttribute(Type attrType)
            => AppTypes.Where(t => t.GetCustomAttribute(attrType) != null);

        public static IEnumerable<MethodInfo> GetPublicStaticMethods(this Type type)
            => type.GetMethods(BindingFlags.Static | BindingFlags.Public);

        public static bool DerivesFromOrImplements(this Type type, Type baseType)
        {
            return type.IsSubclassOf(baseType) || (baseType.IsInterface && type.GetInterfaces().Contains(baseType));
        }
    }
}
