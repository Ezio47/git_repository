using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseClassLibrary;
using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using PublicClassLibrary;

namespace ManagerSystemClassLibrary.BaseDT
{
    /// <summary>
    /// 值班日期表基本的增删改查方法
    /// </summary>
    public class OD_DATE
    {
        #region 值班日期表添加方法 Message Add(OD_ODTYPE_Model o)
        /// <summary>
        /// 值班日期表添加方法
        /// </summary>
        /// <param name="m">对象</param>
        /// <returns></returns>
        public static Message Add(OD_ODTYPE_Model m)
        {
            //if (o.Flog == "false")
            //{
            //先删除日期表中该类别下的日期
            StringBuilder s = new StringBuilder();
            s.AppendFormat("delete OD_DATE where 1=1 ");
            s.AppendFormat(" and OD_TYPEID={0}", ClsSql.EncodeSql(m.OD_TYPEID.ToString()));
            bool b = DataBaseClass.ExeSql(s.ToString());
            //}

            //执行插入
            StringBuilder sCreate = new StringBuilder();
            sCreate = sCreate.Append(" insert OD_DATE( OD_TYPEID,ONDUTYDATE, WEEK)select " + m.OD_TYPEID + ",dateadd(day,number,'" + m.OD_DATEBEGIN + "') as dt, ");
            sCreate = sCreate.Append(" datename(weekday,dateadd(day,number,'" + m.OD_DATEBEGIN + "')) from master.dbo.spt_values  where type ='P' ");
            sCreate = sCreate.Append(" and number <=DATEDIFF(day, '" + m.OD_DATEBEGIN + "','" + m.OD_DATEEND + "')");

            //删除不在值班日期之内的用户数据
            StringBuilder sbUser = new StringBuilder();
            sbUser.AppendFormat("delete from OD_USER where BYORGNO='{0}' ", m.BYORGNO);
            sbUser.AppendFormat(" and OD_TYPEID='{0}'",m.OD_TYPEID);
            sbUser.AppendFormat(" and (ONDUTYDATE<'{0}'", m.OD_DATEBEGIN);
            sbUser.AppendFormat(" or ONDUTYDATE>'{0}')", m.OD_DATEEND);
            
            DataBaseClassLibrary.DataBaseClass.ExeSql(sbUser.ToString());


            if (DataBaseClass.ExeSql(sCreate.ToString()))
                return new Message(true, "生成成功！", m.OD_TYPEID.ToString());
            else
                return new Message(false, "生成失败！", "");


        }

        #endregion

        #region 值班日期表数据集合 DataTable GetModelList()
        /// <summary>
        /// 值班日期表数据集合
        /// </summary>
        /// <returns></returns>
        public static DataTable GetModelList()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select  ONDUTYDATE,WEEK from OD_DATE");
            DataSet dt = DataBaseClass.FullDataSet(sb.ToString());
            return dt.Tables[0];

        }

        #endregion

        #region 带参数值班日期表数据集合查询 DataTable GetModelList(OD_DATE_Model dm)
        /// <summary>
        /// 带参数值班日期表数据集合查询
        /// </summary>
        /// <param name="dm"></param>
        /// <returns></returns>
        public static DataTable GetModelList(OD_DATE_Model dm)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select ONDUTYDATE,WEEK from OD_DATE where 1=1 ");
            if (dm.ODTYPEID!=0)
                sb.AppendFormat(" and OD_TYPEID={0}", dm.ODTYPEID);
            if (!string.IsNullOrEmpty(dm.ONDUTYDATE))
                sb.AppendFormat(" and ONDUTYDATE>='{0}'", ClsSql.EncodeSql(dm.ONDUTYDATE));
            //if (!string.IsNullOrEmpty(dm.ONDUTYYEAR))
            //    sb.AppendFormat(" and ONDUTYDATE<='{0}'", ClsSql.EncodeSql(dm.ONDUTYYEAR));
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }

        #endregion

        #region 根据查询条件获取DataTable  DataTable getDT(OD_DATE_SW  sw)
        /// <summary>
        /// 根据查询条件获取DataTable
        /// </summary>
        /// <param name="sw">参见OD_ODTYPE_SW</param>
        /// <returns>DataTable</returns>
        public static DataTable getDT(OD_DATE_SW  sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT    ");
            sb.AppendFormat(" ONDUTYID, OD_TYPEID, ONDUTYDATE, WEEK");
            sb.AppendFormat(" FROM      OD_DATE");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.OD_TYPEID) == false)
                sb.AppendFormat(" AND OD_TYPEID='{0}'", sw.OD_TYPEID);
            if (string.IsNullOrEmpty(sw.DTBegin) == false)
                sb.AppendFormat(" AND ONDUTYDATE>='{0}'", sw.DTBegin);
            if (string.IsNullOrEmpty(sw.DTEnd) == false)
                sb.AppendFormat(" AND ONDUTYDATE<='{0}'", sw.DTEnd);
            sb.AppendFormat(" order by ONDUTYDATE");
            return DataBaseClass.FullDataSet(sb.ToString()).Tables[0];

        }
        #endregion

    }
}
