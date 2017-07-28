﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 有害生物_报表_人财物类别表
    /// </summary>
    public class PEST_REPORT_RCWTYPE_SW
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string RCWCODE { get; set; }
        /// <summary>
        /// 人财物名称
        /// </summary>
        public string RCWNAME { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public string ORDERBY { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public string ISUSING { get; set; }
        /// <summary>
        /// 是否获取报表类别标题
        /// </summary>
        public bool ISGetOnlyTypeTitle { get; set; }
    }
}
