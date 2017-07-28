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
    /// 有害生物_报表_防治表
    /// </summary>
    public class PEST_REPORT_CONTROLCls
    {
        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(PEST_REPORT_CONTROL_Model m)
        {
            return BaseDT.PEST_REPORT_CONTROL.Save(m);
        }
        #endregion

        #region 获取单条记录
        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <param name="sw">参见模型PEST_REPORT_CONTROL_SW</param>
        /// <returns>参见模型PEST_REPORT_HAPPEN_Model</returns>
        public static PEST_REPORT_CONTROL_Model getModel(PEST_REPORT_CONTROL_SW sw)
        {
            DataTable dt = BaseDT.PEST_REPORT_CONTROL.getDT(sw);
            PEST_REPORT_CONTROL_Model m = new PEST_REPORT_CONTROL_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                //数据库表字段
                m.PEST_REPORT_CONTROLID = dt.Rows[i]["PEST_REPORT_CONTROLID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.HAPPENYEAR = dt.Rows[i]["HAPPENYEAR"].ToString();
                m.HAPPENMONTH = dt.Rows[i]["HAPPENMONTH"].ToString();
                m.PESTBYCODE = dt.Rows[i]["PESTBYCODE"].ToString();
                m.CONTROLMETHODCODE = dt.Rows[i]["CONTROLMETHODCODE"].ToString();
                m.CONTROLAREA = dt.Rows[i]["CONTROLAREA"].ToString();
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
        /// <param name="sw">参见模型PEST_REPORT_CONTROL_SW</param>
        /// <returns>参见模型PEST_REPORT_CONTROL_Model</returns>
        public static IEnumerable<PEST_REPORT_CONTROL_Model> getListModel(PEST_REPORT_CONTROL_SW sw)
        {
            var result = new List<PEST_REPORT_CONTROL_Model>();
            DataTable dt = BaseDT.PEST_REPORT_CONTROL.getDT(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PEST_REPORT_CONTROL_Model m = new PEST_REPORT_CONTROL_Model();
                m.PEST_REPORT_CONTROLID = dt.Rows[i]["PEST_REPORT_CONTROLID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.HAPPENYEAR = dt.Rows[i]["HAPPENYEAR"].ToString();
                m.HAPPENMONTH = dt.Rows[i]["HAPPENMONTH"].ToString();
                m.PESTBYCODE = dt.Rows[i]["PESTBYCODE"].ToString();
                m.CONTROLMETHODCODE = dt.Rows[i]["CONTROLMETHODCODE"].ToString();
                m.CONTROLAREA = dt.Rows[i]["CONTROLAREA"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion

        #region 计算某单位防治面积详细
        /// <summary>
        /// 计算某单位防治面积详细
        /// </summary>
        /// <param name="ORGNO">单位编码</param>
        /// <param name="list1">防治方法列表</param>
        /// <param name="_list2">防治报表数据列表</param>
        /// <returns></returns>
        public static List<float> GetCONTROLDetailArea(string ORGNO, List<T_SYS_DICTModel> list1, List<PEST_REPORT_CONTROL_Model> _list2)
        {
            List<float> result = new List<float>();
            foreach (var dic in list1)
            {
                PEST_REPORT_CONTROL_Model m = _list2.Find(a => a.BYORGNO == ORGNO && a.CONTROLMETHODCODE == dic.DICTVALUE);
                if (m != null && !string.IsNullOrEmpty(m.CONTROLAREA))
                    result.Add(float.Parse(m.CONTROLAREA));
                else
                    result.Add(0);
            }
            result.Add(result.Sum());
            return result;
        } 
        #endregion
    }
}
