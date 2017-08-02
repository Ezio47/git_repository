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
    /// 队伍装备表
    /// </summary>
    public class DC_ARMY_EQUIPCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(DC_ARMY_EQUIP_Model m)
        {
            if (m.opMethod == "Add")
            {
                //SystemCls.LogSave("3", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.DC_ARMY_EQUIP.Add(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            if (m.opMethod == "Mdy")
            {
                //SystemCls.LogSave("4", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.DC_ARMY_EQUIP.Mdy(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);

            }
            if (m.opMethod == "Del")
            {
                //SystemCls.LogSave("5", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.DC_ARMY_EQUIP.Del(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            return new Message(false, "无效操作", "");


        }

        #endregion
        #region 获取单条
        /// <summary>
        /// 根据查询条件获取某一条用户信息记录，用于修改、删除、用户登录验证
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static DC_ARMY_EQUIP_Model getModel(DC_ARMY_EQUIP_SW sw)
        {
            var result = new List<DC_ARMY_EQUIP_Model>();
            DataTable dt36 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "36" });//使用现状
            DataTable dt = BaseDT.DC_ARMY_EQUIP.getDT(sw);//列表
            DC_ARMY_EQUIP_Model m = new DC_ARMY_EQUIP_Model();

            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.DC_ARMY_EQUIP_ID = dt.Rows[i]["DC_ARMY_EQUIP_ID"].ToString();
                m.DC_ARMY_ID = dt.Rows[i]["DC_ARMY_ID"].ToString();
                m.EQUIPNAME = dt.Rows[i]["EQUIPNAME"].ToString();
                m.EQUIPNUM = dt.Rows[i]["EQUIPNUM"].ToString();
                m.EQUIPMODEL = dt.Rows[i]["EQUIPMODEL"].ToString();
                m.EQUIPUSESTATE = dt.Rows[i]["EQUIPUSESTATE"].ToString();
                m.EQUIPUSESTATEName = BaseDT.T_SYS_DICT.getName(dt36, m.EQUIPUSESTATE);
                m.EQUIPSUM = dt.Rows[i]["EQUIPSUM"].ToString();
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
        public static IEnumerable<DC_ARMY_EQUIP_Model> getModelList(DC_ARMY_EQUIP_SW sw)
        {
            var result = new List<DC_ARMY_EQUIP_Model>();

            DataTable dt = BaseDT.DC_ARMY_EQUIP.getDT(sw);//列表
            DataTable dt36 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "36" });//使用现状
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DC_ARMY_EQUIP_Model m = new DC_ARMY_EQUIP_Model();
                m.DC_ARMY_EQUIP_ID = dt.Rows[i]["DC_ARMY_EQUIP_ID"].ToString();
                m.DC_ARMY_ID = dt.Rows[i]["DC_ARMY_ID"].ToString();
                m.EQUIPNAME = dt.Rows[i]["EQUIPNAME"].ToString();
                m.EQUIPNUM = dt.Rows[i]["EQUIPNUM"].ToString();
                m.EQUIPMODEL = dt.Rows[i]["EQUIPMODEL"].ToString();
                m.EQUIPUSESTATE = dt.Rows[i]["EQUIPUSESTATE"].ToString();
                m.EQUIPUSESTATEName = BaseDT.T_SYS_DICT.getName(dt36, m.EQUIPUSESTATE);
                m.EQUIPSUM = dt.Rows[i]["EQUIPSUM"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }

        #endregion
    }

}
