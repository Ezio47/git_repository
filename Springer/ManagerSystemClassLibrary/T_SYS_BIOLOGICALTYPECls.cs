using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 公用_生物分类表
    /// </summary>
    public class T_SYS_BIOLOGICALTYPECls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型T_SYS_BIOLOGICALTYPE_Model</param>
        /// <returns>参见模型Message</returns>
        public static Message Manager(T_SYS_BIOLOGICALTYPE_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msg = BaseDT.T_SYS_BIOLOGICALTYPE.Add(m);
                if (msg.Success == false)
                    return new Message(msg.Success, msg.Msg, "");
                return new Message(msg.Success, msg.Msg, m.returnUrl);
            }
            if (m.opMethod == "Mdy")
            {
                Message msg = BaseDT.T_SYS_BIOLOGICALTYPE.Mdy(m);
                if (msg.Success == false)
                    return new Message(msg.Success, msg.Msg, "");
                return new Message(msg.Success, msg.Msg, m.returnUrl);
            }
            if (m.opMethod == "Del")
            {
                Message msg = BaseDT.T_SYS_BIOLOGICALTYPE.Del(m);
                if (msg.Success == false)
                    return new Message(msg.Success, msg.Msg, "");
                if (PublicCls.BioCodeIsJie(m.BIOLOCODE))
                    m.returnUrl = "";
                if (PublicCls.BioCodeIsMen(m.BIOLOCODE))
                    m.returnUrl = PublicCls.GetJieBioCode(m.BIOLOCODE) + "000000000000";
                if (PublicCls.BioCodeIsGang(m.BIOLOCODE))
                    m.returnUrl = PublicCls.GetMenBioCode(m.BIOLOCODE) + "0000000000";
                if (PublicCls.BioCodeIsMu(m.BIOLOCODE))
                    m.returnUrl = PublicCls.GetGangBioCode(m.BIOLOCODE) + "00000000";
                if (PublicCls.BioCodeIsKe(m.BIOLOCODE))
                    m.returnUrl = PublicCls.GetMuBioCode(m.BIOLOCODE) + "000000";
                if (PublicCls.BioCodeIsShu(m.BIOLOCODE))
                    m.returnUrl = PublicCls.GetKeBioCode(m.BIOLOCODE) + "0000";
                if (PublicCls.BioCodeIsZHong(m.BIOLOCODE))
                    m.returnUrl = PublicCls.GetShuBioCode(m.BIOLOCODE) + "00";
                return new Message(msg.Success, msg.Msg, m.returnUrl);
            }
            return new Message(false, "无效操作!", m.returnUrl);
        }
        #endregion

        #region 获取单条记录
        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <param name="sw">参见条件模型T_SYS_BIOLOGICALTYPE_SW</param>
        /// <returns>参见模型T_SYS_BIOLOGICALTYPE</returns>
        public static T_SYS_BIOLOGICALTYPE_Model getModel(T_SYS_BIOLOGICALTYPE_SW sw)
        {
            DataTable dt = BaseDT.T_SYS_BIOLOGICALTYPE.getModel(sw);
            T_SYS_BIOLOGICALTYPE_Model m = new T_SYS_BIOLOGICALTYPE_Model();
            if (dt.Rows.Count > 0)
            {
                //数据库表字段
                m.BIOLOCODE = dt.Rows[0]["BIOLOCODE"].ToString();
                m.BIOLONAME = dt.Rows[0]["BIOLONAME"].ToString();
                m.BIOLOENNAME = dt.Rows[0]["BIOLOENNAME"].ToString();
                m.ORDERBY = dt.Rows[0]["ORDERBY"].ToString();
                //扩充字段
            }
            dt.Clear();
            dt.Dispose();
            return m;
        }

        #endregion

        #region 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static IEnumerable<T_SYS_BIOLOGICALTYPE_Model> getListModel(T_SYS_BIOLOGICALTYPE_SW sw)
        {
            var result = new List<T_SYS_BIOLOGICALTYPE_Model>();
            DataTable dt = BaseDT.T_SYS_BIOLOGICALTYPE.getDT(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T_SYS_BIOLOGICALTYPE_Model m = new T_SYS_BIOLOGICALTYPE_Model();
                m.BIOLOCODE = dt.Rows[i]["BIOLOCODE"].ToString();
                m.BIOLONAME = dt.Rows[i]["BIOLONAME"].ToString();
                m.BIOLOENNAME = dt.Rows[i]["BIOLOENNAME"].ToString();
                m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }

        /// <summary>
        /// 获取种级数据列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static IEnumerable<T_SYS_BIOLOGICALTYPE_Model> getZhongListModel(T_SYS_BIOLOGICALTYPE_SW sw)
        {
            var result = new List<T_SYS_BIOLOGICALTYPE_Model>();
            DataTable dtALL = BaseDT.T_SYS_BIOLOGICALTYPE.getDT(new T_SYS_BIOLOGICALTYPE_SW { BIOLOCODE = sw.BIOLOCODE });
            DataTable dt = BaseDT.T_SYS_BIOLOGICALTYPE.getDT(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T_SYS_BIOLOGICALTYPE_Model m = new T_SYS_BIOLOGICALTYPE_Model();
                m.BIOLOCODE = dt.Rows[i]["BIOLOCODE"].ToString();
                m.BIOLONAME = dt.Rows[i]["BIOLONAME"].ToString();
                m.BIOLOENNAME = dt.Rows[i]["BIOLOENNAME"].ToString();
                m.BIOLOKENAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtALL, m.BIOLOCODE.Substring(0, 10) + "0000");
                m.BIOLOSHUNAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtALL, m.BIOLOCODE.Substring(0, 12) + "00");
                m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion

        #region 根据分类编码获取分类名称
        /// <summary>
        /// 根据分类编码获取分类名称
        /// </summary>
        /// <param name="BIOLOCODE">编码</param>
        /// <returns>名称</returns>
        public static string getName(string BIOLOCODE)
        {
            return BaseDT.T_SYS_BIOLOGICALTYPE.getName(BIOLOCODE);
        }

        #endregion

        #region 判断是否存在下属分类
        /// <summary>
        /// 判断是否有下级
        /// </summary>
        /// <param name="sw">参见模型T_SYS_BIOLOGICALTYPE_SW</param>
        /// <returns>true:存在 false：不存在</returns>
        public static bool isExistsChild(T_SYS_BIOLOGICALTYPE_SW sw)
        {
            return BaseDT.T_SYS_BIOLOGICALTYPE.isExistsChild(sw);
        }
        #endregion

        #region 树形菜单
        /// <summary>
        /// 类别树形菜单
        /// </summary>
        /// <param name="sw">idcode</param>
        /// <returns></returns>
        public static string GetTypeTree(T_SYS_BIOLOGICALTYPE_SW sw)
        {
            DataTable dt = BaseDT.T_SYS_BIOLOGICALTYPE.getDT(sw);
            JArray JA = GetTreeChild(dt, sw.BIOLOCODE);
            dt.Clear();
            dt.Dispose();
            return JsonConvert.SerializeObject(JA);
        }

        /// <summary>
        /// 递归加载分类
        /// </summary>
        /// <param name="dt">dt</param>
        /// <param name="code">code</param>
        /// <returns></returns>
        private static JArray GetTreeChild(DataTable dt, string code)
        {
            JArray childobjArray = new JArray();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow[] dr = null;
                if (string.IsNullOrEmpty(code))
                    dr = dt.Select("SUBSTRING(BIOLOCODE,1,2) <> '00' AND SUBSTRING(BIOLOCODE,3,12)='000000000000'");
                else
                {
                    string str = "";
                    if (PublicCls.BioCodeIsJie(code))
                    {
                        str = PublicCls.GetJieBioCode(code);
                        dr = dt.Select("SUBSTRING(BIOLOCODE,1,2) = '" + str + "' AND SUBSTRING(BIOLOCODE,3,2)<>'00' AND SUBSTRING(BIOLOCODE,5,10)='0000000000'");
                    }
                    else if (PublicCls.BioCodeIsMen(code))
                    {
                        str = PublicCls.GetMenBioCode(code);
                        dr = dt.Select("SUBSTRING(BIOLOCODE,1,4) = '" + str + "' AND SUBSTRING(BIOLOCODE,5,2)<>'00' AND SUBSTRING(BIOLOCODE,7,8)='00000000'");
                    }
                    else if (PublicCls.BioCodeIsGang(code))
                    {
                        str = PublicCls.GetGangBioCode(code);
                        dr = dt.Select("SUBSTRING(BIOLOCODE,1,6) = '" + str + "' AND SUBSTRING(BIOLOCODE,7,2)<>'00' AND SUBSTRING(BIOLOCODE,9,6)='000000'");
                    }
                    else if (PublicCls.BioCodeIsMu(code))
                    {
                        str = PublicCls.GetMuBioCode(code);
                        dr = dt.Select("SUBSTRING(BIOLOCODE,1,8) = '" + str + "' AND SUBSTRING(BIOLOCODE,9,2)<>'00' AND SUBSTRING(BIOLOCODE,11,4)='0000'");
                    }
                    else if (PublicCls.BioCodeIsKe(code))
                    {
                        str = PublicCls.GetKeBioCode(code);
                        dr = dt.Select("SUBSTRING(BIOLOCODE,1,10) = '" + str + "' AND SUBSTRING(BIOLOCODE,11,2)<>'00' AND SUBSTRING(BIOLOCODE,13,2)='00'");
                    }
                    else if (PublicCls.BioCodeIsShu(code))
                    {
                        str = PublicCls.GetShuBioCode(code);
                        dr = dt.Select("SUBSTRING(BIOLOCODE,1,12) = '" + str + "' AND SUBSTRING(BIOLOCODE,13,2)<>'00'");
                    }
                    else
                    {
                        str = PublicCls.GetZhongBioCode(code);
                        dr = dt.Select("SUBSTRING(BIOLOCODE,1,14) = '" + str + "' AND LEN(BIOLOCODE)>'" + code.Length + "'");
                    }
                }
                if (dr != null && dr.Count() > 0)
                {
                    for (int i = 0; i < dr.Count(); i++)
                    {
                        code = dr[i]["BIOLOCODE"].ToString();
                        bool haveChild = BaseDT.T_SYS_BIOLOGICALTYPE.isExistsChild(new T_SYS_BIOLOGICALTYPE_SW { BIOLOCODE = code });
                        JObject root = new JObject 
                        { 
                            { "id", dr[i]["BIOLOCODE"].ToString() }, 
                            { "text", dr[i]["BIOLONAME"].ToString() + "[" + dr[i]["BIOLOCODE"].ToString()+ "]" }, { "state", haveChild ? "closed":"open" } 
                        };
                        if (haveChild)
                            root.Add("children", GetTreeChild(dt, code));
                        childobjArray.Add(root);
                    }
                }
            }
            return childobjArray;
        }
        #endregion

        #region 获取生物分类下拉框
        /// <summary>
        /// 获取生物分类下拉框
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static string GetSelectOption(T_SYS_BIOLOGICALTYPE_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = BaseDT.T_SYS_BIOLOGICALTYPE.getDT(sw);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string code = dt.Rows[i]["BIOLOCODE"].ToString();
                    string name = dt.Rows[i]["BIOLONAME"].ToString();
                    if (PublicCls.BioCodeIsJie(code))
                        name = "" + name;
                    else if (PublicCls.BioCodeIsMen(code))
                        name = "-" + name;
                    else if (PublicCls.BioCodeIsGang(code))
                        name = "--" + name;
                    else if (PublicCls.BioCodeIsMu(code))
                        name = "---" + name;
                    else if (PublicCls.BioCodeIsKe(code))
                        name = "----" + name;
                    else if (PublicCls.BioCodeIsShu(code))
                        name = "-----" + name;
                    else if (PublicCls.BioCodeIsZHong(code))
                        name = "------" + name;
                    if (!string.IsNullOrEmpty(name))
                    {
                        if (sw.CurCODE == code)//判断是否有需要默认选中的项
                            sb.AppendFormat("<option value=\"{0}\" selected>{1}</option>", code, name);
                        else
                            sb.AppendFormat("<option value=\"{0}\">{1}</option>", code, name);
                    }
                }
            }
            else
            {
                sb.AppendFormat("<option value=\"{0}\">{1}</option>", "", "--暂无类别--");
            }
            dt.Clear();
            dt.Dispose();
            return sb.ToString();
        }
        #endregion
    }
}
