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
    /// 火灾级别预案表
    /// </summary>
    public class JC_FIRE_PLAN
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(JC_FIRE_PLAN_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  JC_FIRE_PLAN(BYORGNO, FIRELEVEL, PLANTITLE, PLANCONTENT, PLANFILENAME)");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.BYORGNO));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.FIRELEVEL));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.PLANTITLE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.PLANCONTENT));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.PLANFILENAME));
            sb.AppendFormat(")");
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "添加成功！", "");
            else
                return new Message(false, "添加失败，请检查各输入框是否正确！", "");
        }

        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Mdy(JC_FIRE_PLAN_Model m)
        {
            StringBuilder sb = new StringBuilder();
            //HID, HNAME, SN, PHONE, SEX, BIRTH, ONSTATE, BYORGNO
            sb.AppendFormat("UPDATE JC_FIRE_PLAN");
            sb.AppendFormat(" set ");
            sb.AppendFormat("BYORGNO='{0}'", ClsSql.EncodeSql(m.BYORGNO));
            sb.AppendFormat(",FIRELEVEL='{0}'", ClsSql.EncodeSql(m.FIRELEVEL));
            sb.AppendFormat(",PLANTITLE='{0}'", ClsSql.EncodeSql(m.PLANTITLE));
            sb.AppendFormat(",PLANCONTENT='{0}'", ClsSql.EncodeSql(m.PLANCONTENT));
            sb.AppendFormat(",PLANFILENAME='{0}'", ClsSql.EncodeSql(m.PLANFILENAME));
            sb.AppendFormat(" where JC_FIRE_PLANID= '{0}'", ClsSql.EncodeSql(m.JC_FIRE_PLANID));

            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功！", "");
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
        public static Message Del(JC_FIRE_PLAN_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete JC_FIRE_PLAN");
            sb.AppendFormat(" where JC_FIRE_PLANID= '{0}'", ClsSql.EncodeSql(m.JC_FIRE_PLANID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功！", "");
            else
                return new Message(false, "删除失败，请检查各输入框是否正确！", "");
        }

        #endregion

        #region 判断记录是否存在
        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>true存在 false不存在</returns>
        public static bool isExists(JC_FIRE_PLAN_SW sw)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from JC_FIRE_PLAN where 1=1");
            if (string.IsNullOrEmpty(sw.JC_FIRE_PLANID) == false)
                sb.AppendFormat(" where JC_FIRE_PLANID= '{0}'", ClsSql.EncodeSql(sw.JC_FIRE_PLANID));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }

        #endregion

        #region 获取记录
        /// <summary>
        /// 获取记录
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(JC_FIRE_PLAN_SW sw)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat(" FROM      JC_FIRE_PLAN");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.JC_FIRE_PLANID) == false)
                sb.AppendFormat(" AND JC_FIRE_PLANID = '{0}'", ClsSql.EncodeSql(sw.JC_FIRE_PLANID));
            if (string.IsNullOrEmpty(sw.FIRELEVEL) == false)//火险等级
                sb.AppendFormat(" AND FIRELEVEL = '{0}'", ClsSql.EncodeSql(sw.FIRELEVEL));
            if (string.IsNullOrEmpty(sw.PLANTITLE) == false)
                sb.AppendFormat(" AND PLANTITLE like '%{0}%'", ClsSql.EncodeSql(sw.PLANTITLE));
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
            if (string.IsNullOrEmpty(sw.searchOrgNo) == false)//根据当前单位编码，反查上级单位预案,主要用于调度指挥
            {

                if (PublicCls.OrgIsShi(sw.searchOrgNo))
                {
                    sb.AppendFormat(" and BYORGNO = '{0}'", PublicCls.getShiIncOrgNo(sw.searchOrgNo)+"00000");
                }
                else if (PublicCls.OrgIsXian(sw.searchOrgNo))
                {
                    sb.AppendFormat(" and BYORGNO in  ('{0}','{1}')", PublicCls.getXianIncOrgNo(sw.searchOrgNo) + "000", PublicCls.getShiIncOrgNo(sw.searchOrgNo) + "00000");
                }
                else
                {
                    sb.AppendFormat(" and BYORGNO in  ('{0}','{1}','{2}')", PublicCls.getXianIncOrgNo(sw.searchOrgNo) + "000", PublicCls.getShiIncOrgNo(sw.searchOrgNo) + "00000",sw.searchOrgNo);
                }
            }
            string sql = "SELECT JC_FIRE_PLANID, BYORGNO, FIRELEVEL, PLANTITLE, PLANCONTENT, PLANFILENAME"
                + sb.ToString()
                + " order by BYORGNO DESC, FIRELEVEL";

            DataSet ds = DataBaseClass.FullDataSet(sql);
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
        public static DataTable getDT(JC_FIRE_PLAN_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendFormat(" FROM      JC_FIRE_PLAN");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.JC_FIRE_PLANID) == false)
                sb.AppendFormat(" AND JC_FIRE_PLANID = '{0}'", ClsSql.EncodeSql(sw.JC_FIRE_PLANID));
            if (string.IsNullOrEmpty(sw.FIRELEVEL) == false)
                sb.AppendFormat(" AND FIRELEVEL = '{0}'", ClsSql.EncodeSql(sw.FIRELEVEL));
            if (string.IsNullOrEmpty(sw.PLANTITLE) == false)
                sb.AppendFormat(" AND PLANTITLE like '%{0}%'", ClsSql.EncodeSql(sw.PLANTITLE));
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

            string sql = "SELECT JC_FIRE_PLANID, BYORGNO, FIRELEVEL, PLANTITLE, PLANCONTENT, PLANFILENAME"
                + sb.ToString()
                + " order by BYORGNO  ,FIRELEVEL";
            string sqlC = "select count(1) " + sb.ToString();
            total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
            sw.curPage = PagerCls.getCurPage(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
            DataSet ds = DataBaseClass.FullDataSet(sql, (sw.curPage - 1) * sw.pageSize, sw.pageSize, "a");
            return ds.Tables[0];
        }

        #endregion
    }
}
