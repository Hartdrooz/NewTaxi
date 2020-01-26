using NewTaxi.Infrastructure.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace NewTaxi.Infrastructure.Extensions
{
    public static class InfrastructureExtensions
    {
        public static string GetPartitionKeyName<T>(this T entity) where T : class
        {
            PropertyInfo[] props = typeof(T).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                object[] attrs = prop.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    PartitionKeyAttribute pAttr = attr as PartitionKeyAttribute;
                    if (pAttr != null)
                        return prop.Name;
                }
            }

            return string.Empty;
        }
    }
}
