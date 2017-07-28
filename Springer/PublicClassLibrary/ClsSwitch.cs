using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicClassLibrary
{
    /// <summary>
    /// 数据转换操作类
    /// </summary>
    public class ClsSwitch
    {
        /// <summary>
        /// 转换成标准格式日期
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>参见模型</returns>
        public static string SwitDate(object obj)
        {
            if (obj == null)
            {
                return "";
            }
            if (string.IsNullOrEmpty(obj.ToString()) == true)
                return "";

            if (Convert.ToDateTime(obj).ToString("yyyy-MM-dd") == "1900-01-01")
                return "";
            return Convert.ToDateTime(obj).ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 转换为时间类型yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="obj">要转移的时间</param>
        /// <returns>格式化后的时间</returns>
        public static string SwitTM(object obj)
        {
            if (obj==null)
            {
                return "";
            }
            if (string.IsNullOrEmpty(obj.ToString()) == true)
                return "";

            if (Convert.ToDateTime(obj).ToString("yyyy-MM-dd") == "1900-01-01")
                return "";
            return Convert.ToDateTime(obj).ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 转换为时间类型yyyy-MM-dd HH:mm
        /// </summary>
        /// <param name="obj">要转移的时间</param>
        /// <returns>格式化后的时间</returns>
        public static string SwitMN(object obj)
        {
            if (obj == null)
            {
                return "";
            }
            if (string.IsNullOrEmpty(obj.ToString()) == true)
                return "";

            if (Convert.ToDateTime(obj).ToString("yyyy-MM-dd") == "1900-01-01")
                return "";
            return Convert.ToDateTime(obj).ToString("yyyy-MM-dd HH:mm");
        }
        /// <summary>
        /// 比较开始时间是否小于结束时间
        /// </summary>
        /// <param name="dateBegin">开始时间</param>
        /// <param name="dateEnd">结束时间</param>
        /// <param name="type">0 开始日期小于结束日期 1开始日期小于等于结束日期</param>
        /// <returns>true 小于等于结束日期 false 大于等于结束日期</returns>
        public static bool compDate(object dateBegin, object dateEnd,string type)
        {
            //DateTime.Compare(t1, t2) >    0
            //DateTime.Compare(t1,t2),方法获取一个数字,果之小于0,则t1<t2,大于0,则t1>t2, 等于0,则t1=t2
            if (string.IsNullOrEmpty(dateBegin.ToString()) || string.IsNullOrEmpty(dateEnd.ToString()))
                return false;
            if (DateTime.Compare(Convert.ToDateTime(dateBegin), Convert.ToDateTime(dateEnd)) <= 0)
                return true;
            return false;

        }
    }
}
