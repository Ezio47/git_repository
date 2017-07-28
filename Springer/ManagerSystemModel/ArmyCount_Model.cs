using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// Echarts队伍统计模型
    /// </summary>
   public  class ArmyCount_Model
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
        /// 队伍个数统计
        /// </summary>
        public string ArmyTypeCount { get; set; }
        /// <summary>
        /// 专业队伍个数统计
        /// </summary>
        public string ArmyType1Count { get; set; }
        /// <summary>
        /// 半专业队伍个数统计
        /// </summary>
        public string ArmyType2Count { get; set; }
        /// <summary>
        /// 应急队伍个数统计
        /// </summary>
        public string ArmyType3Count { get; set; }
        /// <summary>
        /// 群众队伍个数统计
        /// </summary>
        public string ArmyType4Count { get; set; }
        /// <summary>
        /// 队伍人数统计
        /// </summary>
        public string ArmyMemCount { get; set; }
        /// <summary>
        /// 专业队伍人数统计
        /// </summary>
        public string ArmyMem1Count { get; set; }
        /// <summary>
        /// 半专业队伍人数统计
        /// </summary>
        public string ArmyMem2Count { get; set; }
        /// <summary>
        /// 应急队伍人数统计
        /// </summary>
        public string ArmyMem3Count { get; set; }
        /// <summary>
        /// 群众队伍人数统计
        /// </summary>
        public string ArmyMem4Count { get; set; }
    }
}
