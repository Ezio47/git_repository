using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace TLW.Project.SmsMessageWcfService.Interfaces
{
    [ServiceContract]
    public interface IVideoService
    {
        /// <summary>
        /// 测试Msg
        /// </summary>
        /// <param name="text">测试内容</param>
        /// <returns></returns>
        [OperationContract]
        string GetVideoMsgTest(string text);
    }
}
