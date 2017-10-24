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
    /// 三维-动物分布
    /// </summary>
  public  class TD_WILD_ANIMALDISTRIBUTECls
    {
        /// <summary>
        ///动物点的增删改查
        /// </summary>
        /// <param name="m">对象</param>
        /// <returns></returns>
      public static Message Manager(WILD_ANIMALDISTRIBUTEPoint_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msgUser = BaseDT.SDE.TD_WILD_ANIMALDISTRIBUTE.Add(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
            }
            if (m.opMethod == "Mdy")
            {
                Message msgUser = BaseDT.SDE.TD_WILD_ANIMALDISTRIBUTE.Mdy(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
            }

            if (m.opMethod == "Del")
            {
                Message msgUser = BaseDT.SDE.TD_WILD_ANIMALDISTRIBUTE.Del(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
            }
            return new Message(false, "无效操作", "");
        }
      /// <summary>
      ///动物区域的增删改查
      /// </summary>
      /// <param name="m">对象</param>
      /// <returns></returns>
      public static Message ManagerArea(WILD_ANIMALDISTRIBUTEArea_Model m)
      {
          if (m.opMethod == "Add")
          {
              Message msgUser = BaseDT.SDE.TD_WILD_ANIMALDISTRIBUTE.AddA(m);
              return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
          }
          if (m.opMethod == "Mdy")
          {
              Message msgUser = BaseDT.SDE.TD_WILD_ANIMALDISTRIBUTE.MdyA(m);
              return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
          }

          if (m.opMethod == "Del")
          {
              Message msgUser = BaseDT.SDE.TD_WILD_ANIMALDISTRIBUTE.DelA(m);
              return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
          }
          return new Message(false, "无效操作", "");
      }
    }
}
