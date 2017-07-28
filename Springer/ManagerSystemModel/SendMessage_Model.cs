using ManagerSystemModel.ExtenAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ManagerSystemModel
{
    /// <summary>
    /// 短信发送
    /// </summary>
   public class SendMessage_Model
    {
       /// <summary>
       /// 序号
       /// </summary>
       public string EM_MESSAGEID { get; set; }
       /// <summary>
       /// 发送人员
       /// </summary>
       public string NAME { get; set; }
       /// <summary>
       /// 电话号码
       /// </summary>
       public string PHONE { get; set; }
       /// <summary>
       /// 短信的内容
       /// </summary>
       public string MessageContent { get; set; }
       /// <summary>
       /// 短信的人员
       /// </summary>
       public string MessageName { get; set; }
       /// <summary>
       /// 短信的主题
       /// </summary>
       public string MessageTitle { get; set; }
       /// <summary>
       /// 地址
       /// </summary>
       public string URL { get; set; }
       /// <summary>
       /// 方法
       /// </summary>
       public string Opmethod { get; set; }
       /// <summary>
       /// 排序
       /// </summary>
       public string ORDERBY { get; set; }
       /// <summary>
       /// 电话号码list
       /// </summary>
       public string PHONELIST { get; set; }
       /// <summary>
       /// 人员list
       /// </summary>
       public string NAMELIST { get; set; }
       /// <summary>
       /// 组织机构list
       /// </summary>
       public string BYORGNOLIST { get; set; }
    }
}
