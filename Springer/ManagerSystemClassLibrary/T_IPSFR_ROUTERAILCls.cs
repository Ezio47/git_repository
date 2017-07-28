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
    /// 护林员路线/围栏管理
    /// </summary>
   public class T_IPSFR_ROUTERAILCls
   {
       #region 增、删、改

       /// <summary>
       /// 增、删、改
       /// </summary>
       /// <param name="m">参见模型T_IPSFR_ROUTERAIL_Model</param>
       /// <returns>参见模型Message</returns>
       public static Message Manager(T_IPSFR_ROUTERAIL_Model m)
       {
           if (m.opMethod == "Add")
           {
               SystemCls.LogSave("3", "护林员路线添加：" + m.HID, ClsStr.getModelContent(m));
               Message msg = BaseDT.T_IPSFR_ROUTERAIL.Add(m);
               return new Message(msg.Success, msg.Msg, "");
           }
           if (m.opMethod == "AddBatch")
           {
               SystemCls.LogSave("3", "护林员路线添加：" + m.HID, ClsStr.getModelContent(m));
               Message msg = BaseDT.T_IPSFR_ROUTERAIL.AddBatch(m);
               return new Message(msg.Success, msg.Msg, "");
           }
           if (m.opMethod == "Mdy")
           {
               SystemCls.LogSave("4", "护林员路线修改:" + m.HID, ClsStr.getModelContent(m));
               Message msg = BaseDT.T_IPSFR_ROUTERAIL.Mdy(m);
               return new Message(msg.Success, msg.Msg, "");

           }
           if (m.opMethod == "Del")
           {
               SystemCls.LogSave("5", "护林员路线删除:" + m.HID, ClsStr.getModelContent(m));
               Message msg = BaseDT.T_IPSFR_ROUTERAIL.Del(m);
               return new Message(msg.Success, msg.Msg, "");
           }
           if (m.opMethod == "DelBatch")
           {
               SystemCls.LogSave("5", "护林员路线删除:" + m.HID, ClsStr.getModelContent(m));
               Message msg = BaseDT.T_IPSFR_ROUTERAIL.DelBatch(m);
               return new Message(msg.Success, msg.Msg, "");
           }
           return new Message(false, "无效操作", "");


       }
       #endregion


       #region 获取一条记录
       /// <summary>
       /// 获取一条记录
       /// </summary>
       /// <param name="sw">参见条件模型T_IPSFR_ROUTERAIL_SW</param>
       /// <returns>参见模型T_IPSFR_ROUTERAIL_Model</returns>
       public static T_IPSFR_ROUTERAIL_Model getModel(T_IPSFR_ROUTERAIL_SW sw)
       {
           DataTable dt = BaseDT.T_IPSFR_ROUTERAIL.getDT(sw);//列表

           T_IPSFR_ROUTERAIL_Model m = new T_IPSFR_ROUTERAIL_Model();
           int i = 0;
           m.ROADID = dt.Rows[i]["ROADID"].ToString();
           m.HID = dt.Rows[i]["HID"].ToString();
           m.LONGITUDE = dt.Rows[i]["LONGITUDE"].ToString();
           m.LATITUDE = dt.Rows[i]["LATITUDE"].ToString();
           m.HEIGHT = dt.Rows[i]["HEIGHT"].ToString();
           m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
           m.ROADTYPE = dt.Rows[i]["ROADTYPE"].ToString();

           DataTable dtHRUser = BaseDT.T_IPSFR_USER.getDT(new T_IPSFR_USER_SW { HID = m.HID });
           if (dtHRUser.Rows.Count > 0)
           {
               m.HName = dtHRUser.Rows[0]["HNAME"].ToString();
               m.Phone = dtHRUser.Rows[0]["PHONE"].ToString();
           }
           dtHRUser.Clear();
           dtHRUser.Dispose();
           dt.Clear();
           dt.Dispose();
           return m;
       }

       #endregion

       #region 获取列表
       /// <summary>
       /// 获取列表
       /// </summary>
       /// <param name="sw">参见条件模型T_IPSFR_ROUTERAIL_SW</param>
       /// <returns>参见模型</returns>
       public static IEnumerable<T_IPSFR_ROUTERAIL_Model> getModelList(T_IPSFR_ROUTERAIL_SW sw)
       {
           DataTable dt = BaseDT.T_IPSFR_ROUTERAIL.getDT(sw);//列表
           var result = new List<T_IPSFR_ROUTERAIL_Model>();
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               T_IPSFR_ROUTERAIL_Model m = new T_IPSFR_ROUTERAIL_Model();
               m.ROADID = dt.Rows[i]["ROADID"].ToString();
               m.HID = dt.Rows[i]["HID"].ToString();
               m.LONGITUDE = dt.Rows[i]["LONGITUDE"].ToString();
               m.LATITUDE = dt.Rows[i]["LATITUDE"].ToString();
               m.HEIGHT = dt.Rows[i]["HEIGHT"].ToString();
               m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
               m.ROADTYPE = dt.Rows[i]["ROADTYPE"].ToString();
               m.LINEARAEID = dt.Rows[i]["LINEARAEID"].ToString();
               result.Add(m);
           }
           dt.Clear();
           dt.Dispose();
           return result;
       }

       #endregion
    }
}
