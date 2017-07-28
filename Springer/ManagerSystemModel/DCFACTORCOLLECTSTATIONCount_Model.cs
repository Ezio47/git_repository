using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 设施-因子采集站统计
    /// </summary>
    public class DCFACTORCOLLECTSTATIONCount_Model
    {
        /// <summary>
        /// 因子采集站id
        /// </summary>
        public string DC_UTILITY_FACTORCOLLECTSTATION_ID { get; set; }
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
        /// 无线电传输方式
        /// </summary>
        public string TRANSFERMODETYPE { get; set; }
        /// <summary>
        /// 无线电传输方式统计
        /// </summary>
        public string TRANSFERMODETYPECount { get; set; }
        /// <summary>
        /// 无线电传输方式统计模型
        /// </summary>
        public IEnumerable<TRANSFERMODETYPECountModel> TRANSFERMODETYPECountModel { get; set; }
    }
}
