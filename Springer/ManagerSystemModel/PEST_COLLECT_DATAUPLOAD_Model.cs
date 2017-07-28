﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 有害生物_采集数据上报上传表
    /// </summary>
    public class PEST_COLLECT_DATAUPLOAD_Model
    {
        /// <summary>
        /// 上传ID
        /// </summary>
        public string  PESTCOLLDATAUPLOADID { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public string PESTCOLLDATAID { get; set; }
        /// <summary>
        /// 上传URL
        /// </summary>
        public string UPLOADURL { get; set; }
        /// <summary>
        /// 上传名
        /// </summary>
        public string UPLOADNAME { get; set; }
        /// <summary>
        /// 上传描述
        /// </summary>
        public string UPLOADDESCRIBE { get; set; }
        /// <summary>
        /// 上传类型
        /// </summary>
        public string  UPLOADTYPE { get; set; }
        /// <summary>
        /// 操作方法
        /// </summary>
        public string opMethod { get; set; }
        /// <summary>
        /// 返回网址
        /// </summary>
        public string returnUrl { get; set; }
    }
}