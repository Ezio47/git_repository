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

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 单位管理类
    /// </summary>
    public class T_SYS_ORGCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">防火系统模型</param>
        /// <returns></returns>
        public static Message Manager(T_SYS_ORGModel m)
        {
            if (m.opMethod == "Add")
            {
                SystemCls.LogSave("6", "组织机构:" + m.ORGNAME, ClsStr.getModelContent(m));
                Message msgOrg = BaseDT.T_SYS_ORG.Add(m);
                if (msgOrg.Success == false)
                    return new Message(msgOrg.Success, msgOrg.Msg, "");
                return new Message(msgOrg.Success, msgOrg.Msg, m.returnUrl);
            }
            if (m.opMethod == "Mdy")
            {
                SystemCls.LogSave("7", "组织机构:" + m.ORGNAME, ClsStr.getModelContent(m));
                Message msgOrg = BaseDT.T_SYS_ORG.Mdy(m);
                return new Message(msgOrg.Success, msgOrg.Msg, m.returnUrl);
            }
            if (m.opMethod == "Del")
            {
                SystemCls.LogSave("8", "组织机构:" + m.ORGNAME, ClsStr.getModelContent(m));
                Message msgOrg = BaseDT.T_SYS_ORG.Del(m);
                return new Message(msgOrg.Success, msgOrg.Msg, m.returnUrl);
            }
            return new Message(false, "无效操作", "");
        }

        /// <summary>
        /// 护林员参数管理
        /// </summary>
        /// <returns></returns>
        public static Message Update(T_SYS_ORGModel m)
        {
            Message msgOrg = BaseDT.T_SYS_ORG.update(m);
            return new Message(msgOrg.Success, msgOrg.Msg, m.returnUrl);
        }
        #endregion

        #region 获取组织机构JsonString
        /// <summary>
        /// 获取组织机构json字符串
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getOrgJsonStr(T_SYS_ORGSW sw)
        {
            DataTable dt = BaseDT.T_SYS_ORG.getDT(sw);
            char[] specialChars = new char[] { ',' };
            string JSONstring = "[";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string orgNo = dt.Rows[i]["ORGNO"].ToString();
                string orgName = dt.Rows[i]["ORGNAME"].ToString();
                JSONstring += "{";
                JSONstring += "\"id\":\"" + orgNo + "\",";
                JSONstring += "\"text\":\"" + orgName + "\"";
                JSONstring += "},";
            }
            JSONstring = JSONstring.TrimEnd(specialChars);
            JSONstring += "]";
            return JSONstring.ToString();
        }
        #endregion

        #region 获取组织机构下拉框
        /// <summary>
        /// 获取组织机构下拉框
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static string getSelectOption(T_SYS_ORGSW sw)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = BaseDT.T_SYS_ORG.getDT(sw);
            if (string.IsNullOrEmpty(sw.CurORGNO))
                sw.CurORGNO = ConfigCls.getTopOrgCode();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string orgNo = dt.Rows[i]["ORGNO"].ToString();
                string orgName = dt.Rows[i]["ORGNAME"].ToString();
                if (orgNo.Substring(4, 11) == "00000000000")//获取所有市的
                    orgName = "" + orgName;
                else if (orgNo.Substring(6, 9) == "000000000")//获取所有县的
                    orgName = "--" + orgName;
                else if (orgNo.Substring(9, 6) == "000000")//获取所有乡镇的
                {
                     orgName = "----" + orgName;
                }
                else
                {
                    if (sw.OnlyGetShiXian != "1")
                    {
                        orgName = "------" + orgName;
                    }
                    else
                    {
                        orgName = "";
                    }
                }
                if (!string.IsNullOrEmpty(orgName))
                {
                    if (sw.CurORGNO == orgNo)//判断是否有需要默认选中的项
                        sb.AppendFormat("<option value=\"{0}\" selected>{1}</option>", orgNo, orgName);
                    else
                        sb.AppendFormat("<option value=\"{0}\">{1}</option>", orgNo, orgName);
                }
            }
            dt.Clear();
            dt.Dispose();
            return sb.ToString();
        }

        /// <summary>
        /// 根据机构编码获取下级机构下拉框
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getSelectOptionByORGNO(T_SYS_ORGSW sw)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = BaseDT.T_SYS_ORG.getDT(sw);
            if (string.IsNullOrEmpty(sw.CurORGNO))
                sw.CurORGNO = ConfigCls.getTopOrgCode();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string orgNo = dt.Rows[i]["ORGNO"].ToString();
                string orgName = dt.Rows[i]["ORGNAME"].ToString();
                if (sw.CurORGNO == orgNo)//判断是否有需要默认选中的项
                    sb.AppendFormat("<option value=\"{0}\" selected>{1}</option>", orgNo, orgName);
                else
                    sb.AppendFormat("<option value=\"{0}\">{1}</option>", orgNo, orgName);
            }
            dt.Clear();
            dt.Dispose();
            return sb.ToString();
        }
        /// <summary>
        /// 获取气象卫星预报 单位选择
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getSelectOptionBYWeather(T_SYS_ORGSW sw)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = BaseDT.T_SYS_ORG.getDT(sw);
            if (string.IsNullOrEmpty(sw.CurORGNO))
                sw.CurORGNO = ConfigCls.getTopOrgCode();
            if (dt.Rows.Count > 0)
            {
                var drArr = dt.Select("SUBSTRING(ORGNO,5,2) <>'00'");
                foreach (var dr in drArr)
                {
                    string orgNo = dr["ORGNO"].ToString();
                    string orgName = dr["ORGNAME"].ToString();

                    if (orgNo.Substring(4, 11) == "00000000000")//获取所有县的
                        orgName = "" + orgName;
                    else if (orgNo.Substring(6, 9) == "000000000")//获取所有市的
                        orgName = "--" + orgName;
                    else if (orgNo.Substring(9, 6) == "000000")//获取所有乡镇的
                    {
                        orgName = "----" + orgName;
                    }
                    else
                    {
                        if (sw.OnlyGetShiXian != "1")
                            orgName = "------" + orgName;
                        else
                            orgName = "";
                    }
                    if (!string.IsNullOrEmpty(orgName))
                    {
                        if (sw.CurORGNO == orgNo)//判断是否有需要默认选中的项
                            sb.AppendFormat("<option value=\"{0}\" selected>{1}</option>", orgNo, orgName);
                        else
                            sb.AppendFormat("<option value=\"{0}\">{1}</option>", orgNo, orgName);
                    }
                }
            }
            dt.Clear();
            dt.Dispose();
            return sb.ToString();
        }

        /// <summary>
        /// 卫星报警页面机构选取
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getSelectOptionByWX(T_SYS_ORGSW sw)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = BaseDT.T_SYS_ORG.getDT(sw);
            if (string.IsNullOrEmpty(sw.CurORGNO))
                sw.CurORGNO = ConfigCls.getTopOrgCode();
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                string orgNo = dt.Rows[i]["ORGNO"].ToString();
                string orgName = dt.Rows[i]["ORGNAME"].ToString();
                if (orgNo.Substring(6, 9) == "000000000")
                    sb.AppendFormat("<option value=\"{0}\">{1}</option>", orgNo, orgName);
            }
            dt.Clear();
            dt.Dispose();
            return sb.ToString();

        }

        /// <summary>
        /// 获取所有县组织机构下拉框
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getSelectOptionByCity(T_SYS_ORGSW sw)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = BaseDT.T_SYS_ORG.getDT(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string orgNo = dt.Rows[i]["ORGNO"].ToString();
                string orgName = dt.Rows[i]["ORGNAME"].ToString();
                if (string.IsNullOrEmpty(sw.CurORGNO))
                    sw.CurORGNO = ConfigCls.getTopOrgCode();
                if (sw.CurORGNO == orgNo)//判断是否有需要默认选中的项
                    sb.AppendFormat("<option value=\"{0}\" selected>{1}</option>", orgNo, orgName);
                else
                    sb.AppendFormat("<option value=\"{0}\">{1}</option>", orgNo, orgName);
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
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static T_SYS_ORGModel getModel(T_SYS_ORGSW sw)
        {
            DataTable dt = BaseDT.T_SYS_ORG.getDT(sw);
            T_SYS_ORGModel m = new T_SYS_ORGModel();
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag(), ORGNO = sw.ORGNO });//获取组织机构
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                //数据库表字段 ORGNO, ORGNAME, ORGDUTY, LEADER, AREACODE, SYSFLAG, ORGJC,  JD, WD
                m.ORGNO = dt.Rows[i]["ORGNO"].ToString();
                m.ORGNAME = dt.Rows[i]["ORGNAME"].ToString();
                m.ORGDUTY = dt.Rows[i]["ORGDUTY"].ToString();
                m.LEADER = dt.Rows[i]["LEADER"].ToString();
                m.AREACODE = dt.Rows[i]["AREACODE"].ToString();
                m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtORG, dt.Rows[i]["ORGNO"].ToString());
                m.ORGJC = dt.Rows[i]["ORGJC"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.COMMANDNAME = dt.Rows[i]["COMMANDNAME"].ToString();
                m.WXJC = dt.Rows[i]["WXJC"].ToString();
                m.WEATHERJC = dt.Rows[i]["WEATHERJC"].ToString();
                m.POSTCODE = dt.Rows[i]["POSTCODE"].ToString();
                m.DUTYTELL = dt.Rows[i]["DUTYTELL"].ToString();
                m.FAX = dt.Rows[i]["FAX"].ToString();
                m.MOBILEPARAMLIST = dt.Rows[i]["MOBILEPARAMLIST"].ToString();
                m.ADDRESS = dt.Rows[i]["ADDRESS"].ToString();
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            return m;
        }

        #endregion

        #region 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static IEnumerable<T_SYS_ORGModel> getListModel(T_SYS_ORGSW sw)
        {
            DataTable dt = BaseDT.T_SYS_ORG.getDT(sw);//列表
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取组织机构
            DataTable dtArea = BaseDT.T_ALL_AREA.getDT();//获取区划编码
            var result = new List<T_SYS_ORGModel>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T_SYS_ORGModel m = new T_SYS_ORGModel();
                m.ORGNO = dt.Rows[i]["ORGNO"].ToString();
                m.ORGNAME = dt.Rows[i]["ORGNAME"].ToString();
                m.ORGDUTY = dt.Rows[i]["ORGDUTY"].ToString();
                m.LEADER = dt.Rows[i]["LEADER"].ToString();
                m.AREACODE = dt.Rows[i]["AREACODE"].ToString();
                m.ORGJC = dt.Rows[i]["ORGJC"].ToString();//ORGJC 单位简称
                m.SYSFLAG = dt.Rows[i]["SYSFLAG"].ToString();
                m.AreaNAME = BaseDT.T_ALL_AREA.getName(dtArea, m.AREACODE);
                // m.ORGJC = dt.Rows[i]["ORGJC"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.COMMANDNAME = dt.Rows[i]["COMMANDNAME"].ToString();
                m.WXJC = dt.Rows[i]["WXJC"].ToString();
                m.WEATHERJC = dt.Rows[i]["WEATHERJC"].ToString();
                m.POSTCODE = dt.Rows[i]["POSTCODE"].ToString();
                m.DUTYTELL = dt.Rows[i]["DUTYTELL"].ToString();
                m.FAX = dt.Rows[i]["FAX"].ToString();
                m.MOBILEPARAMLIST = dt.Rows[i]["MOBILEPARAMLIST"].ToString();
                m.ADDRESS = dt.Rows[i]["ADDRESS"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            dtArea.Clear();
            dtArea.Dispose();
            return result;
        }
        #endregion

        #region 获取市县经纬度
        /// <summary>
        /// 获取市县经纬度
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string GetOrgLonLat(T_SYS_ORGSW sw)
        {
            string strxy = "";
            string distance = "";//放大级别
            string orgname = "";//机构名称
            var model = getModel(sw);
            var bbxz = PublicCls.OrgIsZhen(sw.ORGNO);//乡镇
            var bbsx = PublicCls.OrgIsXian(sw.ORGNO);//市县
            if (!string.IsNullOrEmpty(model.JD) && !string.IsNullOrEmpty(model.WD))
            {
                orgname = model.ORGJC;
                if (bbxz)//乡镇
                {
                    distance = System.Configuration.ConfigurationManager.AppSettings["3DZoomTownshipLayer"].ToString().Trim();
                }
                else if (bbsx)//市县
                {
                    distance = System.Configuration.ConfigurationManager.AppSettings["3DZoomCountyLayer"].ToString().Trim();
                }
                else
                {
                    return "";
                }
                strxy = model.JD + "," + model.WD + "," + distance + "," + orgname;
            }
            else//若未获取经纬度，则获取上级单位的经纬度
            {
                if (bbxz)
                {
                    sw.ORGNO = sw.ORGNO.Substring(0, 6) + "000000000";//市县
                    distance = System.Configuration.ConfigurationManager.AppSettings["3DZoomCountyLayer"].ToString();
                    var DataModel = getModel(sw);
                    if (!string.IsNullOrEmpty(DataModel.JD) && !string.IsNullOrEmpty(DataModel.WD))
                    {
                        strxy = model.JD + "," + model.WD + "," + distance + "," + orgname;
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    if (bbsx)
                    {
                        return "";
                    }
                }
            }
            return strxy;
        }
        #endregion

        #region 根据当前单位编码获取上级单位信息
        /// <summary>
        /// 根据当前单位编码获取上级单位信息
        /// </summary>
        /// <param name="sw">sw.ORGNO 当前单位编码</param>
        /// <returns>参见模型 m.ORGNO m.ORGNAME</returns>
        public static IEnumerable<T_SYS_ORGModel> getNavListModel(T_SYS_ORGSW sw)
        {
            string curUserOrg = SystemCls.getCurUserOrgNo();//获取当前登录用户单位编码
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取所有组织机构
            var result = new List<T_SYS_ORGModel>();
            string orgList = "";// sw.ORGNO;
            if (PublicCls.OrgIsShi(curUserOrg))
            {
                if (PublicCls.OrgIsShi(sw.ORGNO))//如果是市，为顶级
                {
                    orgList = sw.ORGNO;
                }
                else if (PublicCls.OrgIsXian(sw.ORGNO))//县，需要获取县及市
                {
                    string orgS = PublicCls.getShiIncOrgNo(sw.ORGNO) + "00000000000";
                    orgList = orgS + "," + sw.ORGNO;
                }
                else if (PublicCls.OrgIsZhen(sw.ORGNO))//镇，需要获取乡、县及市
                {
                    string orgS = PublicCls.getShiIncOrgNo(sw.ORGNO) + "00000000000";
                    string orgX = PublicCls.getXianIncOrgNo(sw.ORGNO) + "000000000";
                    orgList = orgS + "," + orgX + "," + sw.ORGNO;
                }
                else
                {
                    orgList = "";
                }
            }
            else if (PublicCls.OrgIsXian(curUserOrg))
            {
                if (PublicCls.OrgIsXian(sw.ORGNO))//县，需要获取县及市
                {
                    orgList = sw.ORGNO;
                }
                else if (PublicCls.OrgIsZhen(sw.ORGNO))//镇，需要获取乡、县及市
                {
                    string orgX = PublicCls.getXianIncOrgNo(sw.ORGNO) + "000000000";
                    orgList = orgX + "," + sw.ORGNO;
                }
                else
                {
                    orgList = "";
                }
            }
            else if (PublicCls.OrgIsZhen(curUserOrg))
            {
                if (PublicCls.OrgIsZhen(sw.ORGNO))//镇，需要获取乡、县及市
                {
                    orgList = sw.ORGNO;
                }
                else
                {
                    orgList = "";
                }
            }
            string[] arr = orgList.Split(',');
            for (int i = 0; i < arr.Length; i++)
            {
                T_SYS_ORGModel m = new T_SYS_ORGModel();
                m.ORGNO = arr[i];
                m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtORG, m.ORGNO);
                result.Add(m);
            }
            return result;
        }
        #endregion

        #region 组织机构树形菜单展示
        /// <summary>
        /// 组织机构树形菜单
        /// </summary>
        /// <param name="dtOrg"></param>
        /// <param name="orgNo"></param>
        /// <returns></returns>
        private static JArray getTreeORGChild(DataTable dtOrg, string orgNo)
        {
            JArray childArray = new JArray();
            if (orgNo.Substring(4, 11) == "00000000000")
            {
                string orgshi = orgNo.Substring(0, 4) + "xxxxxxxxxxx";
                string orgname = T_SYS_ORGCls.getorgname(orgNo);
                JObject root = new JObject { { "id", orgshi }, { "text", orgname } };
                childArray.Add(root);
                DataRow[] drOrg = dtOrg.Select("SUBSTRING(ORGNO,1,4) = '" + orgNo.Substring(0, 4) + "' AND ORGNO<>'" + orgNo + "' and SUBSTRING(ORGNO,7,9)='000000000'", "");//获取市下级县
                for (int i = 0; i < drOrg.Length; i++)
                {
                    string orgxian = drOrg[i]["ORGNO"].ToString();//县编码
                    string name = drOrg[i]["ORGNAME"].ToString() + "所有";
                    string orgxianre = orgxian.Substring(0, 6) + "xxxxxxxxx";//县的替代边吗
                    JObject root1 = new JObject { { "id", orgxianre }, { "text", name } };
                    root1.Add("children", getTreeORGChild(dtOrg, orgxianre));
                    childArray.Add(root1);
                }
                return childArray;
            }
            else if (orgNo.Substring(6, 9) == "xxxxxxxxx" && orgNo.Substring(4, 2) != "xx")//获取所有县下属的乡包括县
            {
                DataRow[] drOrg = dtOrg.Select("SUBSTRING(ORGNO,1,6) = '" + orgNo.Substring(0, 6) + "' AND ORGNO<>'" + orgNo + "' and SUBSTRING(ORGNO,10,6)='000000'", "");//获取所有县且〈〉市的
                for (int i = 0; i < drOrg.Length; i++)
                {
                    JObject root = new JObject { { "id", drOrg[i]["ORGNO"].ToString() }, { "text", drOrg[i]["ORGNAME"].ToString() } };
                    childArray.Add(root);
                }
                return childArray;
            }
            return childArray;
        }

        /// <summary>
        /// 获取组织机构树形菜单
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getORGTree(T_SYS_ORGSW sw)
        {
            JArray jObjects = new JArray();
            string curUserOrg = SystemCls.getCurUserOrgNo();//获取当前登录用户的机构编码
            DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { TopORGNO = curUserOrg, SYSFLAG = ConfigCls.getSystemFlag() });//获取当前登录用户有权限查看的单位
            DataRow[] drOrg = dtOrg.Select("", "ORGNO");
            if (drOrg.Length > 0)
            {
                if (drOrg[0]["ORGNO"].ToString().Substring(4, 11) == "00000000000")
                {
                    JObject root = new JObject { { "id", drOrg[0]["ORGNO"].ToString() }, { "text", "全部" } };
                    root.Add("children", getTreeORGChild(dtOrg, drOrg[0]["ORGNO"].ToString()));
                    jObjects.Add(root);
                }
                else if (drOrg[0]["ORGNO"].ToString().Substring(6, 9) == "000000000")
                {
                    string orgno = drOrg[0]["ORGNO"].ToString().Substring(0, 6) + "xxxxxxxxx";
                    JObject root = new JObject { { "id", orgno }, { "text", drOrg[0]["ORGNAME"].ToString() + "所有" } };
                    root.Add("children", getTreeORGChild(dtOrg, orgno));
                    jObjects.Add(root);
                }
                else
                {
                    JObject root = new JObject { { "id", drOrg[0]["ORGNO"].ToString() }, { "text", drOrg[0]["ORGNAME"].ToString() } };
                    jObjects.Add(root);
                }
            }
            dtOrg.Clear();
            dtOrg.Dispose();
            return JsonConvert.SerializeObject(jObjects);
        }
        #endregion

        #region 组织机构树形菜单展示不包括所有
        /// <summary>
        /// 组织机构树形菜单展示不包括所有
        /// </summary>
        /// <param name="dtOrg"></param>
        /// <param name="orgNo"></param>
        /// <returns></returns>
        private static JArray getTreeOnlyORGChild(DataTable dtOrg, string orgNo)
        {
            JArray childArray = new JArray();
            if (orgNo.Substring(4, 11) == "00000000000")
            {
                DataRow[] drOrg = dtOrg.Select("SUBSTRING(ORGNO,1,4) = '" + orgNo.Substring(0, 4) + "' AND ORGNO<>'" + orgNo + "' and SUBSTRING(ORGNO,7,9)='000000000'", "");//获取市下级县
                for (int i = 0; i < drOrg.Length; i++)
                {
                    string orgxian = drOrg[i]["ORGNO"].ToString();//县编码
                    string name = drOrg[i]["ORGNAME"].ToString();
                    JObject root1 = new JObject { { "id", orgxian }, { "text", name } };
                    root1.Add("children", getTreeOnlyORGChild(dtOrg, orgxian));
                    childArray.Add(root1);
                }
                return childArray;
            }
            else if (orgNo.Substring(6, 9) == "000000000" && orgNo.Substring(4, 2) != "00")//获取所有县下属的乡
            {
                DataRow[] drOrg = dtOrg.Select("SUBSTRING(ORGNO,1,6) = '" + orgNo.Substring(0, 6) + "' AND ORGNO<>'" + orgNo + "' and SUBSTRING(ORGNO,10,6)='000000'", "");//获取所有县且〈〉市的
                for (int i = 0; i < drOrg.Length; i++)
                {
                    JObject root = new JObject { { "id", drOrg[i]["ORGNO"].ToString() }, { "text", drOrg[i]["ORGNAME"].ToString() } };
                    childArray.Add(root);
                }
                return childArray;
            }
            return childArray;
        }
        /// <summary>
        /// 获取组织机构树形菜单
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getOnlyORGTree(T_SYS_ORGSW sw)
        {
            JArray jObjects = new JArray();
            string curUserOrg = SystemCls.getCurUserOrgNo();//获取当前登录用户的机构编码
            DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { TopORGNO = curUserOrg, SYSFLAG = ConfigCls.getSystemFlag() });//获取当前登录用户有权限查看的单位
            DataRow[] drOrg = dtOrg.Select("", "ORGNO");
            if (drOrg.Length > 0)
            {
                JObject root = new JObject { { "id", drOrg[0]["ORGNO"].ToString() }, { "text", drOrg[0]["ORGNAME"].ToString() } };
                root.Add("children", getTreeOnlyORGChild(dtOrg, drOrg[0]["ORGNO"].ToString()));
                jObjects.Add(root);
            }
            dtOrg.Clear();
            dtOrg.Dispose();
            return JsonConvert.SerializeObject(jObjects);
        }
        #endregion

        #region 通过组织机构码组织机构名和指挥部名称
        /// <summary>
        /// 组织机构码组织机构名和指挥部名称
        /// </summary>
        /// <param name="orgno">组织机构码</param>
        /// <returns></returns>
        public static string getComandname(string orgno)
        {
            T_SYS_ORGModel m = getModel(new T_SYS_ORGSW { ORGNO = orgno });
            return m.ORGNAME + m.COMMANDNAME;
        }
        #endregion

        #region 通过组织机构码获取组织机构码名
        /// <summary>
        /// 通过组织机构码获取组织机构名
        /// </summary>
        /// <param name="orgno"></param>
        /// <returns></returns>
        public static string getorgname(string orgno)
        {
            T_SYS_ORGModel m = getModel(new T_SYS_ORGSW { ORGNO = orgno });
            return m.ORGNAME;
        }
        #endregion

        #region 通过组织机构名获取组织机构吗
        /// <summary>
        /// 通过组织机构名获取组织机构吗
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string getCodeByName(string name)
        {
            string orgno = "";
            if (string.IsNullOrEmpty(name)==false)
            {
                orgno = BaseDT.T_SYS_ORG.getCodeByName(name);
            }
            return orgno;
        }
        #endregion
    }
}