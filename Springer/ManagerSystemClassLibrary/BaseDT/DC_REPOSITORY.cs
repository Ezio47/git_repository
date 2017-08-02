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
    /// 数据中心_仓库表
    /// </summary>
     public class DC_REPOSITORY
     {
         #region 获取数据
         /// <summary>
         /// 获取数据
         /// </summary>
         /// <param name="sw"></param>
         /// <returns></returns>
         public static DataTable getDT (DC_REPOSITORY_SW sw)
         {
             StringBuilder sb = new StringBuilder();
             sb.AppendFormat("      from DC_REPOSITORY a ");
             sb.AppendFormat(" where 1=1 ");
             if (string.IsNullOrEmpty(sw.DCREPOSITORYID) == false)
                 sb.AppendFormat(" and DCREPOSITORYID = '{0}'", ClsSql.EncodeSql(sw.DCREPOSITORYID));
             if (string.IsNullOrEmpty(sw.REPTYPEID) == false)
                 sb.AppendFormat("and REPTYPEID = '{0}'", ClsSql.EncodeSql(sw.REPTYPEID));
             if (string.IsNullOrEmpty(sw.NAME) == false)
                 sb.AppendFormat("and NAME = '{0}'", ClsSql.EncodeSql(sw.NAME));
             if (string.IsNullOrEmpty(sw.ADDRESS) == false)
                 sb.AppendFormat("and ADDRESS = '{0}'", ClsSql.EncodeSql(sw.ADDRESS));
             if (!string.IsNullOrEmpty(sw.BYORGNO))
             {
                 if (sw.BYORGNO.Substring(4, 11) == "00000000000")//获取所有市的
                        sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,4) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
                    else if (sw.BYORGNO.Substring(6, 9) == "000000000")//获取所有县的
                        sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,6) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
                    else if (sw.BYORGNO.Substring(9, 6) == "000000")//获取说有乡镇的
                        sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,9) = '{0}' or BYORGNO is null or BYORGNO=''", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 9)));
                    else if (sw.BYORGNO.Substring(12, 3) == "000")//获取说有村的
                        sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,12) = '{0}' or BYORGNO is null or BYORGNO=''", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 12)));
                    else
                        sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
             }
             if (string.IsNullOrEmpty(sw.RESPONSIBLEMAN) == false)
                 sb.AppendFormat("and RESPONSIBLEMAN = '{0}'", ClsSql.EncodeSql(sw.RESPONSIBLEMAN));
             if (string.IsNullOrEmpty(sw.LINKWAY) == false)
                 sb.AppendFormat("and LINKWAY = '{0}'", ClsSql.EncodeSql(sw.LINKWAY));
             string sql = ("select DCREPOSITORYID,REPTYPEID,NAME,ADDRESS,BYORGNO,RESPONSIBLEMAN,LINKWAY,JD,WD") + sb.ToString() + ("order by BYORGNO,DCREPOSITORYID desc");
             DataSet ds = DataBaseClass.FullDataSet(sql);
             return ds.Tables[0];
         }
         #endregion

         #region 添加
         /// <summary>
         /// 添加
         /// </summary>
         /// <param name="m"></param>
         /// <returns></returns>
         public static Message Add(DC_REPOSITORY_Model m) 
         {
             if (DC_ARMY.isExistsPoint(new DC_ARMY_Model { JD = m.JD, WD = m.WD }) == false)
             {
                 StringBuilder sb = new StringBuilder();
                 sb.AppendFormat("INSERT INTO  DC_REPOSITORY( NAME,ADDRESS,BYORGNO,RESPONSIBLEMAN,LINKWAY,REPTYPEID,JD,WD ) output inserted.DCREPOSITORYID");
                 sb.AppendFormat(" VALUES(");
                 sb.AppendFormat("{0},", ClsSql.saveNullField(m.NAME));
                 sb.AppendFormat("{0},", ClsSql.saveNullField(m.ADDRESS));
                 sb.AppendFormat("{0},", ClsSql.saveNullField(m.BYORGNO));
                 sb.AppendFormat("{0},", ClsSql.saveNullField(m.RESPONSIBLEMAN));
                 sb.AppendFormat("{0},", ClsSql.saveNullField(m.LINKWAY));
                 sb.AppendFormat("{0},", ClsSql.saveNullField(m.REPTYPEID));
                 sb.AppendFormat("{0},", ClsSql.saveNullField(m.JD));
                 sb.AppendFormat("{0}", ClsSql.saveNullField(m.WD));
                 sb.AppendFormat(")");
                 try
                 {
                     string strid = DataBaseClass.ReturnSqlField(sb.ToString());
                     return new Message(true, "添加成功！", strid);
                 }
                 catch (Exception)
                 {

                     throw;
                 }
             }
             else
             {
                 return new Message(false, "添加失败,已有相同的位置的仓库！", "");
             }
         }
         #endregion

         #region 判断是否相同坐标
         /// <summary>
         /// 判断是否相同坐标
         /// </summary>
         /// <param name="m"></param>
         /// <returns></returns>
         public static bool isExistsPoint(DC_REPOSITORY_Model m)
         {

             StringBuilder sb = new StringBuilder();
             sb.AppendFormat("select 1 from DC_REPOSITORY where 1=1");
             if (string.IsNullOrEmpty(m.JD) == false && string.IsNullOrEmpty(m.WD) == false)
                 sb.AppendFormat(" and JD='{0}'", ClsSql.EncodeSql(m.JD));
             sb.AppendFormat(" and WD='{0}'", ClsSql.EncodeSql(m.WD));
             return DataBaseClass.JudgeRecordExists(sb.ToString());
         }
         #endregion

         #region 修改
         /// <summary>
         /// 修改
         /// </summary>
         /// <param name="m"></param>
         /// <returns></returns>
         public static Message Mdy(DC_REPOSITORY_Model m)
         {
             StringBuilder sb = new StringBuilder();
             sb.AppendFormat("UPDATE DC_REPOSITORY");
             sb.AppendFormat(" set ");
             sb.AppendFormat("NAME={0}", ClsSql.saveNullField(m.NAME));
             sb.AppendFormat(",ADDRESS={0}", ClsSql.saveNullField(m.ADDRESS));
             sb.AppendFormat(",BYORGNO={0}", ClsSql.saveNullField(m.BYORGNO));
             sb.AppendFormat(",RESPONSIBLEMAN={0}", ClsSql.saveNullField(m.RESPONSIBLEMAN));
             sb.AppendFormat(",LINKWAY={0}", ClsSql.saveNullField(m.LINKWAY));
             sb.AppendFormat(",REPTYPEID={0}", ClsSql.saveNullField(m.REPTYPEID));
             sb.AppendFormat(",JD={0}", ClsSql.saveNullField(m.JD));
             sb.AppendFormat(",WD={0}", ClsSql.saveNullField(m.WD));
             sb.AppendFormat(" where DCREPOSITORYID= '{0}'", ClsSql.EncodeSql(m.DCREPOSITORYID));
             bool bln = DataBaseClass.ExeSql(sb.ToString());
             if (bln == true)
                 return new Message(true, "修改成功！", m.DCREPOSITORYID);
             else
                 return new Message(false, "修改失败，请检查各输入框是否正确！", "");
         }
         #endregion

         #region 删除
         /// <summary>
         /// 删除
         /// </summary>
         /// <param name="m">参见模型</param>
         /// <returns>参见模型</returns>
         public static Message Del(DC_REPOSITORY_Model m)
         {
             StringBuilder sb = new StringBuilder();
             sb.AppendFormat("delete DC_REPOSITORY");
             sb.AppendFormat(" where DCREPOSITORYID= '{0}'", ClsSql.EncodeSql(m.DCREPOSITORYID));
             bool bln = DataBaseClass.ExeSql(sb.ToString());
             if (bln == true)
                 return new Message(true, "删除成功！", "");
             else
                 return new Message(false, "删除失败，请检查各输入框是否正确！", "");
         }

         #endregion

         #region 获取分页
         /// <summary>
         /// 获取分页
         /// </summary>
         /// <param name="sw"></param>
         /// <param name="total"></param>
         /// <returns></returns>
         public static DataTable getDT(DC_REPOSITORY_SW sw, out int total)
         {
             StringBuilder sb = new StringBuilder();
             sb.AppendFormat(" FROM     DC_REPOSITORY");
             sb.AppendFormat(" WHERE   1=1");
             if (string.IsNullOrEmpty(sw.REPTYPEID) == false)
                 sb.AppendFormat("and REPTYPEID = '{0}'", ClsSql.EncodeSql(sw.REPTYPEID));
             if (string.IsNullOrEmpty(sw.NAME) == false)
                 sb.AppendFormat("and NAME like '%{0}%'", ClsSql.EncodeSql(sw.NAME));
             if (string.IsNullOrEmpty(sw.ADDRESS) == false)
                 sb.AppendFormat("and ADDRESS = '{0}'", ClsSql.EncodeSql(sw.ADDRESS));
             if (!string.IsNullOrEmpty(sw.BYORGNO))
             {
                 if (sw.BYORGNO.Substring(4, 11) == "00000000000")//获取所有市的
                     sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,4) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
                 else if (sw.BYORGNO.Substring(4, 11) == "xxxxxxxxxxx")//单独市
                     sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,15) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4) + "00000000000"));
                 else if (sw.BYORGNO.Substring(6, 9) == "xxxxxxxxx")//获取所有县的
                     sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,6) = '{0}' )", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
                 else
                     sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
             }
             if (string.IsNullOrEmpty(sw.RESPONSIBLEMAN) == false)
                 sb.AppendFormat("and RESPONSIBLEMAN = '{0}'", ClsSql.EncodeSql(sw.RESPONSIBLEMAN));
             if (string.IsNullOrEmpty(sw.LINKWAY) == false)
                 sb.AppendFormat("and LINKWAY = '{0}'", ClsSql.EncodeSql(sw.LINKWAY));
             string sql = ("select DCREPOSITORYID,REPTYPEID,NAME,ADDRESS,BYORGNO,RESPONSIBLEMAN,LINKWAY,JD,WD") + sb.ToString() + ("order by BYORGNO,DCREPOSITORYID desc");
             string sqlC = "select count(1) " + sb.ToString();
             total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
             sw.curPage = PagerCls.getCurPage(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
             DataSet ds = DataBaseClass.FullDataSet(sql, (sw.curPage - 1) * sw.pageSize, sw.pageSize, "a");
             return ds.Tables[0];
         }
       #endregion

         #region 统计当前用户下仓库的数量
         /// <summary>
         /// 统计当前用户下装备的记录数量
         /// </summary>
         /// <param name="sw"></param>
         /// <returns></returns>
         public static string getNum(DC_REPOSITORY_SW sw)
         {
             string total = "";
             StringBuilder sb = new StringBuilder();
             sb.AppendFormat("    from    DC_REPOSITORY a ");
             sb.AppendFormat("where 1 = 1 ");
             if (sw.BYORGNO.Substring(4, 5) == "00000")//获取所有市的
                 sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,4) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
             else if (sw.BYORGNO.Substring(6, 3) == "000")//获取所有县的
                 sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,6) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
             else
                 sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
             string sqlC = "select count(1) " + sb.ToString();
             total = (DataBaseClass.ReturnSqlField(sqlC)).ToString();
             return total;
         }
         #endregion
     }
}
