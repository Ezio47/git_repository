using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel.SDEModel;
using ManagerSystemModel;
using PublicClassLibrary;
using ManagerSystemClassLibrary.BaseDT.SDE;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 三维-监测站
    /// </summary>
    public class TD_MONITORINGSTATIONCls
    {
        /// <summary>
        /// 三维-监测站管理
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message Manager(TD_MONITORINGSTATION_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msgUser = BaseDT.SDE.TD_MONITORINGSTATION.Add(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
            }
            if (m.opMethod == "Mdy")
            {
                Message msgUser = BaseDT.SDE.TD_MONITORINGSTATION.Mdy(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
            }

            if (m.opMethod == "Del")
            {
                Message msgUser = BaseDT.SDE.TD_MONITORINGSTATION.Del(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
            }
            return new Message(false, "无效操作", "");
        }
    }
}
