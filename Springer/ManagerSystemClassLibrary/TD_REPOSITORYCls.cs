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
    /// 仓库三维
    /// </summary>
   public class TD_REPOSITORYCls
    {
       /// <summary>
       /// 增删改查
       /// </summary>
       /// <param name="m">对象</param>
       /// <returns></returns>
       public static Message Manager(TD_REPOSITORY_Model m)
       {
           if (m.opMethod == "Add")
           {
               Message msgUser = BaseDT.SDE.TD_REPOSITORY.Add(m);
               return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
           }
           if (m.opMethod == "Mdy")
           {
               Message msgUser = BaseDT.SDE.TD_REPOSITORY.Mdy(m);
               return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
           }

           if (m.opMethod == "Del")
           {
               Message msgUser = BaseDT.SDE.TD_REPOSITORY.Del(m);
               return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
           }
           return new Message(false, "无效操作", "");
       }
    }
}
