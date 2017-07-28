using DataBaseClassLibrary;
using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using PublicClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary.BaseDT
{
    /// <summary>
    /// 短信发送
    /// </summary>
    public class YJ_DCSMS_SEND
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(YJ_DCSMS_SEND_SW m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  YJ_DCSMS_SEND (YJ_DCSMS_TMPID,TMPCONTENT,SMSSENDUSERLIST,DCDATE,BYORGNO,SMSSENDSTATUS)");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.YJ_DCSMS_TMPID));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.TMPCONTENT));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.SMSSENDUSERLIST));
            sb.AppendFormat(",'{0}'", ClsSwitch.SwitTM(m.DCDATE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.BYORGNO));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.SMSSENDSTATUS));
            sb.AppendFormat(")");
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "添加成功！", "");
            else
                return new Message(false, "添加失败！", "");
        }
    }
}
