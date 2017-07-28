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

namespace ManagerSystemClassLibrary.BaseDT.Duty
{
    /// <summary>
    /// 值班移交
    /// </summary>
    public class DUTY_HANDOVER
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(DUTY_HANDOVER_Model m)
        {
            if (m.DUTYTYPE == "-3")//如果是填写日志
            {
                if (isExists(new DUTY_HANDOVER_SW { DUTYTYPE = "-3", DUTYDATE = m.DUTYDATE }) == true)//先删除当天日志
                    DelDayLog(new DUTY_HANDOVER_Model { DUTYTYPE = "-3", DUTYDATE = m.DUTYDATE });
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("insert DUTY_HANDOVER(DUTYDATE, BYORGNO, DUTYUSERTYPE, DUTYTYPE, DUTYUSERID, OPTIME, OPCONTENT)");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.DUTYDATE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.BYORGNO));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.DUTYUSERTYPE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.DUTYTYPE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.DUTYUSERID));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.OPTIME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.OPCONTENT));
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
        public static Message Mdy(DUTY_HANDOVER_Model m)
        {

            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("UPDATE DUTY_HANDOVER");
            sb.AppendFormat(" set ");
            sb.AppendFormat(" OPTIME='{0}'", PublicClassLibrary.ClsSwitch.SwitTM(DateTime.Now));
            if (string.IsNullOrEmpty(m.DUTYDATE) == false)
                sb.AppendFormat(",DUTYDATE='{0}'", ClsSql.EncodeSql(m.DUTYDATE));
            if (string.IsNullOrEmpty(m.BYORGNO) == false)
                sb.AppendFormat(",BYORGNO='{0}'", ClsSql.EncodeSql(m.BYORGNO));
            if (string.IsNullOrEmpty(m.DUTYUSERTYPE) == false)
                sb.AppendFormat(",DUTYUSERTYPE='{0}'", ClsSql.EncodeSql(m.DUTYUSERTYPE));
            if (string.IsNullOrEmpty(m.DUTYTYPE) == false)
                sb.AppendFormat(",DUTYTYPE='{0}'", ClsSql.EncodeSql(m.DUTYTYPE));
            if (string.IsNullOrEmpty(m.DUTYUSERID) == false)
                sb.AppendFormat(",DUTYUSERID='{0}'", ClsSql.EncodeSql(m.DUTYUSERID));
            if (string.IsNullOrEmpty(m.OPCONTENT) == false)
                sb.AppendFormat(",OPCONTENT='{0}'", ClsSql.EncodeSql(m.OPCONTENT));
            sb.AppendFormat(" where ODHID='{0}'", ClsSql.EncodeSql(m.DHID));
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
        public static Message Del(DUTY_HANDOVER_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete DUTY_HANDOVER");
            sb.AppendFormat(" where DHID='{0}'", ClsSql.EncodeSql(m.DHID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功！", "");
            else
                return new Message(false, "删除失败，请检查各输入框是否正确！", "");
        }

        #endregion

        #region 删除日志
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message DelDayLog(DUTY_HANDOVER_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete DUTY_HANDOVER");
            sb.AppendFormat(" where DUTYTYPE='{0}'", "-3");
            sb.AppendFormat(" and DUTYDATE='{0}'", m.DUTYDATE);
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
        public static bool isExists(DUTY_HANDOVER_SW sw)
        {
            //DUTYDATE, BYORGNO, DUTYUSERTYPE, DUTYTYPE, DUTYUSERID, OPTIME, OPCONTENT
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from DUTY_HANDOVER where 1=1");
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
                sb.AppendFormat(" and BYORGNO='{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            if (string.IsNullOrEmpty(sw.DHID) == false)
                sb.AppendFormat(" and DHID='{0}'", ClsSql.EncodeSql(sw.DHID));
            if (string.IsNullOrEmpty(sw.DUTYUSERTYPE) == false)
                sb.AppendFormat(" and DUTYUSERTYPE='{0}'", ClsSql.EncodeSql(sw.DUTYUSERTYPE));
            if (string.IsNullOrEmpty(sw.DUTYTYPE) == false)
                sb.AppendFormat(" and DUTYTYPE='{0}'", ClsSql.EncodeSql(sw.DUTYTYPE));
            if (string.IsNullOrEmpty(sw.DUTYUSERID) == false)
                sb.AppendFormat(" and DUTYUSERID='{0}'", ClsSql.EncodeSql(sw.DUTYUSERID));
            if (string.IsNullOrEmpty(sw.DUTYDATE) == false)
                sb.AppendFormat(" and DUTYDATE='{0}'", ClsSql.EncodeSql(sw.DUTYDATE));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }

        #endregion

        #region 获取DT DataTable getDT(DUTY_HANDOVER_SW sw)
        /// <summary>
        /// 获取DT
        /// </summary>
        /// <param name="sw">参见OD_HANDOVER_SW</param>
        /// <returns>DT</returns>
        public static DataTable getDT(DUTY_HANDOVER_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT DHID, DUTYDATE, BYORGNO, DUTYUSERTYPE, DUTYTYPE, DUTYUSERID, OPTIME,  OPCONTENT FROM      DUTY_HANDOVER");
            sb.AppendFormat(" WHERE 1=1");
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
                sb.AppendFormat(" AND BYORGNO='{0}'", sw.BYORGNO);
            if (string.IsNullOrEmpty(sw.DUTYDATE) == false)
                sb.AppendFormat(" AND DUTYDATE='{0}'", sw.DUTYDATE);
            if (string.IsNullOrEmpty(sw.DUTYUSERTYPE) == false)
                sb.AppendFormat(" AND DUTYUSERTYPE='{0}'", sw.DUTYUSERTYPE);
            if (string.IsNullOrEmpty(sw.DUTYTYPE) == false)
                sb.AppendFormat(" AND DUTYTYPE='{0}'", sw.DUTYTYPE);
            if (sw.isGetUPOne == "1")//获取上一班次交班信息
            {
                sb.Clear();
                sb.AppendFormat("SELECT top 1 DHID, DUTYDATE, BYORGNO, DUTYUSERTYPE, DUTYTYPE, DUTYUSERID, OPTIME,  OPCONTENT FROM      DUTY_HANDOVER");
                sb.AppendFormat(" WHERE 1=1");
                if (string.IsNullOrEmpty(sw.BYORGNO) == false)
                    sb.AppendFormat(" AND BYORGNO='{0}'", sw.BYORGNO);
                if (string.IsNullOrEmpty(sw.DUTYDATE) == false)
                    sb.AppendFormat(" AND DUTYDATE<='{0}'", sw.DUTYDATE);
                if (string.IsNullOrEmpty(sw.DUTYTYPE) == false)
                    sb.AppendFormat(" AND DUTYTYPE='{0}'", sw.DUTYTYPE);
                sb.AppendFormat(" AND DHID not in(select DHID from DUTY_HANDOVER where BYORGNO='{0}' AND DUTYDATE='{1}' AND DUTYUSERTYPE='{2}' )", sw.BYORGNO, sw.DUTYDATE, sw.DUTYUSERTYPE);

                sb.AppendFormat(" ORDER BY DUTYDATE DESC, DUTYUSERTYPE DESC", "");
            }
            return DataBaseClass.FullDataSet(sb.ToString()).Tables[0];
        }

        #endregion
    }
}
