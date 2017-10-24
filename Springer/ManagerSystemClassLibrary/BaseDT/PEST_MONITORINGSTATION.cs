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
    /// 监测点表
    /// </summary>
    public class PEST_MONITORINGSTATION
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(PEST_MONITORINGSTATION_Model m)
        {
            if (IsExistsPoint(new PEST_MONITORINGSTATION_Model { JD = m.JD, WD = m.WD }) == false)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("INSERT INTO  PEST_MONITORINGSTATION(NUMBER, NAME, ADDRESS, MODEL, BYORGNO, TRANSFERMODETYPE, MONICONTENT, BUILDDATE, USESTATE, MANAGERSTATE, MARK, JD, WD, WORTH)");
                sb.AppendFormat(" OUTPUT INSERTED.PEST_MONITORINGSTATIONID");
                sb.AppendFormat(" VALUES(");
                sb.AppendFormat("{0}", ClsSql.saveNullField(m.NUMBER));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.NAME));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.ADDRESS));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.MODEL));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.BYORGNO));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.TRANSFERMODETYPE));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.MONICONTENT));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.BUILDDATE));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.USESTATE));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.MANAGERSTATE));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.MARK));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.JD));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.WD));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.WORTH));
                sb.AppendFormat(")");
                try
                {
                    string sId = DataBaseClass.ReturnSqlField(sb.ToString());
                    if (sId != "")
                        return new Message(true, "添加成功!", sId);
                    else
                        return new Message(false, "添加失败!", "");
                }
                catch
                {
                    return new Message(false, "添加失败!", "");
                }
            }
            else
                return new Message(false, "添加失败,已有相同的位置的监测点!", "");
        }

        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Mdy(PEST_MONITORINGSTATION_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE PEST_MONITORINGSTATION");
            sb.AppendFormat(" SET ");
            sb.AppendFormat("NUMBER={0}", ClsSql.saveNullField(m.NUMBER));
            sb.AppendFormat(",NAME={0}", ClsSql.saveNullField(m.NAME));
            sb.AppendFormat(",ADDRESS={0}", ClsSql.saveNullField(m.ADDRESS));
            sb.AppendFormat(",MODEL={0}", ClsSql.saveNullField(m.MODEL));
            sb.AppendFormat(",BYORGNO={0}", ClsSql.saveNullField(m.BYORGNO));
            sb.AppendFormat(",TRANSFERMODETYPE={0}", ClsSql.saveNullField(m.TRANSFERMODETYPE));
            sb.AppendFormat(",MONICONTENT={0}", ClsSql.saveNullField(m.MONICONTENT));
            sb.AppendFormat(",BUILDDATE={0}", ClsSql.saveNullField(m.BUILDDATE));
            sb.AppendFormat(",USESTATE={0}", ClsSql.saveNullField(m.USESTATE));
            sb.AppendFormat(",MANAGERSTATE={0}", ClsSql.saveNullField(m.MANAGERSTATE));
            sb.AppendFormat(",MARK={0}", ClsSql.saveNullField(m.MARK));
            sb.AppendFormat(",JD={0}", ClsSql.saveNullField(m.JD));
            sb.AppendFormat(",WD={0}", ClsSql.saveNullField(m.WD));
            sb.AppendFormat(",WORTH={0}", ClsSql.saveNullField(m.WORTH));
            sb.AppendFormat(" WHERE PEST_MONITORINGSTATIONID= '{0}'", ClsSql.EncodeSql(m.PEST_MONITORINGSTATIONID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功!", m.PEST_MONITORINGSTATIONID);
            else
                return new Message(false, "修改失败!", "");
        }

        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(PEST_MONITORINGSTATION_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete PEST_MONITORINGSTATION");
            sb.AppendFormat(" where PEST_MONITORINGSTATIONID= '{0}'", ClsSql.EncodeSql(m.PEST_MONITORINGSTATIONID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功!", "");
            else
                return new Message(false, "删除失败!", "");
        }

        #endregion

        #region 判断记录是否存在
        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>true存在 false不存在</returns>
        public static bool isExists(PEST_MONITORINGSTATION_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from PEST_MONITORINGSTATION where 1=1");
            if (string.IsNullOrEmpty(sw.PEST_MONITORINGSTATIONID) == false)
                sb.AppendFormat(" and PEST_MONITORINGSTATIONID= '{0}'", ClsSql.EncodeSql(sw.PEST_MONITORINGSTATIONID));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }

        #endregion

        #region 获取列表
        /// <summary>
        /// 获取记录
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(PEST_MONITORINGSTATION_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM  PEST_MONITORINGSTATION WHERE  1=1");
            if (string.IsNullOrEmpty(sw.PEST_MONITORINGSTATIONID) == false)
                sb.AppendFormat(" AND PEST_MONITORINGSTATIONID = '{0}'", ClsSql.EncodeSql(sw.PEST_MONITORINGSTATIONID));
            if (string.IsNullOrEmpty(sw.NUMBER) == false)
                sb.AppendFormat(" AND NUMBER like '%{0}%'", ClsSql.EncodeSql(sw.NUMBER));
            if (string.IsNullOrEmpty(sw.NAME) == false)
                sb.AppendFormat(" AND NAME like '%{0}%'", ClsSql.EncodeSql(sw.NAME));
            if (string.IsNullOrEmpty(sw.USESTATE) == false)
                sb.AppendFormat(" AND USESTATE = '{0}'", ClsSql.EncodeSql(sw.USESTATE));
            if (string.IsNullOrEmpty(sw.MANAGERSTATE) == false)
                sb.AppendFormat(" AND MANAGERSTATE = '{0}'", ClsSql.EncodeSql(sw.MANAGERSTATE));
            if (string.IsNullOrEmpty(sw.TRANSFERMODETYPE) == false)
                sb.AppendFormat(" AND TRANSFERMODETYPE = '{0}'", ClsSql.EncodeSql(sw.TRANSFERMODETYPE));
            if (!string.IsNullOrEmpty(sw.BYORGNO))
            {
                if (sw.BYORGNO.Substring(4, 11) == "00000000000")//获取所有市的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,4) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
                else if (sw.BYORGNO.Substring(4, 11) == "xxxxxxxxxxx")//单独市
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,9) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4) + "00000000000"));
                else if (sw.BYORGNO.Substring(6, 9) == "xxxxxxxxx")//获取所有县的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,6) = '{0}' )", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
                else
                    sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            }
            string sql = "SELECT PEST_MONITORINGSTATIONID, NUMBER, NAME, ADDRESS, MODEL, BYORGNO, TRANSFERMODETYPE, MONICONTENT, BUILDDATE, USESTATE, MANAGERSTATE, MARK, JD, WD, WORTH"
                + sb.ToString() + " ORDER BY BYORGNO, BUILDDATE DESC  ";
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
        public static DataTable getDT(PEST_MONITORINGSTATION_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM  PEST_MONITORINGSTATION WHERE  1=1");
            if (string.IsNullOrEmpty(sw.PEST_MONITORINGSTATIONID) == false)
                sb.AppendFormat(" AND PEST_MONITORINGSTATIONID = '{0}'", ClsSql.EncodeSql(sw.PEST_MONITORINGSTATIONID));
            if (string.IsNullOrEmpty(sw.NUMBER) == false)
                sb.AppendFormat(" AND NUMBER like '%{0}%'", ClsSql.EncodeSql(sw.NUMBER));
            if (string.IsNullOrEmpty(sw.NAME) == false)
                sb.AppendFormat(" AND NAME like '%{0}%'", ClsSql.EncodeSql(sw.NAME));
            if (string.IsNullOrEmpty(sw.USESTATE) == false)
                sb.AppendFormat(" AND USESTATE = '{0}'", ClsSql.EncodeSql(sw.USESTATE));
            if (string.IsNullOrEmpty(sw.MANAGERSTATE) == false)
                sb.AppendFormat(" AND MANAGERSTATE = '{0}'", ClsSql.EncodeSql(sw.MANAGERSTATE));
            if (string.IsNullOrEmpty(sw.TRANSFERMODETYPE) == false)
                sb.AppendFormat(" AND TRANSFERMODETYPE = '{0}'", ClsSql.EncodeSql(sw.TRANSFERMODETYPE));
            if (!string.IsNullOrEmpty(sw.BYORGNO))
            {
                if (sw.BYORGNO.Substring(4, 11) == "00000000000")//获取所有市的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,4) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
                else if (sw.BYORGNO.Substring(4, 11) == "xxxxxxxxxxx")//单独市
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,9) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4) + "00000000000"));
                else if (sw.BYORGNO.Substring(6, 9) == "xxxxxxxxx")//获取所有县的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,6) = '{0}' )", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
                else
                    sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            }
            string sql = "SELECT PEST_MONITORINGSTATIONID, NUMBER, NAME, ADDRESS, MODEL, BYORGNO, TRANSFERMODETYPE, MONICONTENT, BUILDDATE, USESTATE, MANAGERSTATE, MARK, JD, WD, WORTH "
                + sb.ToString() + " ORDER BY BYORGNO, BUILDDATE DESC ";
            string sqlC = "select count(1) " + sb.ToString();
            total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
            sw.CurPage = PagerCls.getCurPage(new PagerSW { curPage = sw.CurPage, pageSize = sw.PageSize, rowCount = total });
            DataSet ds = DataBaseClass.FullDataSet(sql, (sw.CurPage - 1) * sw.PageSize, sw.PageSize, "a");
            return ds.Tables[0];
        }

        #endregion

        #region 修改经纬度
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message MdyJWD(PEST_MONITORINGSTATION_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE PEST_MONITORINGSTATION");
            sb.AppendFormat(" set ");
            sb.AppendFormat("JD={0}", ClsSql.saveNullField(m.JD));
            sb.AppendFormat(",WD={0}", ClsSql.saveNullField(m.WD));
            sb.AppendFormat(" WHERE DC_UTILITY_MONITORINGSTATION_ID= '{0}'", ClsSql.EncodeSql(m.PEST_MONITORINGSTATIONID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功!", m.PEST_MONITORINGSTATIONID);
            else
                return new Message(false, "修改失败!", "");
        }

        #endregion

        #region 判断是否相同坐标
        /// <summary>
        /// 判断是否相同坐标
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static bool IsExistsPoint(PEST_MONITORINGSTATION_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from PEST_MONITORINGSTATION where 1=1");
            sb.AppendFormat(" and JD='{0}'", ClsSql.EncodeSql(m.JD));
            sb.AppendFormat(" and WD='{0}'", ClsSql.EncodeSql(m.WD));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion
    }
}
