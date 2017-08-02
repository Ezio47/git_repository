using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataBaseClassLibrary;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel;
using PublicClassLibrary;

namespace ManagerSystemClassLibrary.BaseDT
{
    /// <summary>
    /// 系统用户公共表操作基本类
    /// </summary>
   public class T_SYSSEC_USER
   {
       /// <summary>
       /// 添加
       /// </summary>
       /// <param name="m">参见模型</param>
       /// <returns>参见模型</returns>
       public static Message Add(T_SYSSEC_IPSUSER_Model m)
       {
           if (isExists(new T_SYSSEC_IPSUSER_SW { LOGINUSERNAME = m.LOGINUSERNAME }) == true)
               return new Message(false, "添加失败，登陆名重复请重新输入！", "");

           StringBuilder sb = new StringBuilder();
           sb.AppendFormat("INSERT INTO T_SYSSEC_USER(ORGNO,LOGINUSERNAME,USERNAME,USERPWD,REGISTERTIME,LOGINNUM,NOTE,DEPARTMENT)");
           sb.AppendFormat("VALUES(");
           sb.AppendFormat("'{0}'",ClsSql.EncodeSql( m.ORGNO));
           sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.LOGINUSERNAME));
           sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.USERNAME));
           sb.AppendFormat(",'{0}'", ClsStr.getSystemManMd5(ClsSql.EncodeSql(m.USERPWD)));
           sb.AppendFormat(",'{0}'", PublicClassLibrary.ClsSwitch.SwitTM(DateTime.Now));
           sb.AppendFormat(",'{0}'","0");
           sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.NOTE));
           sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.DEPARTMENT));
           sb.AppendFormat(")");
           bool bln = DataBaseClass.ExeSql(sb.ToString());
           if (bln == true)
               return new Message(true, "添加成功！", "");
           else
               return new Message(false, "添加失败，请检查各输入框是否正确！", "");
       }

       /// <summary>
       /// 修改
       /// </summary>
       /// <param name="m">参见模型</param>
       /// <returns>参见模型</returns>
       public static Message Mdy(T_SYSSEC_IPSUSER_Model m)
       {
           if (string.IsNullOrEmpty(m.USERID))
               return new Message(false, "修改失败，无效的用户序号！", "");
           if (isExists(new T_SYSSEC_IPSUSER_SW { USERID = m.USERID }) == false)
               return new Message(false, "修改失败，用户名不存在！", "");

           StringBuilder sb = new StringBuilder();
           //(ORGNO,LOGINUSERNAME,USERNAME,USERPWD,REGISTERTIME,LOGINNUM,NOTE)
           sb.AppendFormat("UPDATE T_SYSSEC_USER");
           sb.AppendFormat(" set ");
           sb.AppendFormat("ORGNO='{0}'", ClsSql.EncodeSql(m.ORGNO));
           sb.AppendFormat(",LOGINUSERNAME='{0}'", ClsSql.EncodeSql(m.LOGINUSERNAME));
           sb.AppendFormat(",USERNAME='{0}'", ClsSql.EncodeSql(m.USERNAME));
           if (string.IsNullOrEmpty(m.USERPWD)==false)
               sb.AppendFormat(",USERPWD='{0}'", ClsStr.getSystemManMd5(ClsSql.EncodeSql(m.USERPWD))); 
           sb.AppendFormat(" ,NOTE= '{0}'", ClsSql.EncodeSql(m.NOTE));
           sb.AppendFormat(" ,DEPARTMENT= '{0}'", ClsSql.EncodeSql(m.DEPARTMENT));
           sb.AppendFormat(" where USERID= '{0}'", ClsSql.EncodeSql(m.USERID));
           
           bool bln = DataBaseClass.ExeSql(sb.ToString());
           if (bln == true)
               return new Message(true, "修改成功！", "");
           else
               return new Message(false, "修改失败，请检查各输入框是否正确！", "");
       }

       /// <summary>
       /// 删除
       /// </summary>
       /// <param name="m">参见模型</param>
       /// <returns>参见模型</returns>
       public static Message Del(T_SYSSEC_IPSUSER_Model m)
       {
           StringBuilder sb = new StringBuilder();
           sb.AppendFormat("delete T_SYSSEC_USER");
           sb.AppendFormat(" where USERID= '{0}'", ClsSql.EncodeSql(m.USERID));
           bool bln = DataBaseClass.ExeSql(sb.ToString());
           if (bln == true)
               return new Message(true, "删除成功！", "");
           else
               return new Message(false, "删除失败，请检查各输入框是否正确！", "");
       }

       /// <summary>
       /// 判断记录是否存在
       /// </summary>
       /// <param name="sw">参见模型</param>
       /// <returns>true存在 false不存在</returns>
       public static bool isExists(T_SYSSEC_IPSUSER_SW sw)
       {
           StringBuilder sb = new StringBuilder();
           sb.AppendFormat("select 1 from T_SYSSEC_USER where 1=1");
           if (string.IsNullOrEmpty(sw.USERID) == false)
               sb.AppendFormat(" and USERID={0}",ClsSql.EncodeSql(sw.USERID));
           if (string.IsNullOrEmpty(sw.LOGINUSERNAME) == false)
               sb.AppendFormat(" and LOGINUSERNAME='{0}'", ClsSql.EncodeSql(sw.LOGINUSERNAME));
           return DataBaseClass.JudgeRecordExists(sb.ToString());
       }

       /// <summary>
       /// 获取数据
       /// </summary>
       /// <returns>参见模型</returns>
       public static DataTable getDT(T_SYSSEC_IPSUSER_SW sw)
       {         
           StringBuilder sb = new StringBuilder();
           sb.AppendFormat("SELECT    USERID, ORGNO, LOGINUSERNAME, USERNAME, USERPWD, REGISTERTIME, LOGINNUM, LOGINIP,LASTTIME, NOTE, DEPARTMENT");
           sb.AppendFormat(" FROM      T_SYSSEC_USER a");
           sb.AppendFormat(" WHERE   1=1");
           if (string.IsNullOrEmpty(sw.USERID) == false)
           {
               if (sw.USERID.Split(',').Length > 1)
                   sb.AppendFormat(" AND a.USERID in({0})", ClsSql.EncodeSql(sw.USERID));
               else
                   sb.AppendFormat(" AND a.USERID ='{0}'", ClsSql.EncodeSql(sw.USERID));
           }
            //sb.AppendFormat(" AND USERID in( {0})",ClsSql.EncodeSql( sw.USERID));
           if (string.IsNullOrEmpty(sw.LOGINUSERNAME) == false)
               sb.AppendFormat(" AND LOGINUSERNAME = '{0}'", ClsSql.EncodeSql(sw.LOGINUSERNAME));
           if (string.IsNullOrEmpty(sw.DEPARTMENT) == false)
               sb.AppendFormat(" AND DEPARTMENT = '{0}'", ClsSql.EncodeSql(sw.DEPARTMENT));
           if (string.IsNullOrEmpty(sw.curOrgNo) == false)
               sb.AppendFormat(" AND ORGNO = '{0}'", ClsSql.EncodeSql(sw.curOrgNo));
           if (!string.IsNullOrEmpty(sw.ORGNO))
           {
               if (sw.ORGNO.Substring(4, 9) == "00000000000")//获取所有市的
                   sb.AppendFormat(" AND (SUBSTRING(ORGNO,1,4) = '{0}' or ORGNO is null or ORGNO='')", ClsSql.EncodeSql(sw.ORGNO.Substring(0, 4)));
               else if (sw.ORGNO.Substring(6, 9) == "000000000")//获取所有县的
                   sb.AppendFormat(" AND (SUBSTRING(ORGNO,1,6) = '{0}' or ORGNO is null or ORGNO='')", ClsSql.EncodeSql(sw.ORGNO.Substring(0, 6)));
               else if (sw.ORGNO.Substring(9,6)=="000000")//获取所有镇的
                   sb.AppendFormat(" AND ORGNO = '{0}'", ClsSql.EncodeSql(sw.ORGNO));
               else
                   sb.AppendFormat(" AND ORGNO = '{0}'", ClsSql.EncodeSql(sw.ORGNO));
           }
           sb.AppendFormat(" ORDER BY USERID DESC");
           DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
           return ds.Tables[0];
       }

       /// <summary>
       /// 根据编号获取用户登录名
       /// </summary>
       /// <param name="dt">用户DataTable</param>
       /// <param name="value">编号</param>
       /// <returns>用户登录名</returns>
       public static string getName(DataTable dt, string value)
       {
           if (dt == null)
               return "";
           if (string.IsNullOrEmpty(value))
               return "";
           string str = "";
           DataRow[] dr = dt.Select("USERID='" + value + "'");
           if (dr.Length > 0)
               str = dr[0]["LOGINUSERNAME"].ToString();
           return str;
       }

       /// <summary>
       /// 根据用户序号列表获取中文姓名列表
       /// </summary>
       /// <param name="dt"></param>
       /// <param name="UserIDList">1,3,5</param>
       /// <returns>张三,李四,王二</returns>
       public static string getNameByUserList(DataTable dt, string UserIDList)
       {
           if (dt == null)
               return "";
           if (string.IsNullOrEmpty(UserIDList))
               return "";
           string str = "";
           string[] arr = UserIDList.Split(',');
           for (int i = 0; i < arr.Length; i++)
           {
               DataRow[] dr = dt.Select("USERID='" + arr[i] + "'");
               if (dr.Length > 0)
               {
                   if (string.IsNullOrEmpty(str))
                       str = dr[0]["USERNAME"].ToString();
                   else
                       str += "," + dr[0]["USERNAME"].ToString();
               }
           }
           return str;
       }

       /// <summary>
       /// 更新OA账号状态
       /// </summary>
       /// <param name="userIdList"></param>
       /// <param name="value"></param>
       /// <returns></returns>
       public static Message MdyIsOpenOA(string userIdList, string value)
       {
           List<string> sqllist = new List<string>();
           string[] userIdArray = userIdList.Split(',');
           foreach (string id in userIdArray)
           {
               StringBuilder sb = new StringBuilder();
               sb.AppendFormat("update T_SYSSEC_USER SET");
               sb.AppendFormat(" IsOpenOA='{0}'", ClsSql.EncodeSql(value));
               sb.AppendFormat(" where USERID= '{0}'", ClsSql.EncodeSql(id));
               sqllist.Add(sb.ToString());
           }
           var y = DataBaseClass.ExecuteSqlTran(sqllist);
           if (y > 0)
           {
               return new Message(true, "更新成功!", "");
           }
           else
           {
               return new Message(false, "更新失败，事物回滚机制!", "");
           }
       }
    }
}
