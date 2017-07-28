using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TLW.Project.SmsMessageWcfService.Model;

namespace TLW.Project.SmsMessageWcfService.Interfaces
{

    /// <summary>
    /// 短信发送接口
    /// </summary>
    [ServiceContract]
    public interface ISmsSendService
    {

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="content">短信内容（限制在500内）发送内容（1-500 个汉字）UTF-8编码</param>
        /// <param name="mobile">手机号码。多个以英文逗号隔开</param>
        /// <returns></returns>
        [OperationContract]
        SmsMessage SendMsg(string content, string mobile);


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
        [OperationContract]
        SmsMessage SmsTemplateModify(string content, string tid, string type = "operate_templet");

        /// <summary>
        /// 获取企业签名
        /// </summary>
        /// <param name="SignName">传值签名</param>
        /// <returns></returns>
        [OperationContract]
        string GetSign(string SignName);

        /// <summary>
        /// 获取短信剩余数量
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        SmsMessage GetSmsTotal();

        /// <summary>
        /// 测试Msg
        /// </summary>
        /// <param name="text">测试内容</param>
        /// <returns></returns>
        [OperationContract]
        SmsMessage GetMsgTest(string text);

        /// <summary>
        /// 测试Str
        /// </summary>
        /// <param name="text">测试内容</param>
        /// <returns></returns>
        [OperationContract]
        string GetStrTest(string text);

    }
}
