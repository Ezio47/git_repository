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
    /// 火情反馈表
    /// </summary>
    public class JC_FIRETICKLING
    {
        private static ILog logs = LogHelper.GetInstance();
        #region 增加

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(JC_FIRETICKLING_SW m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  JC_FIRETICKLING(JCFID,DL,FORESTNAME,FORESTFIRETYPE,FUELTYPE,HOTTYPE,CHECKTIME,YY,JXHQSJ,FIREBEGINTIME,FIREENDTIME,ISOUTFIRE,BURNEDAREA,OVERDOAREA,LOSTFORESTAREA,ELSELOSSINTRO,FIREINTRO,BYORGNO,MANUSERID,MANTIME,MANSTATE)");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.JCFID));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.DL));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.FORESTNAME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.FORESTFIRETYPE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.FUELTYPE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.HOTTYPE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(ClsSwitch.SwitTM(m.CHECKTIME)));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.YY));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.JXHQSJ));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(ClsSwitch.SwitTM(m.FIREBEGINTIME)));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(ClsSwitch.SwitTM(m.FIREENDTIME)));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ISOUTFIRE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.BURNEDAREA));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.OVERDOAREA));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.LOSTFORESTAREA));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ELSELOSSINTRO));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.FIREINTRO));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.BYORGNO));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.MANUSERID));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(ClsSwitch.SwitTM(m.MANTIME)));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.MANSTATE));
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

        /// <summary>
        /// 获取火情反馈表信息
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable GetDT(JC_FIRETICKLING_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select * from JC_FIRETICKLING");
            sb.Append(" where 1 = 1 ");
            if (!string.IsNullOrEmpty(sw.JCFID))
            {
                sb.AppendFormat(" And JCFID='{0}'", sw.JCFID);
            }
            if (!string.IsNullOrEmpty(sw.MANSTATE))
            {
                sb.AppendFormat(" And MANSTATE={0}", sw.MANSTATE);
            }
            if (!string.IsNullOrEmpty(sw.MANUSERID))
            {
                sb.AppendFormat(" And MANUSERID={0}", sw.MANUSERID);
            }
            if (!string.IsNullOrEmpty(sw.BYORGNO))
            {
                sb.AppendFormat(" And BYORGNO={0}", sw.BYORGNO);
            }
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        #endregion

        #region 反馈火情数据事务
        /// <summary>
        /// 获取反馈信息记录
        /// </summary>
        /// <param name="jcfid"></param>
        /// <returns></returns>
        public static DataTable GetFKDT(string jcfid)
        {
            // select a.JCFID JCFID ,a.WXBH WXBH,a.DQRDBH DQRDBH,a.RSMJ RSMJ,a.JD JD,a.WD WD,a.RECEIVETIME RECEIVETIME,a.ISSUEDTIME ISSUEDTIME,a.ZQWZ ZQWZ,b.* from JC_FIRE a 
            //left join  (select top 1 *  from JC_FIRETICKLING c  order by c.FKID desc ) b on a.JCFID=b.JCFID
            //where a.JCFID='2'
            StringBuilder sb = new StringBuilder();
            sb.Append(" Select a.JCFID JCFID ,a.FIRENAME FIRENAME,a.BYORGNO XFORGNO,a.FIREFROMID FIREFROMID, a.FIREFROM FIREFROM,a.FIRETIME FIRETIME,a.WXBH WXBH,a.DQRDBH DQRDBH,a.RSMJ RSMJ,a.JD JCJD,a.WD JCWD,a.RECEIVETIME RECEIVETIME,a.ISSUEDTIME ISSUEDTIME,a.ZQWZ ZQWZ,a.MANSTATE JCMANSTATE,a.PFFLAG PFFLAG,b.* from  JC_FIRE a");
            sb.AppendFormat(" left join  (select top 1 *  from JC_FIRETICKLING c   where c.JCFID='{0}'  And c.MANSTATE not in('0','1','2','3') order by c.FKID desc ) b ", jcfid);
            sb.Append(" on a.JCFID=b.JCFID ");
            sb.Append(" Where 1 = 1 ");
            sb.AppendFormat(" And a.JCFID='{0}'", jcfid);
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }

        #endregion

        #region 获取最新的反馈信息
        /// <summary>
        /// 根据JCFID获取最新的反馈信息
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable Latestfeedback(JC_FIRETICKLING_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" Select top 1 * from JC_FIRETICKLING  Where 1=1 ");
            if (!string.IsNullOrEmpty(sw.JCFID))
            {
                sb.AppendFormat(" AND JCFID ={0}", ClsSql.EncodeSql(sw.JCFID));
            }
            if (!string.IsNullOrEmpty(sw.DL))
            {
                sb.AppendFormat(" AND DL ={0}", ClsSql.EncodeSql(sw.DL));
            }
            if (!string.IsNullOrEmpty(sw.FORESTNAME))
            {
                sb.AppendFormat(" AND FORESTNAME ={0}", ClsSql.EncodeSql(sw.FORESTNAME));
            }
            if (!string.IsNullOrEmpty(sw.FORESTFIRETYPE))
            {
                sb.AppendFormat(" AND FORESTFIRETYPE ={0}", ClsSql.EncodeSql(sw.FORESTFIRETYPE));
            }
            if (!string.IsNullOrEmpty(sw.FUELTYPE))
            {
                sb.AppendFormat(" AND FUELTYPE ={0}", ClsSql.EncodeSql(sw.FUELTYPE));
            }
            if (!string.IsNullOrEmpty(sw.HOTTYPE))
            {
                sb.AppendFormat(" AND HOTTYPE ={0}", ClsSql.EncodeSql(sw.HOTTYPE));
            }
            if (!string.IsNullOrEmpty(sw.CHECKTIME))
            {
                sb.AppendFormat(" AND CHECKTIME ={0}", ClsSql.EncodeSql(sw.CHECKTIME));
            }
            if (!string.IsNullOrEmpty(sw.YY))
            {
                sb.AppendFormat(" AND YY ={0}", ClsSql.EncodeSql(sw.YY));

            }
            if (!string.IsNullOrEmpty(sw.JXHQSJ))
            {
                sb.AppendFormat(" AND JXHQSJ ={0}", ClsSql.EncodeSql(sw.JXHQSJ));

            }
            if (!string.IsNullOrEmpty(sw.FIREBEGINTIME))
            {
                sb.AppendFormat(" AND FIREBEGINTIME ={0}", ClsSql.EncodeSql(sw.FIREBEGINTIME));

            }
            if (!string.IsNullOrEmpty(sw.ISOUTFIRE))
            {
                sb.AppendFormat(" AND ISOUTFIRE ={0}", ClsSql.EncodeSql(sw.ISOUTFIRE));
            }
            if (!string.IsNullOrEmpty(sw.OVERDOAREA))
            {
                sb.AppendFormat(" AND OVERDOAREA ={0}", ClsSql.EncodeSql(sw.OVERDOAREA));
            }
            if (!string.IsNullOrEmpty(sw.LOSTFORESTAREA))
            {
                sb.AppendFormat(" AND LOSTFORESTAREA ={0}", ClsSql.EncodeSql(sw.LOSTFORESTAREA));
            }
            if (!string.IsNullOrEmpty(sw.ELSELOSSINTRO))
            {
                sb.AppendFormat(" AND ELSELOSSINTRO ={0}", ClsSql.EncodeSql(sw.ELSELOSSINTRO));
            }
            if (!string.IsNullOrEmpty(sw.FIREINTRO))
            {
                sb.AppendFormat(" AND FIREINTRO ={0}", ClsSql.EncodeSql(sw.FIREINTRO));
            }
            if (!string.IsNullOrEmpty(sw.BYORGNO))
            {
                sb.AppendFormat(" AND BYORGNO ={0}", ClsSql.EncodeSql(sw.BYORGNO));
            }
            if (!string.IsNullOrEmpty(sw.MANUSERID))
            {
                sb.AppendFormat(" AND MANUSERID ={0}", ClsSql.EncodeSql(sw.MANUSERID));
            }
            if (!string.IsNullOrEmpty(sw.MANTIME))
            {
                sb.AppendFormat(" AND MANTIME ={0}", ClsSql.EncodeSql(sw.MANTIME));
            }
            if (!string.IsNullOrEmpty(sw.MANSTATE))
            {
                sb.AppendFormat(" AND MANSTATE ={0}", ClsSql.EncodeSql(sw.MANSTATE));
            }
            if (!string.IsNullOrEmpty(sw.AUDITREASON))
            {
                sb.AppendFormat(" AND AUDITREASON ={0}", ClsSql.EncodeSql(sw.AUDITREASON));
            }
            if (!string.IsNullOrEmpty(sw.ADDRESS))
            {
                sb.AppendFormat(" AND ADDRESS ={0}", ClsSql.EncodeSql(sw.ADDRESS));
            }
            if (!string.IsNullOrEmpty(sw.JD))
            {
                sb.AppendFormat(" AND JD ={0}", ClsSql.EncodeSql(sw.JD));
            }
            if (!string.IsNullOrEmpty(sw.WD))
            {
                sb.AppendFormat(" AND WD ={0}", ClsSql.EncodeSql(sw.WD));
            }
            sb.AppendFormat(" order by MANTIME desc ");
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }

        #endregion

        #region 根据jcfid获取最新的热点类别
        /// <summary>
        /// 根据jcfid获取最新的热点类别
        /// </summary>
        /// <param name="dt">火情反馈最新DataTable</param>
        /// <param name="value">jcfid</param>
        /// <returns>热点类别</returns>
        public static string gethotetype(DataTable dt, string value)
        {
            if (dt == null)
                return "";
            if (string.IsNullOrEmpty(value))
                return "";
            string str = "";
            DataRow[] dr = dt.Select("JCFID='" + value + "'");
            if (dr.Length > 0)
                str = dr[0]["HOTTYPE"].ToString();
            return str;
        }
        #endregion

    }
}
