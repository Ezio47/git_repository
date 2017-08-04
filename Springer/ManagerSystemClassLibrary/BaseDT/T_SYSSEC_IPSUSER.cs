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
    /// 系统用户管理基本类
    /// </summary>
    public class T_SYSSEC_IPSUSER
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(T_SYSSEC_IPSUSER_Model m)
        {
            //判断用户序号是否存在于系统扩展表中
            if (T_SYSSEC_USER.isExists(new T_SYSSEC_IPSUSER_SW { USERID = m.USERID }) == false)
                return new Message(false, "添加失败，系统用户不存在！", "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO T_SYSSEC_IPSUSER(USERID,SEX,PHONE,USERJOB)");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.USERID));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.SEX));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.PHONE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.USERJOB));
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
            //判断用户序号是否存在于系统扩展表中
            if (T_SYSSEC_USER.isExists(new T_SYSSEC_IPSUSER_SW { USERID = m.USERID }) == false)
                return new Message(false, "修改失败，系统用户不存在！", "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("update T_SYSSEC_IPSUSER");
            sb.AppendFormat(" set ");
            sb.AppendFormat(" SEX='{0}'", ClsSql.EncodeSql(m.SEX));
            sb.AppendFormat(",PHONE='{0}'", ClsSql.EncodeSql(m.PHONE));
            sb.AppendFormat(",USERJOB='{0}'", ClsSql.EncodeSql(m.USERJOB));
            sb.AppendFormat(" where USERID= '{0}'", ClsSql.EncodeSql(m.USERID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功！", "");
            else
                return new Message(false, "修改失败，请检查各输入框是否正确！", "");
        }

        /// <summary>
        /// 修改用户最后操作时间
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message MdyLastOpTime(T_SYSSEC_IPSUSER_Model m)
        {
            //判断用户序号是否存在于系统扩展表中
            if (T_SYSSEC_USER.isExists(new T_SYSSEC_IPSUSER_SW { USERID = m.USERID }) == false)
                return new Message(false, "修改失败，系统用户不存在！", "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("update T_SYSSEC_USER");
            sb.AppendFormat(" set ");
            sb.AppendFormat(" LASTOPTIME='{0}'", ClsSql.EncodeSql(PublicClassLibrary.ClsSwitch.SwitTM(DateTime.Now)));
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
            //判断用户序号是否存在于系统扩展表中
            if (isExists(new T_SYSSEC_IPSUSER_SW { USERID = m.USERID }) == false)
                return new Message(false, "删除失败，系统用户不存在！", "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete T_SYSSEC_IPSUSER");
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
            sb.AppendFormat("select 1 from T_SYSSEC_IPSUSER where 1=1");
            if (string.IsNullOrEmpty(sw.USERID) == false)
                sb.AppendFormat(" and USERID={0}", sw.USERID);
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getDT(T_SYSSEC_IPSUSER_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM T_SYSSEC_IPSUSER a right outer join T_SYSSEC_USER b on a.userid=b.userid WHERE 1=1");
            //针对护林员用户，系统用户表中需要有对应的记录
            sb.AppendFormat(" AND b.USERID is not null");
            //根据登录用户名查询
            if (string.IsNullOrEmpty(sw.LOGINUSERNAME) == false)
                sb.AppendFormat(" AND b.LOGINUSERNAME like '%{0}%'", sw.LOGINUSERNAME);
            //根据用户名查询
            if (string.IsNullOrEmpty(sw.USERNAME) == false)
                sb.AppendFormat(" AND b.USERNAME like '%{0}%'", sw.USERNAME);
            //根据用户序号查询
            if (string.IsNullOrEmpty(sw.USERID) == false)
                sb.AppendFormat(" AND a.USERID ={0}", sw.USERID);
            //根据科室查询
            if (string.IsNullOrEmpty(sw.DEPARTMENT) == false)
                sb.AppendFormat(" AND b.DEPARTMENT = '{0}'", sw.DEPARTMENT);
            //根据组织机构查询
            if (!string.IsNullOrEmpty(sw.ORGNO))
            {
                if (sw.ORGNO.Substring(4, 11) == "00000000000")//获取所有市的
                    sb.AppendFormat(" AND (SUBSTRING(b.ORGNO,1,4) = '{0}' or b.ORGNO is null or b.ORGNO='')", ClsSql.EncodeSql(sw.ORGNO.Substring(0, 4)));
                else if (sw.ORGNO.Substring(6, 9) == "000000000")//获取所有县的
                    sb.AppendFormat(" AND (SUBSTRING(b.ORGNO,1,6) = '{0}' or b.ORGNO is null or b.ORGNO='')", ClsSql.EncodeSql(sw.ORGNO.Substring(0, 6)));
                else if (sw.ORGNO.Substring(9, 6) == "000000")//获取所有县的
                    sb.AppendFormat(" AND (SUBSTRING(b.ORGNO,1,9) = '{0}' or b.ORGNO is null or b.ORGNO='')", ClsSql.EncodeSql(sw.ORGNO.Substring(0, 9)));
                else
                    sb.AppendFormat(" AND b.ORGNO = '{0}'", ClsSql.EncodeSql(sw.ORGNO));
            }
            if (string.IsNullOrEmpty(sw.USERPWD) == false)
                sb.AppendFormat(" AND b.USERPWD='{0}'", sw.USERPWD);

            string sql = "SELECT a.GID,a.SEX, a.PHONE, a.USERJOB"
                + ",b.USERID,b.ORGNO, b.LOGINUSERNAME, b.USERNAME, b.USERPWD, b.REGISTERTIME, b.LOGINNUM, b.LOGINIP, b.LASTTIME, b.NOTE,b.DEPARTMENT"
                + sb.ToString() + " order by b.ORGNO";
            string sqlC = "select count(1) " + sb.ToString();
            total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
            sw.curPage = PagerCls.getCurPage(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
            DataSet ds = DataBaseClass.FullDataSet(sql, (sw.curPage - 1) * sw.pageSize, sw.pageSize, "a");
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getDT2(T_SYSSEC_IPSUSER_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM T_SYSSEC_IPSUSER a right outer join T_SYSSEC_USER b on a.userid=b.userid WHERE 1=1");
            //针对护林员用户，系统用户表中需要有对应的记录
            sb.AppendFormat(" AND b.USERID is not null");
            //根据登录用户名查询
            if (string.IsNullOrEmpty(sw.LOGINUSERNAME) == false)
                sb.AppendFormat(" AND b.LOGINUSERNAME like '%{0}%'", sw.LOGINUSERNAME);
            //根据用户名查询
            if (string.IsNullOrEmpty(sw.USERNAME) == false)
                sb.AppendFormat(" AND b.USERNAME like '%{0}%'", sw.USERNAME);
            //根据组织机构查询
            if (!string.IsNullOrEmpty(sw.ORGNO))
                sb.AppendFormat(" AND b.ORGNO = '{0}'", ClsSql.EncodeSql(sw.ORGNO));
            //根据是否开通OA
            if (!string.IsNullOrEmpty(sw.IsOpenOA))
            {
                if (sw.IsOpenOA == "1")
                    sb.AppendFormat(" AND b.IsOpenOA = '{0}'", ClsSql.EncodeSql(sw.IsOpenOA));
                else
                    sb.AppendFormat(" AND (b.IsOpenOA = '{0}' or b.IsOpenOA is NULL )", ClsSql.EncodeSql(sw.IsOpenOA));
            }

            string sql = "SELECT a.GID,a.SEX, a.PHONE, a.USERJOB"
                + ",b.USERID,b.ORGNO, b.LOGINUSERNAME, b.USERNAME, b.USERPWD, b.REGISTERTIME, b.LOGINNUM, b.LOGINIP, b.LASTTIME, b.NOTE,b.DEPARTMENT,b.IsOpenOA"
                + sb.ToString() + " order by b.ORGNO";
            string sqlC = "select count(1) " + sb.ToString();
            total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
            sw.curPage = PagerCls.getCurPage(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
            DataSet ds = DataBaseClass.FullDataSet(sql, (sw.curPage - 1) * sw.pageSize, sw.pageSize, "a");
            return ds.Tables[0];
        }

        /// <summary>
        /// 无分页
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static DataTable getDT(T_SYSSEC_IPSUSER_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM      T_SYSSEC_IPSUSER a right outer join T_SYSSEC_USER b on a.userid=b.userid WHERE 1=1");
            //针对护林员用户，系统用户表中需要有对应的记录
            sb.AppendFormat(" AND b.USERID is not null");
            //根据用户名查询
            if (string.IsNullOrEmpty(sw.LOGINUSERNAME) == false)
                //sb.AppendFormat(" AND b.LOGINUSERNAME like '%{0}%'", sw.LOGINUSERNAME);
                sb.AppendFormat(" AND b.LOGINUSERNAME = '{0}'", sw.LOGINUSERNAME);
            //根据用户序号查询
            if (string.IsNullOrEmpty(sw.USERID) == false)
            {
                if (sw.USERID.Split(',').Length == 1)
                    sb.AppendFormat(" AND b.USERID ={0}", sw.USERID);
                else
                    sb.AppendFormat(" AND b.USERID in({0})", sw.USERID);
            }
            if (string.IsNullOrEmpty(sw.curOrgNo) == false)
                sb.AppendFormat(" AND ORGNO = '{0}'", ClsSql.EncodeSql(sw.curOrgNo));
            if (string.IsNullOrEmpty(sw.DEPARTMENT) == false)
                sb.AppendFormat(" AND b.DEPARTMENT = '{0}'", ClsSql.EncodeSql(sw.DEPARTMENT));
            if (!string.IsNullOrEmpty(sw.ORGNO))
            {
                if (sw.ORGNO.Substring(4, 11) == "00000000000")//获取所有市的
                    sb.AppendFormat(" AND (SUBSTRING(b.ORGNO,1,4) = '{0}' or b.ORGNO is null or b.ORGNO='')", ClsSql.EncodeSql(sw.ORGNO.Substring(0, 4)));
                else if (sw.ORGNO.Substring(6, 9) == "000000000")//获取所有县的
                    sb.AppendFormat(" AND (SUBSTRING(b.ORGNO,1,6) = '{0}' or b.ORGNO is null or b.ORGNO='')", ClsSql.EncodeSql(sw.ORGNO.Substring(0, 6)));
                else if (sw.ORGNO.Substring(9, 6) == "000000")//获取所有县的
                    sb.AppendFormat(" AND (SUBSTRING(b.ORGNO,1,9 )= '{0}' or b.ORGNO is null or b.ORGNO=''", ClsSql.EncodeSql(sw.ORGNO.Substring(0, 9)));
                else
                    sb.AppendFormat(" AND b.ORGNO = '{0}'", ClsSql.EncodeSql(sw.ORGNO));
            }
            string sql = "SELECT    a.GID, a.SEX, a.PHONE, a.USERJOB"
                + ",b.USERID, b.ORGNO, b.LOGINUSERNAME, b.USERNAME, b.USERPWD, b.DEPARTMENT"
                + ", b.REGISTERTIME, b.LOGINNUM, b.LOGINIP, b.LASTTIME, b.NOTE,b.LASTOPTIME"
                + sb.ToString()
            + " order by b.ORGNO";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }

        #region 获取手机号码字符串组合
        /// <summary>
        /// 获取手机号码字符串组合
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getphone(T_SYSSEC_IPSUSER_SW sw)
        {
            DataTable dt = getDT(sw);
            string phone = "";
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == dt.Rows.Count - 1)
                    {
                        phone += dt.Rows[i]["PHONE"].ToString();
                    }
                    else
                    {
                        phone += dt.Rows[i]["PHONE"].ToString() + ",";
                    }
                }
            }
            dt.Clear();
            dt.Dispose();
            return phone;
        }
        #endregion
    }
}
