using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 设施-宣传碑牌
    /// </summary>
   public  class DCPROPAGANDASTELECount_Model
    {
       /// <summary>
        /// 宣传碑牌id
        /// </summary>
       public string DC_UTILITY_PROPAGANDASTELE_ID { get; set; }
        /// <summary>
        /// 组织机构码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 组织机构名
        /// </summary>
        public string ORGName { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string NAME { get; set; }
        /// <summary>
        /// 使用形状
        /// </summary>
        public string USESTATE { get; set; }
        /// <summary>
        /// 使用形状统计
        /// </summary>
        public string USESTATECount { get; set; }
        /// <summary>
        /// 使用形状统计模型
        /// </summary>
        public IEnumerable<USESTATECountModel> USESTATECountModel { get; set; }
        /// <summary>
        /// 维护管理类型
        /// </summary>
        public string MANAGERSTATE { get; set; }
        /// <summary>
        /// 维护管理统计
        /// </summary>
        public string MANAGERSTATECount { get; set; }
        /// <summary>
        /// 维护管理类型统计模型
        /// </summary>
        public IEnumerable<MANAGERSTATECountModel> MANAGERSTATECountModel { get; set; }
        /// <summary>
        /// 宣传碑牌类型
        /// </summary>
        public string PROPAGANDASTELETYPE { get; set; }
        /// <summary>
        /// 宣传碑牌类型统计
        /// </summary>
        public string PROPAGANDASTELETYPECount { get; set; }
        /// <summary>
        /// 宣传碑牌类型统计模型
        /// </summary>
        public IEnumerable<PROPAGANDASTELETYPECountModel> PROPAGANDASTELETYPECountModel { get; set; }
       /// <summary>
        /// 结构类型
       /// </summary>
        public string STRUCTURETYPE { get; set; }
       /// <summary>
       /// 结构类型统计
       /// </summary>
        public string STRUCTURETYPECount { get; set; }
       /// <summary>
       /// 结构类型
       /// </summary>
        public IEnumerable<STRUCTURETYPECountModel> STRUCTURETYPECountModel { get; set; }
    }
    /// <summary>
   /// 宣传碑牌类型统计模型
    /// </summary>
   public class PROPAGANDASTELETYPECountModel 
    {
        /// <summary>
        /// 类型名
        /// </summary>
       public string PROPAGANDASTELETYPENAME { get; set; }
        /// <summary>
        /// 类型值
        /// </summary>
       public string PROPAGANDASTELETYPEVALUE { get; set; }
        /// <summary>
        /// 宣传碑牌类型统计
        /// </summary>
        public string DICTPROPAGANDASTELETYPECount { get; set; }

    }
}
