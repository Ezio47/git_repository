using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel;
using DataBaseClassLibrary;
using PublicClassLibrary;

namespace ManagerSystemClassLibrary.BaseDT
{

    /// <summary>
    /// 监测_群众报警表
    /// </summary>
    public class JC_PERALARM
    {
        #region 增加

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(JC_PERALARM_Model m)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  JC_PERALARM(FIRENAME,BYORGNO,PERALARMPHONE, PERALARMNAME, PERALARMTIME, PERALARMADDRESS, PERALARMCONTENT ,MANSTATE , MANRESULT, MANTIME, MANUSERID,JD, WD, PEARLARMPRE)");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.FIRENAME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.BYORGNO));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.PERALARMPHONE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.PERALARMNAME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.PERALARMTIME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.PERALARMADDRESS));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.PERALARMCONTENT));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.MANSTATE));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.MANRESULT));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.MANTIME));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.MANUSERID));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.JD));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.WD));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.PEARLARMPRE));
            //sb.AppendFormat(",{0}", ClsSql.saveNullField(m.PEARLARMISSUED));
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
        public static Message Mdy(JC_PERALARM_Model m)
        {
            //PERALARMID, PERALARMPHONE, PERALARMNAME, PERALARMTIME, PERALARMADDRESS, PERALARMCONTENT, MANSTATE, MANRESULT, MANTIME, MANUSERID
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE JC_PERALARM");
            sb.AppendFormat(" set ");
            sb.AppendFormat("FIRENAME='{0}'", ClsSql.EncodeSql(m.FIRENAME));
            sb.AppendFormat(",BYORGNO='{0}'", ClsSql.EncodeSql(m.BYORGNO));
            sb.AppendFormat(",PERALARMPHONE='{0}'", ClsSql.EncodeSql(m.PERALARMPHONE));
            sb.AppendFormat(",PERALARMNAME='{0}'", ClsSql.EncodeSql(m.PERALARMNAME));
            sb.AppendFormat(",PERALARMTIME='{0}'", ClsSql.EncodeSql(m.PERALARMTIME));
            sb.AppendFormat(",PERALARMADDRESS='{0}'", ClsSql.EncodeSql(m.PERALARMADDRESS));
            sb.AppendFormat(",PERALARMCONTENT='{0}'", ClsSql.EncodeSql(m.PERALARMCONTENT));
            if (string.IsNullOrEmpty(m.MANSTATE) == false)
            sb.AppendFormat(",MANSTATE='{0}'", ClsSql.EncodeSql(m.MANSTATE));
            sb.AppendFormat(",MANRESULT='{0}'", ClsSql.EncodeSql(m.MANRESULT));
            if (string.IsNullOrEmpty(m.MANTIME) == false)
                sb.AppendFormat(",MANTIME='{0}'", ClsSql.EncodeSql(m.MANTIME));
            if (string.IsNullOrEmpty(m.MANUSERID) == false)
            sb.AppendFormat(",MANUSERID='{0}'", ClsSql.EncodeSql(m.MANUSERID));
            //JD, WD, PEARLARMPRE, PEARLARMISSUED
            if(string.IsNullOrEmpty(m.JD)==false)
                sb.AppendFormat(",JD='{0}'", ClsSql.EncodeSql(m.JD));
            if (string.IsNullOrEmpty(m.WD) == false)
            sb.AppendFormat(",WD='{0}'", ClsSql.EncodeSql(m.WD));
            sb.AppendFormat(",PEARLARMPRE='{0}'", ClsSql.EncodeSql(m.PEARLARMPRE));
            sb.AppendFormat(",PEARLARMISSUED='{0}'", ClsSql.EncodeSql(m.PEARLARMISSUED));
            sb.AppendFormat(" where PERALARMID= '{0}'", ClsSql.EncodeSql(m.PERALARMID));

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
        public static Message Del(JC_PERALARM_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete JC_PERALARM");
            sb.AppendFormat(" where PERALARMID= '{0}'", ClsSql.EncodeSql(m.PERALARMID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功！", "");
            else
                return new Message(false, "删除失败，请检查各输入框是否正确！", "");
        }

        #endregion

        #region 管理
        /// <summary>
        /// 管理
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Man(JC_PERALARM_Model m)
        {
            if (string.IsNullOrEmpty(m.MANTIME))
                m.MANTIME = ClsSwitch.SwitTM(DateTime.Now);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE JC_PERALARM");
            sb.AppendFormat(" set "); 
            sb.AppendFormat("MANSTATE='{0}'", ClsSql.EncodeSql(m.MANSTATE));
            sb.AppendFormat(",MANRESULT='{0}'", ClsSql.EncodeSql(m.MANRESULT));
            sb.AppendFormat(",MANTIME='{0}'", ClsSql.EncodeSql(m.MANTIME));
            sb.AppendFormat(",MANUSERID='{0}'", ClsSql.EncodeSql(m.MANUSERID));
            sb.AppendFormat(" where PERALARMID= '{0}'", ClsSql.EncodeSql(m.PERALARMID));

            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功！", "");
            else
                return new Message(false, "修改失败，请检查各输入框是否正确！", "");
        }

        #endregion

        #region 获取数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getDT(JC_PERALARM_SW sw)
        {
            StringBuilder sb = new StringBuilder();


            //PERALARMID, PERALARMPHONE, PERALARMNAME, PERALARMTIME, PERALARMADDRESS, PERALARMCONTENT, MANSTATE, MANRESULT, MANTIME, MANUSERID
            sb.AppendFormat(" FROM      JC_PERALARM a");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.PERALARMID) == false)
                sb.AppendFormat(" AND PERALARMID = '{0}'", ClsSql.EncodeSql(sw.PERALARMID));
            if (string.IsNullOrEmpty(sw.PERALARMPHONE) == false)
                sb.AppendFormat(" AND PERALARMPHONE = '{0}'", ClsSql.EncodeSql(sw.PERALARMPHONE));
            if (string.IsNullOrEmpty(sw.PERALARMNAME) == false)
                sb.AppendFormat(" AND PERALARMNAME = '{0}'", ClsSql.EncodeSql(sw.PERALARMNAME));
            if (string.IsNullOrEmpty(sw.PERALARMTIME) == false)
                sb.AppendFormat(" AND PERALARMTIME = '{0}'", ClsSql.EncodeSql(sw.PERALARMTIME));
            if (!string.IsNullOrEmpty(sw.DateBegin))
            {
                sb.AppendFormat(" AND PERALARMTIME>='{0} 00:00:00'", sw.DateBegin);
            }
            if (!string.IsNullOrEmpty(sw.DateEnd))
            {
                sb.AppendFormat(" AND PERALARMTIME<='{0} 23:59:59'", sw.DateEnd);
            }
            if (string.IsNullOrEmpty(sw.MANSTATE) == false)
                sb.AppendFormat(" AND MANSTATE = '{0}'", ClsSql.EncodeSql(sw.MANSTATE));

            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
            {
                sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));

                //if (PublicCls.OrgIsShi(sw.BYORGNO))
                //{
                //    sb.AppendFormat(" and BYORGNO like '{0}%'", PublicCls.getShiIncOrgNo(sw.BYORGNO));
                //}
                //else if (PublicCls.OrgIsXian(sw.BYORGNO))
                //{
                //    sb.AppendFormat(" and BYORGNO like  '{0}%'", PublicCls.getXianIncOrgNo(sw.BYORGNO));
                //}
                //else
                //{
                //    sb.AppendFormat(" and BYORGNO = '{0}'", PublicCls.getZhenIncOrgNo(sw.BYORGNO));
                //}

            }
            string sql = "SELECT    PERALARMID,BYORGNO,FIRENAME, PERALARMPHONE, PERALARMNAME, PERALARMTIME, PERALARMADDRESS, PERALARMCONTENT, MANSTATE, MANRESULT, MANTIME, MANUSERID,JD, WD, PEARLARMPRE, PEARLARMISSUED"
                + sb.ToString()
                + " order by PERALARMTIME DESC";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }

        #endregion


        #region 获取分页数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getDT(JC_PERALARM_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat(" FROM      JC_PERALARM a");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.PERALARMID) == false)
                sb.AppendFormat(" AND PERALARMID = '{0}'", ClsSql.EncodeSql(sw.PERALARMID));
            if (string.IsNullOrEmpty(sw.PERALARMPHONE) == false)
                sb.AppendFormat(" AND PERALARMPHONE like '%{0}%'", ClsSql.EncodeSql(sw.PERALARMPHONE));
            if (string.IsNullOrEmpty(sw.PERALARMNAME) == false)
                sb.AppendFormat(" AND PERALARMNAME like '%{0}%'", ClsSql.EncodeSql(sw.PERALARMNAME));
            if (!string.IsNullOrEmpty(sw.DateBegin))
            {
                sb.AppendFormat(" AND PERALARMTIME>='{0} 00:00:00'", sw.DateBegin);
            }
            if (!string.IsNullOrEmpty(sw.DateEnd))
            {
                sb.AppendFormat(" AND PERALARMTIME<='{0} 23:59:59'", sw.DateEnd);
            }
            if (string.IsNullOrEmpty(sw.MANSTATE) == false)
                sb.AppendFormat(" AND MANSTATE = '{0}'", ClsSql.EncodeSql(sw.MANSTATE));

            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
            {

                if (PublicCls.OrgIsShi(sw.BYORGNO))
                {
                    sb.AppendFormat(" and BYORGNO like '{0}%'", PublicCls.getShiIncOrgNo(sw.BYORGNO));
                }
                else if (PublicCls.OrgIsXian(sw.BYORGNO))
                {
                    sb.AppendFormat(" and BYORGNO like  '{0}%'", PublicCls.getXianIncOrgNo(sw.BYORGNO));
                }
                else
                {
                    sb.AppendFormat(" and BYORGNO = '{0}'", PublicCls.getZhenIncOrgNo(sw.BYORGNO));
                }

            }
            string sql = "SELECT    PERALARMID,BYORGNO, FIRENAME,PERALARMPHONE, PERALARMNAME, PERALARMTIME, PERALARMADDRESS, PERALARMCONTENT, MANSTATE, MANRESULT, MANTIME, MANUSERID,JD, WD, PEARLARMPRE, PEARLARMISSUED"
                + sb.ToString()
                + " order by PERALARMTIME DESC";
            string sqlC = "select count(1) " + sb.ToString();
            total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
            sw.curPage = PagerCls.getCurPage(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
            DataSet ds = DataBaseClass.FullDataSet(sql, (sw.curPage - 1) * sw.pageSize, sw.pageSize, "a");
            return ds.Tables[0];
        }

        #endregion
    }
}
