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
    /// TD_DUTYROUTE
    /// </summary>
    public class TD_DUTYROUTE
    {
        /// <summary>
        /// 三维添加
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message Add(TD_DUTYROUTE_Model m)
        {
            Del(m);//先删除在添加，相当于修改
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("insert into ZERENLUXIAN(OBJECTID,ORGNAME,NAME,TELEPHONE,DISPLAY_X,DISPLAY_Y,Shape) values(");
            sb.AppendFormat("{0},", ClsSql.saveNullField(m.OBJECTID));
            sb.AppendFormat("{0},", ClsSql.saveNullField(m.ORGNAME));
            sb.AppendFormat("{0},", ClsSql.saveNullField(m.NAME));
            sb.AppendFormat("{0},", ClsSql.saveNullField(m.TELEPHONE));
            sb.AppendFormat("{0},", ClsSql.saveNullField(m.DISPLAY_X));
            sb.AppendFormat("{0},", ClsSql.saveNullField(m.DISPLAY_Y)); 
            sb.AppendFormat("{0})", m.Shape);
            bool bln = SDEDataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "添加成功！", "");
            else
                return new Message(false, "添加失败，请检查各输入框是否正确！", "");

        }
        /// <summary>
        /// 三维删除
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message Del(TD_DUTYROUTE_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" ZERENLUXIAN");
            sb.AppendFormat(" WHERE OBJECTID = '{0}'", ClsSql.EncodeSql(m.OBJECTID));
            string sql = "DELETE" + sb.ToString();
            bool bln = SDEDataBaseClass.ExeSql(sql);
            if (bln == true)
                return new Message(true, "删除成功！", "");
            else
                return new Message(false, "删除失败，请检查各输入框是否正确！", "");

        }
        /// <summary>
        /// 三维修改
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message Mdy(TD_DUTYROUTE_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE ZERENLUXIAN");
            sb.AppendFormat(" set ");
            sb.AppendFormat("NAME='{0}'", ClsSql.EncodeSql(m.NAME));
            sb.AppendFormat(",TELEPHONE='{0}'", ClsSql.EncodeSql(m.TELEPHONE));
            sb.AppendFormat(",ORGNAME='{0}'", ClsSql.EncodeSql(m.ORGNAME));
            sb.AppendFormat(" where OBJECTID= '{0}'", ClsSql.EncodeSql(m.OBJECTID));

            bool bln = SDEDataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功！", "");
            else
                return new Message(false, "修改失败，请检查各输入框是否正确！", "");
        }
    }
}
