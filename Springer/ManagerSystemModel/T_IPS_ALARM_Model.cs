using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 一键报警Model
    /// </summary>
   public class T_IPS_ALARM_Model
    {
       /// <summary>
        /// 报警ID
       /// </summary>
        public string ALARMID { get; set; }
       /// <summary>
        /// 经度
       /// </summary>
        public string LONGITUDE { get; set; }
       /// <summary>
        /// 纬度
       /// </summary>
        public string LATITUDE { get; set; }
       /// <summary>
       /// 高度
       /// </summary>
        public string HEIGHT { get; set; }
       /// <summary>
        /// 手机号码
       /// </summary>
        public string PHONE { get; set; }
       /// <summary>
        /// 发生地
       /// </summary>
        public string ADDRESS { get; set; }
       /// <summary>
        /// 报警时间
       /// </summary>
        public string ALARMTIME { get; set; }
       /// <summary>
        /// 报警内容
       /// </summary>
        public string ALARMCONTENT { get; set; }
       /// <summary>
        /// 报警处理状态
       /// </summary>
        public string MANSTATE { get; set; }
       /// <summary>
        /// 反馈结果
       /// </summary>
        public string MANRESULT { get; set; }
       /// <summary>
        /// 处理时间
       /// </summary>
        public string MANTIME { get; set; }
       /// <summary>
        /// 处理人
       /// </summary>
        public string MANUSERID { get; set; }

       /// <summary>
       /// 处理人用户名
       /// </summary>
        public string ManUserName { get; set; }
        /// <summary>
        /// 护林员ID
        /// </summary>
        public string HID { get; set; }
        /// <summary>
        /// 护林员名称
        /// </summary>
        public string HName { get; set; }

       /// <summary>
       /// 护林员机构编码
       /// </summary>
        public string OrgNo { get; set; }
       /// <summary>
       /// 护林员机构名称
       /// </summary>
        public string OrgNoName { get; set; }
       /// <summary>
       /// 方法
       /// </summary>
        public string opMethod { get; set; }

       /// <summary>
       /// 权限
       /// </summary>
        public string Rights { get; set; }
    }
}
