using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PublicClassLibrary.ThirdDockService
{
    /// <summary>
    /// 参数修改--》通知手机段即使更新（信云服务对接）
    /// </summary>
    public class TaskUtil
    {

        private ILog logs = LogHelper.GetInstance();
        private static string gateWayUrl = "http://mctalk.net:8001/ThingTalkServices/TruTalkService.asmx";
        private static string mobileServiceUrl = System.Configuration.ConfigurationManager.AppSettings["mobileServiceUrl"];
        
        static TaskUtil()
        {
            if (!string.IsNullOrEmpty(mobileServiceUrl))
            {
                gateWayUrl = mobileServiceUrl;
            }
        }

        /// <summary>
        /// 通知数据更新
        /// </summary>
        /// <param name="notifyType">通知类别</param>
        /// <param name="eID">单位ID</param>
        public void NotifyRefreshData(string notifyType, string eID)
        {
            string requestUrl = string.Format("{0}/RequestPhoneData?requestCmd=RefreshCache&requestData={1},{2}", gateWayUrl, notifyType, eID);
            var myTask = Task.Factory.StartNew(TaskNotifyCacheUserData, requestUrl);
            Task.WaitAll(myTask);
        }

        /// <summary>
        /// 通知网关应用缓存指定单位的用户数据
        /// </summary>
        /// <param name="url"></param>
        private void TaskNotifyCacheUserData(object url)
        {
            string requestUrl = url.ToString();
            try
            {
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(requestUrl);
                myHttpWebRequest.Method = "Get";

                System.IO.StreamReader body = new StreamReader(myHttpWebRequest.GetResponse().GetResponseStream(), Encoding.GetEncoding("UTF-8"));
                XmlDocument doc = new XmlDocument();
                var retData = body.ReadToEnd();
                if (retData.Contains("8888")) retData = "8888";
                logs.Info(string.Format("TaskUtil-TaskNotifyCacheUserData SUCCESS：{0}.", retData));
            }
            catch (Exception e)
            {
                logs.Error(string.Format("TaskUtil-TaskNotifyCacheUserData DEFAULT：{0}.", e.Message));
            }
        }
    }
}
