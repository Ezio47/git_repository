using DataBaseClassLibrary;
using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PublicClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 公用_机构表_联系人
    /// </summary>
    public class T_SYS_ORG_LINKCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(T_SYS_ORG_LINK_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msgUser = BaseDT.T_SYS_ORG_LINK.Add(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            if (m.opMethod == "Mdy")
            {
                Message msgUser = BaseDT.T_SYS_ORG_LINK.Mdy(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);

            }
            if (m.opMethod == "Del")
            {
                Message msgUser = BaseDT.T_SYS_ORG_LINK.Del(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            return new Message(false, "无效操作!", "");
        }

        #endregion

        #region 获取单条
        /// <summary>
        /// 根据查询条件获取某一条用户信息记录，用于修改、删除、用户登录验证
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static T_SYS_ORG_LINK_Model getModel(T_SYS_ORG_LINK_SW sw)
        {
            var result = new List<T_SYS_ORG_LINK_Model>();

            DataTable dt = BaseDT.T_SYS_ORG_LINK.getDT(sw);//列表

            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dt45 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "45" });//组织机构联系人类型
            T_SYS_ORG_LINK_Model m = new T_SYS_ORG_LINK_Model();

            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.ORGLINK_ID = dt.Rows[i]["ORGLINK_ID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.ORGLINKTYPE = dt.Rows[i]["ORGLINKTYPE"].ToString();
                m.ORGLINKTYPEName = BaseDT.T_SYS_DICT.getName(dt45, m.ORGLINKTYPE);
                m.UNITNAME = dt.Rows[i]["UNITNAME"].ToString();
                m.NAME = dt.Rows[i]["NAME"].ToString();
                m.USERJOB = dt.Rows[i]["USERJOB"].ToString();
                m.PHONE = dt.Rows[i]["PHONE"].ToString();
                m.Tell = dt.Rows[i]["Tell"].ToString();
                m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
                m.ORGName = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            dt45.Clear();
            dt45.Dispose();
            return m;
        }

        #endregion

        #region 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<T_SYS_ORG_LINK_Model> getModelList(T_SYS_ORG_LINK_SW sw)
        {
            var result = new List<T_SYS_ORG_LINK_Model>();

            DataTable dt = BaseDT.T_SYS_ORG_LINK.getDT(sw);//列表

            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dt45 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "45" });//组织机构联系人类型
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T_SYS_ORG_LINK_Model m = new T_SYS_ORG_LINK_Model();
                m.ORGLINK_ID = dt.Rows[i]["ORGLINK_ID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.ORGLINKTYPE = dt.Rows[i]["ORGLINKTYPE"].ToString();
                m.ORGLINKTYPEName = BaseDT.T_SYS_DICT.getName(dt45, m.ORGLINKTYPE);
                m.UNITNAME = dt.Rows[i]["UNITNAME"].ToString();
                m.NAME = dt.Rows[i]["NAME"].ToString();
                m.USERJOB = dt.Rows[i]["USERJOB"].ToString();
                m.PHONE = dt.Rows[i]["PHONE"].ToString();
                m.Tell = dt.Rows[i]["Tell"].ToString();
                m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
                m.ORGName = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            dt45.Clear();
            dt45.Dispose();
            return result;
        }

        #endregion

        #region 通过组织机构码获取下属自然村
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<T_SYS_ORG_LINK_Model> getVillageList(T_SYS_ORG_LINK_SW sw)
        {
            var result = new List<T_SYS_ORG_LINK_Model>();

            DataTable dt = BaseDT.T_SYS_ORG_LINK.getDT(sw);//列表

            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dt45 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "45" });//组织机构联系人类型
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T_SYS_ORG_LINK_Model m = new T_SYS_ORG_LINK_Model();
                m.ORGLINK_ID = dt.Rows[i]["ORGLINK_ID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.ORGLINKTYPE = dt.Rows[i]["ORGLINKTYPE"].ToString();
                m.ORGLINKTYPEName = BaseDT.T_SYS_DICT.getName(dt45, m.ORGLINKTYPE);
                m.UNITNAME = dt.Rows[i]["UNITNAME"].ToString();
                m.NAME = dt.Rows[i]["NAME"].ToString();
                m.USERJOB = dt.Rows[i]["USERJOB"].ToString();
                m.PHONE = dt.Rows[i]["PHONE"].ToString();
                m.Tell = dt.Rows[i]["Tell"].ToString();
                m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
                m.ORGName = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            dt45.Clear();
            dt45.Dispose();
            return result;
        }

        #endregion

        #region 获取分页
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <param name="total">记录总数</param>
        /// <returns>参见模型</returns>
        public static IEnumerable<T_SYS_ORG_LINK_Model> getModelList(T_SYS_ORG_LINK_SW sw, out int total)
        {
            var result = new List<T_SYS_ORG_LINK_Model>();

            DataTable dt = BaseDT.T_SYS_ORG_LINK.getDT(sw, out total);//列表

            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dt45 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "45" });//组织机构联系人类型

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T_SYS_ORG_LINK_Model m = new T_SYS_ORG_LINK_Model();
                m.ORGLINK_ID = dt.Rows[i]["ORGLINK_ID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.ORGLINKTYPE = dt.Rows[i]["ORGLINKTYPE"].ToString();
                m.ORGLINKTYPEName = BaseDT.T_SYS_DICT.getName(dt45, m.ORGLINKTYPE);
                m.UNITNAME = dt.Rows[i]["UNITNAME"].ToString();
                m.NAME = dt.Rows[i]["NAME"].ToString();
                m.USERJOB = dt.Rows[i]["USERJOB"].ToString();
                m.PHONE = dt.Rows[i]["PHONE"].ToString();
                m.Tell = dt.Rows[i]["Tell"].ToString();
                m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
                m.ORGName = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            dt45.Clear();
            dt45.Dispose();
            return result;
        }
        #endregion

        #region 获取有记录的所有类型
        /// <summary>
        /// 获取有记录的所有类型
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<T_SYS_DICTModel> getDict45(T_SYS_ORG_LINK_SW sw)
        {
            DataTable dt45 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "45" });//组织机构联系人类型
            DataTable dt = BaseDT.T_SYS_ORG_LINK.getDT(sw);//列表

            var result = new List<T_SYS_DICTModel>();
            for (int i = 0; i < dt45.Rows.Count; i++)
            {
                T_SYS_DICTModel m = new T_SYS_DICTModel();
                m.DICTTYPEID = dt45.Rows[i]["DICTTYPEID"].ToString();
                m.DICTID = dt45.Rows[i]["DICTID"].ToString();
                m.DICTTYPEID = dt45.Rows[i]["DICTTYPEID"].ToString();
                m.DICTNAME = dt45.Rows[i]["DICTNAME"].ToString();
                m.DICTVALUE = dt45.Rows[i]["DICTVALUE"].ToString();
                m.ORDERBY = dt45.Rows[i]["ORDERBY"].ToString();
                m.STANDBY1 = dt45.Rows[i]["STANDBY1"].ToString();
                m.STANDBY2 = dt45.Rows[i]["STANDBY2"].ToString();
                m.STANDBY3 = dt45.Rows[i]["STANDBY3"].ToString();
                m.STANDBY4 = dt45.Rows[i]["STANDBY4"].ToString();
                DataRow[] dr = dt.Select("ORGLINKTYPE='" + m.DICTVALUE + "'");
                //if(dr.Count()>0)不需要判断是否有记录，而是显示所有类别
                result.Add(m);
            }
            return result;
        }
        #endregion

        #region 组织机构联系人TREE
        /// <summary>
        /// 组织机构联系人TREE
        /// </summary>
        /// <param name="OrgNo">组织结构编码</param>
        /// <returns></returns>
        public static string GetOrgTree(string OrgNo)
        {
            JArray jObjects = new JArray();
            string curUserOrg = SystemCls.getCurUserOrgNo();//获取当前登录用户的机构编码
            if (string.IsNullOrEmpty(OrgNo) == false)
                curUserOrg = OrgNo;
            var dtOrg = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { TopORGNO = curUserOrg, SYSFLAG = ConfigCls.getSystemFlag() });//获取当前登录用户有权限查看的单位

            DataTable dtLink = BaseDT.T_SYS_ORG_LINK.getDT(new T_SYS_ORG_LINK_SW { });
            DataTable dtVillagecommittee = BaseDT.T_SYS_ORG_CWH.getDT(new T_SYS_ORG_CWH_SW { });

            //JObject root = new JObject 
            //     { 
            //      { "id", "1"+"|" + "1" }, 
            //       { "text", "组织机构成员" } 
            //     };
            #region 市级用户
            if (PublicCls.OrgIsShi(curUserOrg))
            {
                DataRow[] drOrg = dtOrg.Select("", "ORGNO");
                //JArray childArrayroot = new JArray();
                JObject root1 = new JObject 
                   {   
                   { "id", drOrg[0]["ORGNO"].ToString()}, 
                   { "text", drOrg[0]["ORGNAME"].ToString()},
                   {"treeType","1"},
                   { "flag","" }
                   };
                //childArrayroot.Add(root1);
                //root.Add("children", childArrayroot);
                JArray childArray = new JArray();
                DataRow[] drLink = dtLink.Select("BYORGNO = '" + curUserOrg + "'", "");
                for (int i = 0; i < drLink.Length; i++)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("<font title={0}>", drLink[i]["PHONE"].ToString());
                    sb.AppendFormat("{0}[{1}] </font>", drLink[i]["NAME"].ToString(), drLink[i]["USERJOB"].ToString());
                    JObject roota = new JObject 
                        { 
                        { "id", drLink[i]["PHONE"].ToString()},
                        { "text",sb.ToString() } ,
                        { "flag",drLink[i]["NAME"].ToString() }, 
                        { "phone",drLink[i]["PHONE"].ToString() }
                        };
                    childArray.Add(roota);
                }
                DataRow[] drOrgC = dtOrg.Select("SUBSTRING(ORGNO,1,4) = '" + curUserOrg.Substring(0, 4) + "' AND ORGNO<>'" + curUserOrg + "' and SUBSTRING(ORGNO,7,9)='000000000'", "");//获取所有县且〈〉市的
                for (int i = 0; i < drOrgC.Length; i++)
                {
                    JObject rootb = new JObject
                     {
                         {"id",drOrgC[i]["ORGNO"].ToString()},//ORGNO
                         {"text",drOrgC[i]["ORGNAME"].ToString()},
                         {"flag","" }, 
                         {"state","closed"},
                     };
                    childArray.Add(rootb);
                }
                root1.Add("children", childArray);
                jObjects.Add(root1);
            }

            #endregion

            #region 县级用户
            else if (PublicCls.OrgIsXian(curUserOrg))
            {
                DataRow[] drOrg = dtOrg.Select("", "ORGNO");
                //JArray childArrayroot = new JArray();
                JObject root1 = new JObject 
                  { 
                   { "id", drOrg[0]["ORGNO"].ToString()}, 
                   { "text", drOrg[0]["ORGNAME"].ToString() } ,
                   { "flag","" }
                  };
                //childArrayroot.Add(root1);
                //root.Add("children", childArrayroot);
                JArray jObjectsC = new JArray();
                DataRow[] drLink = dtLink.Select("BYORGNO = '" + curUserOrg + "'", "");
                for (int i = 0; i < drLink.Length; i++)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("<font title={0}>", drLink[i]["PHONE"].ToString());
                    sb.AppendFormat("{0}[{1}] </font>", drLink[i]["NAME"].ToString(), drLink[i]["USERJOB"].ToString());
                    JObject rootc = new JObject
                        { 
                           { "id",drLink[i]["PHONE"].ToString()},
                           { "text",sb.ToString() } ,
                           { "flag",drLink[i]["NAME"].ToString() }, 
                           { "phone",drLink[i]["PHONE"].ToString() }
                        };
                    jObjectsC.Add(rootc);
                    if (string.IsNullOrEmpty(OrgNo) == false)//异步加载，不显示县名
                    {
                        jObjects.Add(rootc);
                    }
                }
                DataRow[] drOrgC = dtOrg.Select("SUBSTRING(ORGNO,1,6) = '" + curUserOrg.Substring(0, 6) + "' AND ORGNO<>'" + curUserOrg + "' and SUBSTRING(ORGNO,10,6)='000000'", ""); //获取所有镇且〈〉县的
                for (int i = 0; i < drOrgC.Length; i++)
                {
                    JObject rootd = new JObject
                        {
                         {"id",drOrgC[i]["ORGNO"].ToString()},//ORGNO
                         {"text",drOrgC[i]["ORGNAME"].ToString()},
                         {"flag","" }, 
                         {"state","closed"} ,
                       };
                    jObjectsC.Add(rootd);
                    if (string.IsNullOrEmpty(OrgNo) == false)//异步加载，不显示县名
                    {
                        jObjects.Add(rootd);
                    }
                }
                if (string.IsNullOrEmpty(OrgNo))//县级用户登录
                {
                    jObjects.Add(root1);
                    root1.Add("children", jObjectsC);
                }
            }
            #endregion

            #region 乡镇级用户
            else if (PublicCls.OrgIsZhen(curUserOrg))
            {
                DataRow[] drOrg = dtOrg.Select("", "ORGNO");
                //JArray childArrayroot = new JArray();
                JObject root1 = new JObject 
                  { 
                   { "id", drOrg[0]["ORGNO"].ToString()}, 
                   { "text", drOrg[0]["ORGNAME"].ToString() } ,
                   { "flag","" }, 
                   {"treeType","1"},
                  };
                //childArrayroot.Add(root1);
                //root.Add("children", childArrayroot);
                JArray childArray = new JArray();
                DataRow[] drLink = dtLink.Select("BYORGNO = '" + curUserOrg + "'", "");
                for (int i = 0; i < drLink.Length; i++)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("<font title={0}>", drLink[i]["PHONE"].ToString());
                    sb.AppendFormat("{0}[{1}] </font>", drLink[i]["NAME"].ToString(), drLink[i]["USERJOB"].ToString());
                    JObject rootf = new JObject
                        { 
                           { "id", drLink[i]["PHONE"].ToString()},
                           { "text",sb.ToString() } ,
                           { "flag",drLink[i]["NAME"].ToString() }, 
                           { "phone",drLink[i]["PHONE"].ToString() }
                        };
                    childArray.Add(rootf);
                    if (string.IsNullOrEmpty(OrgNo) == false)//异步加载
                    {
                        jObjects.Add(rootf);
                    }
                }
                DataRow[] drVillagecommittee = dtVillagecommittee.Select("BYORGNO = '" + curUserOrg + "'", "");//获取乡下面的村委会
                for (int i = 0; i < drVillagecommittee.Length; i++)
                {
                    JObject rootg = new JObject
                     {
                         {"id",drVillagecommittee[i]["CWHID"].ToString()},//ORGNO
                         {"text",drVillagecommittee[i]["CWHNAME"].ToString()},
                         {"state","closed"}
                     };
                    childArray.Add(rootg);
                    if (string.IsNullOrEmpty(OrgNo) == false)//异步加载
                    {
                        jObjects.Add(rootg);
                    }
                }
                if (string.IsNullOrEmpty(OrgNo))//乡镇用户登录
                {
                    //jObjects.Add(root);
                    root1.Add("children", childArray);
                }
            }
            else
            {
                JArray childArray = new JArray();
                DataRow[] drLink = dtLink.Select("BYORGNO = '" + curUserOrg + "'", "");
                for (int i = 0; i < drLink.Length; i++)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("<font title={0}>", drLink[i]["PHONE"].ToString());
                    if (string.IsNullOrEmpty(drLink[i]["UNITNAME"].ToString()) == false)
                    {
                        sb.AppendFormat("{0}[{1}][{2}] </font>", drLink[i]["NAME"].ToString(), drLink[i]["USERJOB"].ToString(), drLink[i]["UNITNAME"].ToString());
                    }
                    else
                    {
                        sb.AppendFormat("{0}[{1}][{2}] </font>", drLink[i]["NAME"].ToString(), drLink[i]["USERJOB"].ToString(), "--");
                    }
                    JObject rootf = new JObject
                        { 
                           { "id", drLink[i]["PHONE"].ToString()},
                           { "text",sb.ToString() } ,
                           { "flag",drLink[i]["NAME"].ToString() }, 
                           { "phone",drLink[i]["PHONE"].ToString() }
                        };
                    childArray.Add(rootf);
                    if (string.IsNullOrEmpty(OrgNo) == false)//异步加载
                    {
                        jObjects.Add(rootf);
                    }
                }
            }
            #endregion

            dtOrg.Clear();
            dtOrg.Dispose();
            return JsonConvert.SerializeObject(jObjects);
        }
        #endregion

        #region 根据组织机构获取短信发送tree字符串
        /// <summary>
        /// 根据组织机构获取短信发送tree字符串
        /// </summary>
        /// <param name="orgno"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static SendMessage_Model getstr(string orgno, string type)
        {
            var s = "";
            var d = "";
            var dt = new DataTable();
            var dtvillage = new DataTable();
            var dtipuser = new DataTable();
            if (orgno == "1")
            {
                dt = BaseDT.T_SYS_ORG_LINK.getDT(new T_SYS_ORG_LINK_SW { BYORGNO = SystemCls.getCurUserOrgNo() });//列表
            }
            else if (orgno == "2")
            {
                dtipuser = BaseDT.T_IPSFR_USER.getDT(new T_IPSFR_USER_SW { BYORGNO = SystemCls.getCurUserOrgNo() });
            }
            else
            {
                if (PublicCls.OrgIsShi(orgno) == false && PublicCls.OrgIsXian(orgno) == false && PublicCls.OrgIsZhen(orgno) == false)
                {
                    dtvillage = BaseDT.T_SYS_ORG_CWH.getVillagecComDT(new T_SYS_ORG_CWH_SW { CWHID = orgno });
                }
                else
                {
                    dtvillage = BaseDT.T_SYS_ORG_CWH.getVillagecComDT(new T_SYS_ORG_CWH_SW { BYORGNO = orgno });
                    dt = BaseDT.T_SYS_ORG_LINK.getDT(new T_SYS_ORG_LINK_SW { BYORGNO = orgno });//列表
                    dtipuser = BaseDT.T_IPSFR_USER.getDT(new T_IPSFR_USER_SW { BYORGNO = orgno });
                }
            }
            if (type == "1")
            {
                if (PublicCls.OrgIsShi(orgno) == false && PublicCls.OrgIsXian(orgno) == false && PublicCls.OrgIsZhen(orgno) == false)
                {
                    for (int i = 0; i < dtvillage.Rows.Count; i++)
                    {
                        T_SYS_ORG_LINK_Model m = new T_SYS_ORG_LINK_Model();
                        m.NAME = dtvillage.Rows[i]["NAME"].ToString();
                        m.PHONE = dtvillage.Rows[i]["PHONE"].ToString();
                        s += m.NAME + ",";
                        d += m.PHONE + ",";
                    }
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        T_SYS_ORG_LINK_Model m = new T_SYS_ORG_LINK_Model();
                        m.NAME = dt.Rows[i]["NAME"].ToString();
                        m.PHONE = dt.Rows[i]["PHONE"].ToString();
                        s += m.NAME + ",";
                        d += m.PHONE + ",";
                    }
                    for (int i = 0; i < dtvillage.Rows.Count; i++)
                    {
                        T_SYS_ORG_LINK_Model m = new T_SYS_ORG_LINK_Model();
                        m.NAME = dtvillage.Rows[i]["NAME"].ToString();
                        m.PHONE = dtvillage.Rows[i]["PHONE"].ToString();
                        s += m.NAME + ",";
                        d += m.PHONE + ",";
                    }
                }
            }
            if (type == "2")
            {
                for (int i = 0; i < dtipuser.Rows.Count; i++)
                {
                    T_SYS_ORG_LINK_Model m = new T_SYS_ORG_LINK_Model();
                    m.NAME = dtipuser.Rows[i]["HNAME"].ToString();
                    m.PHONE = dtipuser.Rows[i]["PHONE"].ToString();
                    s += m.NAME + ",";
                    d += m.PHONE + ",";
                }
            }
            s = s.TrimEnd(',');
            d = d.TrimEnd(',');
            SendMessage_Model m1 = new SendMessage_Model();
            m1.PHONELIST = d;
            m1.NAMELIST = s;
            dt.Clear();
            dt.Dispose();
            dtvillage.Clear();
            dtvillage.Dispose();
            dtipuser.Clear();
            dtipuser.Dispose();
            return m1;
        }

        #endregion

        #region 根据手机号码作为id获取tree的父节点
        /// <summary>
        /// 根据组织机构获取短信发送tree字符串
        /// </summary>
        /// <param name="phonelist"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static SendMessage_Model getOrgno(string phonelist, string name)
        {
            var s = "";
            DataTable dt = BaseDT.T_SYS_ORG_LINK.getDT(new T_SYS_ORG_LINK_SW { PHONE = phonelist, keys = name });//列表
            DataTable dtipuser = BaseDT.T_IPSFR_USER.getDT(new T_IPSFR_USER_SW { PHONELIST = phonelist, HNAME = name });
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    T_SYS_ORG_LINK_Model m = new T_SYS_ORG_LINK_Model();
                    m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                    s += m.BYORGNO + "|" + "1" + ",";
                }
            }
            if (dtipuser.Rows.Count > 0)
            {
                for (int i = 0; i < dtipuser.Rows.Count; i++)
                {
                    T_SYS_ORG_LINK_Model m = new T_SYS_ORG_LINK_Model();
                    m.BYORGNO = dtipuser.Rows[i]["BYORGNO"].ToString();
                    s += m.BYORGNO + "|" + "2" + ",";
                }
            }
            s = s.TrimEnd(',');
            SendMessage_Model m1 = new SendMessage_Model();
            m1.BYORGNOLIST = s;
            dt.Clear();
            dt.Dispose();
            dtipuser.Clear();
            dtipuser.Dispose();
            return m1;
        }
        #endregion
    }
}
