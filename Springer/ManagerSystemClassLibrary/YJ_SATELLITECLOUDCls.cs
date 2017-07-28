using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel;
using DataBaseClassLibrary;
using PublicClassLibrary;

using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
namespace ManagerSystemClassLibrary
{

    /// <summary>
    /// 预警_卫星云图表
    /// </summary>
    public class YJ_SATELLITECLOUDCls
    {
        #region  根据查询条件获取某一条信息记录

        /// <summary>
        /// 根据查询条件获取某一条信息记录
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static YJ_SATELLITECLOUD_Model getModel(YJ_SATELLITECLOUD_SW sw)
        {
            DataTable dt = BaseDT.YJ_SATELLITECLOUD.getDT(sw);
            YJ_SATELLITECLOUD_Model m = new YJ_SATELLITECLOUD_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.CLOUDID = dt.Rows[i]["CLOUDID"].ToString();
                m.CLOUDTIME = ClsSwitch.SwitTM(dt.Rows[i]["CLOUDTIME"].ToString());
                m.CLOUDNAME = dt.Rows[i]["CLOUDNAME"].ToString();
                m.CLOUDFILENAME = dt.Rows[i]["CLOUDFILENAME"].ToString();
            }
            dt.Clear();
            dt.Dispose();
            return m;
        }
        #endregion

        #region 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>

        public static IEnumerable<YJ_SATELLITECLOUD_Model> getListModel(YJ_SATELLITECLOUD_SW sw)
        {
            DataTable dt = BaseDT.YJ_SATELLITECLOUD.getDT(sw);//列表
            var result = new List<YJ_SATELLITECLOUD_Model>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                YJ_SATELLITECLOUD_Model m = new YJ_SATELLITECLOUD_Model();
                m.CLOUDID = dt.Rows[i]["CLOUDID"].ToString();
                m.CLOUDTIME = ClsSwitch.SwitTM(dt.Rows[i]["CLOUDTIME"].ToString());
                m.CLOUDNAME = dt.Rows[i]["CLOUDNAME"].ToString();
                m.CLOUDFILENAME = dt.Rows[i]["CLOUDFILENAME"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }

        #endregion

        #region 获取最新列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw">参见模型 sw.TopCount 默认为10 sw.MANSTATE 默认取所有</param>
        /// <returns>参见模型</returns>

        public static IEnumerable<YJ_SATELLITECLOUD_Model> getListModelTop(YJ_SATELLITECLOUD_SW sw)
        {
            DataTable dt = BaseDT.YJ_SATELLITECLOUD.getTopDT(sw);//列表
            var result = new List<YJ_SATELLITECLOUD_Model>();
            string path = ConfigCls.getConfigValue("WxImagesPath");//压缩卫星云图
            string orginalpath = ConfigCls.getConfigValue("WxImagesOriginalPath");//原始卫星云图
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                YJ_SATELLITECLOUD_Model m = new YJ_SATELLITECLOUD_Model();
                m.CLOUDID = dt.Rows[i]["CLOUDID"].ToString();
                m.CLOUDTIME = ClsSwitch.SwitTM(dt.Rows[i]["CLOUDTIME"].ToString());
                m.CLOUDNAME = dt.Rows[i]["CLOUDNAME"].ToString();
                m.CLOUDFILENAME = path + dt.Rows[i]["CLOUDFILENAME"].ToString();
                m.CLOUDORIGIONNAME = orginalpath + dt.Rows[i]["CLOUDORIGIONNAME"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }

        #endregion
    }
}
