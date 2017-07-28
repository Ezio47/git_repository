using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel.SDEModel;
using PublicClassLibrary;
using DataBaseClassLibrary;
using System.Data;

namespace ManagerSystemClassLibrary.BaseDT.SDE
{
    /// <summary>
    /// 三维-隔离带-线
    /// </summary>
    public class TD_ISOLATIONSTRIP
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
          public static Message Add(TD_ISOLATIONSTRIP_Model m)
       {
           StringBuilder sb = new StringBuilder();
           sb.AppendFormat("insert into GELIDAI(OBJECTID,NAME,Shape,DISPLAY_X,DISPLAY_Y,category) values(");
           sb.AppendFormat("{0},", ClsSql.saveNullField(m.OBJECTID));
           sb.AppendFormat("{0},", ClsSql.saveNullField(m.NAME));
           sb.AppendFormat("{0},", m.Shape);
           sb.AppendFormat("{0},", ClsSql.saveNullField(m.CENTRE_X));
           sb.AppendFormat("{0},", ClsSql.saveNullField(m.CENTRE_Y));
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
        public static Message Mdy(TD_ISOLATIONSTRIP_Model m)
       {
           if (TD_ISOLATIONSTRIP.isExists(new TD_ISOLATIONSTRIP_Model { OBJECTID = m.OBJECTID }) == false)
           {
               StringBuilder sb = new StringBuilder();
               sb.AppendFormat("insert into GELIDAI(OBJECTID,NAME,Shape,DISPLAY_X,DISPLAY_Y,category) values(");
               sb.AppendFormat("{0},", ClsSql.saveNullField(m.OBJECTID));
               sb.AppendFormat("{0},", ClsSql.saveNullField(m.NAME));
               sb.AppendFormat("{0},", m.Shape);
               sb.AppendFormat("{0},", ClsSql.saveNullField(m.CENTRE_X));
               sb.AppendFormat("{0},", ClsSql.saveNullField(m.CENTRE_Y));
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
               sb.AppendFormat("UPDATE GELIDAI");
               sb.AppendFormat(" set ");
               sb.AppendFormat("NAME={0}", ClsSql.saveNullField(m.NAME));
               sb.AppendFormat(",category={0}", ClsSql.saveNullField(m.TYPE));
               sb.AppendFormat(",Shape={0}", m.Shape);
               sb.AppendFormat(",DISPLAY_X={0}", ClsSql.saveNullField(m.CENTRE_X));
               sb.AppendFormat(",DISPLAY_Y={0}", ClsSql.saveNullField(m.CENTRE_Y));
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
        public static Message Del(TD_ISOLATIONSTRIP_Model m)
       {
           StringBuilder sb = new StringBuilder();
           sb.AppendFormat("delete GELIDAI");
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
        public static bool isExists(TD_ISOLATIONSTRIP_Model m)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from GELIDAI where 1=1");
            if (string.IsNullOrEmpty(m.OBJECTID) == false)
                sb.AppendFormat(" and OBJECTID='{0}'", ClsSql.EncodeSql(m.OBJECTID));
            return SDEDataBaseClass.JudgeRecordExists(sb.ToString());
        }
    }
}

