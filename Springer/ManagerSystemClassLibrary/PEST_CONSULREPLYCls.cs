using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using PublicClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 有害生物_专家会诊回复表
    /// </summary>
    public class PEST_CONSULREPLYCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(PEST_CONSULREPLY_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msg = BaseDT.PEST_CONSULREPLY.Add(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Mdy")
            {
                Message msg = BaseDT.PEST_CONSULREPLY.Mdy(m);
                return new Message(msg.Success, msg.Msg, msg.Url);

            }
            if (m.opMethod == "Del")
            {
                Message msg = BaseDT.PEST_CONSULREPLY.Del(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            return new Message(false, "无效操作", "");
        }

        #endregion

        #region 获取单条数据
        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static PEST_CONSULREPLY_Model getModel(PEST_CONSULREPLY_SW sw)
        {
            DataTable dt = BaseDT.PEST_CONSULREPLY.getDT(sw);//列表
            PEST_CONSULREPLY_Model m = new PEST_CONSULREPLY_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.PEST_CONSULREPLYID = dt.Rows[i]["PEST_CONSULREPLYID"].ToString();
                m.PEST_CONSULTATIONID = dt.Rows[i]["PEST_CONSULTATIONID"].ToString();
                m.REPLYUID = dt.Rows[i]["REPLYUID"].ToString();
                m.REPLYUSERANME = T_SYSSEC_IPSUSERCls.getname(m.REPLYUID);
                m.REPLYTIME = ClsSwitch.SwitMN(dt.Rows[i]["REPLYTIME"].ToString());
                m.REPLYCONTENT = dt.Rows[i]["REPLYCONTENT"].ToString();
            }
            dt.Clear();
            dt.Dispose();
            return m;
        }

        #endregion

        #region 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<PEST_CONSULREPLY_Model> getListModel(PEST_CONSULREPLY_SW sw)
        {
            var result = new List<PEST_CONSULREPLY_Model>();
            DataTable dt = BaseDT.PEST_CONSULREPLY.getDT(sw);//列表
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PEST_CONSULREPLY_Model m = new PEST_CONSULREPLY_Model();
                m.PEST_CONSULREPLYID = dt.Rows[i]["PEST_CONSULREPLYID"].ToString();
                m.PEST_CONSULTATIONID = dt.Rows[i]["PEST_CONSULTATIONID"].ToString();
                m.REPLYUID = dt.Rows[i]["REPLYUID"].ToString();
                if (!string.IsNullOrEmpty(m.REPLYUID))
                    m.REPLYUSERANME = T_SYSSEC_IPSUSERCls.getname(m.REPLYUID);
                m.REPLYTIME = ClsSwitch.SwitMN(dt.Rows[i]["REPLYTIME"].ToString());
                m.REPLYCONTENT = dt.Rows[i]["REPLYCONTENT"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion

        #region 获取分页列表
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static IEnumerable<PEST_CONSULREPLY_Model> getListModel(PEST_CONSULREPLY_SW sw, out int total)
        {
            var result = new List<PEST_CONSULREPLY_Model>();
            DataTable dt = BaseDT.PEST_CONSULREPLY.getDT(sw, out total);//列表
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PEST_CONSULREPLY_Model m = new PEST_CONSULREPLY_Model();
                m.PEST_CONSULREPLYID = dt.Rows[i]["PEST_CONSULREPLYID"].ToString();
                m.PEST_CONSULTATIONID = dt.Rows[i]["PEST_CONSULTATIONID"].ToString();
                m.REPLYUID = dt.Rows[i]["REPLYUID"].ToString();
                if (!string.IsNullOrEmpty(m.REPLYUID))
                    m.REPLYUSERANME = T_SYSSEC_IPSUSERCls.getname(m.REPLYUID);
                m.REPLYTIME = ClsSwitch.SwitMN(dt.Rows[i]["REPLYTIME"].ToString());
                m.REPLYCONTENT = dt.Rows[i]["REPLYCONTENT"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion
    }
}
