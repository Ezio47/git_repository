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
    /// 数据中心_设施_营房
    /// </summary>
    public class DC_UTILITY_CAMP_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string DC_UTILITY_CAMP_ID { get; set; }
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
        /// 所属机构（多个）
        /// </summary>
        [DisplayName("所属机构")]
        [DicType("OrgNO")]
        public string ORGNOS { get; set; }
        /// <summary>
        /// 面积（像素）
        /// </summary>
        [DisplayName("面积")]
        [UnitDisplay("公顷", "营房面积")]
        public string AREA { get; set; }
        /// <summary>
        /// 建设日期
        /// </summary>
        [DisplayName("建设日期")]
        [DicType("Date")]
        public string BUILDDATE { get; set; }
        /// <summary>
        /// 楼层
        /// </summary>
        [DisplayName("楼层")]
        public string FLOOR { get; set; }
        /// <summary>
        /// 结构类型
        /// </summary>
        [DisplayName("楼层")]
        [DicType("34")]
        public string STRUCTURETYPE { get; set; }
        /// <summary>
        /// 结构类型名称
        /// </summary>
        public string STRUCTURETYPEName { get; set; }
        /// <summary>
        /// 附属设施
        /// </summary>
        [DisplayName("附属设施")]
        public string SUBFACILITIES { get; set; }
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
        /// 最高级别最值机构码
        /// </summary>
        public string TopORGNO { get; set; }
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
