﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 数据中心_资源
    /// </summary>
    public class DC_RESOURCE_SW
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string DC_RESOURCEID { get; set; }
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