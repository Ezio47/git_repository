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
    /// 获取三位图层树图层
    /// </summary>
    public class T_SYS_LAYER
    {
        #region 获取数据
        /// <summary>
        /// 获取数据
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
        #region 获取所有数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getALLDT()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT   LAYERNAME FROM T_SYS_LAYER");
            sb.AppendFormat(" Where 1=1 ");
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        #endregion
        #region 获取所有数据
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
    }
}
