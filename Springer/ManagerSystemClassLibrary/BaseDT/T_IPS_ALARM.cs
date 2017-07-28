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
    /// 报警管理类
    /// </summary>
    public class T_IPS_ALARM
    {
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(T_IPS_ALARM_Model m)
        {
            if (string.IsNullOrEmpty(m.ALARMID))
                return new Message(false, "删除失败，请选择要处理的记录！", "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("DELETE FROM T_IPS_ALARM");
            sb.AppendFormat(" where  ALARMID= '{0}'", ClsSql.EncodeSql(m.ALARMID));

            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功！", "");
            else
                return new Message(false, "删除失败，请检查各输入框是否正确！", "");
        }

        /// <summary>
        /// 报警处理
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Man(T_IPS_ALARM_Model m)
        {
            if (string.IsNullOrEmpty(m.ALARMID))
                return new Message(false, "修改失败，请选择要处理的记录！", "");
            StringBuilder sb = new StringBuilder();
            //(ORGNO,LOGINUSERNAME,USERNAME,USERPWD,REGISTERTIME,LOGINNUM,NOTE)
            sb.AppendFormat("UPDATE T_IPS_ALARM");
            sb.AppendFormat(" set ");
            sb.AppendFormat(" ALARMCONTENT='{0}'", ClsSql.EncodeSql(m.ALARMCONTENT));
            sb.AppendFormat(" ,MANSTATE='{0}'", "1");
            sb.AppendFormat(" ,MANRESULT='{0}'", ClsSql.EncodeSql(m.MANRESULT));
            sb.AppendFormat(" ,MANTIME='{0}'", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sb.AppendFormat(" ,MANUSERID='{0}'",SystemCls.getUserID());
            sb.AppendFormat(" where  ALARMID= '{0}'", ClsSql.EncodeSql(m.ALARMID));

            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "处理成功！", "");
            else
                return new Message(false, "处理失败，请检查各输入框是否正确！", "");
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>参见模型</returns>

        public static DataTable getDT(T_IPS_ALARM_SW sw)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT ALARMID, LONGITUDE, LATITUDE, HEIGHT, PHONE, ADDRESS, ALARMTIME, ALARMCONTENT, MANSTATE,MANRESULT, MANTIME, MANUSERID");
            sb.AppendFormat(" FROM T_IPS_ALARM");
            sb.AppendFormat(" WHERE   1=1");
            //判断是否是多少，如果是多少，用in，如果只是一个，用=  主要考虑到速度
            if (string.IsNullOrEmpty(sw.ALARMID) == false)
            {
                if (sw.ALARMID.Split(',').Length > 1)
                    sb.AppendFormat(" AND ALARMID in({0})", ClsSql.EncodeSql(sw.ALARMID));
                else
                    sb.AppendFormat(" AND ALARMID ='{0}'", ClsSql.EncodeSql(sw.ALARMID));
            }


            //if (string.IsNullOrEmpty(sw.PHONE) == false)
            //{
            //    if (sw.PHONE.Length == 11)//精确查询
            //        sb.AppendFormat(" AND PHONE= '{0}'", ClsSql.EncodeSql(sw.PHONE));
            //    else//模糊查询
            //        sb.AppendFormat(" AND PHONE like '%{0}%'", ClsSql.EncodeSql(sw.PHONE));
            //}
            if (string.IsNullOrEmpty(sw.MANSTATE) == false)
                sb.AppendFormat(" AND MANSTATE ='{0}'", ClsSql.EncodeSql(sw.MANSTATE));

            if (string.IsNullOrEmpty(sw.orgNo) == false)
            {
                if (PublicCls.OrgIsShi(sw.orgNo))
                {
                    sb.AppendFormat(" and PHONE in (SELECT    PHONE FROM      T_IPSFR_USER where left(BYORGNO,4)='{0}')", PublicCls.getShiIncOrgNo(sw.orgNo));
                }
                else if (PublicCls.OrgIsXian(sw.orgNo))
                {
                    sb.AppendFormat(" and PHONE in (SELECT    PHONE FROM      T_IPSFR_USER where left(BYORGNO,6)='{0}')", PublicCls.getXianIncOrgNo(sw.orgNo));
                }
                else
                {
                    sb.AppendFormat(" and PHONE in (SELECT    PHONE FROM      T_IPSFR_USER where BYORGNO='{0}')", PublicCls.getZhenIncOrgNo(sw.orgNo));
                }
                //if (sw.orgNo.Substring(4, 5) == "00000")//获取所有县的
                //    sb.AppendFormat(" AND PHONE in (SELECT    PHONE FROM      T_IPSFR_USER where SUBSTRING(BYORGNO,1,4)) = '{0}'", ClsSql.EncodeSql(sw.orgNo.Substring(0, 4)));
                //else if (sw.orgNo.Substring(6, 3) == "000")//获取所有市的
                //    sb.AppendFormat(" AND PHONE in (SELECT    PHONE FROM      T_IPSFR_USER where SUBSTRING(BYORGNO,1,6)) = '{0}'", ClsSql.EncodeSql(sw.orgNo.Substring(0, 6)));
                //else
                //    sb.AppendFormat(" AND PHONE in (SELECT    PHONE FROM      T_IPSFR_USER where BYORGNO = '{0}')", ClsSql.EncodeSql(sw.orgNo));
            }
            if (!string.IsNullOrEmpty(sw.DateBegin))
            {
                sb.AppendFormat(" AND ALARMTIME>='{0} 00:00:00'", sw.DateBegin);
            }
            if (!string.IsNullOrEmpty(sw.DateEnd))
            {
                sb.AppendFormat(" AND ALARMTIME<='{0} 23:59:59'", sw.DateEnd);
            }

            sb.AppendFormat(" ORDER BY ALARMTIME DESC ");

            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
    }
}
