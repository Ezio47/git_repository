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
        #region 添加
        /// <summary>
        /// 添加菜单管理
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(T_SYS_MENU_Model m)
        {
            List<string> sqlList = new List<string>();

            #region T_SYS_MENU
            if (isExists(new T_SYS_MENU_SW { MENUCODE = m.MENUCODE, }) == true)
                return new Message(false, "添加失败，该编码已存在!", "");
            StringBuilder sb1 = new StringBuilder();
            sb1.AppendFormat("INSERT  INTO  T_SYS_MENU(MENUCODE,MENUNAME,MENUURL,MENUICO,LICLASS,ORDERBY,MENURIGHTFLAG,SYSFLAG,MENUOPENMETHOD,MENULINKMODE,MENUDROWMTHOD,ISTOPMENU) VALUES( ");
            sb1.AppendFormat(" '{0}'", ClsSql.EncodeSql(m.MENUCODE));
            sb1.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.MENUNAME));
            sb1.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.MENUURL));
            sb1.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.MENUICO));
            sb1.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.LICLASS));
            sb1.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ORDERBY));
            sb1.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.MENURIGHTFLAG));
            sb1.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.SYSFLAG));
            sb1.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.MENUOPENMETHOD));
            sb1.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.MENULINKMODE));
            sb1.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.MENUDROWMTHOD));
            sb1.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ISTOPMENU));
            sb1.AppendFormat(")");
            sqlList.Add(sb1.ToString());
            #endregion

            #region T_SYSSEC_RIGHT
            StringBuilder sb2 = new StringBuilder();
            sb2.AppendFormat("INSERT  INTO  T_SYSSEC_RIGHT(RIGHTID,RIGHTNAME,SYSFLAG) VALUES( ");
            sb2.AppendFormat("'{0}'", ClsSql.EncodeSql(m.MENUCODE));
            sb2.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.MENUNAME));
            sb2.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.SYSFLAG));
            sb2.AppendFormat(")");
            sqlList.Add(sb2.ToString());
            #endregion

            var y = DataBaseClass.ExecuteSqlTran(sqlList);
            if (y > 0)
                return new Message(true, "添加成功!", "");
            else
                return new Message(false, "添加失败,事物回滚机制!", "");
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
            List<string> sqlList = new List<string>();

            #region T_SYS_MENU
            StringBuilder sb1 = new StringBuilder();
            sb1.AppendFormat(" Update T_SYS_MENU SET ");
            sb1.AppendFormat(" MENUCODE='{0}'", ClsSql.EncodeSql(m.MENUCODE));
            sb1.AppendFormat(",MENUNAME='{0}'", ClsSql.EncodeSql(m.MENUNAME));
            sb1.AppendFormat(",MENUURL='{0}'", ClsSql.EncodeSql(m.MENUURL));
            sb1.AppendFormat(",MENUICO='{0}'", ClsSql.EncodeSql(m.MENUICO));
            sb1.AppendFormat(",LICLASS='{0}'", ClsSql.EncodeSql(m.LICLASS));
            sb1.AppendFormat(",ORDERBY='{0}'", ClsSql.EncodeSql(m.ORDERBY));
            sb1.AppendFormat(",MENURIGHTFLAG='{0}'", ClsSql.EncodeSql(m.MENURIGHTFLAG));
            sb1.AppendFormat(",SYSFLAG='{0}'", ClsSql.EncodeSql(m.SYSFLAG));
            sb1.AppendFormat(",MENUOPENMETHOD='{0}'", ClsSql.EncodeSql(m.MENUOPENMETHOD));
            sb1.AppendFormat(",MENULINKMODE='{0}'", ClsSql.EncodeSql(m.MENULINKMODE));
            sb1.AppendFormat(",MENUDROWMTHOD='{0}'", ClsSql.EncodeSql(m.MENUDROWMTHOD));
            sb1.AppendFormat(",ISTOPMENU='{0}'", ClsSql.EncodeSql(m.ISTOPMENU));
            sb1.AppendFormat(" where MENUCODE= '{0}'", ClsSql.EncodeSql(m.MENUCODE));
            sqlList.Add(sb1.ToString());
            #endregion

            #region T_SYSSEC_RIGHT
            StringBuilder sb2 = new StringBuilder();
            sb2.AppendFormat(" Update T_SYSSEC_RIGHT SET ");
            sb2.AppendFormat(" RIGHTID='{0}'", ClsSql.EncodeSql(m.MENUCODE));
            sb2.AppendFormat(",RIGHTNAME='{0}'", ClsSql.EncodeSql(m.MENUNAME));
            sb2.AppendFormat(",SYSFLAG='{0}'", ClsSql.EncodeSql(m.SYSFLAG));
            sb2.AppendFormat(" where RIGHTID= '{0}'", ClsSql.EncodeSql(m.MENUCODE));
            sqlList.Add(sb2.ToString());
            #endregion

            var y = DataBaseClass.ExecuteSqlTran(sqlList);
            if (y > 0)
                return new Message(true, "修改成功!", "");
            else
                return new Message(false, "修改失败,事物回滚机制!", "");
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
                    return new Message(false, "存在下属菜单管理，暂无法删除!先删除下属菜单，再删除本级!", m.returnUrl);
            }
            List<string> sqlList = new List<string>();

            #region T_SYS_MENU
            StringBuilder sb1 = new StringBuilder();
            sb1.AppendFormat("delete from T_SYS_MENU ");
            sb1.AppendFormat(" where MENUCODE  in  (");
            for (int i = 0; i < arrMENUCODE.Length; i++)
            {
                if (i != arrMENUCODE.Length - 1)
                    sb1.AppendFormat("'{0}',", ClsSql.EncodeSql(arrMENUCODE[i]));
                else
                    sb1.AppendFormat("'{0}'", ClsSql.EncodeSql(arrMENUCODE[i]));
            }
            sb1.AppendFormat(")");
            sqlList.Add(sb1.ToString()); 
            #endregion

            #region T_SYSSEC_RIGHT
            StringBuilder sb2 = new StringBuilder();
            sb2.AppendFormat("delete from T_SYSSEC_RIGHT ");
            sb2.AppendFormat(" where rightid  =");
            sb2.AppendFormat("'{0}'", ClsSql.EncodeSql(m.MENUCODE));
            sqlList.Add(sb2.ToString()); 
            #endregion

            var y = DataBaseClass.ExecuteSqlTran(sqlList);
            if (y > 0)
                return new Message(true, "删除成功!", "");
            else
                return new Message(false, "删除失败,事物回滚机制!", "");
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
