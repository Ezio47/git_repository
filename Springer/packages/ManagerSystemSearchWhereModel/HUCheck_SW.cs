using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
   public class HUCheck_SW
    {
    }

   #region 漏检查询条件

   public class OmitCheckCount_SW
   {
       /// <summary>
       /// 组织机构编码
       /// </summary>
       public string ORGNO { get; set; }
       /// <summary>
       /// 开始日期
       /// </summary>
       public string DateBegin { get; set; }
       /// <summary>
       /// 结束日期
       /// </summary>
       public string DateEnd { get; set; }
       /// <summary>
       /// 查询护林员用户名
       /// </summary>
       public string HUNM { get; set; }
   }
   #endregion

   #region 考勤查询条件

   public class HUCheckINCount_SW
   {
       /// <summary>
       /// 组织机构编码
       /// </summary>
       public string ORGNO { get; set; }
       /// <summary>
       /// 开始日期
       /// </summary>
       public string DateBegin { get; set; }
       /// <summary>
       /// 结束日期
       /// </summary>
       public string DateEnd { get; set; }
       /// <summary>
       /// 查询护林员用户名
       /// </summary>
       public string HUNM { get; set; }
   }
   #endregion
}
