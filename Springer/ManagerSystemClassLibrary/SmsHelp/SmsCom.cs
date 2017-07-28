using log4net;
using ManagerSystemClassLibrary.SmsSendService;
using PublicClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary.SmsHelp
{
    /// <summary>
    /// 短信
    /// </summary>
    public class SmsCom
    {
        /// <summary>
        /// 记录日志
        /// </summary>
        public static ILog logs = LogHelper.GetInstance();
        static SmsSendServiceClient client = new SmsSendServiceClient();//短信服务


        /// <summary>
        /// 获取短信数量
        /// </summary>
        /// <returns></returns>
        public static SmsMessage GetMsgCount()
        {
            var ms = client.GetSmsTotal();
            if (ms.Success == false)
            {
                logs.ErrorFormat("获取短信数量失败，错误为{0}", ms.Msg);
            }
            return ms;
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="content">短信内容</param>
        /// <param name="mobile">手机号码列表 多个用英文逗号分隔</param>
        /// <returns></returns>
        public static SmsMessage SendMsg(string content, string mobile)
        {
            var ms = client.SendMsg(content, mobile);
            if (ms.Success == false)
            {
                logs.ErrorFormat("短信发送失败，号码为{0}，内容为{1}", mobile, content);
            }
            return ms;
        }

        /// <summary>
        /// 短信模板添加修改
        /// </summary>
        /// <param name="content">模板内容</param>
        /// <param name="tid">模板tid</param>
        /// <param name="type">固定值</param>
        /// <returns></returns>
        public static SmsMessage SmsTemplateModify(string content, string tid, string type = "operate_templet")
        {
            string sign = client.GetSign("");//企业签名
            content += sign;
            var ms = client.SmsTemplateModify(content, tid, type);
            if (ms.Success == true)
            {
                logs.InfoFormat("第三方平台短信模板成功，模板tid={0}", ms.Msg);
            }
            else
            {
                logs.ErrorFormat("第三方平台短信模板失败，错误信息为{0}", ms.Msg);
            }
            return ms;
        }
    }
}
