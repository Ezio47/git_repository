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
    /// 交接班
    /// </summary>
    public class OD_HANDOVER
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(OD_HANDOVER_Model m)
        {
            if (m.ONDUTYTYPE == "-3")//如果是填写日志
            {
                if (isExists(new OD_HANDOVER_SW { ONDUTYTYPE = "-3", ONDUTYDATE = m.ONDUTYDATE }) == true)//先删除当天日志
                    DelDayLog(new OD_HANDOVER_Model { ONDUTYTYPE = "-3", ONDUTYDATE = m.ONDUTYDATE });
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("insert OD_HANDOVER(ONDUTYDATE, BYORGNO, ONDUTYUSERTYPE, ONDUTYTYPE, ONDUTYUSERID, OPTIME, OPCONTENT)");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.ONDUTYDATE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.BYORGNO));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ONDUTYUSERTYPE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ONDUTYTYPE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ONDUTYUSERID));
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
        public static Message Mdy(OD_HANDOVER_Model m)
        {
            
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("UPDATE OD_HANDOVER");
            sb.AppendFormat(" set ");
            sb.AppendFormat(" OPTIME='{0}'",PublicClassLibrary.ClsSwitch.SwitTM(DateTime.Now));
            if (string.IsNullOrEmpty(m.ONDUTYDATE)==false)
                sb.AppendFormat(",ONDUTYDATE='{0}'", ClsSql.EncodeSql(m.ONDUTYDATE));
            if (string.IsNullOrEmpty(m.BYORGNO) == false)
                sb.AppendFormat(",BYORGNO='{0}'", ClsSql.EncodeSql(m.BYORGNO));
            if (string.IsNullOrEmpty(m.ONDUTYUSERTYPE) == false)
                sb.AppendFormat(",ONDUTYUSERTYPE='{0}'", ClsSql.EncodeSql(m.ONDUTYUSERTYPE));
            if (string.IsNullOrEmpty(m.ONDUTYTYPE) == false)
                sb.AppendFormat(",ONDUTYTYPE='{0}'", ClsSql.EncodeSql(m.ONDUTYTYPE));
            if (string.IsNullOrEmpty(m.ONDUTYUSERID) == false)
                sb.AppendFormat(",ONDUTYUSERID='{0}'", ClsSql.EncodeSql(m.ONDUTYUSERID));
            if (string.IsNullOrEmpty(m.OPCONTENT) == false)
                sb.AppendFormat(",OPCONTENT='{0}'", ClsSql.EncodeSql(m.OPCONTENT));
            sb.AppendFormat(" where ODHID='{0}'", ClsSql.EncodeSql(m.ODHID));
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
        public static Message Del(OD_HANDOVER_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete OD_HANDOVER");
            sb.AppendFormat(" where ODHID='{0}'", ClsSql.EncodeSql(m.ODHID));
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
        public static Message DelDayLog(OD_HANDOVER_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete OD_HANDOVER");
            sb.AppendFormat(" where ONDUTYTYPE='{0}'", "-3");
            sb.AppendFormat(" and ONDUTYDATE='{0}'", m.ONDUTYDATE);
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
        public static bool isExists(OD_HANDOVER_SW sw)
        {
            //ONDUTYDATE, BYORGNO, ONDUTYUSERTYPE, ONDUTYTYPE, ONDUTYUSERID, OPTIME, OPCONTENT
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from OD_HANDOVER where 1=1");
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
                sb.AppendFormat(" and BYORGNO='{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            if (string.IsNullOrEmpty(sw.ODHID) == false)
                sb.AppendFormat(" and ODHID='{0}'", ClsSql.EncodeSql(sw.ODHID));
            if (string.IsNullOrEmpty(sw.ONDUTYUSERTYPE) == false)
                sb.AppendFormat(" and ONDUTYUSERTYPE='{0}'", ClsSql.EncodeSql(sw.ONDUTYUSERTYPE));
            if (string.IsNullOrEmpty(sw.ONDUTYTYPE) == false)
                sb.AppendFormat(" and ONDUTYTYPE='{0}'", ClsSql.EncodeSql(sw.ONDUTYTYPE));
            if (string.IsNullOrEmpty(sw.ONDUTYUSERID) == false)
                sb.AppendFormat(" and ONDUTYUSERID='{0}'", ClsSql.EncodeSql(sw.ONDUTYUSERID));
            if (string.IsNullOrEmpty(sw.ONDUTYDATE) == false)
                sb.AppendFormat(" and ONDUTYDATE='{0}'", ClsSql.EncodeSql(sw.ONDUTYDATE));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }

        #endregion

        #region 获取DT DataTable getDT(OD_HANDOVER_SW sw)
        /// <summary>
        /// 获取DT
        /// </summary>
        /// <param name="sw">参见OD_HANDOVER_SW</param>
        /// <returns>DT</returns>
        public static DataTable getDT(OD_HANDOVER_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT ODHID, ONDUTYDATE, BYORGNO, ONDUTYUSERTYPE, ONDUTYTYPE, ONDUTYUSERID, OPTIME,  OPCONTENT FROM      OD_HANDOVER");
            sb.AppendFormat(" WHERE 1=1");
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
                sb.AppendFormat(" AND BYORGNO='{0}'", sw.BYORGNO);
            if (string.IsNullOrEmpty(sw.ONDUTYDATE) == false)
                sb.AppendFormat(" AND ONDUTYDATE='{0}'", sw.ONDUTYDATE);
            if (string.IsNullOrEmpty(sw.ONDUTYUSERTYPE) == false)
                sb.AppendFormat(" AND ONDUTYUSERTYPE='{0}'", sw.ONDUTYUSERTYPE);
            if (string.IsNullOrEmpty(sw.ONDUTYTYPE) == false)
                sb.AppendFormat(" AND ONDUTYTYPE='{0}'", sw.ONDUTYTYPE);
            if (sw.isGetUPOne == "1")//获取上一班次交班信息
            {
                sb.Clear();
                sb.AppendFormat("SELECT top 1 ODHID, ONDUTYDATE, BYORGNO, ONDUTYUSERTYPE, ONDUTYTYPE, ONDUTYUSERID, OPTIME,  OPCONTENT FROM      OD_HANDOVER");
                sb.AppendFormat(" WHERE 1=1");
                if (string.IsNullOrEmpty(sw.BYORGNO) == false)
                    sb.AppendFormat(" AND BYORGNO='{0}'", sw.BYORGNO);
                if (string.IsNullOrEmpty(sw.ONDUTYDATE) == false)
                    sb.AppendFormat(" AND ONDUTYDATE<='{0}'", sw.ONDUTYDATE);
                if (string.IsNullOrEmpty(sw.ONDUTYTYPE) == false)
                    sb.AppendFormat(" AND ONDUTYTYPE='{0}'", sw.ONDUTYTYPE);
                sb.AppendFormat(" AND ODHID not in(select ODHID from OD_HANDOVER where BYORGNO='{0}' AND ONDUTYDATE='{1}' AND ONDUTYUSERTYPE='{2}' )", sw.BYORGNO, sw.ONDUTYDATE, sw.ONDUTYUSERTYPE);

                sb.AppendFormat(" ORDER BY ONDUTYDATE DESC, ONDUTYUSERTYPE DESC", "");
            }
            return DataBaseClass.FullDataSet(sb.ToString()).Tables[0];
        }

        #endregion
    }
}
