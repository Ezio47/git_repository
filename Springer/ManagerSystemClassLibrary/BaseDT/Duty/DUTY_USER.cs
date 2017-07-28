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

namespace ManagerSystemClassLibrary.BaseDT.Duty
{
    /// <summary>
    /// 值班人员表
    /// </summary>
    public class DUTY_USER
    {
        #region 根据查询条件获取DataTable DataTable getDT(DUTY_USER_SW sw)
        /// <summary>
        /// 根据查询条件获取DataTable
        /// </summary>
        /// <param name="sw">参见DUTY_USER_SW</param>
        /// <returns>DataTable</returns>
        public static DataTable getDT(DUTY_USER_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT    ");
            sb.AppendFormat(" DUID, DUTYDATE, BYORGNO, DUTYUSERTYPE, DUTYUSERID, ISATTENDED,ATTENDEDTIME");
            sb.AppendFormat(" FROM      DUTY_USER");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
                sb.AppendFormat(" AND BYORGNO='{0}'", sw.BYORGNO);
            if (string.IsNullOrEmpty(sw.DUTYUSERID) == false)
                sb.AppendFormat(" AND DUTYUSERID='{0}'", sw.DUTYUSERID);
            if (string.IsNullOrEmpty(sw.DTBegin) == false)
                sb.AppendFormat(" AND DUTYDATE>='{0}'", sw.DTBegin);
            if (string.IsNullOrEmpty(sw.DTEnd) == false)
                sb.AppendFormat(" AND DUTYDATE<='{0}'", sw.DTEnd);
            if (string.IsNullOrEmpty(sw.DUTYDATE) == false)
                sb.AppendFormat(" AND DUTYDATE='{0}'", sw.DUTYDATE);
            if (string.IsNullOrEmpty(sw.ISATTENDED) == false)
                sb.AppendFormat(" AND ISATTENDED='{0}'", sw.ISATTENDED);
            if (string.IsNullOrEmpty(sw.DUTYUSERTYPE) == false)
                sb.AppendFormat(" AND DUTYUSERTYPE='{0}'", sw.DUTYUSERTYPE);
            sb.AppendFormat(" order by DUTYDATE");
            return DataBaseClass.FullDataSet(sb.ToString()).Tables[0];

        }
        #endregion

        #region 添加排班 

        /// <summary>
        /// 排班表添加方法
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message Add(DUTY_USER_Model m)
        {
            //先删除
            if (1 == 1)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("delete from  DUTY_USER");//(DUTYDATE, BYORGNO, DUTYUSERTYPE, DUTYUSERID, ISATTENDED)");
                sb.AppendFormat(" where BYORGNO= '{0}'", m.BYORGNO);
                sb.AppendFormat(" and DUTYDATE= '{0}'", m.DUTYDATE);
                bool bln = DataBaseClass.ExeSql(sb.ToString());
            }
            //DateTime dtbeg = Convert.ToDateTime(m.dateBegin);
            //DateTime dtend = Convert.ToDateTime(m.dateEnd);
            //TimeSpan ts = dtend.Subtract(dtbeg);//判断下相差多少天
            List<string> sqllist = new List<string>();
            if (string.IsNullOrEmpty(m.DUTYUSERID) == false)//插入
            {
                StringBuilder sb = new StringBuilder();
                string insertstr = "";
                string[] arr = m.DUTYUSERID.Split('#');
                for (int i = 0; i < arr.Length; i++)//循环操作早班、中班、晚班、带班、总带班
                {
                    var USERID = arr[i].Split(',');
                    if (USERID.Length>0)
                    {
                        sb.AppendFormat("INSERT INTO  DUTY_USER(DUTYDATE, BYORGNO, DUTYUSERTYPE, DUTYUSERID, ISATTENDED)");
                        for (int j = 0; j < USERID.Length; j++)
                        {
                            switch (i)
                            {
                                case 0:
                                    m.DUTYUSERTYPE = "1";
                                    break;
                                case 1:
                                    m.DUTYUSERTYPE = "2";
                                    break;
                                case 2:
                                    m.DUTYUSERTYPE = "3";
                                    break;
                                case 3:
                                    m.DUTYUSERTYPE = "-1";
                                    break;
                                default:
                                    m.DUTYUSERTYPE = "-2";
                                    break;
                            }
                            sb.AppendFormat(" select '{0}'", ClsSql.EncodeSql(m.DUTYDATE));
                            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.BYORGNO));
                            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.DUTYUSERTYPE));
                            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(USERID[j]));
                            sb.AppendFormat(", '0'");
                            sb.AppendFormat(" UNION ALL ");
                        }
                        insertstr += sb.ToString();

                        string str = insertstr.Substring(0, insertstr.Length - 10);
                        insertstr = "";
                        sb.Remove(0, sb.Length);
                        sqllist.Add(str);
                    }               
                }
            }
            var z = DataBaseClass.ExecuteSqlTran(sqllist);
            if (z > 0)
            {
                return new Message(true, "保存成功！", "");
            }
            else
            {
                return new Message(false, "保存失败，事物回滚！", "");
            }
        }
        #endregion

        #region 批量生成添加排班

        /// <summary>
        /// 排班表添加方法
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message PLAdd(DUTY_USER_Model m)
        {
            //先删除
            if (1 == 1)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("delete from  DUTY_USER");//(DUTYDATE, BYORGNO, DUTYUSERTYPE, DUTYUSERID, ISATTENDED)");
                sb.AppendFormat(" where BYORGNO= '{0}'", m.BYORGNO);
                sb.AppendFormat(" and  DUTYDATE>='{0}'", m.dateBegin);
                sb.AppendFormat(" and  DUTYDATE<='{0}'", m.dateEnd);
                bool bln = DataBaseClass.ExeSql(sb.ToString());
            }

            List<string> sqllist = new List<string>();
            if (string.IsNullOrEmpty(m.DUTYUSERID) == false)//插入
            {
                StringBuilder sb = new StringBuilder();
                string insertstr = "";
                string[] arr = m.DUTYUSERID.Split('#');
                for (DateTime dt = Convert.ToDateTime(m.dateBegin); dt <= Convert.ToDateTime(m.dateEnd); dt = dt.AddDays(1))
                {
                    for (int i = 0; i < arr.Length; i++)//循环操作早班、中班、晚班、带班、总带班
                    {
                        var USERID = arr[i].Split(',');
                        if (USERID.Length>0)
                        {
                            sb.AppendFormat("INSERT INTO  DUTY_USER(DUTYDATE, BYORGNO, DUTYUSERTYPE, DUTYUSERID, ISATTENDED)");
                            for (int j = 0; j < USERID.Length; j++)
                            {
                                switch (i)
                                {
                                    case 0:
                                        m.DUTYUSERTYPE = "1";
                                        break;
                                    case 1:
                                        m.DUTYUSERTYPE = "2";
                                        break;
                                    case 2:
                                        m.DUTYUSERTYPE = "3";
                                        break;
                                    case 3:
                                        m.DUTYUSERTYPE = "-1";
                                        break;
                                    default:
                                        m.DUTYUSERTYPE = "-2";
                                        break;
                                }
                                sb.AppendFormat(" select '{0}'", ClsSql.EncodeSql(dt.ToString("yyyy-MM-dd")));
                                sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.BYORGNO));
                                sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.DUTYUSERTYPE));
                                sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(USERID[j]));
                                sb.AppendFormat(", '0'");
                                sb.AppendFormat(" UNION ALL ");
                            }
                            insertstr += sb.ToString();

                            string str = insertstr.Substring(0, insertstr.Length - 10);
                            insertstr = "";
                            sb.Remove(0, sb.Length);
                            sqllist.Add(str);
                        }                     
                    }
                }                           
            }
            var z = DataBaseClass.ExecuteSqlTran(sqllist);
            if (z > 0)
            {
                return new Message(true, "保存成功！", "");
            }
            else
            {
                return new Message(false, "保存失败，事物回滚！", "");
            }
        }
        #endregion

        #region 删除排班
        /// <summary>
        /// 删除排班
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message Del(DUTY_USER_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete from  DUTY_USER");//(DUTYDATE, BYORGNO, DUTYUSERTYPE, DUTYUSERID, ISATTENDED)");
            sb.AppendFormat(" where BYORGNO= '{0}'", m.BYORGNO);
            sb.AppendFormat(" and DUTYDATE= '{0}'", m.DUTYDATE);
            bool bln = DataBaseClass.ExeSql(sb.ToString());
          
            if (bln)
            {
                return new Message(true, "删除成功！", "");
            }
            else
            {
                return new Message(false, "删除失败！", "");
            }
        }
        #endregion

        #region 签到 
        /// <summary>
        /// 签到
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message Sign(DUTY_USER_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("update DUTY_USER set");
            sb.AppendFormat("  ISATTENDED='{0}'", ClsSql.EncodeSql("1"));
            sb.AppendFormat("  ,ATTENDEDTIME='{0}'", PublicClassLibrary.ClsSwitch.SwitTM(DateTime.Now));
            sb.AppendFormat(" where 1=1");
            sb.AppendFormat(" AND DUTYDATE='{0}'", ClsSql.EncodeSql(m.DUTYDATE));
            sb.AppendFormat(" AND BYORGNO='{0}'", ClsSql.EncodeSql(m.BYORGNO));
            sb.AppendFormat(" AND DUTYUSERID='{0}'", ClsSql.EncodeSql(m.DUTYUSERID));
            sb.AppendFormat(" AND DUTYUSERTYPE='{0}'", ClsSql.EncodeSql(m.DUTYUSERTYPE));
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

        #region 获取周末人员值班信息 DataTable getWeekDT(OD_USER_SW sw)
        /// <summary>
        /// 获取周末人员值班信息
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getWeekDT(DUTY_USER_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT    ");
            sb.AppendFormat(" DUID, DUTYDATE, BYORGNO, DUTYUSERTYPE, DUTYUSERID, ISATTENDED,ATTENDEDTIME");
            sb.AppendFormat(" FROM      DUTY_USER");
            sb.AppendFormat(" WHERE   1=1");
            sb.AppendFormat(" AND DUTYDATE in(SELECT DISTINCT DUTYDATE FROM  DUTY_USER where datepart(weekday,DUTYDATE)=1 or datepart(weekday,DUTYDATE)=7)");
            if (string.IsNullOrEmpty(sw.DTBegin) == false)
                sb.AppendFormat(" AND DUTYDATE>='{0}'", sw.DTBegin);
            if (string.IsNullOrEmpty(sw.DTEnd) == false)
                sb.AppendFormat(" AND DUTYDATE<='{0}'", sw.DTEnd);
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

            sb.AppendFormat(" order by DUTYDATE");
            return DataBaseClass.FullDataSet(sb.ToString()).Tables[0];

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
            return dt.Compute("count(DUTYUSERID)", "DUTYUSERID='" + uid + "'").ToString();
        }

        #endregion

        #region 统计值班次数
        /// <summary>
        /// 值班次数
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable GetDutyCoutDT(DUTY_USER_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select USERNAME,sum( CASE WHEN DUTYUSERTYPE = '1' THEN 1 ELSE 0 END) AS 'zaob',");
            sb.AppendFormat("sum( CASE WHEN DUTYUSERTYPE = '2' THEN 1 ELSE 0 END) AS 'zhongb',");
            sb.AppendFormat("sum( CASE WHEN DUTYUSERTYPE = '3' THEN 1 ELSE 0 END) AS 'wanb',");
            sb.AppendFormat("sum(CASE WHEN  DUTYUSERTYPE = '-1' THEN 1 ELSE 0 END) AS 'daib'");
            sb.AppendFormat(" FROM dbo.DUTY_USER left join dbo.T_SYSSEC_USER on  DUTYUSERID=USERID  where 1=1 ");
            sb.AppendFormat("and DUTYDATE>='{0}'", ClsSql.EncodeSql(sw.DTBegin));
            sb.AppendFormat("and DUTYDATE<='{0}'", ClsSql.EncodeSql(sw.DTEnd));
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
            sb.AppendFormat("select ou.DUTYUSERID from DUTY_USER as ou where 1=1");
            if (!string.IsNullOrEmpty(ondutyTime))
                sb.AppendFormat("  and ou.DUTYDATE='{0}'", ClsSwitch.SwitDate(ondutyTime));
            if (!string.IsNullOrEmpty(byorgon))
                sb.AppendFormat("  and ou.BYORGNO in({0})", ClsSql.EncodeSql(byorgon));
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        #endregion
    }
}
