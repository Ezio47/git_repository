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
    /// 无人机管理
    /// </summary>
    public class JC_UAVCls
    {
        #region 增、删、改
        /// <summary>
        /// 无人机增删改
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message Manager(JC_UAV_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msgUser = BaseDT.JC_UAV.Add(m);//任务信息表
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
            }
            else if (m.opMethod == "Del")
            {
                Message msgUser = BaseDT.JC_UAV.Del(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
            }
            else if (m.opMethod == "Mdy")
            {
                Message msgUser = BaseDT.JC_UAV.Update(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
            }
            return new Message(false, "无效操作", "");
        }
        #endregion

        #region 获取列表
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <param name="total">记录总数</param>
        /// <returns>参见模型</returns>
        public static IEnumerable<JC_UAV_Model> getModelList(JC_UAV_SW sw, out int total)
        {
            var result = new List<JC_UAV_Model>();

            DataTable dt = BaseDT.JC_UAV.getDT(sw, out total);//列表
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                JC_UAV_Model m = new JC_UAV_Model();
                m.UAVID = dt.Rows[i]["UAVID"].ToString();
                m.UAVNAME = dt.Rows[i]["UAVNAME"].ToString();
                m.UAVEQUIPNAME = dt.Rows[i]["UAVEQUIPNAME"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
                m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            return result;
        }
        #endregion

        #region 获取单条
        /// <summary>
        /// 根据查询条件获取某一条记录
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static JC_UAV_Model getModel(JC_UAV_SW sw)
        {
            JC_UAV_Model m = new JC_UAV_Model();
            DataTable dt = BaseDT.JC_UAV.getDT(sw);//列表
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位   
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.UAVID = dt.Rows[i]["UAVID"].ToString();
                m.UAVNAME = dt.Rows[i]["UAVNAME"].ToString();
                m.UAVEQUIPNAME = dt.Rows[i]["UAVEQUIPNAME"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            return m;
        }

        #endregion



    }
}
