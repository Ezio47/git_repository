using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 设施-瞭望台统计
    /// </summary>
    public class DCOVERWATCHCount_Model
    {
        /// <summary>
        /// 瞭望台id
        /// </summary>
        public string DC_UTILITY_OVERWATCH_ID { get; set; }
        /// <summary>
        /// 组织机构码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 组织机构名
        /// </summary>
        public string ORGName { get; set; }
        /// <summary>
        /// 瞭望台名称
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
        public IEnumerable<TYPECountModel> TYPECountModel { get; set; }
    }
    /// <summary>
    /// 瞭望台-结构类型统计模型
    /// </summary>
    public class TYPECountModel
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
