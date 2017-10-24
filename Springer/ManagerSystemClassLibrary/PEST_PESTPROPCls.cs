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
    /// 有害生物_属性表
    /// </summary>
    public class PEST_PESTPROPCls
    {
        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(PEST_PESTPROP_Model m)
        {
            Message msg = BaseDT.PEST_PESTPROP.Save(m);
            return new Message(msg.Success, msg.Msg, msg.Url); ;
        }
        #endregion

        #region 获取单条数据
        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static PEST_PESTPROP_Model getModel(PEST_PESTPROP_SW sw)
        {
            DataTable dt = BaseDT.PEST_PESTPROP.getDT(sw);
            PEST_PESTPROP_Model m = new PEST_PESTPROP_Model();
            DataTable dt125 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "125" });//生物检疫性
            DataTable dt126 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "126" });//生物危害等级
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.PEST_PESTPROPID = dt.Rows[i]["PEST_PESTPROPID"].ToString();
                m.BIOLOGICALTYPECODE = dt.Rows[i]["BIOLOGICALTYPECODE"].ToString();
                m.QUARANTINE = dt.Rows[i]["QUARANTINE"].ToString();
                m.QUARANTINENAME = BaseDT.T_SYS_DICT.getName(dt125, m.QUARANTINE);
                m.RISK = dt.Rows[i]["RISK"].ToString();
                m.RISKNAME = BaseDT.T_SYS_DICT.getName(dt126, m.RISK);
            }
            dt.Clear();
            dt.Dispose();
            return m;
        }
        #endregion
    }
}
