using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 护林员用户
    /// </summary>
   public  class T_IPSFR_USER_SW
    {
       /// <summary>
       /// 护林员ID
       /// </summary>
       public string HID { get; set; }
       /// <summary>
       /// 护林员名
       /// </summary>
       public string HNAME { get; set; }
       /// <summary>
       /// 终端编号
       /// </summary>
       public string SN { get; set; }
       /// <summary>
       /// 手机号码
       /// </summary>
       public string PHONE { get; set; }
       /// <summary>
       /// 性别
       /// </summary>
       public string SEX { get; set; }
       /// <summary>
       /// 出生日期
       /// </summary>
       public string BIRTH { get; set; }
       /// <summary>
       /// 固\兼职
       /// </summary>
       public string ONSTATE { get; set; }
       /// <summary>
       /// 所属机构编码
       /// </summary>
       public string BYORGNO { get; set; }
       /// <summary>
       /// 是否有效
       /// </summary>     
       public string ISENABLE { get; set; }
       /// <summary>
       /// 需巡检距离
       /// </summary>
       public string PATROLLENGTH { get; set; }
       /// <summary>
       /// 护林员和电话号码
       /// </summary>
       public string PhoneHname { get; set; }
       /// <summary>
       /// 多个组织机构编码
       /// </summary>
       public string Orgs { get; set; }

       /// <summary>
       /// 每页行数
       /// </summary>
       public int pageSize { get; set; }
       /// <summary>
       /// 当前页数
       /// </summary>
       public int curPage { get; set; }
       /// <summary>
       /// 护林员参数
       /// </summary>
       public string MOBILEPARAMLIST { get; set; }
       /// <summary>
       /// 手机号码集合
       /// </summary>
       public string PHONELIST { get; set; }
       /// <summary>
       /// 顶级单位编码
       /// </summary>
       public string TopORGNO { get; set; }

    }
}
