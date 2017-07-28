using DataBaseClassLibrary;
using log4net;
using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using PublicClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary.BaseDT
{

    /// <summary>
    /// 监测_火情属性表
    /// </summary>
    public class JC_FIRE_PROP
    {
        private static ILog logs = LogHelper.GetInstance();
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(JC_FIRE_PROP_Model m)
        {
            if (string.IsNullOrEmpty(m.JCFID))
                return new Message(false, "添加失败，请传递正确的火情序号！", "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  JC_FIRE_PROP(   JCFID, GHMJ, GHLDMJ, SHSLMJ, RYS, RYW, MGSD, ZDQY, GJJL, ZZH, QHS, SSJB,FIRELEVEL,FIRECODE)");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.JCFID));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.GHMJ));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.GHLDMJ));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.SHSLMJ));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.RYS));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.RYW));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.MGSD));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.ZDQY));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.GJJL));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.ZZH));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.QHS));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.SSJB));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRELEVEL));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRECODE));
            sb.AppendFormat(")");
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "添加成功！", "");
            else
            {
                logs.Error(sb.ToString());
                return new Message(false, "添加失败，请检查各输入框是否正确！", "");
            }
        }

        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Mdy(JC_FIRE_PROP_Model m)
        {
            StringBuilder sb = new StringBuilder();
            //HID, HNAME, SN, PHONE, SEX, BIRTH, ONSTATE, BYORGNO
            sb.AppendFormat("UPDATE JC_FIRE_PROP");
            sb.AppendFormat(" set ");
            sb.AppendFormat("GHMJ={0}", ClsSql.saveNullField(m.GHMJ));
            sb.AppendFormat(",GHLDMJ={0}", ClsSql.saveNullField(m.GHLDMJ));
            sb.AppendFormat(",SHSLMJ={0}", ClsSql.saveNullField(m.SHSLMJ));
            sb.AppendFormat(",RYS={0}", ClsSql.saveNullField(m.RYS));
            sb.AppendFormat(",RYW={0}", ClsSql.saveNullField(m.RYW));
            sb.AppendFormat(",MGSD={0}", ClsSql.saveNullField(m.MGSD));
            sb.AppendFormat(",ZDQY={0}", ClsSql.saveNullField(m.ZDQY));
            sb.AppendFormat(",GJJL={0}", ClsSql.saveNullField(m.GJJL));
            sb.AppendFormat(",ZZH={0}", ClsSql.saveNullField(m.ZZH));
            sb.AppendFormat(",QHS={0}", ClsSql.saveNullField(m.QHS));
            sb.AppendFormat(",SSJB={0}", ClsSql.saveNullField(m.SSJB));
            sb.AppendFormat(",FIRELEVEL='{0}'", ClsSql.EncodeSql(m.FIRELEVEL));
            sb.AppendFormat(",FIRECODE='{0}'", ClsSql.EncodeSql(m.FIRECODE));
            sb.AppendFormat(" where JC_FIRE_PROPID= '{0}'", ClsSql.EncodeSql(m.JC_FIRE_PROPID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功！", "");
            else
            {
                logs.Error(sb.ToString());
                return new Message(false, "修改失败，请检查各输入框是否正确！", "");
            }
              
        }

        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(JC_FIRE_PROP_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete JC_FIRE_PROP");
            sb.AppendFormat(" where JC_FIRE_PROPID= '{0}'", ClsSql.EncodeSql(m.JC_FIRE_PROPID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功！", "");
            else
            {
                logs.Error(sb.ToString());
                return new Message(false, "删除失败，请检查各输入框是否正确！", "");
            }
        }

        #endregion

        #region 判断记录是否存在
        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>true存在 false不存在</returns>
        public static bool isExists(JC_FIRE_PROP_SW sw)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from JC_FIRE_PROP where 1=1");
            if (!string.IsNullOrEmpty(sw.JC_FIRE_PROPID))
                sb.AppendFormat(" AND JC_FIRE_PROPID= '{0}'", ClsSql.EncodeSql(sw.JC_FIRE_PROPID));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }

        #endregion

        #region 获取记录
        /// <summary>
        /// 获取记录
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(JC_FIRE_PROP_SW sw)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat(" FROM      JC_FIRE_PROP");
            sb.AppendFormat(" WHERE   1=1 ");
            if (string.IsNullOrEmpty(sw.JC_FIRE_PROPID) == false)
                sb.AppendFormat(" AND JC_FIRE_PROPID = '{0}'", ClsSql.EncodeSql(sw.JC_FIRE_PROPID));
            if (string.IsNullOrEmpty(sw.JCFID) == false)
                sb.AppendFormat(" AND JCFID = '{0}'", ClsSql.EncodeSql(sw.JCFID));
            if (string.IsNullOrEmpty(sw.FIRELEVEL) == false)
                sb.AppendFormat(" AND FIRELEVEL = '{0}'", ClsSql.EncodeSql(sw.FIRELEVEL));
            string sql = "";
            sql = "SELECT JC_FIRE_PROPID, JCFID, GHMJ, GHLDMJ, SHSLMJ, RYS, RYW, MGSD, ZDQY, GJJL, ZZH, QHS, SSJB,FIRELEVEL,FIRECODE"
                + sb.ToString()
                + " order by JC_FIRE_PROPID DESC";

            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }


        #endregion
        #region 获取火情等级
        /// <summary>
        /// 获取火情登记
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="jcfid"></param>
        /// <returns></returns>
        public static string getfirelevel(DataTable dt, string jcfid) 
        {
            if (dt == null)
                return "";
            if (string.IsNullOrEmpty(jcfid))
                return "";
            string str = "";
            DataRow[] dr = dt.Select("JCFID='" + jcfid + "'");
            if (dr.Length > 0)
                str = dr[0]["FIRELEVEL"].ToString();
            return str;
        }
        #endregion
    }
}
