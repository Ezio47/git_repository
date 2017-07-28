using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLW.Project.SmsMessageWcfService.Common;
using TLW.Project.SmsMessageWcfService.Interfaces;

namespace TLW.Project.SmsMessageWcfService.Services
{
    /// <summary>
    /// 视频对接服务
    /// </summary>
    public class VideoService : IVideoService
    {

        private readonly ILog logs = LogHelper.GetInstance();//log 

        #region Test
        /// <summary>
        /// 测试Msg
        /// </summary>
        /// <param name="text">测试内容</param>
        /// <returns></returns>
        public string GetVideoMsgTest(string text)
        {

            return text;
        }
        #endregion
    }
}
