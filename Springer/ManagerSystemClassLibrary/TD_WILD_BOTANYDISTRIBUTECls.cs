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
    /// 野生植物-三维
    /// </summary>
   public class TD_WILD_BOTANYDISTRIBUTECls
    {
       /// <summary>
       /// 增、删、改
       /// </summary>
       /// <param name="m"></param>
       /// <returns></returns>
       public static Message Manager(WILD_BotanyPoint_Model m)
       {
           if (m.opMethod == "Add")
           {
               Message msgUser = BaseDT.SDE.TD_WILD_BOTANYDISTRIBUTE.Add(m);
               return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
           }
           if (m.opMethod == "Mdy")
           {
               Message msgUser = BaseDT.SDE.TD_WILD_BOTANYDISTRIBUTE.Mdy(m);
               return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
           }

           if (m.opMethod == "Del")
           {
               Message msgUser = BaseDT.SDE.TD_WILD_BOTANYDISTRIBUTE.Del(m);
               return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
           }
           return new Message(false, "无效操作", "");
       }
    }
}
