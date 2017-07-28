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
    /// 出围管理基本类
    /// </summary>
    public class T_IPSFR_ROUTERAIL_RAIL
    {

        #region 根据单位返回市县乡及各日期后的统计
        /// <summary>
        /// 根据单位返回市县乡及各日期后的统计
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>dt</returns>
        public static DataTable getDTByOrgNoToDate(OutRaiLCount_SW sw)
        {
            sw.DateBegin = sw.DateBegin + " 00:00:00";
            sw.DateEnd = sw.DateEnd + " 23:59:59";
            StringBuilder sb = new StringBuilder();
            if (PublicCls.OrgIsShi(sw.TopORGNO))
            {
                sb.AppendFormat(" select count(substring(BYORGNO,1,6)+'000') as C,substring(BYORGNO,1,6)+'000' as BYORGNO,cast(CONVERT(varchar(100), sbtime, 111) as date) as SBDATE");
                sb.AppendFormat(" FROM T_IPSFR_ROUTERAIL_RAIL a  LEFT OUTER JOIN T_IPSFR_USER b ON a.HID = b.HID");
                sb.AppendFormat(" WHERE   (sbtime >= '{0}') AND (sbtime <= '{1}')", sw.DateBegin, sw.DateEnd);
                sb.AppendFormat(" group by  substring(BYORGNO,1,6)+'000',cast(CONVERT(varchar(100), sbtime, 111) as date)");
            }
            else if (PublicCls.OrgIsXian(sw.TopORGNO))
            {
                sb.AppendFormat(" select count(BYORGNO) as C, BYORGNO,cast(CONVERT(varchar(100), sbtime, 111) as date) as SBDATE");
                sb.AppendFormat(" FROM T_IPSFR_ROUTERAIL_RAIL a  LEFT OUTER JOIN T_IPSFR_USER b ON a.HID = b.HID");
                sb.AppendFormat(" WHERE   (sbtime >= '{0}') AND (sbtime <= '{1}')", sw.DateBegin, sw.DateEnd);
                sb.AppendFormat(" and   substring(BYORGNO,1,6)+'000'='{0}'", sw.TopORGNO);
                sb.AppendFormat(" group by  BYORGNO,cast(CONVERT(varchar(100), sbtime, 111) as date)");
            }
            else
            {
                sb.AppendFormat(" select count(a.HID) as C,a.HID as  BYORGNO,cast(CONVERT(varchar(100), sbtime, 111) as date) as SBDATE");
                sb.AppendFormat("  FROM T_IPSFR_ROUTERAIL_RAIL a  LEFT OUTER JOIN T_IPSFR_USER b ON a.HID = b.HID");
                sb.AppendFormat(" WHERE   (sbtime >= '{0}') AND (sbtime <= '{1}')", sw.DateBegin, sw.DateEnd);
                sb.AppendFormat(" and   BYORGNO='{0}'", sw.TopORGNO);
                sb.AppendFormat(" group by  a.HID,cast(CONVERT(varchar(100), sbtime, 111) as date)");
            }
            return DataBaseClass.FullDataSet(sb.ToString()).Tables[0];
        }

        #endregion

        /// <summary>
        /// 获取DataTable
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>DataTable</returns>
        public static DataTable getDT(OutRaiLCount_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" SELECT   a.HID,a.LONGITUDE,a.LATITUDE,a.SBTIME,b.HNAME,b.PHONE,b.BYORGNO");
            sb.AppendFormat(" FROM T_IPSFR_ROUTERAIL_RAIL a  LEFT OUTER JOIN T_IPSFR_USER b ON a.HID = b.HID");
            sb.AppendFormat(" where 1=1");//护林员启用状态
            if (string.IsNullOrEmpty(sw.DateBegin) == false)
                sb.AppendFormat(" and a.SBTIME >= '{0} 00:00:00'", sw.DateBegin);
            if (string.IsNullOrEmpty(sw.DateEnd) == false)
                sb.AppendFormat(" and a.SBTIME <= '{0} 23:59:59'", sw.DateEnd);

            if (string.IsNullOrEmpty(sw.orgNo) == false)
            {
                if (PublicCls.OrgIsShi(sw.orgNo))
                {
                    sb.AppendFormat(" and left(b.BYORGNO,4)='{0}'", PublicCls.getShiIncOrgNo(sw.orgNo));
                }
                else if (PublicCls.OrgIsXian(sw.orgNo))
                {
                    sb.AppendFormat(" and left(b.BYORGNO,6)='{0}'", PublicCls.getXianIncOrgNo(sw.orgNo));
                }
                else
                {
                    sb.AppendFormat(" and b.BYORGNO='{0}'", PublicCls.getZhenIncOrgNo(sw.orgNo));
                }
            }
            if (string.IsNullOrEmpty(sw.PhoneHname) == false)
                sb.AppendFormat(" AND (b.PHONE  like '%{0}%' or b.HNAME like '%{0}%')", ClsSql.EncodeSql(sw.PhoneHname));

            //sb.AppendFormat(" order by a.HID,a.SBTIME desc");
            return DataBaseClass.FullDataSet(sb.ToString()).Tables[0];
        }

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
    }
}
