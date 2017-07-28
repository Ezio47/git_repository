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
    /// 任务反馈
    /// </summary>
    public class TASK_FEEDBACK
    {
        #region 增加 删除 修改

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">model</param>
        /// <param name="TASK_INFOID">任务序号</param>
        /// <returns></returns>
        public static Message Add(TASK_FEEDBACK_Model m, string TASK_INFOID)
        {
            List<string> sqllist = new List<string>();
            var HID = m.HID.Split(',');
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  TASK_FEEDBACK(HID, TASK_INFOID, RECEIVETIME, ACCEPTTIME, FEEDBACKTIME, FEEDBACKCONTENT, FEEDBACKSTATUS)");           
            for (int i = 0; i < HID.Length; i++)
            {
                sb.AppendFormat(" select '{0}'", ClsSql.EncodeSql(HID[i]));//护林员id
                sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(TASK_INFOID));//任务id
                sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.RECEIVETIME));//接收时间
                sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ACCEPTTIME));
                sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.FEEDBACKTIME));
                sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.FEEDBACKCONTENT));
                sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.FEEDBACKSTATUS));
                sb.AppendFormat(" UNION ALL ");
            }

            string insertStr = sb.ToString();
            if (insertStr.Contains(" UNION ALL "))
            {
                insertStr = insertStr.Substring(0, insertStr.Length - 10);
                sqllist.Add(insertStr);
            }
            var j = DataBaseClass.ExecuteSqlTran(sqllist);
            if (j>0)
                return new Message(true, "添加成功！", "");
            else
                return new Message(false, "添加失败，请检查各输入框是否正确！", "");
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Update(TASK_FEEDBACK_Model m)
        {
            List<string> sqllist = new List<string>();
            if (!string.IsNullOrEmpty(m.OHID)||!string.IsNullOrEmpty(m.NHID))
            {
                if (!string.IsNullOrEmpty(m.OHID))
                {
                    var OHID = m.OHID.Split(',');
                    for (int i = 0; i < OHID.Length; i++)//更新原有护林员的状态
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("update  TASK_FEEDBACK set ");
                        //sb.AppendFormat(" HID= '{0}',", ClsSql.EncodeSql(OHID[i]));
                        sb.AppendFormat(" RECEIVETIME='{0}',", ClsSql.EncodeSql(m.RECEIVETIME));
                        sb.AppendFormat(" ACCEPTTIME= '{0}',", ClsSql.EncodeSql(m.ACCEPTTIME));
                        sb.AppendFormat(" FEEDBACKTIME= '{0}',", ClsSql.EncodeSql(m.FEEDBACKTIME));
                        sb.AppendFormat(" FEEDBACKCONTENT= '{0}',", ClsSql.EncodeSql(m.FEEDBACKCONTENT));
                        sb.AppendFormat(" FEEDBACKSTATUS= '{0}'", "-1");
                        sb.AppendFormat(" where TASK_INFOID = '{0}'", ClsSql.EncodeSql(m.TASK_INFOID));
                        sb.AppendFormat(" and HID = '{0}'", ClsSql.EncodeSql(OHID[i]));
                        sqllist.Add(sb.ToString());
                    }
                }
                if (!string.IsNullOrEmpty(m.NHID))
                {
                    var NHID = m.NHID.Split(',');
                    StringBuilder sbInsert = new StringBuilder();
                    sbInsert.AppendFormat("INSERT INTO  TASK_FEEDBACK(HID, TASK_INFOID, RECEIVETIME, ACCEPTTIME, FEEDBACKTIME, FEEDBACKCONTENT, FEEDBACKSTATUS)");
                    for (int i = 0; i < NHID.Length; i++)
                    {
                        sbInsert.AppendFormat(" select '{0}'", ClsSql.EncodeSql(NHID[i]));//护林员id
                        sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.TASK_INFOID));//任务id
                        sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.RECEIVETIME));//接收时间
                        sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ACCEPTTIME));
                        sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.FEEDBACKTIME));
                        sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.FEEDBACKCONTENT));
                        sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.FEEDBACKSTATUS));
                        sbInsert.AppendFormat(" UNION ALL ");
                    }

                    string insertStr = sbInsert.ToString();
                    if (insertStr.Contains(" UNION ALL "))
                    {
                        insertStr = insertStr.Substring(0, insertStr.Length - 10);
                        sqllist.Add(insertStr);
                    }
                }
                var j = DataBaseClass.ExecuteSqlTran(sqllist);
                if (j > 0)
                    return new Message(true, "修改成功！", "");
                else
                    return new Message(false, "修改失败，请检查各输入框是否正确！", "");
            }
            return new Message(true, "修改成功！", "");
            
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message Del(TASK_FEEDBACK_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete from  TASK_FEEDBACK");
            sb.AppendFormat(" where 1=1 ");
            if (string.IsNullOrEmpty(m.TASK_INFOID) == false)
                sb.AppendFormat(" AND TASK_INFOID = '{0}'", ClsSql.EncodeSql(m.TASK_INFOID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功！", "");
            else
                return new Message(false, "删除失败！", "");
        }

        #endregion

        #region 获取信息
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(TASK_FEEDBACK_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM      TASK_FEEDBACK");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.HID) == false)
                sb.AppendFormat(" AND HID = '{0}'", ClsSql.EncodeSql(sw.HID));
            if (string.IsNullOrEmpty(sw.TASK_INFOID) == false)
                sb.AppendFormat(" AND TASK_INFOID = '{0}'", ClsSql.EncodeSql(sw.TASK_INFOID));
            if (string.IsNullOrEmpty(sw.TASK_FEEDBACKID) == false)
                sb.AppendFormat(" AND TASK_FEEDBACKID = '{0}'", ClsSql.EncodeSql(sw.TASK_FEEDBACKID));
            if (string.IsNullOrEmpty(sw.RECEIVETIME) == false)
                sb.AppendFormat(" AND RECEIVETIME = '{0}'", ClsSql.EncodeSql(sw.RECEIVETIME));
            if (string.IsNullOrEmpty(sw.ACCEPTTIME) == false)
                sb.AppendFormat(" AND ACCEPTTIME = '{0}'", ClsSql.EncodeSql(sw.ACCEPTTIME));
            if (string.IsNullOrEmpty(sw.FEEDBACKTIME) == false)
                sb.AppendFormat(" AND FEEDBACKTIME = '{0}'", ClsSql.EncodeSql(sw.FEEDBACKTIME));
            if (string.IsNullOrEmpty(sw.FEEDBACKSTATUS) == false)
                sb.AppendFormat(" AND FEEDBACKSTATUS = '{0}'", ClsSql.EncodeSql(sw.FEEDBACKSTATUS));

            string sql = "SELECT TASK_FEEDBACKID,HID, TASK_INFOID, RECEIVETIME, ACCEPTTIME, FEEDBACKTIME, FEEDBACKCONTENT, FEEDBACKSTATUS"
                + sb.ToString()
                + " order by HID,TASK_INFOID desc";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }

        #endregion
    }
}
