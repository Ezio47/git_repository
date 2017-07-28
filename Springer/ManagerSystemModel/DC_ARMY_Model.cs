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
    /// 数据中心_队伍
    /// </summary>
    public class DC_ARMY_Model
    {

        /// <summary>
        /// 序号
        /// </summary>
        public string DC_ARMY_ID { get; set; }
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
        /// 队伍类型
        /// </summary>
        [DisplayName("队伍类型")]
        [DicType("26")]
        public string ARMYTYPE { get; set; }

        /// <summary>
        /// 所属机构编码
        /// </summary>
        [DisplayName("所属机构")]
        [DicType("OrgNO")]
        public string BYORGNO { get; set; }
        /// <summary>
        /// 人数
        /// </summary>
        [DisplayName("人数")]
        public string ARMYMEMBERCOUNT { get; set; }
        /// <summary>
        /// 队长
        /// </summary>
        [DisplayName("队长")]
        public string ARMYLEADER { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        [DisplayName("联系方式")]
        public string CONTACTS { get; set; }
        /// <summary>
        /// 队伍特点
        /// </summary>
        [DisplayName("队伍特点")]
        public string ARMYCHARACTER { get; set; }
        /// <summary>
        /// 装备
        /// </summary>
        public string ARMYEQUIP { get; set; }
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
        /// 队伍类型名称
        /// </summary>
        public string ARMYTYPEName { get; set; }
        /// <summary>
        /// 所属单位名称
        /// </summary>
        public string ORGName { get; set; }
        /// <summary>
        /// 所属县市名称
        /// </summary>
        public string ORGXSName { get; set; }
        /// <summary>
        /// 操作方法
        /// </summary>
        public string opMethod { get; set; }
        /// <summary>
        /// 返回网址
        /// </summary>
        public string returnUrl { get; set; }
    }
}
