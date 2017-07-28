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
    /// 监测_火情标绘表
    /// </summary>
    public class JC_FIRE_PLOTTING
    {
        private static ILog logs = LogHelper.GetInstance();
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(JC_FIRE_PLOTTING_Model m)
        {
            if (string.IsNullOrEmpty(m.JCFID))
                return new Message(false, "添加失败，请传递正确的火情序号！", "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  JC_FIRE_PLOTTING(  JCFID, PLOTTINGTIME, PLOTTINGTITLE, PLOTTINGFILENAME)");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.JCFID));
            sb.AppendFormat(",'{0}'", PublicClassLibrary.ClsSwitch.SwitTM(DateTime.Now));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.PLOTTINGTITLE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.PLOTTINGFILENAME));
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
        public static Message Mdy(JC_FIRE_PLOTTING_Model m)
        {
            StringBuilder sb = new StringBuilder();
            //HID, HNAME, SN, PHONE, SEX, BIRTH, ONSTATE, BYORGNO
            //JCFID, PLOTTINGTIME, PLOTTINGTITLE, PLOTTINGFILENAME
            sb.AppendFormat("UPDATE JC_FIRE_PLOTTING");
            sb.AppendFormat(" set ");
            sb.AppendFormat("PLOTTINGTITLE={0}", ClsSql.saveNullField(m.PLOTTINGTITLE));
            sb.AppendFormat(",PLOTTINGFILENAME={0}", ClsSql.saveNullField(m.PLOTTINGFILENAME));
            sb.AppendFormat(" where JC_FIRE_PLOTTINGID= '{0}'", ClsSql.EncodeSql(m.JC_FIRE_PLOTTINGID));
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
        public static Message Del(JC_FIRE_PLOTTING_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete JC_FIRE_PLOTTING");
            sb.AppendFormat(" where JC_FIRE_PLOTTINGID= '{0}'", ClsSql.EncodeSql(m.JC_FIRE_PLOTTINGID));
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
        public static bool isExists(JC_FIRE_PLOTTING_SW sw)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from JC_FIRE_PLOTTING where 1=1");
            if (string.IsNullOrEmpty(sw.JC_FIRE_PLOTTINGID) == false)
                sb.AppendFormat(" where JC_FIRE_PLOTTINGID= '{0}'", ClsSql.EncodeSql(sw.JC_FIRE_PLOTTINGID));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }

        #endregion

        #region 获取记录
        /// <summary>
        /// 获取记录
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(JC_FIRE_PLOTTING_SW sw)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat(" FROM      JC_FIRE_PLOTTING");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.JC_FIRE_PLOTTINGID) == false)
                sb.AppendFormat(" AND JC_FIRE_PLOTTINGID = '{0}'", ClsSql.EncodeSql(sw.JC_FIRE_PLOTTINGID));
            if (string.IsNullOrEmpty(sw.JCFID) == false)
                sb.AppendFormat(" AND JCFID = '{0}'", ClsSql.EncodeSql(sw.JCFID));
            string sql = "";
            sql = " SELECT JC_FIRE_PLOTTINGID, JCFID, PLOTTINGTIME, PLOTTINGTITLE, PLOTTINGFILENAME"
            + sb.ToString()
            + " order by PLOTTINGTIME";

            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }

        #endregion
    }
}
