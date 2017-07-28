using Springer.Common;
using Springer.DBUtility;
using Springer.EntityModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Springer.DAL
{
    public class TASK_FEEDBACKDAL
    {
        #region 获取信息
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public  DataSet getDT(string HID)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT TASK_FEEDBACKID,HID, TASK_INFOID, RECEIVETIME, ACCEPTTIME, FEEDBACKTIME, FEEDBACKCONTENT, FEEDBACKSTATUS");
            sb.AppendFormat(" FROM  TASK_FEEDBACK");
            sb.AppendFormat(" WHERE   1=1");
            sb.AppendFormat(" AND HID = '{0}'", HID);
            sb.AppendFormat("order by TASK_INFOID desc");
            return DbHelperSQL.Query(sb.ToString());
        }

        public  int UpdateStatus(string hid,string taskinfoid,string accepttime) 
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("update  TASK_FEEDBACK set ");
            sb.AppendFormat(" ACCEPTTIME= '{0}',", accepttime);
            sb.AppendFormat(" FEEDBACKSTATUS= '{0}'", "1");
            sb.AppendFormat(" where TASK_INFOID = '{0}'", taskinfoid);
            sb.AppendFormat(" and HID = '{0}'",hid);

            int iRecord = DbHelperSQL.ExecuteSql(sb.ToString());
            return iRecord;
        }

        public int UpdateContent(string hid, string taskinfoid, string feedbacktime,string feedbackcontent)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("update  TASK_FEEDBACK set ");
            sb.AppendFormat(" FEEDBACKTIME= '{0}',", feedbacktime);
            sb.AppendFormat(" FEEDBACKCONTENT= '{0}',", feedbackcontent);
            sb.AppendFormat(" FEEDBACKSTATUS= '{0}'", "2");
            sb.AppendFormat(" where TASK_INFOID = '{0}'", taskinfoid);
            sb.AppendFormat(" and HID = '{0}'", hid);
            int iRecord = DbHelperSQL.ExecuteSql(sb.ToString());
            return iRecord;
        }

        public bool UpdateFeedback(TASK_FEEDBACK_Model m)
        {
            TASK_INFODAL dal = new TASK_INFODAL();
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("update  TASK_FEEDBACK set ");
            //sb.AppendFormat(" HID= '{0}',", ClsSql.EncodeSql(OHID[i]));
            if (string.IsNullOrEmpty(m.RECEIVETIME)==false)
                sb.AppendFormat(" RECEIVETIME='{0}',",m.RECEIVETIME);
            if (string.IsNullOrEmpty(m.ACCEPTTIME) == false)
                sb.AppendFormat(" ACCEPTTIME= '{0}',", m.ACCEPTTIME);
            if (string.IsNullOrEmpty(m.FEEDBACKTIME) == false)
                sb.AppendFormat(" FEEDBACKTIME= '{0}',", m.FEEDBACKTIME);
            if (string.IsNullOrEmpty(m.FEEDBACKCONTENT) == false)
                sb.AppendFormat(" FEEDBACKCONTENT= '{0}',", m.FEEDBACKCONTENT);
            if (string.IsNullOrEmpty(m.FEEDBACKSTATUS) == false)
                sb.AppendFormat(" FEEDBACKSTATUS= '{0}'", m.FEEDBACKSTATUS);
            sb.AppendFormat(" where TASK_INFOID = '{0}'", m.TASK_INFOID);
            sb.AppendFormat(" and HID = '{0}'", m.HID);
            int iRecord = DbHelperSQL.ExecuteSql(sb.ToString());

            //bool bln = true;
            //if ( m.FEEDBACKSTATUS == "2")//反馈 修改任务表中任务的状态为已反馈
            //{
            //     bln = dal.update("3", m.TASK_INFOID);
            //}
            
            if (iRecord>0)//if (iRecord > 0 && bln)
                return true;
            else
                return false;
        }

        #endregion   
    }
}
