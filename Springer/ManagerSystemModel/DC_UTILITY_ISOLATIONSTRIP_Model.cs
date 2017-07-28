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
    /// 数据中心_设施_隔离带
    /// </summary>
    public class DC_UTILITY_ISOLATIONSTRIP_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string DC_UTILITY_ISOLATIONSTRIP_ID { get; set; }

        /// <summary>
        /// 隔离带类型名称
        /// </summary>
        public string ISOLATIONTYPEName { get; set; }
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
        /// 隔离带类型
        /// </summary>
        [DisplayName("隔离带类型")]
        [DicType("35")]
        public string ISOLATIONTYPE { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        [DisplayName("所属机构")]
        [DicType("OrgNo")]
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
        /// 宽度
        /// </summary>
        [DisplayName("宽度")]
        [UnitDisplay("米", "宽度")]
        public string WIDTH { get; set; }
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
        /// 计划面积
        /// </summary>
        [DisplayName("计划面积")]
        [UnitDisplay("公顷", "计划面积")]
        public string PLANAREA { get; set; }
        /// <summary>
        /// 实际面积
        /// </summary>
        [DisplayName("实际面积")]
        [UnitDisplay("公顷", "实际面积")]
        public string REALAREA { get; set; }
        /// <summary>
        /// 价值
        /// </summary>
         [DisplayName("总额")]
        public string WORTH { get; set; }
        /// <summary>
        ///树种
        /// </summary>
       [DisplayName("树种组成")]
       [DicType("52")]
        public string TREETYPE { get; set; }
        /// <summary>
        /// 树名称
        /// </summary>
       public string TREETYPEName { get; set; }
       /// <summary>
       /// 所属县市名称
       /// </summary>
       public string ORGXSName { get; set; }
        /// <summary>
        /// 步行通道宽度
        /// </summary>
        [DisplayName("步行通道宽度")]
       public string AlleywayWideth { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        [DisplayName("位置")]
        [DicType("53")]
       public string Position { get; set; }
       /// <summary>
       /// 位置名称
       /// </summary>
       public string PositionName { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        [DisplayName("单价")]
       public string Price { get; set; }
        /// <summary>
        /// 树种
        /// </summary>
        [DisplayName("树种")]
        public string KINDTYPE { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        [DisplayName("录入时间")]
        [DicType("Date")]
        public string ENTRYTIME { get; set; }
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
