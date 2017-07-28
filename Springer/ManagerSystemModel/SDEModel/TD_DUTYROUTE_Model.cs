using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel.SDEModel
{
    /// <summary>
    /// 三维-责任路线 
    /// </summary>
    public class TD_DUTYROUTE_Model
    {
        /// <summary>
        /// 副键ID
        /// </summary>
        public string OBJECTID { get; set; }
        /// <summary>
        /// 主键ID
        /// </summary>
        public string OBJECTID_1 { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string NAME { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string DISPLAY_X { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public string DISPLAY_Y { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public string TELEPHONE { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string TYPE { get; set; }
        /// <summary>
        /// 空间数据shape字段
        /// </summary>
        public string Shape { get; set; }
        /// <summary>
        /// 方法
        /// </summary>
        public string opMethod { get; set; }
        /// <summary>
        /// 组织机构
        /// </summary>
        public string ORGNAME { get; set; }
    }
}
