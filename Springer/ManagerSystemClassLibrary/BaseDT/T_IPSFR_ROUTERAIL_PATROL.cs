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
    /// 漏检管理类
    /// </summary>
   public class T_IPSFR_ROUTERAIL_PATROL
   {
       #region 根据单位返回市县乡及各日期后的统计
       /// <summary>
       /// 根据单位返回市县乡及各日期后的统计
       /// </summary>
       /// <param name="sw">参见模型</param>
       /// <returns>dt</returns>
       public static DataTable getDTByOrgNoToDate(PatrolRouteStat_SW sw)
       {
           StringBuilder sb = new StringBuilder();
           if (PublicCls.OrgIsShi(sw.TopORGNO))
           {
               sb.AppendFormat(" select count(substring(BYORGNO,1,6)+'000') as C,substring(BYORGNO,1,6)+'000' as BYORGNO,ROUTEDATE,ROUTESTATE");
               sb.AppendFormat(" FROM T_IPSFR_ROUTERAIL_PATROL a  LEFT OUTER JOIN T_IPSFR_USER b ON a.HID = b.HID");
               sb.AppendFormat(" WHERE   (ROUTEDATE >= '{0}') AND (ROUTEDATE <= '{1}')", sw.DateBegin, sw.DateEnd);
               sb.AppendFormat(" group by  substring(BYORGNO,1,6)+'000',ROUTEDATE,ROUTESTATE");
           }
           else if (PublicCls.OrgIsXian(sw.TopORGNO))
           {
               sb.AppendFormat(" select count(BYORGNO) as C, BYORGNO,ROUTEDATE,ROUTESTATE");
               sb.AppendFormat(" FROM T_IPSFR_ROUTERAIL_PATROL a  LEFT OUTER JOIN T_IPSFR_USER b ON a.HID = b.HID");
               sb.AppendFormat(" WHERE   (ROUTEDATE >= '{0}') AND (ROUTEDATE <= '{1}')", sw.DateBegin, sw.DateEnd);
               sb.AppendFormat(" and   substring(BYORGNO,1,6)+'000'='{0}'", sw.TopORGNO);
               sb.AppendFormat(" group by  BYORGNO,ROUTEDATE,ROUTESTATE");
           }
           else
           {
               sb.AppendFormat(" select count(a.HID) as C,a.HID as  BYORGNO,ROUTEDATE,ROUTESTATE");
               sb.AppendFormat(" FROM T_IPSFR_ROUTERAIL_PATROL a  LEFT OUTER JOIN T_IPSFR_USER b ON a.HID = b.HID");
               sb.AppendFormat(" WHERE   (ROUTEDATE >= '{0}') AND (ROUTEDATE <= '{1}')", sw.DateBegin, sw.DateEnd);
               sb.AppendFormat(" and   BYORGNO='{0}'", sw.TopORGNO);
               sb.AppendFormat(" group by  a.HID,ROUTEDATE,ROUTESTATE");
           }
           return DataBaseClass.FullDataSet(sb.ToString()).Tables[0];
       }

       #endregion

       #region 根据单位返回市县乡优化后的统计
       /// <summary>
       /// 根据单位返回市县乡优化后的统计
       /// </summary>
       /// <param name="sw"></param>
       /// <returns></returns>
       public static DataTable getDTByOrgNo(PatrolRouteStat_SW sw)
       {

           StringBuilder sb = new StringBuilder();
           if (PublicCls.OrgIsShi(sw.TopORGNO))
           {
               sb.AppendFormat(" select count(substring(BYORGNO,1,6)+'000') as C,a.ROUTESTATE,substring(BYORGNO,1,6)+'000' as BYORGNO");
               sb.AppendFormat(" FROM T_IPSFR_ROUTERAIL_PATROL a  LEFT OUTER JOIN T_IPSFR_USER b ON a.HID = b.HID");
               sb.AppendFormat(" WHERE   (ROUTEDATE >= '{0}') AND (ROUTEDATE <= '{1}')", sw.DateBegin + " 00:00:00", sw.DateEnd + " 23:59:59");
               sb.AppendFormat(" group by  ROUTESTATE,substring(BYORGNO,1,6)+'000'");
           }
           else if (PublicCls.OrgIsXian(sw.TopORGNO))
           {
               sb.AppendFormat(" select count(BYORGNO) as C,a.ROUTESTATE, BYORGNO");
               sb.AppendFormat(" FROM T_IPSFR_ROUTERAIL_PATROL a  LEFT OUTER JOIN T_IPSFR_USER b ON a.HID = b.HID");
               sb.AppendFormat(" WHERE   (ROUTEDATE >= '{0}') AND (ROUTEDATE <= '{1}')", sw.DateBegin + " 00:00:00", sw.DateEnd + " 23:59:59");
               sb.AppendFormat(" and   substring(BYORGNO,1,6)+'000'='{0}'", sw.TopORGNO);
               sb.AppendFormat(" group by  ROUTESTATE,BYORGNO");
           }
           else
           {
               sb.AppendFormat("select count(a.hid) as C,a.ROUTESTATE,a.hid as  BYORGNO");
               sb.AppendFormat(" FROM T_IPSFR_ROUTERAIL_PATROL a  LEFT OUTER JOIN T_IPSFR_USER b ON a.HID = b.HID");
               sb.AppendFormat(" WHERE   (ROUTEDATE >= '{0}') AND (ROUTEDATE <= '{1}')", sw.DateBegin + " 00:00:00", sw.DateEnd + " 23:59:59");
               sb.AppendFormat(" and   BYORGNO='{0}'", sw.TopORGNO);
               sb.AppendFormat(" group by  ROUTESTATE,a.hid");
           }
           return DataBaseClass.FullDataSet(sb.ToString()).Tables[0];
       }

       #endregion

       #region 获取分组统计后的统计SUM
       /// <summary>
       /// 获取分组统计后的统计SUM
       /// </summary>
       /// <param name="dt"></param>
       /// <param name="orgNo"></param>
       /// <param name="state"></param>
       /// <returns></returns>
       public static string getSumByOrgNo(DataTable dt, string orgNo, string state)
       {
           string str = "";
           if (string.IsNullOrEmpty(orgNo))//返回所有记录个数
           {
               if (string.IsNullOrEmpty(state))
                   str = dt.Compute("sum(C)", "").ToString();
               else
                   str = dt.Compute("sum(C)", "ROUTESTATE=" + state).ToString();
           }
           else
           {
               if (string.IsNullOrEmpty(state))
                   str = dt.Compute("sum(C)", "BYORGNO=" + orgNo + "").ToString();
               else
                   str = dt.Compute("sum(C)", "BYORGNO=" + orgNo + " and ROUTESTATE=" + state).ToString();
           }
           if (string.IsNullOrEmpty(str))
               str = "0";
           return str;
       }

       #endregion


       #region 获取DataTable getDT(T_IPSFR_ROUTERAIL_PATROL_SW sw)
       /// <summary>
       /// 获取DataTable
       /// </summary>
       /// <param name="sw">参见模型</param>
       /// <returns>返回DataTable</returns>
       public static DataTable getDT(T_IPSFR_ROUTERAIL_PATROL_SW sw)
       {
          
           StringBuilder sb = new StringBuilder();
           sb.AppendFormat(" SELECT   a.HID, a.ROUTEDATE, a.LONGITUDE, a.ROADID,a.LATITUDE, a.ROUTESTATE,b.BYORGNO,b.PHONE,b.HNAME");
           sb.AppendFormat(" FROM T_IPSFR_ROUTERAIL_PATROL a  LEFT OUTER JOIN T_IPSFR_USER b ON a.HID = b.HID");
           sb.AppendFormat(" where 1=1");//护林员启用状态
           if (string.IsNullOrEmpty(sw.HID) == false)
               sb.AppendFormat(" and a.HID='{0}'", sw.HID);
           if (string.IsNullOrEmpty(sw.DateBegin) == false)
               sb.AppendFormat(" and ROUTEDATE >= '{0}'", sw.DateBegin);
           if (string.IsNullOrEmpty(sw.DateEnd) == false)
               sb.AppendFormat(" and ROUTEDATE <= '{0}'", sw.DateEnd);
           if (string.IsNullOrEmpty(sw.orgNo) == false)
           {
               if (PublicCls.OrgIsShi(sw.orgNo))
               {
                   sb.AppendFormat(" and b.BYORGNO like '{0}%'", PublicCls.getShiIncOrgNo(sw.orgNo));
               }
               else if (PublicCls.OrgIsXian(sw.orgNo))
               {
                   sb.AppendFormat(" and b.BYORGNO like '{0}%'", PublicCls.getXianIncOrgNo(sw.orgNo));
               }
               else
               {
                   sb.AppendFormat(" and b.BYORGNO='{0}'", PublicCls.getZhenIncOrgNo(sw.orgNo));
               }
           } 
           if (string.IsNullOrEmpty(sw.PhoneHname) == false)
               sb.AppendFormat(" AND (b.PHONE  like '%{0}%' or b.HNAME like '%{0}%')", ClsSql.EncodeSql(sw.PhoneHname));

           sb.AppendFormat(" order by a.HID,a.ROUTEDATE DESC,a.ROUTESTATE desc");
           return DataBaseClass.FullDataSet(sb.ToString()).Tables[0];
       }

       #endregion

       #region 根据DataTable和OrgNo判断记录个数
       /// <summary>
       /// 根据DataTable和OrgNo判断记录个数
       /// </summary>
       /// <param name="dt">DataTable</param>
       /// <param name="orgNo">单位编码</param>
       /// <param name="state">状态</param>
       /// <returns>记录个数</returns>
       public static string getCountByOrgNo(DataTable dt, string orgNo,string state)
       {
           if (string.IsNullOrEmpty(orgNo))//返回所有记录个数
               return dt.Rows.Count.ToString();
           string sqlStr = "";
           if (string.IsNullOrEmpty(state) == false)
           {
               if (state == "0") //未检
                   sqlStr = " AND ROUTESTATE=0";
               else
                   sqlStr = " AND ROUTESTATE=1";
           }
           if (PublicCls.OrgIsShi(orgNo))//市
           {
               return dt.Compute("count(HID)", "substring(BYORGNO,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "'"+sqlStr+"").ToString();
           }
           else if (PublicCls.OrgIsXian(orgNo))//县
           {
               return dt.Compute("count(HID)", "substring(BYORGNO,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "'" + sqlStr + "").ToString();
           }
           else if (PublicCls.OrgIsZhen(orgNo))
           {
               return dt.Compute("count(HID)", "BYORGNO='" + PublicCls.getZhenIncOrgNo(orgNo) + "'" + sqlStr + "").ToString();
           }
           else //机构编码可能不正确
               return "";
       }
       #endregion

       #region 根据DataTable和HID判断记录个数
       /// <summary>
       /// 根据DataTable和HID判断记录个数
       /// </summary>
       /// <param name="dt">DataTable</param>
       /// <param name="HID">护林员序号</param>
       /// <param name="state">状态</param>
       /// <returns>记录个数</returns>
       public static string getCountByHID(DataTable dt, string HID, string state)
       {
           if (string.IsNullOrEmpty(HID))//返回所有记录个数
               return dt.Rows.Count.ToString();
           string sqlStr = "";
           if (string.IsNullOrEmpty(state) == false)
           {
               if (state == "0") //未检
                   sqlStr = " AND ROUTESTATE=0";
               else
                   sqlStr = " AND ROUTESTATE=1";
           }
           
               return dt.Compute("count(HID)", "HID='" + HID + "'" + sqlStr + "").ToString();
          
       }
       #endregion
    }
}
