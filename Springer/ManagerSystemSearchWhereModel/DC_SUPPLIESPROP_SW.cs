using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 数据中心_物资属性表
    /// </summary>
     public class DC_SUPPLIESPROP_SW
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string DC_SUPPLIESPROP_ID { get; set; }
        /// <summary>
        /// 物资名称
        /// </summary>
        public string DCSUPPROPNAME { get; set; }
        /// <summary>
        /// 物资型号
        /// </summary>
        public string DCSUPPROPMODEL { get; set; }
        /// <summary>
        /// 物资单位
        /// </summary>
        public string DCSUPPROPUNIT { get; set; }
        /// <summary>
        /// 物资生产厂家
        /// </summary>
        public string DCSUPPROPFACTORY { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string MARK { get; set; }
         /// <summary>
         /// 下拉框显示所有
         /// </summary>
        public string isShowAll { get; set; }
    }
}
