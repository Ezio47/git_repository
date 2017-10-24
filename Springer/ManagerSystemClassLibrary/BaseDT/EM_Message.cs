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
    /// 短信模板
    /// </summary>
   public class EM_Message
   {
       #region 添加
       /// <summary>
       /// 模板添加
       /// </summary>
       /// <param name="m"></param>
       /// <returns></returns>
       public static Message Add(SendMessage_Model m)
       {
           StringBuilder sb = new StringBuilder();
           sb.AppendFormat("INSERT INTO  EM_MESSAGE(  TMPCONTENT, ORDERBY)");
           sb.AppendFormat("VALUES(");
           sb.AppendFormat("{0}", ClsSql.saveNullField(m.MessageContent));
           //sb.AppendFormat(",{0}", ClsSql.saveNullField(m.MessageContent));
           sb.AppendFormat(",{0}", ClsSql.saveNullField(m.ORDERBY));
           sb.AppendFormat(")");
           bool bln = DataBaseClass.ExeSql(sb.ToString());
           if (bln == true)
               return new Message(true, "添加成功！", "");
           else
               return new Message(false, "添加失败！", "");
       }
       #endregion

       #region 修改
       /// <summary>
       /// 模板修改
       /// </summary>
       /// <param name="m"></param>
       /// <returns></returns>
       public static Message Mdy(SendMessage_Model m)
       {
           StringBuilder sb = new StringBuilder();
           sb.AppendFormat("UPDATE EM_MESSAGE");
           sb.AppendFormat(" set ");
           sb.AppendFormat("TMPCONTENT={0}", ClsSql.saveNullField(m.MessageContent));
           sb.AppendFormat(",ORDERBY={0}", ClsSql.saveNullField(m.ORDERBY));
           sb.AppendFormat(" where EM_MESSAGEID= '{0}'", ClsSql.EncodeSql(m.EM_MESSAGEID));
           bool bln = DataBaseClass.ExeSql(sb.ToString());
           if (bln == true)
               return new Message(true, "修改成功！", m.EM_MESSAGEID);
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
       public static Message Del(SendMessage_Model m)
       {
           StringBuilder sb = new StringBuilder();
           sb.AppendFormat("delete EM_MESSAGE");
           sb.AppendFormat(" where EM_MESSAGEID= '{0}'", ClsSql.EncodeSql(m.EM_MESSAGEID));
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
       public static DataTable getDT(EM_Message_SW sw)
       {
           StringBuilder sb = new StringBuilder();
           sb.AppendFormat(" FROM      EM_MESSAGE");
           sb.AppendFormat(" WHERE   1=1");
           if (string.IsNullOrEmpty(sw.EM_MESSAGEID) == false)
               sb.AppendFormat(" AND EM_MESSAGEID = '{0}'", ClsSql.EncodeSql(sw.EM_MESSAGEID));
           if (string.IsNullOrEmpty(sw.MessageContent) == false)
               sb.AppendFormat(" AND TMPCONTENT = '{0}'", ClsSql.EncodeSql(sw.MessageContent));
           string sql = "SELECT EM_MESSAGEID, TMPCONTENT, ORDERBY"
               + sb.ToString()
               + " order by EM_MESSAGEID,ORDERBY";

           DataSet ds = DataBaseClass.FullDataSet(sql);
           return ds.Tables[0];
       }

       #endregion

       #region 获取记录涉及分页
       /// <summary>
       /// 获取记录
       /// </summary>
       /// <param name="sw"></param>
       ///  <param name="total">总共</param>
       /// <returns></returns>
       public static DataTable getDT(EM_Message_SW sw, out int total)
       {
           StringBuilder sb = new StringBuilder();
           sb.AppendFormat(" FROM      EM_MESSAGE");
           sb.AppendFormat(" WHERE   1=1");
           if (string.IsNullOrEmpty(sw.EM_MESSAGEID) == false)
               sb.AppendFormat(" AND EM_MESSAGEID = '{0}'", ClsSql.EncodeSql(sw.EM_MESSAGEID));
           if (string.IsNullOrEmpty(sw.MessageContent) == false)
               sb.AppendFormat(" AND TMPCONTENT = '{0}'", ClsSql.EncodeSql(sw.MessageContent));
           string sql = "SELECT EM_MESSAGEID, TMPCONTENT, ORDERBY"
               + sb.ToString()
               + " order by EM_MESSAGEID,ORDERBY";
           string sqlC = "select count(1) " + sb.ToString();
           total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
           sw.curPage = PagerCls.getCurPage(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
           DataSet ds = DataBaseClass.FullDataSet(sql, (sw.curPage - 1) * sw.pageSize, sw.pageSize, "a");
           //DataSet ds = DataBaseClass.FullDataSet(sql);
           return ds.Tables[0];
       }

       #endregion

    }
}
