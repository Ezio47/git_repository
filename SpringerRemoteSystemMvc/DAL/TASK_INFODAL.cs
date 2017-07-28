using Springer.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Springer.DAL
{
    public class TASK_INFODAL
    {
        #region 获取信息
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public DataSet getDT(string TASK_INFOID, string TASKBEGINTIME, string TASKENDTIME, int TASKSTAUTS)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT TASK_INFOID,BYORGNO, TASKTITLE, TASKTYPE, TASKLEVEL, TASKSTAUTS, TASKSTARTTIME, TASKBEGINTIME, TASKENDTIME,TASKCONTENT");
            sb.AppendFormat(" FROM  TASK_INFO");
            sb.AppendFormat(" WHERE   1=1");
            sb.AppendFormat(" AND TASK_INFOID = '{0}'", TASK_INFOID);
            if (string.IsNullOrEmpty(TASKBEGINTIME) == false)
                sb.AppendFormat(" AND TASKBEGINTIME = '{0}'", TASKBEGINTIME);
            if (string.IsNullOrEmpty(TASKENDTIME) == false)
                sb.AppendFormat(" AND TASKENDTIME = '{0}'", TASKENDTIME);
            if (string.IsNullOrEmpty(TASKENDTIME) == false)
                sb.AppendFormat(" AND TASKSTAUTS = '{0}'", TASKSTAUTS);
            sb.AppendFormat("order by TASK_INFOID desc");
            return DbHelperSQL.Query(sb.ToString());
        }

        public bool update(string status,string taskinfoid)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("update  TASK_INFO set ");
            //if (string.IsNullOrEmpty(status) == false)
            sb.AppendFormat(" TASKSTAUTS= '{0}'", status);
            sb.AppendFormat(" where TASK_INFOID = '{0}'", taskinfoid);
            int j = DbHelperSQL.ExecuteSql(sb.ToString());
            if (j > 0)
                return true;
            else
                return false;
        }
        #endregion   
    }
}
