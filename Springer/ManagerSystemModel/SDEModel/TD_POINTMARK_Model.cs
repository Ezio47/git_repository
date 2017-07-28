﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel.SDEModel
{
    /// <summary>
    /// 三维自然村添加
    /// </summary>
   public class TD_POINTMARK_Model
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string OBJECTID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string NAME { get; set; }
        /// <summary>
        /// 地图名称
        /// </summary>
        public string MAPNAME { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string JD { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public string WD { get; set; }
        /// <summary>
        /// 所属组织机构
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 空间数据shape字段
        /// </summary>
        public string Shape { get; set; }
        /// <summary>
        /// 方法
        /// </summary>
        public string opMethod { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string KIND { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string TELEPHONE { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string ADDRESS { get; set; }
       /// <summary>
       /// 组织机构名称
       /// </summary>
        public string ORGNAME { get; set; }

        /// <summary>
        /// 所属县市
        /// </summary>
        public string BYORGNOXS { get; set; }
        /// <summary>
        /// 县市名称
        /// </summary>
        public string ORGXSNAME { get; set; }
       /// <summary>
       /// 所属类型
       /// </summary>
        public string TYPE { get; set; }
       /// <summary>
       /// 类型名称
       /// </summary>
        public string TYPENAME { get; set; }
        /// <summary>
        /// 所属自然村
        /// </summary>
        public string VILLAGE { get; set; }
    
    }
}
