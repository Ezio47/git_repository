using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLW.Project.SmsMessageWcfService.Common;
using TLW.Project.SmsMessageWcfService.Interfaces;
using TLW.Project.SmsMessageWcfService.Model;

namespace TLW.Project.SmsMessageWcfService.Services
{
    public class SmsSendService : ISmsSendService
    {
        private readonly ILog logs = LogHelper.GetInstance();//log 
        static string SmsUrl = System.Configuration.ConfigurationManager.AppSettings["SmsUrl"];//短信地址
        static string SmsUser = System.Configuration.ConfigurationManager.AppSettings["SmsUser"];//短信用户名
        static string SmsPwd = System.Configuration.ConfigurationManager.AppSettings["SmsPwd"];//短信pwd
        static string SmsSign = System.Configuration.ConfigurationManager.AppSettings["SmsSign"];//短信签名

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="content">短信内容（限制在500内）发送内容（1-500 个汉字）UTF-8编码</param>
        /// <param name="mobile">手机号码。多个以英文逗号隔开</param>
        /// <returns></returns>
        public SmsMessage SendMsg(string content, string mobile)
        {
            SmsMessage ms = null;
            if (string.IsNullOrEmpty(SmsUser) || string.IsNullOrEmpty(SmsPwd) || string.IsNullOrEmpty(SmsSign) || string.IsNullOrEmpty(content) || string.IsNullOrEmpty(mobile))
            {
                ms = new SmsMessage(false, "条件缺失，请检查发送短信相关条件");
                logs.Error(mobile + "=====" + "条件缺失，请检查发送短信相关条件");
                return ms;
            }
            StringBuilder sms = new StringBuilder();
            sms.AppendFormat("name={0}", SmsUser.Trim());
            sms.AppendFormat("&pwd={0}", SmsPwd.Trim());//登陆平台，管理中心--基本资料--接口密码（28位密文）；复制使用即可。
            sms.AppendFormat("&content={0}", content.Trim());
            sms.AppendFormat("&mobile={0}", mobile.Trim());
            sms.AppendFormat("&sign={0}", SmsSign.Trim());// 公司的简称或产品的简称都可以
            sms.Append("&type=pt");
            string resp = HttpCom.PushToWeb(SmsUrl, sms.ToString(), Encoding.UTF8);
            string[] msg = resp.Split(',');
            if (msg[0] == "0")
            {
                logs.Info(mobile + "=====" + "短信发送成功");
                ms = new SmsMessage(true, msg[5]);
            }
            else
            {
                ms = new SmsMessage(false, msg[1]);
                logs.Error(mobile + "=====" + msg[1]);
            }
            return ms;
        }


        /// <summary>
        /// 短信模板添加修改
        /// </summary>
        /// <param name="content">必填参数。模板内容 UTF-8编码
        ///格式：固定内容@固定内容@固定内容
        ///@ 可代表任意多个字符；可变的地方用@代替
        ///例子：您的验证码为：@ 20分钟内有效。您获得了@个积分，验证后在个人中心领取【企业签名】
        ///</param>
        /// <param name="tid">
        /// 添加时不填
        ///修改时填模板的ID号；
        ///如id号不存在则返回  3,tid is not true
        /// </param>
        /// <param name="type">固定值 operate_templet</param>
        /// <returns>提交响应为英文逗号隔开的一行数据，状态,模板ID号 ,如果响应的状态不是“0”，状态,消息。</returns>
        public SmsMessage SmsTemplateModify(string content, string tid, string type = "operate_templet")
        {
            SmsMessage ms = null;
            if (string.IsNullOrEmpty(content))
            {
                ms = new SmsMessage(false, "短信内容缺失，请检查");
                logs.Error("短信内容缺失，请检查");
                return ms;
            }
            StringBuilder smsplateContent = new StringBuilder();
            smsplateContent.AppendFormat("name={0}", SmsUser.Trim());
            smsplateContent.AppendFormat("&pwd={0}", SmsPwd.Trim());//登陆平台，管理中心--基本资料--接口密码（28位密文）；复制使用即可。
            smsplateContent.AppendFormat("&content={0}", content.Trim());
            smsplateContent.AppendFormat("&tid={0}", tid);
            smsplateContent.Append("&type=operate_templet");
            string resp = HttpCom.PushToWeb(SmsUrl, smsplateContent.ToString(), Encoding.UTF8);
            string[] msg = resp.Split(',');
            if (msg[0] == "0")
            {
                logs.Info("===短信模板===" + "修改成功，模版tid===" + msg[1]);
                ms = new SmsMessage(true, msg[1]);
            }
            else
            {
                logs.Error("===短信模板===修改失败，错误信息===" + msg[1] + "===错误code====" + msg[0]);
                ms = new SmsMessage(false, msg[1]);
            }
            return ms;
        }


        /// <summary>
        /// 获取企业签名
        /// </summary>
        /// <param name="SignName">传值签名</param>
        /// <returns></returns>

        public string GetSign(string SignName)
        {
            if (!string.IsNullOrEmpty(SignName))
            {
                return SignName;
            }
            else
            {
                return SmsSign;
            }
        }

        /// <summary>
        /// 获取短信剩余数量
        /// </summary>
        /// <returns></returns>
        public SmsMessage GetSmsTotal()
        {
            SmsMessage ms = null;
            StringBuilder sms = new StringBuilder();
            sms.AppendFormat("name={0}", SmsUser.Trim());
            sms.AppendFormat("&pwd={0}", SmsPwd.Trim());//登陆平台，管理中心--基本资料--接口密码（28位密文）；复制使用即可。
            sms.Append("&type=balance");
            string resp = HttpCom.PushToWeb(SmsUrl, sms.ToString(), Encoding.UTF8);
            string[] msg = resp.Split(',');
            if (msg[0] == "0")
            {
                logs.Info("获取短信数：" + msg[1]);
                ms = new SmsMessage(true, msg[1]);
            }
            else
            {
                logs.Error("获取短信数失败");
                ms = new SmsMessage(false, msg[1]);
            }

            return ms;
        }


        #region Test
        /// <summary>
        /// 测试Msg
        /// </summary>
        /// <param name="text">测试内容</param>
        /// <returns></returns>
        public SmsMessage GetMsgTest(string text)
        {
            var ms = new SmsMessage(true, text);
            logs.Info("地址===" + SmsUrl);
            logs.Info("GetMsgTest===" + text);
            return ms;
        }

        /// <summary>
        /// 测试Str
        /// </summary>
        /// <param name="text">测试内容</param>
        /// <returns></returns>
        public string GetStrTest(string text)
        {
            logs.Info("地址===" + SmsUrl);
            logs.Info("GetStrTest===" + text);
            return text;
        }
        #endregion
    }
}
