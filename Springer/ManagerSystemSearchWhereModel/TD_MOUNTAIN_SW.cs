using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 
    /// </summary>
   public  class TD_MOUNTAIN_SW
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
        /// 组织机构码
        /// </summary>
        public string ORGNO { get; set; }
        /// <summary>
        /// 空间数据shape字段
        /// </summary>
        public string Shape { get; set; }
        /// <summary>
        /// 方法
        /// </summary>
        public string opMethod { get; set; }
        /// <summary>
        /// 所属自然村
        /// </summary>
        public string VILLAGE { get; set; }    
        /// <summary>
        /// 每页行数
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 当前页数
        /// </summary>
        public int curPage { get; set; }
       /// <summary>
       /// 所属类型
       /// </summary>
        public string type { get; set; }
    }
}
