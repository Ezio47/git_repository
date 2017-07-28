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
    /// 数据中心_设施_宣传碑牌
    /// </summary>
    public class DC_UTILITY_PROPAGANDASTELE_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string DC_UTILITY_PROPAGANDASTELE_ID { get; set; }
        /// <summary>
        /// 宣传碑类型名称
        /// </summary>
        public string PROPAGANDASTELETYPEName { get; set; }
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
        /// 宣传碑类型
        /// </summary>
        [DisplayName("宣传碑类型")]
        [DicType("40")]
        public string PROPAGANDASTELETYPE { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [DisplayName("地址")]
        public string ADDRESS { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        [DisplayName("所属机构")]
        [DicType("OrgNo")]
        public string BYORGNO { get; set; }
        /// <summary>
        /// 结构类型
        /// </summary>
        [DisplayName("结构类型")]
        [DicType("34")]
        public string STRUCTURETYPE { get; set; }
        /// <summary>
        /// 结构类型名称
        /// </summary>
        public string STRUCTURETYPEName { get; set; }
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
        /// 经度
        /// </summary>
        [DisplayName("经度")]
        public string JD { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        [DisplayName("纬度")]
        public string WD { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public string MARK { get; set; }

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