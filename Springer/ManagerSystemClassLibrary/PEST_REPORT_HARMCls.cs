using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 有害生物_报表_成灾表
    /// </summary>
    public class PEST_REPORT_HARMCls
    {
        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(PEST_REPORT_HARM_Model m)
        {
            return BaseDT.PEST_REPORT_HARM.Save(m);
        }
        #endregion

        #region 获取单条记录
        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static PEST_REPORT_HARM_Model getModel(PEST_REPORT_HARM_SW sw)
        {
            DataTable dt = BaseDT.PEST_REPORT_HARM.getDT(sw);
            PEST_REPORT_HARM_Model m = new PEST_REPORT_HARM_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                //数据库表字段
                m.PEST_REPORT_HARMID = dt.Rows[i]["PEST_REPORT_HARMID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.HAPPENYEAR = dt.Rows[i]["HAPPENYEAR"].ToString();
                m.HAPPENMONTH = dt.Rows[i]["HAPPENMONTH"].ToString();
                m.PESTBYCODE = dt.Rows[i]["PESTBYCODE"].ToString();
                m.DISASTERAREA = dt.Rows[i]["DISASTERAREA"].ToString();
                m.FORECASTDISASTERAREA = dt.Rows[i]["FORECASTDISASTERAREA"].ToString();
                m.DIEPLATECOUNT = dt.Rows[i]["DIEPLATECOUNT"].ToString();
                //扩充字段
            }
            dt.Clear();
            dt.Dispose();
            return m;
        }

        #endregion

        #region 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw">参见模型PEST_REPORT_HAPPEN_SW</param>
        /// <returns>参见模型PEST_REPORT_HAPPEN_Model</returns>
        public static IEnumerable<PEST_REPORT_HARM_Model> getListModel(PEST_REPORT_HARM_SW sw)
        {
            var result = new List<PEST_REPORT_HARM_Model>();
            DataTable dt = BaseDT.PEST_REPORT_HARM.getDT(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PEST_REPORT_HARM_Model m = new PEST_REPORT_HARM_Model();
                m.PEST_REPORT_HARMID = dt.Rows[i]["PEST_REPORT_HARMID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.HAPPENYEAR = dt.Rows[i]["HAPPENYEAR"].ToString();
                m.HAPPENMONTH = dt.Rows[i]["HAPPENMONTH"].ToString();
                m.PESTBYCODE = dt.Rows[i]["PESTBYCODE"].ToString();
                m.DISASTERAREA = dt.Rows[i]["DISASTERAREA"].ToString();
                m.FORECASTDISASTERAREA = dt.Rows[i]["FORECASTDISASTERAREA"].ToString();
                m.DIEPLATECOUNT = dt.Rows[i]["DIEPLATECOUNT"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion
    }
}
