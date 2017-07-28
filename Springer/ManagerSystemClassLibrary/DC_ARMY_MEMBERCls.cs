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
    /// 数据中心_队伍_人员表
    /// </summary>
   public class DC_ARMY_MEMBERCls
   {
       #region 增、删、改
       /// <summary>
       /// 增、删、改
       /// </summary>
       /// <param name="m">参见模型</param>
       /// <returns>参见模型</returns>
       public static Message Manager(DC_ARMY_MEMBER_Model m)
       {
           if (m.opMethod == "Add")
           {
               //SystemCls.LogSave("3", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
               Message msgUser = BaseDT.DC_ARMY_MEMBER.Add(m);
               return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
           }
           if (m.opMethod == "Mdy")
           {
               //SystemCls.LogSave("4", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
               Message msgUser = BaseDT.DC_ARMY_MEMBER.Mdy(m);
               return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);

           }
           if (m.opMethod == "Del")
           {
               //SystemCls.LogSave("5", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
               Message msgUser = BaseDT.DC_ARMY_MEMBER.Del(m);
               return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
           }
           return new Message(false, "无效操作", "");


       }

       #endregion

       #region 获取单条
       /// <summary>
       /// 根据查询条件获取某一条用户信息记录，用于修改、删除、用户登录验证
       /// </summary>
       /// <param name="sw">参见模型</param>
       /// <returns>参见模型</returns>
       public static DC_ARMY_MEMBER_Model getModel(DC_ARMY_MEMBER_SW sw)
       {
           var result = new List<DC_ARMY_MEMBER_Model>();

           DataTable dt = BaseDT.DC_ARMY_MEMBER.getDT(sw);//列表
           DC_ARMY_MEMBER_Model m = new DC_ARMY_MEMBER_Model();

           if (dt.Rows.Count > 0)
           {
               int i = 0;
               m.DC_ARMY_MEMBER_ID = dt.Rows[i]["DC_ARMY_MEMBER_ID"].ToString();
               m.DC_ARMY_ID = dt.Rows[i]["DC_ARMY_ID"].ToString();
               m.MEMBERNAME = dt.Rows[i]["MEMBERNAME"].ToString();
               m.SEX = dt.Rows[i]["SEX"].ToString();
               m.CONTACTS = dt.Rows[i]["CONTACTS"].ToString();
               m.BIRTH = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BIRTH"].ToString());
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
       public static IEnumerable<DC_ARMY_MEMBER_Model> getModelList(DC_ARMY_MEMBER_SW sw)
       {
           var result = new List<DC_ARMY_MEMBER_Model>();

           DataTable dt = BaseDT.DC_ARMY_MEMBER.getDT(sw);//列表
           DataTable dtSex = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTFLAG = "性别" });//性别
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               DC_ARMY_MEMBER_Model m = new DC_ARMY_MEMBER_Model();
               m.DC_ARMY_MEMBER_ID = dt.Rows[i]["DC_ARMY_MEMBER_ID"].ToString();
               m.DC_ARMY_ID = dt.Rows[i]["DC_ARMY_ID"].ToString();
               m.MEMBERNAME = dt.Rows[i]["MEMBERNAME"].ToString();
               m.SEX = dt.Rows[i]["SEX"].ToString();
               m.SEXNAME = BaseDT.T_SYS_DICT.getName(dtSex, dt.Rows[i]["SEX"].ToString());
               m.CONTACTS = dt.Rows[i]["CONTACTS"].ToString();
               m.BIRTH = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BIRTH"].ToString());
               result.Add(m);
           }
           dt.Clear();
           dt.Dispose();
           return result;
       }

       #endregion

       #region 获取分页
       /// <summary>
       /// 获取分页
       /// </summary>
       /// <param name="sw">参见模型</param>
       /// <param name="total">记录总数</param>
       /// <returns>参见模型</returns>
       public static IEnumerable<DC_ARMY_MEMBER_Model> getModelList(DC_ARMY_MEMBER_SW sw, out int total)
       {
           var result = new List<DC_ARMY_MEMBER_Model>();

           DataTable dt = BaseDT.DC_ARMY_MEMBER.getDT(sw, out total);//列表

           for (int i = 0; i < dt.Rows.Count; i++)
           {
               DC_ARMY_MEMBER_Model m = new DC_ARMY_MEMBER_Model();
               m.DC_ARMY_MEMBER_ID = dt.Rows[i]["DC_ARMY_MEMBER_ID"].ToString();
               m.DC_ARMY_ID = dt.Rows[i]["DC_ARMY_ID"].ToString();
               m.MEMBERNAME = dt.Rows[i]["MEMBERNAME"].ToString();
               m.SEX = dt.Rows[i]["SEX"].ToString();
               m.CONTACTS = dt.Rows[i]["CONTACTS"].ToString();
               m.BIRTH =PublicClassLibrary.ClsSwitch.SwitDate( dt.Rows[i]["BIRTH"].ToString());
               result.Add(m);
           }
           dt.Clear();
           dt.Dispose();
           return result;
       }
       #endregion
    }
}
