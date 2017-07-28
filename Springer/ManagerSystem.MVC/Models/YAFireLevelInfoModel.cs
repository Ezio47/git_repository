using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerSystem.MVC.Models
{
    /// <summary>
    /// 预案火情等级信息
    /// </summary>
    public class YAFireLevelInfoModel
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
        /// 火情名称
        /// </summary>
        public string FIRENAME { get; set; }
        /// <summary>
        /// 起火时间
        /// </summary>
        public string FIRETIME { get; set; }
        /// <summary>
        /// 起火结束时间
        /// </summary>
        public string FIREENDTIME { get; set; }
        /// <summary>
        /// 是否已灭 1为已灭
        /// </summary>
        public string ISOUTFIRE { get; set; }

        /// <summary>
        /// 是否已灭NAME 1为已灭
        /// </summary>
        public string ISOUTFIRENAME { get; set; }
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
        /// 是否敏感时段
        /// </summary>
        public string MGSDNAME { get; set; }
        /// <summary>
        /// 是否重点区域
        /// </summary>
        public string ZDQY { get; set; }
        /// <summary>
        /// 是否重点区域
        /// </summary>
        public string ZDQYNAME { get; set; }
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
        /// 经度
        /// </summary>
        public string JD { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public string WD { get; set; }
    }
}