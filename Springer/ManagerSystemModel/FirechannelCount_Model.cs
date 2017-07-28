﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 放火通道-echarts
    /// </summary>
   public class FirechannelCount_Model
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
        /// 便道数统计
        /// </summary>
        public string Fireleveltype1Count { get; set; }
        /// <summary>
        /// 林区道路数统计
        /// </summary>
        public string Fireleveltype2Count { get; set; }
        /// <summary>
        /// 人行道数统计
        /// </summary>
        public string Fireusetypetype1Count { get; set; }
        /// <summary>
        /// 车行道数统计
        /// </summary>
        public string Fireusetypetype2Count { get; set; }
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
        /// <summary>
        /// 未维护类型数统计
        /// </summary>
        public string Managerstate1Count { get; set; }
        /// <summary>
        /// 维护类型数统计
        /// </summary>
        public string Managerstate2Count { get; set; }
        /// <summary>
        /// 新建类型数统计
        /// </summary>
        public string Managerstate3Count { get; set; }
    }
}
