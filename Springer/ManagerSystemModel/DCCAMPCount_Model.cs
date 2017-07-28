using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 设施-营房统计模型
    /// </summary>
    public class DCCAMPCount_Model
    {
        /// <summary>
        /// 营房id
        /// </summary>
        public string DC_UTILITY_CAMP_ID { get; set; }
        /// <summary>
        /// 组织机构码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 组织机构名
        /// </summary>
        public string ORGName { get; set; }
        /// <summary>
        /// 营房名称
        /// </summary>
        public string NAME { get; set; }
        /// <summary>
        /// 结构类型
        /// </summary>
        public string STRUCTURETYPE { get; set; }
        /// <summary>
        /// 结构类型统计
        /// </summary>
        public string STRUCTURETYPECount { get; set; }
        /// <summary>
        /// 结构类型统计模型
        /// </summary>
        public IEnumerable<STRUCTURETYPECountModel> STRUCTURETYPECountModel { get; set; }
    }
    /// <summary>
    /// 结构类型统计模型
    /// </summary>
    public class STRUCTURETYPECountModel
    {
        /// <summary>
        /// 类型值
        /// </summary>
        public string STRUCTURETYPEVALUE { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string STRUCTURETYPENAME { get; set; }
        /// <summary>
        /// 结构类型统计
        /// </summary>
        public string STRUCTURETYPECount { get; set; }

    }
}
