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
    /// 上传管理
    /// </summary>
    public class T_IPSCOL_COLLECT
    {

        #region 上传管理
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(T_IPSCOL_COLLECT_SW sw)
        {
            if (string.IsNullOrEmpty(sw.COLLECTID))
                return new Message(false, "删除失败，请选择要处理的记录！", "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("DELETE FROM T_IPSCOL_COLLECTDATA");
            sb.AppendFormat(" where  COLLECTID= '{0}'", ClsSql.EncodeSql(sw.COLLECTID));

            bool bln = DataBaseClass.ExeSql(sb.ToString());
            DelDetail(new T_IPSCOL_COLLECT_SW { COLLECTID = sw.COLLECTID });
            DelUpload(new T_IPSCOL_COLLECT_SW { COLLECTID = sw.COLLECTID });
            if (bln == true)
                return new Message(true, "删除成功！", "");
            else
                return new Message(false, "删除失败，请检查各输入框是否正确！", "");
        }

        /// <summary>
        /// 增加采集明细坐标
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message AddDetail(T_IPSCOL_COLLECTDETAIL_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Insert into T_IPSCOL_DATADETAIL (COLLECTID,LONGITUDE,LATITUDE,COLLECTTIME) values({0},{1},{2},'{3}')", sw.COLLECTID, sw.LON, sw.LAT, sw.COLLECTTIME);
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "增加成功！", "");
            else
                return new Message(false, "增加失败！", "");
        }

        /// <summary>
        /// 删除明细
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message DelDetail(T_IPSCOL_COLLECT_SW sw)
        {
            if (string.IsNullOrEmpty(sw.COLLECTDETAILID) && string.IsNullOrEmpty(sw.COLLECTID))
                return new Message(false, "删除失败，请选择要处理的记录！", "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("DELETE FROM T_IPSCOL_DATADETAIL");
            sb.AppendFormat(" where 1=1");
            if (string.IsNullOrEmpty(sw.COLLECTDETAILID) == false)
                sb.AppendFormat(" and  COLLECTDETAILID= '{0}'", ClsSql.EncodeSql(sw.COLLECTDETAILID));
            if (string.IsNullOrEmpty(sw.COLLECTID) == false)
                sb.AppendFormat(" and  COLLECTID= '{0}'", ClsSql.EncodeSql(sw.COLLECTID));

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
        public static Message DelUpload(T_IPSCOL_COLLECT_SW sw)
        {
            if (string.IsNullOrEmpty(sw.COLLECTUPLOADID) || string.IsNullOrEmpty(sw.COLLECTID))
                return new Message(false, "删除失败，请选择要处理的记录！", "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("DELETE FROM T_IPSCOL_DATAUPLOAD");
            sb.AppendFormat(" where 1=1");
            if (string.IsNullOrEmpty(sw.COLLECTUPLOADID) == false)
                sb.AppendFormat(" and  COLLECTUPLOADID= '{0}'", ClsSql.EncodeSql(sw.COLLECTUPLOADID));
            if (string.IsNullOrEmpty(sw.COLLECTID) == false)
                sb.AppendFormat(" and  COLLECTID= '{0}'", ClsSql.EncodeSql(sw.COLLECTID));

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
        public static Message Man(T_IPSCOL_COLLECT_Model m)
        {
            if (string.IsNullOrEmpty(m.COLLECTID))
                return new Message(false, "修改失败，请选择要处理的记录！", "");
            StringBuilder sb = new StringBuilder();
            //(ORGNO,LOGINUSERNAME,USERNAME,USERPWD,REGISTERTIME,LOGINNUM,NOTE)
            sb.AppendFormat("UPDATE T_IPSCOL_COLLECTDATA");
            sb.AppendFormat(" set ");
            sb.AppendFormat(" COLLECTNAME='{0}'", ClsSql.EncodeSql(m.COLLECTNAME));
            sb.AppendFormat(" ,MANSTATE='{0}'", "1");
            sb.AppendFormat(" ,MANRESULT='{0}'", ClsSql.EncodeSql(m.MANRESULT));
            sb.AppendFormat(" ,MANTIME='{0}'", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sb.AppendFormat(" ,MANUSERID='{0}'", SystemCls.getUserID());
            sb.AppendFormat(" where  COLLECTID= '{0}'", ClsSql.EncodeSql(m.COLLECTID));

            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "处理成功！", "");
            else
                return new Message(false, "处理失败，请检查各输入框是否正确！", "");
        }

        #endregion


        #region 获取上报与护林员、单位组合查询
        /// <summary>
        /// 获取上报与护林员、单位组合查询 用于统计
        /// </summary>
        /// <param name="sw">参见模型 sw.TopORGNO 顶级单位编码 sw.DateBegin 开始日期 年月日 sw.DateEnd 结束日期 年月日</param>
        /// <returns>返回DataTable HID, SYSTYPEVALUE, REPORTTIME, HNAME,ORGNAME,ORGNO</returns>
        public static DataTable getDTByOrgHUse(T_IPSCOL_COLLECT_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT a.HID, a.SYSTYPEVALUE, a.COLLECTTIME, b.HNAME,c.ORGNAME,c.ORGNO");
            sb.AppendFormat(" FROM T_IPSCOL_COLLECTDATA a ");
            sb.AppendFormat(" left outer join T_IPSFR_USER b on a.hid=b.hid");
            sb.AppendFormat(" left outer join T_SYS_ORG c on b.BYORGNO=c.ORGNO");
            sb.AppendFormat(" WHERE 1=1");

            if (string.IsNullOrEmpty(sw.TopORGNO) == false)
            {
                if (sw.TopORGNO.Substring(4, 5) == "00000")//获取所有市的
                    sb.AppendFormat(" AND SUBSTRING(ORGNO,1,4) = '{0}'", ClsSql.EncodeSql(sw.TopORGNO.Substring(0, 4)));
                else if (sw.TopORGNO.Substring(6, 3) == "000")//获取所有县的
                    sb.AppendFormat(" AND SUBSTRING(ORGNO,1,6) = '{0}'", ClsSql.EncodeSql(sw.TopORGNO.Substring(0, 6)));
                else
                    sb.AppendFormat(" AND ORGNO = '{0}'", ClsSql.EncodeSql(sw.TopORGNO));
            }
            if (!string.IsNullOrEmpty(sw.DateBegin))
            {
                sb.AppendFormat(" AND COLLECTTIME>='{0} 00:00:00'", sw.DateBegin);
            }
            if (!string.IsNullOrEmpty(sw.DateEnd))
            {
                sb.AppendFormat(" AND COLLECTTIME<='{0} 23:59:59'", sw.DateEnd);
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
        /// <param name="SYSTYPEVALUE">上报类型ID</param>
        /// <returns>采集数量</returns>
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
                    return dt.Compute("count(HID)", "ORGNO='" + PublicCls.getZhenIncOrgNo(ORGNO) + "'").ToString();
                else
                    return dt.Compute("count(HID)", "ORGNO='" + PublicCls.getZhenIncOrgNo(ORGNO) + "' and SYSTYPEVALUE='" + SYSTYPEVALUE + "'").ToString();
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
        /// <returns>参见模型</returns>

        public static DataTable getDT(T_IPSCOL_COLLECT_SW sw)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT COLLECTID, HID, SYSTYPEVALUE, ADDRESS, COLLECTTIME, COLLECTNAME, MANSTATE, MANRESULT,                MANTIME, MANUSERID");
            sb.AppendFormat(" FROM T_IPSCOL_COLLECTDATA");
            sb.AppendFormat(" WHERE   1=1");
            //判断是否是多少，如果是多少，用in，如果只是一个，用=  主要考虑到速度
            if (string.IsNullOrEmpty(sw.COLLECTID) == false)
            {
                if (sw.COLLECTID.Split(',').Length > 1)
                    sb.AppendFormat(" AND COLLECTID in({0})", ClsSql.EncodeSql(sw.COLLECTID));
                else
                    sb.AppendFormat(" AND COLLECTID ='{0}'", ClsSql.EncodeSql(sw.COLLECTID));
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
                if (sw.orgNo.Substring(4, 5) == "00000")//获取所有县的
                    sb.AppendFormat(" AND HID in (SELECT    HID FROM      T_IPSFR_USER where BYORGNO like '{0}%')", ClsSql.EncodeSql(sw.orgNo.Substring(0, 4)));
                else if (sw.orgNo.Substring(6, 3) == "000")//获取所有市的
                    sb.AppendFormat(" AND HID in (SELECT    HID FROM      T_IPSFR_USER where BYORGNO like '{0}%')", ClsSql.EncodeSql(sw.orgNo.Substring(0, 6)));
                else
                    sb.AppendFormat(" AND HID in (SELECT    HID FROM      T_IPSFR_USER where BYORGNO = '{0}')", ClsSql.EncodeSql(sw.orgNo));
            }
            if (!string.IsNullOrEmpty(sw.DateBegin))
            {
                sb.AppendFormat(" AND COLLECTTIME>='{0} 00:00:00'", sw.DateBegin);
            }
            if (!string.IsNullOrEmpty(sw.DateEnd))
            {
                sb.AppendFormat(" AND COLLECTTIME<='{0} 23:59:59'", sw.DateEnd);
            }

            sb.AppendFormat(" ORDER BY COLLECTTIME DESC ");

            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }


        /// <summary>
        /// 获取数据 联护林员
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>DataTable</returns>

        public static DataTable getDtUnionHUser(T_IPSCOL_COLLECT_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT a.COLLECTID, a.HID, a.SYSTYPEVALUE, a.ADDRESS, a.COLLECTTIME, a.COLLECTNAME, a.MANSTATE, a.MANRESULT, a.MANTIME, a.MANUSERID");
            sb.AppendFormat(" FROM T_IPSCOL_COLLECTDATA a");
            sb.AppendFormat("  left join T_IPSFR_USER b  on a.HID=b.HID");
            sb.AppendFormat(" WHERE   1=1");
            if (!string.IsNullOrEmpty(sw.HUserName))
            {
                sb.AppendFormat("  And b.HNAME like '%{0}%'", sw.HUserName);
            }
            if (!string.IsNullOrEmpty(sw.SYSTYPEVALUE))
            {
                sb.AppendFormat(" AND a.SYSTYPEVALUE ='{0}'", ClsSql.EncodeSql(sw.SYSTYPEVALUE));
            }
            if (string.IsNullOrEmpty(sw.orgNo) == false)
            {
                if (sw.orgNo.Substring(4, 5) == "00000")//获取所有县的
                    sb.AppendFormat(" AND a.HID in (SELECT    HID FROM      T_IPSFR_USER where BYORGNO like '{0}%')", ClsSql.EncodeSql(sw.orgNo.Substring(0, 4)));
                else if (sw.orgNo.Substring(6, 3) == "000")//获取所有市的
                    sb.AppendFormat(" AND a.HID in (SELECT    HID FROM      T_IPSFR_USER where BYORGNO like '{0}%')", ClsSql.EncodeSql(sw.orgNo.Substring(0, 6)));
                else
                    sb.AppendFormat(" AND a.HID in (SELECT    HID FROM      T_IPSFR_USER where BYORGNO = '{0}')", ClsSql.EncodeSql(sw.orgNo));
            }
            if (!string.IsNullOrEmpty(sw.DateBegin))
            {
                sb.AppendFormat(" AND a.COLLECTTIME>='{0} 00:00:00'", sw.DateBegin);
            }
            if (!string.IsNullOrEmpty(sw.DateEnd))
            {
                sb.AppendFormat(" AND a.COLLECTTIME<='{0} 23:59:59'", sw.DateEnd);
            }

            sb.AppendFormat(" ORDER BY a.COLLECTTIME DESC ");
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 获取明细
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static DataTable getDetailDT(T_IPSCOL_COLLECT_SW sw)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT COLLECTDETAILID, COLLECTID, LONGITUDE, LATITUDE, HEIGHT, DIRECTION, COLLECTTIME");
            sb.AppendFormat(" FROM T_IPSCOL_DATADETAIL");
            sb.AppendFormat(" WHERE   1=1");
            //判断是否是多少，如果是多少，用in，如果只是一个，用=  主要考虑到速度
            if (string.IsNullOrEmpty(sw.COLLECTID) == false)
            {
                if (sw.COLLECTID.Split(',').Length > 1)
                    sb.AppendFormat(" AND COLLECTID in({0})", ClsSql.EncodeSql(sw.COLLECTID));
                else
                    sb.AppendFormat(" AND COLLECTID ='{0}'", ClsSql.EncodeSql(sw.COLLECTID));
            }
            sb.AppendFormat(" ORDER BY COLLECTTIME DESC ");

            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 获取上传数据
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static DataTable getUploadDT(T_IPSCOL_COLLECT_SW sw)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT  COLLECTUPLOADID, COLLECTID, UPLOADURL, UPLOADNAME, UPLOADDESCRIBE");
            sb.AppendFormat(" FROM T_IPSCOL_DATAUPLOAD");
            sb.AppendFormat(" WHERE   1=1");
            //判断是否是多少，如果是多少，用in，如果只是一个，用=  主要考虑到速度
            if (string.IsNullOrEmpty(sw.COLLECTID) == false)
            {
                if (sw.COLLECTID.Split(',').Length > 1)
                    sb.AppendFormat(" AND COLLECTID in({0})", ClsSql.EncodeSql(sw.COLLECTID));
                else
                    sb.AppendFormat(" AND COLLECTID ='{0}'", ClsSql.EncodeSql(sw.COLLECTID));
            }
            sb.AppendFormat(" ORDER BY COLLECTUPLOADID DESC ");

            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        #endregion
    }
}
