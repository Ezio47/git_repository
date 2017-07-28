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
    /// 数据中心_装备_新
    /// </summary>
    public class DC_EQUIP_NEW_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string DC_EQUIP_NEW_ID { get; set; }
        /// <summary>
        /// 装备类型名称
        /// </summary>
        public string EQUIPTYPEName { get; set; }
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
        /// 装备类型
        /// </summary>
        [DisplayName("装备类型")]
        [DicType("32")]
        public string EQUIPTYPE { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        [DisplayName("所属机构")]
        [DicType("OrgNo")]
        public string BYORGNO { get; set; }
        /// <summary>
        /// 所属单位名称
        /// </summary>
        public string ORGName { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        [DisplayName("型号")]
        public string MODEL { get; set; }
        /// <summary>
        /// 购买年份
        /// </summary>
        [DisplayName("购买年份")]
        public string BUYYEAR { get; set; }
        /// <summary>
        /// 使用现状类型
        /// </summary>
        [DisplayName("使用现状")]
        [DicType("36")]
        public string USESTATE { get; set; }
        /// <summary>
        /// 使用现状类型名称
        /// </summary>
        public string USESTATEName { get; set; }
        /// <summary>
        /// 储存地点
        /// </summary>
        [DisplayName("储存地点")]
        public string STOREADDR { get; set; }

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
        /// 数量
        /// </summary>
        public string EQUIPAMOUNT { get; set; }
        /// <summary>
        /// 所属仓库
        /// </summary>
        public string REPID { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string REPNAME { get; set; }
        /// <summary>
        /// 装备单位
        /// </summary>
        public string DCSUPPROPUNIT { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public string PRICE { get; set; }
        /// <summary>
        /// 所属县市
        /// </summary>
        public string ORGXSName { get; set; }
    }
}
