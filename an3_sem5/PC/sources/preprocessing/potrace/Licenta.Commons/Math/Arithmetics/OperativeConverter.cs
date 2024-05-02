using Licenta.Commons.Utils;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Licenta.Commons.Math.Arithmetics
{
    public static class OperativeConverter
    {
        static Dictionary<(Type FromType, Type ToType), MethodInfo> Converters;

        static OperativeConverter()
        {
            Converters = (from t in Reflection.GetAllTypesHavingAttribute(typeof(OperativeConverterAttribute))
                          from m in t.GetPublicStaticMethods()
                          where m.ReturnType.DerivesFromOrImplements(typeof(IOperative))
                          let parameters = m.GetParameters()
                          where parameters.Length == 1 && parameters[0].GetType().DerivesFromOrImplements(typeof(IOperative))
                          let key = (parameters[0].GetType(), m.ReturnType)
                          select (key, m)).ToDictionary(_ => _.key, _ => _.m);
        }

        public static T Convert<T>(IOperative x) where T: IOperative
        {
            if (x == null) throw new ArgumentNullException(nameof(x));
            if (x is T) return (T)x.Clone();
            if(!Converters.TryGetValue((x.GetType(), typeof(T)), out MethodInfo converter))
                throw new InvalidCastException($"Cannot convert {x.GetType()} to {typeof(T)}");
            return (T)converter.Invoke(null, new object[] { x });
        }
    }
}
