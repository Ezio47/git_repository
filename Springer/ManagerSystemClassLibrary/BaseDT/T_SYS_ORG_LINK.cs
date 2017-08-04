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
    /// 公用_机构表_联系人
    /// </summary>
    public class T_SYS_ORG_LINK
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(T_SYS_ORG_LINK_Model m)
        {
            if (isExists(new T_SYS_ORG_LINK_SW { PHONE = m.PHONE, BYORGNO = m.BYORGNO, ORGLINKTYPE = m.ORGLINKTYPE, ORDERBY = m.ORDERBY }) == true)
            {
                return new Message(false, "添加失败，已重复！", "");
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  T_SYS_ORG_LINK( BYORGNO, ORGLINKTYPE,UNITNAME, NAME, USERJOB, PHONE, Tell, ORDERBY)");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.BYORGNO));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ORGLINKTYPE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.UNITNAME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.NAME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.USERJOB));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.PHONE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.Tell));
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
        public static Message Mdy(T_SYS_ORG_LINK_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE T_SYS_ORG_LINK");
            sb.AppendFormat(" set ");
            sb.AppendFormat("BYORGNO='{0}'", ClsSql.EncodeSql(m.BYORGNO));
            sb.AppendFormat(",ORGLINKTYPE='{0}'", ClsSql.EncodeSql(m.ORGLINKTYPE));
            sb.AppendFormat(",UNITNAME='{0}'", ClsSql.EncodeSql(m.UNITNAME));
            sb.AppendFormat(",NAME='{0}'", ClsSql.EncodeSql(m.NAME));
            sb.AppendFormat(",USERJOB='{0}'", ClsSql.EncodeSql(m.USERJOB));
            sb.AppendFormat(",PHONE='{0}'", ClsSql.EncodeSql(m.PHONE));
            sb.AppendFormat(",Tell='{0}'", ClsSql.EncodeSql(m.Tell));
            sb.AppendFormat(",ORDERBY='{0}'", ClsSql.EncodeSql(m.ORDERBY));
            sb.AppendFormat(" where ORGLINK_ID= '{0}'", ClsSql.EncodeSql(m.ORGLINK_ID));
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
        public static Message Del(T_SYS_ORG_LINK_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete T_SYS_ORG_LINK");
            sb.AppendFormat(" where ORGLINK_ID= '{0}'", ClsSql.EncodeSql(m.ORGLINK_ID));
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
        public static bool isExists(T_SYS_ORG_LINK_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from T_SYS_ORG_LINK where 1=1");
            if (string.IsNullOrEmpty(sw.ORGLINK_ID) == false)
                sb.AppendFormat(" AND ORGLINK_ID= '{0}'", ClsSql.EncodeSql(sw.ORGLINK_ID));
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
                sb.AppendFormat(" AND BYORGNO= '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            if (string.IsNullOrEmpty(sw.PHONE) == false)
                sb.AppendFormat(" AND PHONE= '{0}'", ClsSql.EncodeSql(sw.PHONE));
            if (string.IsNullOrEmpty(sw.ORDERBY) == false)
                sb.AppendFormat(" AND ORDERBY= '{0}'", ClsSql.EncodeSql(sw.ORDERBY));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }

        #endregion

        #region 获取记录
        /// <summary>
        /// 获取记录
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(T_SYS_ORG_LINK_SW sw)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat(" FROM      T_SYS_ORG_LINK");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.ORGLINK_ID) == false)
                sb.AppendFormat(" AND ORGLINK_ID = '{0}'", ClsSql.EncodeSql(sw.ORGLINK_ID));
            if (string.IsNullOrEmpty(sw.ORGNO) == false)
                sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.ORGNO));
            if (string.IsNullOrEmpty(sw.ORGLINKTYPE) == false)
                sb.AppendFormat(" AND ORGLINKTYPE = '{0}'", ClsSql.EncodeSql(sw.ORGLINKTYPE));
            if (string.IsNullOrEmpty(sw.PHONE) == false)
            {
                if (sw.PHONE.Split(',').Length > 1)
                    sb.AppendFormat(" AND PHONE in({0})", ClsSql.SwitchStrToSqlIn(sw.PHONE));
                else
                    sb.AppendFormat(" AND PHONE ='{0}'", ClsSql.EncodeSql(sw.PHONE));
            }
            if (string.IsNullOrEmpty(sw.keys) == false)
                sb.AppendFormat(" AND (NAME like '%{0}%' or USERJOB like '%{0}%' or PHONE like '%{0}%' or Tell like '%{0}%' or UNITNAME like '%{0}%')", ClsSql.EncodeSql(sw.keys));
            if (!string.IsNullOrEmpty(sw.BYORGNO))
            {
                if (sw.BYORGNO.Substring(4, 11) == "00000000000")//获取所有市的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,4) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
                else if (sw.BYORGNO.Substring(6, 9) == "000000000")//获取所有县的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,6) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
                else if (sw.BYORGNO.Substring(9,6) == "000000")//获取所有乡镇的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,9) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 9)));
                else
                    sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            }
            string sql = "SELECT ORGLINK_ID, BYORGNO, ORGLINKTYPE,UNITNAME, NAME, USERJOB, PHONE, Tell, ORDERBY"
                + sb.ToString()
                + " order by BYORGNO,ORGLINKTYPE,ORDERBY";

            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }

        #endregion

        #region 获取分页列表
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static DataTable getDT(T_SYS_ORG_LINK_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendFormat(" FROM      T_SYS_ORG_LINK");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.ORGLINK_ID) == false)
                sb.AppendFormat(" AND ORGLINK_ID = '{0}'", ClsSql.EncodeSql(sw.ORGLINK_ID));
            if (string.IsNullOrEmpty(sw.ORGNO) == false)
                sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.ORGNO));
            if (string.IsNullOrEmpty(sw.ORGLINKTYPE) == false)
                sb.AppendFormat(" AND ORGLINKTYPE = '{0}'", ClsSql.EncodeSql(sw.ORGLINKTYPE));
            if (string.IsNullOrEmpty(sw.keys) == false)
                sb.AppendFormat(" AND (NAME like '%{0}%' or USERJOB like '%{0}%' or PHONE like '%{0}%' or Tell like '%{0}%')", ClsSql.EncodeSql(sw.keys));
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
            string sql = "SELECT ORGLINK_ID, BYORGNO, ORGLINKTYPE, UNITNAME,NAME, USERJOB, PHONE, Tell, ORDERBY"
                + sb.ToString()
                + " order by BYORGNO,ORGLINKTYPE,ORDERBY";

            string sqlC = "select count(1) " + sb.ToString();
            total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
            sw.curPage = PagerCls.getCurPage(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
            DataSet ds = DataBaseClass.FullDataSet(sql, (sw.curPage - 1) * sw.pageSize, sw.pageSize, "a");
            return ds.Tables[0];
        }

        #endregion
    }
}
