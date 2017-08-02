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
    /// 队伍装备表
    /// </summary>
    public class DC_ARMY_EQUIP
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(DC_ARMY_EQUIP_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  DC_ARMY_EQUIP( DC_ARMY_ID, EQUIPNAME, EQUIPNUM, EQUIPMODEL, EQUIPUSESTATE,EQUIPSUM)");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("{0}", ClsSql.saveNullField(m.DC_ARMY_ID));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.EQUIPNAME));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.EQUIPNUM));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.EQUIPMODEL));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.EQUIPUSESTATE));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.EQUIPSUM));
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
        public static Message Mdy(DC_ARMY_EQUIP_Model m)
        {
            StringBuilder sb = new StringBuilder();
            //HID, HNAME, SN, PHONE, SEX, BIRTH, ONSTATE, BYORGNO
            sb.AppendFormat("UPDATE DC_ARMY_EQUIP");
            sb.AppendFormat(" set ");
            sb.AppendFormat("DC_ARMY_ID={0}", ClsSql.saveNullField(m.DC_ARMY_ID));
            sb.AppendFormat(",EQUIPNAME={0}", ClsSql.saveNullField(m.EQUIPNAME));
            sb.AppendFormat(",EQUIPNUM={0}", ClsSql.saveNullField(m.EQUIPNUM));
            sb.AppendFormat(",EQUIPMODEL={0}", ClsSql.saveNullField(m.EQUIPMODEL));
            sb.AppendFormat(",EQUIPUSESTATE={0}", ClsSql.saveNullField(m.EQUIPUSESTATE));
            sb.AppendFormat(",EQUIPSUM={0}", ClsSql.saveNullField(m.EQUIPSUM));
            sb.AppendFormat(" where DC_ARMY_EQUIP_ID= '{0}'", ClsSql.EncodeSql(m.DC_ARMY_EQUIP_ID));
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
        public static Message Del(DC_ARMY_EQUIP_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete DC_ARMY_EQUIP");
            sb.AppendFormat(" where DC_ARMY_EQUIP_ID= '{0}'", ClsSql.EncodeSql(m.DC_ARMY_EQUIP_ID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功！", "");
            else
                return new Message(false, "删除失败，请检查各输入框是否正确！", "");
        }

        #endregion

        #region 获取记录
        /// <summary>
        /// 获取记录
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(DC_ARMY_EQUIP_SW sw)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat(" FROM      DC_ARMY_EQUIP");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.DC_ARMY_EQUIP_ID) == false)
                sb.AppendFormat(" AND DC_ARMY_EQUIP_ID = '{0}'", ClsSql.EncodeSql(sw.DC_ARMY_EQUIP_ID));
            if (string.IsNullOrEmpty(sw.DC_ARMY_ID) == false)
                sb.AppendFormat(" AND DC_ARMY_ID = '{0}'", ClsSql.EncodeSql(sw.DC_ARMY_ID));
            if (string.IsNullOrEmpty(sw.EQUIPNAME) == false)
                sb.AppendFormat(" AND EQUIPNAME = '{0}'", ClsSql.EncodeSql(sw.EQUIPNAME));
            if (string.IsNullOrEmpty(sw.EQUIPNUM) == false)
                sb.AppendFormat(" AND EQUIPNUM = '{0}'", ClsSql.EncodeSql(sw.EQUIPNUM));
            if (string.IsNullOrEmpty(sw.EQUIPMODEL) == false)
                sb.AppendFormat(" AND EQUIPMODEL = '{0}'", ClsSql.EncodeSql(sw.EQUIPMODEL));
            if (string.IsNullOrEmpty(sw.EQUIPUSESTATE) == false)
                sb.AppendFormat(" AND EQUIPUSESTATE = '{0}'", ClsSql.EncodeSql(sw.EQUIPUSESTATE));
            if (string.IsNullOrEmpty(sw.EQUIPSUM) == false)
                sb.AppendFormat(" AND EQUIPSUM = '{0}'", ClsSql.EncodeSql(sw.EQUIPSUM));
            string sql = "";
            sql = "SELECT DC_ARMY_EQUIP_ID, DC_ARMY_ID,EQUIPNAME,EQUIPNUM, EQUIPMODEL, EQUIPUSESTATE, EQUIPSUM"
            + sb.ToString()
            + " order by DC_ARMY_ID";

            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }

        #endregion
    }
}
