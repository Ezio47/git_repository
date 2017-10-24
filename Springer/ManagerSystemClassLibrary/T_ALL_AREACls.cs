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
using ManagerSystemClassLibrary.BaseDT;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 行政区划管理
    /// </summary>
    public class T_ALL_AREACls
    {
        #region 增删改
        /// <summary>
        /// 增删改
        /// </summary>
        /// <param name="m">参见模型T_ALL_AREA_Model</param>
        /// <returns>参见模型Message</returns>
        public static Message Manager(T_ALL_AREA_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msgArea = BaseDT.T_ALL_AREA.Add(m);
                if (msgArea.Success == false)
                    return new Message(msgArea.Success, msgArea.Msg, "");
                return new Message(msgArea.Success, msgArea.Msg, msgArea.Url);
            }
            if (m.opMethod == "Mdy")
            {
                Message msgArea = BaseDT.T_ALL_AREA.Mdy(m);
                return new Message(msgArea.Success, msgArea.Msg, msgArea.Url);
            }
            if (m.opMethod == "Del")
            {
                return BaseDT.T_ALL_AREA.Del(m);
            }
            return new Message(false, "无效操作", m.returnUrl);
        }

        #endregion

        #region 行政区划下拉框
        /// <summary>
        ///行政区划下拉框
        /// </summary>
        /// <param name="sw">参见条件模型T_ALL_AREA_SW</param>
        /// <returns>返回下拉框<code><option></option></code></returns>
        public static string getAREANAMESelectOption(T_ALL_AREA_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = BaseDT.T_ALL_AREA.getDT();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string AREACODE = dt.Rows[i]["AREACODE"].ToString();
                string AREANAME = dt.Rows[i]["AREANAME"].ToString();
                if (AREACODE.Substring(2, 13) == "0000000000000")
                    AREANAME = "" + AREANAME;
                if (AREACODE.Substring(4, 11) == "00000000000")//获取所有市的
                    AREANAME = "-" + AREANAME;
                else if (AREACODE.Substring(6, 9) == "000000000")//获取所有县的
                    AREANAME = "--" + AREANAME;
                else if (AREACODE.Substring(9, 6) == "000000")//获取所有乡镇的
                    AREANAME = "----" + AREANAME;
                else
                    AREANAME = "------" + AREANAME;

                if (sw.CurAREACODE == AREACODE)//判断是否有需要默认选中的项
                    sb.AppendFormat("<option value=\"{0}\" selected>{1}</option>", AREACODE, AREANAME);
                else
                    sb.AppendFormat("<option value=\"{0}\">{1}</option>", AREACODE, AREANAME);
            }
            dt.Clear();
            dt.Dispose();
            return sb.ToString();
        }

        #endregion

        #region 根据编号获取区划名称
        /// <summary>
        /// 根据编号获取区划名称
        /// </summary>
        /// <param name="sw">参见条件模型T_ALL_AREA_SW</param>
        /// <returns>区划名称</returns>
        public static string getNameByID(T_ALL_AREA_SW sw)
        {
            DataTable dt = BaseDT.T_ALL_AREA.getDT(sw);
            string str = "";
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                    str = dt.Rows[0]["AREANAME"].ToString();
            }
            dt.Clear();
            dt.Dispose();
            return str;
        }

        /// <summary>
        /// 根据行政区划码取名称下拉框
        /// </summary>
        /// <param name="sw">查询模型</param>
        /// <param name="code">编码</param>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public static string getSelectOptionBYWeather(T_ALL_AREA_SW sw, out string code, out string name)
        {
            code = "";
            name = "";
            StringBuilder sb = new StringBuilder();
            DataTable dt = BaseDT.T_ALL_AREA.getDT(sw);
            if (dt.Rows.Count > 0)
            {
                code = dt.Rows[0]["AREACODE"].ToString();
                name = dt.Rows[0]["AREANAME"].ToString();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string areaCode = dt.Rows[i]["AREACODE"].ToString();
                    string areaName = dt.Rows[i]["AREANAME"].ToString();
                    if (sw.CurAREACODE == areaCode)
                    {
                        code = dt.Rows[i]["AREACODE"].ToString();
                        name = dt.Rows[i]["AREANAME"].ToString();
                        sb.AppendFormat("<option value=\"{0}\" selected = \"selected\" >{1}</option>", areaCode, areaName);
                    }
                    else
                    {
                        sb.AppendFormat("<option value=\"{0}\" >{1}</option>", areaCode, areaName);
                    }
                }
            }
            dt.Clear();
            dt.Dispose();
            return sb.ToString();
        }
        #endregion

        #region 获取单条记录
        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <param name="sw">参见条件模型T_ALL_AREA_SW</param>
        /// <returns>参见模型T_ALL_AREA_Model</returns>
        public static T_ALL_AREA_Model getModel(T_ALL_AREA_SW sw)
        {
            DataTable dt = BaseDT.T_ALL_AREA.getDT(sw);
            T_ALL_AREA_Model m = new T_ALL_AREA_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                //数据库表字段
                m.AREAID = dt.Rows[i]["AREAID"].ToString();
                m.AREACODE = dt.Rows[i]["AREACODE"].ToString();
                m.AREANAME = dt.Rows[i]["AREANAME"].ToString();
                m.AREAJC = dt.Rows[i]["AREAJC"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                //扩充字段
            }
            dt.Clear();
            dt.Dispose();
            return m;
        }

        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <param name="sw">参见条件模型T_ALL_AREA_SW</param>
        /// <returns>参见模型T_ALL_AREA_Model</returns>
        public static T_ALL_AREA_Model getModel2(T_ALL_AREA_SW sw)
        {
            DataTable dt = BaseDT.T_ALL_AREA.getDT(sw);
            DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW());
            T_ALL_AREA_Model m = new T_ALL_AREA_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                //数据库表字段
                m.AREAID = dt.Rows[i]["AREAID"].ToString();
                m.AREACODE = dt.Rows[i]["AREACODE"].ToString();
                m.AREANAME = dt.Rows[i]["AREANAME"].ToString();
                m.AREAJC = dt.Rows[i]["AREAJC"].ToString();
                m.JD = BaseDT.T_SYS_ORG.getJD(dtOrg, m.AREACODE);
                m.WD = BaseDT.T_SYS_ORG.getWD(dtOrg, m.AREACODE);
                //扩充字段
            }
            dt.Clear();
            dt.Dispose();
            dtOrg.Clear();
            dtOrg.Dispose();
            return m;
        }

        #endregion

        #region 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw">参见条件模型T_ALL_AREA_SW</param>
        /// <returns>参见模型T_ALL_AREA_Model</returns>
        public static IEnumerable<T_ALL_AREA_Model> getListModel(T_ALL_AREA_SW sw)
        {
            var result = new List<T_ALL_AREA_Model>();
            DataTable dt = BaseDT.T_ALL_AREA.getDT(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T_ALL_AREA_Model m = new T_ALL_AREA_Model();
                m.AREACODE = dt.Rows[i]["AREACODE"].ToString();
                m.AREAID = dt.Rows[i]["AREAID"].ToString();
                m.AREANAME = dt.Rows[i]["AREANAME"].ToString();
                m.AREAJC = dt.Rows[i]["AREAJC"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw">参见条件模型T_ALL_AREA_SW</param>
        /// <returns>参见模型T_ALL_AREA_Model</returns>
        public static IEnumerable<T_ALL_AREA_Model> getListModel2(T_ALL_AREA_SW sw)
        {
            var result = new List<T_ALL_AREA_Model>();
            DataTable dt = BaseDT.T_ALL_AREA.getDT(sw);
            DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T_ALL_AREA_Model m = new T_ALL_AREA_Model();
                m.AREACODE = dt.Rows[i]["AREACODE"].ToString();
                m.AREAID = dt.Rows[i]["AREAID"].ToString();
                m.AREANAME = dt.Rows[i]["AREANAME"].ToString();
                m.AREAJC = dt.Rows[i]["AREAJC"].ToString();
                m.JD = BaseDT.T_SYS_ORG.getJD(dtOrg, m.AREACODE);
                m.WD = BaseDT.T_SYS_ORG.getWD(dtOrg, m.AREACODE);
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtOrg.Clear();
            dtOrg.Dispose();
            return result;
        }

        /// <summary>
        /// 根据地区编码获取列表
        /// </summary>
        /// <param name="sw">参见条件模型T_ALL_AREA_SW</param>
        /// <returns>参见模型T_ALL_AREA_Model</returns>
        public static IEnumerable<T_ALL_AREA_Model> getListModeBYAREACODE(T_ALL_AREA_SW sw)
        {
            var result = new List<T_ALL_AREA_Model>();
            DataTable dt = BaseDT.T_ALL_AREA.getDT(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string areaCode = dt.Rows[i]["AREACODE"].ToString();
                if (areaCode.Substring(0, 6) == sw.CurAREACODE.Substring(0, 6) && areaCode != sw.CurAREACODE)
                {
                    T_ALL_AREA_Model m = new T_ALL_AREA_Model();
                    m.AREACODE = dt.Rows[i]["AREACODE"].ToString();
                    m.AREAID = dt.Rows[i]["AREAID"].ToString();
                    m.AREANAME = dt.Rows[i]["AREANAME"].ToString();
                    m.AREAJC = dt.Rows[i]["AREAJC"].ToString();
                    m.JD = dt.Rows[i]["JD"].ToString();
                    m.WD = dt.Rows[i]["WD"].ToString();
                    result.Add(m);
                }
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }

        #endregion

    }
}