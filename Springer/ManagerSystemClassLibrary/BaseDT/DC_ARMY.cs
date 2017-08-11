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
    /// 数据中心_队伍
    /// </summary>
    public class DC_ARMY
    {
        #region 添加
        /// <summary>
        /// 添加返回当前记录的id
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message Add(DC_ARMY_Model m)
        {
            if (DC_ARMY.isExistsPoint(new DC_ARMY_Model { JD = m.JD, WD = m.WD }) == false)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("INSERT INTO  DC_ARMY( ARMYTYPE, NUMBER, NAME, BYORGNO, ARMYMEMBERCOUNT, ARMYLEADER, CONTACTS,ARMYCHARACTER, ARMYEQUIP, MARK, JD, WD) output inserted.DC_ARMY_ID");
                sb.AppendFormat(" VALUES(");
                sb.AppendFormat("{0}", ClsSql.saveNullField(m.ARMYTYPE));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.NUMBER));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.NAME));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.BYORGNO));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.ARMYMEMBERCOUNT));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.ARMYLEADER));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.CONTACTS));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.ARMYCHARACTER));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.ARMYEQUIP));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.MARK));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.JD));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.WD));
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
            else 
            {
                return new Message(false, "添加失败,已有相同的位置的队伍！", "");
            }
        }
   

        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Mdy(DC_ARMY_Model m)
        {
            StringBuilder sb = new StringBuilder();
            //HID, HNAME, SN, PHONE, SEX, BIRTH, ONSTATE, BYORGNO
            sb.AppendFormat("UPDATE DC_ARMY");
            sb.AppendFormat(" set ");
            sb.AppendFormat("ARMYTYPE={0}", ClsSql.saveNullField(m.ARMYTYPE));
            sb.AppendFormat(",NUMBER={0}", ClsSql.saveNullField(m.NUMBER));
            sb.AppendFormat(",NAME={0}", ClsSql.saveNullField(m.NAME));
            sb.AppendFormat(",BYORGNO={0}", ClsSql.saveNullField(m.BYORGNO));
            sb.AppendFormat(",ARMYMEMBERCOUNT={0}", ClsSql.saveNullField(m.ARMYMEMBERCOUNT));
            sb.AppendFormat(",ARMYLEADER={0}", ClsSql.saveNullField(m.ARMYLEADER));
            sb.AppendFormat(",CONTACTS={0}", ClsSql.saveNullField(m.CONTACTS));
            sb.AppendFormat(",ARMYCHARACTER={0}", ClsSql.saveNullField(m.ARMYCHARACTER));
            sb.AppendFormat(",ARMYEQUIP={0}", ClsSql.saveNullField(m.ARMYEQUIP));
            sb.AppendFormat(",MARK={0}", ClsSql.saveNullField(m.MARK));
            sb.AppendFormat(",JD={0}", ClsSql.saveNullField(m.JD));
            sb.AppendFormat(",WD={0}", ClsSql.saveNullField(m.WD));
            sb.AppendFormat(" where DC_ARMY_ID= '{0}'", ClsSql.EncodeSql(m.DC_ARMY_ID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功！", m.DC_ARMY_ID);
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
        public static Message MdyJWD(DC_ARMY_Model m)
        {
            StringBuilder sb = new StringBuilder();
            //HID, HNAME, SN, PHONE, SEX, BIRTH, ONSTATE, BYORGNO
            sb.AppendFormat("UPDATE DC_ARMY");
            sb.AppendFormat(" set ");
            sb.AppendFormat("JD={0}", ClsSql.saveNullField(m.JD));
            sb.AppendFormat(",WD={0}", ClsSql.saveNullField(m.WD));
            sb.AppendFormat(" where DC_ARMY_ID= '{0}'", ClsSql.EncodeSql(m.DC_ARMY_ID));
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
        public static Message Del(DC_ARMY_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete DC_ARMY");
            sb.AppendFormat(" where DC_ARMY_ID= '{0}'", ClsSql.EncodeSql(m.DC_ARMY_ID));
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
        public static bool isExists(DC_ARMY_SW sw)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from DC_ARMY where 1=1");
            if (string.IsNullOrEmpty(sw.DC_ARMY_ID) == false)
                sb.AppendFormat(" where DC_ARMY_ID= '{0}'", ClsSql.EncodeSql(sw.DC_ARMY_ID));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }

        #endregion

        #region 判断是否相同坐标
        /// <summary>
        /// 判断是否相同坐标
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static bool isExistsPoint(DC_ARMY_Model m)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from DC_ARMY where 1=1");
            if (string.IsNullOrEmpty(m.JD) == false && string.IsNullOrEmpty(m.WD) == false)
                sb.AppendFormat(" and JD='{0}'", ClsSql.EncodeSql(m.JD));
            sb.AppendFormat(" and WD='{0}'", ClsSql.EncodeSql(m.WD));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion

        #region 获取记录
        /// <summary>
        /// 获取记录
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(DC_ARMY_SW sw)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat(" FROM      DC_ARMY");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.DC_ARMY_ID) == false)
                sb.AppendFormat(" AND DC_ARMY_ID = '{0}'", ClsSql.EncodeSql(sw.DC_ARMY_ID));
            if (string.IsNullOrEmpty(sw.ARMYTYPE) == false)
                sb.AppendFormat(" AND ARMYTYPE = '{0}'", ClsSql.EncodeSql(sw.ARMYTYPE));
            if (string.IsNullOrEmpty(sw.NUMBER) == false)
                sb.AppendFormat(" AND NUMBER like '%{0}%'", ClsSql.EncodeSql(sw.NUMBER));
            if (string.IsNullOrEmpty(sw.NAME) == false)
                sb.AppendFormat(" AND NAME like '%{0}%'", ClsSql.EncodeSql(sw.NAME));
            if (!string.IsNullOrEmpty(sw.BYORGNO))
            {
                if (sw.ORGNOSXZ != "1")
                {
                    if (sw.BYORGNO.Substring(4, 11) == "00000000000")//获取所有市的
                        sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,4) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
                    else if (sw.BYORGNO.Substring(6, 9) == "000000000")//获取所有县的
                        sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,6) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
                    else if (sw.BYORGNO.Substring(9, 6) == "000000")//获取说有乡镇的
                        sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,9) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 9)));
                    else if (sw.BYORGNO.Substring(12, 3) == "000")//获取说有村的
                        sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,12) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 12)));
                    else
                        sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
                }
                else
                {
                    sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
                }
            }
            string sql = "SELECT DC_ARMY_ID, ARMYTYPE, NUMBER, NAME, BYORGNO, ARMYMEMBERCOUNT, ARMYLEADER, CONTACTS,ARMYCHARACTER, ARMYEQUIP, MARK, JD, WD"
                + sb.ToString()
                + " order by BYORGNO,ARMYTYPE";

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
        public static DataTable getDT(DC_ARMY_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendFormat(" FROM      DC_ARMY");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.DC_ARMY_ID) == false)
                sb.AppendFormat(" AND DC_ARMY_ID = '{0}'", ClsSql.EncodeSql(sw.DC_ARMY_ID));
            if (string.IsNullOrEmpty(sw.ARMYTYPE) == false)
                sb.AppendFormat(" AND ARMYTYPE = '{0}'", ClsSql.EncodeSql(sw.ARMYTYPE));
            if (string.IsNullOrEmpty(sw.NUMBER) == false)
                sb.AppendFormat(" AND NUMBER like '%{0}%'", ClsSql.EncodeSql(sw.NUMBER));
            if (string.IsNullOrEmpty(sw.NAME) == false)
                sb.AppendFormat(" AND NAME like '%{0}%'", ClsSql.EncodeSql(sw.NAME));
            if (!string.IsNullOrEmpty(sw.BYORGNO))
            {
                if (sw.BYORGNO.Substring(4, 11) == "00000000000")//获取所有市的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,4) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
                else if (sw.BYORGNO.Substring(4, 11) == "xxxxxxxxxxx")//单独市
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,15) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4) + "00000000000"));
                else if (sw.BYORGNO.Substring(6, 9) == "xxxxxxxxx")//获取所有县的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,6) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
                else if (sw.BYORGNO.Substring(9, 6) == "000000")//获取说有乡镇的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,9) = '{0}'or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 9)));
                else if (sw.BYORGNO.Substring(12, 3) == "000")//获取说有村的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,12) = '{0}'or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 12)));
                else
                    sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            }
            string sql = "SELECT DC_ARMY_ID, ARMYTYPE, NUMBER, NAME, BYORGNO, ARMYMEMBERCOUNT, ARMYLEADER, CONTACTS,ARMYCHARACTER, ARMYEQUIP, MARK, JD, WD"
                + sb.ToString()
                + " order by BYORGNO,ARMYTYPE,NUMBER,DC_ARMY_ID desc";
            string sqlC = "select count(1) " + sb.ToString();
            total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
            sw.curPage = PagerCls.getCurPage(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
            DataSet ds = DataBaseClass.FullDataSet(sql, (sw.curPage - 1) * sw.pageSize, sw.pageSize, "a");
            return ds.Tables[0];
        }

        #endregion

        #region 根据DataTable和OrgNo和队伍类型判断记录个数
        /// <summary>
        /// 根据DataTable和OrgNo和队伍类型判断记录个数
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="orgNo">单位编码</param>
        /// /// <param name="DICTVALUE">队伍类型</param>
        /// <returns>记录个数</returns>
        public static string getCountarmyByOrgNo(DataTable dt, string orgNo, string DICTVALUE)
        {
            if (string.IsNullOrEmpty(orgNo))//返回所有记录个数
                return dt.Rows.Count.ToString();
            if (PublicCls.OrgIsShi(orgNo))//市
            {
                if (string.IsNullOrEmpty(DICTVALUE))
                    return dt.Compute("count(DC_ARMY_ID)", "substring(BYORGNO,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "'").ToString();
                else
                    return dt.Compute("count(DC_ARMY_ID)", "substring(BYORGNO,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "' and ARMYTYPE='" + DICTVALUE + "'").ToString();
            }
            else if (PublicCls.OrgIsXian(orgNo))//县
            {
                if (string.IsNullOrEmpty(DICTVALUE))
                    return dt.Compute("count(DC_ARMY_ID)", "substring(BYORGNO,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "'").ToString();
                else
                    return dt.Compute("count(DC_ARMY_ID)", "substring(BYORGNO,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "' and ARMYTYPE='" + DICTVALUE + "'").ToString();
            }
            else if (PublicCls.OrgIsZhen(orgNo))
            {
                if (string.IsNullOrEmpty(DICTVALUE))
                    return dt.Compute("count(DC_ARMY_ID)", "substring(BYORGNO,1,9)='" + PublicCls.getZhenIncOrgNo(orgNo) + "'").ToString();
                else
                    return dt.Compute("count(DC_ARMY_ID)", "substring(BYORGNO,1,9)='" + PublicCls.getZhenIncOrgNo(orgNo) + "' and ARMYTYPE='" + DICTVALUE + "'").ToString();
            }
            else //机构编码可能不正确
                return "";
        }
        #endregion

        #region 根据DataTable和队伍类型判断县市的记录个数
        /// <summary>
        /// 根据DataTable和OrgNo和队伍类型判断记录个数
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="orgNo">单位编码</param>
        /// /// <param name="DICTVALUE">队伍类型</param>
        /// <returns>记录个数</returns>
        public static string getCountXSByOrgNo(DataTable dt, string orgNo, string DICTVALUE)
        {
            if (string.IsNullOrEmpty(orgNo))//返回所有记录个数
                return dt.Rows.Count.ToString();
            if (PublicCls.OrgIsZhen(orgNo) == false)
            {
                string result = "";
                if (string.IsNullOrEmpty(DICTVALUE))
                    result=dt.Compute("count(DC_ARMY_ID)", "BYORGNO='" + orgNo + "'").ToString();
               else
                result =dt.Compute("count(DC_ARMY_ID)", "BYORGNO='" + orgNo + "' and ARMYTYPE='" + DICTVALUE + "'").ToString();
                return result;
            }
            else //机构编码可能不正确
                return "";
        }
        #endregion
       
        #region 根据DataTable和OrgNo获取队伍个数
        /// <summary>
        /// 统计队伍人数
        /// </summary>
        /// <param name="dt">dt</param>
        /// <param name="orgNo">orgNo</param>
        /// <param name="DICTVALUE">DICTVALUE</param>
        /// <returns></returns>
        public static string getMCount(DataTable dt, string orgNo, string DICTVALUE)
        {
            if (string.IsNullOrEmpty(orgNo))//返回所有记录个数
                return dt.Rows.Count.ToString();
            
                if (PublicCls.OrgIsShi(orgNo))//市
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("Sum(ARMYMEMBERCOUNT)", "substring(BYORGNO,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("Sum(ARMYMEMBERCOUNT)", "substring(BYORGNO,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "' and ARMYTYPE='" + DICTVALUE + "'").ToString();
                }
                else if (PublicCls.OrgIsXian(orgNo))//县
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("Sum(ARMYMEMBERCOUNT)", "substring(BYORGNO,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("Sum(ARMYMEMBERCOUNT)", "substring(BYORGNO,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "' and ARMYTYPE='" + DICTVALUE + "'").ToString();
                }
                else if (PublicCls.OrgIsZhen(orgNo))
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("Sum(ARMYMEMBERCOUNT)", "substring(BYORGNO,1,9)='" + PublicCls.getZhenIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("Sum(ARMYMEMBERCOUNT)", "substring(BYORGNO,1,9)='" + PublicCls.getZhenIncOrgNo(orgNo) + "' and ARMYTYPE='" + DICTVALUE + "'").ToString();
                }

                else //机构编码可能不正确
                    return "";
            
        }
        #endregion

        #region 根据DataTable和OrgNo县市获取队伍个数
        /// <summary>
        /// 统计队伍人数
        /// </summary>
        /// <param name="dt">dt</param>
        /// <param name="orgNo">orgNo</param>
        /// <param name="DICTVALUE">DICTVALUE</param>
        /// <returns></returns>
        public static string getMXSCount(DataTable dt, string orgNo, string DICTVALUE)
        {
            if (string.IsNullOrEmpty(orgNo))//返回所有记录个数
                return dt.Rows.Count.ToString();

             if (PublicCls.OrgIsZhen(orgNo)==false)
            {
                if (string.IsNullOrEmpty(DICTVALUE))
                    return dt.Compute("Sum(ARMYMEMBERCOUNT)", "BYORGNO='" + orgNo + "'").ToString();
                else
                    return dt.Compute("Sum(ARMYMEMBERCOUNT)", "BYORGNO='" + orgNo + "' and ARMYTYPE='" + DICTVALUE + "'").ToString();
            }

            else //机构编码可能不正确
                return "";

        }
        #endregion

        #region 统计当前用户下队伍的数量
        /// <summary>
        /// 统计当前用户下队伍的数量
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getNum(DC_ARMY_SW sw)
        {
            string total = "";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("    from    DC_ARMY a ");
            sb.AppendFormat("where 1 = 1 ");
            if (sw.BYORGNO.Substring(4, 11) == "00000000000")//获取所有市的
                sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,4) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
            else if (sw.BYORGNO.Substring(6, 9) == "000000000")//获取所有县的
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
