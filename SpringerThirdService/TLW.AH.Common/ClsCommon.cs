using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TLW.AH.Common
{
    /// <summary>
    /// 共通方法
    /// </summary>
    public class ClsCommon
    {

        /// <summary>
        /// 将一个对象的值赋值给另外一个对象
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="model1">被赋值的对象</param>
        /// <param name="model2">取值的对象</param>
        /// <param name="par">不进行复制的对象名称</param>
        public static void Copy123<T>(T model1, T model2, params string[] par)
        {
            //得到类型
            Type type = typeof(T);
            //取得属性集合
            PropertyInfo[] pi = type.GetProperties();
            foreach (PropertyInfo item in pi)
            {
                if (par.Any(m => m.Equals(item.Name, StringComparison.OrdinalIgnoreCase)))
                    continue;
                else
                    item.SetValue(model1, item.GetValue(model2, null), null);

            }
        }

        /// <summary>
        /// 将一个对象的值赋值给另外一个对象
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="model1">被赋值的对象</param>
        /// <param name="model2">取值的对象</param>
        /// <param name="par">进行复制的对象名称</param>
        public static void Copy<T>(T model1, T model2, params string[] par)
        {
            //得到类型
            Type type = typeof(T);
            //取得属性集合
            PropertyInfo[] pi = type.GetProperties();
            foreach (var item in par)
            {
                var temp = pi.Where(x => x.Name.Equals(item, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                if (temp != null)
                    temp.SetValue(model1, temp.GetValue(model2, null), null);
            }
        }
    }
}
