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
    /// 预警_气象信息表
    /// </summary>
    public class YJ_WEATHER
    {

        #region 增加

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(YJ_WEATHER_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  YJ_WEATHER(WEATHERDATE, BYORGNO, TOWNNAME, JD, WD, P, T, W, F)");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.WEATHERDATE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.BYORGNO));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.TOWNNAME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.JD));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.WD));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.P));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.T));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.W));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.F));
            sb.AppendFormat(")");
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "添加成功！", "");
            else
                return new Message(false, "添加失败，请检查各输入框是否正确！", "");
        }
        #endregion

        #region 获取最新数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getNewDT(YJ_WEATHER_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" SELECT WEATHERID, WEATHERDATE, BYORGNO, TOWNNAME, JD, WD, P, T, W, F,TCUR,THIGH,TLOWER");
            sb.AppendFormat(" FROM YJ_WEATHER");
            sb.AppendFormat(" where CONVERT(varchar(10),WEATHERDATE,120)=  CONVERT(varchar(10), (select max(WEATHERDATE) from YJ_WEATHER), 120)");
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
            {

                sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            }

            if (string.IsNullOrEmpty(sw.TopORGNO) == false)
            {
                if (sw.TopORGNO.Substring(4, 5) == "00000")//获取所有市的
                    sb.AppendFormat(" AND SUBSTRING(BYORGNO,1,4) = '{0}'", ClsSql.EncodeSql(sw.TopORGNO.Substring(0, 4)));
                else if (sw.TopORGNO.Substring(6, 3) == "000")//获取所有县的
                    sb.AppendFormat(" AND SUBSTRING(BYORGNO,1,6) = '{0}'", ClsSql.EncodeSql(sw.TopORGNO.Substring(0, 6)));
                else
                    sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.TopORGNO));
            }
            string sql = sb.ToString()
                + " order by BYORGNO DESC";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }

        #endregion

        #region 获取单条气象信息
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getDT(YJ_WEATHER_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" SELECT WEATHERID, WEATHERDATE, BYORGNO, TOWNNAME, JD, WD, P, T, W, F,TCUR,THIGH,TLOWER");
            sb.AppendFormat(" FROM YJ_WEATHER");
            sb.AppendFormat(" where 1 = 1 ");
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
            {

                sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            }
            if (string.IsNullOrEmpty(sw.WEATHERDATE) == false)
            {

                sb.AppendFormat(" AND CONVERT(varchar(10),WEATHERDATE,120) = '{0}'", ClsSql.EncodeSql(sw.WEATHERDATE));
            }
            if (string.IsNullOrEmpty(sw.TopORGNO) == false)
            {
                if (sw.TopORGNO.Substring(4, 5) == "00000")//获取所有市的
                    sb.AppendFormat(" AND SUBSTRING(BYORGNO,1,4) = '{0}'", ClsSql.EncodeSql(sw.TopORGNO.Substring(0, 4)));
                else if (sw.TopORGNO.Substring(6, 3) == "000")//获取所有县的
                    sb.AppendFormat(" AND SUBSTRING(BYORGNO,1,6) = '{0}'", ClsSql.EncodeSql(sw.TopORGNO.Substring(0, 6)));
                else
                    sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.TopORGNO));
            }
            string sql = sb.ToString()
                + " order by WEATHERDATE DESC";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }

        #endregion

    }
}
