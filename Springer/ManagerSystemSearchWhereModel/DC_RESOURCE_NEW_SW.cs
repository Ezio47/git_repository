using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{

    /// <summary>
    /// 数据中心_资源_新
    /// </summary>
    public class DC_RESOURCE_NEW_SW
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string DC_RESOURCE_NEW_ID { get; set; }
        /// <summary>
        /// 资源类型
        /// </summary>
        public string RESOURCETYPE { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string NUMBER { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string NAME { get; set; }
        /// <summary>
        /// 所属机构（多个）
        /// </summary>
        public string ORGNOS { get; set; }
        /// <summary>
        /// 树种
        /// </summary>
        public string KINDTYPE { get; set; }
        /// <summary>
        /// 林龄类别
        /// </summary>
        public string AGETYPE { get; set; }
        /// <summary>
        /// 起源类型
        /// </summary>
        public string ORIGINTYPE { get; set; }
        /// <summary>
        /// 可燃类型
        /// </summary>
        public string BURNTYPE { get; set; }
        /// <summary>
        /// 林木类型
        /// </summary>
        public string TREETYPE { get; set; }
        /// <summary>
        /// 每页行数
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 当前页数
        /// </summary>
        public int curPage { get; set; }
        /// <summary>
        /// 顶级组织机构码
        /// </summary>
        public string TopORGNO { get; set; }
        /// <summary>
        /// 挂钩领导
        /// </summary>
        public string POTHOOKLEADER { get; set; }
        /// <summary>
        /// 职务
        /// </summary>
        public string POTHOOKLEADERJOB { get; set; }
        /// <summary>
        ///领导联系电话
        /// </summary>
        public string POTHOOKLEADERTLEE { get; set; }
        /// <summary>
        /// 责任人
        /// </summary>
        public string DUTYPERSON { get; set; }
        /// <summary>
        /// 责任人联系电话
        /// </summary>
        public string DUTYPERSONTELL { get; set; }
        /// <summary>
        /// 经纬度集合
        /// </summary>
        public string JWDLIST { get; set; }
        /// <summary>
        /// 单独取市县镇
        /// </summary>
        public string ORGNOSXZ { get; set; }
    }
}
