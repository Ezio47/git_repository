using DataBaseClassLibrary;
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
    /// 野生植物分布
    /// </summary>
  public  class WILD_BOTANYDISTRIBUTE
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
      public static Message Add(WILD_BOTANYDISTRIBUTE_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  WILD_BOTANYDISTRIBUTE( BIOLOGICALTYPECODE, POPULATIONTYPE, JD, WD, BOTANYCOUNT,BOTANYAREA, MARK,BYORGNO) output inserted.WILD_BOTANYDISTRIBUTEID");
            sb.AppendFormat(" VALUES(");
            sb.AppendFormat("{0}", ClsSql.saveNullField(m.BIOLOGICALTYPECODE));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.POPULATIONTYPE));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.JD));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.WD));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.BOTANYCOUNT));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.BOTANYAREA));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.MARK));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.BYORGNO));
            sb.AppendFormat(")");
            try
            {
                string strid = DataBaseClass.ReturnSqlField(sb.ToString());
                if (string.IsNullOrEmpty(strid) == false)
                {
                    return new Message(true, "添加成功！", strid);
                }
                else
                {
                    return new Message(false, "添加失败！请检查输入框是否正确", strid);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
      public static Message Mdy(WILD_BOTANYDISTRIBUTE_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE WILD_BOTANYDISTRIBUTE");
            sb.AppendFormat(" set ");
            sb.AppendFormat("BIOLOGICALTYPECODE={0}", ClsSql.saveNullField(m.BIOLOGICALTYPECODE));
            sb.AppendFormat(",POPULATIONTYPE={0}", ClsSql.saveNullField(m.POPULATIONTYPE));
            sb.AppendFormat(",MARK={0}", ClsSql.saveNullField(m.MARK));
            sb.AppendFormat(",JD={0}", ClsSql.saveNullField(m.JD));
            sb.AppendFormat(",WD={0}", ClsSql.saveNullField(m.WD));
            sb.AppendFormat(",BYORGNO ={0}", ClsSql.saveNullField(m.BYORGNO));
            sb.AppendFormat(",BOTANYCOUNT ={0}", ClsSql.saveNullField(m.BOTANYCOUNT));
            sb.AppendFormat(",BOTANYAREA ={0}", ClsSql.saveNullField(m.BOTANYAREA));
            sb.AppendFormat(" where WILD_BOTANYDISTRIBUTEID= '{0}'", ClsSql.EncodeSql(m.WILD_BOTANYDISTRIBUTEID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功！", m.WILD_BOTANYDISTRIBUTEID);
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
      public static Message Del(WILD_BOTANYDISTRIBUTE_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete WILD_BOTANYDISTRIBUTE");
            sb.AppendFormat(" where WILD_BOTANYDISTRIBUTEID= '{0}'", ClsSql.EncodeSql(m.WILD_BOTANYDISTRIBUTEID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功！", "");
            else
                return new Message(false, "删除失败，请检查各输入框是否正确！", "");
        }

        #endregion

        #region 获取记录
        /// <summary>
        /// 获取记录
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
      public static DataTable getDT(WILD_BOTANYDISTRIBUTE_SW sw)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat(" FROM      WILD_BOTANYDISTRIBUTE");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.WILD_BOTANYDISTRIBUTEID) == false)
                sb.AppendFormat(" AND WILD_BOTANYDISTRIBUTEID = '{0}'", ClsSql.EncodeSql(sw.WILD_BOTANYDISTRIBUTEID));
            if (string.IsNullOrEmpty(sw.BIOLOGICALTYPECODE) == false)
                sb.AppendFormat(" AND BIOLOGICALTYPECODE = '{0}'", ClsSql.EncodeSql(sw.BIOLOGICALTYPECODE));
            if (string.IsNullOrEmpty(sw.POPULATIONTYPE) == false)
                sb.AppendFormat(" AND POPULATIONTYPE  = '{0}'", ClsSql.EncodeSql(sw.POPULATIONTYPE));
            if (string.IsNullOrEmpty(sw.ORGNO) == false)
            {
                sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.ORGNO));
            }
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
            {
                if (sw.BYORGNO.Substring(4, 11) == "00000000000")//获取所有市的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,4) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
                else if (sw.BYORGNO.Substring(4, 11) == "xxxxxxxxxxx")//单独市
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,15) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4) + "00000000000"));
                else if (sw.BYORGNO.Substring(6, 9) == "xxxxxxxxx")//获取所有县的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,6) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
                else if (sw.BYORGNO.Substring(9, 6) == "000000")//获取所有乡镇的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,9) = '{0}'or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 9)));
                else if (sw.BYORGNO.Substring(12, 3) == "000")//获取所有村的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,12) = '{0}'or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 12)));
                else
                    sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            }
            string sql = "SELECT WILD_BOTANYDISTRIBUTEID, BIOLOGICALTYPECODE, POPULATIONTYPE, JD, WD, BYORGNO, BOTANYCOUNT,BOTANYAREA, MARK"
                + sb.ToString()
                + " order by WILD_BOTANYDISTRIBUTEID desc";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }

        #endregion

        #region 获取分页列表
        /// <summary>
        ///获取分页列表
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="total"></param>
        /// <returns></returns>
      public static DataTable getDT(WILD_BOTANYDISTRIBUTE_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM      WILD_BOTANYDISTRIBUTE");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.WILD_BOTANYDISTRIBUTEID) == false)
                sb.AppendFormat(" AND WILD_BOTANYDISTRIBUTEID = '{0}'", ClsSql.EncodeSql(sw.WILD_BOTANYDISTRIBUTEID));
            if (string.IsNullOrEmpty(sw.BIOLOGICALTYPECODE) == false)
                sb.AppendFormat(" AND BIOLOGICALTYPECODE = '{0}'", ClsSql.EncodeSql(sw.BIOLOGICALTYPECODE));
            if (string.IsNullOrEmpty(sw.POPULATIONTYPE) == false)
                sb.AppendFormat(" AND POPULATIONTYPE  = '{0}'", ClsSql.EncodeSql(sw.POPULATIONTYPE));
            if (string.IsNullOrEmpty(sw.ORGNO) == false)
            {
                sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.ORGNO));
            }
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
            {
                if (sw.BYORGNO.Substring(4, 11) == "00000000000")//获取所有市的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,4) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
                else if (sw.BYORGNO.Substring(4, 11) == "xxxxxxxxxxx")//单独市
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,15) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4) + "00000000000"));
                else if (sw.BYORGNO.Substring(6, 9) == "xxxxxxxxx")//获取所有县的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,6) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
                else if (sw.BYORGNO.Substring(9, 6) == "000000")//获取所有乡镇的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,9) = '{0}'or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 9)));
                else if (sw.BYORGNO.Substring(12, 3) == "000")//获取所有村的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,12) = '{0}'or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 12)));
                else
                    sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            }
            string sql = "SELECT WILD_BOTANYDISTRIBUTEID, BIOLOGICALTYPECODE, POPULATIONTYPE, JD, WD, BYORGNO, BOTANYCOUNT,BOTANYAREA, MARK"
                + sb.ToString()
                + " order by BYORGNO,WILD_BOTANYDISTRIBUTEID  desc";
            string sqlC = "select count(1) " + sb.ToString();
            total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
            sw.curPage = PagerCls.getCurPage(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
            DataSet ds = DataBaseClass.FullDataSet(sql, (sw.curPage - 1) * sw.pageSize, sw.pageSize, "a");
            return ds.Tables[0];
        }

        #endregion

      #region 统计当前用户下野生植物的记录数量
      /// <summary>
      /// 统计当前用户下植物的记录数量
      /// </summary>
      /// <param name="sw"></param>
      /// <returns></returns>
      public static string getNum(WILD_BOTANYDISTRIBUTE_SW sw)
      {
          string total = "";
          StringBuilder sb = new StringBuilder();
          sb.AppendFormat("    from    WILD_BOTANYDISTRIBUTE a ");
          sb.AppendFormat("where 1 = 1 ");
          if (sw.BYORGNO.Substring(4, 11) == "00000000000")  //获取所有市的
              sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,4) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
          else if (sw.BYORGNO.Substring(6, 9) == "000000000" && sw.BYORGNO.Substring(4, 11) != "00000000000") //获取所有县的
              sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,6) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
          else
              sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,9) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 9)));
          string sqlC = "select count(1) " + sb.ToString();
          total = (DataBaseClass.ReturnSqlField(sqlC)).ToString();
          return total;
      }
      #endregion
    }
}
