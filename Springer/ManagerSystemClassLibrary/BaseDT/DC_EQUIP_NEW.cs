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
    /// 数据中心_装备_新
    /// </summary>
    public class DC_EQUIP_NEW
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(DC_EQUIP_NEW_Model m)
        {
            
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("INSERT INTO  DC_EQUIP_NEW( EQUIPTYPE, NUMBER, NAME, BYORGNO, MODEL, BUYYEAR, USESTATE, STOREADDR, MARK, JD, WD,WORTH,EQUIPAMOUNT,REPID,DCSUPPROPUNIT,PRICE) output inserted.DC_EQUIP_NEW_ID");
                sb.AppendFormat(" VALUES(");
                sb.AppendFormat("{0}", ClsSql.saveNullField(m.EQUIPTYPE));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.NUMBER));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.NAME));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.BYORGNO));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.MODEL));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.BUYYEAR));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.USESTATE));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.STOREADDR));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.MARK));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.JD));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.WD));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.WORTH));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.EQUIPAMOUNT));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.REPID));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.DCSUPPROPUNIT));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.PRICE));
                sb.AppendFormat(")");
                try
                {
                    string strid = DataBaseClass.ReturnSqlField(sb.ToString());
                    return new Message(true, "添加成功！", strid);
                }
                catch (Exception)
                {

                    throw;
                }
            
            
        }

        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Mdy(DC_EQUIP_NEW_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE DC_EQUIP_NEW");
            sb.AppendFormat(" set ");
            sb.AppendFormat("EQUIPTYPE={0}", ClsSql.saveNullField(m.EQUIPTYPE));
            sb.AppendFormat(",NUMBER={0}", ClsSql.saveNullField(m.NUMBER));
            sb.AppendFormat(",NAME={0}", ClsSql.saveNullField(m.NAME));
            sb.AppendFormat(",BYORGNO={0}", ClsSql.saveNullField(m.BYORGNO));
            sb.AppendFormat(",MODEL={0}", ClsSql.saveNullField(m.MODEL));
            sb.AppendFormat(",BUYYEAR={0}", ClsSql.saveNullField(m.BUYYEAR));
            sb.AppendFormat(",USESTATE={0}", ClsSql.saveNullField(m.USESTATE));
            sb.AppendFormat(",STOREADDR={0}", ClsSql.saveNullField(m.STOREADDR));
            sb.AppendFormat(",MARK={0}", ClsSql.saveNullField(m.MARK));
            sb.AppendFormat(",JD={0}", ClsSql.saveNullField(m.JD));
            sb.AppendFormat(",WD={0}", ClsSql.saveNullField(m.WD));
            sb.AppendFormat(",WORTH={0}", ClsSql.saveNullField(m.WORTH));
            sb.AppendFormat(",EQUIPAMOUNT={0}", ClsSql.saveNullField(m.EQUIPAMOUNT));
            sb.AppendFormat(",REPID={0}", ClsSql.saveNullField(m.REPID));
            sb.AppendFormat(",DCSUPPROPUNIT={0}", ClsSql.saveNullField(m.DCSUPPROPUNIT));
            sb.AppendFormat(",PRICE={0}", ClsSql.saveNullField(m.PRICE));
            sb.AppendFormat(" where DC_EQUIP_NEW_ID= '{0}'", ClsSql.EncodeSql(m.DC_EQUIP_NEW_ID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功！", m.DC_EQUIP_NEW_ID);
            else
                return new Message(false, "修改失败，请检查各输入框是否正确！", "");
        }

        #endregion

        #region 随着同步更新
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message MdyCount(DC_EQUIP_NEW_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE DC_EQUIP_NEW");
            sb.AppendFormat(" set ");
            sb.AppendFormat(" WORTH={0}", ClsSql.saveNullField(m.WORTH));
            sb.AppendFormat(",EQUIPAMOUNT={0}", ClsSql.saveNullField(m.EQUIPAMOUNT));
            sb.AppendFormat(",REPID={0} ", ClsSql.saveNullField(m.REPID));
            sb.AppendFormat(" where DC_EQUIP_NEW_ID= '{0}'", ClsSql.EncodeSql(m.DC_EQUIP_NEW_ID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功！", m.DC_EQUIP_NEW_ID);
            else
                return new Message(false, "修改失败，请检查各输入框是否正确！", "");
        }

        #endregion

        #region 修改经纬度
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message MdyJWD(DC_EQUIP_NEW_Model m)
        {
            StringBuilder sb = new StringBuilder();
            //HID, HNAME, SN, PHONE, SEX, BIRTH, ONSTATE, BYORGNO
            sb.AppendFormat("UPDATE DC_EQUIP_NEW");
            sb.AppendFormat(" set ");
            sb.AppendFormat("JD={0}", ClsSql.saveNullField(m.JD));
            sb.AppendFormat(",WD={0}", ClsSql.saveNullField(m.WD));
            sb.AppendFormat(" where DC_EQUIP_NEW_ID= '{0}'", ClsSql.EncodeSql(m.DC_EQUIP_NEW_ID));
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
        public static Message Del(DC_EQUIP_NEW_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete DC_EQUIP_NEW");
            sb.AppendFormat(" where DC_EQUIP_NEW_ID= '{0}'", ClsSql.EncodeSql(m.DC_EQUIP_NEW_ID));
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
        public static bool isExists(DC_EQUIP_NEW_SW sw)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from DC_EQUIP_NEW where 1=1");
            if (string.IsNullOrEmpty(sw.DC_EQUIP_NEW_ID) == false)
                sb.AppendFormat(" where DC_EQUIP_NEW_ID= '{0}'", ClsSql.EncodeSql(sw.DC_EQUIP_NEW_ID));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }

        #endregion
    
        #region 获取记录
        /// <summary>
        /// 获取记录
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(DC_EQUIP_NEW_SW sw)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat(" FROM      DC_EQUIP_NEW");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.DC_EQUIP_NEW_ID) == false)
                sb.AppendFormat(" AND DC_EQUIP_NEW_ID = '{0}'", ClsSql.EncodeSql(sw.DC_EQUIP_NEW_ID));
            if (string.IsNullOrEmpty(sw.EQUIPTYPE) == false)
                sb.AppendFormat(" AND EQUIPTYPE = '{0}'", ClsSql.EncodeSql(sw.EQUIPTYPE));
            if (string.IsNullOrEmpty(sw.NUMBER) == false)
                sb.AppendFormat(" AND NUMBER like '%{0}%'", ClsSql.EncodeSql(sw.NUMBER));
            if (string.IsNullOrEmpty(sw.NAME) == false)
                sb.AppendFormat(" AND NAME like '%{0}%'", ClsSql.EncodeSql(sw.NAME));
            if (string.IsNullOrEmpty(sw.MODEL) == false)
                sb.AppendFormat(" AND MODEL like '%{0}%'", ClsSql.EncodeSql(sw.MODEL));
            if (string.IsNullOrEmpty(sw.USESTATE) == false)
                sb.AppendFormat(" AND USESTATE = '{0}'", ClsSql.EncodeSql(sw.USESTATE));
            if (!string.IsNullOrEmpty(sw.BYORGNO))
            {
                if (sw.ORGNOSXZ != "1")
                {
                    if (sw.BYORGNO.Substring(4, 11) == "00000000000")//获取所有市的
                        sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,4) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
                    else if (sw.BYORGNO.Substring(6, 9) == "000000000")//获取所有县的
                        sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,6) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
                    else if (sw.BYORGNO.Substring(9, 6) == "000000")//获取说有乡镇的
                        sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,9) = '{0}' or BYORGNO is null or BYORGNO=''", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 9)));
                    else if (sw.BYORGNO.Substring(12, 3) == "000")//获取说有村的
                        sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,12) = '{0}' or BYORGNO is null or BYORGNO=''", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 12)));
                    else
                        sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
                }
                else 
                {
                    sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
                }
            }
            string sql = "SELECT DC_EQUIP_NEW_ID, EQUIPTYPE, NUMBER, NAME, BYORGNO, MODEL, BUYYEAR, USESTATE,STOREADDR, MARK, JD, WD,WORTH,EQUIPAMOUNT,REPID,DCSUPPROPUNIT,PRICE"
                + sb.ToString()
                + " order by BYORGNO";

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
        public static DataTable getDT(DC_EQUIP_NEW_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendFormat(" FROM      DC_EQUIP_NEW");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.DC_EQUIP_NEW_ID) == false)
                sb.AppendFormat(" AND DC_EQUIP_NEW_ID = '{0}'", ClsSql.EncodeSql(sw.DC_EQUIP_NEW_ID));
            if (string.IsNullOrEmpty(sw.EQUIPTYPE) == false)
                sb.AppendFormat(" AND EQUIPTYPE = '{0}'", ClsSql.EncodeSql(sw.EQUIPTYPE));
            if (string.IsNullOrEmpty(sw.NUMBER) == false)
                sb.AppendFormat(" AND NUMBER like '%{0}%'", ClsSql.EncodeSql(sw.NUMBER));
            if (string.IsNullOrEmpty(sw.NAME) == false)
                sb.AppendFormat(" AND NAME like '%{0}%'", ClsSql.EncodeSql(sw.NAME));
            if (string.IsNullOrEmpty(sw.MODEL) == false)
                sb.AppendFormat(" AND MODEL like '%{0}%'", ClsSql.EncodeSql(sw.MODEL));
            if (string.IsNullOrEmpty(sw.USESTATE) == false)
                sb.AppendFormat(" AND USESTATE = '{0}'", ClsSql.EncodeSql(sw.USESTATE));
            if (!string.IsNullOrEmpty(sw.BYORGNO))
            {
                if (sw.BYORGNO.Substring(4, 11) == "00000000000")//获取所有市的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,4) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
                else if (sw.BYORGNO.Substring(4, 11) == "xxxxxxxxxxx")//单独市
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,15) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4) + "00000000000"));
                else if (sw.BYORGNO.Substring(6, 9) == "xxxxxxxxx")//获取所有县的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,6) = '{0}' )", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
                else
                    sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            }
            string sql = "SELECT DC_EQUIP_NEW_ID, EQUIPTYPE, NUMBER, NAME, BYORGNO, MODEL, BUYYEAR, USESTATE,STOREADDR, MARK, JD, WD,WORTH,EQUIPAMOUNT,REPID,DCSUPPROPUNIT,PRICE"
                + sb.ToString()
                + " order by BYORGNO,DC_EQUIP_NEW_ID desc";
            string sqlC = "select count(1) " + sb.ToString();
            total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
            sw.curPage = PagerCls.getCurPage(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
            DataSet ds = DataBaseClass.FullDataSet(sql, (sw.curPage - 1) * sw.pageSize, sw.pageSize, "a");
            return ds.Tables[0];
        }

        #endregion

        #region 根据DataTable和OrgNo和装备的各个类型判断记录个数
        /// <summary>
        /// 根据DataTable和OrgNo和装备类型判断记录个数
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="orgNo">单位编码</param>
        /// <param name="DICTVALUE">装备的各个类型</param>
        /// /// <param name="TYPE">确定统计那个装备的类型</param>
        /// <returns>记录个数</returns>
        public static string getCountEquipByOrgNo(DataTable dt, string orgNo, string DICTVALUE, string TYPE)
        {
            if (string.IsNullOrEmpty(orgNo))//返回所有记录个数
                return dt.Rows.Count.ToString();
            if (TYPE == "1")//统计装备类型
            {
                if (PublicCls.OrgIsShi(orgNo))//市
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_EQUIP_NEW_ID)", "substring(BYORGNO,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("count(DC_EQUIP_NEW_ID)", "substring(BYORGNO,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "' and EQUIPTYPE='" + DICTVALUE + "'").ToString();
                }
                else if (PublicCls.OrgIsXian(orgNo))//县
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_EQUIP_NEW_ID)", "substring(BYORGNO,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("count(DC_EQUIP_NEW_ID)", "substring(BYORGNO,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "' and EQUIPTYPE='" + DICTVALUE + "'").ToString();
                }
                else if (PublicCls.OrgIsZhen(orgNo))
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_EQUIP_NEW_ID)", "BYORGNO='" + PublicCls.getZhenIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("count(DC_EQUIP_NEW_ID)", "BYORGNO='" + PublicCls.getZhenIncOrgNo(orgNo) + "' and EQUIPTYPE='" + DICTVALUE + "'").ToString();
                }
                else //机构编码可能不正确
                    return "";
            }
            if (TYPE == "2")//统计使用现状
            {
                if (PublicCls.OrgIsShi(orgNo))//市
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_EQUIP_NEW_ID)", "substring(BYORGNO,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("count(DC_EQUIP_NEW_ID)", "substring(BYORGNO,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "' and USESTATE='" + DICTVALUE + "'").ToString();
                }
                else if (PublicCls.OrgIsXian(orgNo))//县
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_EQUIP_NEW_ID)", "substring(BYORGNO,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("count(DC_EQUIP_NEW_ID)", "substring(BYORGNO,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "' and USESTATE='" + DICTVALUE + "'").ToString();
                }
                else if (PublicCls.OrgIsZhen(orgNo))
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_EQUIP_NEW_ID)", "BYORGNO='" + PublicCls.getZhenIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("count(DC_EQUIP_NEW_ID)", "BYORGNO='" + PublicCls.getZhenIncOrgNo(orgNo) + "' and USESTATE='" + DICTVALUE + "'").ToString();
                }
                else //机构编码可能不正确
                    return "";
            }
            else
                return "";
        }
        #endregion

        #region 根据DataTable和OrgNo和装备的县市各个类型判断记录个数
        /// <summary>
        /// 根据DataTable和OrgNo和装备类型判断记录个数
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="orgNo">单位编码</param>
        /// <param name="DICTVALUE">装备的各个类型</param>
        /// /// <param name="TYPE">确定统计那个装备的类型</param>
        /// <returns>记录个数</returns>
        public static string getCountXSByOrgNo(DataTable dt, string orgNo, string DICTVALUE, string TYPE)
        {
            if (string.IsNullOrEmpty(orgNo))//返回所有记录个数
                return dt.Rows.Count.ToString();
            if (TYPE == "1")//统计装备类型
            {
                 if (PublicCls.OrgIsZhen(orgNo)==false)
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_EQUIP_NEW_ID)", "BYORGNO='" + orgNo + "'").ToString();
                    else
                        return dt.Compute("count(DC_EQUIP_NEW_ID)", "BYORGNO='" + orgNo + "' and EQUIPTYPE='" + DICTVALUE + "'").ToString();
                }
                else //机构编码可能不正确
                    return "";
            }
            if (TYPE == "2")//统计使用现状
            {
                 if (PublicCls.OrgIsZhen(orgNo)==false)
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_EQUIP_NEW_ID)", "BYORGNO='" + orgNo + "'").ToString();
                    else
                        return dt.Compute("count(DC_EQUIP_NEW_ID)", "BYORGNO='" + orgNo + "' and USESTATE='" + DICTVALUE + "'").ToString();
                }
                else //机构编码可能不正确
                    return "";
            }
            else
                return "";
        }
        #endregion

        #region 统计当前用户下装备的记录数量
        /// <summary>
        /// 统计当前用户下装备的记录数量
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getNum(DC_EQUIP_NEW_SW sw)
        {
            string total = "";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("    from    DC_EQUIP_NEW a ");
            sb.AppendFormat("where 1 = 1 ");
            if (sw.BYORGNO.Substring(4, 5) == "00000")//获取所有市的
                sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,4) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
            else if (sw.BYORGNO.Substring(6, 3) == "000")//获取所有县的
                sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,6) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
            else
                sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            string sqlC = "select count(1) " + sb.ToString();
            total = (DataBaseClass.ReturnSqlField(sqlC)).ToString();
            return total;
        }
        #endregion

    }
}
