using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 短信模板检索条件
    /// </summary>
    public class YJ_DCSMS_TMP_SW
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
    }
}
