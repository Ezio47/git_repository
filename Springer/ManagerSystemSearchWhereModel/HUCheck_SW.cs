using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 护林员考核
    /// </summary>
   public class HUCheck_SW
    {
    }
   #region 出围查询条件
    /// <summary>
    /// 出围统计
    /// </summary>
   public class OutRaiLCount_SW
   {
       /// <summary>
       /// 组织机构编码
       /// </summary>
       public string orgNo { get; set; }
       /// <summary>
       /// 开始日期
       /// </summary>
       public string DateBegin { get; set; }
       /// <summary>
       /// 结束日期
       /// </summary>
       public string DateEnd { get; set; }
       /// <summary>
       /// 查询护林员电话、用户名
       /// </summary>
       public string PhoneHname { get; set; }
       /// <summary>
       /// 顶级单位编码
       /// </summary>
       public string TopORGNO { get; set; }
   }
   #endregion

   #region 怠工查询条件
    /// <summary>
    /// 怠工统计
    /// </summary>
   public class SabotageCount_SW
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
       public string PhoneHname { get; set; }
       /// <summary>
       /// 顶级单位编码
       /// </summary>
       public string TopORGNO { get; set; }
   }
   #endregion

   #region 漏检查询条件
    /// <summary>
    /// 漏检统计
    /// </summary>
   public class OmitCheckCount_SW
   {
       /// <summary>
       /// 护林员编号
       /// </summary>
       public string HID { get; set; }
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
       /// 查询护林员电话、用户名
       /// </summary>
       public string PhoneHname { get; set; }
       /// <summary>
       /// 顶级单位编码
       /// </summary>
       public string TopORGNO { get; set; }
   }
   #endregion

   #region 考勤查询条件
    /// <summary>
    /// 考勤统计
    /// </summary>
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
