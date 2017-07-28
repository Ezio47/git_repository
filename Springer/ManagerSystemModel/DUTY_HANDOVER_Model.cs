using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 值班提交
    /// </summary>
    public class DUTY_HANDOVER_Model
    {
        /// <summary>
        /// 值班交班序号
        /// </summary>
        public string DHID { get; set; }
        /// <summary>
        /// 值班日期
        /// </summary>
        public string DUTYDATE { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 人员值班类别
        /// </summary>
        public string DUTYUSERTYPE { get; set; }
        /// <summary>
        /// 值班类别
        /// </summary>
        public string DUTYTYPE { get; set; }
        /// <summary>
        /// 值班人序号
        /// </summary>
        public string DUTYUSERID { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public string OPTIME { get; set; }
        /// <summary>
        /// 操作内容
        /// </summary>
        public string OPCONTENT { get; set; }
        /// <summary>
        /// 值班人名称
        /// </summary>
        public string DUTYUSERName { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string ORGNAME { get; set; }
        /// <summary>
        /// 操作方法
        /// </summary>
        public string opMethod { get; set; }
    }
}
