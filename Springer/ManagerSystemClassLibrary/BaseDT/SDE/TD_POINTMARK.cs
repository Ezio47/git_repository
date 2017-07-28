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
    /// 三维-自然村
    /// </summary>
    public class TD_POINTMARK
    {
        /// <summary>
        /// 三维添加
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message Add(TD_POINTMARK_Model m)
        {
            if (PublicCls.OrgIsZhen(m.BYORGNO) == false)
                return new Message(false, "添加失败，所属单位必须为乡镇！", "");
            if (TD_POINTMARK.isExists(new TD_POINTMARK_Model { JD = m.JD, WD = m.WD }) == false)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("insert into CUNZHUDI(NAME,MAPNAME,BYORGNO,BYORGNOXS,TYPE,VILLAGE,DISPLAY_X,DISPLAY_Y,Shape,KIND,TELEPHONE,ADDRESS) values(");
                sb.AppendFormat("{0},", ClsSql.saveNullField(m.NAME));
                sb.AppendFormat("{0},", ClsSql.saveNullField(m.MAPNAME));
                sb.AppendFormat("{0},", ClsSql.saveNullField(m.BYORGNO));
                sb.AppendFormat("{0},", ClsSql.saveNullField(m.BYORGNOXS));
                sb.AppendFormat("{0},", ClsSql.saveNullField(m.TYPE));
                sb.AppendFormat("{0},", ClsSql.saveNullField(m.VILLAGE));
                sb.AppendFormat("{0},", ClsSql.saveNullField(m.JD));
                sb.AppendFormat("{0},", ClsSql.saveNullField(m.WD));
                sb.AppendFormat("{0},", m.Shape);
                sb.AppendFormat("{0},", ClsSql.saveNullField(m.KIND));
                sb.AppendFormat("{0},", ClsSql.saveNullField(m.TELEPHONE));
                sb.AppendFormat("{0})", ClsSql.saveNullField(m.ADDRESS));
                bool bln = SDEDataBaseClass.ExeSql(sb.ToString());
                if (bln == true)
                    return new Message(true, "添加成功！", "");
                else
                    return new Message(false, "添加失败，请检查各输入框是否正确！", "");
            }
            else 
            { 
                return new Message(false, "已有相同地址的自然村,请重新选择地址", ""); 
            }

        }
        /// <summary>
        /// 三维-修改
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message Mdy(TD_POINTMARK_Model m)
        {
            if (PublicCls.OrgIsZhen(m.BYORGNO) == false)
                return new Message(false, "添加失败，所属单位必须为乡镇！", "");
            
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("UPDATE CUNZHUDI");
                sb.AppendFormat(" set ");
                sb.AppendFormat("NAME={0}", ClsSql.saveNullField(m.NAME));
                sb.AppendFormat(",MAPNAME={0}", ClsSql.saveNullField(m.MAPNAME));
                sb.AppendFormat(",BYORGNO={0}", ClsSql.saveNullField(m.BYORGNO));
                sb.AppendFormat(",BYORGNOXS={0}", ClsSql.saveNullField(m.BYORGNOXS));
                sb.AppendFormat(",TYPE={0}", ClsSql.saveNullField(m.TYPE));
                sb.AppendFormat(",VILLAGE={0}", ClsSql.saveNullField(m.VILLAGE));
                sb.AppendFormat(",DISPLAY_X={0}", ClsSql.saveNullField(m.JD));
                sb.AppendFormat(",DISPLAY_Y={0}", ClsSql.saveNullField(m.WD));
                sb.AppendFormat(",KIND={0}", ClsSql.saveNullField(m.KIND));
                sb.AppendFormat(",TELEPHONE={0}", ClsSql.saveNullField(m.TELEPHONE));
                sb.AppendFormat(",ADDRESS={0}", ClsSql.saveNullField(m.ADDRESS));
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
        public static Message Del(TD_POINTMARK_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete CUNZHUDI");
            sb.AppendFormat(" where OBJECTID= '{0}'", ClsSql.EncodeSql(m.OBJECTID));
            bool bln = SDEDataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功！", "");
            else
                return new Message(false, "删除失败，请检查各输入框是否正确！", "");
        }
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static bool isExists(TD_POINTMARK_Model m)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from CUNZHUDI where 1=1");
            if (string.IsNullOrEmpty(m.JD) == false && string.IsNullOrEmpty(m.WD) == false)
                sb.AppendFormat(" and DISPLAY_X='{0}'", ClsSql.EncodeSql(m.JD));
            sb.AppendFormat(" and DISPLAY_Y='{0}'", ClsSql.EncodeSql(m.WD));
            return SDEDataBaseClass.JudgeRecordExists(sb.ToString());
        }

        #region 获取记录
        /// <summary>
        /// 获取记录
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(TD_POINTMARK_SW sw)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat(" FROM      CUNZHUDI");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.OBJECTID) == false)
                sb.AppendFormat(" AND OBJECTID = '{0}'", ClsSql.EncodeSql(sw.OBJECTID));
            if (string.IsNullOrEmpty(sw.ORGNO) == false)
                sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.ORGNO));
            if (string.IsNullOrEmpty(sw.NAME) == false)
                sb.AppendFormat(" AND NAME like '%{0}%'", ClsSql.EncodeSql(sw.NAME));       
            if (!string.IsNullOrEmpty(sw.BYORGNO))
            {
                if (sw.BYORGNO.Substring(4, 5) == "00000")//获取所有市的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,4) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
                else if (sw.BYORGNO.Substring(6, 3) == "000")//获取所有县的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,6) = '{0}' or BYORGNO is null or BYORGNO='')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
                else
                    sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            }
            string sql = "SELECT OBJECTID,NAME,MAPNAME,BYORGNO,BYORGNOXS,TYPE,VILLAGE,DISPLAY_X,DISPLAY_Y,Shape,KIND,TELEPHONE,ADDRESS"
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
        public static DataTable getDT(TD_POINTMARK_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat(" FROM      CUNZHUDI");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.OBJECTID) == false)
                sb.AppendFormat(" AND OBJECTID = '{0}'", ClsSql.EncodeSql(sw.OBJECTID));
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
            string sql = "SELECT OBJECTID,NAME,MAPNAME,BYORGNO,BYORGNOXS,TYPE,VILLAGE,DISPLAY_X,DISPLAY_Y,Shape,KIND,TELEPHONE,ADDRESS"
                + sb.ToString()
                + " order by BYORGNO,OBJECTID desc";
            string sqlC = "select count(1) " + sb.ToString();
            total = int.Parse(SDEDataBaseClass.ReturnSqlField(sqlC));
            sw.curPage = PagerCls.getCurPage(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
            DataSet ds = SDEDataBaseClass.FullDataSet(sql, (sw.curPage - 1) * sw.pageSize, sw.pageSize, "a");
            return ds.Tables[0];
        }

        #endregion
    }
}
