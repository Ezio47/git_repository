using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicClassLibrary
{
    /// <summary>
    /// 数据库语句操作类
    /// </summary>
    public class ClsSql
    {
        /// <summary>
        /// 判断字段，如果为空，则添加null
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string saveNullField(string str)
        {
            if (string.IsNullOrEmpty(str))
                return "null";
            else
                return "'" + EncodeSql(str) + "'";
        }

        /// <summary>
        /// 去除ＳＱＬ非法字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns>参见模型</returns>
        public static string EncodeSql(string str)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            //str = str.Replace("'", "'");
            //str = str.Replace("<", "&lt;");
            //str = str.Replace(">", "&gt;");
            str = str.Replace("'", "''");
            //str = str.Replace(" ", "&nbsp;");
            ////str = str.Replace("\n", "<br/>");
            ////str = str.Replace("\r\n", "<br/>");
            //str = str.Replace("select", "selects");
            //str = str.Replace("sp_", "sp__");
            //str = str.Replace("xp_", "xp__");
            //str = str.Replace("+", "＋");
            //str = str.Replace("--", "－－");
            //str = str.Replace("exec", "execs");
            //str = str.Replace("declare", "declares");
            str = str.Trim();
            return str;
        }

        /// <summary>
        /// 将字符串分隔成SQL中In字符型条件
        /// </summary>
        /// <param name="str"></param>
        /// <returns>参见模型</returns>
        public static string SwitchStrToSqlIn(string str)
        {
            if (string.IsNullOrEmpty(str)) return "";
            StringBuilder sb = new StringBuilder();
            string[] arr = str.Split(',');
            for (int i = 0; i < arr.Length; i++)
            {
                if (i > 0)
                    sb.Append(",");
                sb.AppendFormat("'{0}'", arr[i]);
            }
            return sb.ToString();
        }
    }
}
