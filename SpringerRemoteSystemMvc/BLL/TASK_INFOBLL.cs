using Springer.DAL;
using Springer.EntityModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Springer.BLL
{
    public class TASK_INFOBLL
    {
        private readonly TASK_INFODAL dal = new TASK_INFODAL();
        #region 获取列表
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <param name="total">记录总数</param>
        /// <returns>参见模型</returns>
        public  List<TASK_INFO_Model> getModelList(string TASK_INFOID,string TASKBEGINTIME,string TASKENDTIME,int STATUS)
        {
            var result = new List<TASK_INFO_Model>();
            
            DataSet ds = dal.getDT(TASK_INFOID,TASKBEGINTIME, TASKENDTIME,STATUS);
            DataTable dt = ds.Tables[0];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TASK_INFO_Model m = new TASK_INFO_Model();
                m.TASK_INFOID = dt.Rows[i]["TASK_INFOID"].ToString();
                m.TASKTITLE = dt.Rows[i]["TASKTITLE"].ToString();
                m.TASKTYPE = dt.Rows[i]["TASKTYPE"].ToString();
                m.TASKLEVEL = dt.Rows[i]["TASKLEVEL"].ToString();
                m.TASKSTAUTS = dt.Rows[i]["TASKSTAUTS"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                //m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                m.TASKSTARTTIME = dt.Rows[i]["TASKSTARTTIME"].ToString();
                m.TASKBEGINTIME = dt.Rows[i]["TASKBEGINTIME"].ToString();
                m.TASKENDTIME = dt.Rows[i]["TASKENDTIME"].ToString();
                m.TASKCONTENT = dt.Rows[i]["TASKCONTENT"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion
    }
}
