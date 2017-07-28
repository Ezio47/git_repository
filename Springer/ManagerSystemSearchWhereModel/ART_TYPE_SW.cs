using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 文档类别查询条件
    /// </summary>
    public class ART_TYPE_SW
    {
        /// <summary>
        /// 类别序号
        /// </summary>
        public string ARTTYPEID { get; set; }
        /// <summary>
        /// 父类别序号
        /// </summary>
        public string ARTTYPERID { get; set; }
    }
}