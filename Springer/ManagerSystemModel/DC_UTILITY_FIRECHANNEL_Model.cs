using ManagerSystemModel.ExtenAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 数据中心_设施_防火通道
    /// </summary>
    public class DC_UTILITY_FIRECHANNEL_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string DC_UTILITY_FIRECHANNEL_ID { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        [DisplayName("编号")]
        public string NUMBER { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        public string NAME { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        [DisplayName("所属机构")]
        [DicType("OrgNO")]
        public string BYORGNO { get; set; }
        /// <summary>
        /// 建设日期
        /// </summary>
        [DisplayName("建设日期")]
        [DicType("Date")]
        public string BUILDDATE { get; set; }
        /// <summary>
        /// 使用现状类型
        /// </summary>
        [DisplayName("使用现状类型")]
        [DicType("36")]
        public string USESTATE { get; set; }
        /// <summary>
        /// 使用现状类型名称
        /// </summary>
        public string USESTATEName { get; set; }
        /// <summary>
        /// 维护管理类型
        /// </summary>
        [DisplayName("维护管理类型")]
        [DicType("37")]
        public string MANAGERSTATE { get; set; }
        /// <summary>
        /// 维护管理类型名称
        /// </summary>
        public string MANAGERSTATEName { get; set; }
        /// <summary>
        /// 防火通道等级类型
        /// </summary>
        [DisplayName("防火通道等级类型")]
        [DicType("38")]
        public string FIRECHANNELLEVELTYPE { get; set; }
        /// <summary>
        /// 防火通道等级类型名称
        /// </summary>
        public string FIRECHANNELLEVELTYPEName { get; set; }
        /// <summary>
        /// 防火通道使用性质
        /// </summary>
        [DisplayName("防火通道使用性质")]
        [DicType("39")]
        public string FIRECHANNELUSERTYPE { get; set; }
        /// <summary>
        /// 防火通道使用性质名称
        /// </summary>
        public string FIRECHANNELUSERTYPEName { get; set; }
        /// <summary>
        /// 长度
        /// </summary>
        [DisplayName("长度")]
        [UnitDisplay("米", "长度")]
        public string LENGTH { get; set; }
        /// <summary>
        /// 经度开始
        /// </summary>
        public string JDBEGIN { get; set; }
        /// <summary>
        /// 纬度开始
        /// </summary>
        public string WDBEGIN { get; set; }
        /// <summary>
        /// 经度结束
        /// </summary>
        public string JDEND { get; set; }
        /// <summary>
        /// 纬度结束
        /// </summary>
        public string WDEND { get; set; }
        /// <summary>
        /// 所属单位名称
        /// </summary>
        public string ORGName { get; set; }
        /// <summary>
        /// 操作方法
        /// </summary>
        public string opMethod { get; set; }
        /// <summary>
        /// 返回网址
        /// </summary>
        public string returnUrl { get; set; }

        /// <summary>
        /// 经纬度点集合
        /// </summary>
        public string JWDLIST { get; set; }
        /// <summary>
        /// 价值
        /// </summary>
        public string WORTH { get; set; }
        /// <summary>
        /// 所属县市名称
        /// </summary>
        public string ORGXSName { get; set; }
        /// <summary>
        /// 建设开始日期
        /// </summary>
        [DisplayName("建设开始日期")]
        [DicType("Date")]
        public string BUILDDATEBEGIN { get; set; }
        /// <summary>
        /// 建设结束日期
        /// </summary>
        [DisplayName("建设结束日期")]
        [DicType("Date")]
        public string BUILDDATEEND { get; set; }
    }
}
