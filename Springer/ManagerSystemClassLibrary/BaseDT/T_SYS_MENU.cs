using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel;
using DataBaseClassLibrary;
using PublicClassLibrary;

namespace ManagerSystemClassLibrary.BaseDT
{
    /// <summary>
    /// 系统菜单管理
    /// </summary>
    public class T_SYS_MENU
    {
        ///// <summary>
        ///// 获取列表
        ///// </summary>
        ///// <param name="sw">参见模型 sw.SYSFLAG系统标识符 sw.MENUCODE 菜单编码</param>
        ///// <returns></returns>
        //public static DataTable getDT(T_SYS_MENU_SW sw)
        //{

        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendFormat("SELECT    MENUID, MENUCODE, MENUNAME, MENUURL, MENUICO, LICLASS, ORDERBY, MENURIGHTFLAG, SYSFLAG");
        //    sb.AppendFormat(" FROM      T_SYS_MENU");
        //    sb.AppendFormat(" WHERE   1=1");
        //    if (string.IsNullOrEmpty(sw.SYSFLAG) == false)
        //        sb.AppendFormat(" AND SYSFLAG = '{0}'", sw.SYSFLAG);
        //    if (string.IsNullOrEmpty(sw.MENUCODE) == false)
        //        sb.AppendFormat(" AND MENUCODE = '{0}'", ClsSql.EncodeSql(sw.MENUCODE));
        //    sb.AppendFormat(" ORDER BY MENUCODE");

        //    DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
        //    return ds.Tables[0];
        //}

        #region 添加
        /// <summary>
        /// 添加菜单管理
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(T_SYS_MENU_Model m)
        {
            if (isExists(new T_SYS_MENU_SW { MENUCODE = m.MENUCODE, }) == true)
                return new Message(false, "添加失败，该编码已存在!", "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT  INTO  T_SYS_MENU(MENUCODE,MENUNAME,MENUURL,MENUICO,LICLASS,ORDERBY,MENURIGHTFLAG,SYSFLAG,MENUOPENMETHOD,MENULINKMODE,MENUDROWMTHOD,ISTOPMENU)");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.MENUCODE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.MENUNAME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.MENUURL));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.MENUICO));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.LICLASS));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ORDERBY));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.MENURIGHTFLAG));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.SYSFLAG));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.MENUOPENMETHOD));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.MENULINKMODE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.MENUDROWMTHOD));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ISTOPMENU));
            sb.AppendFormat(")");
            //添加数据至T_SYSSEC_RIGHT表中
            sb.AppendFormat(";");
            sb.AppendFormat("INSERT  INTO  T_SYSSEC_RIGHT(RIGHTID,RIGHTNAME,SYSFLAG)");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.MENUCODE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.MENUNAME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.SYSFLAG));
            sb.AppendFormat(")");
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "添加成功!", m.returnUrl);
            else
                return new Message(false, "添加失败，请检查各输入框是否正确!", m.returnUrl);
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Mdy(T_SYS_MENU_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Update T_SYS_MENU");
            sb.AppendFormat(" set ");
            sb.AppendFormat("MENUCODE='{0}'", ClsSql.EncodeSql(m.MENUCODE));
            sb.AppendFormat(",MENUNAME='{0}'", ClsSql.EncodeSql(m.MENUNAME));
            sb.AppendFormat(",MENUURL='{0}'", ClsSql.EncodeSql(m.MENUURL));
            sb.AppendFormat(",MENUICO='{0}'", ClsSql.EncodeSql(m.MENUICO));
            sb.AppendFormat(",LICLASS='{0}'", ClsSql.EncodeSql(m.LICLASS));
            sb.AppendFormat(",ORDERBY='{0}'", ClsSql.EncodeSql(m.ORDERBY));
            sb.AppendFormat(",MENURIGHTFLAG='{0}'", ClsSql.EncodeSql(m.MENURIGHTFLAG));
            sb.AppendFormat(",SYSFLAG='{0}'", ClsSql.EncodeSql(m.SYSFLAG));
            sb.AppendFormat(",MENUOPENMETHOD='{0}'", ClsSql.EncodeSql(m.MENUOPENMETHOD));
            sb.AppendFormat(",MENULINKMODE='{0}'", ClsSql.EncodeSql(m.MENULINKMODE));
            sb.AppendFormat(",MENUDROWMTHOD='{0}'", ClsSql.EncodeSql(m.MENUDROWMTHOD));
            sb.AppendFormat(",ISTOPMENU='{0}'", ClsSql.EncodeSql(m.ISTOPMENU));
            sb.AppendFormat(" where MENUCODE= '{0}'", ClsSql.EncodeSql(m.MENUCODE));

            //修改T_SYSSEC_RIGHT表中的数据
            // sb.AppendFormat(";");
            sb.AppendFormat("Update T_SYSSEC_RIGHT");
            sb.AppendFormat(" set ");
            sb.AppendFormat("RIGHTID='{0}'", ClsSql.EncodeSql(m.MENUCODE));
            sb.AppendFormat(",RIGHTNAME='{0}'", ClsSql.EncodeSql(m.MENUNAME));
            sb.AppendFormat(",SYSFLAG='{0}'", ClsSql.EncodeSql(m.SYSFLAG));
            sb.AppendFormat(" where RIGHTID= '{0}'", ClsSql.EncodeSql(m.MENUCODE));

            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功!", m.returnUrl);
            else
                return new Message(false, "修改失败，请检查各输入框是否正确!", m.returnUrl);
        }

        #endregion

        #region 删除
        /// <summary>
        /// 删除(包含所有的子目录）
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(T_SYS_MENU_Model m)
        {
            string[] arrMENUCODE = m.MENUCODE.Split(',');
            for (int i = 0; i < arrMENUCODE.Length; i++)
            {
                if (isExistsChild(new T_SYS_MENU_SW { MENUCODE = arrMENUCODE[i] }))
                {
                    return new Message(false, "存在下属菜单管理，暂无法删除!先删除下属菜单，再删除本级!", m.returnUrl);
                }
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete from T_SYS_MENU");
            sb.AppendFormat(" where MENUCODE  in  (");
            for (int i = 0; i < arrMENUCODE.Length; i++)
            {
                if (i != arrMENUCODE.Length - 1)
                    sb.AppendFormat("'{0}',", ClsSql.EncodeSql(arrMENUCODE[i]));
                else
                    sb.AppendFormat("'{0}'", ClsSql.EncodeSql(arrMENUCODE[i]));
            }
            sb.AppendFormat(")");
            //删除T_SYSSEC_RIGHT表中的数据
            sb.AppendFormat("delete from T_SYSSEC_RIGHT");
            sb.AppendFormat(" where rightid  =");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.MENUCODE));

            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功！", "");
            else
                return new Message(false, "删除失败，请检查各输入框是否正确！", "");
        }

        #endregion

        #region 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static DataTable getDT(T_SYS_MENU_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT MENUCODE,MENUNAME,MENUURL,MENUICO,LICLASS,ORDERBY,MENURIGHTFLAG,SYSFLAG,MENUOPENMETHOD,MENULINKMODE,MENUDROWMTHOD,ISTOPMENU");
            sb.AppendFormat(" FROM  T_SYS_MENU");
            sb.AppendFormat(" WHERE   1=1");
            if (sw.IsGetTopCode)
                sb.AppendFormat(" AND Len(MENUCODE)='3'");
            if (!string.IsNullOrEmpty(sw.MENUCODE))
                sb.AppendFormat(" AND MENUCODE like '%{0}%'", ClsSql.EncodeSql(sw.MENUCODE));
            if (string.IsNullOrEmpty(sw.MENUNAME) == false)
                sb.AppendFormat(" AND MENUNAME like '%{0}%'", ClsSql.EncodeSql(sw.MENUNAME));

            if (string.IsNullOrEmpty(sw.MENUCODE) == false)
            {
                sb.AppendFormat(" AND Len(MENUCODE) = '{0}'", ClsSql.EncodeSql(sw.ChildCODELength.ToString()));
                sb.AppendFormat(" AND Substring(MENUCODE,1,{0}) = '{1}'", ClsSql.EncodeSql(sw.MENUCODE).Length.ToString(), ClsSql.EncodeSql(sw.MENUCODE));
            }
            sb.AppendFormat(" ORDER BY ORDERBY");
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }

        #endregion

        #region 判断记录是否存在
        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>true存在 false不存在 </returns>
        public static bool isExists(T_SYS_MENU_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from T_SYS_MENU where 1=1");
            if (string.IsNullOrEmpty(sw.MENUCODE) == false)
                sb.AppendFormat(" and MENUCODE='{0}'", ClsSql.EncodeSql(sw.MENUCODE));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion

        #region 判断是否有下级
        /// <summary>
        /// 判断记录是否存在下级
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>true存在 false不存在 </returns>
        public static bool isExistsChild(T_SYS_MENU_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from T_SYS_MENU where 1=1");
            if (string.IsNullOrEmpty(sw.MENUCODE) == false)
            {
                sb.AppendFormat(" AND len(MENUCODE)>'{0}'", ClsSql.EncodeSql(sw.MENUCODE).Length);
                sb.AppendFormat(" AND substring(MENUCODE,1,{0})= '{1}'", ClsSql.EncodeSql(sw.MENUCODE).Length, ClsSql.EncodeSql(sw.MENUCODE));
            }
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion

        #region 获取数据模型
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getModel(T_SYS_MENU_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT MENUCODE,MENUNAME,MENUURL,MENUICO,LICLASS,ORDERBY,MENURIGHTFLAG,SYSFLAG,MENUOPENMETHOD,MENULINKMODE,MENUDROWMTHOD,ISTOPMENU FROM T_SYS_MENU WHERE 1=1");
            if (!string.IsNullOrEmpty(sw.MENUCODE))
                sb.AppendFormat(" AND MENUCODE = '{0}'", ClsSql.EncodeSql(sw.MENUCODE));
            if (!string.IsNullOrEmpty(sw.MENUNAME))
                sb.AppendFormat(" AND MENUNAME = '{0}'", ClsSql.EncodeSql(sw.MENUNAME));
            if (!string.IsNullOrEmpty(sw.MENUURL))
                sb.AppendFormat(" AND MENUURL = '{0}'", ClsSql.EncodeSql(sw.MENUURL));
            if (!string.IsNullOrEmpty(sw.MENUICO))
                sb.AppendFormat(" AND MENUICO = '{0}'", ClsSql.EncodeSql(sw.MENUICO));
            if (!string.IsNullOrEmpty(sw.LICLASS))
                sb.AppendFormat(" AND LICLASS = '{0}'", ClsSql.EncodeSql(sw.LICLASS));
            if (!string.IsNullOrEmpty(sw.ORDERBY))
                sb.AppendFormat(" AND ORDERBY = '{0}'", ClsSql.EncodeSql(sw.ORDERBY));
            if (!string.IsNullOrEmpty(sw.MENURIGHTFLAG))
                sb.AppendFormat(" AND MENURIGHTFLAG = '{0}'", ClsSql.EncodeSql(sw.MENURIGHTFLAG));
            if (!string.IsNullOrEmpty(sw.SYSFLAG))
                sb.AppendFormat(" AND SYSFLAG = '{0}'", ClsSql.EncodeSql(sw.SYSFLAG));
            if (!string.IsNullOrEmpty(sw.MENUOPENMETHOD))
                sb.AppendFormat(" AND MENUOPENMETHOD = '{0}'", ClsSql.EncodeSql(sw.MENUOPENMETHOD));
            if (!string.IsNullOrEmpty(sw.MENULINKMODE))
                sb.AppendFormat(" AND MENULINKMODE = '{0}'", ClsSql.EncodeSql(sw.MENULINKMODE));
            if (!string.IsNullOrEmpty(sw.MENUDROWMTHOD))
                sb.AppendFormat(" AND MENUDROWMTHOD = '{0}'", ClsSql.EncodeSql(sw.MENUDROWMTHOD));
            if (!string.IsNullOrEmpty(sw.ISTOPMENU))
                sb.AppendFormat(" AND ISTOPMENU = '{0}'", ClsSql.EncodeSql(sw.ISTOPMENU));
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        #endregion

        #region 根据菜单编码查询菜单名称
        /// <summary>
        /// 根据菜单编码查询菜单名称
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static string getMenuNameByCode(T_SYS_MENU_SW sw)
        {
            string sql = "SELECT MENUNAME FROM T_SYS_MENU WHERE MENUCODE='" + sw.MENUCODE + "' and SYSFLAG='" + sw.SYSFLAG + "'";
            return DataBaseClass.ReturnSqlField(sql);
        }
        #endregion
    }
}
