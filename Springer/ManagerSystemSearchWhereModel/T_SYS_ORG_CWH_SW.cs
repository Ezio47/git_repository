using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 公用_机构表_村委会
    /// </summary>
   public class T_SYS_ORG_CWH_SW
   {
       /// <summary>
       /// 序号
       /// </summary>
       public string CWHID { get; set; }
       /// <summary>
       /// 机构编码
       /// </summary>
       public string ORGNO { get; set; }
       /// <summary>
       /// 所属机构编码
       /// </summary>
       public string BYORGNO { get; set; }
       /// <summary>
       /// 村委会名称
       /// </summary>
       public string CWHNAME { get; set; }
       /// <summary>
       /// 所属类型
       /// </summary>
       public string ORGLINKTYPE { get; set; }
       /// <summary>
       /// 所关联的自然村名字
       /// </summary>
       public string UNITNAME { get; set; }
       /// <summary>
       /// 所有县的机构吗
       /// </summary>
       public string GetContyORGNOByCity { get; set; }
       /// <summary>
       /// 所有乡镇机构吗
       /// </summary>
       public string GetXZOrgNOByConty { get; set; }
       /// <summary>
       /// 只取市县
       /// </summary>
       public string OnlyGetShiXian { get; set; }
       /// <summary>
       /// 顶级组织机构吗
       /// </summary>
       public string TopORGNO { get; set; }
    }
}
