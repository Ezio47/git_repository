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
    /// 数据中心_队伍_人员表
    /// </summary>
    public class DC_ARMY_MEMBER
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(DC_ARMY_MEMBER_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  DC_ARMY_MEMBER( DC_ARMY_ID, MEMBERNAME, SEX, CONTACTS, BIRTH)");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("{0}", ClsSql.saveNullField(m.DC_ARMY_ID));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.MEMBERNAME));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.SEX));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.CONTACTS));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.BIRTH));

            sb.AppendFormat(")");
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "添加成功！", "");
            else
                return new Message(false, "添加失败，请检查各输入框是否正确！", "");
        }

        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Mdy(DC_ARMY_MEMBER_Model m)
        {
            StringBuilder sb = new StringBuilder();
            //HID, HNAME, SN, PHONE, SEX, BIRTH, ONSTATE, BYORGNO
            sb.AppendFormat("UPDATE DC_ARMY_MEMBER");
            sb.AppendFormat(" set ");
            sb.AppendFormat("DC_ARMY_ID={0}", ClsSql.saveNullField(m.DC_ARMY_ID));
            sb.AppendFormat(",MEMBERNAME={0}", ClsSql.saveNullField(m.MEMBERNAME));
            sb.AppendFormat(",SEX={0}", ClsSql.saveNullField(m.SEX));
            sb.AppendFormat(",CONTACTS={0}", ClsSql.saveNullField(m.CONTACTS));
            sb.AppendFormat(",BIRTH={0}", ClsSql.saveNullField(m.BIRTH));
            sb.AppendFormat(" where DC_ARMY_MEMBER_ID= '{0}'", ClsSql.EncodeSql(m.DC_ARMY_MEMBER_ID));
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
        public static Message Del(DC_ARMY_MEMBER_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete DC_ARMY_MEMBER");
            sb.AppendFormat(" where DC_ARMY_MEMBER_ID= '{0}'", ClsSql.EncodeSql(m.DC_ARMY_MEMBER_ID));
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
        public static bool isExists(DC_ARMY_MEMBER_SW sw)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from DC_ARMY_MEMBER where 1=1");
            if (string.IsNullOrEmpty(sw.DC_ARMY_MEMBER_ID) == false)
                sb.AppendFormat(" where DC_ARMY_MEMBER_ID= '{0}'", ClsSql.EncodeSql(sw.DC_ARMY_MEMBER_ID));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }

        #endregion

        #region 获取记录
        /// <summary>
        /// 获取记录
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(DC_ARMY_MEMBER_SW sw)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat(" FROM      DC_ARMY_MEMBER");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.DC_ARMY_MEMBER_ID) == false)
                sb.AppendFormat(" AND DC_ARMY_MEMBER_ID = '{0}'", ClsSql.EncodeSql(sw.DC_ARMY_MEMBER_ID));
            if (string.IsNullOrEmpty(sw.DC_ARMY_ID) == false)
                sb.AppendFormat(" AND DC_ARMY_ID = '{0}'", ClsSql.EncodeSql(sw.DC_ARMY_ID));
            if (string.IsNullOrEmpty(sw.MEMBERNAME) == false)
                sb.AppendFormat(" AND MEMBERNAME like '%{0}%'", ClsSql.EncodeSql(sw.MEMBERNAME));
            if (string.IsNullOrEmpty(sw.SEX) == false)
                sb.AppendFormat(" AND SEX = '{0}'", ClsSql.EncodeSql(sw.SEX));
            string sql = "";
            sql = "SELECT DC_ARMY_MEMBER_ID, DC_ARMY_ID, MEMBERNAME, SEX, CONTACTS, BIRTH"
            + sb.ToString()
            + " order by MEMBERNAME";

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
        public static DataTable getDT(DC_ARMY_MEMBER_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendFormat(" FROM      DC_ARMY_MEMBER");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.DC_ARMY_MEMBER_ID) == false)
                sb.AppendFormat(" AND DC_ARMY_MEMBER_ID = '{0}'", ClsSql.EncodeSql(sw.DC_ARMY_MEMBER_ID));
            if (string.IsNullOrEmpty(sw.DC_ARMY_ID) == false)
                sb.AppendFormat(" AND DC_ARMY_ID = '{0}'", ClsSql.EncodeSql(sw.DC_ARMY_ID));
            if (string.IsNullOrEmpty(sw.MEMBERNAME) == false)
                sb.AppendFormat(" AND MEMBERNAME like '%{0}%'", ClsSql.EncodeSql(sw.MEMBERNAME));
            if (string.IsNullOrEmpty(sw.SEX) == false)
                sb.AppendFormat(" AND SEX = '{0}'", ClsSql.EncodeSql(sw.SEX));
            string
            sql = "SELECT DC_ARMY_MEMBER_ID, DC_ARMY_ID, MEMBERNAME, SEX, CONTACTS, BIRTH"
            + sb.ToString()
            + " order by MEMBERNAME";
            string sqlC = "select count(1) " + sb.ToString();
            total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
            sw.curPage = PagerCls.getCurPage(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
            DataSet ds = DataBaseClass.FullDataSet(sql, (sw.curPage - 1) * sw.pageSize, sw.pageSize, "a");
            return ds.Tables[0];
        }

        #endregion
    }
}
