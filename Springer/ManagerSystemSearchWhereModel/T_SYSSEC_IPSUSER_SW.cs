using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 系统用户护林员扩展
    /// </summary>
   public class T_SYSSEC_IPSUSER_SW
   {
       /// <summary>
       /// 序号
       /// </summary>
       public string USERID { get; set; }
       /// <summary>
       /// 登录用户名
       /// </summary>
       public string LOGINUSERNAME { get; set; }
       /// <summary>
       /// 用户真实姓名
       /// </summary>
       public string USERNAME { get; set; }
       /// <summary>
       /// 当前机构编码，用于获取该机构的所有用户（不含下级单位用户）
       /// </summary>
       public string curOrgNo { get; set; }
       /// <summary>
       /// 用户密码
       /// </summary>
       public string USERPWD { get; set; }
       /// <summary>
       /// 每页行数
       /// </summary>
       public int pageSize { get; set; }
       /// <summary>
       /// 当前页数
       /// </summary>
       public int curPage { get; set; }
       /// <summary>
       /// 格式化组合用户信息字符串 将字段信息替换 [userName] 中文名 [orgName] 单位名 其他待补充
       /// </summary>
       public string formatUserStr { get; set; }
       /// <summary>
       /// 多用户之间分隔符
       /// </summary>
       public string splitUserStr { get; set; }
       /// <summary>
       /// 科室
       /// </summary>
       public string DEPARTMENT { get; set; }
       /// <summary>
       /// 组织机构
       /// </summary>
       public string ORGNO { get; set; }
       /// <summary>
       /// 手机号码
       /// </summary>
       public string phone { get; set; }
       /// <summary>
       /// 是否开通OA 1：开通；0：未开通
       /// </summary>
       public string IsOpenOA { get; set; }
    }
}
