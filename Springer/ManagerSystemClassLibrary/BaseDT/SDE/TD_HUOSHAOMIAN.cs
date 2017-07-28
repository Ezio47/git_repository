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
    /// 火烧面三维
    /// </summary>
    public class TD_HUOSHAOMIAN
    {
        /// <summary>
        /// 三维添加
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message Add(TD_HUOSHAOMIAN_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("insert into HUOSHAOMIAN(OBJECTID,NAME,DISPLAY_X,DISPLAY_Y,Shape,category) values(");
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
        public static Message Mdy(TD_HUOSHAOMIAN_Model m)
        {
            if (TD_HUOSHAOMIAN.isExists(new TD_HUOSHAOMIAN_Model { OBJECTID = m.OBJECTID }) == false) //如果开始添加空的的经纬度之后再修改则插入空间库是添加
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("insert into HUOSHAOMIAN(OBJECTID,NAME,DISPLAY_X,DISPLAY_Y,Shape,category) values(");
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
                sb.AppendFormat("UPDATE HUOSHAOMIAN");
                sb.AppendFormat(" set ");
                sb.AppendFormat("NAME={0}", ClsSql.saveNullField(m.NAME));
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
        public static Message Del(TD_HUOSHAOMIAN_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete HUOSHAOMIAN");
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
        public static bool isExists(TD_HUOSHAOMIAN_Model m)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from HUOSHAOMIAN where 1=1");
            if (string.IsNullOrEmpty(m.OBJECTID) == false)
                sb.AppendFormat(" and OBJECTID='{0}'", ClsSql.EncodeSql(m.OBJECTID));
            return SDEDataBaseClass.JudgeRecordExists(sb.ToString());
        }
    }
}
