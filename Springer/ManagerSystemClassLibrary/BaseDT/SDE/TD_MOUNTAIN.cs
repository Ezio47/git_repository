using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel.SDEModel;
using PublicClassLibrary;
using DataBaseClassLibrary;
using System.Data;

namespace ManagerSystemClassLibrary.BaseDT.SDE
{
    /// <summary>
    /// 三维-山数据
    /// </summary>
    public class TD_MOUNTAIN
    {
        /// <summary>
        /// 三维添加
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message Add(TD_MOUNTAIN_Model m)
        {
            if (PublicCls.OrgIsZhen(m.BYORGNO) == false)
                return new Message(false, "添加失败，请选择所属乡镇！", "");
            if (TD_MOUNTAIN.isPExists(new TD_MOUNTAIN_Model { JD = m.JD, WD = m.WD }) == false)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("insert into MOUNTAIN(NAME,BYORGNO,BYORGNOXS,DISPLAY_X,DISPLAY_Y,Shape,VILLAGE,TYPE) values(");
                sb.AppendFormat("{0},", ClsSql.saveNullField(m.NAME));
                sb.AppendFormat("{0},", ClsSql.saveNullField(m.BYORGNO));
                sb.AppendFormat("{0},", ClsSql.saveNullField(m.BYORGNOXS));
                sb.AppendFormat("{0},", ClsSql.saveNullField(m.JD));
                sb.AppendFormat("{0},", ClsSql.saveNullField(m.WD));
                sb.AppendFormat("{0},", m.Shape);
                sb.AppendFormat("{0},", ClsSql.saveNullField(m.VILLAGE));
                sb.AppendFormat("{0})", ClsSql.saveNullField(m.TYPE));
                bool bln = SDEDataBaseClass.ExeSql(sb.ToString());
                if (bln == true)
                    return new Message(true, "添加成功！", "");
                else
                    return new Message(false, "添加失败，请检查各输入框是否正确！", "");
            }
            else
            {
                return new Message(false, "已有相同地址的山,请重新选择地址", "");
            }
        }
        /// <summary>
        /// 三维-修改
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message Mdy(TD_MOUNTAIN_Model m)
        {
            if (PublicCls.OrgIsZhen(m.BYORGNO) == false)
                return new Message(false, "修改失败，请选择所属乡镇！", "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE MOUNTAIN");
            sb.AppendFormat(" set ");
            sb.AppendFormat("NAME={0}", ClsSql.saveNullField(m.NAME));
            sb.AppendFormat(",BYORGNO={0}", ClsSql.saveNullField(m.BYORGNO));
            sb.AppendFormat(",BYORGNOXS={0}", ClsSql.saveNullField(m.BYORGNOXS));
            sb.AppendFormat(",DISPLAY_X={0}", ClsSql.saveNullField(m.JD));
            sb.AppendFormat(",DISPLAY_Y={0}", ClsSql.saveNullField(m.WD));
            sb.AppendFormat(",VILLAGE={0}", ClsSql.saveNullField(m.VILLAGE));
            sb.AppendFormat(",TYPE={0}", ClsSql.saveNullField(m.TYPE));
            sb.AppendFormat(",Shape={0}", m.Shape);
            sb.AppendFormat(" where OBJECTID= {0}", ClsSql.saveNullField(m.OBJECTID));
            bool bln = SDEDataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功！", "");
            else
                return new Message(false, "修改失败，请检查各输入框是否正确！", "");
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message Del(TD_MOUNTAIN_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete MOUNTAIN");
            sb.AppendFormat(" where OBJECTID= '{0}'", ClsSql.EncodeSql(m.OBJECTID));
            bool bln = SDEDataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功！", "");
            else
                return new Message(false, "删除失败，请检查各输入框是否正确！", "");
        }
        /// <summary>
        /// 是否存在相同的点
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static bool isPExists(TD_MOUNTAIN_Model m)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from MOUNTAIN where 1=1");
            if (string.IsNullOrEmpty(m.JD) == false && string.IsNullOrEmpty(m.WD) == false)
                sb.AppendFormat(" and DISPLAY_X='{0}'", ClsSql.EncodeSql(m.JD));
            sb.AppendFormat(" and DISPLAY_Y='{0}'", ClsSql.EncodeSql(m.WD));
            return SDEDataBaseClass.JudgeRecordExists(sb.ToString());
        }

        #region 判断记录是否存在
        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>true存在 false不存在</returns>
        public static bool isExists(TD_MOUNTAIN_Model m)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from MOUNTAIN where 1=1");
            if (string.IsNullOrEmpty(m.OBJECTID) == false)
                sb.AppendFormat(" where OBJECTID= '{0}'", ClsSql.EncodeSql(m.OBJECTID));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }

        #endregion

        #region 获取记录
        /// <summary>
        /// 获取记录
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(TD_MOUNTAIN_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM      MOUNTAIN");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.OBJECTID) == false)
                sb.AppendFormat(" AND OBJECTID = '{0}'", ClsSql.EncodeSql(sw.OBJECTID));
            if (string.IsNullOrEmpty(sw.ORGNO) == false)
                sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.ORGNO));
            if (string.IsNullOrEmpty(sw.NAME) == false)
                sb.AppendFormat(" AND NAME like '%{0}%'", ClsSql.EncodeSql(sw.NAME));
            if (string.IsNullOrEmpty(sw.type) == false)
                sb.AppendFormat(" AND TYPE = '{0}'", ClsSql.EncodeSql(sw.type));
            if (!string.IsNullOrEmpty(sw.BYORGNO))
            {
                if (sw.BYORGNO.Substring(4, 5) == "00000")//获取所有市的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,4) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
                else if (sw.BYORGNO.Substring(6, 3) == "000")//获取所有县的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,6) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
                else
                    sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            }
            string sql = "SELECT OBJECTID,NAME,BYORGNO,BYORGNOXS,DISPLAY_X,DISPLAY_Y,Shape,VILLAGE,TYPE"
                + sb.ToString()
                + " order by BYORGNO,OBJECTID";

            DataSet ds = SDEDataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }

        #endregion

        #region 获取分页列表
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static DataTable getDT(TD_MOUNTAIN_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(sw.type)==false)
            {
                if (sw.type == "1")
                {
                    sb.Append(" select OBJECTID,BYORGNO,NAME,BYORGNOXS,DISPLAY_X,DISPLAY_Y,Shape,VILLAGE,[TYPE] from   CUNZHUDI    ");
                    sb.AppendFormat(" WHERE   1=1");
                    if (string.IsNullOrEmpty(sw.type) == false)
                        sb.AppendFormat(" AND TYPE = '{0}'", ClsSql.EncodeSql(sw.type));
                    if (string.IsNullOrEmpty(sw.type) == false)
                        sb.AppendFormat(" AND [TYPE] = '{0}'", ClsSql.EncodeSql(sw.type));
                    if (string.IsNullOrEmpty(sw.ORGNO) == false)
                        sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.ORGNO));
                    if (string.IsNullOrEmpty(sw.NAME) == false)
                        sb.AppendFormat(" AND NAME like '%{0}%'", ClsSql.EncodeSql(sw.NAME));
                    if (!string.IsNullOrEmpty(sw.BYORGNO))
                    {
                        if (sw.BYORGNO.Substring(4, 5) == "00000")//获取所有市的
                            sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,4) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
                        else if (sw.BYORGNO.Substring(4, 5) == "xxxxx")//单独市
                            sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,9) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4) + "00000"));
                        else if (sw.BYORGNO.Substring(6, 3) == "xxx")//获取所有县的
                            sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,6) = '{0}' )", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
                        else
                            sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
                    }
                }
                else
                {
                    sb.Append("select OBJECTID,BYORGNO,NAME,BYORGNOXS,DISPLAY_X,DISPLAY_Y,Shape,VILLAGE,[TYPE] from MOUNTAIN  ");
                    sb.AppendFormat(" WHERE   1=1");
                    if (string.IsNullOrEmpty(sw.type) == false)
                        sb.AppendFormat(" AND TYPE = '{0}'", ClsSql.EncodeSql(sw.type));
                    if (string.IsNullOrEmpty(sw.type) == false)
                        sb.AppendFormat(" AND [TYPE] = '{0}'", ClsSql.EncodeSql(sw.type));
                    if (string.IsNullOrEmpty(sw.ORGNO) == false)
                        sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.ORGNO));
                    if (string.IsNullOrEmpty(sw.NAME) == false)
                        sb.AppendFormat(" AND NAME like '%{0}%'", ClsSql.EncodeSql(sw.NAME));
                    if (!string.IsNullOrEmpty(sw.BYORGNO))
                    {
                        if (sw.BYORGNO.Substring(4, 5) == "00000")//获取所有市的
                            sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,4) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
                        else if (sw.BYORGNO.Substring(4, 5) == "xxxxx")//单独市
                            sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,9) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO));
                        else if (sw.BYORGNO.Substring(6, 3) == "xxx")//获取所有县的
                            sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,6) = '{0}' )", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
                        else
                            sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
                    }
                }
            }
            else 
            {
                sb.Append(" select OBJECTID,BYORGNO,NAME,BYORGNOXS,DISPLAY_X,DISPLAY_Y,Shape,VILLAGE,[TYPE] from   CUNZHUDI  ");
                sb.AppendFormat(" WHERE   1=1");
                sb.AppendFormat(" AND [TYPE] = '{0}'", "1");
                if (string.IsNullOrEmpty(sw.ORGNO) == false)
                    sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.ORGNO));
                if (string.IsNullOrEmpty(sw.NAME) == false)
                    sb.AppendFormat(" AND NAME like '%{0}%'", ClsSql.EncodeSql(sw.NAME));
                if (!string.IsNullOrEmpty(sw.BYORGNO))
                {
                    if (sw.BYORGNO.Substring(4, 5) == "00000")//获取所有市的
                        sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,4) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
                    else if (sw.BYORGNO.Substring(4, 5) == "xxxxx")//单独市
                        sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,9) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO));
                    else if (sw.BYORGNO.Substring(6, 3) == "xxx")//获取所有县的
                        sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,6) = '{0}' )", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
                    else
                        sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
                }
                sb.AppendFormat(" union all ");
                sb.Append("select OBJECTID,BYORGNO,NAME,BYORGNOXS,DISPLAY_X,DISPLAY_Y,Shape,VILLAGE,[TYPE] from MOUNTAIN  ");
                sb.AppendFormat(" WHERE   1=1");
                if (string.IsNullOrEmpty(sw.type) == false)
                    sb.AppendFormat(" AND [TYPE] = '{0}'", ClsSql.EncodeSql(sw.type));
                if (string.IsNullOrEmpty(sw.ORGNO) == false)
                    sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.ORGNO));
                if (string.IsNullOrEmpty(sw.NAME) == false)
                    sb.AppendFormat(" AND NAME like '%{0}%'", ClsSql.EncodeSql(sw.NAME));
                if (!string.IsNullOrEmpty(sw.BYORGNO))
                {
                    if (sw.BYORGNO.Substring(4, 5) == "00000")//获取所有市的
                        sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,4) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
                    else if (sw.BYORGNO.Substring(4, 5) == "xxxxx")//单独市
                        sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,9) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO));
                    else if (sw.BYORGNO.Substring(6, 3) == "xxx")//获取所有县的
                        sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,6) = '{0}' )", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
                    else
                        sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
                }
            }
            
           
            string sql = "select OBJECTID,BYORGNO,NAME,BYORGNOXS,DISPLAY_X,DISPLAY_Y,Shape,VILLAGE,[TYPE] from (  "
                + sb.ToString()
                + " ) aa order by BYORGNO,OBJECTID desc";
            string sqlC = "select count(1) from  (" + sb.ToString() + ")ee";
            total = int.Parse(SDEDataBaseClass.ReturnSqlField(sqlC));
            sw.curPage = PagerCls.getCurPage(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
            DataSet ds = SDEDataBaseClass.FullDataSet(sql, (sw.curPage - 1) * sw.pageSize, sw.pageSize, "a");
            return ds.Tables[0];
        }

        #endregion

        #region 统计当前用户下自定义数据的数量
        /// <summary>
        /// 统计当前用户下自定义数据的数量
        /// </summary>
        /// <param name="orgno"></param>
        /// <returns></returns>
        public static string getNum(string orgno)
        {
            string total = "";
            string total1 = "";
            string total2 = "";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("    from    MOUNTAIN a ");
            sb.AppendFormat("where 1 = 1 ");
            if (orgno.Substring(4, 5) == "00000")//获取所有市的
                sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,4) = '{0}' )", ClsSql.EncodeSql(orgno.Substring(0, 4)));
            else if (orgno.Substring(6, 3) == "000")//获取所有县的
                sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,6) = '{0}' )", ClsSql.EncodeSql(orgno.Substring(0, 6)));
            else
                sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(orgno));
            string sqlC = "select count(1) " + sb.ToString();
            total = SDEDataBaseClass.ReturnSqlField(sqlC);
            StringBuilder sb1 = new StringBuilder();
            sb1.AppendFormat("    from    CUNZHUDI a ");
            sb1.AppendFormat("where 1 = 1 ");
            if (orgno.Substring(4, 5) == "00000")//获取所有市的
                sb1.AppendFormat(" AND (SUBSTRING(BYORGNO,1,4) = '{0}')", ClsSql.EncodeSql(orgno.Substring(0, 4)));
            else if (orgno.Substring(6, 3) == "000")//获取所有县的
                sb1.AppendFormat(" AND (SUBSTRING(BYORGNO,1,6) = '{0}')", ClsSql.EncodeSql(orgno.Substring(0, 6)));
            else
                sb1.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(orgno));
            string sql = "select count(1) " + sb1.ToString();
            total1 = SDEDataBaseClass.ReturnSqlField(sql);
            if (total=="")
            {
                total = "0";
            }
            if (total1 == "")
            {
                total1 = "0";
            }
            total2 = (int.Parse(total) + int.Parse(total1)).ToString();
            return total2;
        }
        #endregion
    }
}
