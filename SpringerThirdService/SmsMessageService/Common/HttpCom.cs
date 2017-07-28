using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TLW.Project.SmsMessageWcfService.Common
{
    /// <summary>
    /// http 共通
    /// </summary>
    public class HttpCom
    {
        /// <summary>
        /// 发送短信 =>post请求
        /// </summary>
        /// <param name="weburl">短信地址</param>
        /// <param name="data"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string PushToWeb(string weburl, string data, Encoding encode)
        {
            byte[] byteArray = encode.GetBytes(data);

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(weburl));
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = byteArray.Length;
            Stream newStream = webRequest.GetRequestStream();
            newStream.Write(byteArray, 0, byteArray.Length);
            newStream.Close();

            //接收返回信息：
            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            StreamReader aspx = new StreamReader(response.GetResponseStream(), encode);
            return aspx.ReadToEnd();
        }


        /// <summary>
        /// http协议post请求
        /// </summary>
        /// <param name="Url">请求地址</param>
        /// <param name="postDataStr">请求参数</param>
        /// <returns></returns>
        public static string HttpPost(string Url, string postDataStr)
        {
            //CookieContainer cookie = new CookieContainer();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";//以上信息在监听请求的时候都有的直接复制过来
            request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
            //request.CookieContainer = cookie;

            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
            myStreamWriter.Write(postDataStr);
            myStreamWriter.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //response.Cookies = cookie.GetCookies(response.ResponseUri);
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }


        /// <summary>
        /// http协议get请求
        /// </summary>
        /// <param name="Url">请求地址</param>
        /// <param name="postDataStr">请求参数</param>
        /// <returns></returns>
        public static string HttpGet(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));

            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }
    }
}
