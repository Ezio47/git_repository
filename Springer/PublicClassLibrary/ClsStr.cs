using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections;
using System.Security.Cryptography;
using System.IO;
using System.Web.Security;
namespace PublicClassLibrary
{
    /// <summary>
    /// 字符串操作类
    /// </summary>
    public class ClsStr
    {
        /// <summary>
        /// 根据日期获取星期几
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Week(object str)
        {
            if (string.IsNullOrEmpty(str.ToString()))
                return "";
            
            string[] weekdays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            string week = weekdays[Convert.ToInt32(Convert.ToDateTime(str).DayOfWeek)];
            return week;
        }

        /// <summary>
        /// 根据时间获取上周、本周、下周开始结束时间
        /// </summary>
        /// <param name="str">时间</param>
        /// <returns>上周、本周、下周开始结束时间</returns>
        public static string[] getWeeks(string str)
        {
            string[] arr = new string[6];
            if (string.IsNullOrEmpty(str))
                return arr;
            DateTime dt = Convert.ToDateTime(str);
            int weeknow = Convert.ToInt32(dt.DayOfWeek);
            //因为是以星期一为第一天，所以要判断weeknow等于0时，要向前推6天。  
            weeknow = (weeknow == 0 ? (7 - 1) : (weeknow - 1)); 
            int daydiff = (-1) * weeknow;            
            arr[2] = dt.AddDays(daydiff).ToString("yyyy-MM-dd");//本周开始时间
            arr[3] = DateTime.Parse(arr[2]).AddDays(6).ToString("yyyy-MM-dd");//本周结束时间
            arr[0] = DateTime.Parse(arr[2]).AddDays(-7).ToString("yyyy-MM-dd");//上周开始时间
            arr[1] = DateTime.Parse(arr[2]).AddDays(-1).ToString("yyyy-MM-dd");//上周开始时间
            arr[4] = DateTime.Parse(arr[3]).AddDays(1).ToString("yyyy-MM-dd");//下周开始时间
            arr[5] = DateTime.Parse(arr[3]).AddDays(7).ToString("yyyy-MM-dd");//下周开始时间           
            //string datebegin = System.DateTime.Now.AddDays(daydiff).ToString("yyyyMMdd");
            //string dateend = System.DateTime.Now.AddDays(dayadd).ToString("yyyyMMdd");
            return arr;// datebegin + " - " + dateend; 
        }

        /// <summary>
        /// 判断两个日期相差天数
        /// </summary>
        /// <param name="obj1">开始日期</param>
        /// <param name="obj2">结束日期</param>
        /// <returns>相差天数</returns>
        public static int getDateDiff(object obj1, object obj2)
        {           
           DateTime dt1 = Convert.ToDateTime(obj1);
           DateTime dt2 = Convert.ToDateTime(obj2);
           int days = dt2.Subtract(dt1).Days ;
            return days;
        }

        /// <summary>
        /// 判断两个日期相差分钟数
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public static int getMinutesDiff(object obj1, object obj2)
        {
            DateTime dt1 = Convert.ToDateTime(obj1);
            DateTime dt2 = Convert.ToDateTime(obj2);
            TimeSpan ts = dt2.Subtract(dt1);
            int Minutes = ts.Days * 24 * 60 + ts.Hours * 60 + ts.Minutes;
            return Minutes;
        }

        /// <summary>
        /// 计算相差
        /// </summary>
        /// <param name="obj1">被减数</param>
        /// <param name="obj2">减数</param>
        /// <returns>计算相差</returns>
        public static double getDiff(object obj1, object obj2)
        {
            if (string.IsNullOrEmpty(obj1.ToString()))
                return 0;
            if (string.IsNullOrEmpty(obj2.ToString()))
                return 0;
            return double.Parse(obj1.ToString())- double.Parse(obj2.ToString()) ;
        }

        /// <summary>
        /// 计算百分率
        /// </summary>
        /// <param name="obj1">分子</param>
        /// <param name="obj2">分母</param>
        /// <returns>计算百分率</returns>
        public static double getPercent(object obj1, object obj2)
        {
            if (string.IsNullOrEmpty(obj1.ToString()))
                return 0;
            if (string.IsNullOrEmpty(obj2.ToString()))
                return 0;
            if (double.Parse(obj2.ToString()) == 0)
                return 0;
            return (double.Parse(obj1.ToString()) / double.Parse(obj2.ToString())) * 100; ;
        }

        /// <summary>
        /// 将Model组合成字符串
        /// </summary>
        /// <param name="model">object型</param>
        /// <returns>将Model组合成字符串</returns>
        public static string getModelContent(object model)
        {
            if (model == null) return "";
            StringBuilder sb = new StringBuilder();
            Type type = model.GetType();//assembly.GetType("Reflect_test.PurchaseOrderHeadManageModel", true, true); //命名空间名称 + 类名  
            //创建类的实例  
            //object obj = Activator.CreateInstance(type, true);
            //获取公共属性  
            PropertyInfo[] Propertys = type.GetProperties();
            for (int i = 0; i < Propertys.Length; i++)
            {
                // Propertys[i].SetValue(Propertys[i], i, null); //设置值  
                PropertyInfo pi = type.GetProperty(Propertys[i].Name);
                object val = pi.GetValue(model, null);
                string value = val == null ? "" : val.ToString();
                sb.AppendFormat("{0}:{1}\r\n",Propertys[i].Name,value);
                //sb += Propertys[i].Name + "|" + value + "_"; //获取值  
                // Console.WriteLine("属性名：{0},类型：{1}", Propertys[i].Name, Propertys[i].PropertyType);
            }
            //if (sb.Length > 0) sb = sb.Substring(0, sb.Length - 1);
            return sb.ToString();
        }

        /// <summary>
        /// 获取字符串随机加密
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <returns>字符串随机加密</returns>
        public static string getSystemManMd5(string str)
        {
            string str0 = md5(str).Substring(1, 8);
            string str1 = EncryptA01(str, str0);
            string str2 = str0 + str1;
            return str2;
        }

        /// <summary>
        /// md5加密
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <returns>参见模型</returns>
        public static string md5(string str)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").Substring(8, 16);
            //MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            //byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(str));
            //StringBuilder sBuilder = new StringBuilder();
            //for (int i = 0; i < data.Length; i++)
            //{
            //    sBuilder.Append(data[i].ToString("x2"));
            //}
            //return sBuilder.ToString().Substring(8, 16); 
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="pToEncrypt">要加密的字符串</param>
        /// <param name="sKey">加密密码 8个字符</param>
        /// <returns>加密后的字符串</returns>
        public static string EncryptA01(string pToEncrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            //把字符串放到byte数组中  
            //原来使用的UTF8编码，我改成Unicode编码了，不行  
            byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
            //byte[]  inputByteArray=Encoding.Unicode.GetBytes(pToEncrypt);  
            //建立加密对象的密钥和偏移量  
            //原文使用ASCIIEncoding.ASCII方法的GetBytes方法  
            //使得输入密码必须输入英文文本  
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            //Write  the  byte  array  into  the  crypto  stream  
            //(It  will  end  up  in  the  memory  stream)  
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            //Get  the  data  back  from  the  memory  stream,  and  into  a  string  
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                //Format  as  hex  
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="pToDecrypt">要解密的字符串</param>
        /// <param name="sKey">解密密码 8个字符（需要与加密密码保持一致）</param>
        /// <returns>解密后的字符串</returns>
        public static string DecryptA01(string pToDecrypt, string sKey)
        {
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                //Put  the  input  string  into  the  byte  array  
                byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
                for (int x = 0; x < pToDecrypt.Length / 2; x++)
                {
                    int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                    inputByteArray[x] = (byte)i;
                }
                //建立加密对象的密钥和偏移量，此值重要，不能修改  
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                //Flush  the  data  through  the  crypto  stream  into  the  memory  stream  
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                //Get  the  decrypted  data  back  from  the  memory  stream  
                //建立StringBuild对象，CreateDecrypt使用的是流对象，必须把解密后的文本变成流对象  
                StringBuilder ret = new StringBuilder();
                return System.Text.Encoding.Default.GetString(ms.ToArray());
            }
            catch
            {
                return "";
            }
        }
    }
}
