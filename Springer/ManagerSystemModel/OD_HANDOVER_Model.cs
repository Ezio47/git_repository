using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{

    /// <summary>
    /// 交接班记录表
    /// </summary>
    public class OD_HANDOVER_Model
    {
        /// <summary>
        /// 值班交班序号
        /// </summary>
        public string ODHID { get; set; }
        /// <summary>
        /// 值班日期
        /// </summary>
        public string ONDUTYDATE { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 人员值班类别
        /// </summary>
        public string ONDUTYUSERTYPE { get; set; }
        /// <summary>
        /// 值班类别
        /// </summary>
        public string ONDUTYTYPE { get; set; }
        /// <summary>
        /// 值班人序号
        /// </summary>
        public string ONDUTYUSERID { get; set; }
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
        public string ONDUTYUSERName { get; set; }
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
