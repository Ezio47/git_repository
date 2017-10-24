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
    /// 有害生物__动态属性表
    /// </summary>
    public class PEST_PESTDYNAMICPROPCls
    {
        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(PEST_PESTDYNAMICPROP_Model m)
        {
            return BaseDT.PEST_PESTDYNAMICPROP.Save(m);
        }
        #endregion

        #region 获取单条记录
        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static PEST_PESTDYNAMICPROP_Model getModel(PEST_PESTDYNAMICPROP_SW sw)
        {
            DataTable dt = BaseDT.PEST_PESTDYNAMICPROP.getDT(sw);
            PEST_PESTDYNAMICPROP_Model m = new PEST_PESTDYNAMICPROP_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                //数据库表字段
                m.PEST_PESTDYNAMICPROPID = dt.Rows[i]["PEST_PESTDYNAMICPROPID"].ToString();
                m.BIOLOGICALTYPECODE = dt.Rows[i]["BIOLOGICALTYPECODE"].ToString();
                m.DYNAMICPROPCODE = dt.Rows[i]["DYNAMICPROPCODE"].ToString();
                m.DYNAMICPROPCONTENT = dt.Rows[i]["DYNAMICPROPCONTENT"].ToString();
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
        public static IEnumerable<PEST_PESTDYNAMICPROP_Model> getListModel(PEST_PESTDYNAMICPROP_SW sw)
        {
            var result = new List<PEST_PESTDYNAMICPROP_Model>();
            DataTable dt = BaseDT.PEST_PESTDYNAMICPROP.getDT(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PEST_PESTDYNAMICPROP_Model m = new PEST_PESTDYNAMICPROP_Model();
                m.PEST_PESTDYNAMICPROPID = dt.Rows[i]["PEST_PESTDYNAMICPROPID"].ToString();
                m.BIOLOGICALTYPECODE = dt.Rows[i]["BIOLOGICALTYPECODE"].ToString();
                m.DYNAMICPROPCODE = dt.Rows[i]["DYNAMICPROPCODE"].ToString();
                m.DYNAMICPROPCONTENT = dt.Rows[i]["DYNAMICPROPCONTENT"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion
    }
}
