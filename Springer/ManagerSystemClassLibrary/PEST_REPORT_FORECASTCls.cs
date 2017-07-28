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
    /// 有害生物_报表_预测表
    /// </summary>
    public class PEST_REPORT_FORECASTCls
    {
        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(PEST_REPORT_FORECAST_Model m)
        {
            return BaseDT.PEST_REPORT_FORECAST.Save(m);
        }
        #endregion

        #region 获取单条记录
        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static PEST_REPORT_FORECAST_Model getModel(PEST_REPORT_FORECAST_SW sw)
        {
            DataTable dt = BaseDT.PEST_REPORT_FORECAST.getDT(sw);
            PEST_REPORT_FORECAST_Model m = new PEST_REPORT_FORECAST_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                //数据库表字段
                m.PEST_REPORT_FORECASTID = dt.Rows[i]["PEST_REPORT_FORECASTID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.FORECASTYEAR = dt.Rows[i]["FORECASTYEAR"].ToString();
                m.PESTBYCODE = dt.Rows[i]["PESTBYCODE"].ToString();
                m.FORECASTSTAGECODE = dt.Rows[i]["FORECASTSTAGECODE"].ToString();
                m.FORECASTAREA = dt.Rows[i]["FORECASTAREA"].ToString();
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
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static IEnumerable<PEST_REPORT_FORECAST_Model> getListModel(PEST_REPORT_FORECAST_SW sw)
        {
            var result = new List<PEST_REPORT_FORECAST_Model>();
            DataTable dt = BaseDT.PEST_REPORT_FORECAST.getDT(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PEST_REPORT_FORECAST_Model m = new PEST_REPORT_FORECAST_Model();
                m.PEST_REPORT_FORECASTID = dt.Rows[i]["PEST_REPORT_FORECASTID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.FORECASTYEAR = dt.Rows[i]["FORECASTYEAR"].ToString();
                m.PESTBYCODE = dt.Rows[i]["PESTBYCODE"].ToString();
                m.FORECASTSTAGECODE = dt.Rows[i]["FORECASTSTAGECODE"].ToString();
                m.FORECASTAREA = dt.Rows[i]["FORECASTAREA"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion

        #region 计算某单位某种有害生物寄主面积
        /// <summary>
        /// 计算某单位某种有害生物寄主面积
        /// </summary>
        /// <param name="ORGNO">组织机构编码</param>
        /// <param name="_TREESPECIESList">本地树种信息列表</param>
        /// <returns></returns>
        public static float GetJZArea(string ORGNO, List<PEST_LOCALTREESPECIES_Model> _TREESPECIESList)
        {
            float JZArea = 0;
            List<PEST_LOCALTREESPECIES_Model> _templist = _TREESPECIESList.FindAll(a => a.BYORGNO == ORGNO);
            foreach (var t in _templist)
            {
                if (!string.IsNullOrEmpty(t.TSPAREA))
                    JZArea += float.Parse(t.TSPAREA);
            }
            return JZArea;
        }
        #endregion

        #region 计算某单位预测面积合计
        /// <summary>
        /// 计算某单位预测面积合计
        /// </summary>
        /// <param name="ORGNO">组织机构编码</param>
        /// <param name="_list">数据列表</param>
        /// <returns></returns>
        public static float GetHJArea(string ORGNO, List<PEST_REPORT_FORECAST_Model> _list)
        {
            float HJArea = 0;
            List<PEST_REPORT_FORECAST_Model> _tempList = _list.FindAll(a => a.BYORGNO == ORGNO);
            foreach (var m in _tempList)
            {
                if (!string.IsNullOrEmpty(m.FORECASTAREA))
                    HJArea += float.Parse(m.FORECASTAREA);
            }
            return HJArea;
        }
        #endregion
    }
}
