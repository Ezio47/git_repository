﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Springer.Common
{
    public class HttpCommon
    {

        public string HttpPost(string Url, string postDataStr)
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



        public string HttpGet(string Url, string postDataStr)
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
