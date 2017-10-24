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
    /// 野生动物-动态属性表
    /// </summary>
   public class WILD_ANIMALDYNAMICPROPCls
    {
        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
       public static Message Manager(WILD_ANIMALDYNAMICPROP_Model m)
        {
            return BaseDT.WILD_ANIMALDYNAMICPROP.Save(m);
        }
        #endregion

       #region 获取单条记录
       /// <summary>
       /// 获取单条记录
       /// </summary>
       /// <param name="sw">参见模型</param>
       /// <returns>参见模型</returns>
       public static WILD_ANIMALDYNAMICPROP_Model getModel(WILD_ANIMALDYNAMICPROP_SW sw)
       {
           DataTable dt = BaseDT.WILD_ANIMALDYNAMICPROP.getDT(sw);
           WILD_ANIMALDYNAMICPROP_Model m = new WILD_ANIMALDYNAMICPROP_Model();
           if (dt.Rows.Count > 0)
           {
               int i = 0;
               //数据库表字段
               m.WILD_ANIMALDYNAMICPROPID = dt.Rows[i]["WILD_ANIMALDYNAMICPROPID"].ToString();
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
       public static IEnumerable<WILD_ANIMALDYNAMICPROP_Model> getListModel(WILD_ANIMALDYNAMICPROP_SW sw)
       {
           var result = new List<WILD_ANIMALDYNAMICPROP_Model>();
           DataTable dt = BaseDT.WILD_ANIMALDYNAMICPROP.getDT(sw);
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               WILD_ANIMALDYNAMICPROP_Model m = new WILD_ANIMALDYNAMICPROP_Model();
               m.WILD_ANIMALDYNAMICPROPID = dt.Rows[i]["WILD_ANIMALDYNAMICPROPID"].ToString();
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
