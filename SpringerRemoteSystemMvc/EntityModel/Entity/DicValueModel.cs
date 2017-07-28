using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Springer.EntityModel.Entity
{
    /// <summary>
    /// 数据字典
    /// </summary>
    public class DicValueModel
    {

        /// <summary>
        /// 类型名
        /// </summary>
        public string typename { get; set; }
        /// <summary>
        /// 类型ID
        /// </summary>
        public string typeid { get; set; }

        /// <summary>
        /// 点线面类型
        /// </summary>
        public string spatialtype { get; set; }
    }
}
