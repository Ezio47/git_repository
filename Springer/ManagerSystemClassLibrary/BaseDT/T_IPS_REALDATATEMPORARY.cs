using DataBaseClassLibrary;
using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using ManagerSystemSearchWhereModel.LogicModel;
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
    /// 实时监测中间表
    /// </summary>
    public class T_IPS_REALDATATEMPORARY
    {
        #region 判断护林员当前是否在线
        /// <summary>
        /// 判断护林员当前是否在线
        /// </summary>
        /// <example>
        /// 传递参数：
        /// sw.USERID       护林员ID列表，多用户以逗号分隔
        /// sw.SearchTime   查询时间，空默认取当前时间
        /// sw.ORGNO        组织机构编码，非空取该组织机构下的所有护林员
        /// </example>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static DataTable getOnLineDtByOrgno(T_IPS_REALDATATEMPORARYSW sw)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" SELECT   a.BYORGNO,a.HNAME,a.PHONE,a.ONSTATE,a.HID,b.SBTIME,b.ISOUTRAIL");
            sb.AppendFormat(" FROM      T_IPSFR_USER a left outer join T_IPS_REALDATATEMPORARY  b on a.HID=b.USERID");
            sb.AppendFormat(" where a.ISENABLE=1");//护林员启用状态
            if (string.IsNullOrEmpty(sw.USERID) == false)
            {
                sb.AppendFormat(" and a.HID in({0})", sw.USERID);
            }

            if (string.IsNullOrEmpty(sw.SearchTime))
            {
                sb.AppendFormat(" and DATEDIFF(mi,SBTIME,'{1}') <= '{0}'  ", ConfigCls.inLineTimeInterval().ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            else
            {

                sb.AppendFormat(" and DATEDIFF(mi,SBTIME,'{1}') >= '{0}'", ConfigCls.inLineTimeInterval().ToString(), sw.SearchTime);
            }
            if (string.IsNullOrEmpty(sw.ORGNO) == false)
            {
                if (PublicCls.OrgIsShi(sw.ORGNO))
                {
                    sb.AppendFormat(" and a.BYORGNO like'{0}%'", PublicCls.getShiIncOrgNo(sw.ORGNO));
                }
                else if (PublicCls.OrgIsXian(sw.ORGNO))
                {
                    sb.AppendFormat(" and a.BYORGNO like '{0}%'", PublicCls.getXianIncOrgNo(sw.ORGNO));
                }
                else
                {
                    sb.AppendFormat(" and a.BYORGNO = '{0}'", PublicCls.getZhenIncOrgNo(sw.ORGNO));
                }
            }
            if (string.IsNullOrEmpty(sw.PhoneHname) == false)
            {

                sb.AppendFormat(" AND (a.PHONE  like '%{0}%' or a.HNAME like '%{0}%')", ClsSql.EncodeSql(sw.PhoneHname));
            }
            return DataBaseClass.FullDataSet(sb.ToString()).Tables[0];
        }

        #endregion

        #region 护林员定位 获取护林员最新一条位置信息
        /// <summary>
        /// 护林员定位 获取护林员最新一条位置信息
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>DataTable</returns>
        public static DataTable getTopOneDT(T_IPS_REALDATATEMPORARYSW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select REALDATAID, USERID, LONGITUDE, LATITUDE, HEIGHT, ELECTRIC, SPEED, DIRECTION, SBTIME, NOTE, ");
            sb.AppendFormat("    ORGNO, SBDATE, SBTIMEBEGIN, PATROLLENGTH, ISOUTRAIL");
            sb.AppendFormat(" from T_IPS_REALDATATEMPORARY t ");
            sb.AppendFormat(" where  1=1 ");
            if (string.IsNullOrEmpty(sw.USERID) == false)
            {
                sb.AppendFormat(" AND t.USERID in({0})", sw.USERID);
            }
            sb.AppendFormat(" AND REALDATAID=  (select top 1 REALDATAID from T_IPS_REALDATATEMPORARY where USERID = t.USERID order by SBDATE desc)");

            if (!string.IsNullOrEmpty(sw.ORGNO) && PublicCls.OrgIsZhen(sw.ORGNO.Trim()))
            {
                sb.AppendFormat(" AND USERID in (select HID from T_IPSFR_USER where BYORGNO ='{0}')", sw.ORGNO.Trim());
            }
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        #endregion

        #region 护林员周边火点距离
        /// <summary>
        /// 护林员周边火点距离
        /// </summary>
        /// <param name="sw">护林员距离</param>
        /// <returns></returns>
        public static DataTable getDTByArea(HlyAreaDataSW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" select REALDATAID, USERID, LONGITUDE, LATITUDE, HEIGHT, ELECTRIC, SPEED, DIRECTION, SBTIME, NOTE,    ORGNO, SBDATE, SBTIMEBEGIN, PATROLLENGTH, ISOUTRAIL ");
            sb.AppendFormat(" from T_IPS_REALDATATEMPORARY t ");
            sb.AppendFormat(" where (DATEDIFF(d, SBTIME, '{0}') = 0) AND  (dbo.fnGetDistance(LATITUDE,LONGITUDE, {1}, {2}) <= {3}) ", sw.DATETIME, sw.WD, sw.JD, sw.AREA);
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        #endregion

        #region 获取各护林员最新数据

        /// <summary>
        /// 获取各护林员最新数据
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getDT(T_IPS_REALDATATEMPORARYSW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT   REALDATAID, USERID, LONGITUDE, LATITUDE, HEIGHT, ELECTRIC, SPEED, DIRECTION, SBTIME, NOTE,ORGNO, SBDATE, SBTIMEBEGIN, PATROLLENGTH");
            sb.AppendFormat(" FROM    T_IPS_REALDATATEMPORARY ");


            //sb.AppendFormat(" SELECT   a.BYORGNO,a.HNAME,a.PHONE,a.HID,b.SBTIME");
            //sb.AppendFormat(" FROM      T_IPSFR_USER a left outer join T_IPS_REALDATATEMPORARY  b on a.HID=b.USERID");


            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.USERID) == false)
            {
                sb.AppendFormat(" and USERID in({0})", sw.USERID);
            }

            if (string.IsNullOrEmpty(sw.SearchTime) == false)//查询某一天的数据
                sb.AppendFormat(" and SBDATE = '{0}'", Convert.ToDateTime(sw.SearchTime).ToString("yyyy-MM-dd"));
            if (string.IsNullOrEmpty(sw.DateBegin) == false)
                sb.AppendFormat(" and SBDATE >= '{0}'", sw.DateBegin);
            if (string.IsNullOrEmpty(sw.DateEnd) == false)
                sb.AppendFormat(" and SBDATE <= '{0}'", sw.DateEnd);
            if (!string.IsNullOrEmpty(sw.ORGNO))
            {
                sb.AppendFormat(" AND USERID in (select HID from T_IPSFR_USER where BYORGNO ='{0}')", sw.ORGNO);
            }
            sb.AppendFormat(" ORDER BY SBDATE,SBTIME desc");

            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        #endregion

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
                /*
    select count(substring(BYORGNO,1,6)+'000') as C,substring(BYORGNO,1,6)+'000' as BYORGNO,CONVERT(varchar(100),SBDATE, 111) as DBDATE
    FROM T_IPS_REALDATATEMPORARY a  LEFT OUTER JOIN T_IPSFR_USER b ON a.USERID = b.HID
    WHERE   (SBDATE >= '2017-4-1') AND (SBDATE <= '2017-5-6')
    --and substring(BYORGNO,1,6)+'000'='532501000'
    group by  substring(BYORGNO,1,6)+'000',CONVERT(varchar(100),SBDATE, 111)
                 */
                sb.AppendFormat(" select count(substring(BYORGNO,1,6)+'000') as C,substring(BYORGNO,1,6)+'000' as BYORGNO,SBDATE");
                sb.AppendFormat(" FROM T_IPS_REALDATATEMPORARY a  LEFT OUTER JOIN T_IPSFR_USER b ON a.USERID = b.HID");
                sb.AppendFormat(" WHERE   (SBDATE >= '{0}') AND (SBDATE <= '{1}')", sw.DateBegin, sw.DateEnd);
                sb.AppendFormat(" group by  substring(BYORGNO,1,6)+'000',SBDATE");
            }
            else if (PublicCls.OrgIsXian(sw.TopORGNO))
            {
                //                select count(BYORGNO) as C, BYORGNO,CONVERT(varchar(100),SBDATE, 111) as DBDATE
                //FROM T_IPS_REALDATATEMPORARY a  LEFT OUTER JOIN T_IPSFR_USER b ON a.USERID = b.HID
                //WHERE   (SBDATE >= '2017-4-1') AND (SBDATE <= '2017-5-6')
                //and   substring(BYORGNO,1,6)+'000'='532501000'--", sw.TopORGNO);
                //group by  BYORGNO,CONVERT(varchar(100),SBDATE, 111)
                sb.AppendFormat(" select count(BYORGNO) as C, BYORGNO,SBDATE");
                sb.AppendFormat(" FROM T_IPS_REALDATATEMPORARY a  LEFT OUTER JOIN T_IPSFR_USER b ON a.USERID = b.HID");
                sb.AppendFormat(" WHERE   (SBDATE >= '{0}') AND (SBDATE <= '{1}')", sw.DateBegin, sw.DateEnd);
                sb.AppendFormat(" and   substring(BYORGNO,1,6)+'000'='{0}'", sw.TopORGNO);
                sb.AppendFormat(" group by  BYORGNO,SBDATE");
            }
            else
            {
                //select count(a.USERID) as C,a.USERID as  BYORGNO,CONVERT(varchar(100),SBDATE, 111) as DBDATE
                // FROM T_IPS_REALDATATEMPORARY a  LEFT OUTER JOIN T_IPSFR_USER b ON a.USERID = b.HID
                //WHERE   (SBDATE >= '2017-4-1') AND (SBDATE <= '2017-5-6')
                //and   BYORGNO='532501001'--", sw.TopORGNO);
                //group by  a.USERID,CONVERT(varchar(100),SBDATE, 111)
                sb.AppendFormat(" select count(a.USERID) as C,a.USERID as  BYORGNO,SBDATE");
                sb.AppendFormat("  FROM T_IPS_REALDATATEMPORARY a  LEFT OUTER JOIN T_IPSFR_USER b ON a.USERID = b.HID");
                sb.AppendFormat(" WHERE   (SBDATE >= '{0}') AND (SBDATE <= '{1}')", sw.DateBegin, sw.DateEnd);
                sb.AppendFormat(" and   BYORGNO='{0}'", sw.TopORGNO);
                sb.AppendFormat(" group by  a.USERID,SBDATE");
            }
            return DataBaseClass.FullDataSet(sb.ToString()).Tables[0];
        }
        
        #endregion

        #region 根据单位返回市县乡巡检总数 已巡 未巡 
        /// <summary>
        /// 根据单位返回市县乡巡检总数 已巡 未巡
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getPATROLCOUNTDTByOrgNo(PatrolRouteStat_SW sw)
        {

            StringBuilder sb = new StringBuilder();
            if (PublicCls.OrgIsShi(sw.TopORGNO))
            {
                sb.AppendFormat(" select sum(PATROLCOUNT) as C1,sum(PATROLYESCOUNT) as C2,sum(PATROLNOCOUNT) as C3,substring(BYORGNO,1,6)+'000' as BYORGNO");
                sb.AppendFormat(" FROM T_IPS_REALDATATEMPORARY a  LEFT OUTER JOIN T_IPSFR_USER b ON a.USERID = b.HID");
                sb.AppendFormat(" WHERE   (SBDATE >= '{0}') AND (SBDATE <= '{1}')", sw.DateBegin, sw.DateEnd);
                sb.AppendFormat(" group by  substring(BYORGNO,1,6)+'000'");
            }
            else if (PublicCls.OrgIsXian(sw.TopORGNO))
            {
                sb.AppendFormat(" select sum(PATROLCOUNT) as C1,sum(PATROLYESCOUNT) as C2,sum(PATROLNOCOUNT) as C3, BYORGNO");
                sb.AppendFormat(" FROM T_IPS_REALDATATEMPORARY a  LEFT OUTER JOIN T_IPSFR_USER b ON a.USERID = b.HID");
                sb.AppendFormat(" WHERE   (SBDATE >= '{0}') AND (SBDATE <= '{1}')", sw.DateBegin, sw.DateEnd);
                sb.AppendFormat(" and   substring(BYORGNO,1,6)+'000'='{0}'", sw.TopORGNO);
                sb.AppendFormat(" group by  BYORGNO");
            }
            else
            {
                sb.AppendFormat(" select sum(PATROLCOUNT) as C1,sum(PATROLYESCOUNT) as C2,sum(PATROLNOCOUNT) as C3,a.USERID as BYORGNO");
                sb.AppendFormat("  FROM T_IPS_REALDATATEMPORARY a  LEFT OUTER JOIN T_IPSFR_USER b ON a.USERID = b.HID");
                sb.AppendFormat(" WHERE   (SBDATE >= '{0}') AND (SBDATE <= '{1}')", sw.DateBegin, sw.DateEnd);
                sb.AppendFormat(" and   BYORGNO='{0}'", sw.TopORGNO);
                sb.AppendFormat(" group by  a.USERID");
            }
            return DataBaseClass.FullDataSet(sb.ToString()).Tables[0];
        }

        #endregion

        #region 根据单位返回市县乡优化后的统计
        /// <summary>
        /// 根据单位返回市县乡优化后的统计
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>DT</returns>
        public static DataTable getDTByOrgNo(PatrolRouteStat_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            if (PublicCls.OrgIsShi(sw.TopORGNO))
            {
                sb.AppendFormat(" select count(substring(BYORGNO,1,6)+'000') as C,sum(PATROLCOUNT) as C1,sum(PATROLYESCOUNT) as C2,sum(PATROLNOCOUNT) as C3,substring(BYORGNO,1,6)+'000' as BYORGNO");
                sb.AppendFormat(" FROM T_IPS_REALDATATEMPORARY a  LEFT OUTER JOIN T_IPSFR_USER b ON a.USERID = b.HID");
                sb.AppendFormat(" WHERE   (SBDATE >= '{0}') AND (SBDATE <= '{1}')", sw.DateBegin, sw.DateEnd);
                sb.AppendFormat(" group by  substring(BYORGNO,1,6)+'000'");
            }
            else if (PublicCls.OrgIsXian(sw.TopORGNO))
            {
                sb.AppendFormat(" select count(BYORGNO) as C,sum(PATROLCOUNT) as C1,sum(PATROLYESCOUNT) as C2,sum(PATROLNOCOUNT) as C3, BYORGNO");
                sb.AppendFormat(" FROM T_IPS_REALDATATEMPORARY a  LEFT OUTER JOIN T_IPSFR_USER b ON a.USERID = b.HID");
                sb.AppendFormat(" WHERE   (SBDATE >= '{0}') AND (SBDATE <= '{1}')", sw.DateBegin, sw.DateEnd);
                sb.AppendFormat(" and   substring(BYORGNO,1,6)+'000'='{0}'", sw.TopORGNO);
                sb.AppendFormat(" group by  BYORGNO");
            }
            else
            {
                sb.AppendFormat("select count(a.USERID) as C,sum(PATROLCOUNT) as C1,sum(PATROLYESCOUNT) as C2,sum(PATROLNOCOUNT) as C3,a.USERID as  BYORGNO");
                sb.AppendFormat("  FROM T_IPS_REALDATATEMPORARY a  LEFT OUTER JOIN T_IPSFR_USER b ON a.USERID = b.HID");
                sb.AppendFormat(" WHERE   (SBDATE >= '{0}') AND (SBDATE <= '{1}')", sw.DateBegin, sw.DateEnd);
                sb.AppendFormat(" and   BYORGNO='{0}'", sw.TopORGNO);
                sb.AppendFormat(" group by  a.USERID");
            }
            return DataBaseClass.FullDataSet(sb.ToString()).Tables[0];

        }
        /// <summary>
        /// 获取分组统计后的统计SUM
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="orgNo"></param> 
        /// <returns></returns>
        public static string getDTByOrgNoSum(DataTable dt, string orgNo)
        {
            string str = "";
            if (string.IsNullOrEmpty(orgNo))//返回所有记录个数
            {
                str = dt.Compute("sum(C)", "").ToString();
            }
            else
            {
                str = dt.Compute("sum(C)", "BYORGNO=" + orgNo + "").ToString();
            }
            if (string.IsNullOrEmpty(str))
                str = "0";
            return str;
        }

        #endregion

        #region 怠工 根据单位返回市县乡优化后的统计
        /// <summary>
        /// 怠工
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>DT</returns>
        public static DataTable getDTSabotageByOrgNo(SabotageCount_SW sw)
        {
            string LengthError = (ConfigCls.getPatrolLengthError() / 1000).ToString();//巡检距离误差
            StringBuilder sb = new StringBuilder();
            if (PublicCls.OrgIsShi(sw.TopORGNO))
            {
                sb.AppendFormat(" select count(substring(BYORGNO,1,6)+'000') as C,substring(BYORGNO,1,6)+'000' as BYORGNO,SBDATE,");
                sb.AppendFormat(" case when(a.PATROLLENGTH-b.PATROLLENGTH)<{0} then 1 else 0 end as PatrolLenErro", LengthError);
                sb.AppendFormat(" FROM      T_IPSFR_USER a left outer join T_IPS_REALDATATEMPORARY  b on a.HID=b.USERID");
                sb.AppendFormat(" where a.ISENABLE=1 and b.USERID is not null and a.PATROLLENGTH>0");
                sb.AppendFormat(" and   (SBDATE >= '{0}') AND (SBDATE <= '{1}')", sw.DateBegin, sw.DateEnd);
                sb.AppendFormat(" group by SBDATE,substring(BYORGNO,1,6)+'000',case when(a.PATROLLENGTH-b.PATROLLENGTH)<{0} then 1 else 0 end", LengthError);
            }
            else if (PublicCls.OrgIsXian(sw.TopORGNO))
            {
                sb.AppendFormat(" select count(BYORGNO) as C,BYORGNO,SBDATE,");
                sb.AppendFormat(" case when(a.PATROLLENGTH-b.PATROLLENGTH)<{0} then 1 else 0 end as PatrolLenErro", LengthError);
                sb.AppendFormat(" FROM      T_IPSFR_USER a left outer join T_IPS_REALDATATEMPORARY  b on a.HID=b.USERID");
                sb.AppendFormat(" where a.ISENABLE=1 and b.USERID is not null and a.PATROLLENGTH>0");
                sb.AppendFormat(" and   (SBDATE >= '{0}') AND (SBDATE <= '{1}')", sw.DateBegin, sw.DateEnd);
                sb.AppendFormat(" and   substring(BYORGNO,1,6)+'000'='{0}'", sw.TopORGNO);
                sb.AppendFormat(" group by SBDATE,BYORGNO,case when(a.PATROLLENGTH-b.PATROLLENGTH)<{0} then 1 else 0 end", LengthError);

            }
            else
            {
                sb.AppendFormat(" select count(b.USERID) as C,b.USERID as BYORGNO,SBDATE,");
                sb.AppendFormat(" case when(a.PATROLLENGTH-b.PATROLLENGTH)<{0} then 1 else 0 end as PatrolLenErro", LengthError);
                sb.AppendFormat(" FROM      T_IPSFR_USER a left outer join T_IPS_REALDATATEMPORARY  b on a.HID=b.USERID");
                sb.AppendFormat(" where a.ISENABLE=1 and b.USERID is not null and a.PATROLLENGTH>0");
                sb.AppendFormat(" and   (SBDATE >= '{0}') AND (SBDATE <= '{1}')", sw.DateBegin, sw.DateEnd);
                sb.AppendFormat(" and   BYORGNO='{0}'", sw.TopORGNO);
                sb.AppendFormat(" group by SBDATE,b.USERID,case when(a.PATROLLENGTH-b.PATROLLENGTH)<{0} then 1 else 0 end", LengthError);


            }
            return DataBaseClass.FullDataSet(sb.ToString()).Tables[0];

        }

        #endregion

        #region 护林员与数据上传中间表组合查询
        /// <summary>
        /// 护林员与数据上传中间表组合查询
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static DataTable getHRAndRealDataDT(T_IPS_REALDATATEMPORARYSW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" SELECT   a.BYORGNO,a.HNAME,a.HID,a.PHONE,b.USERID, b.SBDATE,(b.PATROLLENGTH-a.PATROLLENGTH) as PatrolLenError,a.PATROLLENGTH,b.PATROLLENGTH as RealPATROLLENGTH");
            sb.AppendFormat(" FROM      T_IPSFR_USER a left outer join T_IPS_REALDATATEMPORARY  b on a.HID=b.USERID");
            sb.AppendFormat(" where a.ISENABLE=1 and b.USERID is not null");//护林员启用状态
            if (string.IsNullOrEmpty(sw.DateBegin) == false)
                sb.AppendFormat(" and SBDATE >= '{0}'", sw.DateBegin);
            if (string.IsNullOrEmpty(sw.DateEnd) == false)
                sb.AppendFormat(" and SBDATE <= '{0}'", sw.DateEnd);
            if (string.IsNullOrEmpty(sw.ORGNO) == false)
            {
                if (PublicCls.OrgIsShi(sw.ORGNO))
                {
                    sb.AppendFormat(" and a.BYORGNO like'{0}%'", PublicCls.getShiIncOrgNo(sw.ORGNO));
                }
                else if (PublicCls.OrgIsXian(sw.ORGNO))
                {
                    sb.AppendFormat(" and a.BYORGNO like '{0}%'", PublicCls.getXianIncOrgNo(sw.ORGNO));
                }
                else
                {
                    sb.AppendFormat(" and a.BYORGNO = '{0}'", PublicCls.getZhenIncOrgNo(sw.ORGNO));
                }
            }
            if (string.IsNullOrEmpty(sw.PhoneHname) == false)
                sb.AppendFormat(" AND (a.PHONE  like '%{0}%' or a.HNAME like '%{0}%')", ClsSql.EncodeSql(sw.PhoneHname));
            return DataBaseClass.FullDataSet(sb.ToString()).Tables[0];
        }

        #endregion

        #region 根据DataTable和OrgNo LengthError 判断怠工记录个数 --正常的
        /// <summary>
        /// 正常的
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="orgNo"></param>
        /// <param name="LengthError"></param>
        /// <returns>参见模型</returns>
        public static string getCountByPatrolLengthError(DataTable dt, string orgNo, float LengthError)
        {
            if (PublicCls.OrgIsShi(orgNo))//市
            {
                return dt.Compute("count(HID)", "substring(BYORGNO,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "' and PatrolLenError>=" + LengthError.ToString()).ToString();
            }
            else if (PublicCls.OrgIsXian(orgNo))//县
            {
                return dt.Compute("count(HID)", "substring(BYORGNO,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "' and PatrolLenError>=" + LengthError.ToString()).ToString();
            }
            else if (PublicCls.OrgIsZhen(orgNo))
            {
                return dt.Compute("count(HID)", "BYORGNO='" + PublicCls.getZhenIncOrgNo(orgNo) + "' and PatrolLenError>=" + LengthError.ToString()).ToString();
            }
            else //机构编码可能不正确
                return "";
        }

        #endregion

        #region 根据DataTable、orgNo和SBDATE判断记录个数
        /// <summary>
        /// 根据DataTable、orgNo和SBDATE判断记录个数
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="orgNo">单位编码</param>
        /// <param name="SBDATE">上报日期</param>
        /// <returns>记录个数</returns>
        public static string getCountByOrgNoSBDATE(DataTable dt, string orgNo, string SBDATE)
        {

            if (PublicCls.OrgIsShi(orgNo))//市
            {
                return dt.Compute("count(HID)", "substring(BYORGNO,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "' and SBDATE='" + SBDATE + "'").ToString();
            }
            else if (PublicCls.OrgIsXian(orgNo))//县
            {
                return dt.Compute("count(HID)", "substring(BYORGNO,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "' and SBDATE='" + SBDATE + "'").ToString();
            }
            else if (PublicCls.OrgIsZhen(orgNo))
            {
                return dt.Compute("count(HID)", "BYORGNO='" + PublicCls.getZhenIncOrgNo(orgNo) + "' and SBDATE='" + SBDATE + "'").ToString();
            }
            else //机构编码可能不正确
                return "";
        }
        #endregion

        #region 根据DataTable和OrgNo判断记录个数
        /// <summary>
        /// 根据DataTable和OrgNo判断记录个数
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="orgNo">单位编码</param>
        /// <returns>记录个数</returns>
        public static string getCountByOrgNo(DataTable dt, string orgNo)
        {
            if (string.IsNullOrEmpty(orgNo))//返回所有记录个数
                return dt.Rows.Count.ToString();
            if (PublicCls.OrgIsShi(orgNo))//市
            {
                return dt.Compute("count(HID)", "substring(BYORGNO,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "'").ToString();
            }
            else if (PublicCls.OrgIsXian(orgNo))//县
            {
                return dt.Compute("count(HID)", "substring(BYORGNO,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "'").ToString();
            }
            else if (PublicCls.OrgIsZhen(orgNo))
            {
                return dt.Compute("count(HID)", "BYORGNO='" + PublicCls.getZhenIncOrgNo(orgNo) + "'").ToString();
            }
            else //机构编码可能不正确
                return "";
        }
        #endregion

        #region 根据DataTable和HID判断记录个数
        /// <summary>
        /// 根据DataTable和HID判断记录个数
        /// </summary>
        /// <param name="dt">护林员DataTable</param>
        /// <param name="HID">护林员序号</param>
        /// <returns>记录个数</returns>
        public static string getCountByHID(DataTable dt, string HID)
        {
            if (string.IsNullOrEmpty(HID))//返回所有记录个数
                return dt.Rows.Count.ToString();

            return dt.Compute("count(HID)", "HID='" + HID + "'").ToString();

        }
        #endregion

        #region 根据DataTable、SBDATE和HID判断记录个数
        /// <summary>
        /// 根据DataTable、SBDATE和HID判断记录个数
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="HID">护林员序号</param>
        /// <param name="SBDATE">上报日期</param>
        /// <returns>记录个数</returns>
        public static string getCountBySBDATEHID(DataTable dt, string HID, string SBDATE)
        {
            if (string.IsNullOrEmpty(HID))//返回所有记录个数
                return dt.Rows.Count.ToString();

            return dt.Compute("count(HID)", "HID='" + HID + "' and SBDATE='" + SBDATE + "'").ToString();

        }
        #endregion

    }
}
