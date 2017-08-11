using DataBaseClassLibrary;
using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using PublicClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary.BaseDT
{
    /// <summary>
    /// 公用_机构表_村委会
    /// </summary>
    public class T_SYS_ORG_CWH
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(T_SYS_ORG_CWH_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  T_SYS_ORG_CWH( BYORGNO, CWHNAME, ORDERBY)");
            sb.AppendFormat("VALUES("); 
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.BYORGNO));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.CWHNAME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ORDERBY));
            sb.AppendFormat(")");
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "添加成功！", "");
            else
                return new Message(false, "添加失败，请检查各输入框是否正确！", "");
        }

        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Mdy(T_SYS_ORG_CWH_Model m)
        {
            if (string.IsNullOrEmpty(m.ORDERBY))//如果排序号为空，则取最大+1
                m.ORDERBY = DataBaseClass.ReturnSqlField("select max(ORDERBY)+1 from T_SYS_ORG_CWH where BYORGNO='" + ClsSql.EncodeSql(m.BYORGNO) + "'");
            if (string.IsNullOrEmpty(m.ORDERBY))
                m.ORDERBY = "1";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE T_SYS_ORG_CWH");
            sb.AppendFormat(" set ");
            sb.AppendFormat("BYORGNO='{0}'", ClsSql.EncodeSql(m.BYORGNO));
            sb.AppendFormat(",CWHNAME='{0}'", ClsSql.EncodeSql(m.CWHNAME));
            sb.AppendFormat(",ORDERBY='{0}'", ClsSql.EncodeSql(m.ORDERBY));
            sb.AppendFormat(" where CWHID= '{0}'", ClsSql.EncodeSql(m.CWHID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功！", "");
            else
                return new Message(false, "修改失败，请检查各输入框是否正确！", "");
        }

        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(T_SYS_ORG_CWH_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete T_SYS_ORG_CWH");
            sb.AppendFormat(" where CWHID= '{0}'", ClsSql.EncodeSql(m.CWHID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功！", "");
            else
                return new Message(false, "删除失败，请检查各输入框是否正确！", "");
        }

        #endregion

        #region 判断记录是否存在
        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>true存在 false不存在</returns>
        public static bool isExists(T_SYS_ORG_CWH_SW sw)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from T_SYS_ORG_CWH where 1=1");
            sb.AppendFormat(" where 1= 1", ClsSql.EncodeSql(sw.CWHID));
            if (string.IsNullOrEmpty(sw.CWHID) == false)
                sb.AppendFormat(" AND CWHID= '{0}'", ClsSql.EncodeSql(sw.CWHID));
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
                sb.AppendFormat(" AND BYORGNO= '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            if (string.IsNullOrEmpty(sw.CWHNAME) == false)
                sb.AppendFormat(" AND CWHNAME= '{0}'", ClsSql.EncodeSql(sw.CWHNAME));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }

        #endregion

        #region 获取记录
        /// <summary>
        /// 获取记录
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(T_SYS_ORG_CWH_SW sw)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat(" FROM      T_SYS_ORG_CWH");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.CWHID) == false)
                sb.AppendFormat(" AND CWHID = '{0}'", ClsSql.EncodeSql(sw.CWHID));
            if (string.IsNullOrEmpty(sw.ORGNO) == false)
                sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.ORGNO));
            if (string.IsNullOrEmpty(sw.CWHNAME) == false)
                sb.AppendFormat(" AND (CWHNAME like '%{0}%')", ClsSql.EncodeSql(sw.CWHNAME));

            if (!string.IsNullOrEmpty(sw.BYORGNO))
            {
                if (sw.BYORGNO.Substring(4, 11) == "00000000000")//获取所有市的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,4) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
                else if (sw.BYORGNO.Substring(6, 9) == "000000000")//获取所有县的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,6) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
                else if (sw.BYORGNO.Substring(9, 6) == "000000")//获取所有乡镇的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,9) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 9)));
                else
                    sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            }

            string sql = "SELECT CWHID, BYORGNO, CWHNAME+'村委会' as CWHNAME, ORDERBY"
                + sb.ToString()
                + " order by BYORGNO,ORDERBY";

            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }

        #endregion

        #region 获取村委会下属不重复自然村记录
        /// <summary>
        /// 获取记录
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getVillageDT(T_SYS_ORG_CWH_SW sw)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" Left Join T_SYS_ORG_LINK b   on convert(varchar,a.CWHID)=b.BYORGNO ");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.CWHID) == false)
                sb.AppendFormat(" AND a.CWHID = '{0}'", ClsSql.EncodeSql(sw.CWHID));
            if (string.IsNullOrEmpty(sw.ORGNO) == false)
                sb.AppendFormat(" AND a.BYORGNO = '{0}'", ClsSql.EncodeSql(sw.ORGNO));
            if (string.IsNullOrEmpty(sw.CWHNAME) == false)
                sb.AppendFormat(" AND (a.CWHNAME like '%{0}%')", ClsSql.EncodeSql(sw.CWHNAME));
            if (!string.IsNullOrEmpty(sw.BYORGNO))
            {
                if (sw.BYORGNO.Substring(4, 11) == "00000000000")//获取所有市的
                    sb.AppendFormat(" AND (SUBSTRING(a.BYORGNO,1,4) = '{0}' or a.BYORGNO is null or a.BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
                else if (sw.BYORGNO.Substring(6, 9) == "000000000")//获取所有县的
                    sb.AppendFormat(" AND (SUBSTRING(a.BYORGNO,1,6) = '{0}' or a.BYORGNO is null or a.BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
                else if (sw.BYORGNO.Substring(9, 6) == "000000")//获取所有乡镇的
                    sb.AppendFormat(" AND (SUBSTRING(a.BYORGNO,1,9) = '{0}' or a.BYORGNO is null or a.BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 9)));
                else
                    sb.AppendFormat(" AND a.BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            }
            if (!string.IsNullOrEmpty(sw.GetContyORGNOByCity))//市获取所有县
            {
                sb.AppendFormat(" AND (SUBSTRING(a.BYORGNO,1,4)='{0}' and SUBSTRING(a.BYORGNO,5,2)<>'00' and SUBSTRING(a.BYORGNO,7,9) = '000000000')", ClsSql.EncodeSql(sw.GetContyORGNOByCity.Substring(0, 4)));
            }
            if (!string.IsNullOrEmpty(sw.GetXZOrgNOByConty))
            {
                sb.AppendFormat(" AND (SUBSTRING(a.BYORGNO,1,6)='{0}' and SUBSTRING(a.BYORGNO,7,9) != '000000000')", ClsSql.EncodeSql(sw.GetXZOrgNOByConty.Substring(0, 6)));
            }
            if (string.IsNullOrEmpty(sw.OnlyGetShiXian) == false)
            {

                sb.AppendFormat(" AND (SUBSTRING(a.BYORGNO,1,4)='{0}' and SUBSTRING(a.BYORGNO,7,9) = '000000000')", ClsSql.EncodeSql(sw.TopORGNO.Substring(0, 4)));
            }
            if (string.IsNullOrEmpty(sw.ORGLINKTYPE) == false)
                sb.AppendFormat(" AND b.ORGLINKTYPE = '{0}'", ClsSql.EncodeSql(sw.ORGLINKTYPE));
            if (string.IsNullOrEmpty(sw.UNITNAME) == false)
                sb.AppendFormat(" AND b.UNITNAME = '{0}'", ClsSql.EncodeSql(sw.UNITNAME));
            string sql = "SELECT distinct(b.UNITNAME),a.CWHID, a.BYORGNO, a.CWHNAME+'村委会' as CWHNAME, a.ORDERBY ,b.ORGLINK_ID,b.ORGLINKTYPE,b.NAME,b.USERJOB,b.PHONE,b.BYORGNO as ORGNO from T_SYS_ORG_CWH a"
                + sb.ToString()
                + " order by a.BYORGNO,a.ORDERBY";

            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }

        #endregion

        #region 获取村委会下属自然村记录
        /// <summary>
        /// 获取记录
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getVillagecComDT(T_SYS_ORG_CWH_SW sw)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" Left Join T_SYS_ORG_LINK b   on convert(varchar, a.CWHID)=b.BYORGNO ");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.CWHID) == false)
                sb.AppendFormat(" AND a.CWHID = '{0}'", ClsSql.EncodeSql(sw.CWHID));
            if (string.IsNullOrEmpty(sw.ORGNO) == false)
                sb.AppendFormat(" AND a.BYORGNO = '{0}'", ClsSql.EncodeSql(sw.ORGNO));
            if (string.IsNullOrEmpty(sw.CWHNAME) == false)
                sb.AppendFormat(" AND (a.CWHNAME like '%{0}%')", ClsSql.EncodeSql(sw.CWHNAME));
            if (!string.IsNullOrEmpty(sw.BYORGNO))
            {
                if (sw.BYORGNO.Substring(4, 11) == "00000000000")//获取所有市的
                    sb.AppendFormat(" AND (SUBSTRING(a.BYORGNO,1,4) = '{0}' or a.BYORGNO is null or a.BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
                else if (sw.BYORGNO.Substring(6, 9) == "000000000")//获取所有县的
                    sb.AppendFormat(" AND (SUBSTRING(a.BYORGNO,1,6) = '{0}' or a.BYORGNO is null or a.BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
                else if (sw.BYORGNO.Substring(9, 6) == "000000")//获取所有乡镇的
                    sb.AppendFormat(" AND (SUBSTRING(a.BYORGNO,1,9) = '{0}' or a.BYORGNO is null or a.BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 9)));
                else
                    sb.AppendFormat(" AND a.BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            }
            if (!string.IsNullOrEmpty(sw.GetContyORGNOByCity))//市获取所有县
            {
                sb.AppendFormat(" AND (SUBSTRING(a.BYORGNO,1,4)='{0}' and SUBSTRING(a.BYORGNO,5,2)<>'00' and SUBSTRING(a.BYORGNO,7,9) = '000000000')", ClsSql.EncodeSql(sw.GetContyORGNOByCity.Substring(0, 4)));
            }
            if (!string.IsNullOrEmpty(sw.GetXZOrgNOByConty))
            {
                sb.AppendFormat(" AND (SUBSTRING(a.BYORGNO,1,6)='{0}' and SUBSTRING(a.BYORGNO,7,9) != '000000000')", ClsSql.EncodeSql(sw.GetXZOrgNOByConty.Substring(0, 6)));
            }
            if (string.IsNullOrEmpty(sw.OnlyGetShiXian) == false)
            {

                sb.AppendFormat(" AND (SUBSTRING(a.BYORGNO,1,4)='{0}' and SUBSTRING(a.BYORGNO,7,9) = '000000000')", ClsSql.EncodeSql(sw.TopORGNO.Substring(0, 4)));
            }
            if (string.IsNullOrEmpty(sw.ORGLINKTYPE) == false)
                sb.AppendFormat(" AND b.ORGLINKTYPE = '{0}'", ClsSql.EncodeSql(sw.ORGLINKTYPE));
            if (string.IsNullOrEmpty(sw.UNITNAME) == false)
                sb.AppendFormat(" AND b.UNITNAME = '{0}'", ClsSql.EncodeSql(sw.UNITNAME));
            string sql = "SELECT b.UNITNAME,a.CWHID, a.BYORGNO, a.CWHNAME+'村委会' as CWHNAME, a.ORDERBY ,b.ORGLINK_ID,b.ORGLINKTYPE,b.NAME,b.USERJOB,b.PHONE,b.BYORGNO as ORGNO from T_SYS_ORG_CWH a"
                + sb.ToString()
                + " order by a.BYORGNO,a.ORDERBY";

            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }

        #endregion
    }
}
