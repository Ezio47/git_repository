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
    /// 三维--病虫害
    /// </summary>
    public class BINGCHONGHAI
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message Add(BINGCHONGHAI_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO BINGCHONGHAI(OBJECTID,NAME,category,DISPLAY_X,DISPLAY_Y,Shape) values(");
            sb.AppendFormat("{0},", ClsSql.saveNullField(m.OBJECTID.ToString()));
            sb.AppendFormat("{0},", ClsSql.saveNullField(m.NAME));
            sb.AppendFormat("{0},", ClsSql.saveNullField(m.category));
            sb.AppendFormat("{0},", ClsSql.saveNullField(m.DISPLAY_X));
            sb.AppendFormat("{0},", ClsSql.saveNullField(m.DISPLAY_Y));
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
        public static Message Mdy(BINGCHONGHAI_Model m)
        {
            if (isExists(new BINGCHONGHAI_Model { OBJECTID = m.OBJECTID }) == false)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("INSERT INTO BINGCHONGHAI(OBJECTID,NAME,category,DISPLAY_X,DISPLAY_Y,Shape) values(");
                sb.AppendFormat("{0},", ClsSql.saveNullField(m.OBJECTID.ToString()));
                sb.AppendFormat("{0},", ClsSql.saveNullField(m.NAME));
                sb.AppendFormat("{0},", ClsSql.saveNullField(m.category));
                sb.AppendFormat("{0},", ClsSql.saveNullField(m.DISPLAY_X));
                sb.AppendFormat("{0},", ClsSql.saveNullField(m.DISPLAY_Y));
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
                sb.AppendFormat("UPDATE BINGCHONGHAI SET ");
                sb.AppendFormat(" NAME={0}", ClsSql.saveNullField(m.NAME));
                sb.AppendFormat(",category={0}", ClsSql.saveNullField(m.category));              
                sb.AppendFormat(",DISPLAY_X={0}", ClsSql.saveNullField(m.DISPLAY_X));
                sb.AppendFormat(",DISPLAY_Y={0}", ClsSql.saveNullField(m.DISPLAY_Y));
                if (!string.IsNullOrEmpty(m.Shape))
                    sb.AppendFormat(",Shape={0}", m.Shape);
                else
                    sb.Append(",Shape=null");
                sb.AppendFormat(" where OBJECTID= {0}", ClsSql.saveNullField(m.OBJECTID.ToString()));
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
        public static Message Del(BINGCHONGHAI_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete BINGCHONGHAI");
            sb.AppendFormat(" where OBJECTID= {0}", ClsSql.saveNullField(m.OBJECTID.ToString()));
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
        public static bool isExists(BINGCHONGHAI_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from BINGCHONGHAI where 1=1");
            if (string.IsNullOrEmpty(m.OBJECTID.ToString()) == false)
                sb.AppendFormat(" and OBJECTID={0}", ClsSql.saveNullField(m.OBJECTID.ToString()));
            return SDEDataBaseClass.JudgeRecordExists(sb.ToString());
        }

        /// <summary>
        /// 获取最大ID
        /// </summary>
        /// <returns></returns>
        public static int GetMaxOBJECTID()
        {
            string sql = "Select max(OBJECTID) from BINGCHONGHAI";
            string value = SDEDataBaseClass.ReturnSqlField(sql);
            if (value != "")
                return int.Parse(value);
            else
                return 0;
        }
    }
}
