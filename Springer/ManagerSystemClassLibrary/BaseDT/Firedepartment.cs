using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using PublicClassLibrary;
using DataBaseClassLibrary;
using System.Data;

namespace ManagerSystemClassLibrary.BaseDT
{
    /// <summary>
    /// 消防队伍
    /// </summary>
   public class Firedepartment
    {
       /// <summary>
       /// 消防队伍添加
       /// </summary>
       /// <param name="m"></param>
       /// <returns></returns>
       public static Message Add(Firedepartment_Model m)
       {
           StringBuilder sb = new StringBuilder();
           sb.AppendFormat("insert into XIAOFANGDUIWU(OBJECTID,Name,DISPLAY_X,DISPLAY_Y,Shape,category) values(");
           sb.AppendFormat("{0},", ClsSql.saveNullField(m.OBJECTID));
           sb.AppendFormat("{0},", ClsSql.saveNullField(m.NAME));
           sb.AppendFormat("{0},", ClsSql.saveNullField(m.JD));
           sb.AppendFormat("{0},", ClsSql.saveNullField(m.WD));
           sb.AppendFormat("{0},", m.Shape);
           sb.AppendFormat("{0})", ClsSql.saveNullField(m.TYPE));
           bool bln = SDEDataBaseClass.ExeSql(sb.ToString());
           if (bln == true)
               return new Message(true, "添加成功！", "");
           else
               return new Message(false, "添加失败，请检查各输入框是否正确！", "");

       }
       /// <summary>
       /// 修改
       /// </summary>
       /// <param name="m"></param>
       /// <returns></returns>
       public static Message Mdy(Firedepartment_Model m)
       {
           if (Firedepartment.isExists(new Firedepartment_Model { OBJECTID = m.OBJECTID }) == false) //如果开始添加空的的经纬度之后再修改则插入空间库是添加
           {
               StringBuilder sb = new StringBuilder();
               sb.AppendFormat("insert into XIAOFANGDUIWU(OBJECTID,Name,DISPLAY_X,DISPLAY_Y,Shape,category) values(");
               sb.AppendFormat("{0},", ClsSql.saveNullField(m.OBJECTID));
               sb.AppendFormat("{0},", ClsSql.saveNullField(m.NAME));
               sb.AppendFormat("{0},", ClsSql.saveNullField(m.JD));
               sb.AppendFormat("{0},", ClsSql.saveNullField(m.WD));
               sb.AppendFormat("{0},", m.Shape);
               sb.AppendFormat("{0})", ClsSql.saveNullField(m.TYPE));
               bool bln = SDEDataBaseClass.ExeSql(sb.ToString());
               if (bln == true)
                   return new Message(true, "添加成功！", "");
               else
                   return new Message(false, "添加失败，请检查各输入框是否正确！", "");
           }
           else
           {
               StringBuilder sb = new StringBuilder();
               sb.AppendFormat("UPDATE XIAOFANGDUIWU");
               sb.AppendFormat(" set ");
               sb.AppendFormat("Name={0}", ClsSql.saveNullField(m.NAME));
               sb.AppendFormat(",DISPLAY_X={0}", ClsSql.saveNullField(m.JD));
               sb.AppendFormat(",DISPLAY_Y={0}", ClsSql.saveNullField(m.WD));
               sb.AppendFormat(",category={0}", ClsSql.saveNullField(m.TYPE));
               sb.AppendFormat(",Shape={0}", m.Shape);
               sb.AppendFormat(" where OBJECTID= {0}", ClsSql.saveNullField(m.OBJECTID));
               bool bln = SDEDataBaseClass.ExeSql(sb.ToString());
               if (bln == true)
                   return new Message(true, "修改成功！", "");
               else
                   return new Message(false, "修改失败，请检查各输入框是否正确！", "");
           }
       }
       /// <summary>
       /// 删除
       /// </summary>
       /// <param name="m"></param>
       /// <returns></returns>
       public static Message Del(Firedepartment_Model m)
       {
           StringBuilder sb = new StringBuilder();
           sb.AppendFormat("delete XIAOFANGDUIWU");
           sb.AppendFormat(" where OBJECTID= '{0}'", ClsSql.EncodeSql(m.OBJECTID));
           bool bln = SDEDataBaseClass.ExeSql(sb.ToString());
           if (bln == true)
               return new Message(true, "删除成功！", "");
           else
               return new Message(false, "删除失败，请检查各输入框是否正确！", "");
       }
       /// <summary>
       /// 判断是否存在
       /// </summary>
       /// <param name="m"></param>
       /// <returns></returns>
       public static bool isExists(Firedepartment_Model m)
       {

           StringBuilder sb = new StringBuilder();
           sb.AppendFormat("select 1 from XIAOFANGDUIWU where 1=1");
           if (string.IsNullOrEmpty(m.OBJECTID) == false)
               sb.AppendFormat(" and OBJECTID='{0}'", ClsSql.EncodeSql(m.OBJECTID));
           return SDEDataBaseClass.JudgeRecordExists(sb.ToString());
       }
    }
}
