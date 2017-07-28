using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 值班日期表实体
    /// </summary>
    public class OD_DATE_Model
    {        
        /// <summary>
        /// 值班表序号
        /// </summary>
        public string ONDUTYID { get; set; }
        /// <summary>
        /// 类别ID
        /// </summary>
        public string OD_TYPEID { get; set; }
        /// <summary>
        /// 值班_排班类别表id
        /// </summary>
        public int ODTYPEID { get; set; }
        /// <summary>
        /// 值班日期
        /// </summary>
        public string ONDUTYDATE { get; set; }
        /// <summary>
        /// 星期
        /// </summary>
        public string WEEK { get; set; }
        /// <summary>
        /// 操作方法
        /// </summary>
        public string opMethod { get; set; }
        /// <summary>
        /// 判断新建还是重新生成排班日期
        /// </summary>
        public string Flog { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ONDUTYYEAR { get; set; }
    }
}
