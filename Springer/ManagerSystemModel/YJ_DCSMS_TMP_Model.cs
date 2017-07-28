using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 
    /// </summary>
    public class YJ_DCSMS_TMP_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string YJ_DCSMS_TMPID { get; set; }
        /// <summary>
        /// 短信发送群名称
        /// </summary>
        public string SMSGROUPNAME { get; set; }
        /// <summary>
        /// 短信发送群类别 0为从通讯录中选择 1值班员 2护林员
        /// </summary>
        public string SMSGROUPTYPE { get; set; }
        /// <summary>
        /// 火险等级
        /// </summary>
        public string DANGERCLASS { get; set; }
        /// <summary>
        /// 模板内容
        /// </summary>
        public string TMPCONTENT { get; set; }
        /// <summary>
        /// 接收人员序号列表
        /// </summary>
        public string SMSSENDUSERLIST { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public string ORDERBY { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public string ISENABLE { get; set; }
        /// <summary>
        /// 是否启用名称 0 未启用 1启用
        /// </summary>
        public string ISENABLEName { get; set; }
        /// <summary>
        /// 火灾级别名称
        /// </summary>
        public string FIRELEVELName { get; set; }
        /// <summary>
        /// 号码来源类型名称
        /// </summary>
        public string SMSGROUPTYPEName { get; set; }
        /// <summary>
        /// 火灾预警等级模型
        /// </summary>
        public T_SYS_DICTModel dicModel { get; set; }
        
        /// <summary>
        /// 操作方法
        /// </summary>
        public string opMethod { get; set; }
        /// <summary>
        /// 返回网址
        /// </summary>
        public string returnUrl { get; set; }

        /// <summary>
        /// 添加TID
        /// </summary>
        public string TID { get; set; }
    }
}
