using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 数据中心_专业队
    /// </summary>
    public class DC_PROTEAM_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string DC_PROTEAMID {get;set; }
        /// <summary>
        /// 类别序号
        /// </summary>
        public string TYPEID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string PROTEAMNAME { get; set; }
        /// <summary>
        /// 装备
        /// </summary>
        public string EQUIP { get; set; }
        /// <summary>
        /// 能力
        /// </summary>
        public string CAPACITY { get; set; }
        /// <summary>
        /// 领导
        /// </summary>
        public string LEADER { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string LINKWAY { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string JD { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public string WD { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public string ORDERBY { get; set; }
    }
}
