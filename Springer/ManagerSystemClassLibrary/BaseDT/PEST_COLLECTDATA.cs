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
    /// 有害生物_采集数据表
    /// </summary>
    public class PEST_COLLECTDATA
    {
        #region 增、删、改
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(PEST_COLLECTDATA_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO PEST_COLLECTDATA(HID, BYORGNO, COLLECTRESOURCE, VILLAGENAME, SMALLADDRESS, SMALLCLASSCODE, SMALLCLASSAREA,");
            sb.AppendFormat("HOSTTREESPECIESCODE, SEARCHTYPE, COLLECTPESTCODE, HARMPOSITION, HARMLEVEL, DEADCOUNT, UNKNOWNDIEOFFCOUNT ,");
            sb.AppendFormat("ELSEDIEOFFCOUNT, SAMPLECOUNT, MARK, UPLOADTIME, MANSTATE, MANRESULT, MANTIME, MANUSERID, KID, JWDLIST) ");
            sb.AppendFormat(" OUTPUT INSERTED.PESTCOLLDATAID ");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.HID));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.BYORGNO));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.COLLECTRESOURCE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.VILLAGENAME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.SMALLADDRESS));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.SMALLCLASSCODE));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.SMALLCLASSAREA));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.HOSTTREESPECIESCODE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.SEARCHTYPE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.COLLECTPESTCODE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.HARMPOSITION));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.HARMLEVEL));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.DEADCOUNT));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.UNKNOWNDIEOFFCOUNT));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.ELSEDIEOFFCOUNT));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.SAMPLECOUNT));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.MARK));
            sb.AppendFormat(",'{0}'", m.UPLOADTIME);
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.MANSTATE));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.MANRESULT));
            sb.AppendFormat(",'{0}'", m.MANTIME);
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.MANUSERID));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.KID));
            sb.AppendFormat(",{0})", ClsSql.saveNullField(m.JWDLIST));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "添加成功!", "");
            else
                return new Message(false, "添加失败!", "");
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Mdy(PEST_COLLECTDATA_Model m)
        {
            if (isExists(new PEST_COLLECTDATA_SW { PESTCOLLDATAID = m.PESTCOLLDATAID }) == false)
                return new Message(false, "修改失败，采集数据不存在!", "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("update PEST_COLLECTDATA SET ");
            sb.AppendFormat(" HID='{0}'", ClsSql.EncodeSql(m.HID));
            sb.AppendFormat(",BYORGNO='{0}'", ClsSql.EncodeSql(m.BYORGNO));
            sb.AppendFormat(",COLLECTRESOURCE='{0}'", ClsSql.EncodeSql(m.COLLECTRESOURCE));
            sb.AppendFormat(",VILLAGENAME='{0}'", ClsSql.EncodeSql(m.VILLAGENAME));
            sb.AppendFormat(",SMALLADDRESS='{0}'", ClsSql.EncodeSql(m.SMALLADDRESS));
            sb.AppendFormat(",SMALLCLASSCODE='{0}'", ClsSql.EncodeSql(m.SMALLCLASSCODE));
            sb.AppendFormat(",SMALLCLASSAREA={0}", ClsSql.saveNullField(m.SMALLCLASSAREA));
            sb.AppendFormat(",HOSTTREESPECIESCODE='{0}'", ClsSql.EncodeSql(m.HOSTTREESPECIESCODE));
            sb.AppendFormat(",SEARCHTYPE='{0}'", ClsSql.EncodeSql(m.SEARCHTYPE));
            sb.AppendFormat(",COLLECTPESTCODE='{0}'", ClsSql.EncodeSql(m.COLLECTPESTCODE));
            sb.AppendFormat(",HARMPOSITION='{0}'", ClsSql.EncodeSql(m.HARMPOSITION));
            sb.AppendFormat(",HARMLEVEL='{0}'", ClsSql.EncodeSql(m.HARMLEVEL));
            sb.AppendFormat(",DEADCOUNT={0}", ClsSql.saveNullField(m.DEADCOUNT));
            sb.AppendFormat(",UNKNOWNDIEOFFCOUNT={0}", ClsSql.saveNullField(m.UNKNOWNDIEOFFCOUNT));
            sb.AppendFormat(",ELSEDIEOFFCOUNT={0}", ClsSql.saveNullField(m.ELSEDIEOFFCOUNT));
            sb.AppendFormat(",SAMPLECOUNT={0}", ClsSql.saveNullField(m.SAMPLECOUNT));
            sb.AppendFormat(",MARK={0}", ClsSql.saveNullField(m.MARK));
            sb.AppendFormat(",MANSTATE='{0}'", ClsSql.EncodeSql(m.MANSTATE));
            sb.AppendFormat(",MANRESULT={0}", ClsSql.saveNullField(m.MANRESULT));
            sb.AppendFormat(",MANTIME='{0}'", m.MANTIME);
            sb.AppendFormat(",MANUSERID='{0}'", ClsSql.EncodeSql(m.MANUSERID));
            sb.AppendFormat(",KID={0}", ClsSql.saveNullField(m.KID));
            sb.AppendFormat(",JWDLIST={0}", ClsSql.saveNullField(m.JWDLIST));
            sb.AppendFormat(" WHERE PESTCOLLDATAID= '{0}'", ClsSql.EncodeSql(m.PESTCOLLDATAID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功!", m.PESTCOLLDATAID);
            else
                return new Message(false, "修改失败!", "");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(PEST_COLLECTDATA_Model m)
        {
            string[] arrPESTCOLLDATAID = m.PESTCOLLDATAID.Split(',');
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete from PEST_COLLECTDATA");
            sb.AppendFormat(" where PESTCOLLDATAID  in  (");
            for (int i = 0; i < arrPESTCOLLDATAID.Length; i++)
            {
                if (i != arrPESTCOLLDATAID.Length - 1)
                    sb.AppendFormat("'{0}',", ClsSql.EncodeSql(arrPESTCOLLDATAID[i]));
                else
                    sb.AppendFormat("'{0}'", ClsSql.EncodeSql(arrPESTCOLLDATAID[i]));
            }
            sb.AppendFormat(")");
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功!", "");
            else
                return new Message(false, "删除失败!", "");
        }
        #endregion

        #region 判断数据是否存在
        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>true存在 false不存在</returns>
        public static bool isExists(PEST_COLLECTDATA_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from PEST_COLLECTDATA  where 1=1");
            if (string.IsNullOrEmpty(sw.PESTCOLLDATAID) == false)
                sb.AppendFormat(" and PESTCOLLDATAID='{0}'", ClsSql.EncodeSql(sw.PESTCOLLDATAID));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion

        #region 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw">查询模型</param>
        /// <param name="total">总记录数</param>
        /// <returns></returns>
        public static DataTable getDT(PEST_COLLECTDATA_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM PEST_COLLECTDATA  WHERE 1=1");

            #region 查询条件
            //根据时间查询
            if (string.IsNullOrEmpty(sw.StartTime) == false)
                sb.AppendFormat(" AND UPLOADTIME >= '{0}'", DateTime.Parse(sw.StartTime));
            if (string.IsNullOrEmpty(sw.EndTime) == false)
                sb.AppendFormat(" AND UPLOADTIME <= '{0}'", DateTime.Parse(sw.EndTime).AddDays(1).AddSeconds(-1));
            //根据村名查询
            if (string.IsNullOrEmpty(sw.VILLAGENAME) == false)
                sb.AppendFormat(" AND VILLAGENAME like  '%{0}%'", ClsSql.EncodeSql(sw.VILLAGENAME));
            //根据小地名查询
            if (string.IsNullOrEmpty(sw.SMALLADDRESS) == false)
                sb.AppendFormat(" AND SMALLADDRESS = '{0}'", ClsSql.EncodeSql(sw.SMALLADDRESS));
            //根据组织机构查询
            if (!string.IsNullOrEmpty(sw.BYORGNO))
            {
                if (sw.BYORGNO.Substring(4, 11) == "00000000000")  //获取所有市的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,4) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
                else if (sw.BYORGNO.Substring(6, 9) == "000000000" && sw.BYORGNO.Substring(4, 11) != "00000000000") //获取所有县的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,6) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
                else if (sw.BYORGNO.Substring(9, 6) == "000000" && sw.BYORGNO.Substring(6, 9) != "000000000")   //获取所有乡镇的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,9) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 9)));
                else if (sw.BYORGNO.Substring(9, 6) != "000000")   //获取所有村的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,12) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 12)));
                else
                    sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            }
            #endregion

            string sql = "SELECT * " + sb.ToString() + " order by BYORGNO,UPLOADTIME DESC ";
            string sqlC = "select count(1) " + sb.ToString();
            total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
            sw.CurPage = PagerCls.getCurPage(new PagerSW { curPage = sw.CurPage, pageSize = sw.PageSize, rowCount = total });
            DataSet ds = DataBaseClass.FullDataSet(sql, (sw.CurPage - 1) * sw.PageSize, sw.PageSize, "PEST_COLLECTDATA");
            return ds.Tables[0];
        }

        /// <summary>
        ///  获取数据
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(PEST_COLLECTDATA_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM PEST_COLLECTDATA  WHERE 1=1");

            #region 查询条件
            //根据序号查询
            if (string.IsNullOrEmpty(sw.PESTCOLLDATAID) == false)
                sb.AppendFormat(" AND PESTCOLLDATAID = '{0}'", ClsSql.EncodeSql(sw.PESTCOLLDATAID));
            //根据时间查询
            if (string.IsNullOrEmpty(sw.StartTime) == false)
                sb.AppendFormat(" AND UPLOADTIME >= '{0}'", Convert.ToDateTime(sw.StartTime));
            if (string.IsNullOrEmpty(sw.EndTime) == false)
                sb.AppendFormat(" AND UPLOADTIME <= '{0}'", Convert.ToDateTime(sw.EndTime));
            //根据村名查询
            if (string.IsNullOrEmpty(sw.VILLAGENAME) == false)
                sb.AppendFormat(" AND VILLAGENAME like  '%{0}%'", ClsSql.EncodeSql(sw.VILLAGENAME));
            //根据小地名查询
            if (string.IsNullOrEmpty(sw.SMALLADDRESS) == false)
                sb.AppendFormat(" AND SMALLADDRESS = '{0}'", ClsSql.EncodeSql(sw.SMALLADDRESS));
            //根据组织机构查询
            if (!string.IsNullOrEmpty(sw.BYORGNO))
            {
                if (sw.BYORGNO.Substring(4, 11) == "00000000000")  //获取所有市的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,4) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
                else if (sw.BYORGNO.Substring(6, 9) == "000000000" && sw.BYORGNO.Substring(4, 11) != "00000000000") //获取所有县的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,6) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
                else if (sw.BYORGNO.Substring(9, 6) == "000000" && sw.BYORGNO.Substring(6, 9) != "000000000")   //获取所有乡镇的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,9) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 9)));
                else if (sw.BYORGNO.Substring(9, 6) != "000000")   //获取所有村的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,12) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 12)));
                else
                    sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            }
            #endregion

            string sql = "SELECT * " + sb.ToString() + " order by BYORGNO,UPLOADTIME DESC ";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }
        #endregion
    }
}
