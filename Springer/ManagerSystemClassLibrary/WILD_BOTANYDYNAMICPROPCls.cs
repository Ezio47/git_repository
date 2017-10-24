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
    /// 野生植物动态属性表
    /// </summary>
   public class WILD_BOTANYDYNAMICPROPCls
    {
        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
       public static Message Manager(WILD_BOTANYDYNAMICPROP_Model m)
        {
            return BaseDT.WILD_BOTANYDYNAMICPROP.Save(m);
        }
        #endregion

        #region 获取单条记录
        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
       public static WILD_BOTANYDYNAMICPROP_Model getModel(WILD_BOTANYDYNAMICPROP_SW sw)
        {
            DataTable dt = BaseDT.WILD_BOTANYDYNAMICPROP.getDT(sw);
            WILD_BOTANYDYNAMICPROP_Model m = new WILD_BOTANYDYNAMICPROP_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                //数据库表字段
                m.WILD_BOTANYDYNAMICPROPID = dt.Rows[i]["WILD_BOTANYDYNAMICPROPID"].ToString();
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
       public static IEnumerable<WILD_BOTANYDYNAMICPROP_Model> getListModel(WILD_BOTANYDYNAMICPROP_SW sw)
        {
            var result = new List<WILD_BOTANYDYNAMICPROP_Model>();
            DataTable dt = BaseDT.WILD_BOTANYDYNAMICPROP.getDT(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                WILD_BOTANYDYNAMICPROP_Model m = new WILD_BOTANYDYNAMICPROP_Model();
                m.WILD_BOTANYDYNAMICPROPID = dt.Rows[i]["WILD_BOTANYDYNAMICPROPID"].ToString();
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
