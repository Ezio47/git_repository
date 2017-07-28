using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 通讯录类别管理
    /// </summary>
   public class T_SYS_ADDREDDTYPE_SW
   {
       /// <summary>
       /// 序号
       /// </summary>
       public string ATID { get; set; }
       /// <summary>
       /// 父序号
       /// </summary>
       public string RATID { get; set; }
       /// <summary>
       /// 当前查询序号 用于选择卡默认选项
       /// </summary>
       public string CurATID { get; set; }
       /// <summary>
       /// 树形菜单用户专用　模板 {ADID}{PHONE}{PHONE}{ADNAME}{USERJOB}
       /// </summary>
       public string treeIDShowUserType { get; set; }
       /// <summary>
       /// 树形菜单用户专用　模板 {ADID}{PHONE}{PHONE}{ADNAME}{USERJOB}
       /// </summary>
       public string treeNameShowUserType { get; set; }

       /// <summary>
       /// 树形菜单类别时，ＩＤ是否为空，１空０否
       /// </summary>
       public string treeIsShowTypeID { get; set; }
       /// <summary>
       /// 树菜菜单默认打开方式
       /// </summary>
       public string isTreeOpen { get; set; }

    }
}
