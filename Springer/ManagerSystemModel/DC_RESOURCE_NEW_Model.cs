using ManagerSystemModel.ExtenAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{

    /// <summary>
    /// 数据中心_资源_新
    /// </summary>
    public class DC_RESOURCE_NEW_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string DC_RESOURCE_NEW_ID { get; set; }
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
        /// 资源类型
        /// </summary>
        [DisplayName("资源类型")]
        [DicType("28")]
        public string RESOURCETYPE { get; set; }
        /// <summary>
        /// 资源类型名称
        /// </summary>
        public string RESOURCETYPEName { get; set; }

        /// <summary>
        /// 所属机构（多个）
        /// </summary>
        [DisplayName("所属机构")]
        [DicType("OrgNo")]
        public string ORGNOS { get; set; }
        /// <summary>
        /// 所属机构（多个）名称
        /// </summary>
        public string ORGNOSName { get; set; }
        /// <summary>
        /// 树种
        /// </summary>
        public string KINDTYPE { get; set; }
        /// <summary>
        /// 林龄类别
        /// </summary>
        [DisplayName("林龄类别")]
        [DicType("27")]
        public string AGETYPE { get; set; }
        /// <summary>
        /// 林龄类别名称
        /// </summary>
        public string AGETYPEName { get; set; }
        /// <summary>
        /// 起源类型
        /// </summary>
        [DisplayName("起源类型")]
        [DicType("29")]
        public string ORIGINTYPE { get; set; }
        /// <summary>
        /// 起源类型名称
        /// </summary>
        public string ORIGINTYPEName { get; set; }
        /// <summary>
        /// 面积
        /// </summary>
        // [Display(Name = "111", Description = "iiiii")]
        [DisplayName("面积")]
        [UnitDisplay("公顷", "资源面积")]
        public string AREA { get; set; }
        /// <summary>
        /// 可燃类型
        /// </summary>
        [DisplayName("可燃类型")]
        [DicType("30")]
        public string BURNTYPE { get; set; }
        /// <summary>
        /// 可燃类型名称
        /// </summary>
        public string BURNTYPEName { get; set; }
        /// <summary>
        /// 林木类型
        /// </summary>
        [DisplayName("林木类型")]
        [DicType("31")]
        public string TREETYPE { get; set; }
        /// <summary>
        /// 林木类型名称
        /// </summary>
        public string TREETYPEName { get; set; }
        /// <summary>
        /// 坡向
        /// </summary>
        [DisplayName("坡向")]
        public string ASPECT { get; set; }
        /// <summary>
        /// 坡度
        /// </summary>
        [DisplayName("坡度")]
        public string ANGLE { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string MARK { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string JD { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public string WD { get; set; }
        /// <summary>
        /// 操作方法
        /// </summary>
        public string opMethod { get; set; }
        /// <summary>
        /// 返回网址
        /// </summary>
        public string returnUrl { get; set; }
        /// <summary>
        /// 挂钩领导
        /// </summary>
        [DisplayName("挂钩领导")]
        public string POTHOOKLEADER { get; set; }
        /// <summary>
        /// 职务
        /// </summary>
        [DisplayName("职务")]
        public string POTHOOKLEADERJOB { get; set; }
        /// <summary>
        ///领导联系电话
        /// </summary>
        [DisplayName("领导电话")]
        public string POTHOOKLEADERTLEE { get; set; }
        /// <summary>
        /// 责任人
        /// </summary>
        [DisplayName("责任人")]
        public string DUTYPERSON { get; set; }
        /// <summary>
        /// 责任人联系电话
        /// </summary>
        [DisplayName("责任人联系电话")]
        public string DUTYPERSONTELL { get; set; }
        /// <summary>
        /// 经纬度集合
        /// </summary>
        public string JWDLIST { get; set; }
        /// <summary>
        /// 所属县市名称
        /// </summary>
        public string ORGXSName { get; set; }
    }
}
