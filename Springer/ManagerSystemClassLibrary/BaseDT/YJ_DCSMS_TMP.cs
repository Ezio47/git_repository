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
    /// 短信模板设置
    /// </summary>
    public class YJ_DCSMS_TMP
    {

        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(YJ_DCSMS_TMP_Model m)
        {
            if (string.IsNullOrEmpty(m.ORDERBY) == true)
                m.ORDERBY = "0";
            if (string.IsNullOrEmpty(m.ISENABLE) == true)
                m.ISENABLE = "0";

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  YJ_DCSMS_TMP( SMSGROUPNAME, SMSGROUPTYPE, DANGERCLASS, TMPCONTENT,SMSSENDUSERLIST, ORDERBY, ISENABLE)");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.SMSGROUPNAME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.SMSGROUPTYPE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.DANGERCLASS));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.TMPCONTENT));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.SMSSENDUSERLIST));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ORDERBY));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ISENABLE));

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
        public static Message Mdy(YJ_DCSMS_TMP_Model m)
        {
            //if (string.IsNullOrEmpty(m.ORDERBY) == true)
            //    m.ORDERBY = "0";
            //if (string.IsNullOrEmpty(m.ISENABLE) == true)
            //    m.ISENABLE = "0";
            StringBuilder sb = new StringBuilder();
            //HID, HNAME, SN, PHONE, SEX, BIRTH, ONSTATE, BYORGNO
            sb.AppendFormat("UPDATE YJ_DCSMS_TMP");
            sb.AppendFormat(" set ");
            sb.AppendFormat("TMPCONTENT='{0}'", ClsSql.EncodeSql(m.TMPCONTENT));
            sb.AppendFormat(",SMSSENDUSERLIST='{0}'", ClsSql.EncodeSql(m.SMSSENDUSERLIST));
            if (string.IsNullOrEmpty(m.SMSGROUPNAME) == false)
                sb.AppendFormat(",SMSGROUPNAME='{0}'", ClsSql.EncodeSql(m.SMSGROUPNAME));
            if (string.IsNullOrEmpty(m.SMSGROUPTYPE) == false)
                sb.AppendFormat(",SMSGROUPTYPE='{0}'", ClsSql.EncodeSql(m.SMSGROUPTYPE));
            if (string.IsNullOrEmpty(m.DANGERCLASS) == false)
                sb.AppendFormat(",DANGERCLASS='{0}'", ClsSql.EncodeSql(m.DANGERCLASS));
            if (string.IsNullOrEmpty(m.ORDERBY) == false)
                sb.AppendFormat(",ORDERBY='{0}'", ClsSql.EncodeSql(m.ORDERBY));
            if (string.IsNullOrEmpty(m.ISENABLE) == false)
            sb.AppendFormat(",ISENABLE='{0}'", ClsSql.EncodeSql(m.ISENABLE));
            if (string.IsNullOrEmpty(m.TID) == false)
                sb.AppendFormat(",TID='{0}'", ClsSql.EncodeSql(m.TID));

            sb.AppendFormat(" where YJ_DCSMS_TMPID= '{0}'", ClsSql.EncodeSql(m.YJ_DCSMS_TMPID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功！", "");
            else
                return new Message(false, "修改失败，请检查各输入框是否正确！", "");
        }

        #endregion

        #region 批量更改启用状态
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message MdyISENABLE(YJ_DCSMS_TMP_Model m)
        {
            if (string.IsNullOrEmpty(m.YJ_DCSMS_TMPID))
                return new Message(false, "修改失败，请选择要修改的记录！", "");
            StringBuilder sb = new StringBuilder();
            //HID, HNAME, SN, PHONE, SEX, BIRTH, ONSTATE, BYORGNO
            sb.AppendFormat("UPDATE YJ_DCSMS_TMP");
            sb.AppendFormat(" set ");
            sb.AppendFormat(" ISENABLE='{0}'", ClsSql.EncodeSql(m.ISENABLE));
            

            sb.AppendFormat(" where YJ_DCSMS_TMPID in ({0})", ClsSql.EncodeSql(m.YJ_DCSMS_TMPID));
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
        public static Message Del(YJ_DCSMS_TMP_Model m)
        {
            if (string.IsNullOrEmpty(m.YJ_DCSMS_TMPID))
                return new Message(false, "删除失败，请选择要删除的记录！", "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete YJ_DCSMS_TMP");
            sb.AppendFormat(" where YJ_DCSMS_TMPID in({0})", ClsSql.EncodeSql(m.YJ_DCSMS_TMPID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功！", "");
            else
                return new Message(false, "删除失败，请检查各输入框是否正确！", "");
        }

        #endregion

        #region 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(YJ_DCSMS_TMP_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT YJ_DCSMS_TMPID, SMSGROUPNAME, SMSGROUPTYPE, DANGERCLASS, TMPCONTENT, SMSSENDUSERLIST, ORDERBY, ISENABLE,TID");
            sb.AppendFormat(" FROM  YJ_DCSMS_TMP");
            sb.AppendFormat(" WHERE   1=1");
            if (!string.IsNullOrEmpty(sw.YJ_DCSMS_TMPID))//火险id
            {
                sb.AppendFormat(" AND  YJ_DCSMS_TMPID='{0}'", ClsSql.EncodeSql(sw.YJ_DCSMS_TMPID));
            }
            if (!string.IsNullOrEmpty(sw.DANGERCLASS))//火险等级
            {
                sb.AppendFormat(" AND  DANGERCLASS='{0}'", ClsSql.EncodeSql(sw.DANGERCLASS));
            }
            if (!string.IsNullOrEmpty(sw.ISENABLE))//是否启用
            {
                sb.AppendFormat(" AND  ISENABLE='{0}'", ClsSql.EncodeSql(sw.ISENABLE));
            }


            sb.AppendFormat(" ORDER BY DANGERCLASS,ORDERBY ");

            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }

        #endregion

    }
}
