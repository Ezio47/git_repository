using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 设施-防火通道统计模型
    /// </summary>
    public class DCFIRECHANNELCount_Model
    {
        /// <summary>
        /// 防火通道id
        /// </summary>
         public string DC_UTILITY_FIRECHANNEL_ID { get; set; }
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
        /// 防火通道等级类型
        /// </summary>
        public string FIRECHANNELLEVELTYPE { get; set; }
        /// <summary>
        /// 防火通道等级类型统计
        /// </summary>
        public string FIRECHANNELLEVELTYPECount { get; set; }
        /// <summary>
        /// 防火通道等级类型类型统计模型
        /// </summary>
        public IEnumerable<FIRECHANNELLEVELTYPECountModel> FIRECHANNELLEVELTYPECountModel { get; set; }
        /// <summary>
        /// 防火通道使用性质
        /// </summary>
        public string FIRECHANNELUSERTYPE { get; set; }
        /// <summary>
        /// 防火通道使用性质统计
        /// </summary>
        public string FIRECHANNELUSERTYPECount { get; set; }
        /// <summary>
        /// 防火通道使用性质统计模型
        /// </summary>
        public IEnumerable<FIRECHANNELUSERTYPECountModel> FIRECHANNELUSERTYPECountModel { get; set; }
    }
    /// <summary>
    /// 维护管理类型统计模型
    /// </summary>
    public class MANAGERSTATECountModel 
    {
        /// <summary>
        /// 类型值
        /// </summary>
        public string MANAGERSTATVALUE { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string MANAGERSTATNAME { get; set; }
        /// <summary>
        /// 维护管理类型统计
        /// </summary>
        public string MANAGERSTATCount { get; set; }
        /// <summary>
        /// 维护管理类型长度统计
        /// </summary>
        public string MANAGERSTATLENGTHCount { get; set; }
    }
    /// <summary>
    /// 防火通道使用性质统计模型
    /// </summary>
    public class FIRECHANNELUSERTYPECountModel
    {
        /// <summary>
        /// 类型值
        /// </summary>
        public string FIRECHANNELUSERTYPEVALUE { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string FIRECHANNELUSERTYPENAME { get; set; }
        /// <summary>
        /// 防火通道等级类型类型统计
        /// </summary>
        public string FIRECHANNELUSERTYPECount { get; set; }
    }
    /// <summary>
    /// 防火通道等级统计模型
    /// </summary>
    public class FIRECHANNELLEVELTYPECountModel
    {
        /// <summary>
        /// 类型值
        /// </summary>
        public string FIRECHANNELLEVELTYPEVALUE { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string FIRECHANNELLEVELTYPENAME { get; set; }
        /// <summary>
        /// 防火通道使用性质统计
        /// </summary>
        public string FIRECHANNELLEVELTYPECount { get; set; }
    }
}
