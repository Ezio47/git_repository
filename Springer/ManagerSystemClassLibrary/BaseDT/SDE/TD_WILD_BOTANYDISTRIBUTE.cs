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
    /// 三维-野生植物分布
    /// </summary>
   public class TD_WILD_BOTANYDISTRIBUTE
    {
        #region 添加
        /// <summary>
        /// 三维-野生动物点入库
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
       public static Message Add(WILD_BotanyPoint_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("insert into BOTANYPOINT(OBJECTID,NAME,JD,WD,Shape,TYPE,BYORGNO) values(");
            sb.AppendFormat("{0},", ClsSql.saveNullField(m.OBJECTID));
            sb.AppendFormat("{0},", ClsSql.saveNullField(m.NAME));
            sb.AppendFormat("{0},", ClsSql.saveNullField(m.JD));
            sb.AppendFormat("{0},", ClsSql.saveNullField(m.WD));
            sb.AppendFormat("{0},", m.Shape);
            sb.AppendFormat("{0},", ClsSql.saveNullField(m.TYPE));
            sb.AppendFormat("{0})", ClsSql.saveNullField(m.BYORGNO));
            bool bln = SDEDataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "添加成功！", "");
            else
                return new Message(false, "添加失败，请检查各输入框是否正确！", "");

        }
        
        #endregion

        #region 修改
        /// <summary>
        /// 点的修改
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
       public static Message Mdy(WILD_BotanyPoint_Model m)
        {
            if (TD_WILD_BOTANYDISTRIBUTE.isExists(new WILD_BotanyPoint_Model { OBJECTID = m.OBJECTID }) == false)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("insert into BOTANYPOINT(OBJECTID,NAME,JD,WD,Shape,TYPE,BYORGNO) values(");
                sb.AppendFormat("{0},", ClsSql.saveNullField(m.OBJECTID));
                sb.AppendFormat("{0},", ClsSql.saveNullField(m.NAME));
                sb.AppendFormat("{0},", ClsSql.saveNullField(m.JD));
                sb.AppendFormat("{0},", ClsSql.saveNullField(m.WD));
                sb.AppendFormat("{0},", m.Shape);
                sb.AppendFormat("{0},", ClsSql.saveNullField(m.TYPE));
                sb.AppendFormat("{0})", ClsSql.saveNullField(m.BYORGNO));
                bool bln = SDEDataBaseClass.ExeSql(sb.ToString());
                if (bln == true)
                    return new Message(true, "添加成功！", "");
                else
                    return new Message(false, "添加失败，请检查各输入框是否正确！", "");
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("UPDATE BOTANYPOINT");
                sb.AppendFormat(" set ");
                sb.AppendFormat("NAME={0}", ClsSql.saveNullField(m.NAME));
                sb.AppendFormat(",JD={0}", ClsSql.saveNullField(m.JD));
                sb.AppendFormat(",WD={0}", ClsSql.saveNullField(m.WD));
                sb.AppendFormat(",TYPE={0}", ClsSql.saveNullField(m.TYPE));
                sb.AppendFormat(",Shape={0}", m.Shape);
                sb.AppendFormat(",BYORGNO={0}", ClsSql.saveNullField(m.BYORGNO));
                sb.AppendFormat(" where OBJECTID= {0}", ClsSql.saveNullField(m.OBJECTID));
                bool bln = SDEDataBaseClass.ExeSql(sb.ToString());
                if (bln == true)
                    return new Message(true, "修改成功！", "");
                else
                    return new Message(false, "修改失败，请检查各输入框是否正确！", "");
            }
        }

        #endregion

        #region 删除
        /// <summary>
        /// 点的删除
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message Del(WILD_BotanyPoint_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete BOTANYPOINT");
            sb.AppendFormat(" where OBJECTID= '{0}'", ClsSql.EncodeSql(m.OBJECTID));
            bool bln = SDEDataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功！", "");
            else
                return new Message(false, "删除失败，请检查各输入框是否正确！", "");
        }
      
        #endregion

        #region 判断记录是否存在
        /// <summary>
        /// 判断点记录是否存在
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static bool isExists(WILD_BotanyPoint_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from BOTANYPOINT where 1=1");
            if (string.IsNullOrEmpty(m.OBJECTID) == false)
                sb.AppendFormat(" and OBJECTID='{0}'", ClsSql.EncodeSql(m.OBJECTID));
            return SDEDataBaseClass.JudgeRecordExists(sb.ToString());
        }
   
        #endregion
    }
}
