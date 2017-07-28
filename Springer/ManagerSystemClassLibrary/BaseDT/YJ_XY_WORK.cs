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
    /// 预警响应相关工作表
    /// </summary>
    public class YJ_XY_WORK
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(YJ_XY_WORK_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  YJ_XY_WORK( DANGERCLASS, YJXYDEPT, YJXYCONTENT)");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.DANGERCLASS));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.YJXYDEPT));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.YJXYCONTENT));

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
        public static Message Mdy(YJ_XY_WORK_Model m)
        {
            StringBuilder sb = new StringBuilder();
            //HID, HNAME, SN, PHONE, SEX, BIRTH, ONSTATE, BYORGNO
            sb.AppendFormat("UPDATE YJ_XY_WORK");
            sb.AppendFormat(" set "); 
            //sb.AppendFormat("DANGERCLASS='{0}'", ClsSql.EncodeSql(m.DANGERCLASS));
            //sb.AppendFormat(",YJXYDEPT='{0}'", ClsSql.EncodeSql(m.YJXYDEPT));
            sb.AppendFormat("YJXYCONTENT='{0}'", ClsSql.EncodeSql(m.YJXYCONTENT));
            sb.AppendFormat(" where YJXYID= '{0}'", ClsSql.EncodeSql(m.YJXYID));
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
        public static Message Del(YJ_XY_WORK_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete YJ_XY_WORK");
            sb.AppendFormat(" where YJXYID= '{0}'", ClsSql.EncodeSql(m.YJXYID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功！", "");
            else
                return new Message(false, "删除失败，请检查各输入框是否正确！", "");
        }

        #endregion

        #region 判断记录是否存在
        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>true存在 false不存在</returns>
        public static bool isExists(YJ_XY_WORK_SW sw)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from YJ_XY_WORK where 1=1");
            if (string.IsNullOrEmpty(sw.YJXYID) == false)
                sb.AppendFormat(" where YJXYID= '{0}'", ClsSql.EncodeSql(sw.YJXYID));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }

        #endregion

        #region 获取记录
        /// <summary>
        /// 获取记录
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(YJ_XY_WORK_SW sw)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat(" FROM      YJ_XY_WORK");
            sb.AppendFormat(" WHERE   1=1");
            
              string  sql = "SELECT  YJXYID, DANGERCLASS, YJXYDEPT, YJXYCONTENT"
                   + sb.ToString()
                   + " order by DANGERCLASS ,YJXYDEPT";

            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }

        #endregion

        #region 根据等级、部门获取响应措施
        /// <summary>
        /// 根据等级、部门获取响应措施
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="DANGERCLASS">火险等级</param>
        /// <param name="YJXYDEPT">响应部门</param>
        /// <returns>响应措施</returns>
        public static string getContent(DataTable dt,string DANGERCLASS,string YJXYDEPT)
        {
            DataRow[] dr= dt.Select("DANGERCLASS='" + DANGERCLASS + "' and YJXYDEPT='"+YJXYDEPT+"'", "");
            if (dr.Length > 0)
                return dr[0]["YJXYCONTENT"].ToString();
            else
                return "";
        }

        #endregion
        #region 根据等级、部门获取序号
        /// <summary>
        /// 根据等级、部门获取响应措施
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="DANGERCLASS">火险等级</param>
        /// <param name="YJXYDEPT">响应部门</param>
        /// <returns>响应措施</returns>
        public static string getID(DataTable dt, string DANGERCLASS, string YJXYDEPT)
        {
            DataRow[] dr = dt.Select("DANGERCLASS='" + DANGERCLASS + "' and YJXYDEPT='" + YJXYDEPT + "'", "");
            if (dr.Length > 0)
                return dr[0]["YJXYID"].ToString();
            else
                return "";
        }

        #endregion
    }
}
