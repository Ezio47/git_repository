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
    /// 预警监测-红外相机 采信图片
    /// </summary>
    public class JC_INFRAREDCAMERA_PHOTO
    {
        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(JC_INFRAREDCAMERA_PHOTO_Model m)
        {
            DataBaseClass.ExeSql("delete tb_rcvmmsfiles where mmsfilesid in(select mmsfilesid from tb_rcvtmp where smid in(" + m.smid + "))");
            DataBaseClass.ExeSql("delete  from tb_rcvtmp where smid in(" + m.smid + ")");
            bool bln = true;// DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功！", "");
            else
                return new Message(false, "删除失败，请检查各输入框是否正确！", "");
        }
        #endregion

        #region 管理
        /// <summary>
        /// 管理
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Man(JC_INFRAREDCAMERA_PHOTO_Model m)
        {
            if (string.IsNullOrEmpty(m.MANTIME))
                m.MANTIME = ClsSwitch.SwitTM(DateTime.Now);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE tb_rcvtmp");
            sb.AppendFormat(" set ");
            sb.AppendFormat("MANSTATE='{0}'", ClsSql.EncodeSql(m.MANSTATE));
            sb.AppendFormat(",MANRESULT='{0}'", ClsSql.EncodeSql(m.MANRESULT));
            sb.AppendFormat(",MANTIME='{0}'", ClsSql.EncodeSql(m.MANTIME));
            sb.AppendFormat(",MANUSERID='{0}'", ClsSql.EncodeSql(m.MANUSERID));
            sb.AppendFormat(" where smid= '{0}'", ClsSql.EncodeSql(m.smid));

            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功！", "");
            else
                return new Message(false, "修改失败，请检查各输入框是否正确！", "");
        }

        #endregion

        #region 获取数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getDT(JC_INFRAREDCAMERA_PHOTO_SW sw)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat(" SELECT   a.smid, a.tpa, a.recvdatetime, a.mmsfilesid, a.MANSTATE, a.MANRESULT, a.MANTIME, a.MANUSERID, b.filetype,   b.filename");
            sb.AppendFormat(" FROM      tb_rcvtmp AS a LEFT OUTER JOIN tb_rcvmmsfiles AS b ON a.mmsfilesid = b.mmsfilesid");
            sb.AppendFormat(" WHERE   (b.filetype = 'IMG')");

            if (string.IsNullOrEmpty(sw.smid) == false)
                sb.AppendFormat(" AND smid = '{0}'", ClsSql.EncodeSql(sw.smid));
            if (string.IsNullOrEmpty(sw.tpa) == false)
                sb.AppendFormat(" AND tpa = '{0}'", ClsSql.EncodeSql(sw.tpa));

            if (!string.IsNullOrEmpty(sw.DateBegin))
            {
                sb.AppendFormat(" AND recvdatetime>='{0} 00:00:00'", sw.DateBegin);
            }
            if (!string.IsNullOrEmpty(sw.DateEnd))
            {
                sb.AppendFormat(" AND recvdatetime<='{0} 23:59:59'", sw.DateEnd);
            }
            if (string.IsNullOrEmpty(sw.MANSTATE) == false)
                sb.AppendFormat(" AND MANSTATE = '{0}'", ClsSql.EncodeSql(sw.MANSTATE));
            string sql = sb.ToString()
                + " order by recvdatetime DESC";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }

        #endregion

        #region 获取数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getNewDT(JC_INFRAREDCAMERA_PHOTO_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" SELECT  JC_INFRAREDCAMERA_PHOTOID,INFRAREDCAMERAID,PHOTOTIME,PHOTOTITLE");
            sb.AppendFormat(" FROM    JC_INFRAREDCAMERA_PHOTO");
            sb.AppendFormat(" WHERE  1=1 ");
            if (string.IsNullOrEmpty(sw.INFRAREDCAMERAID) == false)
                sb.AppendFormat(" AND INFRAREDCAMERAID = '{0}'", ClsSql.EncodeSql(sw.INFRAREDCAMERAID));
            string sql = sb.ToString()
                + " order by PHOTOTIME DESC";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }

        #endregion

        #region 获取最新数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getTopDT(JC_INFRAREDCAMERA_PHOTO_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(sw.TopCount))//获取最新记录个数
                sw.TopCount = "10";//默认10条
            sb.AppendFormat(" SELECT top {0}  a.smid, a.tpa, a.recvdatetime, a.mmsfilesid, a.MANSTATE, a.MANRESULT, a.MANTIME, a.MANUSERID, b.filetype,   b.filename",sw.TopCount);
            sb.AppendFormat(" FROM      tb_rcvtmp AS a LEFT OUTER JOIN tb_rcvmmsfiles AS b ON a.mmsfilesid = b.mmsfilesid");
            sb.AppendFormat(" WHERE   (b.filetype = 'IMG')");

            if (string.IsNullOrEmpty(sw.MANSTATE) == false)
                sb.AppendFormat(" AND MANSTATE = '{0}'", ClsSql.EncodeSql(sw.MANSTATE));
            string sql = sb.ToString()
                + " order by recvdatetime DESC";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }

        #endregion


        #region 获取分页数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getDT(JC_INFRAREDCAMERA_PHOTO_SW sw, out int total)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat(" FROM      tb_rcvtmp AS a LEFT OUTER JOIN tb_rcvmmsfiles AS b ON a.mmsfilesid = b.mmsfilesid");
            sb.AppendFormat(" WHERE   (b.filetype = 'IMG')");

            if (string.IsNullOrEmpty(sw.smid) == false)
                sb.AppendFormat(" AND smid = '{0}'", ClsSql.EncodeSql(sw.smid));
            if (string.IsNullOrEmpty(sw.tpa) == false)
                sb.AppendFormat(" AND tpa like '%{0}%'", ClsSql.EncodeSql(sw.tpa));

            if (!string.IsNullOrEmpty(sw.DateBegin))
            {
                sb.AppendFormat(" AND recvdatetime>='{0} 00:00:00'", sw.DateBegin);
            }
            if (!string.IsNullOrEmpty(sw.DateEnd))
            {
                sb.AppendFormat(" AND recvdatetime<='{0} 23:59:59'", sw.DateEnd);
            }
            if (string.IsNullOrEmpty(sw.MANSTATE) == false)
                sb.AppendFormat(" AND MANSTATE = '{0}'", ClsSql.EncodeSql(sw.MANSTATE));
           

            string sql = "SELECT   a.smid, a.tpa, a.recvdatetime, a.mmsfilesid, a.MANSTATE, a.MANRESULT, a.MANTIME, a.MANUSERID, b.filetype,   b.filename"
                + sb.ToString()
                + " order by recvdatetime DESC";
            string sqlC = "select count(1) " + sb.ToString();
            total = int.Parse(DataBaseClass.ReturnSqlField(sqlC));
            sw.curPage = PagerCls.getCurPage(new PagerSW { curPage = sw.curPage, pageSize = sw.pageSize, rowCount = total });
            DataSet ds = DataBaseClass.FullDataSet(sql, (sw.curPage - 1) * sw.pageSize, sw.pageSize, "a");
            return ds.Tables[0];
        }

        #endregion
        
    }
}
