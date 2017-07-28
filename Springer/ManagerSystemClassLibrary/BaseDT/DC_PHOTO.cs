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
    /// 数据中心_照片
    /// </summary>
    public class DC_PHOTO
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(DC_PHOTO_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  DC_PHOTO( PHOTOTITLE, PHOTOFILENAME, PHOTOEXPLAIN, PHOTOTIME, PHOTOTYPE, PRID)");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("{0}", ClsSql.saveNullField(m.PHOTOTITLE));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.PHOTOFILENAME));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.PHOTOEXPLAIN));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.PHOTOTIME));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.PHOTOTYPE));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.PRID));

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
        public static Message Mdy(DC_PHOTO_Model m)
        {
            StringBuilder sb = new StringBuilder();
            //HID, HNAME, SN, PHONE, SEX, BIRTH, ONSTATE, BYORGNO
            sb.AppendFormat("UPDATE DC_PHOTO");
            sb.AppendFormat(" set ");
            //sb.AppendFormat("PHOTO_ID={0}", ClsSql.saveNullField(m.PHOTO_ID));
            sb.AppendFormat("PHOTOTITLE={0}", ClsSql.saveNullField(m.PHOTOTITLE));
            sb.AppendFormat(",PHOTOFILENAME={0}", ClsSql.saveNullField(m.PHOTOFILENAME));
            sb.AppendFormat(",PHOTOEXPLAIN={0}", ClsSql.saveNullField(m.PHOTOEXPLAIN));
            sb.AppendFormat(",PHOTOTIME={0}", ClsSql.saveNullField(m.PHOTOTIME));
            sb.AppendFormat(",PHOTOTYPE={0}", ClsSql.saveNullField(m.PHOTOTYPE));
            sb.AppendFormat(",PRID={0}", ClsSql.saveNullField(m.PRID));
            sb.AppendFormat(" where PHOTO_ID= '{0}'", ClsSql.EncodeSql(m.PHOTO_ID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功！", "");
            else
                return new Message(false, "修改失败，请检查各输入框是否正确！", "");
        }

        #endregion

        #region 修改但不涉及到图片上传
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message MdyTP(DC_PHOTO_Model m)
        {
            StringBuilder sb = new StringBuilder();
            //HID, HNAME, SN, PHONE, SEX, BIRTH, ONSTATE, BYORGNO
            sb.AppendFormat("UPDATE DC_PHOTO");
            sb.AppendFormat(" set ");
            //sb.AppendFormat("PHOTO_ID={0}", ClsSql.saveNullField(m.PHOTO_ID));
            sb.AppendFormat("PHOTOTITLE={0}", ClsSql.saveNullField(m.PHOTOTITLE));
            //sb.AppendFormat(",PHOTOFILENAME={0}", ClsSql.saveNullField(m.PHOTOFILENAME));
            sb.AppendFormat(",PHOTOEXPLAIN={0}", ClsSql.saveNullField(m.PHOTOEXPLAIN));
            sb.AppendFormat(",PHOTOTIME={0}", ClsSql.saveNullField(m.PHOTOTIME));
            sb.AppendFormat(",PHOTOTYPE={0}", ClsSql.saveNullField(m.PHOTOTYPE));
            sb.AppendFormat(",PRID={0}", ClsSql.saveNullField(m.PRID));
            sb.AppendFormat(" where PHOTO_ID= '{0}'", ClsSql.EncodeSql(m.PHOTO_ID));
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
        public static Message Del(DC_PHOTO_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete DC_PHOTO");
            sb.AppendFormat(" where PHOTO_ID= '{0}'", ClsSql.EncodeSql(m.PHOTO_ID));
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
        public static bool isExists(DC_PHOTO_SW sw)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from DC_PHOTO where 1=1");
            if (string.IsNullOrEmpty(sw.PHOTO_ID) == false)
                sb.AppendFormat(" where PHOTO_ID= '{0}'", ClsSql.EncodeSql(sw.PHOTO_ID));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }

        #endregion

        #region 获取记录
        /// <summary>
        /// 获取记录
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(DC_PHOTO_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM      DC_PHOTO");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.PHOTO_ID) == false)
                sb.AppendFormat(" AND PHOTO_ID = '{0}'", ClsSql.EncodeSql(sw.PHOTO_ID));
            if (string.IsNullOrEmpty(sw.PHOTOTYPE) == false)
                sb.AppendFormat(" AND PHOTOTYPE = '{0}'", ClsSql.EncodeSql(sw.PHOTOTYPE));
            if (string.IsNullOrEmpty(sw.PRID) == false)
                sb.AppendFormat(" AND PRID = '{0}'", ClsSql.EncodeSql(sw.PRID));
            string sql = "";
            sql = "SELECT PHOTO_ID, PHOTOTITLE, PHOTOFILENAME, PHOTOEXPLAIN, PHOTOTIME, PHOTOTYPE, PRID"
            + sb.ToString()
            + " order by PHOTO_ID desc";
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
        public static DataTable getDT(DC_PHOTO_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendFormat(" FROM      DC_PHOTO");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.PHOTO_ID) == false)
                sb.AppendFormat(" AND PHOTO_ID = '{0}'", ClsSql.EncodeSql(sw.PHOTO_ID));
            if (string.IsNullOrEmpty(sw.PHOTOTYPE) == false)
                sb.AppendFormat(" AND PHOTOTYPE = '{0}'", ClsSql.EncodeSql(sw.PHOTOTYPE));
            if (string.IsNullOrEmpty(sw.PRID) == false)
                sb.AppendFormat(" AND PRID = '{0}'", ClsSql.EncodeSql(sw.PRID));
            string sql = "SELECT PHOTO_ID, PHOTOTITLE, PHOTOFILENAME, PHOTOEXPLAIN, PHOTOTIME, PHOTOTYPE, PRID"
            + sb.ToString()
            + " order by PHOTO_ID desc";
            string sqlC = "select count(1) " + sb.ToString();
            total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
            sw.curPage = PagerCls.getCurPage(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
            DataSet ds = DataBaseClass.FullDataSet(sql, (sw.curPage - 1) * sw.pageSize, sw.pageSize, "a");
            return ds.Tables[0];
        }

        #endregion
    }
}
