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
    /// 数据上报类
    /// </summary>
    public class T_IPSRPT_REPORT
    {
        #region 上传管理
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(T_IPSRPT_REPORT_SW sw)
        {
            if (string.IsNullOrEmpty(sw.REPORTID))
                return new Message(false, "删除失败，请选择要处理的记录！", "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("DELETE FROM T_IPSRPT_REPORTDATA");
            sb.AppendFormat(" where  REPORTID= '{0}'", ClsSql.EncodeSql(sw.REPORTID));

            bool bln = DataBaseClass.ExeSql(sb.ToString());
            DelDetail(new T_IPSRPT_REPORT_SW { REPORTID = sw.REPORTID });
            DelUpload(new T_IPSRPT_REPORT_SW { REPORTID = sw.REPORTID });
            if (bln == true)
                return new Message(true, "删除成功！", "");
            else
                return new Message(false, "删除失败，请检查各输入框是否正确！", "");
        }
        /// <summary>
        /// 删除明细
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message DelDetail(T_IPSRPT_REPORT_SW sw)
        {
            if (string.IsNullOrEmpty(sw.REPORTDETAILID) || string.IsNullOrEmpty(sw.REPORTID))
                return new Message(false, "删除失败，请选择要处理的记录！", "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("DELETE FROM T_IPSRPT_DATADETAIL");
            sb.AppendFormat(" where 1=1");
            if (string.IsNullOrEmpty(sw.REPORTDETAILID) == false)
                sb.AppendFormat(" and  REPORTDETAILID= '{0}'", ClsSql.EncodeSql(sw.REPORTDETAILID));
            if (string.IsNullOrEmpty(sw.REPORTID) == false)
                sb.AppendFormat(" and  REPORTID= '{0}'", ClsSql.EncodeSql(sw.REPORTID));

            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功！", "");
            else
                return new Message(false, "删除失败，请检查各输入框是否正确！", "");
        }
        /// <summary>
        /// 删除上传
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message DelUpload(T_IPSRPT_REPORT_SW sw)
        {
            if (string.IsNullOrEmpty(sw.REPORTUPLOADID) || string.IsNullOrEmpty(sw.REPORTID))
                return new Message(false, "删除失败，请选择要处理的记录！", "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("DELETE FROM T_IPSRPT_DATAUPLOAD");
            sb.AppendFormat(" where 1=1");
            if (string.IsNullOrEmpty(sw.REPORTUPLOADID) == false)
                sb.AppendFormat(" and  REPORTUPLOADID= '{0}'", ClsSql.EncodeSql(sw.REPORTUPLOADID));
            if (string.IsNullOrEmpty(sw.REPORTID) == false)
                sb.AppendFormat(" and  REPORTID= '{0}'", ClsSql.EncodeSql(sw.REPORTID));

            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功！", "");
            else
                return new Message(false, "删除失败，请检查各输入框是否正确！", "");
        }
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Man(T_IPSRPT_REPORT_Model m)
        {
            if (string.IsNullOrEmpty(m.REPORTID))
                return new Message(false, "修改失败，请选择要处理的记录！", "");
            StringBuilder sb = new StringBuilder();
            //(ORGNO,LOGINUSERNAME,USERNAME,USERPWD,REGISTERTIME,LOGINNUM,NOTE)
            sb.AppendFormat("UPDATE T_IPSRPT_REPORTDATA");
            sb.AppendFormat(" set ");
            sb.AppendFormat(" COLLECTNAME='{0}'", ClsSql.EncodeSql(m.COLLECTNAME));
            sb.AppendFormat(" ,MANSTATE='{0}'", "1");
            sb.AppendFormat(" ,MANRESULT='{0}'", ClsSql.EncodeSql(m.MANRESULT));
            if (!string.IsNullOrEmpty(m.ADDRESS))
            {
                sb.AppendFormat(" ,ADDRESS='{0}'", ClsSql.EncodeSql(m.ADDRESS));
            }
            sb.AppendFormat(" ,MANTIME='{0}'", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sb.AppendFormat(" ,MANUSERID='{0}'", SystemCls.getUserID());
            sb.AppendFormat(" where  REPORTID= '{0}'", ClsSql.EncodeSql(m.REPORTID));

            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "处理成功！", "");
            else
                return new Message(false, "处理失败，请检查各输入框是否正确！", "");
        }
        #endregion

        #region 根据DataTable和类型判断记录个数
        /// <summary>
        /// 判断记录个数
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="typeID">类别序号</param>
        /// <returns>记录个数</returns>
        public static string getCountByType(DataTable dt, string typeID)
        {

            return dt.Compute("count(REPORTID)", "SYSTYPEVALUE='" + typeID + "'").ToString();

        }
        #endregion
        #region 获取上报与护林员、单位组合查询
        /// <summary>
        /// 获取上报与护林员、单位组合查询 用于统计
        /// </summary>
        /// <param name="sw">参见模型 sw.TopORGNO 顶级单位编码 sw.DateBegin 开始日期 年月日 sw.DateEnd 结束日期 年月日</param>
        /// <returns>返回DataTable HID, SYSTYPEVALUE, REPORTTIME, HNAME,ORGNAME,ORGNO</returns>
        public static DataTable getDTByOrgHUse(T_IPSRPT_REPORT_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT a.HID, a.SYSTYPEVALUE, a.REPORTTIME, b.HNAME,c.ORGNAME,c.ORGNO");
            sb.AppendFormat(" FROM T_IPSRPT_REPORTDATA a ");
            sb.AppendFormat(" left outer join T_IPSFR_USER b on a.hid=b.hid");
            sb.AppendFormat(" left outer join T_SYS_ORG c on b.BYORGNO=c.ORGNO");
            sb.AppendFormat(" WHERE 1=1");

            if (string.IsNullOrEmpty(sw.TopORGNO) == false)
            {
                if (sw.TopORGNO.Substring(4, 11) == "00000000000")//获取所有市的
                    sb.AppendFormat(" AND SUBSTRING(ORGNO,1,4) = '{0}'", ClsSql.EncodeSql(sw.TopORGNO.Substring(0, 4)));
                else if (sw.TopORGNO.Substring(6, 9) == "000000000")//获取所有县的
                    sb.AppendFormat(" AND SUBSTRING(ORGNO,1,6) = '{0}'", ClsSql.EncodeSql(sw.TopORGNO.Substring(0, 6)));
                else if (sw.TopORGNO.Substring(9,6)=="000000")
                    sb.AppendFormat(" AND SUBSTRING(ORGNO,1,9) = '{0}'", ClsSql.EncodeSql(sw.TopORGNO.Substring(0, 9)));
                else
                    sb.AppendFormat(" AND ORGNO = '{0}'", ClsSql.EncodeSql(sw.TopORGNO));
            }
            if (!string.IsNullOrEmpty(sw.DateBegin))
            {
                sb.AppendFormat(" AND REPORTTIME>='{0} 00:00:00'", sw.DateBegin);
            }
            if (!string.IsNullOrEmpty(sw.DateEnd))
            {
                sb.AppendFormat(" AND REPORTTIME<='{0} 23:59:59'", sw.DateEnd);
            }

            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        #endregion
        #region 获取上报与护林员、单位组合数量统计
        /// <summary>
        /// 获取上报与护林员、单位组合数量统计
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="ORGNO">单位编码</param>
        /// <param name="SYSTYPEVALUE">类型ID</param>
        /// <returns>上报数量</returns>
        public static string getCountByOrgHUse(DataTable dt, string ORGNO, string SYSTYPEVALUE)
        {
            if (PublicCls.OrgIsShi(ORGNO))//市
            {
                if (string.IsNullOrEmpty(SYSTYPEVALUE))
                    return dt.Compute("count(HID)", "substring(ORGNO,1,4)='" + PublicCls.getShiIncOrgNo(ORGNO) + "'").ToString();
                else
                    return dt.Compute("count(HID)", "substring(ORGNO,1,4)='" + PublicCls.getShiIncOrgNo(ORGNO) + "' and SYSTYPEVALUE='" + SYSTYPEVALUE + "'").ToString();
            }
            else if (PublicCls.OrgIsXian(ORGNO))//县
            {
                if (string.IsNullOrEmpty(SYSTYPEVALUE))
                    return dt.Compute("count(HID)", "substring(ORGNO,1,6)='" + PublicCls.getXianIncOrgNo(ORGNO) + "'").ToString();
                else
                    return dt.Compute("count(HID)", "substring(ORGNO,1,6)='" + PublicCls.getXianIncOrgNo(ORGNO) + "' and SYSTYPEVALUE='" + SYSTYPEVALUE + "'").ToString();
            }
            else
            {
                if (string.IsNullOrEmpty(SYSTYPEVALUE))
                    return dt.Compute("count(HID)", "substring(ORGNO,1,9)='" + PublicCls.getZhenIncOrgNo(ORGNO) + "'").ToString();
                else
                    return dt.Compute("count(HID)", "substring(ORGNO,1,9)='" + PublicCls.getZhenIncOrgNo(ORGNO) + "' and SYSTYPEVALUE='" + SYSTYPEVALUE + "'").ToString();
            }
            //return str;
        }
        #endregion

        #region 获取上报与护林员、单位组合数量统计 统计各个护林员
        /// <summary>
        /// 获取上报与护林员、单位组合数量统计 统计各个护林员
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="HID">护林员ID</param>
        /// <param name="SYSTYPEVALUE">上报类型ID</param>
        /// <returns>上报数量</returns>
        public static string getCountByHID(DataTable dt, string HID, string SYSTYPEVALUE)
        {
            if (string.IsNullOrEmpty(SYSTYPEVALUE))
                return dt.Compute("count(HID)", "HID='" + HID + "'").ToString();
            else
                return dt.Compute("count(HID)", "HID='" + HID + "' and SYSTYPEVALUE='" + SYSTYPEVALUE + "'").ToString();

        }
        #endregion

        #region 获取DataTable
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>DataTable</returns>

        public static DataTable getDT(T_IPSRPT_REPORT_SW sw)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT REPORTID, HID, SYSTYPEVALUE, ADDRESS, REPORTTIME, COLLECTNAME, MANSTATE, MANRESULT,MANTIME, MANUSERID");
            sb.AppendFormat(" FROM T_IPSRPT_REPORTDATA ");
            sb.AppendFormat(" WHERE   1=1");
            //判断是否是多少，如果是多少，用in，如果只是一个，用=  主要考虑到速度
            if (string.IsNullOrEmpty(sw.REPORTID) == false)
            {
                if (sw.REPORTID.Split(',').Length > 1)
                    sb.AppendFormat(" AND REPORTID in({0})", ClsSql.EncodeSql(sw.REPORTID));
                else
                    sb.AppendFormat(" AND REPORTID ='{0}'", ClsSql.EncodeSql(sw.REPORTID));
            }
            if (string.IsNullOrEmpty(sw.SYSTYPEVALUE) == false)
                sb.AppendFormat(" AND SYSTYPEVALUE ='{0}'", ClsSql.EncodeSql(sw.SYSTYPEVALUE));

            if (string.IsNullOrEmpty(sw.MANSTATE) == false)
                sb.AppendFormat(" AND MANSTATE ='{0}'", ClsSql.EncodeSql(sw.MANSTATE));
            //根据护林员ID查询
            if (string.IsNullOrEmpty(sw.HID) == false)
                sb.AppendFormat(" AND HID ='{0}'", ClsSql.EncodeSql(sw.HID));

            if (string.IsNullOrEmpty(sw.orgNo) == false)
            {
                if (PublicCls.OrgIsShi(sw.orgNo))
                {
                    sb.AppendFormat(" and HID in (SELECT    HID FROM      T_IPSFR_USER where BYORGNO like '{0}%')", PublicCls.getShiIncOrgNo(sw.orgNo));
                }
                else if (PublicCls.OrgIsXian(sw.orgNo))
                {
                    sb.AppendFormat(" and HID in (SELECT    HID FROM      T_IPSFR_USER where BYORGNO like '{0}%')", PublicCls.getXianIncOrgNo(sw.orgNo));
                }
                else if (PublicCls.OrgIsZhen(sw.orgNo))
                {
                    sb.AppendFormat(" and HID in (SELECT    HID FROM      T_IPSFR_USER where BYORGNO='{0}')", PublicCls.getZhenIncOrgNo(sw.orgNo));
                }
                else
                {
                    //sb.AppendFormat(" and HID in (SELECT    HID FROM      T_IPSFR_USER where BYORGNO='{0}')", PublicCls.getZhenIncOrgNo(sw.orgNo));
                }

            }
            if (!string.IsNullOrEmpty(sw.DateBegin))
            {
                sb.AppendFormat(" AND REPORTTIME>='{0} 00:00:00'", sw.DateBegin);
            }
            if (!string.IsNullOrEmpty(sw.DateEnd))
            {
                sb.AppendFormat(" AND REPORTTIME<='{0} 23:59:59'", sw.DateEnd);
            }

            sb.AppendFormat(" ORDER BY REPORTTIME DESC ");

            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取数据 联护林员
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>DataTable</returns>

        public static DataTable getDtUnionHUser(T_IPSRPT_REPORT_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT a.REPORTID, a.HID, a.SYSTYPEVALUE, a.ADDRESS, a.REPORTTIME, a.COLLECTNAME, a.MANSTATE, a.MANRESULT,a.MANTIME, a.MANUSERID");
            sb.AppendFormat(" FROM T_IPSRPT_REPORTDATA a");
            sb.AppendFormat("  left join T_IPSFR_USER b  on a.HID=b.HID");
            sb.AppendFormat(" WHERE   1=1");
            if (!string.IsNullOrEmpty(sw.HUserName))
            {
                sb.AppendFormat("  And b.HNAME like '%{0}%'", sw.HUserName);
            }

            if (!string.IsNullOrEmpty(sw.DateBegin))
            {
                sb.AppendFormat(" AND a.REPORTTIME>='{0} 00:00:00'", sw.DateBegin);
            }
            if (!string.IsNullOrEmpty(sw.DateEnd))
            {
                sb.AppendFormat(" AND a.REPORTTIME<='{0} 23:59:59'", sw.DateEnd);
            }

            sb.AppendFormat(" ORDER BY a.REPORTTIME DESC ");

            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 获取明细
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static DataTable getDetailDT(T_IPSRPT_REPORT_SW sw)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT REPORTDETAILID, REPORTID, LONGITUDE, LATITUDE, HEIGHT, DIRECTION, SBTIME");
            sb.AppendFormat(" FROM T_IPSRPT_DATADETAIL");
            sb.AppendFormat(" WHERE   1=1");
            //判断是否是多少，如果是多少，用in，如果只是一个，用=  主要考虑到速度
            if (string.IsNullOrEmpty(sw.REPORTID) == false)
            {
                if (sw.REPORTID.Split(',').Length > 1)
                    sb.AppendFormat(" AND REPORTID in({0})", ClsSql.EncodeSql(sw.REPORTID));
                else
                    sb.AppendFormat(" AND REPORTID ='{0}'", ClsSql.EncodeSql(sw.REPORTID));
            }
            sb.AppendFormat(" ORDER BY SBTIME DESC ");

            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 获取上传数据
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static DataTable getUploadDT(T_IPSRPT_REPORT_SW sw)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT  REPORTUPLOADID, REPORTID, UPLOADURL, UPLOADNAME, UPLOADDESCRIBE,UPLOADTYPE");
            sb.AppendFormat(" FROM T_IPSRPT_DATAUPLOAD");
            sb.AppendFormat(" WHERE   1=1");
            //判断是否是多少，如果是多少，用in，如果只是一个，用=  主要考虑到速度
            if (string.IsNullOrEmpty(sw.REPORTID) == false)
            {
                if (sw.REPORTID.Split(',').Length > 1)
                    sb.AppendFormat(" AND REPORTID in({0})", ClsSql.EncodeSql(sw.REPORTID));
                else
                    sb.AppendFormat(" AND REPORTID ='{0}'", ClsSql.EncodeSql(sw.REPORTID));
            }
            sb.AppendFormat(" ORDER BY REPORTUPLOADID DESC ");

            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        #endregion
    }
}
