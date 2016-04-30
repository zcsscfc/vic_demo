using System;
using System.Linq;

namespace ApiFramework.Extensions
{
    public static class ReflectionExtensions
    {
        public static bool HasAttribute<T>(this Type type, bool inherit = true) where T : Attribute
        {
            return type.GetCustomAttributes(inherit).Any(o => o.GetType() == typeof (T));
        }
    }
}