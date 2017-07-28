using DataBaseClassLibrary;
using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PublicClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 短信发送
    /// </summary>
    public class SendMessageCls
    {
        #region 短信发送
        /// <summary>
        /// 短信发送
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message SEND(SendMessage_Model m)
        {
            string name = SystemCls.getCookieInfo().trueName;
            string title = m.MessageTitle;
            string MessageName = m.MessageName;
            string phone = m.PHONE;
            if (string.IsNullOrEmpty(phone))
            {
                return new Message(false, "该成员未录入号码", m.URL);
            }
            string phonelist = "";
            string[] arr = m.PHONE.Split(',');
            string[] brr = arr.Where(s => !string.IsNullOrEmpty(s)).ToArray();
            List<string> list = new List<string>();
            foreach (string eachString in brr)//去掉重复的手机号码
            {
                if (!list.Contains(eachString))
                    list.Add(eachString);
            }
            brr = list.ToArray();
            for (int i = 0; i < brr.Length; i++)
            {
                if (i == brr.Length - 1)
                {
                    phonelist += brr[i];
                }
                else
                {
                    phonelist += brr[i] + ",";
                }
            }
            string content = m.MessageContent;
            var ms = SmsHelp.SmsCom.SendMsg(content, phonelist);
            return new Message(ms.Success, ms.Msg, m.URL);
        }
    }
        #endregion

}
