using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 群组的模型
    /// </summary>
   public  class E_GROUP_Model
    {
        /// <summary>
        /// 群组id
        /// </summary>
       public string EGROUPID { get; set; }
       /// <summary>
       /// 群组关联人员id
       /// </summary>
       public string EGROUPUSERID { get; set; }
       /// <summary>
       /// 群组名称
       /// </summary>
       public string EGROUPNAME{ get; set; }
       /// <summary>
       /// 群组成员
       /// </summary>
       public string EGROUPMEMBERLIST { get; set; }
       /// <summary>
       /// 电话号码组合
       /// </summary>
       public string EGROUPPHONELIST { get; set; }
       /// <summary>
       /// 确定群组的类型1,表示邮件群组,2表示短信群组
       /// </summary>
       public string EGROUPTYPE { get; set; }
       /// <summary>
       /// 操作方法
       /// </summary>
       public string opMethod { get; set; }
       /// <summary>
       /// 返回地址
       /// </summary>
       public string URL { get; set; }
    }
}
