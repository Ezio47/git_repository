using DataBaseClassLibrary;
using ManagerSystemModel;
using PublicClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary.BaseDT
{
    /// <summary>
    /// 系统管理_任务流转
    /// </summary>
    public class TASK_TURNOVER
    {
        #region 增加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <param name="TASK_INFOID">任务信息ID</param>
        /// <returns>参见模型</returns>
        public static Message Add(TASK_TURNOVER_Model m, string TASK_INFOID)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  TASK_TURNOVER(TASK_INFOID, OPUID, OPTIME, OPSTATUS, OPTITLE, OPIP)");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(TASK_INFOID));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.OPUID));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.OPTIME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.OPSTATUS));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.OPTITLE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.OPIP));
            sb.AppendFormat(")");
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "添加成功！", "");
            else
                return new Message(false, "添加失败，请检查各输入框是否正确！", "");

        }
        #endregion
    }
}
