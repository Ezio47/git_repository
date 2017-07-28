using DataBaseClassLibrary;
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
    /// 监测_火情属性表
    /// </summary>
    public class JC_FIRE_PROPCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(JC_FIRE_PROP_Model m)
        {
            if (m.opMethod == "Save")//传递Model，判断是否存在，如果不存在则为添加，否则为修改
            {
                if (!string.IsNullOrEmpty(m.JC_FIRE_PROPID))
                {
                    if (!BaseDT.JC_FIRE_PROP.isExists(new JC_FIRE_PROP_SW { JC_FIRE_PROPID = m.JC_FIRE_PROPID }))
                        return BaseDT.JC_FIRE_PROP.Add(m);
                    else
                        return BaseDT.JC_FIRE_PROP.Mdy(m);
                }
                else
                {
                    return BaseDT.JC_FIRE_PROP.Add(m);
                }
            }
            if (m.opMethod == "Add")
            {
                //SystemCls.LogSave("3", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.JC_FIRE_PROP.Add(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            if (m.opMethod == "Mdy")
            {
                //SystemCls.LogSave("4", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.JC_FIRE_PROP.Mdy(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);

            }
            if (m.opMethod == "Del")
            {
                //SystemCls.LogSave("5", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.JC_FIRE_PROP.Del(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            return new Message(false, "无效操作", "");


        }

        #endregion

        #region 获取单条
        /// <summary>
        /// 根据查询条件获取某一条用户信息记录，用于修改、删除
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static JC_FIRE_PROP_Model getModel(JC_FIRE_PROP_SW sw)
        {
            DataTable dt = BaseDT.JC_FIRE_PROP.getDT(sw);//列表

            JC_FIRE_PROP_Model m = new JC_FIRE_PROP_Model();

            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.JC_FIRE_PROPID = dt.Rows[i]["JC_FIRE_PROPID"].ToString();
                m.JCFID = dt.Rows[i]["JCFID"].ToString();
                m.GHMJ = dt.Rows[i]["GHMJ"].ToString();
                m.GHLDMJ = dt.Rows[i]["GHLDMJ"].ToString();
                m.SHSLMJ = dt.Rows[i]["SHSLMJ"].ToString();
                m.RYS = dt.Rows[i]["RYS"].ToString();
                m.RYW = dt.Rows[i]["RYW"].ToString();
                m.MGSD = dt.Rows[i]["MGSD"].ToString();
                m.ZDQY = dt.Rows[i]["ZDQY"].ToString();
                m.GJJL = dt.Rows[i]["GJJL"].ToString();
                m.ZZH = dt.Rows[i]["ZZH"].ToString();
                m.QHS = dt.Rows[i]["QHS"].ToString();
                m.SSJB = dt.Rows[i]["SSJB"].ToString();
                m.FIRELEVEL = dt.Rows[i]["FIRELEVEL"].ToString();
                m.FIRECODE = dt.Rows[i]["FIRECODE"].ToString();
            }
            dt.Clear();
            dt.Dispose();
            return m;
        }


        /// <summary>
        /// 获取所有
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<JC_FIRE_PROP_Model> getListModel(JC_FIRE_PROP_SW sw)
        {
            var result = new List<JC_FIRE_PROP_Model>();

            DataTable dt = BaseDT.JC_FIRE_PROP.getDT(sw);//列表
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                JC_FIRE_PROP_Model m = new JC_FIRE_PROP_Model();
                m.JC_FIRE_PROPID = dt.Rows[i]["JC_FIRE_PROPID"].ToString();
                m.JCFID = dt.Rows[i]["JCFID"].ToString();
                m.GHMJ = dt.Rows[i]["GHMJ"].ToString();
                m.GHLDMJ = dt.Rows[i]["GHLDMJ"].ToString();
                m.SHSLMJ = dt.Rows[i]["SHSLMJ"].ToString();
                m.RYS = dt.Rows[i]["RYS"].ToString();
                m.RYW = dt.Rows[i]["RYW"].ToString();
                m.MGSD = dt.Rows[i]["MGSD"].ToString();
                m.ZDQY = dt.Rows[i]["ZDQY"].ToString();
                m.GJJL = dt.Rows[i]["GJJL"].ToString();
                m.ZZH = dt.Rows[i]["ZZH"].ToString();
                m.QHS = dt.Rows[i]["QHS"].ToString();
                m.SSJB = dt.Rows[i]["SSJB"].ToString();
                m.FIRELEVEL = dt.Rows[i]["FIRELEVEL"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }

        #endregion
    }
}
