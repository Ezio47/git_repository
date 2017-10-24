using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel;
using DataBaseClassLibrary;
using PublicClassLibrary;
using System.IO;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 系统用户_自定义属性表
    /// </summary>
   public class T_SYSSEC_USER_DEFINPROPCls
   {
      
       #region 单条记录Model
       /// <summary>
       /// 根据UID和字典值获取属性值
       /// </summary>
       /// <param name="sw"></param>
       /// <returns></returns>
       public static string getPROPVALUEByUIDDICTVALUE(T_SYSSEC_USER_DEFINPROP_SW sw)
       {
           return BaseDT.T_SYSSEC_USER_DEFINPROP.getPROPVALUEByUIDDICTVALUE(sw);
       }
      
       #endregion
    }
}
