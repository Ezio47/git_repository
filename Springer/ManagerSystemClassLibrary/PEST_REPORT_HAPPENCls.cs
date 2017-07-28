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
    /// 有害生物_报表_发生表
    /// </summary>
    public class PEST_REPORT_HAPPENCls
    {
        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(PEST_REPORT_HAPPEN_Model m)
        {
            return BaseDT.PEST_REPORT_HAPPEN.Save(m);
        }
        #endregion

        #region 获取单条记录
        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static PEST_REPORT_HAPPEN_Model getModel(PEST_REPORT_HAPPEN_SW sw)
        {
            DataTable dt = BaseDT.PEST_REPORT_HAPPEN.getDT(sw);
            PEST_REPORT_HAPPEN_Model m = new PEST_REPORT_HAPPEN_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                //数据库表字段
                m.PEST_REPORT_HAPPENID = dt.Rows[i]["PEST_REPORT_HAPPENID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.HAPPENYEAR = dt.Rows[i]["HAPPENYEAR"].ToString();
                m.HAPPENMONTH = dt.Rows[i]["HAPPENMONTH"].ToString();
                m.PESTBYCODE = dt.Rows[i]["PESTBYCODE"].ToString();
                m.HARMTYPEID = dt.Rows[i]["HARMTYPEID"].ToString();
                m.HARMLEVELCODE = dt.Rows[i]["HARMLEVELCODE"].ToString();
                m.HAPPENAREA = dt.Rows[i]["HAPPENAREA"].ToString();
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
        public static IEnumerable<PEST_REPORT_HAPPEN_Model> getListModel(PEST_REPORT_HAPPEN_SW sw)
        {
            var result = new List<PEST_REPORT_HAPPEN_Model>();
            DataTable dt = BaseDT.PEST_REPORT_HAPPEN.getDT(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PEST_REPORT_HAPPEN_Model m = new PEST_REPORT_HAPPEN_Model();
                m.PEST_REPORT_HAPPENID = dt.Rows[i]["PEST_REPORT_HAPPENID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.HAPPENYEAR = dt.Rows[i]["HAPPENYEAR"].ToString();
                m.HAPPENMONTH = dt.Rows[i]["HAPPENMONTH"].ToString();
                m.PESTBYCODE = dt.Rows[i]["PESTBYCODE"].ToString();
                m.HARMTYPEID = dt.Rows[i]["HARMTYPEID"].ToString();
                m.HARMLEVELCODE = dt.Rows[i]["HARMLEVELCODE"].ToString();
                m.HAPPENAREA = dt.Rows[i]["HAPPENAREA"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion

        #region 计算某地区的某种有害生物的某类型面积详细
        /// <summary>
        /// 计算某地区某年月的某类型面积详细
        /// </summary>
        /// <param name="ORGNO">机构编码</param>
        /// <param name="PESTCODE">有害生物编码</param>
        /// <param name="value">面积类型值</param>
        /// <param name="dic106">危害级别</param>
        /// <param name="list">数据列表</param>
        /// <returns></returns>
        public static List<string> GetDetailArea(string ORGNO, string PESTCODE, string value, List<T_SYS_DICTModel> dic106, List<PEST_REPORT_HAPPEN_Model> list)
        {
            List<string> result = new List<string>();
            float count = 0;
            foreach (var d in dic106)
            {
                PEST_REPORT_HAPPEN_Model m = new PEST_REPORT_HAPPEN_Model();
                if (PESTCODE != "")
                    m = list.Find(a => a.BYORGNO == ORGNO && a.PESTBYCODE == PESTCODE && a.HARMTYPEID == value && a.HARMLEVELCODE == d.DICTVALUE);
                else
                    m = list.Find(a => a.BYORGNO == ORGNO && a.HARMTYPEID == value && a.HARMLEVELCODE == d.DICTVALUE);
                if (m != null && !string.IsNullOrEmpty(m.HAPPENAREA))
                {
                    result.Add(m.HAPPENAREA);
                    count += float.Parse(m.HAPPENAREA);
                }
                else
                    result.Add("");
            }
            if (count > 0)
                result.Add(count.ToString());
            else
                result.Add("");
            return result;
        }
        #endregion

        #region 计算某地区某年月的某种有害生物无发生面积
        /// <summary>
        /// 计算某地区的某种有害生物无发生面积
        /// </summary>
        /// <param name="OBSERVEAREA">应施面积</param>
        /// <param name="ORGNO">机构编码</param>
        /// <param name="PESTCODE">有害生物编码</param>
        /// <param name="dic105">面积类型</param>
        /// <param name="dic106">危害级别</param>
        /// <param name="list">数据列表</param>
        /// <returns></returns>
        public static float GetNoArea(string OBSERVEAREA, string ORGNO, string PESTCODE, List<T_SYS_DICTModel> dic105, List<T_SYS_DICTModel> dic106, List<PEST_REPORT_HAPPEN_Model> list)
        {
            float NoArea = 0;
            List<float> _list = new List<float>();
            foreach (var d in dic105)
            {
                List<string> templist = GetDetailArea(ORGNO, PESTCODE, d.DICTVALUE, dic106, list);
                float fshj = templist[templist.Count - 1] != "" ? float.Parse(templist[templist.Count - 1]) : 0;
                _list.Add(fshj);
            }
            NoArea = _list[0] > 0 ? float.Parse(OBSERVEAREA) - _list[0] : float.Parse(OBSERVEAREA) - (_list.Sum() - _list[0]);
            return NoArea;
        }
        #endregion
    }
}
