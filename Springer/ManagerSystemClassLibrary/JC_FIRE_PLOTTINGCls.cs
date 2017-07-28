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
    /// 监测_火情标绘表
    /// </summary>
   public class JC_FIRE_PLOTTINGCls
   {
       #region 增、删、改
       /// <summary>
       /// 增、删、改
       /// </summary>
       /// <param name="m">参见模型</param>
       /// <returns>参见模型</returns>
       public static Message Manager(JC_FIRE_PLOTTING_Model m)
       {
           if (m.opMethod == "Add")
           {
               //SystemCls.LogSave("3", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
               Message msgUser = BaseDT.JC_FIRE_PLOTTING.Add(m);
               return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
           }
           if (m.opMethod == "Mdy")
           {
               //SystemCls.LogSave("4", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
               Message msgUser = BaseDT.JC_FIRE_PLOTTING.Mdy(m);
               return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);

           }
           if (m.opMethod == "Del")
           {
               //SystemCls.LogSave("5", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
               Message msgUser = BaseDT.JC_FIRE_PLOTTING.Del(m);
               return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
           }
           return new Message(false, "无效操作", "");


       }

       #endregion

       #region 获取单条记录
       /// <summary>
       /// 获取单条记录
       /// </summary>
       /// <param name="sw">参见模型</param>
       /// <returns>参见模型</returns>
       public static JC_FIRE_PLOTTING_Model getModel(JC_FIRE_PLOTTING_SW sw)
       {
           var result = new List<JC_FIRE_PLOTTING_Model>();

           DataTable dt = BaseDT.JC_FIRE_PLOTTING.getDT(sw);//列表

           JC_FIRE_PLOTTING_Model m = new JC_FIRE_PLOTTING_Model();
           if (dt.Rows.Count > 0)
           {
               int i = 0;
               m.JC_FIRE_PLOTTINGID = dt.Rows[i]["JC_FIRE_PLOTTINGID"].ToString();
               m.JCFID = dt.Rows[i]["JCFID"].ToString();
               m.PLOTTINGTIME =PublicClassLibrary.ClsSwitch.SwitTM( dt.Rows[i]["PLOTTINGTIME"].ToString());
               m.PLOTTINGTITLE = dt.Rows[i]["PLOTTINGTITLE"].ToString();
               m.PLOTTINGFILENAME = dt.Rows[i]["PLOTTINGFILENAME"].ToString();
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
       public static IEnumerable<JC_FIRE_PLOTTING_Model> getModelList(JC_FIRE_PLOTTING_SW sw)
       {
           var result = new List<JC_FIRE_PLOTTING_Model>();

           DataTable dt = BaseDT.JC_FIRE_PLOTTING.getDT(sw);//列表

           for (int i = 0; i < dt.Rows.Count; i++)
           {
               JC_FIRE_PLOTTING_Model m = new JC_FIRE_PLOTTING_Model();
               m.JC_FIRE_PLOTTINGID = dt.Rows[i]["JC_FIRE_PLOTTINGID"].ToString();
               m.JCFID = dt.Rows[i]["JCFID"].ToString();
               m.PLOTTINGTIME = PublicClassLibrary.ClsSwitch.SwitTM(dt.Rows[i]["PLOTTINGTIME"].ToString());
               m.PLOTTINGTITLE = dt.Rows[i]["PLOTTINGTITLE"].ToString();
               m.PLOTTINGFILENAME = dt.Rows[i]["PLOTTINGFILENAME"].ToString();
               result.Add(m);
           }
           dt.Clear();
           dt.Dispose();
           return result;
       }

       #endregion
    }
}
