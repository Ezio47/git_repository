using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel;
using DataBaseClassLibrary;
using PublicClassLibrary;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 值班班次
    /// </summary>
   public class OD_CLASSCls
   {
       #region 根据班次判断是否早退 bool isEarlyOut(OD_CLASS_SW sw)
       /// <summary>
       /// 根据班次判断是否早退
       /// </summary>
       /// <param name="sw">ONDUTYCLASSID 必传 某一班次 curTime 要判断的时间 默认为空取当前时间</param>
       /// <returns>true 早退 false 早退</returns>
       public static bool isEarlyOut(OD_CLASS_SW sw)
       {

           OD_CLASS_Model m = GetModel(sw);//获取该班次信息
           if (string.IsNullOrEmpty(sw.curTime))
               sw.curTime = PublicClassLibrary.ClsSwitch.SwitTM(DateTime.Now);
           if(string.IsNullOrEmpty(sw.judgeDate))
               sw.judgeDate = PublicClassLibrary.ClsSwitch.SwitDate(DateTime.Now);
           //班次开始时间
           DateTime dtB = Convert.ToDateTime(PublicClassLibrary.ClsSwitch.SwitDate(sw.judgeDate) + " " + m.ONDUTYBEGINTIME);
           //班次结束时间
           DateTime dtE = Convert.ToDateTime(PublicClassLibrary.ClsSwitch.SwitDate(sw.judgeDate) + " " + m.ONDUTYENDTIME);
           if (PublicClassLibrary.ClsSwitch.compDate(dtB, dtE, "1") == false)//如果结束时间小于开始时间，则结束时间加1天，即为跨天的时间
               dtE = dtE.AddDays(1);
           if (PublicClassLibrary.ClsSwitch.compDate(sw.curTime, dtE, "1") == true)
               return true;
           else
               return false;
           //return bln;

       }
       #endregion
       #region 根据班次判断是否迟到 bool isLate(OD_CLASS_SW sw)
       /// <summary>
       /// 根据班次判断是否迟到
       /// </summary>
       /// <param name="sw">ONDUTYCLASSID 必传 某一班次 curTime 要判断的时间 默认为空取当前时间</param>
       /// <returns>true 迟到 false 未迟到</returns>
       public static bool isLate(OD_CLASS_SW sw)
       {
           
           OD_CLASS_Model m = GetModel(sw);//获取该班次信息
           if (string.IsNullOrEmpty(sw.curTime))
               sw.curTime = PublicClassLibrary.ClsSwitch.SwitTM(DateTime.Now);
           //班次开始时间
           DateTime dtB = Convert.ToDateTime(PublicClassLibrary.ClsSwitch.SwitDate(sw.curTime) + " " + m.ONDUTYBEGINTIME);
           //班次结束时间
           DateTime dtE = Convert.ToDateTime(PublicClassLibrary.ClsSwitch.SwitDate(sw.curTime) + " " + m.ONDUTYENDTIME);
           if (PublicClassLibrary.ClsSwitch.compDate(dtB,dtE,"1")==false)//如果结束时间小于开始时间，则结束时间加1天，即为跨天的时间
               dtE = dtE.AddDays(1);
           if(PublicClassLibrary.ClsSwitch.compDate(sw.curTime,dtE,"1")==true)
               return false;
           else
               return true;
           //return bln;

       }
       #endregion

       #region 获取单条 IEnumerable<OD_CLASS_Model> GetModelList(OD_CLASS_SW sw)
       /// <summary>
       /// 获取单条
       /// </summary>
       /// <returns></returns>
       public static OD_CLASS_Model GetModel(OD_CLASS_SW sw)
       {
           var dt = BaseDT.OD_CLASS.GetDT(sw);
           OD_CLASS_Model m = new OD_CLASS_Model();
          if(dt.Rows.Count>0)
           {
               int i = 0;
               m.ONDUTYCLASSID = dt.Rows[i]["ONDUTYCLASSID"].ToString();
               m.ONDUTYCLASSNAME = dt.Rows[i]["ONDUTYCLASSNAME"].ToString();
               m.ONDUTYBEGINTIME = dt.Rows[i]["ONDUTYBEGINTIME"].ToString();
               m.ONDUTYENDTIME = dt.Rows[i]["ONDUTYENDTIME"].ToString();
           }
           return m;
       }
       #endregion

       #region 获取列表 IEnumerable<OD_CLASS_Model> GetModelList(OD_CLASS_SW sw)
       /// <summary>
       /// 获取列表
       /// </summary>
       /// <returns></returns>
       public static IEnumerable<OD_CLASS_Model> GetListModel(OD_CLASS_SW sw)
       {
           var result = new List<OD_CLASS_Model>();
           var dt = BaseDT.OD_CLASS.GetDT(sw);
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               OD_CLASS_Model m = new OD_CLASS_Model();
               m.ONDUTYCLASSID = dt.Rows[i]["ONDUTYCLASSID"].ToString();
               m.ONDUTYCLASSNAME = dt.Rows[i]["ONDUTYCLASSNAME"].ToString();
               m.ONDUTYBEGINTIME = dt.Rows[i]["ONDUTYBEGINTIME"].ToString();
               m.ONDUTYENDTIME = dt.Rows[i]["ONDUTYENDTIME"].ToString();
               result.Add(m);
           }
           return result;
       }
       #endregion
    }
}
