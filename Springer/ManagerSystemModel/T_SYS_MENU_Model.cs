using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    #region 系统菜单
    /// <summary>
    /// 系统菜单
    /// </summary>
    public class SystemMenu_Model
    {
        /// <summary>
        /// 菜单编码
        /// </summary>
        public string MENUCODE { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MENUNAME { get; set; }
        /// <summary>
        /// 菜单地址
        /// </summary>
        public string MENUURL { get; set; }
        /// <summary>
        /// 菜单ICO图标
        /// </summary>
        public string MENUICO { get; set; }
        /// <summary>
        /// 显示样式
        /// </summary>
        public string LICLASS { get; set; }


        /// <summary>
        /// 菜单打开方式
        /// </summary>
        public string MENUOPENMETHOD { get; set; }
        /// <summary>
        /// 菜单关联子模块编码
        /// </summary>
        public string MENULINKMODE { get; set; }
        /// <summary>
        /// 菜单下拉方式
        /// </summary>
        public string MENUDROWMTHOD { get; set; }
        /// <summary>
        /// 是否为顶部菜单
        /// </summary>
        public string ISTOPMENU { get; set; }

        /// <summary>
        /// 用于显示提醒数量（根据需要，如火情、上报需要，其他模块不需要）
        /// </summary>
        public string showCount { get; set; }
        /// <summary>
        /// 存放子菜单
        /// </summary>
        public IEnumerable< T_SYS_MENU_Model> subMenuModel { get; set; }
    }

    #endregion
    /// <summary>
    /// 系统菜单
    /// </summary>
   public class T_SYS_MENU_Model
   {
       /// <summary>
       /// 菜单编码
       /// </summary>
       public string MENUCODE { get; set; }
       /// <summary>
       /// 菜单名称
       /// </summary>
       public string MENUNAME { get; set; }
       /// <summary>
       /// 菜单地址
       /// </summary>
       public string MENUURL { get; set; }
       /// <summary>
       /// 菜单ICO图标
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
       /// 对应权限编码
       /// </summary>
       public string MENURIGHTFLAG { get; set; }
       /// <summary>
       /// 所属系统标识
       /// </summary>
       public string SYSFLAG { get; set; }
       /// <summary>
       /// 菜单打开方式
       /// </summary>
       public string MENUOPENMETHOD { get; set; }
       /// <summary>
       /// 菜单打开方式名称
       /// </summary>
       public string MENUOPENMETHODName { get; set; }
       /// <summary>
       /// 菜单关联子模块编码
       /// </summary>
       public string MENULINKMODE { get; set; }
       /// <summary>
       /// 菜单下拉方式
       /// </summary>
       public string MENUDROWMTHOD { get; set; }
       /// <summary>
       /// 菜单下拉方式名称
       /// </summary>
       public string MENUDROWMTHODName { get; set; }
       /// <summary>
       /// 是否为顶部菜单
       /// </summary>
       public string ISTOPMENU { get; set; }
       /// <summary>
       /// 是否为顶部菜单名称
       /// </summary>
       public string ISTOPMENUName { get; set; }

      
       /// <summary>
       /// 用于显示提醒数量（根据需要，如火情、上报需要，其他模块不需要）
       /// </summary>

       public string showCount { get; set; }
       /// <summary>
       /// 类别ID，用于采集
       /// </summary>
       public string TID { get; set; }
       /// <summary>
       /// 操作方法
       /// </summary>
       public string opMethod { get; set; }
       /// <summary>
       /// 返回网址
       /// </summary>
       public string returnUrl { get; set; }
    }
}
