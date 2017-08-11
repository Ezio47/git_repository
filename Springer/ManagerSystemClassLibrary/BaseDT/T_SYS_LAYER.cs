using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel;
using DataBaseClassLibrary;
using PublicClassLibrary;

namespace ManagerSystemClassLibrary.BaseDT
{
    /// <summary>
    /// 系统_图层表
    /// </summary>
    public class T_SYS_LAYER
    {
        #region 获取权限图层数据列表
        /// <summary>
        /// 获取权限图层数据列表
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getDT(T_SYS_LAYER_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT   LAYERCODE, LAYERNAME, LAYERID, ISACTION, LAYERRIGHTID, ISDEFAULTCH,ISFIREROUNDDEFAULT,ISFUROUNDDEFAULT, ORDERBY FROM T_SYS_LAYER");
            sb.AppendFormat(" Where 1=1 ");
            sb.AppendFormat(" and ISACTION=0");
            sb.AppendFormat(" or(");
            sb.AppendFormat(" LAYERRIGHTID in( ");
            sb.AppendFormat(" select rightid from T_SYSSEC_ROLE_RIGHT where roleid in(select roleid from T_SYSSEC_USER_ROLE where USERID='{0}') ", ClsSql.EncodeSql(sw.USERID));
            sb.AppendFormat(" ))");
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        #endregion

        #region 获取图层数据列表
        /// <summary>
        /// 获取图层数据列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns></returns>
        public static DataTable getDT2(T_SYS_LAYER_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT  LAYERCODE, LAYERNAME, LAYERID, ISACTION, LAYERRIGHTID, ISDEFAULTCH,ISFIREROUNDDEFAULT,ISFUROUNDDEFAULT,LAYERPICNAME, ORDERBY FROM T_SYS_LAYER WHERE 1=1");
            if (!string.IsNullOrEmpty(sw.LAYERCODE))
                sb.AppendFormat(" AND LAYERCODE = '{0}'", ClsSql.EncodeSql(sw.LAYERCODE));
            if (!string.IsNullOrEmpty(sw.LAYERNAME))
                sb.AppendFormat(" AND LAYERNAME like '%{0}%'", ClsSql.EncodeSql(sw.LAYERNAME));
            string sql = sb.ToString() + " ORDER BY LAYERCODE ";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }
        #endregion

        #region 获取所有图层名称
        /// <summary>
        /// 获取所有图层名称
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getALLDT()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT  LAYERNAME FROM T_SYS_LAYER Where 1=1");
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        #endregion

        #region 获取火情档案年份数据
        /// <summary>
        /// 获取火情档案年份数据
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getHQDADT()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT distinct(YEAR) from HUOQINGDANGAN order by YEAR desc");
            DataSet ds = SDEDataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        #endregion

        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(T_SYS_LAYER_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO T_SYS_LAYER(LAYERCODE, LAYERNAME, LAYERID, ISACTION, LAYERRIGHTID, ORDERBY, ISDEFAULTCH, ISFIREROUNDDEFAULT, ISFUROUNDDEFAULT, LAYERPICNAME) ");
            sb.AppendFormat(" VALUES(");
            sb.AppendFormat(" {0}", ClsSql.saveNullField(m.LAYERCODE));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.LAYERNAME));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.LAYERID));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.ISACTION));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.LAYERRIGHTID));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.ORDERBY));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.ISDEFAULTCH));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.ISFIREROUNDDEFAULT));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.ISFUROUNDDEFAULT));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.LAYERPICNAME));
            sb.AppendFormat(")");
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功!", "");
            else
                return new Message(false, "修改失败!", "");
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Mdy(T_SYS_LAYER_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE T_SYS_LAYER SET ");
            sb.AppendFormat(" LAYERNAME={0}", ClsSql.saveNullField(m.LAYERNAME));
            sb.AppendFormat(",LAYERID={0}", ClsSql.saveNullField(m.LAYERID));
            sb.AppendFormat(",ISACTION={0}", ClsSql.saveNullField(m.ISACTION));
            sb.AppendFormat(",LAYERRIGHTID={0}", ClsSql.saveNullField(m.LAYERRIGHTID));
            sb.AppendFormat(",ORDERBY={0}", ClsSql.saveNullField(m.ORDERBY));
            sb.AppendFormat(",ISDEFAULTCH={0}", ClsSql.saveNullField(m.ISDEFAULTCH));
            sb.AppendFormat(",ISFIREROUNDDEFAULT={0}", ClsSql.saveNullField(m.ISFIREROUNDDEFAULT));
            sb.AppendFormat(",ISFUROUNDDEFAULT={0}", ClsSql.saveNullField(m.ISFUROUNDDEFAULT));
            sb.AppendFormat(",LAYERPICNAME={0}", ClsSql.saveNullField(m.LAYERPICNAME));
            sb.AppendFormat(" WHERE LAYERCODE= '{0}'", ClsSql.EncodeSql(m.LAYERCODE));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功!", "");
            else
                return new Message(false, "修改失败!", "");
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(T_SYS_LAYER_Model m)
        {           
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("DELETE FROM T_SYS_LAYER WHERE 1=1");
            //删除所属子图层
            if (string.IsNullOrEmpty(m.LAYERCODE) == false)
                sb.AppendFormat(" AND SUBSTRING(LAYERCODE,1,{0})= '{1}'", ClsSql.EncodeSql(m.LAYERCODE).Length.ToString(), ClsSql.EncodeSql(m.LAYERCODE));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功!", "");
            else
                return new Message(false, "删除失败!", "");
        }
        #endregion

        #region 批量更新图层
        /// <summary>
        /// 批量更新图层
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns></returns>
        public static Message PLMdy(T_SYS_LAYER_Model m)
        {
            List<string> sqllist = new List<string>();
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE T_SYS_LAYER SET ");
            if (m.PlCZ == "1")
                sb.AppendFormat(" ISDEFAULTCH = '1' ");
            if (m.PlCZ == "2")
                sb.AppendFormat(" ISDEFAULTCH = '0' ");
            if (m.PlCZ == "3")
                sb.AppendFormat(" ISACTION = '1' ");
            if (m.PlCZ == "4")
                sb.AppendFormat(" ISACTION = '0' ");
            if (m.PlCZ == "5")
                sb.AppendFormat(" ISFIREROUNDDEFAULT = '1' ");
            if (m.PlCZ == "6")
                sb.AppendFormat(" ISFIREROUNDDEFAULT = '0' ");
            if (m.PlCZ == "7")
                sb.AppendFormat(" ISFUROUNDDEFAULT = '1' ");
            if (m.PlCZ == "8")
                sb.AppendFormat(" ISFUROUNDDEFAULT = '0' ");
            sb.AppendFormat(" WHERE LAYERCODE  IN ({0})", ClsSql.SwitchStrToSqlIn(m.LAYERCODE));
            sqllist.Add(sb.ToString());
            var y = DataBaseClass.ExecuteSqlTran(sqllist);
            if (y > 0)
                return new Message(true, "批量操作成功!", "");
            else
                return new Message(false, "批量操作失败,事物回滚机制!", "");

        }
        #endregion

        #region 判断记录是否存在
        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>true存在 false不存在</returns>
        public static bool isExists(T_SYS_LAYER_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from T_SYS_LAYER where 1=1");
            if (string.IsNullOrEmpty(sw.LAYERCODE) == false)
                sb.AppendFormat(" and LAYERCODE= '{0}'", ClsSql.EncodeSql(sw.LAYERCODE));
            if (string.IsNullOrEmpty(sw.LAYERID) == false)
                sb.AppendFormat(" and LAYERID= '{0}'", ClsSql.EncodeSql(sw.LAYERID));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion
    }
}
