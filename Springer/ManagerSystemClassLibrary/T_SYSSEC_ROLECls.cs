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
    /// 角色管理
    /// </summary>
   public class T_SYSSEC_ROLECls
   {
       #region 增、删、改
       /// <summary>
       /// 增、删、改
       /// </summary>
       /// <param name="m">参见模型</param>
       /// <returns>参见模型</returns>
       public static Message Manager(T_SYSSEC_ROLE_Model m)
       {
           if (m.opMethod == "Add")
           {
               SystemCls.LogSave("3", "角色:" + m.ROLENAME, ClsStr.getModelContent(m));
               Message msg0 = BaseDT.T_SYSSEC_ROLE.Add(m);
               if (msg0.Success == false)
                   return new Message(msg0.Success, msg0.Msg, "");

               //获取新添加的UserID
               DataTable dt = BaseDT.T_SYSSEC_ROLE.getDT(new T_SYSSEC_ROLE_SW { ROLENAME=m.ROLENOTE, SYSFLAG=m.SYSFLAG});
               string ID = "";
               if (dt.Rows.Count > 0)
                   ID = dt.Rows[0]["ROLEID"].ToString();
               dt.Clear();
               dt.Dispose();

               Message msg = BaseDT.T_SYSSEC_ROLE_RIGHT.Save(new T_SYSSEC_ROLE_RIGHT_Model { RIGHTID = m.rightIDList, ROLEID = m.ROLEID });               
               return new Message(true, "添加成功!", m.returnUrl);
           }
           if (m.opMethod == "Mdy")
           {
               SystemCls.LogSave("4", "角色:" + m.ROLENAME, ClsStr.getModelContent(m));
               Message msg0 = BaseDT.T_SYSSEC_ROLE.Mdy(m);
               if (msg0.Success == false)
                   return new Message(msg0.Success, msg0.Msg, "");

               Message msg = BaseDT.T_SYSSEC_ROLE_RIGHT.Save(new T_SYSSEC_ROLE_RIGHT_Model { RIGHTID = m.rightIDList, ROLEID = m.ROLEID });
               return new Message(true, "修改成功!", m.returnUrl);
           }
           if (m.opMethod == "Del")
           {
               SystemCls.LogSave("5","角色:"+ m.ROLENAME, ClsStr.getModelContent(m));
               Message msgUser = BaseDT.T_SYSSEC_ROLE.Del(m);
               if (msgUser.Success == false)
                   return new Message(msgUser.Success, msgUser.Msg, "");

               Message msg = BaseDT.T_SYSSEC_ROLE_RIGHT.Save(new T_SYSSEC_ROLE_RIGHT_Model { RIGHTID = "", ROLEID = m.ROLEID });
               return new Message(true, "删除成功!", m.returnUrl);
           }
           return new Message(false, "无效操作", "");
       }

       #endregion

       #region 单条记录查询

       /// <summary>
       /// 根据查询条件获取某一条用户信息记录，用于修改、删除、用户登录验证
       /// </summary>
       /// <param name="sw">参见模型</param>
       /// <returns>参见模型</returns>
       public static T_SYSSEC_ROLE_Model getModel(T_SYSSEC_ROLE_SW sw)
       {
           DataTable dt = BaseDT.T_SYSSEC_ROLE.getDT(sw);
           T_SYSSEC_ROLE_Model m = new T_SYSSEC_ROLE_Model();
           if (dt.Rows.Count > 0)
           {
               int i = 0;
               //数据库表字段
               m.ROLEID = dt.Rows[i]["ROLEID"].ToString();
               m.ROLENAME = dt.Rows[i]["ROLENAME"].ToString();
               m.ROLENOTE = dt.Rows[i]["ROLENOTE"].ToString();
               m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
               m.ROLELEVEL = dt.Rows[i]["ROLELEVEL"].ToString();

               //扩充字段
           }
           return m;
       }
       #endregion

       #region 角色列表
       /// <summary>
       /// 角色列表
       /// </summary>
       /// <returns>参见模型</returns>
       public static IEnumerable<T_SYSSEC_ROLE_Model> getListModel()
       {
           var result = new List<T_SYSSEC_ROLE_Model>();
           DataTable dt = BaseDT.T_SYSSEC_ROLE.getDT(
               new T_SYSSEC_ROLE_SW { 
                   SYSFLAG = ConfigCls.getSystemFlag() 
               });
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               T_SYSSEC_ROLE_Model m = new T_SYSSEC_ROLE_Model();
               m.ROLEID = dt.Rows[i]["ROLEID"].ToString();
               m.ROLENAME = dt.Rows[i]["ROLENAME"].ToString();
               m.ROLENOTE = dt.Rows[i]["ROLENOTE"].ToString();
               m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
               m.ROLELEVEL = dt.Rows[i]["ROLELEVEL"].ToString();
               result.Add(m);
           }
           dt.Clear();
           dt.Dispose();
           return result;
       }
       #endregion 

       #region 获取所有角色及该用户拥有的角色
       /// <summary>
       /// 获取所有角色及该用户拥有的角色
       /// </summary>
       /// <param name="sw">参见模型</param>
       /// <returns>参见模型</returns>
       public static IEnumerable<T_SYSSEC_ROLE_USER_Model> getRoleAndUidModel(T_SYSSEC_ROLE_SW sw)
       {
           var result = new List<T_SYSSEC_ROLE_USER_Model>();
           DataTable dtRole = BaseDT.T_SYSSEC_ROLE.getDT(new T_SYSSEC_ROLE_SW { SYSFLAG = ConfigCls.getSystemFlag() });
           DataTable dtUR = BaseDT.T_SYSSEC_USER_ROLE.getDT(new T_SYSSEC_USER_ROLE_SW { USERID = sw.USERID });
           for (int i = 0; i < dtRole.Rows.Count; i++)
           {
               T_SYSSEC_ROLE_USER_Model m = new T_SYSSEC_ROLE_USER_Model();
               m.isCheck = "0";//默认不拥有
               if (dtUR != null)
               {
                   DataRow[] dr = dtUR.Select("ROLEID=" + dtRole.Rows[i]["ROLEID"].ToString());
                   if (dr.Length == 1)
                       m.isCheck = "1";//拥有角色
               }
               m.ROLEID = dtRole.Rows[i]["ROLEID"].ToString();
               m.ROLENAME = dtRole.Rows[i]["ROLENAME"].ToString();
               result.Add(m);
           }
           dtRole.Clear();
           dtRole.Dispose();
           dtUR.Clear();
           dtUR.Dispose();
           return result;
       }
       #endregion      
    }
}
