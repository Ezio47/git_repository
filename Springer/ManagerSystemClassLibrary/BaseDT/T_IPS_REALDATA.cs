using DataBaseClassLibrary;
using ManagerSystemSearchWhereModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary.BaseDT
{
    /// <summary>
    /// 坐标上传原始数据管理类
    /// </summary>
    public class T_IPS_REALDATA
    {
        /// <summary>
        /// 获取某天数据 注意仅获取某一护林员的 
        /// </summary>
        /// <example>
        /// sw.HID      护林员序号
        /// sw.PHONE    护林员手机号码
        /// sw.searchDate   查询日期，查询某一天数据，格式为年月日或加时分秒
        /// sw.DateBegin    查询开始时间 用于实时监控时，判断记录数>1
        /// sw.DateEnd      查询结束时间 可用于防止查询某一日以外的记录
        /// </example>
        /// <returns>参见模型</returns>
        public static DataTable getDT(T_IPS_REALDATASW sw)
        {
            if (!string.IsNullOrEmpty(sw.PHONE))
            {
                sw.PHONE = "'" + sw.PHONE + "'";
            }
            //判断护林员序号是否为空，非空获取电话号码
            if (string.IsNullOrEmpty(sw.HID) == false)
            {
                string tmp = "";
                DataTable dtHUser = BaseDT.T_IPSFR_USER.getDT(new T_IPSFR_USER_SW { HID = sw.HID });//获取护林员姓名
                for (int i = 0; i < dtHUser.Rows.Count; i++)
                {
                    if (i > 0)
                        tmp += ",";
                    tmp += "'" + dtHUser.Rows[i]["PHONE"].ToString() + "'";
                }
                sw.PHONE = tmp;
                dtHUser.Clear();
                dtHUser.Dispose();
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT    REALDATAID, PHONE, LONGITUDE, LATITUDE, HEIGHT, ELECTRIC, SPEED, DIRECTION, SBTIME, NOTE FROM      T_IPS_REALDATA");
            sb.AppendFormat(" WHERE   1=1");
            if (!string.IsNullOrEmpty(sw.searchDate))
            {
                sb.AppendFormat(" AND SBTIME>='{0} 00:00:00' AND SBTIME<='{0} 23:59:59'", Convert.ToDateTime(sw.searchDate).ToString("yyyy-MM-dd"));
            }
            if (!string.IsNullOrEmpty(sw.DateBegin))
            {
                sb.AppendFormat(" AND SBTIME>='{0}'", sw.DateBegin);
            }
            if (!string.IsNullOrEmpty(sw.DateEnd))
            {
                sb.AppendFormat(" AND SBTIME<='{0}'", sw.DateEnd);
            }
            if (!string.IsNullOrEmpty(sw.PHONE))
            {
                sb.AppendFormat(" AND PHONE  in ({0})", sw.PHONE);
            }
            sb.AppendFormat(" ORDER BY SBTIME ");

            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }

    }
}
