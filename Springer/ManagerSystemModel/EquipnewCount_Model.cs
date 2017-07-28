using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 装备-Echarts
    /// </summary>
    public class EquipnewCount_Model
    {
        /// <summary>
        /// 组织机构码
        /// </summary>
        public string ORGNo { get; set; }
        /// <summary>
        /// 组织机构名
        /// </summary>
        public string ORGName { get; set; }
        /// <summary>
        /// 扑救类统计
        /// </summary>
        public string Equiptyp1Count { get; set; }
        /// <summary>
        /// 阻隔类统计
        /// </summary>
        public string Equiptyp2Count { get; set; }
        /// <summary>
        /// 防护类统计
        /// </summary>
        public string Equiptyp3Count { get; set; }
        /// <summary>
        /// 通讯类统计
        /// </summary>
        public string Equiptyp4Count { get; set; }
        /// <summary>
        /// 户外类统计
        /// </summary>
        public string Equiptyp5Count { get; set; }
        /// <summary>
        /// 运输类统计
        /// </summary>
        public string Equiptyp6Count { get; set; }
        /// <summary>
        /// 在用类型统计
        /// </summary>
        public string Usestate1Count { get; set; }
        /// <summary>
        /// 规划类型数统计
        /// </summary>
        public string Usestate2Count { get; set; }
        /// <summary>
        /// 报废类型数统计
        /// </summary>
        public string Usestate3Count { get; set; }
    }
}
