using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 数据中心_装备
    /// </summary>
    public class DC_EQUIP_Model
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string DC_EQUIPID {get;set;}
        /// <summary>
        /// 类别序号
        /// </summary>
        public string TYPEID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string FACINAME { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string JD { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public string WD { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string MARK { get; set; }
    }
}
