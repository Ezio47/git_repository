using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel;
using DataBaseClassLibrary;
using PublicClassLibrary;

namespace ManagerSystemClassLibrary.BaseDT
{
    /// <summary>
    /// 火情档案管理
    /// </summary>
    public class FIRERECORD_FIREINFO
    {
        #region 添加
        /// <summary>
        /// 添加火情档案管理
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(FIRERECORD_FIREINFO_Model m)
        {
            #region 添加数据至JC_FIRE表中
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT  INTO  JC_FIRE(BYORGNO,FIREFROM,FIRETIME,FIREENDTIME,JD,WD,ZQWZ,ISOUTFIRE) output inserted.JCFID ");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat(" {0}", ClsSql.saveNullField(m.FIREADDRESSTOWNS));
            sb.AppendFormat(",'50'");
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRETIME));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIREENDTIME));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.JD));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.WD));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIREADDRESS));
            sb.AppendFormat(",'1'");
            sb.AppendFormat(")");
            string str = DataBaseClass.ReturnSqlField(sb.ToString());
            #endregion

            if (str != "")
            {
                List<string> sqllist = new List<string>();

                #region 添加数据至FIRERECORD_FIREINFO表中
                StringBuilder sb1 = new StringBuilder();
                sb1.AppendFormat("INSERT  INTO  FIRERECORD_FIREINFO(JCFID, BYORGNO,FIRECODE, FIREADDRESSCOUNTY, FIREADDRESSTOWNS, FIREADDRESSVILLAGES, FIREADDRESS,FIRETIME,FIREENDTIME,");
                sb1.AppendFormat("FIRERECINFO000,FIRERECINFO001, FIRERECINFO020,FIRERECINFO021,FIRERECINFO030,FIRERECINFO031,FIRERECINFO032,FIRERECINFO040,FIRERECINFO041,");
                sb1.AppendFormat("FIRERECINFO050,FIRERECINFO051,FIRERECINFO060,FIRERECINFO061,FIRERECINFO070,FIRERECINFO071,FIRERECINFO072,FIRERECINFO080,FIRERECINFO081,FIRERECINFO082,");
                sb1.AppendFormat("FIRERECINFO090,FIRERECINFO100,FIRERECINFO110,FIRERECINFO111,FIRERECINFO120,FIRERECINFO130,FIRERECINFO140,FIRERECINFO150,FIRERECINFO160,FIRELOSEAREA)");
                sb1.AppendFormat("VALUES(");
                sb1.AppendFormat("'{0}'", ClsSql.EncodeSql(str));
                sb1.AppendFormat(",{0}", ClsSql.saveNullField(m.BYORGNO));
                sb1.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRECODE));
                sb1.AppendFormat(",{0}", ClsSql.saveNullField(m.FIREADDRESSCOUNTY));
                sb1.AppendFormat(",{0}", ClsSql.saveNullField(m.FIREADDRESSTOWNS));
                sb1.AppendFormat(",{0}", ClsSql.saveNullField(m.FIREADDRESSVILLAGES));
                sb1.AppendFormat(",{0}", ClsSql.saveNullField(m.FIREADDRESS));
                sb1.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRETIME));
                sb1.AppendFormat(",{0}", ClsSql.saveNullField(m.FIREENDTIME));
                sb1.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO000));
                sb1.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO001));
                sb1.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO020));
                sb1.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO021));
                sb1.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO030));
                sb1.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO031));
                sb1.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO032));
                sb1.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO040));
                sb1.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO041));
                sb1.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO050));
                sb1.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO051));
                sb1.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO060));
                sb1.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO061));
                sb1.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO070));
                sb1.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO071));
                sb1.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO072));
                sb1.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO080));
                sb1.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO081));
                sb1.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO082));
                sb1.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO090));
                sb1.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO100));
                sb1.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO110));
                sb1.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO111));
                sb1.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO120));
                sb1.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO130));
                sb1.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO140));
                sb1.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO150));
                sb1.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO160));
                sb1.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRELOSEAREA));
                sb1.AppendFormat(")");
                sqllist.Add(sb1.ToString());
                #endregion;

                #region 添加火灾等级到JC_FIRE_PROP中
                StringBuilder sb2 = new StringBuilder();
                sb2.AppendFormat("INSERT  INTO  JC_FIRE_PROP(JCFID,FIRELEVEL) ");
                sb2.AppendFormat("VALUES(");
                sb2.AppendFormat("'{0}'", ClsSql.EncodeSql(str));
                sb2.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO001));
                sb2.AppendFormat(")");
                sqllist.Add(sb2.ToString());
                #endregion

                #region 添加数据至空间库HUOQINGDANGAN
                StringBuilder sb3 = new StringBuilder();
                sb3.AppendFormat("INSERT  INTO  HUOQINGDANGAN(OBJECTID,NAME,JD,WD,ADDRESS,YEAR,Shape) ");
                sb3.AppendFormat("VALUES(");
                sb3.AppendFormat("'{0}'", ClsSql.EncodeSql(str));
                sb3.AppendFormat(",{0}", ClsSql.saveNullField(m.FIREADDRESS));
                sb3.AppendFormat(",{0}", ClsSql.saveNullField(m.JD));
                sb3.AppendFormat(",{0}", ClsSql.saveNullField(m.WD));
                sb3.AppendFormat(",{0}", ClsSql.saveNullField(m.FIREADDRESS));
                string[] sTIME = m.FIRETIME.Split('-');
                sb3.AppendFormat(",{0}", ClsSql.saveNullField(sTIME[0]));
                sb3.AppendFormat(",{0}", m.Shape);
                sb3.AppendFormat(")");
                #endregion

                var y = DataBaseClass.ExecuteSqlTran(sqllist);
                if (y > 0)
                {
                    SDEDataBaseClass.ExeSql(sb3.ToString());
                    return new Message(true, "添加成功!", m.returnUrl);
                }
                else
                    return new Message(false, "添加失败,事物回滚机制!", "");
            }
            else
                return new Message(false, "添加失败,请检查各输入框是否正确!", m.returnUrl);
        }

        #endregion

        #region 修改
        /// <summary>
        /// 修改火情档案
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Mdy(FIRERECORD_FIREINFO_Model m)
        {
            List<string> sqllist = new List<string>();

            #region 更新FIRERECORD_FIREINFO表数据
            StringBuilder sb = new StringBuilder();
            if (isExists(new FIRERECORD_FIREINFO_SW { JCFID = m.JCFID }))
            {
                sb.AppendFormat(" Update FIRERECORD_FIREINFO SET ");
                sb.AppendFormat(" BYORGNO='{0}'", ClsSql.EncodeSql(m.BYORGNO));
                sb.AppendFormat(",FIRECODE={0}", ClsSql.saveNullField(m.FIRECODE));
                sb.AppendFormat(",FIREADDRESSCOUNTY={0}", ClsSql.saveNullField(m.FIREADDRESSCOUNTY));
                sb.AppendFormat(",FIREADDRESSTOWNS={0}", ClsSql.saveNullField(m.FIREADDRESSTOWNS));
                sb.AppendFormat(",FIREADDRESSVILLAGES={0}", ClsSql.saveNullField(m.FIREADDRESSVILLAGES));
                sb.AppendFormat(",FIREADDRESS={0}", ClsSql.saveNullField(m.FIREADDRESS));
                sb.AppendFormat(",FIRETIME={0}", ClsSql.saveNullField(m.FIRETIME));
                sb.AppendFormat(",FIREENDTIME={0}", ClsSql.saveNullField(m.FIREENDTIME));
                sb.AppendFormat(",FIRERECINFO000={0}", ClsSql.saveNullField(m.FIRERECINFO000));
                sb.AppendFormat(",FIRERECINFO001={0}", ClsSql.saveNullField(m.FIRERECINFO001));
                sb.AppendFormat(",FIRERECINFO020={0}", ClsSql.saveNullField(m.FIRERECINFO020));
                sb.AppendFormat(",FIRERECINFO021={0}", ClsSql.saveNullField(m.FIRERECINFO021));
                sb.AppendFormat(",FIRERECINFO030={0}", ClsSql.saveNullField(m.FIRERECINFO030));
                sb.AppendFormat(",FIRERECINFO031={0}", ClsSql.saveNullField(m.FIRERECINFO031));
                sb.AppendFormat(",FIRERECINFO032={0}", ClsSql.saveNullField(m.FIRERECINFO032));
                sb.AppendFormat(",FIRERECINFO040={0}", ClsSql.saveNullField(m.FIRERECINFO040));
                sb.AppendFormat(",FIRERECINFO041={0}", ClsSql.saveNullField(m.FIRERECINFO041));
                sb.AppendFormat(",FIRERECINFO050={0}", ClsSql.saveNullField(m.FIRERECINFO050));
                sb.AppendFormat(",FIRERECINFO051={0}", ClsSql.saveNullField(m.FIRERECINFO051));
                sb.AppendFormat(",FIRERECINFO060={0}", ClsSql.saveNullField(m.FIRERECINFO060));
                sb.AppendFormat(",FIRERECINFO061={0}", ClsSql.saveNullField(m.FIRERECINFO061));
                sb.AppendFormat(",FIRERECINFO070={0}", ClsSql.saveNullField(m.FIRERECINFO070));
                sb.AppendFormat(",FIRERECINFO071={0}", ClsSql.saveNullField(m.FIRERECINFO071));
                sb.AppendFormat(",FIRERECINFO072={0}", ClsSql.saveNullField(m.FIRERECINFO072));
                sb.AppendFormat(",FIRERECINFO080={0}", ClsSql.saveNullField(m.FIRERECINFO080));
                sb.AppendFormat(",FIRERECINFO081={0}", ClsSql.saveNullField(m.FIRERECINFO081));
                sb.AppendFormat(",FIRERECINFO082={0}", ClsSql.saveNullField(m.FIRERECINFO082));
                sb.AppendFormat(",FIRERECINFO090={0}", ClsSql.saveNullField(m.FIRERECINFO090));
                sb.AppendFormat(",FIRERECINFO100={0}", ClsSql.saveNullField(m.FIRERECINFO100));
                sb.AppendFormat(",FIRERECINFO110={0}", ClsSql.saveNullField(m.FIRERECINFO110));
                sb.AppendFormat(",FIRERECINFO111={0}", ClsSql.saveNullField(m.FIRERECINFO111));
                sb.AppendFormat(",FIRERECINFO120={0}", ClsSql.saveNullField(m.FIRERECINFO120));
                sb.AppendFormat(",FIRERECINFO130={0}", ClsSql.saveNullField(m.FIRERECINFO130));
                sb.AppendFormat(",FIRERECINFO140={0}", ClsSql.saveNullField(m.FIRERECINFO140));
                sb.AppendFormat(",FIRERECINFO150={0}", ClsSql.saveNullField(m.FIRERECINFO150));
                sb.AppendFormat(",FIRERECINFO160={0}", ClsSql.saveNullField(m.FIRERECINFO160));
                sb.AppendFormat(",FIRELOSEAREA={0}", ClsSql.saveNullField(m.FIRELOSEAREA));
                sb.AppendFormat(" where JCFID= '{0}'", ClsSql.EncodeSql(m.JCFID));
                sqllist.Add(sb.ToString());
            }
            else
            {
                sb.AppendFormat("INSERT  INTO  FIRERECORD_FIREINFO(JCFID, BYORGNO,FIRECODE, FIREADDRESSCOUNTY, FIREADDRESSTOWNS, FIREADDRESSVILLAGES, FIREADDRESS,FIRETIME,FIREENDTIME,");
                sb.AppendFormat("FIRERECINFO000,FIRERECINFO001, FIRERECINFO020,FIRERECINFO021,FIRERECINFO030,FIRERECINFO031,FIRERECINFO032,FIRERECINFO040,FIRERECINFO041,");
                sb.AppendFormat("FIRERECINFO050,FIRERECINFO051,FIRERECINFO060,FIRERECINFO061,FIRERECINFO070,FIRERECINFO071,FIRERECINFO072,FIRERECINFO080,FIRERECINFO081,FIRERECINFO082,");
                sb.AppendFormat("FIRERECINFO090,FIRERECINFO100,FIRERECINFO110,FIRERECINFO111,FIRERECINFO120,FIRERECINFO130,FIRERECINFO140,FIRERECINFO150,FIRERECINFO160,FIRELOSEAREA)");
                sb.AppendFormat("VALUES(");
                sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.JCFID));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.BYORGNO));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRECODE));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIREADDRESSCOUNTY));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIREADDRESSTOWNS));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIREADDRESSVILLAGES));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIREADDRESS));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRETIME));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIREENDTIME));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO000));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO001));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO020));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO021));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO030));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO031));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO032));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO040));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO041));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO050));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO051));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO060));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO061));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO070));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO071));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO072));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO080));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO081));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO082));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO090));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO100));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO110));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO111));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO120));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO130));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO140));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO150));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO160));
                sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRELOSEAREA));
                sb.AppendFormat(")");
                sqllist.Add(sb.ToString());
            }

            #endregion

            #region 修改JC_FIRE表中的数据
            StringBuilder sc = new StringBuilder();
            sc.AppendFormat(" Update JC_FIRE SET ");
            sc.AppendFormat(" BYORGNO='{0}'", ClsSql.EncodeSql(m.FIREADDRESSTOWNS));
            sc.AppendFormat(",FIRETIME={0}", ClsSql.saveNullField(m.FIRETIME));
            sc.AppendFormat(",FIREENDTIME={0}", ClsSql.saveNullField(m.FIREENDTIME));
            sc.AppendFormat(",ZQWZ={0}", ClsSql.saveNullField(m.FIREADDRESS));
            sc.AppendFormat(",JD={0}", ClsSql.saveNullField(m.JD));
            sc.AppendFormat(",WD={0}", ClsSql.saveNullField(m.WD));
            sc.AppendFormat(" where JCFID= '{0}'", ClsSql.EncodeSql(m.JCFID));
            sqllist.Add(sc.ToString());
            #endregion

            #region 修改火灾等级到JC_FIRE_PROP中
            StringBuilder sd = new StringBuilder();
            if (isExistsfirelevel(new JC_FIRE_PROP_SW { JCFID = m.JCFID }))
            {
                sd.AppendFormat(" Update  JC_FIRE_PROP SET ");
                sd.AppendFormat(" FIRELEVEL={0}", ClsSql.saveNullField(m.FIRERECINFO001));
                sd.AppendFormat(" where JCFID= '{0}'", ClsSql.EncodeSql(m.JCFID));
                sqllist.Add(sd.ToString());
            }
            else
            {
                sd.AppendFormat(" INSERT  INTO  JC_FIRE_PROP(JCFID,FIRELEVEL) ");
                sd.AppendFormat(" VALUES(");
                sd.AppendFormat("'{0}'", ClsSql.EncodeSql(m.JCFID));
                sd.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO001));
                sd.AppendFormat(")");
                sqllist.Add(sd.ToString());
            }
            #endregion

            #region 修改空间库HUOQINGDANGAN的数据
            StringBuilder se = new StringBuilder();
            se.AppendFormat("delete from HUOQINGDANGAN");
            se.AppendFormat(" where OBJECTID= '{0}'", ClsSql.EncodeSql(m.JCFID));
            se.AppendFormat(";");
            se.AppendFormat("INSERT  INTO  HUOQINGDANGAN(OBJECTID,NAME,JD,WD,ADDRESS,YEAR,Shape) ");
            se.AppendFormat("VALUES(");
            se.AppendFormat(" {0}", ClsSql.saveNullField(m.JCFID));
            se.AppendFormat(",{0}", ClsSql.saveNullField(m.FIREADDRESS));
            se.AppendFormat(",{0}", ClsSql.saveNullField(m.JD));
            se.AppendFormat(",{0}", ClsSql.saveNullField(m.WD));
            se.AppendFormat(",{0}", ClsSql.saveNullField(m.FIREADDRESS));
            string[] sTIME = m.FIRETIME.Split('-');
            se.AppendFormat(",{0}", ClsSql.saveNullField(sTIME[0]));
            se.AppendFormat(",{0}", m.Shape);
            se.AppendFormat(")");
            #endregion

            var y = DataBaseClass.ExecuteSqlTran(sqllist);
            if (y > 0)
            {
                SDEDataBaseClass.ExeSql(se.ToString());
                return new Message(true, "修改成功!", m.returnUrl);
            }
            else
                return new Message(false, "修改失败,请检查各输入框是否正确!", m.returnUrl);
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(FIRERECORD_FIREINFO_Model m)
        {
            List<string> sqllist = new List<string>();
            //删除FIRERECORD_FIREINFO表中的数据
            StringBuilder sb1 = new StringBuilder();
            sb1.AppendFormat("delete from FIRERECORD_FIREINFO ");
            sb1.AppendFormat(" where JCFID = '{0}'", ClsSql.EncodeSql(m.JCFID));
            sqllist.Add(sb1.ToString());

            //删除JC_FIRE表中的数据
            StringBuilder sb2 = new StringBuilder();
            sb2.AppendFormat("delete from JC_FIRE ");
            sb2.AppendFormat(" where JCFID ='{0}'", ClsSql.EncodeSql(m.JCFID));
            sqllist.Add(sb2.ToString());

            //删除JC_FIRE_PROP表中的数据
            StringBuilder sb3 = new StringBuilder();
            sb3.AppendFormat("delete from JC_FIRE_PROP ");
            sb3.AppendFormat(" where JCFID = '{0}'", ClsSql.EncodeSql(m.JCFID));
            sqllist.Add(sb3.ToString());

            //删除空间库HUOQINGDANGAN的数据
            StringBuilder sc = new StringBuilder();
            sc.AppendFormat("delete from HUOQINGDANGAN ");
            sc.AppendFormat(" where OBJECTID = '{0}", ClsSql.EncodeSql(m.JCFID));

            var y = DataBaseClass.ExecuteSqlTran(sqllist);
            if (y > 0)
            {
                SDEDataBaseClass.ExeSql(sc.ToString());
                return new Message(true, "删除成功!", m.returnUrl);
            }
            else
                return new Message(false, "删除失败,事物回滚机制!", "");
        }

        #endregion

        #region 获取报表数据列表
        /// <summary>
        /// 获取报表数据列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns></returns>
        public static DataTable getDT(FIRERECORD_FIREINFO_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT *  FROM   FIRERECORD_FIREINFO WHERE   1=1");
            if (string.IsNullOrEmpty(sw.FRFIID) == false)
                sb.AppendFormat(" AND JCFID = '{0}'", sw.JCFID);
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
            {
                //if (sw.BYORGNO.Substring(4, 11) == "00000000000")
                //    sb.AppendFormat(" AND BYORGNO = '{0}'", sw.BYORGNO);
                //else if()
                //    sb.AppendFormat(" AND FIREADDRESSCOUNTY = '{0}'", sw.BYORGNO);
                 if (sw.BYORGNO.Substring(4, 11) == "00000000000")//获取所有市的
                    sb.AppendFormat(" AND SUBSTRING(FIREADDRESSTOWNS,1,4) = '{0}'", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
                else if (sw.BYORGNO.Substring(6, 9) == "000000000")//获取所有县的
                    sb.AppendFormat(" AND SUBSTRING(FIREADDRESSTOWNS,1,6) = '{0}'", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
                else if (sw.BYORGNO.Substring(9, 6) == "000000")//获取所有乡镇的
                    sb.AppendFormat(" AND SUBSTRING(FIREADDRESSTOWNS,1,9) = '{0}'", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 9)));
                else
                     sb.AppendFormat(" AND FIREADDRESSTOWNS = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            }
            if (!string.IsNullOrEmpty(sw.FIRETIME))
                sb.AppendFormat(" AND FIRETIME>='{0}'", sw.FIRETIME);
            if (!string.IsNullOrEmpty(sw.FIREENDTIME))
                sb.AppendFormat(" AND FIRETIME<='{0}'", sw.FIREENDTIME);
            sb.AppendFormat(" ORDER BY FRFIID");
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        #endregion

        #region 获取火情档案数据列表
        /// <summary>
        /// 获取火情档案数据列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns></returns>
        public static DataTable getDT2(FIRERECORD_FIREINFO_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT *  FROM   FIRERECORD_FIREINFO WHERE  1=1");
            if (!string.IsNullOrEmpty(sw.FRFIID))
                sb.AppendFormat(" AND FRFIID = '{0}'", sw.FRFIID);
            if (!string.IsNullOrEmpty(sw.JCFID))
                sb.AppendFormat(" AND JCFID = '{0}'", sw.JCFID);
            sb.AppendFormat(" ORDER BY FRFIID");
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 分页获取火情档案数据列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static DataTable getDT(FIRERECORD_FIREINFO_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM  FIRERECORD_FIREINFO WHERE  1=1");
            if (!string.IsNullOrEmpty(sw.FIREADDRESSTOWNS))
            {
                if (sw.FIREADDRESSTOWNS.Substring(4, 11) == "00000000000")//获取所有市
                    sb.AppendFormat(" AND (SUBSTRING(FIREADDRESSTOWNS,1,4) = '{0}')", ClsSql.EncodeSql(sw.FIREADDRESSTOWNS.Substring(0, 4)));
                else if (sw.FIREADDRESSTOWNS.Substring(4, 11) == "xxxxxxxxxxx")//单独市
                    sb.AppendFormat(" AND (SUBSTRING(FIREADDRESSTOWNS,1,15) = '{0}')", ClsSql.EncodeSql(sw.FIREADDRESSTOWNS.Substring(0, 4) + "00000000000"));
                else if (sw.FIREADDRESSTOWNS.Substring(6, 9) == "xxxxxxxxx")//获取所有县的
                    sb.AppendFormat(" AND (SUBSTRING(FIREADDRESSTOWNS,1,6) = '{0}')", ClsSql.EncodeSql(sw.FIREADDRESSTOWNS.Substring(0, 6)));
                else
                    sb.AppendFormat(" AND FIREADDRESSTOWNS = '{0}'", ClsSql.EncodeSql(sw.FIREADDRESSTOWNS));
            }
            if (!string.IsNullOrEmpty(sw.FIRETIME))
                sb.AppendFormat(" AND FIRETIME>='{0}'", sw.FIRETIME);
            if (!string.IsNullOrEmpty(sw.FIREENDTIME))
                sb.AppendFormat(" AND FIREENDTIME<='{0}'", sw.FIREENDTIME);
            string sql = "SELECT * " + sb.ToString() + " ORDER BY FRFIID ";
            string sqlC = "select count(1) " + sb.ToString();
            total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
            sw.CurPage = PagerCls.getCurPage(new PagerSW { curPage = sw.CurPage, pageSize = sw.PageSize, rowCount = total });
            DataSet ds = DataBaseClass.FullDataSet(sql, (sw.CurPage - 1) * sw.PageSize, sw.PageSize, "a");
            return ds.Tables[0];
        }
        #endregion

        #region 判断记录是否存在
        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>true存在 false不存在 </returns>
        public static bool isExists(FIRERECORD_FIREINFO_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from FIRERECORD_FIREINFO where 1=1");
            if (string.IsNullOrEmpty(sw.JCFID) == false)
                sb.AppendFormat(" and JCFID='{0}'", ClsSql.EncodeSql(sw.JCFID));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion

        #region 判断火灾等级记录是否存在
        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>true存在 false不存在 </returns>
        public static bool isExistsfirelevel(JC_FIRE_PROP_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from JC_FIRE_PROP where 1=1");
            if (string.IsNullOrEmpty(sw.JCFID) == false)
                sb.AppendFormat(" and JCFID='{0}'", ClsSql.EncodeSql(sw.JCFID));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion

        #region 获取FRFIID
        /// <summary>
        /// 获取FRFIID
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns></returns>
        public static DataTable getFRFIID(FIRERECORD_FIREINFO_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT FRFIID, JCFID  FROM  FIRERECORD_FIREINFO WHERE   1=1");
            if (!string.IsNullOrEmpty(sw.JCFID))
                sb.AppendFormat(" AND JCFID = '{0}'", sw.JCFID);
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        #endregion
    }
}
