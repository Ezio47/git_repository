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
    /// 三维-野生动物分布
    /// </summary>
    public class TD_WILD_ANIMALDISTRIBUTE
    {
        #region 添加
        /// <summary>
        /// 三维-野生动物点入库
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message Add(WILD_ANIMALDISTRIBUTEPoint_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("insert into ANIMALPOINT(OBJECTID,NAME,JD,WD,Shape,TYPE) values(");
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
        /// 三维-野生动物区域入库
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message AddA(WILD_ANIMALDISTRIBUTEArea_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("insert into ANIMALAREA(OBJECTID,NAME,Shape,JD,WD,TYPE) values(");
            sb.AppendFormat("{0},", ClsSql.saveNullField(m.OBJECTID));
            sb.AppendFormat("{0},", ClsSql.saveNullField(m.NAME));
            sb.AppendFormat("{0},", m.Shape);
            sb.AppendFormat("{0},", ClsSql.saveNullField(m.JD));
            sb.AppendFormat("{0},", ClsSql.saveNullField(m.WD));
            sb.AppendFormat("{0})", ClsSql.saveNullField(m.TYPE));
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
        public static Message Mdy(WILD_ANIMALDISTRIBUTEPoint_Model m)
        {
            if (TD_WILD_ANIMALDISTRIBUTE.isExists(new WILD_ANIMALDISTRIBUTEPoint_Model { OBJECTID = m.OBJECTID }) == false)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("insert into ANIMALPOINT(OBJECTID,NAME,JD,WD,Shape,TYPE) values(");
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
                sb.AppendFormat("UPDATE ANIMALPOINT");
                sb.AppendFormat(" set ");
                sb.AppendFormat("NAME={0}", ClsSql.saveNullField(m.NAME));
                sb.AppendFormat(",JD={0}", ClsSql.saveNullField(m.JD));
                sb.AppendFormat(",WD={0}", ClsSql.saveNullField(m.WD));
                sb.AppendFormat(",TYPE={0}", ClsSql.saveNullField(m.TYPE));
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
        /// 面的修改
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>

        public static Message MdyA(WILD_ANIMALDISTRIBUTEArea_Model m)
        {
            if (TD_WILD_ANIMALDISTRIBUTE.isExistsArea(new WILD_ANIMALDISTRIBUTEArea_Model { OBJECTID = m.OBJECTID }) == false)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("insert into ANIMALAREA(OBJECTID,NAME,Shape,JD,WD,TYPE) values(");
                sb.AppendFormat("{0},", ClsSql.saveNullField(m.OBJECTID));
                sb.AppendFormat("{0},", ClsSql.saveNullField(m.NAME));
                sb.AppendFormat("{0},", m.Shape);
                sb.AppendFormat("{0},", ClsSql.saveNullField(m.JD));
                sb.AppendFormat("{0},", ClsSql.saveNullField(m.WD));
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
                sb.AppendFormat("UPDATE ANIMALAREA");
                sb.AppendFormat(" set ");
                sb.AppendFormat("NAME={0}", ClsSql.saveNullField(m.NAME));
                sb.AppendFormat(",TYPE={0}", ClsSql.saveNullField(m.TYPE));
                sb.AppendFormat(",Shape={0}", m.Shape);
                sb.AppendFormat(",JD={0}", ClsSql.EncodeSql(m.JD));
                sb.AppendFormat(",WD={0}", ClsSql.EncodeSql(m.WD));
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
        public static Message Del(WILD_ANIMALDISTRIBUTEPoint_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete ANIMALPOINT");
            sb.AppendFormat(" where OBJECTID= '{0}'", ClsSql.EncodeSql(m.OBJECTID));
            bool bln = SDEDataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功！", "");
            else
                return new Message(false, "删除失败，请检查各输入框是否正确！", "");
        }
        /// <summary>
        /// 面的删除
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message DelA(WILD_ANIMALDISTRIBUTEArea_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete ANIMALAREA");
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
        public static bool isExists(WILD_ANIMALDISTRIBUTEPoint_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from ANIMALPOINT where 1=1");
            if (string.IsNullOrEmpty(m.OBJECTID) == false)
                sb.AppendFormat(" and OBJECTID='{0}'", ClsSql.EncodeSql(m.OBJECTID));
            return SDEDataBaseClass.JudgeRecordExists(sb.ToString());
        }
        /// <summary>
        /// 判断面记录是否存在
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static bool isExistsArea(WILD_ANIMALDISTRIBUTEArea_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from ANIMALAREA where 1=1");
            if (string.IsNullOrEmpty(m.OBJECTID) == false)
                sb.AppendFormat(" and OBJECTID='{0}'", ClsSql.EncodeSql(m.OBJECTID));
            return SDEDataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion
       
    }
}
