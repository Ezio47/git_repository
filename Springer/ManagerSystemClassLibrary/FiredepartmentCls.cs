using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel;
using PublicClassLibrary;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 消防队伍
    /// </summary>
    public class FiredepartmentCls
    {
        /// <summary>
        /// 消防队伍的管理
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message Manager(Firedepartment_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msgUser = BaseDT.Firedepartment.Add(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
            }
            if (m.opMethod == "Mdy")
            {
                Message msgUser = BaseDT.Firedepartment.Mdy(m);
                return new Message(msgUser.Success, msgUser.Msg,msgUser.Url );
            }
            
            if (m.opMethod == "Del")
            {
                Message msgUser = BaseDT.Firedepartment.Del(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
            }
            return new Message(false, "无效操作", "");
        }
    }
}
