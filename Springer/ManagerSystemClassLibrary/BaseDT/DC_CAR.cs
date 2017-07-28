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
    /// 数据中心_车辆
    /// </summary>
    public class DC_CAR
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(DC_CAR_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  DC_CAR( CARTYPE, NUMBER, NAME, BYORGNO, BUYYEAR, BUYPRICE, PLATENUM, DRIVER, CONTACTS, GPSEQUIP, GPSTELL, STOREADDR, MARK, JD, WD)");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("{0}", ClsSql.saveNullField(m.CARTYPE));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.NUMBER));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.NAME));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.BYORGNO));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.BUYYEAR));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.BUYPRICE));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.PLATENUM));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.DRIVER));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.CONTACTS));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.GPSEQUIP));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.GPSTELL));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.STOREADDR));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.MARK));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.JD));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.WD));

            sb.AppendFormat(")");
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "添加成功！", "");
            else
                return new Message(false, "添加失败，请检查各输入框是否正确！", "");

            //StringBuilder sb = new StringBuilder();
            //sb.AppendFormat("INSERT INTO  DC_CAR( CARTYPE, NUMBER, NAME, BYORGNO, BUYYEAR, BUYPRICE, PLATENUM, DRIVER, CONTACTS, GPSEQUIP, GPSTELL, STOREADDR, MARK, JD, WD) output inserted.DC_CAR_ID");
            //sb.AppendFormat(" VALUES(");
            //sb.AppendFormat("{0}", ClsSql.saveNullField(m.CARTYPE));
            //sb.AppendFormat(",{0}", ClsSql.saveNullField(m.NUMBER));
            //sb.AppendFormat(",{0}", ClsSql.saveNullField(m.NAME));
            //sb.AppendFormat(",{0}", ClsSql.saveNullField(m.BYORGNO));
            //sb.AppendFormat(",{0}", ClsSql.saveNullField(m.BUYYEAR));
            //sb.AppendFormat(",{0}", ClsSql.saveNullField(m.BUYPRICE));
            //sb.AppendFormat(",{0}", ClsSql.saveNullField(m.PLATENUM));
            //sb.AppendFormat(",{0}", ClsSql.saveNullField(m.DRIVER));
            //sb.AppendFormat(",{0}", ClsSql.saveNullField(m.CONTACTS));
            //sb.AppendFormat(",{0}", ClsSql.saveNullField(m.GPSEQUIP));
            //sb.AppendFormat(",{0}", ClsSql.saveNullField(m.GPSTELL));
            //sb.AppendFormat(",{0}", ClsSql.saveNullField(m.STOREADDR));
            //sb.AppendFormat(",{0}", ClsSql.saveNullField(m.MARK));
            //sb.AppendFormat(",{0}", ClsSql.saveNullField(m.JD));
            //sb.AppendFormat(",{0}", ClsSql.saveNullField(m.WD));
            //sb.AppendFormat(")");
            //try
            //{
            //    string strid = DataBaseClass.ReturnSqlField(sb.ToString());
            //    return new Message(true, "添加成功！", strid);
            //}
            //catch (Exception)
            //{

            //    throw;
            //}
        }

        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Mdy(DC_CAR_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE DC_CAR");
            sb.AppendFormat(" set ");
            sb.AppendFormat("CARTYPE={0}", ClsSql.saveNullField(m.CARTYPE));
            sb.AppendFormat(",NUMBER={0}", ClsSql.saveNullField(m.NUMBER));
            sb.AppendFormat(",NAME={0}", ClsSql.saveNullField(m.NAME));
            sb.AppendFormat(",BYORGNO={0}", ClsSql.saveNullField(m.BYORGNO));
            sb.AppendFormat(",BUYYEAR={0}", ClsSql.saveNullField(m.BUYYEAR));
            sb.AppendFormat(",BUYPRICE={0}", ClsSql.saveNullField(m.BUYPRICE));
            sb.AppendFormat(",PLATENUM={0}", ClsSql.saveNullField(m.PLATENUM));
            sb.AppendFormat(",DRIVER={0}", ClsSql.saveNullField(m.DRIVER));
            sb.AppendFormat(",CONTACTS={0}", ClsSql.saveNullField(m.CONTACTS));
            sb.AppendFormat(",GPSEQUIP={0}", ClsSql.saveNullField(m.GPSEQUIP));
            sb.AppendFormat(",GPSTELL={0}", ClsSql.saveNullField(m.GPSTELL));
            sb.AppendFormat(",STOREADDR={0}", ClsSql.saveNullField(m.STOREADDR));
            sb.AppendFormat(",MARK={0}", ClsSql.saveNullField(m.MARK));
            sb.AppendFormat(",JD={0}", ClsSql.saveNullField(m.JD));
            sb.AppendFormat(",WD={0}", ClsSql.saveNullField(m.WD));
            sb.AppendFormat(" where DC_CAR_ID= '{0}'", ClsSql.EncodeSql(m.DC_CAR_ID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功！", m.DC_CAR_ID);
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
        public static Message MdyJWD(DC_CAR_Model m)
        {
            StringBuilder sb = new StringBuilder();
            //HID, HNAME, SN, PHONE, SEX, BIRTH, ONSTATE, BYORGNO
            sb.AppendFormat("UPDATE DC_CAR");
            sb.AppendFormat(" set ");
            sb.AppendFormat("JD={0}", ClsSql.saveNullField(m.JD));
            sb.AppendFormat(",WD={0}", ClsSql.saveNullField(m.WD));
            sb.AppendFormat(" where DC_CAR_ID= '{0}'", ClsSql.EncodeSql(m.DC_CAR_ID));
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
        public static Message Del(DC_CAR_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete DC_CAR");
            sb.AppendFormat(" where DC_CAR_ID= '{0}'", ClsSql.EncodeSql(m.DC_CAR_ID));
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
        public static bool isExists(DC_CAR_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from DC_CAR where 1=1");
            if (string.IsNullOrEmpty(sw.DC_CAR_ID) == false)
                sb.AppendFormat(" where DC_CAR_ID= '{0}'", ClsSql.EncodeSql(sw.DC_CAR_ID));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }

        #endregion

        #region 获取记录
        /// <summary>
        /// 获取记录
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(DC_CAR_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM      DC_CAR");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.DC_CAR_ID) == false)
                sb.AppendFormat(" AND DC_CAR_ID = '{0}'", ClsSql.EncodeSql(sw.DC_CAR_ID));
            if (string.IsNullOrEmpty(sw.CARTYPE) == false)
                sb.AppendFormat(" AND CARTYPE = '{0}'", ClsSql.EncodeSql(sw.CARTYPE));
            if (string.IsNullOrEmpty(sw.NUMBER) == false)
                sb.AppendFormat(" AND NUMBER like '%{0}%'", ClsSql.EncodeSql(sw.NUMBER));
            if (string.IsNullOrEmpty(sw.NAME) == false)
                sb.AppendFormat(" AND NAME like '%{0}%'", ClsSql.EncodeSql(sw.NAME));
            if (string.IsNullOrEmpty(sw.PLATENUM) == false)
                sb.AppendFormat(" AND PLATENUM like '%{0}%'", ClsSql.EncodeSql(sw.PLATENUM));
            if (!string.IsNullOrEmpty(sw.BYORGNO))
            {
                if (sw.ORGNOSXZ != "1")
                {
                    if (sw.BYORGNO.Substring(4, 5) == "00000")//获取所有市的
                        sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,4) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
                    else if (sw.BYORGNO.Substring(6, 3) == "000")//获取所有县的
                        sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,6) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
                    else
                        sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
                }
                else 
                {
                    sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
                }
            }
            string sql = "SELECT DC_CAR_ID, CARTYPE, NUMBER, NAME, BYORGNO, BUYYEAR, BUYPRICE, PLATENUM, DRIVER, CONTACTS, GPSEQUIP, GPSTELL, STOREADDR, MARK, JD, WD"
                + sb.ToString()
                + " order by BYORGNO,CARTYPE";

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
        public static DataTable getDT(DC_CAR_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendFormat(" FROM      DC_CAR");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.DC_CAR_ID) == false)
                sb.AppendFormat(" AND DC_CAR_ID = '{0}'", ClsSql.EncodeSql(sw.DC_CAR_ID));
            if (string.IsNullOrEmpty(sw.CARTYPE) == false)
                sb.AppendFormat(" AND CARTYPE = '{0}'", ClsSql.EncodeSql(sw.CARTYPE));
            if (string.IsNullOrEmpty(sw.NUMBER) == false)
                sb.AppendFormat(" AND NUMBER like '%{0}%'", ClsSql.EncodeSql(sw.NUMBER));
            if (string.IsNullOrEmpty(sw.NAME) == false)
                sb.AppendFormat(" AND NAME like '%{0}%'", ClsSql.EncodeSql(sw.NAME));
            if (string.IsNullOrEmpty(sw.PLATENUM) == false)
                sb.AppendFormat(" AND PLATENUM like '%{0}%'", ClsSql.EncodeSql(sw.PLATENUM));
            if (!string.IsNullOrEmpty(sw.BYORGNO))
            {
                if (sw.BYORGNO.Substring(4, 5) == "00000")//获取所有市的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,4) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
                else if (sw.BYORGNO.Substring(4, 5) == "xxxxx")//单独市
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,9) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)+ "00000"));
                else if (sw.BYORGNO.Substring(6, 3) == "xxx")//获取所有县的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,6) = '{0}' )", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
                else
                    sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            }
            string sql = "SELECT DC_CAR_ID, CARTYPE, NUMBER, NAME, BYORGNO, BUYYEAR, BUYPRICE, PLATENUM, DRIVER, CONTACTS, GPSEQUIP, GPSTELL, STOREADDR, MARK, JD, WD"
                + sb.ToString()
                + " order by BYORGNO,DC_CAR_ID desc";
            string sqlC = "select count(1) " + sb.ToString();
            total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
            sw.curPage = PagerCls.getCurPage(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
            DataSet ds = DataBaseClass.FullDataSet(sql, (sw.curPage - 1) * sw.pageSize, sw.pageSize, "a");
            return ds.Tables[0];
        }

        #endregion

        #region 根据DataTable和OrgNo和车辆的各个类型判断记录个数
        /// <summary>
        /// 根据DataTable和OrgNo和车辆类型判断记录个数
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="orgNo">单位编码</param>
        /// <param name="DICTVALUE">资源的各个类型</param> 
        /// <returns>记录个数</returns>
        public static string getCountCARByOrgNo(DataTable dt, string orgNo, string DICTVALUE)
        {
            if (string.IsNullOrEmpty(orgNo))//返回所有记录个数
                return dt.Rows.Count.ToString();
            
                if (PublicCls.OrgIsShi(orgNo))//市
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_CAR_ID)", "substring(BYORGNO,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("count(DC_CAR_ID)", "substring(BYORGNO,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "' and CARTYPE='" + DICTVALUE + "'").ToString();
                }
                else if (PublicCls.OrgIsXian(orgNo))//县
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_CAR_ID)", "substring(BYORGNO,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("count(DC_CAR_ID)", "substring(BYORGNO,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "' and CARTYPE='" + DICTVALUE + "'").ToString();
                }
                else if (PublicCls.OrgIsZhen(orgNo))
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(DC_CAR_ID)", "BYORGNO='" + PublicCls.getZhenIncOrgNo(orgNo) + "'").ToString();
                    else
                        return dt.Compute("count(DC_CAR_ID)", "BYORGNO='" + PublicCls.getZhenIncOrgNo(orgNo) + "' and CARTYPE='" + DICTVALUE + "'").ToString();
                }
                else //机构编码可能不正确
                    return "";
            }
        #endregion

        #region 根据DataTable和OrgNo和车辆的各个市县类型判断记录个数
        /// <summary>
        /// 根据DataTable和OrgNo和车辆类型判断记录个数
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="orgNo">单位编码</param>
        /// <param name="DICTVALUE">资源的各个类型</param> 
        /// <returns>记录个数</returns>
        public static string getCountXSByOrgNo(DataTable dt, string orgNo, string DICTVALUE)
        {
            if (string.IsNullOrEmpty(orgNo))//返回所有记录个数
                return dt.Rows.Count.ToString();

            
             if (PublicCls.OrgIsZhen(orgNo)==false)
            {
                if (string.IsNullOrEmpty(DICTVALUE))
                    return dt.Compute("count(DC_CAR_ID)", "BYORGNO='" + orgNo + "'").ToString();
                else
                    return dt.Compute("count(DC_CAR_ID)", "BYORGNO='" + orgNo + "' and CARTYPE='" + DICTVALUE + "'").ToString();
            }
            else //机构编码可能不正确
                return "";
        }
        #endregion

        #region 统计当前用户下车辆的数量
        /// <summary>
        /// 统计当前用户下车辆的数量
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getNum(DC_CAR_SW sw)
        {
            string total = "";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("    from    DC_CAR a ");
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
