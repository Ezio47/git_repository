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
    /// 预警_气象信息表
    /// </summary>
    public class YJ_WEATHERCls
    {

        #region 基本信息增、删、改

        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(YJ_WEATHER_Model m)
        {
            if (m.opMethod == "Add")
            {
                SystemCls.LogSave("3", "气象信息:" + m.WEATHERID, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.YJ_WEATHER.Add(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            //if (m.opMethod == "Mdy")
            //{
            //    SystemCls.LogSave("4", "气象信息:" + m.EMNAME, ClsStr.getModelContent(m));
            //    Message msgUser = BaseDT.YJ_WEATHER.Mdy(m);
            //    return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);

            //}
            //if (m.opMethod == "Del")
            //{
            //    SystemCls.LogSave("5", "气象信息:" + m.EMNAME, ClsStr.getModelContent(m));
            //    Message msgUser = BaseDT.YJ_WEATHER.Del(m);
            //    return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            //}
            return new Message(false, "无效操作", "");


        }
        #endregion


        #region 获取单体气象信息
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>

        public static YJ_WEATHER_Model getModel(YJ_WEATHER_SW sw)
        {
            DataTable dt = BaseDT.YJ_WEATHER.getDT(sw);//列表
            DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { });
            YJ_WEATHER_Model m = new YJ_WEATHER_Model();
            if(dt.Rows.Count>0)
            {
                int i = 0;
                m.WEATHERID = dt.Rows[i]["WEATHERID"].ToString();
                m.WEATHERDATE = ClsSwitch.SwitTM(dt.Rows[i]["WEATHERDATE"].ToString());
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.TOWNNAME = dt.Rows[i]["TOWNNAME"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.P = dt.Rows[i]["P"].ToString();
                m.T = dt.Rows[i]["T"].ToString();
                m.W = dt.Rows[i]["W"].ToString();
                m.F = dt.Rows[i]["F"].ToString();
                m.TCUR = dt.Rows[i]["TCUR"].ToString();
                m.THIGH = dt.Rows[i]["THIGH"].ToString();
                m.TLOWER = dt.Rows[i]["TLOWER"].ToString();
                m.orgName = BaseDT.T_SYS_ORG.getName(dtOrg, m.BYORGNO);
            }
            dt.Clear();
            dt.Dispose();
            dtOrg.Clear();
            dtOrg.Dispose();
            return m;
        }

        #endregion
        
        #region 获取最新列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>

        public static IEnumerable<YJ_WEATHER_Model> getNewListModel(YJ_WEATHER_SW sw)
        {
            DataTable dt = BaseDT.YJ_WEATHER.getNewDT(sw);//列表
            var result = new List<YJ_WEATHER_Model>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                YJ_WEATHER_Model m = new YJ_WEATHER_Model();
                m.WEATHERID = dt.Rows[i]["WEATHERID"].ToString();
                m.WEATHERDATE = ClsSwitch.SwitTM(dt.Rows[i]["WEATHERDATE"].ToString());
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.TOWNNAME = dt.Rows[i]["TOWNNAME"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.P = dt.Rows[i]["P"].ToString();
                m.T = dt.Rows[i]["T"].ToString();
                m.W = dt.Rows[i]["W"].ToString();
                m.F = dt.Rows[i]["F"].ToString();
                m.TCUR = dt.Rows[i]["TCUR"].ToString();
                m.THIGH = dt.Rows[i]["THIGH"].ToString();
                m.TLOWER = dt.Rows[i]["TLOWER"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }

        #endregion

        #region 根据当前单位获取气象信息
        /// <summary>
        /// 根据当前单位获取气象信息
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>返回气象信息</returns>
        public static string getWeather(YJ_WEATHER_SW sw)
        {
            if (string.IsNullOrEmpty(sw.BYORGNO))
                sw.BYORGNO = SystemCls.getCurUserOrgNo();
            YJ_WEATHER_Model wm = YJ_WEATHERCls.getModel(new YJ_WEATHER_SW { BYORGNO = sw.BYORGNO });
            
            string str;
            if (string.IsNullOrEmpty(wm.WEATHERDATE)==false)// (!string.IsNullOrEmpty(wm.THIGH) && !string.IsNullOrEmpty(wm.THIGH) && !string.IsNullOrEmpty(wm.THIGH))
                str ="["+PublicClassLibrary.ClsSwitch.SwitDate( wm.WEATHERDATE)+ " "+wm.orgName+"] 最高气温" + wm.THIGH + "℃,最低气温" + wm.TLOWER + "℃,雨量" + wm.P + "mm";
            else
                str = "暂无气象信息";
            return str;
        }
        #endregion
    }
}
