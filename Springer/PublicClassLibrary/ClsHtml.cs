using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PublicClassLibrary
{
    /// <summary>
    /// Html操作类
    /// </summary>
    public class ClsHtml
    {
        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIP()
        {
            string ipAddress = "";
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] == null)
            {
                ipAddress = HttpContext.Current.Request.ServerVariables["Remote_Addr"];
            }
            else
            {
                ipAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            return ipAddress;
        }
    }
}
