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
    /// 预警_火险等级表
    /// </summary>
    public class YJ_DANGERCLASSCls
    {
        #region 基本信息增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(YJ_DANGERCLASS_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msgUser = BaseDT.YJ_DANGERCLASS.Add(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            else if (m.opMethod == "PLAdd")
            {
                Message msgUser = BaseDT.YJ_DANGERCLASS.PLAdd(m);//保存更新二维
                Message msg = BaseDT.YJ_DANGERCLASS.UpdateAceHuoXianDengJiByXian(m);//更新三维库
                return new Message(msg.Success, msg.Msg, m.returnUrl);
            }
            else if (m.opMethod == "PLAdd2") 
            {
                Message msgUser = BaseDT.YJ_DANGERCLASS.PLAdd2(m);//保存更新二维
                Message msg = BaseDT.YJ_DANGERCLASS.UpdateAceHuoXianDengJiByQHCODE(m);//更新三维库
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
            }
            return new Message(false, "无效操作", "");
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message ExportData(YJ_DANGERCLASS_Model m)
        {
            return BaseDT.YJ_DANGERCLASS.AddExport(m);
        }

        /// <summary>
        /// 删除火险等级
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message RemoveDataLevelClass(YJ_DANGERCLASS_Model m)
        {
            return BaseDT.YJ_DANGERCLASS.DeleteByDCDATE(m);
        }

        /// <summary>
        /// 获取火险等级列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<YJ_DANGERCLASS_Model> getListModel(YJ_DANGERCLASS_SW sw)
        {
            DataTable dt = BaseDT.YJ_DANGERCLASS.getDT(sw);//列表
            var result = new List<YJ_DANGERCLASS_Model>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                YJ_DANGERCLASS_Model m = new YJ_DANGERCLASS_Model();
                m.DANGERID = dt.Rows[i]["DANGERID"].ToString();
                m.DCDATE = ClsSwitch.SwitTM(dt.Rows[i]["DCDATE"].ToString());
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.TOWNNAME = dt.Rows[i]["TOWNNAME"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.DANGERCLASS = dt.Rows[i]["DANGERCLASS"].ToString();
                m.TOPTOWNNAME = dt.Rows[i]["TOPTOWNNAME"].ToString();
                m.WEATHER = dt.Rows[i]["WEATHER"].ToString();
                m.TEMPREATURE = dt.Rows[i]["TEMPREATURE"].ToString();
                m.WINDYSPEED = dt.Rows[i]["WINDYSPEED"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }

        #endregion

        #region 获取单条记录
        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <param name="sw">参见条件模型T_ALL_AREA_SW</param>
        /// <returns>参见模型T_ALL_AREA_Model</returns>
        public static YJ_DANGERCLASS_Model getModel(YJ_DANGERCLASS_SW sw)
        {
            DataTable dt = BaseDT.YJ_DANGERCLASS.getTopDT(sw);
            YJ_DANGERCLASS_Model m = new YJ_DANGERCLASS_Model();
            if (dt.Rows.Count > 0)
            {
                m.DANGERID = dt.Rows[0]["DANGERID"].ToString();
                m.DCDATE = dt.Rows[0]["DCDATE"].ToString();
                m.BYORGNO = dt.Rows[0]["BYORGNO"].ToString();
                m.TOWNNAME = dt.Rows[0]["TOWNNAME"].ToString();
                m.JD = dt.Rows[0]["JD"].ToString();
                m.WD = dt.Rows[0]["WD"].ToString();
                m.DANGERCLASS = dt.Rows[0]["DANGERCLASS"].ToString() == "0" ? "" : dt.Rows[0]["DANGERCLASS"].ToString();
                m.TOPTOWNNAME = dt.Rows[0]["TOPTOWNNAME"].ToString();
                m.WEATHER = dt.Rows[0]["WEATHER"].ToString();
                m.TEMPREATURE = dt.Rows[0]["TEMPREATURE"].ToString();
                m.WINDYSPEED = dt.Rows[0]["WINDYSPEED"].ToString();
            }
            //m.WEATHER =m.WEATHER!= null ? m.WEATHER :"";
            //m.TEMPREATURE = m.TEMPREATURE != null ? m.TEMPREATURE : "";
            //m.WINDYSPEED = m.WINDYSPEED != null ? m.WINDYSPEED : "";
            //m.DANGERCLASS = m.DANGERCLASS != null ? m.DANGERCLASS : "";
            return m;
        }

        #endregion

        #region 获取最新列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw">参见模型 </param>
        /// <returns>参见模型</returns>
        public static IEnumerable<YJ_DANGERCLASS_Model> getListModelTop(YJ_DANGERCLASS_SW sw)
        {
            DataTable dt = BaseDT.YJ_DANGERCLASS.getNewDT(sw);//列表
            var result = new List<YJ_DANGERCLASS_Model>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                YJ_DANGERCLASS_Model m = new YJ_DANGERCLASS_Model();
                m.DANGERID = dt.Rows[i]["DANGERID"].ToString();
                m.DCDATE = ClsSwitch.SwitTM(dt.Rows[i]["DCDATE"].ToString());
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.TOWNNAME = dt.Rows[i]["TOWNNAME"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.DANGERCLASS = dt.Rows[i]["DANGERCLASS"].ToString();
                m.TOPTOWNNAME = dt.Rows[i]["TOPTOWNNAME"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }

        #endregion

        #region 获取某一区域最新数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw">参见模型 </param>
        /// <returns>参见模型</returns>
        public static IEnumerable<YJ_DANGERCLASS_Model> getListModelArea(YJ_DANGERCLASS_SW sw)
        {
            DataTable dt = BaseDT.YJ_DANGERCLASS.getNewArea(sw);//列表
            var result = new List<YJ_DANGERCLASS_Model>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                YJ_DANGERCLASS_Model m = new YJ_DANGERCLASS_Model();
                m.DANGERID = dt.Rows[i]["DANGERID"].ToString();
                m.DCDATE = ClsSwitch.SwitTM(dt.Rows[i]["DCDATE"].ToString());
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.TOWNNAME = dt.Rows[i]["TOWNNAME"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.DANGERCLASS = dt.Rows[i]["DANGERCLASS"].ToString();
                m.TOPTOWNNAME = dt.Rows[i]["TOPTOWNNAME"].ToString();
                m.WEATHER = dt.Rows[i]["WEATHER"].ToString();
                m.TEMPREATURE = dt.Rows[i]["TEMPREATURE"].ToString();
                m.WINDYSPEED = dt.Rows[i]["WINDYSPEED"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }

        #endregion

        #region 更新空间火险等级数据
        /// <summary>
        /// 更新空间火险等级数据
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message UpdateAceHuoXianDengJiData(YJ_DANGERCLASS_Model m)
        {
            return BaseDT.YJ_DANGERCLASS.UpdateAceHuoXianDengJi(m);
        }
        #endregion

        #region 判断记录是否存在
        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>true存在 false不存在</returns>
        public static bool isExists(YJ_DANGERCLASS_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from YJ_DANGERCLASS where 1=1");
            sb.AppendFormat(" and DCDATE='{0}'", ClsSql.EncodeSql(sw.DCDATE));
            sb.AppendFormat(" and TOWNNAME='{0}'", ClsSql.EncodeSql(sw.TOWNNAME));
            sb.AppendFormat(" and BYORGNO='{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }

        #endregion

        #region 根据单位编码获取火险等级
        /// <summary>
        /// 根据单位编码获取火险等级
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>火险等级</returns>
         public static string getLevelByOrgNo(YJ_DANGERCLASS_SW sw)
        {
            var DANGERCLASS = "";
            if (!string.IsNullOrEmpty(sw.BYORGNO))
            {
                DANGERCLASS = YJ_DANGERCLASSCls.getModel(new YJ_DANGERCLASS_SW { BYORGNO = sw.BYORGNO }).DANGERCLASS;
                if (string.IsNullOrEmpty(DANGERCLASS))
                {
                    sw.BYORGNO = sw.BYORGNO.Substring(0, 6) + "000000000";//如果乡镇没有,查市的 
                    DANGERCLASS = YJ_DANGERCLASSCls.getModel(new YJ_DANGERCLASS_SW { BYORGNO = sw.BYORGNO }).DANGERCLASS;
                    if (string.IsNullOrEmpty(DANGERCLASS))
                    {
                        //BYORGNO = "532503000";//如果市没有,查州的
                        sw.BYORGNO = ConfigCls.getProvincialCapital();// ConfigurationManager.AppSettings["ProvincialCapital"].ToString();//如果市没有,查州的
                        DANGERCLASS = YJ_DANGERCLASSCls.getModel(new YJ_DANGERCLASS_SW { BYORGNO = sw.BYORGNO }).DANGERCLASS;
                        if (string.IsNullOrEmpty(DANGERCLASS))
                        {
                            DANGERCLASS = "";
                            return DANGERCLASS;
                        }
                    }
                }
            }

            return DANGERCLASS;
        }
        #endregion
    }
}
