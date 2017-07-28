using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PublicClassLibrary.PublicCom
{
    /// <summary>
    /// 反射类
    /// </summary>
    public class ReflectCom
    {
        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="propertyname"></param>
        /// <returns></returns>
        public static string GetObjectPropertyValue<T>(T t, string propertyname)
        {
            Type type = typeof(T);
            PropertyInfo property = type.GetProperty(propertyname);
            if (property == null) return string.Empty;
            object o = property.GetValue(t, null);
            if (o == null) return string.Empty;
            return o.ToString();
        }
    }
}
