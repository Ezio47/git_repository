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
    /// TD_CAMPCls
    /// </summary>
    public class TD_CAMPCls
    {
        /// <summary>
        /// 三维-车辆
        /// </summary>
       
            public static Message Manager(TD_CAMP_Model m)
            {
                if (m.opMethod == "Add")
                {
                    Message msgUser = BaseDT.SDE.TD_CAMP.Add(m);
                    return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
                }
                if (m.opMethod == "Mdy")
                {
                    Message msgUser = BaseDT.SDE.TD_CAMP.Mdy(m);
                    return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
                }

                if (m.opMethod == "Del")
                {
                    Message msgUser = BaseDT.SDE.TD_CAMP.Del(m);
                    return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
                }
                return new Message(false, "无效操作", "");
            }
        }
    
}
