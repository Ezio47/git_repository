using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemSearchWhereModel
{
    /// <summary>
    /// 系统模块菜单
    /// </summary>
   public class T_SYS_MENU_SW
   {
       /// <summary>
       /// 菜单序号
       /// </summary>
       public string MENUID { get; set; }
       /// <summary>
       /// 菜单编码
       /// </summary>
       public string MENUCODE { get; set; }
       /// <summary>
       /// 菜单名称
       /// </summary>
       public string MENUNAME { get; set; }
       /// <summary>
       /// 菜单URL
       /// </summary>
       public string MENUURL { get; set; }
       /// <summary>
       /// 菜单图标
       /// </summary>
       public string MENUICO { get; set; }
       /// <summary>
       /// 显示样式
       /// </summary>
       public string LICLASS { get; set; }
       /// <summary>
       /// 排序号
       /// </summary>
       public string ORDERBY { get; set; }
       /// <summary>
       /// 菜单所属权限标识
       /// </summary>
       public string MENURIGHTFLAG { get; set; }
       /// <summary>
       /// 所属系统标识
       /// </summary>
       public string SYSFLAG { get; set; }
       /// <summary>
       /// 所属系统用户ID
       /// </summary>
       public string UID { get; set; }
       /// <summary>
       /// 页面传递类别 如数据采集
       /// </summary>
       public string Method { get; set; }
       /// <summary>
       /// 采集类别序号
       /// </summary>
       public string TID { get; set; }
       /// <summary>
       /// 菜单的打开方式
       /// </summary>
       public string MENUOPENMETHOD { get; set; }
       /// <summary>
       /// 菜单关联子模块
       /// </summary>
       public string MENULINKMODE { get; set; }
       /// <summary>
       /// 菜单的下拉方式
       /// </summary>
       public string MENUDROWMTHOD { get; set; }
       /// <summary>
       /// 是否为顶级菜单
       /// </summary>
       public string ISTOPMENU { get; set; }
       /// <summary>
       /// 获取最上级编码
       /// </summary>
       public bool IsGetTopCode { get; set; }
       /// <summary>
       /// 下级编码长度
       /// </summary>
       public int ChildCODELength { get; set; }
       /// <summary>
       /// 编码列表，用于获取指定编码的菜单 多编码以英文逗号分隔
       /// </summary>
       public string MenuCodeList { get; set; }

    }
}
