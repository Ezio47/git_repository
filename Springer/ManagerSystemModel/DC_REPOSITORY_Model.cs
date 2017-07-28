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
    /// 数据中心_仓库表
    /// </summary>
    public class DC_REPOSITORY_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
         //[DisplayName("仓库id")]
         [DicType("repid")]
        public string DCREPOSITORYID { get; set; }
        /// <summary>
        /// 所属仓库序号
        /// </summary>
        public string REPTYPEID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        public string NAME { get; set; }
        /// <summary>
        /// 组合名称
        /// </summary>
        public string COMNAME { get; set; }
        /// <summary>
        /// 仓库地址
        /// </summary>
        [DisplayName("仓库地址")]
        public string ADDRESS { get; set; }
        /// <summary>
        /// 所属机构编码
        /// </summary>
        [DisplayName("所属机构")]
        [DicType("OrgNo")]
        public string BYORGNO { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        [DisplayName("负责人")]
        public string RESPONSIBLEMAN { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        [DisplayName("联系方式")]
        public string LINKWAY { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        [DisplayName("经度")]
        public string JD { get; set; }
        /// <summary>
        /// 维度
        /// </summary>
        [DisplayName("维度")]
        public string WD { get; set; }
        /// <summary>
        /// 方法
        /// </summary>
        public string opMethod { get; set; }
        /// <summary>
        /// 返回地址
        /// </summary>
        public string returnUrl { get; set; }
        /// <summary>
        /// 组织机构名
        /// </summary>
        public string ORGName { get; set; }
        /// <summary>
        /// 所属县市名称
        /// </summary>
        public string ORGXSName { get; set; }

    }
}
