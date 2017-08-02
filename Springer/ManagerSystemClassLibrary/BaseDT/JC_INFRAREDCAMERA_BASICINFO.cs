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
    /// 预警监测-红外相机
    /// </summary>
    public class JC_INFRAREDCAMERA_BASICINFO
    {
        #region 增加

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(JC_INFRAREDCAMERA_BASICINFO_Model m)
        {
            //if (PublicCls.OrgIsZhen(m.BYORGNO) == false)
            //    return new Message(false, "添加失败，所属单位必须为乡镇！", "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  JC_INFRAREDCAMERA_BASICINFO(BYORGNO,INFRAREDCAMERANAME,PHONE,JD,WD,GC,ADDRESS)");
            sb.AppendFormat("output inserted.INFRAREDCAMERAID ");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.BYORGNO));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.INFRAREDCAMERANAME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.PHONE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.JD));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.WD));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.GC));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ADDRESS));
            sb.AppendFormat(")");
            string strid = DataBaseClass.ReturnSqlField(sb.ToString());
            if(!string.IsNullOrEmpty(strid))
                return new Message(true, "添加成功！", strid);
            else
                return new Message(false, "添加失败，请检查各输入框是否正确！", "");
        }

        /// <summary>
        /// 添加三维库
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <param name="emid">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message AddHONGWAIXIANGJI(JC_INFRAREDCAMERA_BASICINFO_Model m, string emid)
        {
            //if (PublicCls.OrgIsZhen(m.BYORGNO) == false)
            //    return new Message(false, "添加失败，所属单位必须为乡镇！", "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("insert into HONGWAIXIANGJI(OBJECTID,NAME,JD,WD,Shape,ADDRESS) values(");
            sb.AppendFormat("{0},", ClsSql.saveNullField(emid));
            sb.AppendFormat("{0},", ClsSql.saveNullField(m.INFRAREDCAMERANAME));
            sb.AppendFormat("{0},", ClsSql.saveNullField(m.JD));
            sb.AppendFormat("{0},", ClsSql.saveNullField(m.WD));
            sb.AppendFormat("{0},", m.Shape);
            sb.AppendFormat("{0})", ClsSql.saveNullField(m.ADDRESS));

            bool bln = SDEDataBaseClass.ExeSql(sb.ToString());
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
        public static Message Mdy(JC_INFRAREDCAMERA_BASICINFO_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE JC_INFRAREDCAMERA_BASICINFO");
            sb.AppendFormat(" set ");
            sb.AppendFormat(" BYORGNO='{0}'", ClsSql.EncodeSql(m.BYORGNO));
            sb.AppendFormat(",INFRAREDCAMERANAME='{0}'", ClsSql.EncodeSql(m.INFRAREDCAMERANAME));
            sb.AppendFormat(" ,PHONE='{0}'", ClsSql.EncodeSql(m.PHONE));
            sb.AppendFormat(",JD='{0}'", ClsSql.EncodeSql(m.JD));
            sb.AppendFormat(",WD='{0}'", ClsSql.EncodeSql(m.WD));
            sb.AppendFormat(",GC='{0}'", ClsSql.EncodeSql(m.GC));
            sb.AppendFormat(",ADDRESS='{0}'", ClsSql.EncodeSql(m.ADDRESS));
            sb.AppendFormat(" where INFRAREDCAMERAID= '{0}'", ClsSql.EncodeSql(m.INFRAREDCAMERAID));

            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功！", "");
            else
                return new Message(false, "修改失败，请检查各输入框是否正确！", "");
        }

        /// <summary>
        /// 修改空间库
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message MdyHONGWAIXIANGJI(JC_INFRAREDCAMERA_BASICINFO_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE HONGWAIXIANGJI");
            sb.AppendFormat(" set ");
            sb.AppendFormat("NAME={0}", ClsSql.saveNullField(m.INFRAREDCAMERANAME));
            sb.AppendFormat(",JD={0}", ClsSql.saveNullField(m.JD));
            sb.AppendFormat(",WD={0}", ClsSql.saveNullField(m.WD));
            sb.AppendFormat(",ADDRESS={0}", ClsSql.saveNullField(m.ADDRESS));
            sb.AppendFormat(",Shape={0}", m.Shape);
            sb.AppendFormat(" where OBJECTID= {0}", ClsSql.saveNullField(m.INFRAREDCAMERAID));
            bool bln = SDEDataBaseClass.ExeSql(sb.ToString());
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
        public static Message Del(JC_INFRAREDCAMERA_BASICINFO_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete JC_INFRAREDCAMERA_BASICINFO");
            sb.AppendFormat(" where INFRAREDCAMERAID= '{0}'", ClsSql.EncodeSql(m.INFRAREDCAMERAID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功！", "");
            else
                return new Message(false, "删除失败，请检查各输入框是否正确！", "");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message DelHONGWAIXIANGJI(JC_INFRAREDCAMERA_BASICINFO_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete HONGWAIXIANGJI");
            sb.AppendFormat(" where OBJECTID= '{0}'", ClsSql.EncodeSql(m.INFRAREDCAMERAID));
            bool bln = SDEDataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功！", "");
            else
                return new Message(false, "删除失败，请检查各输入框是否正确！", "");
        }

        #endregion

        #region 获取数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getDT(JC_INFRAREDCAMERA_BASICINFO_SW sw)
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendFormat(" FROM      JC_INFRAREDCAMERA_BASICINFO a");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.INFRAREDCAMERAID) == false)
                sb.AppendFormat(" AND INFRAREDCAMERAID = '{0}'", ClsSql.EncodeSql(sw.INFRAREDCAMERAID));
            if (string.IsNullOrEmpty(sw.PHONE) == false)
                sb.AppendFormat(" AND PHONE = '{0}'", ClsSql.EncodeSql(sw.PHONE)); 
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
                else if (PublicCls.OrgIsZhen(sw.BYORGNO))
                {
                    sb.AppendFormat(" and BYORGNO like '{0}%'", PublicCls.getZhenIncOrgNo(sw.BYORGNO));
                }
                else
                {
                    sb.AppendFormat(" and BYORGNO = '{0}'", sw.BYORGNO);
                }
            }
            string sql = "SELECT    INFRAREDCAMERAID,BYORGNO,INFRAREDCAMERANAME,PHONE,JD,WD,GC,ADDRESS"
                + sb.ToString()
                + " order by BYORGNO";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }

        #endregion

        #region 获取分页数据
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getDT(JC_INFRAREDCAMERA_BASICINFO_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendFormat(" FROM      JC_INFRAREDCAMERA_BASICINFO a");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.INFRAREDCAMERAID) == false)
                sb.AppendFormat(" AND INFRAREDCAMERAID = '{0}'", ClsSql.EncodeSql(sw.INFRAREDCAMERAID));
            if (string.IsNullOrEmpty(sw.PHONE) == false)
                sb.AppendFormat(" AND PHONE = '{0}'", ClsSql.EncodeSql(sw.PHONE));
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
                else if (PublicCls.OrgIsZhen(sw.BYORGNO))
                {
                    sb.AppendFormat(" and BYORGNO like '{0}%'", PublicCls.getZhenIncOrgNo(sw.BYORGNO));
                }
                else
                {
                    sb.AppendFormat(" and BYORGNO = '{0}'", sw.BYORGNO);
                }
            }
            string sql = "SELECT    INFRAREDCAMERAID,BYORGNO,INFRAREDCAMERANAME,PHONE,JD,WD,GC,ADDRESS"
                + sb.ToString()
                + " order by BYORGNO";
            string sqlC = "select count(1) " + sb.ToString();
            total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
            sw.curPage = PagerCls.getCurPage(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
            DataSet ds = DataBaseClass.FullDataSet(sql, (sw.curPage - 1) * sw.pageSize, sw.pageSize, "a");
            return ds.Tables[0];
        }

        #endregion
    }
}
