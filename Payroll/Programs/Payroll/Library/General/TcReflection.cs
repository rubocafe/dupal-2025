using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishnatha
// 2015-11-04

namespace Payroll.Library.General
{
    public class TcReflection
    {
        public static Dictionary<string, PropertyInfo> GetPublicReadWriteProperties(Type type)
        {
            var properties = type.GetProperties();
            var dictionary = properties.Where(p => p.CanRead && p.CanWrite).ToDictionary(p => p.Name.ToUpper());
            return dictionary;
        }

        public static object GetPropValue(object src, string propName)
        {
            object value = null;
            var property = src.GetType().GetProperty(propName);
            if (property != null)
            {
                value = property.GetValue(src, null);
            }

            return value;
        }

        internal static object GetPropValue(object src, PropertyInfo property)
        {
            object value = null;
            if (property != null)
            {
                value = property.GetValue(src, null);
            }

            return value;
        }
    }
}
