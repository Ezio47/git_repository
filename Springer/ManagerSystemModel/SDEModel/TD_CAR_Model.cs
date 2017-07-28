using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel.SDEModel
{
    /// <summary>
    /// 三维-车辆
    /// </summary>
    public class TD_CAR_Model
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
    
    }
}
