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
    /// 任务信息
    /// </summary>
    public class TASK_INFO
    {
        #region 增加 删除 修改

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(TASK_INFO_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  TASK_INFO(BYORGNO, TASKTITLE, TASKTYPE, TASKLEVEL, TASKSTAUTS, TASKSTARTTIME, TASKBEGINTIME,TASKENDTIME,TASKCONTENT)");
            sb.AppendFormat("output inserted.TASK_INFOID ");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.BYORGNO));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.TASKTITLE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.TASKTYPE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.TASKLEVEL));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.TASKSTAUTS));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.TASKSTARTTIME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.TASKBEGINTIME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.TASKENDTIME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.TASKCONTENT));
            sb.AppendFormat(")");
            var strid = DataBaseClass.ReturnSqlField(sb.ToString());//返回主键id
            if (!string.IsNullOrEmpty(strid))
                return new Message(true, "添加成功！", strid);
            else
                return new Message(false, "添加失败，请检查各输入框是否正确！", "");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message Del(TASK_INFO_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete from  TASK_INFO");
            sb.AppendFormat(" where 1=1 ");
            if (string.IsNullOrEmpty(m.TASK_INFOID) == false)
                sb.AppendFormat(" AND TASK_INFOID = '{0}'", ClsSql.EncodeSql(m.TASK_INFOID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功！", "");
            else
                return new Message(false, "删除失败！", "");
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Update(TASK_INFO_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("update  TASK_INFO set ");
            //sb.AppendFormat(" TASK_INFOID= '{0}',", ClsSql.EncodeSql(m.TASK_INFOID));
            if (string.IsNullOrEmpty(m.BYORGNO) == false) 
                sb.AppendFormat(" BYORGNO= '{0}',", ClsSql.EncodeSql(m.BYORGNO));
            if (string.IsNullOrEmpty(m.TASKTITLE) == false) 
                sb.AppendFormat(" TASKTITLE='{0}',", ClsSql.EncodeSql(m.TASKTITLE));
            if (string.IsNullOrEmpty(m.TASKTYPE) == false) 
                sb.AppendFormat(" TASKTYPE= '{0}',", ClsSql.EncodeSql(m.TASKTYPE));
            if (string.IsNullOrEmpty(m.TASKLEVEL) == false) 
                sb.AppendFormat(" TASKLEVEL= '{0}',", ClsSql.EncodeSql(m.TASKLEVEL));
            if (string.IsNullOrEmpty(m.TASKSTARTTIME) == false) 
                sb.AppendFormat(" TASKSTARTTIME= '{0}',", ClsSql.EncodeSql(m.TASKSTARTTIME));
            if (string.IsNullOrEmpty(m.TASKBEGINTIME) == false)
                sb.AppendFormat(" TASKBEGINTIME= '{0}',", ClsSql.EncodeSql(m.TASKBEGINTIME));
            if (string.IsNullOrEmpty(m.TASKENDTIME) == false) 
                sb.AppendFormat(" TASKENDTIME= '{0}',", ClsSql.EncodeSql(m.TASKENDTIME));
            if (string.IsNullOrEmpty(m.TASKCONTENT) == false) 
                sb.AppendFormat(" TASKCONTENT= '{0}',", ClsSql.EncodeSql(m.TASKCONTENT));
            if (string.IsNullOrEmpty(m.TASKSTAUTS) == false)
                sb.AppendFormat(" TASKSTAUTS= '{0}'", ClsSql.EncodeSql(m.TASKSTAUTS));
            sb.AppendFormat(" where TASK_INFOID = '{0}'", ClsSql.EncodeSql(m.TASK_INFOID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功！", "");
            else
                return new Message(false, "修改失败！", "");
        }
        #endregion

        #region 获取一条信息
        /// <summary>
        /// 获取一条信息
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(TASK_INFO_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM      TASK_INFO");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.TASKSTAUTS) == false)
                sb.AppendFormat(" AND TASKSTAUTS = '{0}'", ClsSql.EncodeSql(sw.TASKSTAUTS));
            if (string.IsNullOrEmpty(sw.TASK_INFOID) == false)
                sb.AppendFormat(" AND TASK_INFOID = '{0}'", ClsSql.EncodeSql(sw.TASK_INFOID));
            if (string.IsNullOrEmpty(sw.TASKTITLE) == false)
                sb.AppendFormat(" AND TASKTITLE like '%{0}%'", ClsSql.EncodeSql(sw.TASKTITLE));
            if (!string.IsNullOrEmpty(sw.BYORGNO))
            {
                if (sw.BYORGNO.Substring(4, 5) == "00000")//获取所有市的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,4) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
                else if (sw.BYORGNO.Substring(4, 5) == "xxxxx")//单独市
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,9) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4) + "00000"));
                else if (sw.BYORGNO.Substring(6, 3) == "xxx")//获取所有县的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,6) = '{0}' )", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
                else
                    sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            }
            string sql = "SELECT TASK_INFOID,BYORGNO, TASKTITLE, TASKTYPE, TASKLEVEL, TASKSTAUTS, TASKSTARTTIME, TASKBEGINTIME, TASKENDTIME,TASKCONTENT"
                + sb.ToString()
                + " order by BYORGNO,TASK_INFOID desc";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }

        #endregion

        #region 获取分页列表
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static DataTable getDT(TASK_INFO_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM      TASK_INFO");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.TASKSTAUTS) == false)
                sb.AppendFormat(" AND TASKSTAUTS = '{0}'", ClsSql.EncodeSql(sw.TASKSTAUTS));
            if (string.IsNullOrEmpty(sw.TASKTITLE) == false)
                sb.AppendFormat(" AND TASKTITLE like '%{0}%'", ClsSql.EncodeSql(sw.TASKTITLE));
            if (!string.IsNullOrEmpty(sw.BYORGNO))
            {
                if (sw.BYORGNO.Substring(4, 5) == "00000")//获取所有市的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,4) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
                else if (sw.BYORGNO.Substring(4, 5) == "xxxxx")//单独市
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,9) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4) + "00000"));
                else if (sw.BYORGNO.Substring(6, 3) == "xxx")//获取所有县的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,6) = '{0}' )", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
                else
                    sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            }
            string sql = "SELECT TASK_INFOID,BYORGNO, TASKTITLE, TASKTYPE, TASKLEVEL, TASKSTAUTS, TASKSTARTTIME, TASKBEGINTIME, TASKENDTIME,TASKCONTENT"
                + sb.ToString()
                + " order by TASK_INFOID desc";
            string sqlC = "select count(1) " + sb.ToString();
            total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
            sw.curPage = PagerCls.getCurPage(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
            DataSet ds = DataBaseClass.FullDataSet(sql, (sw.curPage - 1) * sw.pageSize, sw.pageSize, "a");
            return ds.Tables[0];
        }

        #endregion
    }
}
