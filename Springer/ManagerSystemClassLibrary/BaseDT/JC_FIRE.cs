using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel;
using DataBaseClassLibrary;
using PublicClassLibrary;

namespace ManagerSystemClassLibrary.BaseDT
{
    /// <summary>
    /// 监测_火情表
    /// </summary>
    public class JC_FIRE
    {
        #region 增加 修改

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(JC_FIRE_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  JC_FIRE(BYORGNO,FIRENAME, FIREFROM, FIREFROMID, FIRETIME, MARK, JD, WD, ZQWZ, WXBH, DQRDBH, RSMJ, DL, YY, JXHQSJ,OWERJCFID,PFUSERID,PFORGNO,PFTIME,PFFLAG,RECEIVETIME,FIREFROMWEATHER )");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.BYORGNO));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.FIRENAME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.FIREFROM));
            if (string.IsNullOrEmpty(m.FIREFROMID))
                sb.AppendFormat(",null");
            else
                sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.FIREFROMID));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.FIRETIME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.MARK));
            if (string.IsNullOrEmpty(m.JD))
                sb.AppendFormat(",null");
            else
                sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.JD));
            if (string.IsNullOrEmpty(m.WD))
                sb.AppendFormat(",null");
            else
                sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.WD));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ZQWZ));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.WXBH));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.DQRDBH));
            if (string.IsNullOrEmpty(m.RSMJ))
                sb.AppendFormat(",null");
            else
                sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.RSMJ));

            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.DL));

            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.YY));
            if (string.IsNullOrEmpty(m.JXHQSJ))
                sb.AppendFormat(",null");
            else
                sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.JXHQSJ));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.OWERJCFID));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.PFUSERID));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.PFORGNO));
            if (string.IsNullOrEmpty(m.PFTIME))
                sb.AppendFormat(",null");
            else
                sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.PFTIME));
            if (string.IsNullOrEmpty(m.PFFLAG))
            {
                m.PFFLAG = "0";
            }
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.PFFLAG));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.RECEIVETIME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.FIREFROMWEATHER));
            sb.AppendFormat(")");
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "添加成功！", "");
            else
                return new Message(false, "添加失败，请检查各输入框是否正确！", "");
        }

        /// <summary>
        /// 批量添加更新
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message PLAdd(JC_FIRE_Model m)
        {
            List<string> sqllist = new List<string>();
            StringBuilder sb = new StringBuilder();

            var arrBYORGNAME = m.ZQWZ.Split(',');
            var arrBYORGNO = m.BYORGNO.Split(',');
            var arrRSMJ = m.RSMJ.Split(',');
            var arrJD = m.JD.Split(',');
            var arrWD = m.WD.Split(',');
            var arrFIRETIME = m.FIRETIME.Split(',');

            for (int i = 0; i < arrBYORGNAME.Length - 1; i++)
            {
                string FIRENAME = arrBYORGNAME[i] + " " + arrFIRETIME[i] + " " + "气象热点火情";
                if (DataBaseClass.JudgeRecordExists("select 1 from JC_FIRE where convert(char(10),FIRETIME,120)='" + Convert.ToDateTime(arrFIRETIME[i]).ToString("yyyy-MM-dd") + "' and BYORGNO='" + arrBYORGNO[i] + "' and FIREFROMWEATHER='1'"))
                {
                    sb.AppendFormat("UPDATE JC_FIRE SET ");
                    sb.AppendFormat(" BYORGNO= '{0}',", ClsSql.EncodeSql(arrBYORGNO[i]));
                    sb.AppendFormat(" ZQWZ='{0}',", ClsSql.EncodeSql(arrBYORGNAME[i]));
                    sb.AppendFormat(" RSMJ= '{0}',", ClsSql.EncodeSql(arrRSMJ[i]));
                    sb.AppendFormat(" JD= '{0}',", ClsSql.EncodeSql(arrJD[i]));
                    sb.AppendFormat(" WD= '{0}',", ClsSql.EncodeSql(arrWD[i]));
                    sb.AppendFormat(" FIRETIME= '{0}'", ClsSql.EncodeSql(arrFIRETIME[i]));
                    sb.AppendFormat(" where convert(char(10),FIRETIME,120)= '{0}'", ClsSql.EncodeSql(Convert.ToDateTime(arrFIRETIME[i]).ToString("yyyy-MM-dd")));
                    sb.AppendFormat(" and BYORGNO = '{0}'", ClsSql.EncodeSql(arrBYORGNO[i]));
                    sb.AppendFormat(" and FIREFROMWEATHER = '1'");
                    //sb.AppendFormat(" and WD = '{0}'", ClsSql.EncodeSql(arrWD[i]));
                    sqllist.Add(sb.ToString());
                    sb.Remove(0, sb.Length);
                }
                else
                {
                    sb.AppendFormat("INSERT INTO  JC_FIRE(BYORGNO,FIRENAME, FIREFROM, FIREFROMID, FIRETIME, MARK, JD, WD, ZQWZ, WXBH, DQRDBH, RSMJ, DL, YY, JXHQSJ,OWERJCFID,PFUSERID,PFORGNO,PFTIME,PFFLAG,RECEIVETIME,FIREFROMWEATHER )");
                    sb.AppendFormat(" select '{0}'", ClsSql.EncodeSql(arrBYORGNO[i]));
                    sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(FIRENAME));
                    sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.FIREFROM));
                    if (string.IsNullOrEmpty(m.FIREFROMID))
                        sb.AppendFormat(",null");
                    else
                        sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.FIREFROMID));
                    sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(arrFIRETIME[i]));
                    sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.MARK));
                    if (string.IsNullOrEmpty(m.JD))
                        sb.AppendFormat(",null");
                    else
                        sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(arrJD[i]));
                    if (string.IsNullOrEmpty(m.WD))
                        sb.AppendFormat(",null");
                    else
                        sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(arrWD[i]));
                    sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(arrBYORGNAME[i]));
                    sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.WXBH));
                    sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.DQRDBH));
                    if (string.IsNullOrEmpty(m.RSMJ))
                        sb.AppendFormat(",null");
                    else
                        sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(arrRSMJ[i]));
                    sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.DL));
                    sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.YY));
                    if (string.IsNullOrEmpty(m.JXHQSJ))
                        sb.AppendFormat(",null");
                    else
                        sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.JXHQSJ));
                    sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.OWERJCFID));
                    sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.PFUSERID));
                    sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.PFORGNO));
                    if (string.IsNullOrEmpty(m.PFTIME))
                        sb.AppendFormat(",null");
                    else
                        sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.PFTIME));
                    sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.PFFLAG));
                    sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.RECEIVETIME));
                    sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.FIREFROMWEATHER));
                    sqllist.Add(sb.ToString());
                    sb.Remove(0, sb.Length);
                }
            }

            var j = DataBaseClass.ExecuteSqlTran(sqllist);
            if (j > 0)
            {
                return new Message(true, "保存成功！", "");
            }
            else
            {
                return new Message(false, "保存失败，事物回滚机制！", "");
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Mdy(JC_FIRE_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE JC_FIRE set ");
            if (!string.IsNullOrEmpty(m.MANSTATE))
            {
                sb.AppendFormat("MANSTATE='{0}'", ClsSql.EncodeSql(m.MANSTATE));
            }
            sb.AppendFormat(",BYORGNO='{0}'", ClsSql.EncodeSql(m.BYORGNO));
            sb.AppendFormat(",ISSUEDTIME='{0}'", ClsSql.EncodeSql(m.ISSUEDTIME));//下发时间
            sb.AppendFormat(",LASTPROCESSTIME='{0}'", ClsSql.EncodeSql(m.LASTPROCESSTIME));
            if (!string.IsNullOrEmpty(m.PFFLAG))
            {
                sb.AppendFormat(",PFFLAG='{0}'", ClsSql.EncodeSql(m.PFFLAG));
            }
            sb.AppendFormat(" where JCFID= '{0}'", ClsSql.EncodeSql(m.JCFID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功！", "");
            else
                return new Message(false, "修改失败！", "");
        }



        /// <summary>
        /// 修改火情已灭
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message MdyFireOver(JC_FIRE_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE JC_FIRE set ");
            sb.AppendFormat(" FIREENDTIME='{0}'", ClsSql.EncodeSql(m.FIREENDTIME));//火情结束时间
            sb.AppendFormat(",ISOUTFIRE='{0}'", ClsSql.EncodeSql(m.ISOUTFIRE));
            sb.AppendFormat(" where JCFID= '{0}'", ClsSql.EncodeSql(m.JCFID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功！", "");
            else
                return new Message(false, "修改失败！", "");
        }
        #endregion

        #region 根据火灾等级及单位编码获取该单位下火灾数据
        /// <summary>
        /// 根据火灾等级及单位编码获取该单位下火灾数据
        /// </summary>
        /// <param name="dt">dt</param>
        /// <param name="ORGNO">单位编码</param>
        /// <param name="level">火灾等级</param>
        /// <returns></returns>
        public static string getFireCountByOrgLevel(DataTable dt, string ORGNO, string level)
        {

            if (PublicCls.OrgIsShi(ORGNO))//市
            {
                if (string.IsNullOrEmpty(level))
                    return dt.Compute("count(JCFID)", "substring(BYORGNO,1,4)='" + PublicCls.getShiIncOrgNo(ORGNO) + "'").ToString();
                else
                    return dt.Compute("count(JCFID)", "substring(BYORGNO,1,4)='" + PublicCls.getShiIncOrgNo(ORGNO) + "' and FIRELEVEL='" + level + "'").ToString();
            }
            else if (PublicCls.OrgIsXian(ORGNO))//县
            {
                if (string.IsNullOrEmpty(level))
                    return dt.Compute("count(JCFID)", "substring(BYORGNO,1,6)='" + PublicCls.getXianIncOrgNo(ORGNO) + "'").ToString();
                else
                    return dt.Compute("count(JCFID)", "substring(BYORGNO,1,6)='" + PublicCls.getXianIncOrgNo(ORGNO) + "' and FIRELEVEL='" + level + "'").ToString();
            }
            else
            {
                if (string.IsNullOrEmpty(level))
                    return dt.Compute("count(JCFID)", "BYORGNO='" + PublicCls.getZhenIncOrgNo(ORGNO) + "'").ToString();
                else
                    return dt.Compute("count(JCFID)", "BYORGNO='" + PublicCls.getZhenIncOrgNo(ORGNO) + "' and FIRELEVEL='" + level + "'").ToString();
            }
        }
        #endregion

        #region 获取火灾及火灾等级数据
        /// <summary>
        /// 获取火灾及火灾等级数据
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable GetDTFireProp(JC_FIRE_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" SELECT   ");
            sb.AppendFormat(" a.JCFID, a.FIRENAME, a.BYORGNO, a.FIREFROM, a.FIREFROMID, a.FIRETIME, a.FIREENDTIME, a.ISOUTFIRE, a.MARK, ");
            sb.AppendFormat(" a.JD, a.WD, a.ZQWZ, a.WXBH, a.DQRDBH, a.RSMJ, a.DL, a.YY, a.JXHQSJ, a.RECEIVETIME, a.ISSUEDTIME, ");
            sb.AppendFormat(" a.LASTPROCESSTIME, a.MANSTATE, b.JC_FIRE_PROPID, b.GHMJ, b.GHLDMJ, b.SHSLMJ, b.RYS, b.RYW, b.MGSD, ");
            sb.AppendFormat(" b.ZDQY, b.GJJL, b.ZZH, b.QHS, b.SSJB, b.FIRELEVEL");

            sb.AppendFormat(" FROM      JC_FIRE AS a LEFT OUTER JOIN");
            sb.AppendFormat("                 JC_FIRE_PROP AS b ON a.JCFID = b.JCFID");
            sb.AppendFormat("                 where 1=1");
            if (!string.IsNullOrEmpty(sw.JCFID))
            {
                sb.AppendFormat(" AND JCFID ='{0}'", ClsSql.EncodeSql(sw.JCFID));
            }
            if (!string.IsNullOrEmpty(sw.ISOUTFIRE))
            {
                sb.AppendFormat(" AND ISOUTFIRE ='{0}'", ClsSql.EncodeSql(sw.ISOUTFIRE));
            }
            if (!string.IsNullOrEmpty(sw.FIRELEVEL))
            {
                sb.AppendFormat(" AND FIRELEVEL ='{0}'", ClsSql.EncodeSql(sw.BYORGNO));
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
            if (!string.IsNullOrEmpty(sw.BYORGNO))
            {
                sb.AppendFormat(" AND BYORGNO ='{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            }
            sb.AppendFormat(" order by a.JCFID desc ");
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }

        #endregion

        #region 获取监测获取信息
        /// <summary>
        /// 获取监测获取信息
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable GetDTCount(JC_FIRE_SW sw)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" Select * from JC_FIRE  Where 1=1 ");
            if (!string.IsNullOrEmpty(sw.JCFID))
            {
                sb.AppendFormat(" AND JCFID ={0}", ClsSql.EncodeSql(sw.JCFID));
            }
            if (!string.IsNullOrEmpty(sw.ISOUTFIRE))
            {
                sb.AppendFormat(" AND ISOUTFIRE ={0}", ClsSql.EncodeSql(sw.ISOUTFIRE));
            }
            if (!string.IsNullOrEmpty(sw.BYORGNO))
            {
                sb.AppendFormat(" AND BYORGNO ={0}", ClsSql.EncodeSql(sw.BYORGNO));
            }
            if (!string.IsNullOrEmpty(sw.FIREFROM))
            {
                sb.AppendFormat(" AND FIREFROM ={0}", ClsSql.EncodeSql(sw.FIREFROM));
            }
            if (!string.IsNullOrEmpty(sw.FIREFROMID))
            {
                sb.AppendFormat(" AND FIREFROMID ={0}", ClsSql.EncodeSql(sw.FIREFROMID));
            }
            if (!string.IsNullOrEmpty(sw.MANSTATE))
            {
                sb.AppendFormat(" AND MANSTATE ={0}", ClsSql.EncodeSql(sw.MANSTATE));
            }
            if (string.IsNullOrEmpty(sw.isCountIndex) == false)
            {
                //用于首页统计未处理数量
                //p.ISOUTFIRE.Trim() == "1" && p.MANSTATE.Trim() != "4") || p.ISOUTFIRE.Trim() != "1"));//筛选热点类型 排除已灭的

                sb.AppendFormat(" AND ((ISOUTFIRE='1' and MANSTATE<>'4') or ISOUTFIRE<>'1')");
            }
            sb.AppendFormat(" order by JCFID desc ");
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }

        #endregion

        #region 获取监测获取信息
        /// <summary>
        /// 获取监测获取信息
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable GetDT(JC_FIRE_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" Select a.* from JC_FIRE a  Where 1=1 ");
            if (!string.IsNullOrEmpty(sw.JCFID))
            {
                sb.AppendFormat(" AND a.JCFID ={0}", ClsSql.EncodeSql(sw.JCFID));
            }
            if (!string.IsNullOrEmpty(sw.JCFIDSTR))
            {
                sb.AppendFormat(" AND a.JCFID  in ({0})", ClsSql.SwitchStrToSqlIn(sw.JCFIDSTR));
            }
            if (!string.IsNullOrEmpty(sw.ISOUTFIRE))
            {
                sb.AppendFormat(" AND a.ISOUTFIRE ={0}", ClsSql.EncodeSql(sw.ISOUTFIRE));
            }
            //if (!string.IsNullOrEmpty(sw.BYORGNO))
            //{
            //    sb.AppendFormat(" AND BYORGNO ={0}", ClsSql.EncodeSql(sw.BYORGNO));
            //}
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
            {
                if (PublicCls.OrgIsShi(sw.BYORGNO.Trim()))
                {
                    sb.AppendFormat(" and a.BYORGNO like '{0}%'", PublicCls.getShiIncOrgNo(sw.BYORGNO));
                }
                else if (PublicCls.OrgIsXian(sw.BYORGNO.Trim()))
                {
                    sb.AppendFormat(" and a.BYORGNO like  '{0}%'", PublicCls.getXianIncOrgNo(sw.BYORGNO));
                }
                else
                {
                    sb.AppendFormat(" and a.BYORGNO = '{0}'", PublicCls.getZhenIncOrgNo(sw.BYORGNO));
                }
            }
            if (!string.IsNullOrEmpty(sw.FIRETIME))
            {
                sb.AppendFormat(" AND convert(char(10),a.FIRETIME,120)='{0}'", ClsSql.EncodeSql(sw.FIRETIME));
            }
            if (!string.IsNullOrEmpty(sw.BeginTime) && !string.IsNullOrEmpty(sw.EndTime))
            {
                sb.AppendFormat(" AND a.RECEIVETIME >='{0} 00:00:00'", sw.BeginTime);
                sb.AppendFormat(" AND a.RECEIVETIME <='{0} 23:59:59'", sw.EndTime);
            }

            if (!string.IsNullOrEmpty(sw.ISOUTFIRE))
            {
                sb.AppendFormat(" AND a.ISOUTFIRE ={0}", ClsSql.EncodeSql(sw.ISOUTFIRE));
            }
            if (!string.IsNullOrEmpty(sw.FIREFROM))
            {
                sb.AppendFormat(" AND a.FIREFROM ={0}", ClsSql.EncodeSql(sw.FIREFROM));
            }
            if (!string.IsNullOrEmpty(sw.FIREFROMWEATHER))
            {
                sb.AppendFormat(" AND a.FIREFROMWEATHER ={0}", ClsSql.EncodeSql(sw.FIREFROMWEATHER));
            }
            if (!string.IsNullOrEmpty(sw.FIREFROMID))
            {
                sb.AppendFormat(" AND a.FIREFROMID ={0}", ClsSql.EncodeSql(sw.FIREFROMID));
            }
            if (!string.IsNullOrEmpty(sw.MANSTATE))
            {
                sb.AppendFormat(" AND a.MANSTATE ={0}", ClsSql.EncodeSql(sw.MANSTATE));
            }
            if (!string.IsNullOrEmpty(sw.MANSTATESTR))
            {
                sb.AppendFormat(" AND a.MANSTATE in ({0})", ClsSql.SwitchStrToSqlIn(sw.MANSTATESTR));
            }
            sb.AppendFormat(" order by a.FIRETIME desc ");
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 获取监测获取信息——优化后
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable GetDTYH(JC_FIRE_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select a.ZQWZ,a.FIRENAME,a.JCFID, a.JD,a.WD,a.BYORGNO ");
            sb.AppendFormat(" ,(select top 1 FIRELEVEL from  JC_FIRE_PROP where jcfid =a.jcfid order by JC_FIRE_PROPID desc)");
            sb.AppendFormat(" as FIRELEVEL from JC_FIRE a where ISOUTFIRE<>1");
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
            {
                if (PublicCls.OrgIsShi(sw.BYORGNO.Trim()))
                {
                    sb.AppendFormat(" and BYORGNO like '{0}%'", PublicCls.getShiIncOrgNo(sw.BYORGNO));
                }
                else if (PublicCls.OrgIsXian(sw.BYORGNO.Trim()))
                {
                    sb.AppendFormat(" and BYORGNO like  '{0}%'", PublicCls.getXianIncOrgNo(sw.BYORGNO));
                }
                else
                {
                    sb.AppendFormat(" and BYORGNO = '{0}'", PublicCls.getZhenIncOrgNo(sw.BYORGNO));
                }
            }
            sb.AppendFormat(" and jcfid in ");
            sb.AppendFormat(" (select jcfid from JC_FIRETICKLING where ISOUTFIRE=0");
            sb.AppendFormat(" and jcfid =a.JCFID and HOTTYPE in(1,6,10))");
            sb.AppendFormat(" order by jcfid desc");
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        
        #endregion

        #region 获取签收反馈个数
        /// <summary>
        /// 获取签收反馈个数
        /// </summary>
        /// <param name="value"></param>
        /// <param name="ftype"></param>
        /// <param name="orgnostr"></param>
        /// <param name="eqtype"></param>
        /// <returns></returns>
        public static int GetCount(string value, string ftype, string orgnostr, string eqtype)
        {
            //select * from JC_FIRE a where a.JCFID in(select JCFID from JC_FIRETICKLING b where  b.MANSTATE!='1')
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" Select  count(1) from JC_FIRE a Where  ");
            if (eqtype == "0")
            {
                sb.AppendFormat(" exists (select JCFID from JC_FIRETICKLING b where  b.MANSTATE in ({0}) and b.jcfid=a.jcfid ) ", ClsSql.EncodeSql(value));
            }
            else
            {
                sb.AppendFormat(" not exists (select JCFID from JC_FIRETICKLING b where  b.MANSTATE in ({0}) and b.jcfid=a.jcfid) ", ClsSql.EncodeSql(value));
            }
            sb.AppendFormat("  And a.ISOUTFIRE!='1' and a.MANSTATE!='19' and a.MANSTATE!='18'");
            if (!string.IsNullOrEmpty(ftype))
            {
                sb.AppendFormat("  And a.FIREFROM='{0}'  ", ClsSql.EncodeSql(ftype));
            }
            if (!string.IsNullOrEmpty(orgnostr))
            {
                sb.AppendFormat("  And a.BYORGNO like '{0}%' ", ClsSql.EncodeSql(orgnostr));
            }
            var i = int.Parse(DataBaseClass.ReturnSqlField(sb.ToString()));
            return i;
        }
        #endregion

        #region 获取监测获取信息和数量 分页
        /// <summary>
        /// 获取监测获取信息和数量
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="total"></param>
        /// <param name="falg">0 分页 1 不分页</param>
        /// <returns></returns>
        public static DataTable GetDTAndTotal(JC_FIRE_SW sw, out int total, string falg = "0")
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Left Join JC_FIRETICKLING b   on a.JCFID=b.JCFID ");
            sb.Append(" Where  (b.FKID=(select max(fkid) from JC_FIRETICKLING where jcfid=b.jcfid) or b.fkid is null) ");
            if (!string.IsNullOrEmpty(sw.BYORGNO))
            {
                if (sw.BYORGNO.Substring(4, 5) == "00000")//获取所有市的
                    sb.AppendFormat(" AND (SUBSTRING(a.BYORGNO,1,4) = '{0}' or a.BYORGNO is null or a.BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
                else if (sw.BYORGNO.Substring(6, 3) == "000")//获取所有县的
                    sb.AppendFormat(" AND (SUBSTRING(a.BYORGNO,1,6) = '{0}' or a.BYORGNO is null or a.BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
                else
                    sb.AppendFormat(" AND (a.BYORGNO = '{0}' or a.BYORGNO is null or a.BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO));
            }
            if (!string.IsNullOrEmpty(sw.FIRENAME))
            {
                sb.AppendFormat(" AND a.FIRENAME like '%{0}%'", ClsSql.EncodeSql(sw.FIRENAME));
            }
            if (!string.IsNullOrEmpty(sw.FIREFROM))
            {
                sb.AppendFormat(" AND a.FIREFROM ='{0}'", ClsSql.EncodeSql(sw.FIREFROM));
            }
            if (!string.IsNullOrEmpty(sw.FIREFROMID))
            {
                sb.AppendFormat(" AND a.FIREFROMID ={0}", ClsSql.EncodeSql(sw.FIREFROMID));
            }
            if (!string.IsNullOrEmpty(sw.ISOUTFIRE))
            {
                sb.AppendFormat(" AND a.ISOUTFIRE ={0}", ClsSql.EncodeSql(sw.ISOUTFIRE));
            }
            if (!string.IsNullOrEmpty(sw.MANSTATE))
            {
                sb.AppendFormat(" AND a.MANSTATE in ({0})", ClsSql.EncodeSql(sw.MANSTATE));
            }

            if (!string.IsNullOrEmpty(sw.BeginTime) && !string.IsNullOrEmpty(sw.EndTime))
            {
                sb.AppendFormat(" AND a.RECEIVETIME >='{0} 00:00:00'", sw.BeginTime);
                sb.AppendFormat(" AND a.RECEIVETIME <='{0} 23:59:59'", sw.EndTime);
            }
            if (!string.IsNullOrEmpty(sw.HOTTYPE))
            {
                sb.AppendFormat(" AND b.HOTTYPE ={0}", ClsSql.EncodeSql(sw.HOTTYPE));
            }

            string sql = " Select * from JC_FIRE a " + sb.ToString() + " order by a.FIRETIME desc ";
            string sqlC = " Select count(*) from JC_FIRE a" + sb.ToString();
            total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
            //(sw.PageIndex - 1) * sw.PageRow, sw.PageRow
            if (falg == "1")
            {
                DataSet ds = DataBaseClass.FullDataSet(sql);
                return ds.Tables[0];
            }
            else
            {
                sw.curPage = PagerCls.getCurPage(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
                DataSet ds = DataBaseClass.FullDataSet(sql, (sw.curPage - 1) * sw.pageSize, sw.pageSize, "a");

                return ds.Tables[0];
            }
        }

        #endregion

        //#region 获取数据中心档案
        ///// <summary>
        ///// 获取数据中心档案
        ///// </summary>
        ///// <param name="sw"></param>
        ///// <param name="total"></param>
        ///// <returns></returns>
        //public static DataTable GetDCDT(JC_FIRE_SW sw, out int total)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append(" Left Join JC_FIRETICKLING b   on a.JCFID=b.JCFID ");
        //    sb.Append(" Where  (b.FKID=(select max(fkid) from JC_FIRETICKLING where jcfid=b.jcfid) or b.fkid is null) ");
        //    if (!string.IsNullOrEmpty(sw.BYORGNO))
        //    {
        //        if (sw.BYORGNO.Substring(4, 5) == "00000")//获取所有市的
        //            sb.AppendFormat(" AND (SUBSTRING(a.BYORGNO,1,4) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
        //        else if (sw.BYORGNO.Substring(4, 5) == "xxxxx")//单独市
        //            sb.AppendFormat(" AND (SUBSTRING(a.BYORGNO,1,9) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4) + "00000"));
        //        else if (sw.BYORGNO.Substring(6, 3) == "xxx")//获取所有县的
        //            sb.AppendFormat(" AND (SUBSTRING(a.BYORGNO,1,6) = '{0}' )", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
        //        else
        //            sb.AppendFormat(" AND a.BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
        //    }
        //    if (!string.IsNullOrEmpty(sw.FIRENAME))
        //    {
        //        sb.AppendFormat(" AND a.FIRENAME like '%{0}%'", ClsSql.EncodeSql(sw.FIRENAME));
        //    }
        //    if (!string.IsNullOrEmpty(sw.FIREFROM))
        //    {
        //        sb.AppendFormat(" AND a.FIREFROM ='{0}'", ClsSql.EncodeSql(sw.FIREFROM));
        //    }
        //    if (!string.IsNullOrEmpty(sw.FIREFROMID))
        //    {
        //        sb.AppendFormat(" AND a.FIREFROMID ={0}", ClsSql.EncodeSql(sw.FIREFROMID));
        //    }
        //    if (!string.IsNullOrEmpty(sw.ISOUTFIRE))
        //    {
        //        sb.AppendFormat(" AND a.ISOUTFIRE ={0}", ClsSql.EncodeSql(sw.ISOUTFIRE));
        //    }
        //    if (!string.IsNullOrEmpty(sw.MANSTATE))
        //    {
        //        sb.AppendFormat(" AND a.MANSTATE in ({0})", ClsSql.EncodeSql(sw.MANSTATE));
        //    }

        //    if (!string.IsNullOrEmpty(sw.BeginTime) && !string.IsNullOrEmpty(sw.EndTime))
        //    {
        //        sb.AppendFormat(" AND a.RECEIVETIME >='{0} 00:00:00'", sw.BeginTime);
        //        sb.AppendFormat(" AND a.RECEIVETIME <='{0} 23:59:59'", sw.EndTime);
        //    }
        //    if (!string.IsNullOrEmpty(sw.HOTTYPE))
        //    {
        //        sb.AppendFormat(" AND b.HOTTYPE ={0}", ClsSql.EncodeSql(sw.HOTTYPE));
        //    }

        //    string sql = " Select * from JC_FIRE a " + sb.ToString() + " order by a.JCFID desc ";
        //    string sqlC = " Select count(*) from JC_FIRE a" + sb.ToString();
        //    total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
        //    //(sw.PageIndex - 1) * sw.PageRow, sw.PageRow


        //    sw.curPage = PagerCls.getCurPage(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
        //    DataSet ds = DataBaseClass.FullDataSet(sql, (sw.curPage - 1) * sw.pageSize, sw.pageSize, "a");

        //    return ds.Tables[0];

        //}

        //#endregion

        #region 签收（反馈）火情事务
        /// <summary>
        /// 签收火情事务
        /// </summary>
        /// <param name="jcfire"></param>
        /// <param name="jcfirefk"></param>
        /// <returns></returns>
        public static Message QSFire(JC_FIRE_Model jcfire, JC_FIRETICKLING_SW jcfirefk)
        {
            Message ms = null;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE JC_FIRE set ");
            if (!string.IsNullOrEmpty(jcfire.MANSTATE))
            {
                sb.AppendFormat("MANSTATE='{0}'", ClsSql.EncodeSql(jcfire.MANSTATE));
            }
            if (!string.IsNullOrEmpty(jcfire.BYORGNO))
            {
                sb.AppendFormat(",BYORGNO='{0}'", ClsSql.EncodeSql(jcfire.BYORGNO));
            }
            if (!string.IsNullOrEmpty(jcfire.ISSUEDTIME))
            {
                sb.AppendFormat(",ISSUEDTIME='{0}'", ClsSql.EncodeSql(jcfire.ISSUEDTIME));//下发时间
            }
            if (!string.IsNullOrEmpty(jcfirefk.FIREENDTIME))//灭火时间
            {
                sb.AppendFormat(",FIREENDTIME='{0}'", ClsSql.EncodeSql(jcfirefk.FIREENDTIME));//灭火时间
            }
            if (!string.IsNullOrEmpty(jcfirefk.ISOUTFIRE))
            {
                sb.AppendFormat(",ISOUTFIRE={0}", ClsSql.EncodeSql(jcfirefk.ISOUTFIRE));//是否已灭 0 未灭 1 为已灭
            }
            sb.AppendFormat(",LASTPROCESSTIME='{0}'", ClsSql.EncodeSql(jcfire.LASTPROCESSTIME));//最后处理时间
            sb.AppendFormat(" where JCFID= '{0}'", ClsSql.EncodeSql(jcfire.JCFID));
            StringBuilder sb1 = new StringBuilder();
            sb1.AppendFormat("INSERT INTO  JC_FIRETICKLING(JCFID,DL,FORESTNAME,FORESTFIRETYPE,FUELTYPE,HOTTYPE,CHECKTIME,YY,JXHQSJ,FIREBEGINTIME,FIREENDTIME,ISOUTFIRE,BURNEDAREA,OVERDOAREA,LOSTFORESTAREA,ELSELOSSINTRO,FIREINTRO,AUDITREASON,ADDRESS,JD,WD,BYORGNO,MANUSERID,MANTIME,MANSTATE)");
            sb1.AppendFormat("VALUES(");
            sb1.AppendFormat(" '{0}'", ClsSql.EncodeSql(jcfirefk.JCFID));
            sb1.AppendFormat(",'{0}'", ClsSql.EncodeSql(jcfirefk.DL));
            sb1.AppendFormat(",'{0}'", ClsSql.EncodeSql(jcfirefk.FORESTNAME));
            sb1.AppendFormat(",'{0}'", ClsSql.EncodeSql(jcfirefk.FORESTFIRETYPE));
            sb1.AppendFormat(",'{0}'", ClsSql.EncodeSql(jcfirefk.FUELTYPE));
            sb1.AppendFormat(",'{0}'", ClsSql.EncodeSql(jcfirefk.HOTTYPE));
            if (string.IsNullOrEmpty(jcfirefk.CHECKTIME))
            {
                sb1.AppendFormat(",NULL");
            }
            else
            {
                sb1.AppendFormat(",'{0}'", ClsSql.EncodeSql(jcfirefk.CHECKTIME));
            }
            sb1.AppendFormat(",'{0}'", ClsSql.EncodeSql(jcfirefk.YY));
            sb1.AppendFormat(",'{0}'", ClsSql.EncodeSql(jcfirefk.JXHQSJ));
            if (string.IsNullOrEmpty(jcfirefk.FIREBEGINTIME))
            {
                sb1.AppendFormat(",NULL");
            }
            else
            {
                sb1.AppendFormat(",'{0}'", ClsSql.EncodeSql(jcfirefk.FIREBEGINTIME));
            }
            if (string.IsNullOrEmpty(jcfirefk.FIREENDTIME))
            {
                sb1.AppendFormat(",NULL");
            }
            else
            {
                sb1.AppendFormat(",'{0}'", ClsSql.EncodeSql(jcfirefk.FIREENDTIME));
            }

            sb1.AppendFormat(",'{0}'", ClsSql.EncodeSql(jcfirefk.ISOUTFIRE));
            sb1.AppendFormat(",'{0}'", ClsSql.EncodeSql(jcfirefk.BURNEDAREA));
            sb1.AppendFormat(",'{0}'", ClsSql.EncodeSql(jcfirefk.OVERDOAREA));
            sb1.AppendFormat(",'{0}'", ClsSql.EncodeSql(jcfirefk.LOSTFORESTAREA));
            sb1.AppendFormat(",'{0}'", ClsSql.EncodeSql(jcfirefk.ELSELOSSINTRO));
            sb1.AppendFormat(",'{0}'", ClsSql.EncodeSql(jcfirefk.FIREINTRO));
            sb1.AppendFormat(",'{0}'", ClsSql.EncodeSql(jcfirefk.AUDITREASON));
            sb1.AppendFormat(",'{0}'", ClsSql.EncodeSql(jcfirefk.ADDRESS));
            sb1.AppendFormat(",'{0}'", ClsSql.EncodeSql(jcfirefk.JD));
            sb1.AppendFormat(",'{0}'", ClsSql.EncodeSql(jcfirefk.WD));
            sb1.AppendFormat(",'{0}'", ClsSql.EncodeSql(jcfirefk.BYORGNO));
            sb1.AppendFormat(",'{0}'", ClsSql.EncodeSql(jcfirefk.MANUSERID));
            sb1.AppendFormat(",'{0}'", ClsSql.EncodeSql(jcfirefk.MANTIME));
            sb1.AppendFormat(",'{0}'", ClsSql.EncodeSql(jcfirefk.MANSTATE));
            sb1.AppendFormat(")");

            List<string> sqllist = new List<string>();
            sqllist.Add(sb.ToString());
            sqllist.Add(sb1.ToString());
            var i = DataBaseClass.ExecuteSqlTran(sqllist);
            if (i > 0)
            {
                ms = new Message(true, "处理成功！", "");
            }
            else
            {
                ms = new Message(false, "处理失败，事物回滚机制！", "");
            }
            return ms;
        }
        #endregion

        #region 根据DataTable和OrgNo和火情来源、热点类别、火险等级来判断记录个数
        /// <summary>
        /// 根据DataTable和OrgNo和火情来源、热点类别、火险等级来判断记录个数
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="orgNo">组织机构码</param>
        /// <param name="DICTVALUE">类型值</param>
        /// <param name="TYPE">确定统计的类型</param>
        /// <returns>记录个数</returns>
        public static string getCountFIREByOrgNo(DataTable dt, string orgNo, string DICTVALUE, string TYPE)
        {
            if (string.IsNullOrEmpty(orgNo))//返回所有记录个数
                return dt.Rows.Count.ToString();
            if (TYPE == "1")//统计火情来源
            {
                if (PublicCls.OrgIsShi(orgNo))//市
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(JCFID)", "substring(BYORGNO,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "'and FIREFROM in ('1','2','3','4','5','6')").ToString();
                    else
                        return dt.Compute("count(JCFID)", "substring(BYORGNO,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "' and FIREFROM='" + DICTVALUE + "'").ToString();
                }
                else if (PublicCls.OrgIsXian(orgNo))//县
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(JCFID)", "substring(BYORGNO,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "'and FIREFROM in ('1','2','3','4','5','6') ").ToString();
                    else
                        return dt.Compute("count(JCFID)", "substring(BYORGNO,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "' and FIREFROM='" + DICTVALUE + "'").ToString();
                }
                else if (PublicCls.OrgIsZhen(orgNo))
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(JCFID)", "BYORGNO='" + PublicCls.getZhenIncOrgNo(orgNo) + "'and FIREFROM in ('1','2','3','4','5','6')").ToString();
                    else
                        return dt.Compute("count(JCFID)", "BYORGNO='" + PublicCls.getZhenIncOrgNo(orgNo) + "' and FIREFROM='" + DICTVALUE + "'").ToString();
                }
                else //机构编码可能不正确
                    return "";
            }
            if (TYPE == "2")//统计热点类别
            {
                if (PublicCls.OrgIsShi(orgNo))//市
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(JCFID)", "substring(BYORGNO,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "' and HOTTYPE is not null and HOTTYPE <>0 ").ToString();
                    else
                        return dt.Compute("count(JCFID)", "substring(BYORGNO,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "' and HOTTYPE ='" + DICTVALUE + "'").ToString();
                }
                else if (PublicCls.OrgIsXian(orgNo))//县
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(JCFID)", "substring(BYORGNO,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "' and HOTTYPE is not null and HOTTYPE <>0").ToString();
                    else
                        return dt.Compute("count(JCFID)", "substring(BYORGNO,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "' and HOTTYPE ='" + DICTVALUE + "'").ToString();
                }
                else if (PublicCls.OrgIsZhen(orgNo))
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(JCFID)", "BYORGNO='" + PublicCls.getZhenIncOrgNo(orgNo) + "' and HOTTYPE is not null and HOTTYPE <>0").ToString();
                    else
                        return dt.Compute("count(JCFID)", "BYORGNO='" + PublicCls.getZhenIncOrgNo(orgNo) + "' and HOTTYPE ='" + DICTVALUE + "'").ToString();
                }
                else //机构编码可能不正确
                    return "";
            }
            if (TYPE == "3")//火线等级统计
            {
                if (PublicCls.OrgIsShi(orgNo))//市
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(JCFID)", "substring(BYORGNO,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "' and FIRELEVEL  is not null  and FIRELEVEL <>0  ").ToString();
                    else
                        return dt.Compute("count(JCFID)", "substring(BYORGNO,1,4)='" + PublicCls.getShiIncOrgNo(orgNo) + "' and FIRELEVEL = '" + DICTVALUE + "'").ToString();
                }
                else if (PublicCls.OrgIsXian(orgNo))//县
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(JCFID)", "substring(BYORGNO,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "' and FIRELEVEL  is not null  and FIRELEVEL <>0  ").ToString();
                    else
                        return dt.Compute("count(JCFID)", "substring(BYORGNO,1,6)='" + PublicCls.getXianIncOrgNo(orgNo) + "' and FIRELEVEL ='" + DICTVALUE + "'").ToString();
                }
                else if (PublicCls.OrgIsZhen(orgNo))
                {
                    if (string.IsNullOrEmpty(DICTVALUE))
                        return dt.Compute("count(JCFID)", "BYORGNO='" + PublicCls.getZhenIncOrgNo(orgNo) + "' and FIRELEVEL  is not null  and FIRELEVEL <>0 ").ToString();
                    else
                        return dt.Compute("count(JCFID)", "BYORGNO='" + PublicCls.getZhenIncOrgNo(orgNo) + "' and FIRELEVEL  = '" + DICTVALUE + "'").ToString();
                }
                else //机构编码可能不正确
                    return "";
            }

            else
                return "";
        }
        #endregion

        #region 档案统计获取数据
        /// <summary>
        /// 档案统计获取数据
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable GetArchivalDT(JC_FIRE_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" Select JCFID,FIRENAME,BYORGNO,FIREFROM,FIREFROMID,FIRETIME,FIREENDTIME,ISOUTFIRE,MARK,JD,WD,ZQWZ,WXBH,DQRDBH,RSMJ,DL,YY,JXHQSJ,OWERJCFID,PFUSERID,PFORGNO,PFTIME,PFFLAG,RECEIVETIME,ISSUEDTIME,LASTPROCESSTIME,MANSTATE,(select top 1 HOTTYPE from JC_FIRETICKLING where JCFID=a.jcfid order by MANTIME desc) as HOTTYPE,(select top 1 FIRELEVEL from JC_FIRE_PROP where JCFID=a.jcfid order by JC_FIRE_PROPID desc) as FIRELEVEL  from JC_FIRE a  Where 1=1 ");
            sb.AppendFormat(" AND ISOUTFIRE ='1'");
            if (!string.IsNullOrEmpty(sw.JCFID))
            {
                sb.AppendFormat(" AND JCFID ={0}", ClsSql.EncodeSql(sw.JCFID));
            }
            if (!string.IsNullOrEmpty(sw.JCFIDSTR))
            {
                sb.AppendFormat(" AND JCFID  in ({0})", ClsSql.SwitchStrToSqlIn(sw.JCFIDSTR));
            }

            //if (!string.IsNullOrEmpty(sw.BYORGNO))
            //{
            //    sb.AppendFormat(" AND BYORGNO ={0}", ClsSql.EncodeSql(sw.BYORGNO));
            //}
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
            {
                if (PublicCls.OrgIsShi(sw.BYORGNO))
                {
                    sb.AppendFormat(" and BYORGNO like '{0}%'", PublicCls.getShiIncOrgNo(sw.BYORGNO));
                }
                else if (PublicCls.OrgIsXian(sw.BYORGNO))
                {
                    sb.AppendFormat(" and BYORGNO like  '{0}%'", PublicCls.getXianIncOrgNo(sw.BYORGNO));
                }
                else
                {
                    sb.AppendFormat(" and BYORGNO = '{0}'", PublicCls.getZhenIncOrgNo(sw.BYORGNO));
                }
            }
            if (!string.IsNullOrEmpty(sw.BeginTime))
            {
                sb.AppendFormat(" AND FIRETIME>='{0} 00:00:00'", sw.BeginTime);
            }
            if (!string.IsNullOrEmpty(sw.EndTime))
            {
                sb.AppendFormat(" AND FIRETIME<='{0} 23:59:59'", sw.EndTime);
            }
            if (!string.IsNullOrEmpty(sw.FIREFROM))
            {
                sb.AppendFormat(" AND FIREFROM ={0}", ClsSql.EncodeSql(sw.FIREFROM));
            }
            if (!string.IsNullOrEmpty(sw.FIREFROMID))
            {
                sb.AppendFormat(" AND FIREFROMID ={0}", ClsSql.EncodeSql(sw.FIREFROMID));
            }
            if (!string.IsNullOrEmpty(sw.MANSTATE))
            {
                sb.AppendFormat(" AND MANSTATE ={0}", ClsSql.EncodeSql(sw.MANSTATE));
            }
            if (!string.IsNullOrEmpty(sw.MANSTATESTR))
            {
                sb.AppendFormat(" AND MANSTATE in ({0})", ClsSql.SwitchStrToSqlIn(sw.MANSTATESTR));
            }
            sb.AppendFormat(" order by JCFID desc ");
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        #endregion

        #region 档案统计只查询火险等级获取数据
        /// <summary>
        /// 档案统计只查询火险等级获取数据
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable GetDTFirelevel(JC_FIRE_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" Select JCFID,FIRENAME,BYORGNO,FIREFROM,FIREFROMID,FIRETIME,FIREENDTIME,ISOUTFIRE,MARK,JD,WD,ZQWZ,WXBH,DQRDBH,RSMJ,DL,YY,JXHQSJ,OWERJCFID,PFUSERID,PFORGNO,PFTIME,PFFLAG,RECEIVETIME,ISSUEDTIME,LASTPROCESSTIME,MANSTATE,(select top 1 HOTTYPE from JC_FIRETICKLING where JCFID=a.jcfid order by MANTIME desc) as HOTTYPE,(select top 1 FIRELEVEL from JC_FIRE_PROP where JCFID=a.jcfid order by JC_FIRE_PROPID desc) as FIRELEVEL  from JC_FIRE a  Where 1=1 ");
            sb.AppendFormat(" AND ISOUTFIRE ='1' AND (select top 1 FIRELEVEL from JC_FIRE_PROP where JCFID=a.jcfid order by JC_FIRE_PROPID desc)<>0 AND (select top 1 FIRELEVEL from JC_FIRE_PROP where JCFID=a.jcfid order by JC_FIRE_PROPID desc)is not null ");
            if (!string.IsNullOrEmpty(sw.JCFID))
            {
                sb.AppendFormat(" AND JCFID ={0}", ClsSql.EncodeSql(sw.JCFID));
            }
            if (!string.IsNullOrEmpty(sw.JCFIDSTR))
            {
                sb.AppendFormat(" AND JCFID  in ({0})", ClsSql.SwitchStrToSqlIn(sw.JCFIDSTR));
            }
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
            {
                if (PublicCls.OrgIsShi(sw.BYORGNO))
                {
                    sb.AppendFormat(" and BYORGNO like '{0}%'", PublicCls.getShiIncOrgNo(sw.BYORGNO));
                }
                else if (PublicCls.OrgIsXian(sw.BYORGNO))
                {
                    sb.AppendFormat(" and BYORGNO like  '{0}%'", PublicCls.getXianIncOrgNo(sw.BYORGNO));
                }
                else
                {
                    sb.AppendFormat(" and BYORGNO = '{0}'", PublicCls.getZhenIncOrgNo(sw.BYORGNO));
                }
            }
            if (!string.IsNullOrEmpty(sw.BeginTime))
            {
                sb.AppendFormat(" AND FIRETIME>='{0} 00:00:00'", sw.BeginTime);
            }
            if (!string.IsNullOrEmpty(sw.EndTime))
            {
                sb.AppendFormat(" AND FIRETIME<='{0} 23:59:59'", sw.EndTime);
            }
            if (!string.IsNullOrEmpty(sw.FIREFROM))
            {
                sb.AppendFormat(" AND FIREFROM ={0}", ClsSql.EncodeSql(sw.FIREFROM));
            }
            if (!string.IsNullOrEmpty(sw.FIREFROMID))
            {
                sb.AppendFormat(" AND FIREFROMID ={0}", ClsSql.EncodeSql(sw.FIREFROMID));
            }
            if (!string.IsNullOrEmpty(sw.MANSTATE))
            {
                sb.AppendFormat(" AND MANSTATE ={0}", ClsSql.EncodeSql(sw.MANSTATE));
            }
            if (!string.IsNullOrEmpty(sw.MANSTATESTR))
            {
                sb.AppendFormat(" AND MANSTATE in ({0})", ClsSql.SwitchStrToSqlIn(sw.MANSTATESTR));
            }
            sb.AppendFormat(" order by JCFID desc ");
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        #endregion

        #region 档案统计查询热点类别获取数据
        /// <summary>
        /// 档案统计查询热点类别获取数据
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable GetDTHottype(JC_FIRE_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" Select JCFID,FIRENAME,BYORGNO,FIREFROM,FIREFROMID,FIRETIME,FIREENDTIME,ISOUTFIRE,MARK,JD,WD,ZQWZ,WXBH,DQRDBH,RSMJ,DL,YY,JXHQSJ,OWERJCFID,PFUSERID,PFORGNO,PFTIME,PFFLAG,RECEIVETIME,ISSUEDTIME,LASTPROCESSTIME,MANSTATE,(select top 1 HOTTYPE from JC_FIRETICKLING where JCFID=a.jcfid order by MANTIME desc) as HOTTYPE,(select top 1 FIRELEVEL from JC_FIRE_PROP where JCFID=a.jcfid order by JC_FIRE_PROPID desc) as FIRELEVEL  from JC_FIRE a  Where 1=1 ");
            sb.AppendFormat(" AND ISOUTFIRE ='1' AND (select top 1 HOTTYPE from JC_FIRETICKLING where JCFID=a.jcfid order by MANTIME desc)<>0 AND (select top 1 HOTTYPE from JC_FIRETICKLING where JCFID=a.jcfid order by MANTIME desc)is not null ");
            if (!string.IsNullOrEmpty(sw.JCFID))
            {
                sb.AppendFormat(" AND JCFID ={0}", ClsSql.EncodeSql(sw.JCFID));
            }
            if (!string.IsNullOrEmpty(sw.JCFIDSTR))
            {
                sb.AppendFormat(" AND JCFID  in ({0})", ClsSql.SwitchStrToSqlIn(sw.JCFIDSTR));
            }
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
            {
                if (PublicCls.OrgIsShi(sw.BYORGNO))
                {
                    sb.AppendFormat(" and BYORGNO like '{0}%'", PublicCls.getShiIncOrgNo(sw.BYORGNO));
                }
                else if (PublicCls.OrgIsXian(sw.BYORGNO))
                {
                    sb.AppendFormat(" and BYORGNO like  '{0}%'", PublicCls.getXianIncOrgNo(sw.BYORGNO));
                }
                else
                {
                    sb.AppendFormat(" and BYORGNO = '{0}'", PublicCls.getZhenIncOrgNo(sw.BYORGNO));
                }
            }
            if (!string.IsNullOrEmpty(sw.BeginTime))
            {
                sb.AppendFormat(" AND FIRETIME>='{0} 00:00:00'", sw.BeginTime);
            }
            if (!string.IsNullOrEmpty(sw.EndTime))
            {
                sb.AppendFormat(" AND FIRETIME<='{0} 23:59:59'", sw.EndTime);
            }
            if (!string.IsNullOrEmpty(sw.FIREFROM))
            {
                sb.AppendFormat(" AND FIREFROM ={0}", ClsSql.EncodeSql(sw.FIREFROM));
            }
            if (!string.IsNullOrEmpty(sw.FIREFROMID))
            {
                sb.AppendFormat(" AND FIREFROMID ={0}", ClsSql.EncodeSql(sw.FIREFROMID));
            }
            if (!string.IsNullOrEmpty(sw.MANSTATE))
            {
                sb.AppendFormat(" AND MANSTATE ={0}", ClsSql.EncodeSql(sw.MANSTATE));
            }
            if (!string.IsNullOrEmpty(sw.MANSTATESTR))
            {
                sb.AppendFormat(" AND MANSTATE in ({0})", ClsSql.SwitchStrToSqlIn(sw.MANSTATESTR));
            }
            sb.AppendFormat(" order by JCFID desc ");
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        #endregion

        #region 获取火情分页数据
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static DataTable getDT(JC_FIRE_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("  from   JC_FIRE  ");
            sb.AppendFormat("where 1=1");
            sb.AppendFormat(" AND (MANSTATE != 19 and MANSTATE != 18 ) AND (ISOUTFIRE !=1  or ISOUTFIRE is null) ");//排除已经上报结束的火情
            sb.AppendFormat(" AND FIREFROM ={0} ", sw.FIREFROM);
            if (!string.IsNullOrEmpty(sw.TopORGNO))
            {
                sb.AppendFormat(" AND BYORGNO  like '{0}%' ", sw.TopORGNO);
            }
            string sql = ("select * ") + sb.ToString() + (" order by JCFID desc ");
            string sqlC = "select count(1) " + sb.ToString();
            total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
            sw.curPage = PagerCls.getCurPage(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
            DataSet ds = DataBaseClass.FullDataSet(sql, (sw.curPage - 1) * sw.pageSize, sw.pageSize, "a");
            return ds.Tables[0];
        }
        #endregion

        #region 统计当前用户下火情的数量
        /// <summary>
        /// 统计当前用户下火情的数量
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getNum(JC_FIRE_SW sw)
        {
            string total = "";
            StringBuilder sb = new StringBuilder();
            sb.Append(" Left Join JC_FIRETICKLING b   on a.JCFID=b.JCFID ");
            sb.Append(" Where  (b.FKID=(select max(fkid) from JC_FIRETICKLING where jcfid=b.jcfid) or b.fkid is null) and a.ISOUTFIRE=1 ");
            if (sw.BYORGNO.Substring(4, 5) == "00000")//获取所有市的
                sb.AppendFormat(" AND (SUBSTRING(a.BYORGNO,1,4) = '{0}' )", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
            else if (sw.BYORGNO.Substring(6, 3) == "000")//获取所有县的
                sb.AppendFormat(" AND (SUBSTRING(a.BYORGNO,1,6) = '{0}' )", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
            else
                sb.AppendFormat(" AND a.BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            string sqlC = " Select count(*) from JC_FIRE a" + sb.ToString();
            total = DataBaseClass.ReturnSqlField(sqlC);
            return total;
        }
        #endregion


        #region 获取数据中心档案(新)
        /// <summary>
        /// 获取数据中心档案
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static DataTable GetDCDT(JC_FIRE_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Left Join JC_FIRETICKLING b   on a.JCFID=b.JCFID ");
            sb.Append(" Where  (b.FKID=(select max(fkid) from JC_FIRETICKLING where jcfid=b.jcfid) or b.fkid is null) ");
            if (!string.IsNullOrEmpty(sw.BYORGNO))
            {
                if (sw.BYORGNO.Substring(4, 5) == "00000")//获取所有市的
                    sb.AppendFormat(" AND (SUBSTRING(a.BYORGNO,1,4) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
                else if (sw.BYORGNO.Substring(4, 5) == "xxxxx")//单独市
                    sb.AppendFormat(" AND (SUBSTRING(a.BYORGNO,1,9) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4) + "00000"));
                else if (sw.BYORGNO.Substring(6, 3) == "xxx")//获取所有县的
                    sb.AppendFormat(" AND (SUBSTRING(a.BYORGNO,1,6) = '{0}' )", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
                else
                    sb.AppendFormat(" AND a.BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            }
            if (!string.IsNullOrEmpty(sw.FIRENAME))
            {
                sb.AppendFormat(" AND a.FIRENAME like '%{0}%'", ClsSql.EncodeSql(sw.FIRENAME));
            }
            if (!string.IsNullOrEmpty(sw.FIREFROM))
            {
                sb.AppendFormat(" AND a.FIREFROM ='{0}'", ClsSql.EncodeSql(sw.FIREFROM));
            }
            if (!string.IsNullOrEmpty(sw.FIREFROMID))
            {
                sb.AppendFormat(" AND a.FIREFROMID ={0}", ClsSql.EncodeSql(sw.FIREFROMID));
            }
            if (!string.IsNullOrEmpty(sw.ISOUTFIRE))
            {
                sb.AppendFormat(" AND a.ISOUTFIRE ={0}", ClsSql.EncodeSql(sw.ISOUTFIRE));
            }
            if (!string.IsNullOrEmpty(sw.MANSTATE))
            {
                sb.AppendFormat(" AND a.MANSTATE in ({0})", ClsSql.EncodeSql(sw.MANSTATE));
            }

            if (!string.IsNullOrEmpty(sw.BeginTime) && !string.IsNullOrEmpty(sw.EndTime))
            {
                sb.AppendFormat(" AND a.RECEIVETIME >='{0} 00:00:00'", sw.BeginTime);
                sb.AppendFormat(" AND a.RECEIVETIME <='{0} 23:59:59'", sw.EndTime);
            }
            if (!string.IsNullOrEmpty(sw.HOTTYPE))
            {
                sb.AppendFormat(" AND b.HOTTYPE ={0}", ClsSql.EncodeSql(sw.HOTTYPE));
            }
            if (!string.IsNullOrEmpty(sw.FIRETIME))
            {
                sb.AppendFormat(" AND a.FIRETIME>='{0}'", sw.FIRETIME);
                //if (!string.IsNullOrEmpty(sw.FIRETIME))
                sb.AppendFormat(" AND a.FIRETIME<='{0}'", sw.FIREENDTIME);
            }

            string sql = " Select * from JC_FIRE a " + sb.ToString() + " order by a.JCFID desc ";
            string sqlC = " Select count(*) from JC_FIRE a" + sb.ToString();
            total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
            //(sw.PageIndex - 1) * sw.PageRow, sw.PageRow


            sw.curPage = PagerCls.getCurPage(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
            DataSet ds = DataBaseClass.FullDataSet(sql, (sw.curPage - 1) * sw.pageSize, sw.pageSize, "a");

            return ds.Tables[0];

        }

        #endregion

        #region 获取BYORGNO
        /// <summary>
        /// 获取BYORGNO
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns></returns>
        public static DataTable getBYORGNO(JC_FIRE_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT * FROM   JC_FIRE WHERE   1=1");
            if (!string.IsNullOrEmpty(sw.JCFID))
                sb.AppendFormat(" AND JCFID = '{0}'", sw.JCFID);
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        #endregion

    }
}
