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
    /// 数据中心_设施_中继站
    /// </summary>
    public class DC_UTILITY_RELAY
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(DC_UTILITY_RELAY_Model m)
        {
            if (DC_UTILITY_RELAY.isExistsPoint(new DC_UTILITY_RELAY_Model { JD = m.JD, WD = m.WD }) == false)
            {
                StringBuilder sb = new StringBuilder();
                //sb.AppendFormat("INSERT INTO  DC_UTILITY_RELAY(NUMBER, NAME, ADDRESS, MODEL, BYORGNO, COMMUNICATIONWAY,BUILDDATE, USESTATE, MANAGERSTATE, MARK, JD, WD)");
                sb.AppendFormat("INSERT INTO  DC_UTILITY_RELAY( NUMBER, NAME, ADDRESS, MODEL, BYORGNO, COMMUNICATIONWAY,BUILDDATE, USESTATE, MANAGERSTATE, MARK, JD, WD,WORTH,BUILDDATEBEGIN,BUILDDATEEND) output inserted.DC_UTILITY_RELAY_ID");
                sb.AppendFormat(" VALUES(");
                sb.AppendFormat("{0}", ClsSql.saveNullField(m.NUMBER));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.NAME));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.ADDRESS));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.MODEL));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.BYORGNO));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.COMMUNICATIONWAY));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.BUILDDATE));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.USESTATE));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.MANAGERSTATE));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.MARK));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.JD));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.WD));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.WORTH));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.BUILDDATEBEGIN));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.BUILDDATEEND));
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
            else { return new Message(false, "添加失败,已有相同的位置的中继站！", ""); }
        }

        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Mdy(DC_UTILITY_RELAY_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE DC_UTILITY_RELAY");
            sb.AppendFormat(" set ");
            sb.AppendFormat("NUMBER={0}", ClsSql.saveNullField(m.NUMBER));
            sb.AppendFormat(",NAME={0}", ClsSql.saveNullField(m.NAME));
            sb.AppendFormat(",ADDRESS={0}", ClsSql.saveNullField(m.ADDRESS));
            sb.AppendFormat(",MODEL={0}", ClsSql.saveNullField(m.MODEL));
            sb.AppendFormat(",BYORGNO={0}", ClsSql.saveNullField(m.BYORGNO));
            sb.AppendFormat(",COMMUNICATIONWAY={0}", ClsSql.saveNullField(m.COMMUNICATIONWAY));
            sb.AppendFormat(",BUILDDATE={0}", ClsSql.saveNullField(m.BUILDDATE));
            sb.AppendFormat(",USESTATE={0}", ClsSql.saveNullField(m.USESTATE));
            sb.AppendFormat(",MANAGERSTATE={0}", ClsSql.saveNullField(m.MANAGERSTATE));
            sb.AppendFormat(",MARK={0}", ClsSql.saveNullField(m.MARK));
            sb.AppendFormat(",JD={0}", ClsSql.saveNullField(m.JD));
            sb.AppendFormat(",WD={0}", ClsSql.saveNullField(m.WD));
            sb.AppendFormat(",WORTH={0}", ClsSql.saveNullField(m.WORTH));
            sb.AppendFormat(",BUILDDATEBEGIN={0}", ClsSql.saveNullField(m.BUILDDATEBEGIN));
            sb.AppendFormat(",BUILDDATEEND={0}", ClsSql.saveNullField(m.BUILDDATEEND));
            sb.AppendFormat(" where DC_UTILITY_RELAY_ID= '{0}'", ClsSql.EncodeSql(m.DC_UTILITY_RELAY_ID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功！", m.DC_UTILITY_RELAY_ID);
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
        public static Message MdyJWD(DC_UTILITY_RELAY_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE DC_UTILITY_RELAY");
            sb.AppendFormat(" set ");
            sb.AppendFormat("JD={0}", ClsSql.saveNullField(m.JD));
            sb.AppendFormat(",WD={0}", ClsSql.saveNullField(m.WD));
            sb.AppendFormat(" where DC_UTILITY_RELAY_ID= '{0}'", ClsSql.EncodeSql(m.DC_UTILITY_RELAY_ID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功！", m.DC_UTILITY_RELAY_ID);
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
        public static Message Del(DC_UTILITY_RELAY_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete DC_UTILITY_RELAY");
            sb.AppendFormat(" where DC_UTILITY_RELAY_ID= '{0}'", ClsSql.EncodeSql(m.DC_UTILITY_RELAY_ID));
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
        public static bool isExists(DC_UTILITY_RELAY_SW sw)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from DC_UTILITY_RELAY where 1=1");
            if (string.IsNullOrEmpty(sw.DC_UTILITY_RELAY_ID) == false)
                sb.AppendFormat(" where DC_UTILITY_RELAY_ID= '{0}'", ClsSql.EncodeSql(sw.DC_UTILITY_RELAY_ID));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }

        #endregion

        #region 获取记录
        /// <summary>
        /// 获取记录
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(DC_UTILITY_RELAY_SW sw)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat(" FROM      DC_UTILITY_RELAY");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.DC_UTILITY_RELAY_ID) == false)
                sb.AppendFormat(" AND DC_UTILITY_RELAY_ID = '{0}'", ClsSql.EncodeSql(sw.DC_UTILITY_RELAY_ID));
            if (string.IsNullOrEmpty(sw.NUMBER) == false)
                sb.AppendFormat(" AND NUMBER like '%{0}%'", ClsSql.EncodeSql(sw.NUMBER));
            if (string.IsNullOrEmpty(sw.NAME) == false)
                sb.AppendFormat(" AND NAME like '%{0}%'", ClsSql.EncodeSql(sw.NAME));
            if (string.IsNullOrEmpty(sw.COMMUNICATIONWAY) == false)
                sb.AppendFormat(" AND COMMUNICATIONWAY = '{0}'", ClsSql.EncodeSql(sw.COMMUNICATIONWAY));

            if (string.IsNullOrEmpty(sw.USESTATE) == false)
                sb.AppendFormat(" AND USESTATE = '{0}'", ClsSql.EncodeSql(sw.USESTATE));
            if (string.IsNullOrEmpty(sw.MANAGERSTATE) == false)
                sb.AppendFormat(" AND MANAGERSTATE = '{0}'", ClsSql.EncodeSql(sw.MANAGERSTATE));
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
            string sql = "SELECT DC_UTILITY_RELAY_ID, NUMBER, NAME, ADDRESS, MODEL, BYORGNO, COMMUNICATIONWAY,BUILDDATE, USESTATE, MANAGERSTATE, MARK, JD, WD,WORTH,BUILDDATEBEGIN,BUILDDATEEND"
                + sb.ToString()
                + " order by DC_UTILITY_RELAY_ID desc";

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
        public static DataTable getDT(DC_UTILITY_RELAY_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendFormat(" FROM      DC_UTILITY_RELAY");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.DC_UTILITY_RELAY_ID) == false)
                sb.AppendFormat(" AND DC_UTILITY_RELAY_ID = '{0}'", ClsSql.EncodeSql(sw.DC_UTILITY_RELAY_ID));
            if (string.IsNullOrEmpty(sw.NUMBER) == false)
                sb.AppendFormat(" AND NUMBER like '%{0}%'", ClsSql.EncodeSql(sw.NUMBER));
            if (string.IsNullOrEmpty(sw.NAME) == false)
                sb.AppendFormat(" AND NAME like '%{0}%'", ClsSql.EncodeSql(sw.NAME));
            if (string.IsNullOrEmpty(sw.COMMUNICATIONWAY) == false)
                sb.AppendFormat(" AND COMMUNICATIONWAY = '{0}'", ClsSql.EncodeSql(sw.COMMUNICATIONWAY));

            if (string.IsNullOrEmpty(sw.USESTATE) == false)
                sb.AppendFormat(" AND USESTATE = '{0}'", ClsSql.EncodeSql(sw.USESTATE));
            if (string.IsNullOrEmpty(sw.MANAGERSTATE) == false)
                sb.AppendFormat(" AND MANAGERSTATE = '{0}'", ClsSql.EncodeSql(sw.MANAGERSTATE));
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
            string sql = "SELECT DC_UTILITY_RELAY_ID, NUMBER, NAME, ADDRESS, MODEL, BYORGNO, COMMUNICATIONWAY,BUILDDATE, USESTATE, MANAGERSTATE, MARK, JD, WD,WORTH,BUILDDATEBEGIN,BUILDDATEEND"
                + sb.ToString()
                + " order by BYORGNO,DC_UTILITY_RELAY_ID desc";
            string sqlC = "select count(1) " + sb.ToString();
            total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
            sw.curPage = PagerCls.getCurPage(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
            DataSet ds = DataBaseClass.FullDataSet(sql, (sw.curPage - 1) * sw.pageSize, sw.pageSize, "a");
            return ds.Tables[0];
        }

        #endregion

        #region 判断是否相同坐标
        /// <summary>
        /// 判断是否相同坐标
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static bool isExistsPoint(DC_UTILITY_RELAY_Model m)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from DC_UTILITY_RELAY where 1=1");
            if (string.IsNullOrEmpty(m.JD) == false && string.IsNullOrEmpty(m.WD) == false)
                sb.AppendFormat(" and JD='{0}'", ClsSql.EncodeSql(m.JD));
            sb.AppendFormat(" and WD='{0}'", ClsSql.EncodeSql(m.WD));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion

        #region 根据DataTable和OrgNo和中继站的各个类型判断记录个数
        /// <summary>
        /// 根据DataTable和OrgNo和中继站的各个类型判断记录个数
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="orgNo">组织机构码</param>
        /// <param name="DICTVALUE">类型值</param>
        /// <param name="TYPE">确定统计那个中继站的类型</param>
        /// <returns>记录个数</returns>
        public static string getCountRELAYByOrgNo(DataTable dt, string orgNo, string DICTVALUE, string TYPE)
        {
            if (string.IsNullOrEmpty(orgNo))//返回所有记录个数
                return dt.Rows.Count.ToString();
            if (TYPE == "1")//统计使用现状类型
            {
                if (PublicCls.OrgIsShi(orgNo))//市
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_UTILITY_RELAY_ID)", "substring(BYORGNO,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("count(DC_UTILITY_RELAY_ID)", "substring(BYORGNO,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "' and USESTATE='" + DICTVALUE + "'").ToString();
                }
                else if (PublicCls.OrgIsXian(orgNo))//县
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_UTILITY_RELAY_ID)", "substring(BYORGNO,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("count(DC_UTILITY_RELAY_ID)", "substring(BYORGNO,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "' and USESTATE='" + DICTVALUE + "'").ToString();
                }
                else if (PublicCls.OrgIsZhen(orgNo))
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_UTILITY_RELAY_ID)", "substring(BYORGNO,1,9)='" + PublicCls.getZhenIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("count(DC_UTILITY_RELAY_ID)", "substring(BYORGNO,1,9)='" + PublicCls.getZhenIncOrgNo(orgNo) + "' and USESTATE='" + DICTVALUE + "'").ToString();
                }
                else //机构编码可能不正确
                    return "";
            }
            if (TYPE == "2")//统计维护管理现状
            {
                if (PublicCls.OrgIsShi(orgNo))//市
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_UTILITY_RELAY_ID)", "substring(BYORGNO,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("count(DC_UTILITY_RELAY_ID)", "substring(BYORGNO,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "' and MANAGERSTATE='" + DICTVALUE + "'").ToString();
                }
                else if (PublicCls.OrgIsXian(orgNo))//县
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_UTILITY_RELAY_ID)", "substring(BYORGNO,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("count(DC_UTILITY_RELAY_ID)", "substring(BYORGNO,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "' and MANAGERSTATE='" + DICTVALUE + "'").ToString();
                }
                else if (PublicCls.OrgIsZhen(orgNo))
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_UTILITY_RELAY_ID)", "substring(BYORGNO,1,9)='" + PublicCls.getZhenIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("count(DC_UTILITY_RELAY_ID)", "substring(BYORGNO,1,9)='" + PublicCls.getZhenIncOrgNo(orgNo) + "' and MANAGERSTATE='" + DICTVALUE + "'").ToString();
                }
                else //机构编码可能不正确
                    return "";
            }
            if (TYPE == "3")//通讯方式
            {
                if (PublicCls.OrgIsShi(orgNo))//市
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_UTILITY_RELAY_ID)", "substring(BYORGNO,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("count(DC_UTILITY_RELAY_ID)", "substring(BYORGNO,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "' and COMMUNICATIONWAY='" + DICTVALUE + "'").ToString();
                }
                else if (PublicCls.OrgIsXian(orgNo))//县
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_UTILITY_RELAY_ID)", "substring(BYORGNO,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("count(DC_UTILITY_RELAY_ID)", "substring(BYORGNO,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "' and COMMUNICATIONWAY='" + DICTVALUE + "'").ToString();
                }
                else if (PublicCls.OrgIsZhen(orgNo))
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_UTILITY_RELAY_ID)", "substring(BYORGNO,1,9)='" + PublicCls.getZhenIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("count(DC_UTILITY_RELAY_ID)", "substring(BYORGNO,1,9)='" + PublicCls.getZhenIncOrgNo(orgNo) + "' and COMMUNICATIONWAY='" + DICTVALUE + "'").ToString();
                }
                else //机构编码可能不正确
                    return "";
            }
            else
                return "";
        }
        #endregion

        #region 根据DataTable和OrgNo和中继站的各个县市类型判断记录个数
        /// <summary>
        /// 根据DataTable和OrgNo和中继站的各个县市类型判断记录个数
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="orgNo">组织机构码</param>
        /// <param name="DICTVALUE">类型值</param>
        /// <param name="TYPE">确定统计那个中继站的类型</param>
        /// <returns>记录个数</returns>
        public static string getCountXSByOrgNo(DataTable dt, string orgNo, string DICTVALUE, string TYPE)
        {
            if (string.IsNullOrEmpty(orgNo))//返回所有记录个数
                return dt.Rows.Count.ToString();
            if (TYPE == "1")//统计使用现状类型
            {
                 if (PublicCls.OrgIsZhen(orgNo)==false)
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_UTILITY_RELAY_ID)", "BYORGNO='" + orgNo + "'").ToString();
                    else
                        return dt.Compute("count(DC_UTILITY_RELAY_ID)", "BYORGNO='" + orgNo + "' and USESTATE='" + DICTVALUE + "'").ToString();
                }
                else //机构编码可能不正确
                    return "";
            }
            if (TYPE == "2")//统计维护管理现状
            {
                if (PublicCls.OrgIsZhen(orgNo)==false)
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_UTILITY_RELAY_ID)", "BYORGNO='" + orgNo + "'").ToString();
                    else
                        return dt.Compute("count(DC_UTILITY_RELAY_ID)", "BYORGNO='" + orgNo + "' and MANAGERSTATE='" + DICTVALUE + "'").ToString();
                }
                else //机构编码可能不正确
                    return "";
            }
            if (TYPE == "3")//通讯方式
            {
                 if (PublicCls.OrgIsZhen(orgNo)==false)
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_UTILITY_RELAY_ID)", "BYORGNO='" + orgNo + "'").ToString();
                    else
                        return dt.Compute("count(DC_UTILITY_RELAY_ID)", "BYORGNO='" + orgNo + "' and COMMUNICATIONWAY='" + DICTVALUE + "'").ToString();
                }
                else //机构编码可能不正确
                    return "";
            }
            else
                return "";
        }
        #endregion

        #region 统计当前用户下中继站的数量
        /// <summary>
        /// 统计当前用户下中继站的数量
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getNum(DC_UTILITY_RELAY_SW sw)
        {
            string total = "";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("    from   DC_UTILITY_RELAY a ");
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