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
            sb.AppendFormat("{0}", ClsSql.saveNullField(m.FIREADDRESSTOWNS));
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

            #region 添加数据至FIRERECORD_FIREINFO表中
            StringBuilder sc = new StringBuilder();
            sc.AppendFormat("INSERT  INTO  FIRERECORD_FIREINFO(JCFID, BYORGNO,FIRECODE, FIREADDRESSCOUNTY, FIREADDRESSTOWNS, FIREADDRESSVILLAGES, FIREADDRESS,FIRETIME,FIREENDTIME,");
            sc.AppendFormat("FIRERECINFO000,FIRERECINFO001, FIRERECINFO020,FIRERECINFO021,FIRERECINFO030,FIRERECINFO031,FIRERECINFO032,FIRERECINFO040,FIRERECINFO041,");
            sc.AppendFormat("FIRERECINFO050,FIRERECINFO051,FIRERECINFO060,FIRERECINFO061,FIRERECINFO070,FIRERECINFO071,FIRERECINFO072,FIRERECINFO080,FIRERECINFO081,FIRERECINFO082,");
            sc.AppendFormat("FIRERECINFO090,FIRERECINFO100,FIRERECINFO110,FIRERECINFO111,FIRERECINFO120,FIRERECINFO130,FIRERECINFO140,FIRERECINFO150,FIRERECINFO160,FIRELOSEAREA)");
            sc.AppendFormat("VALUES(");
            sc.AppendFormat("'{0}'", ClsSql.EncodeSql(str));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.BYORGNO));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRECODE));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.FIREADDRESSCOUNTY));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.FIREADDRESSTOWNS));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.FIREADDRESSVILLAGES));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.FIREADDRESS));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRETIME));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.FIREENDTIME));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO000));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO001));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO020));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO021));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO030));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO031));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO032));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO040));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO041));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO050));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO051));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO060));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO061));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO070));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO071));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO072));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO080));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO081));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO082));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO090));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO100));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO110));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO111));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO120));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO130));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO140));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO150));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO160));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRELOSEAREA));
            sc.AppendFormat(")");

            //添加火灾等级到JC_FIRE_PROP中
            sc.AppendFormat("INSERT  INTO  JC_FIRE_PROP(JCFID,FIRELEVEL) ");
            sc.AppendFormat("VALUES(");
            sc.AppendFormat("'{0}'", ClsSql.EncodeSql(str));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.FIRERECINFO001));
            sc.AppendFormat(")");

            //添加数据至空间库HUOQINGDANGAN
            StringBuilder sd = new StringBuilder();
            sd.AppendFormat("INSERT  INTO  HUOQINGDANGAN(OBJECTID,NAME,JD,WD,ADDRESS,YEAR,Shape) ");
            sd.AppendFormat("VALUES(");
            sd.AppendFormat("'{0}'", ClsSql.EncodeSql(str));
            sd.AppendFormat(",{0}", ClsSql.saveNullField(m.FIREADDRESS));
            sd.AppendFormat(",{0}", ClsSql.saveNullField(m.JD));
            sd.AppendFormat(",{0}", ClsSql.saveNullField(m.WD));
            sd.AppendFormat(",{0}", ClsSql.saveNullField(m.FIREADDRESS));
            string[] sTIME = m.FIRETIME.Split('-');
            sd.AppendFormat(",{0}", ClsSql.saveNullField(sTIME[0]));
            sd.AppendFormat(",{0}", m.Shape);
            sd.AppendFormat(")");
            bool bl = SDEDataBaseClass.ExeSql(sd.ToString());
            bool bln = DataBaseClass.ExeSql(sc.ToString());
            #endregion;
            if (bln == true && bl == true)
                return new Message(true, "添加成功!", m.returnUrl);
            else
                return new Message(false, "添加失败，请检查各输入框是否正确!", m.returnUrl);
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
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Update FIRERECORD_FIREINFO");
            sb.AppendFormat(" set ");
            // sb.AppendFormat("JCFID='{0}'", ClsSql.EncodeSql(m.JCFID));
            sb.AppendFormat("BYORGNO='{0}'", ClsSql.EncodeSql(m.BYORGNO));
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
            //修改JC_FIRE表中的数据
            sb.AppendFormat(";");
            sb.AppendFormat("Update JC_FIRE");
            sb.AppendFormat(" set ");
            sb.AppendFormat("BYORGNO='{0}'", ClsSql.EncodeSql(m.FIREADDRESSTOWNS));
            sb.AppendFormat(",FIRETIME={0}", ClsSql.saveNullField(m.FIRETIME));
            sb.AppendFormat(",FIREENDTIME={0}", ClsSql.saveNullField(m.FIREENDTIME));
            sb.AppendFormat(",ZQWZ={0}", ClsSql.saveNullField(m.FIREADDRESS));
            sb.AppendFormat(",JD={0}", ClsSql.saveNullField(m.JD));
            sb.AppendFormat(",WD={0}", ClsSql.saveNullField(m.WD));
            sb.AppendFormat(" where JCFID= '{0}'", ClsSql.EncodeSql(m.JCFID));
            //修改火灾等级到JC_FIRE_PROP中
            sb.AppendFormat("Update  JC_FIRE_PROP ");
            sb.AppendFormat(" set ");
            sb.AppendFormat("FIRELEVEL={0}", ClsSql.saveNullField(m.FIRERECINFO001));
            sb.AppendFormat(" where JCFID= '{0}'", ClsSql.EncodeSql(m.JCFID));

            //修改空间库HUOQINGDANGAN的数据
            StringBuilder sc = new StringBuilder();
            sc.AppendFormat(";");
            sc.AppendFormat("delete from HUOQINGDANGAN");
            sc.AppendFormat(" where OBJECTID= '{0}'", ClsSql.EncodeSql(m.JCFID));
            sc.AppendFormat(";");
            sc.AppendFormat("INSERT  INTO  HUOQINGDANGAN(OBJECTID,NAME,JD,WD,ADDRESS,YEAR,Shape) ");
            sc.AppendFormat("VALUES(");
            sc.AppendFormat("{0}", ClsSql.saveNullField(m.JCFID));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.FIREADDRESS));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.JD));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.WD));
            sc.AppendFormat(",{0}", ClsSql.saveNullField(m.FIREADDRESS));
            string[] sTIME = m.FIRETIME.Split('-');
            sc.AppendFormat(",{0}", ClsSql.saveNullField(sTIME[0]));
            sc.AppendFormat(",{0}", m.Shape);
            sc.AppendFormat(")");
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            bool bl = SDEDataBaseClass.ExeSql(sc.ToString());
            if (bln == true && bl == true)
                return new Message(true, "修改成功!", m.returnUrl);
            else
                return new Message(false, "修改失败，请检查各输入框是否正确!", m.returnUrl);
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
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete from FIRERECORD_FIREINFO");
            sb.AppendFormat(" where JCFID= '{0}'", ClsSql.EncodeSql(m.JCFID));
            //  删除JC_FIRE表中的数据
            sb.AppendFormat("delete from JC_FIRE");
            sb.AppendFormat(" where JCFID  =");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.JCFID));
            //删除JC_FIRE_PROP表中的数据
            sb.AppendFormat("delete from JC_FIRE_PROP");
            sb.AppendFormat(" where JCFID  =");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.JCFID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());

            //删除空间库HUOQINGDANGAN的数据
            StringBuilder sc = new StringBuilder();
            sc.AppendFormat("delete from HUOQINGDANGAN");
            sc.AppendFormat(" where OBJECTID  =");
            sc.AppendFormat("'{0}'", ClsSql.EncodeSql(m.JCFID));
            bool bl = SDEDataBaseClass.ExeSql(sc.ToString());
            if (bln == true && bl == true)
                return new Message(true, "删除成功!", "");
            else
                return new Message(false, "删除失败，请检查各输入框是否正确!", "");
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
                if (sw.BYORGNO == "532500000000000")
                {
                    sb.AppendFormat(" AND BYORGNO = '{0}'", sw.BYORGNO);
                }
                else
                {
                    sb.AppendFormat(" AND FIREADDRESSCOUNTY = '{0}'", sw.BYORGNO);
                }
            }
            if (!string.IsNullOrEmpty(sw.FIRETIME))
                sb.AppendFormat(" AND FIRETIME>='{0}'", sw.FIRETIME);
            if (!string.IsNullOrEmpty(sw.FIREENDTIME))
                sb.AppendFormat(" AND FIREENDTIME<='{0}'", sw.FIREENDTIME);
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
                if (sw.FIREADDRESSTOWNS.Substring(4, 11) == "00000000000")//获取所有市的
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
            if (string.IsNullOrEmpty(sw.FRFIID) == false)
                sb.AppendFormat(" and FRFIID='{0}'", ClsSql.EncodeSql(sw.FRFIID));
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
            sb.AppendFormat("SELECT * FROM   FIRERECORD_FIREINFO WHERE   1=1");
            if (!string.IsNullOrEmpty(sw.JCFID))
                sb.AppendFormat(" AND JCFID = '{0}'", sw.JCFID);
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        #endregion
    }
}
