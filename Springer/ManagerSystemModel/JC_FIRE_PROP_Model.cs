using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{

    /// <summary>
    /// 监测_火情属性表
    /// </summary>
    public class JC_FIRE_PROP_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string JC_FIRE_PROPID { get; set; }
        /// <summary>
        /// 火灾序号
        /// </summary>
        public string JCFID { get; set; }
        /// <summary>
        /// 过火面积
        /// </summary>
        public string GHMJ { get; set; }
        /// <summary>
        /// 过火林地面积
        /// </summary>
        public string GHLDMJ { get; set; }
        /// <summary>
        /// 受害森林面积
        /// </summary>
        public string SHSLMJ { get; set; }
        /// <summary>
        /// 人员伤
        /// </summary>
        public string RYS { get; set; }
        /// <summary>
        /// 人员亡
        /// </summary>
        public string RYW { get; set; }
        /// <summary>
        /// 是否敏感时段
        /// </summary>
        public string MGSD { get; set; }
        /// <summary>
        /// 是否重点区域
        /// </summary>
        public string ZDQY { get; set; }
        /// <summary>
        /// 国界距离
        /// </summary>
        public string GJJL { get; set; }
        /// <summary>
        /// 州指挥部指挥级别
        /// </summary>
        public string ZZH { get; set; }
        /// <summary>
        /// 起火数级别
        /// </summary>
        public string QHS { get; set; }
        /// <summary>
        /// 损失级别
        /// </summary>
        public string SSJB { get; set; }
        /// <summary>
        /// 火灾级别
        /// </summary>
        public string FIRELEVEL { get; set; }
        /// <summary>
        /// 火灾编号
        /// </summary>
        public string FIRECODE { get; set; }
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