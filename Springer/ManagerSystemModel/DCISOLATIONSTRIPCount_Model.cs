using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 设施-隔离带统计
    /// </summary>
    public class DCISOLATIONSTRIPCount_Model
    {

        /// <summary>
        /// 防火通道id
        /// </summary>
        public string DC_UTILITY_ISOLATIONSTRIP_ID { get; set; }
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
        /// 隔离带类型
        /// </summary>
        public string ISOLATIONTYPE { get; set; }
        /// <summary>
        /// 隔离带类型统计
        /// </summary>
        public string ISOLATIONTYPECount { get; set; }
        /// <summary>
        /// 隔离带类型统计模型
        /// </summary>
        public IEnumerable<ISOLATIONTYPECountModel> ISOLATIONTYPECountModel { get; set; }
        /// <summary>
        /// 长度统计
        /// </summary>
        public string LENGTHCount { get; set; }
    }
    /// <summary>
    /// 隔离带类型统计模型
    /// </summary>
    public class ISOLATIONTYPECountModel 
    {
        /// <summary>
        /// 类型名
        /// </summary>
        public string ISOLATIONTYPENAME { get; set; }
        /// <summary>
        /// 类型值
        /// </summary>
        public string ISOLATIONTYPEVALUE { get; set; }
        /// <summary>
        /// 隔离带类型统计
        /// </summary>
        public string DICTISOLATIONTYPECount { get; set; }
        /// <summary>
        /// 隔离带类型长度统计
        /// </summary>
        public string DICTISOLATIONTYPELENGTHCount { get; set; }

    }
}
