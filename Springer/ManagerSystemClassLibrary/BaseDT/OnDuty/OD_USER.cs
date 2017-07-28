using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System;
using System.Threading.Tasks;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel;
using DataBaseClassLibrary;
using PublicClassLibrary;

namespace ManagerSystemClassLibrary.BaseDT
{
    /// <summary>
    /// OD_USER
    /// </summary>
   public class OD_USER
   {
       #region 根据查询条件获取DataTable DataTable getDT(OD_USER_SW sw)
       /// <summary>
        /// 根据查询条件获取DataTable
        /// </summary>
        /// <param name="sw">参见OD_USER_SW</param>
        /// <returns>DataTable</returns>
        public static DataTable getDT(OD_USER_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT    ");
            sb.AppendFormat(" OD_TYPEID,ODUID, ONDUTYDATE, BYORGNO, ONDUTYUSERTYPE, ONDUTYUSERID, ISATTENDED,ATTENDEDTIME");
            sb.AppendFormat(" FROM      OD_USER");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
                sb.AppendFormat(" AND BYORGNO='{0}'", sw.BYORGNO);
            if (string.IsNullOrEmpty(sw.ONDUTYUSERID) == false)
                sb.AppendFormat(" AND ONDUTYUSERID='{0}'", sw.ONDUTYUSERID);
            if (string.IsNullOrEmpty(sw.OD_TYPEID) == false)
                sb.AppendFormat(" AND OD_TYPEID='{0}'", sw.OD_TYPEID);
            if (string.IsNullOrEmpty(sw.DTBegin) == false)
                sb.AppendFormat(" AND ONDUTYDATE>='{0}'", sw.DTBegin);
            if (string.IsNullOrEmpty(sw.DTEnd) == false)
                sb.AppendFormat(" AND ONDUTYDATE<='{0}'", sw.DTEnd);
            if (string.IsNullOrEmpty(sw.OD_TYPEID) == false)
                sb.AppendFormat(" AND ONDUTYDATE in(select ONDUTYDATE from OD_DATE where  OD_TYPEID={0})", sw.OD_TYPEID);
            if (string.IsNullOrEmpty(sw.ONDUTYDATE) == false)
                sb.AppendFormat(" AND ONDUTYDATE='{0}'", sw.ONDUTYDATE);
            if (string.IsNullOrEmpty(sw.ISATTENDED) == false)
                sb.AppendFormat(" AND ISATTENDED='{0}'", sw.ISATTENDED);
            if (string.IsNullOrEmpty(sw.ONDUTYUSERTYPE) == false)
                sb.AppendFormat(" AND ONDUTYUSERTYPE='{0}'", sw.ONDUTYUSERTYPE);
            sb.AppendFormat(" order by ONDUTYDATE");
            return DataBaseClass.FullDataSet(sb.ToString()).Tables[0];

        }

       #endregion

        #region 获取周末人员值班信息 DataTable getWeekDT(OD_USER_SW sw)
        /// <summary>
        /// 获取周末人员值班信息
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getWeekDT(OD_USER_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT    ");
            sb.AppendFormat(" OD_TYPEID,ODUID, ONDUTYDATE, BYORGNO, ONDUTYUSERTYPE, ONDUTYUSERID, ISATTENDED,ATTENDEDTIME");
            sb.AppendFormat(" FROM      OD_USER");
            sb.AppendFormat(" WHERE   1=1");
            sb.AppendFormat(" AND ONDUTYDATE in(SELECT     ONDUTYDATE FROM      OD_DATE where WEEK in('星期六','星期日'))");
            if (string.IsNullOrEmpty(sw.DTBegin) == false)
                sb.AppendFormat(" AND ONDUTYDATE>='{0}'", sw.DTBegin);
            if (string.IsNullOrEmpty(sw.DTEnd) == false)
                sb.AppendFormat(" AND ONDUTYDATE<='{0}'", sw.DTEnd);
            if (string.IsNullOrEmpty(sw.curOrgNo) == false)
            {

                if (PublicCls.OrgIsShi(sw.curOrgNo))
                {
                    sb.AppendFormat(" and BYORGNO like '{0}%'", PublicCls.getShiIncOrgNo(sw.curOrgNo));
                }
                else if (PublicCls.OrgIsXian(sw.curOrgNo))
                {
                    sb.AppendFormat(" and BYORGNO like  '{0}%'", PublicCls.getXianIncOrgNo(sw.curOrgNo));
                }
                else
                {
                    sb.AppendFormat(" and BYORGNO = '{0}'", PublicCls.getZhenIncOrgNo(sw.curOrgNo));
                }

            }

            sb.AppendFormat(" order by ONDUTYDATE");
            return DataBaseClass.FullDataSet(sb.ToString()).Tables[0];

        }

        #endregion

        #region 排班表向下复制功能 Message Copy(string date, string byorgon)
        /// <summary>
        /// 排班表向下复制功能
        /// </summary>
        /// <param name="m">值班日期</param>
        /// <returns></returns>
        public static Message Copy(OD_USER_Model m)
        {
            DateTime dt = Convert.ToDateTime(m.ONDUTYDATE);
            StringBuilder sbDel = new StringBuilder();
            sbDel.AppendFormat("delete OD_USER where ONDUTYDATE='{0}'",dt.AddDays(1).ToString("yyyy-MM-dd"));
            sbDel.AppendFormat(" and BYORGNO='{0}'", m.BYORGNO);
            //先删除要复制的这天的记录
            DataBaseClass.ExeSql(sbDel.ToString());
            sbDel.Clear();

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("insert OD_USER(OD_TYPEID,ONDUTYDATE,BYORGNO,ONDUTYUSERTYPE,ONDUTYUSERID,ISATTENDED)");
            sb.AppendFormat("select OD_TYPEID,'{0}',BYORGNO,ONDUTYUSERTYPE,ONDUTYUSERID,'0'as ISATTENDED from OD_USER", dt.AddDays(1).ToString("yyyy-MM-dd"));
            sb.AppendFormat("  where ONDUTYDATE='{0}'", ClsSql.EncodeSql(m.ONDUTYDATE));
            sb.AppendFormat("  and BYORGNO='{0}'", ClsSql.EncodeSql(m.BYORGNO));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "复制成功！", "");
            else
                return new Message(false, "复制失败！", "");

        }
        #endregion

        #region 签到 Message Sign(OD_USER_Model o)
        /// <summary>
        /// 签到
        /// </summary>
        /// <param name="m">m</param>
        /// <returns></returns>
        public static Message Sign(OD_USER_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("update OD_USER set");
            sb.AppendFormat("  ISATTENDED='{0}'", ClsSql.EncodeSql("1"));
            sb.AppendFormat("  ,ATTENDEDTIME='{0}'", PublicClassLibrary.ClsSwitch.SwitTM(DateTime.Now));
            sb.AppendFormat(" where 1=1");
            sb.AppendFormat(" AND ONDUTYDATE='{0}'", ClsSql.EncodeSql(m.ONDUTYDATE));
            sb.AppendFormat(" AND BYORGNO='{0}'", ClsSql.EncodeSql(m.BYORGNO));
            sb.AppendFormat(" AND ONDUTYUSERID='{0}'", ClsSql.EncodeSql(m.ONDUTYUSERID));
            sb.AppendFormat(" AND ONDUTYUSERTYPE='{0}'", ClsSql.EncodeSql(m.ONDUTYUSERTYPE));
            bool b = DataBaseClass.ExeSql(sb.ToString());
            if (b)
            {
                return new Message(true, "签到成功", "");
            }
            else
            {
                return new Message(false, "签到失败", "");
            }

        }

        #endregion

        #region 添加排班 Message Add(OD_USER_Model m)

        /// <summary>
        /// 排班表添加方法
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message Add(OD_USER_Model m)
        {
            //先删除
            if(1==1)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("delete from  OD_USER");//(OD_TYPEID, ONDUTYDATE, BYORGNO, ONDUTYUSERTYPE, ONDUTYUSERID, ISATTENDED)");
                sb.AppendFormat(" where BYORGNO= '{0}'", m.BYORGNO);
                sb.AppendFormat(" and OD_TYPEID= '{0}'", m.OD_TYPEID);
                sb.AppendFormat(" and ONDUTYUSERTYPE= '{0}'", m.ONDUTYUSERTYPE);
                sb.AppendFormat(" and  ONDUTYDATE>='{0}'", m.dateBegin);
                sb.AppendFormat(" and  ONDUTYDATE<='{0}'", m.dateEnd);
                bool bln = DataBaseClass.ExeSql(sb.ToString());
            }
            DateTime dtbeg = Convert.ToDateTime(m.dateBegin);
            DateTime dtend = Convert.ToDateTime(m.dateEnd);
            TimeSpan ts = dtend.Subtract(dtbeg);//判断下相差多少天
            if (string.IsNullOrEmpty(m.ONDUTYUSERID)==false)//插入
            {

                string[] arr = m.ONDUTYUSERID.Split(',');
                for (int i = 0; i < arr.Length; i++)//循环操作
                {
                    if (arr[i].Length > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("INSERT INTO  OD_USER(OD_TYPEID, ONDUTYDATE, BYORGNO, ONDUTYUSERTYPE, ONDUTYUSERID, ISATTENDED)");
                        sb.AppendFormat(" select '{0}'", m.OD_TYPEID);
                        sb.AppendFormat(",ONDUTYDATE");
                        sb.AppendFormat(", '{0}'", m.BYORGNO);
                        sb.AppendFormat(", '{0}'", m.ONDUTYUSERTYPE);
                        sb.AppendFormat(", '{0}'", arr[i]);
                        sb.AppendFormat(", '0'");
                        sb.AppendFormat(" from OD_DATE");
                        sb.AppendFormat(" where  OD_TYPEID='{0}'", m.OD_TYPEID);
                        sb.AppendFormat(" and  ONDUTYDATE>='{0}'", m.dateBegin);
                        sb.AppendFormat(" and  ONDUTYDATE<='{0}'", m.dateEnd);
                        //sb.AppendFormat("(");
                        //sb.AppendFormat("'{0}'", m.OD_TYPEID);
                        //sb.AppendFormat(",'{0}'", m.OD_TYPEID);
                        //sb.AppendFormat("'{0}'", m.OD_TYPEID);
                        //sb.AppendFormat("'{0}'", m.OD_TYPEID);

                        //sb.AppendFormat(")");
                        bool bln = DataBaseClass.ExeSql(sb.ToString());
                    }
                }
            }
            //if (bln == true)
                return new Message(true, "操作成功！", ts.Days.ToString());
            //else
            //    return new Message(false, "添加失败，请检查各输入框是否正确！", "");
            //StringBuilder sb = new StringBuilder();
            //sb.AppendFormat("INSERT INTO  OD_USER(ONDUTYDATE, BYORGNO,ONDUTYUSERTYPE,ONDUTYUSERID,ISATTENDED,ATTENDEDTIME )");
            //sb.AppendFormat("VALUES(");
            //sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.ONDUTYDATE));
            //sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.BYORGNO));
            //sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ONDUTYUSERTYPE));
            //sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ONDUTYUSERID));
            //if (string.IsNullOrEmpty(m.ISATTENDED) == true)//是否签到，默认为0 未签到
            //    m.ISATTENDED = "0";
            //sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ISATTENDED));
            //if (string.IsNullOrEmpty(m.ATTENDEDTIME) == true)
            //    sb.AppendFormat(",null");
            //else
            //    sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ATTENDEDTIME));
            //sb.AppendFormat(")");
            //bool bln = DataBaseClass.ExeSql(sb.ToString());
            //if (bln == true)
            //    return new Message(true, "添加成功！", "");
            //else
            //    return new Message(false, "添加失败，请检查各输入框是否正确！", "");
        }
       #endregion

        #region 统计ＤＴ中用户的个数
        /// <summary>
       /// 统计ＤＴ中用户的个数
       /// </summary>
       /// <param name="dt"></param>
       /// <param name="uid"></param>
       /// <returns></returns>
        public static string getCountByDT(DataTable dt, string uid)
        {

            return  dt.Compute("count(ONDUTYUSERID)", "ONDUTYUSERID='" + uid + "'").ToString();
        }

        #endregion


        #region 统计值班次数
        /// <summary>
        /// 值班次数
        /// </summary>
        /// <param name="sw">sw</param>
        /// <returns></returns>
        public static DataTable GetDutyCoutDT(OD_USER_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select USERNAME,sum( CASE WHEN ONDUTYUSERTYPE = '1' THEN 1 ELSE 0 END) AS 'zaob',");
            sb.AppendFormat("sum( CASE WHEN ONDUTYUSERTYPE = '2' THEN 1 ELSE 0 END) AS 'zhongb',");
            sb.AppendFormat("sum( CASE WHEN ONDUTYUSERTYPE = '3' THEN 1 ELSE 0 END) AS 'wanb',");
            sb.AppendFormat("sum(CASE WHEN ONDUTYUSERTYPE = '-1' THEN 1 ELSE 0 END) AS 'daib'");
            sb.AppendFormat(" FROM dbo.OD_USER left join dbo.T_SYSSEC_USER on  ONDUTYUSERID=USERID  where 1=1 ");
            sb.AppendFormat("and ONDUTYDATE>='{0}'", ClsSql.EncodeSql(sw.DTBegin));
            sb.AppendFormat("and ONDUTYDATE<='{0}'", ClsSql.EncodeSql(sw.DTEnd));
            sb.AppendFormat(" and  BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            sb.AppendFormat("  GROUP BY USERNAME order by daib desc ");
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        #endregion


        #region 获取值班人的userID
        /// <summary>
        /// 获取值班人的userID
        /// </summary>
        /// <param name="ondutyTime">日期</param>
        /// <param name="byorgon">组织机构编码（多个以，号分开）</param>
        /// <returns></returns>
        public static DataTable GetOndutyUserid(string ondutyTime, string byorgon)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select ou.ONDUTYUSERID from OD_USER as ou where 1=1");
            if (!string.IsNullOrEmpty(ondutyTime))
                sb.AppendFormat("  and ou.ONDUTYDATE='{0}'", ClsSwitch.SwitDate(ondutyTime));
            if (!string.IsNullOrEmpty(byorgon))
                sb.AppendFormat("  and ou.BYORGNO in({0})", ClsSql.EncodeSql(byorgon));
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        #endregion
   }
}
