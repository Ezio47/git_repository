using DataBaseClassLibrary;
using ManagerSystemModel;
using ManagerSystemModel.SDEModel;
using PublicClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary.BaseDT.SDE
{
    /// <summary>
    /// 有害生物监测点
    /// </summary>
    public class YHSWJCD
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message Add(YHSWJCD_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO YHSWJCD(OBJECTID, NAME, ADDRESS, DISPLAY_X, DISPLAY_Y, Shape) Values(");
            sb.AppendFormat("{0},", ClsSql.saveNullField(m.OBJECTID));
            sb.AppendFormat("{0},", ClsSql.saveNullField(m.NAME));
            sb.AppendFormat("{0},", ClsSql.saveNullField(m.ADDRESS));
            sb.AppendFormat("{0},", ClsSql.saveNullField(m.JD));
            sb.AppendFormat("{0},", ClsSql.saveNullField(m.WD));
            sb.AppendFormat("{0})", m.Shape);
            bool bln = SDEDataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "添加成功!", "");
            else
                return new Message(false, "添加失败!", "");
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message Mdy(YHSWJCD_Model m)
        {
            if (isExists(new YHSWJCD_Model { OBJECTID = m.OBJECTID }) == false)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("INSERT INTO YHSWJCD(OBJECTID, NAME, ADDRESS, DISPLAY_X, DISPLAY_Y, Shape) Values(");
                sb.AppendFormat("{0},", ClsSql.saveNullField(m.OBJECTID));
                sb.AppendFormat("{0},", ClsSql.saveNullField(m.NAME));
                sb.AppendFormat("{0},", ClsSql.saveNullField(m.ADDRESS));
                sb.AppendFormat("{0},", ClsSql.saveNullField(m.JD));
                sb.AppendFormat("{0},", ClsSql.saveNullField(m.WD));
                sb.AppendFormat("{0})", m.Shape);
                bool bln = SDEDataBaseClass.ExeSql(sb.ToString());
                if (bln == true)
                    return new Message(true, "添加成功!", "");
                else
                    return new Message(false, "添加失败!", "");
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("UPDATE YHSWJCD SET ");
                sb.AppendFormat(" NAME={0}", ClsSql.saveNullField(m.NAME));
                sb.AppendFormat(",ADDRESS={0}", ClsSql.saveNullField(m.ADDRESS));
                sb.AppendFormat(",DISPLAY_X={0}", ClsSql.saveNullField(m.JD));
                sb.AppendFormat(",DISPLAY_Y={0}", ClsSql.saveNullField(m.WD));
                if (!string.IsNullOrEmpty(m.Shape))
                    sb.AppendFormat(",Shape={0}", m.Shape);
                else
                    sb.AppendFormat(",Shape=null");
                sb.AppendFormat(" where OBJECTID= {0}", ClsSql.saveNullField(m.OBJECTID));
                bool bln = SDEDataBaseClass.ExeSql(sb.ToString());
                if (bln == true)
                    return new Message(true, "修改成功!", "");
                else
                    return new Message(false, "修改失败!", "");
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message Del(YHSWJCD_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete YHSWJCD");
            sb.AppendFormat(" where OBJECTID= {0}", ClsSql.saveNullField(m.OBJECTID));
            bool bln = SDEDataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功!", "");
            else
                return new Message(false, "删除失败!", "");
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static bool isExists(YHSWJCD_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from YHSWJCD where 1=1");
            if (string.IsNullOrEmpty(m.OBJECTID) == false)
                sb.AppendFormat(" and OBJECTID={0}", ClsSql.saveNullField(m.OBJECTID));
            return SDEDataBaseClass.JudgeRecordExists(sb.ToString());
        }
    }
}
