using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 隔离带-Echarts
    /// </summary>
   public class IsolationstripCount_Model
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
        /// 生物数统计
        /// </summary>
        public string IsolationstripType1Count { get; set; }
        /// <summary>
        /// 生物数长度统计
        /// </summary>
        public string IsolationstripTypeLength1Count { get; set; }
        /// <summary>
        /// 生土数统计
        /// </summary>
        public string IsolationstripType2Count { get; set; }
        /// <summary>
        /// 生土长度数统计
        /// </summary>
        public string IsolationstripTypeLength2Count { get; set; }
        /// <summary>
        /// 火烧线数统计
        /// </summary>
        public string IsolationstripType3Count { get; set; }
        /// <summary>
        /// 火烧线数长度统计
        /// </summary>
        public string IsolationstripTypeLength3Count { get; set; }
        /// <summary>
        /// 林下烧除数统计
        /// </summary>
        public string IsolationstripType4Count { get; set; }
        /// <summary>
        /// 林下烧除长度统计
        /// </summary>
        public string IsolationstripTypeLength4Count { get; set; }
        /// <summary>
        /// 规划生物隔离带数统计
        /// </summary>
        public string IsolationstripType5Count { get; set; }
        /// <summary>
        /// 规划生物隔离带长度数统计
        /// </summary>
        public string IsolationstripTypeLength5Count { get; set; }
        /// <summary>
        /// 在用类型统计
        /// </summary>
        public string Usestate1Count { get; set; }
        /// <summary>
        /// 在用类型长度统计
        /// </summary>
        public string UsestateLength1Count { get; set; }
        /// <summary>
        /// 规划类型数统计
        /// </summary>
        public string Usestate2Count { get; set; }
        /// <summary>
        /// 规划类型数长度统计
        /// </summary>
        public string UsestateLength2Count { get; set; }
        /// <summary>
        /// 报废类型数统计
        /// </summary>
        public string Usestate3Count { get; set; }
        /// <summary>
        /// 报废类型数长度统计
        /// </summary>
        public string UsestateLength3Count { get; set; }
        /// <summary>
        /// 未维护类型数统计
        /// </summary>
        public string Managerstate1Count { get; set; }
        /// <summary>
        /// 未维护类型数长度统计
        /// </summary>
        public string ManagerstateLength1Count { get; set; }
        /// <summary>
        /// 维护类型数统计
        /// </summary>
        public string Managerstate2Count { get; set; }
        /// <summary>
        /// 维护类型数长度统计
        /// </summary>
        public string ManagerstateLength2Count { get; set; }
        /// <summary>
        /// 新建类型数统计
        /// </summary>
        public string Managerstate3Count { get; set; }
        /// <summary>
        /// 新建类型数长度统计
        /// </summary>
        public string ManagerstateLength3Count { get; set; }
    }
}
