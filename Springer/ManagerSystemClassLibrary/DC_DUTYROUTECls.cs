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
using ManagerSystemModel.SDEModel;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 责任路线
    /// </summary>
    public class DC_DUTYROUTECls
    {
        /// <summary>
        /// 增删改
        /// </summary>
        public static Message Manager(TD_DUTYROUTE_Model m)
        {
            if (m.opMethod == "AddBatch")
            {
                //SystemCls.LogSave("3", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.SDE.TD_DUTYROUTE.Add(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
            }
            if (m.opMethod == "Mdy")
            {
                //SystemCls.LogSave("4", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.SDE.TD_DUTYROUTE.Mdy(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);

            }
            if (m.opMethod == "Del")
            {
                //SystemCls.LogSave("5", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.SDE.TD_DUTYROUTE.Del(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
            }
            if (m.opMethod == "DelBatch")
            {
                //SystemCls.LogSave("5", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.SDE.TD_DUTYROUTE.Del(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
            }
            return new Message(false, "无效操作", "");
        }
    }
}

