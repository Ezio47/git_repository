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
    /// 值班管理表
    /// </summary>
    public class DUTY_CLASS
    {
        #region 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable GetDT(DUTY_CLASS_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select BYORGNO,DUTYCLASSID,DUTYCLASSNAME,DUTYBEGINTIME,DUTYENDTIME from DUTY_CLASS");
            sb.AppendFormat(" Where 1=1");
            if (string.IsNullOrEmpty(sw.DUTYCLASSID) == false)
                sb.AppendFormat(" AND DUTYCLASSID='{0}'", sw.DUTYCLASSID);
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
                sb.AppendFormat(" AND BYORGNO='{0}'", sw.BYORGNO);
            sb.AppendFormat(" ORDER BY DUTYCLASSID");
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        } 
        #endregion

        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(DUTY_CLASS_Model m)
        {
            StringBuilder sb = new StringBuilder();

            if (isExists(new DUTY_CLASS_SW { BYORGNO = m.BYORGNO, DUTYCLASSID = m.DUTYCLASSID }) == true)//该条记录存在 
            //return new Message(false, "添加失败，该组织机构码已存在！", "");
            {
                return new Message(false, "添加失败，已存在该班次！", "");
                //sb.AppendFormat("UPDATE DUTY_CLASS SET ");
                //sb.AppendFormat(" DUTYBEGINTIME='{0}',", ClsSql.EncodeSql(m.DUTYBEGINTIME));
                //sb.AppendFormat(" DUTYENDTIME='{0}'", ClsSql.EncodeSql(m.DUTYENDTIME));
                //sb.AppendFormat(" where BYORGNO= '{0}'", ClsSql.EncodeSql(m.BYORGNO));
                //sb.AppendFormat(" and DUTYCLASSID= '{0}'", ClsSql.EncodeSql(m.DUTYCLASSID));
            }
            else {
                sb.AppendFormat("INSERT INTO DUTY_CLASS(BYORGNO, DUTYCLASSID, DUTYCLASSNAME, DUTYBEGINTIME, DUTYENDTIME)");
                sb.AppendFormat("VALUES(");
                sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.BYORGNO));
                sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.DUTYCLASSID));
                sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.DUTYCLASSNAME));
                sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.DUTYBEGINTIME));
                sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.DUTYENDTIME));
                sb.AppendFormat(")");
            }
            
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "保存成功！", "");
            else
                return new Message(false, "保存失败，请检查各输入框是否正确！", "");
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message Del(DUTY_CLASS_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete from DUTY_CLASS");
            sb.AppendFormat(" where DUTYCLASSID= '{0}'", ClsSql.EncodeSql(m.DUTYCLASSID));
            sb.AppendFormat(" and BYORGNO= '{0}'", ClsSql.EncodeSql(m.BYORGNO));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功！", "");
            else
                return new Message(false, "删除失败！", "");
        }
        #endregion

        #region 判断记录是否存在
        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>true存在 false不存在</returns>
        public static bool isExists(DUTY_CLASS_SW sw)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from DUTY_CLASS where 1=1");
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
                sb.AppendFormat(" and BYORGNO='{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            if (string.IsNullOrEmpty(sw.DUTYCLASSID) == false) { 
                sb.AppendFormat(" and DUTYCLASSID='{0}'", ClsSql.EncodeSql(sw.DUTYCLASSID));
            }
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }

        #endregion
    }
}
