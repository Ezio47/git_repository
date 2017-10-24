using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicClassLibrary
{
    /// <summary>
    /// 后台验证手机号码/整数等
    /// </summary>
    public class SpringerCommonValidate
    {
        /// <summary>
        /// 验证电话号码
        /// </summary>
        /// <param name="telephone"></param>
        /// <returns></returns>
        public static bool IsTelephone(string telephone)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(telephone, @"^(\d{3,4}-)?\d{6,8}$");
        }
        /// <summary>
        /// 验证手机号码
        /// </summary>
        /// <param name="str_handset"></param>
        /// <returns></returns>
        public static bool IsHandset(string str_handset)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_handset, @"^1[3|4|5|7|8]\d{9}$");
        }
        /// <summary>
        /// 验证身份证
        /// </summary>
        /// <param name="idcard"></param>
        /// <returns></returns>
        public static bool IsIDcard(string idcard)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(idcard, @"(^\d{18}$)|(^\d{15}$)");
        }
        /// <summary>
        /// 验证整数
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsInteger(string number)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(number, @"^\+?[1-9][0-9]*$");
        }
        /// <summary>
        /// 验证邮编
        /// </summary>
        /// <param name="str_postalcode"></param>
        /// <returns></returns>
        public static bool IsPostalcode(string str_postalcode)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_postalcode, @"^\d{6}$");
        }
        /// <summary>
        /// 验证是否是数字
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsNumber(string number)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(number, @"^[0-9]*$");
        }
    }
}
