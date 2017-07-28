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
    /// 热点管理类
    /// </summary>
    public class T_IPS_HOTS
    {

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(T_IPS_HOTS_Model m)
        {
            if (string.IsNullOrEmpty(m.HOTSID))
                return new Message(false, "删除失败，请选择要处理的记录！", "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("DELETE FROM T_IPS_HOTS");
            sb.AppendFormat(" where  HOTSID= '{0}'", ClsSql.EncodeSql(m.HOTSID));

            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功！", "");
            else
                return new Message(false, "删除失败，请检查各输入框是否正确！", "");
        }

        /// <summary>
        /// 报警处理
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Man(T_IPS_HOTS_Model m)
        {
            if (string.IsNullOrEmpty(m.HOTSID))
                return new Message(false, "修改失败，请选择要处理的记录！", "");
            StringBuilder sb = new StringBuilder();
            //(ORGNO,LOGINUSERNAME,USERNAME,USERPWD,REGISTERTIME,LOGINNUM,NOTE)
            sb.AppendFormat("UPDATE T_IPS_HOTS");
            sb.AppendFormat(" set ");
            sb.AppendFormat(" MANSTATE='{0}'", "1");
            sb.AppendFormat(" ,MANRESULT='{0}'", ClsSql.EncodeSql(m.MANRESULT));
            sb.AppendFormat(" ,MANTIME='{0}'", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sb.AppendFormat(" ,MANUSERID='{0}'", SystemCls.getUserID());
            sb.AppendFormat(" where  HOTSID= '{0}'", ClsSql.EncodeSql(m.HOTSID));

            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "处理成功！", "");
            else
                return new Message(false, "处理失败，请检查各输入框是否正确！", "");
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>参见模型</returns>

        public static DataTable getDT(T_IPS_HOTS_SW sw)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT HOTSID, BH, WXBH, DQRDBH, HLY, ZQWZ, JD, WD, XS, DL, YY, JXHQSJ, FXSJ, SBSJ, BZW, FJBH, WLBH,  XZQH, CZW, MANSTATE, MANRESULT, MANTIME, MANUSERID");
            sb.AppendFormat(" FROM T_IPS_HOTS");
            sb.AppendFormat(" WHERE   1=1");
            //判断是否是多少，如果是多少，用in，如果只是一个，用=  主要考虑到速度
            if (string.IsNullOrEmpty(sw.HOTSID) == false)
            {
                if (sw.HOTSID.Split(',').Length > 1)
                    sb.AppendFormat(" AND HOTSID in({0})", ClsSql.EncodeSql(sw.HOTSID));
                else
                    sb.AppendFormat(" AND HOTSID ='{0}'", ClsSql.EncodeSql(sw.HOTSID));
            }

            if (string.IsNullOrEmpty(sw.MANSTATE) == false)
                sb.AppendFormat(" AND MANSTATE ='{0}'", ClsSql.EncodeSql(sw.MANSTATE));

            if (!string.IsNullOrEmpty(sw.DateBegin))
            {
                sb.AppendFormat(" AND FXSJ>='{0} 00:00:00'", sw.DateBegin);
            }
            if (!string.IsNullOrEmpty(sw.DateEnd))
            {
                sb.AppendFormat(" AND FXSJ<='{0} 23:59:59'", sw.DateEnd);
            }
            sb.AppendFormat(" AND XS is not null ");
            sb.AppendFormat(" ORDER BY FXSJ DESC ");

            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
    }
}
