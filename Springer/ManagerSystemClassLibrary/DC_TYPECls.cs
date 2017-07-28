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
    /// 数据中心-类别表
    /// </summary>
    public class DC_TYPECls
    {

        #region 增、删、改 
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(DC_TYPE_Model m)
        {
            if (m.opMethod == "Add")
            {
                //SystemCls.LogSave("3", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.DC_TYPE.Add(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            if (m.opMethod == "Mdy")
            {
                //SystemCls.LogSave("4", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.DC_TYPE.Mdy(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);

            }
            if (m.opMethod == "Del")
            {
                //SystemCls.LogSave("5", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.DC_TYPE.Del(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            return new Message(false, "无效操作", "");


        }

        #endregion

        #region 根据物资id获取物资名称
        /// <summary>
        /// 根据物资id获取物资名称
        /// </summary>
        /// <param name="dctypeid">物资id</param>
        /// <returns></returns>
        public static string getSupname(string dctypeid) 
        {
            DC_TYPE_Model m = getModel(new DC_TYPE_SW { DCTYPEID = dctypeid });
            return m.DCTYPENAME;
        }
        #endregion
        #region 获取单条
        /// <summary>
        /// 根据查询条件获取某一条信息记录，用于修改、删除、用户登录验证
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static DC_TYPE_Model getModel(DC_TYPE_SW sw)
        {

            DataTable dt = BaseDT.DC_TYPE.getDT(sw);//列表

            DC_TYPE_Model m = new DC_TYPE_Model();

            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.DCTYPEID = dt.Rows[i]["DCTYPEID"].ToString();
                m.DCTYPETOPID = dt.Rows[i]["DCTYPETOPID"].ToString();
                m.DCTYPENAME = dt.Rows[i]["DCTYPENAME"].ToString();
                m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
                m.DCTYPEFLAG = dt.Rows[i]["DCTYPEFLAG"].ToString();
            }
            dt.Clear();
            dt.Dispose();
            return m;
        }

        #endregion

        #region 获取列表
        /// <summary>
        /// 获取文档列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<DC_TYPE_Model> getModelList(DC_TYPE_SW sw)
        {
            var result = new List<DC_TYPE_Model>();

            DataTable dt = BaseDT.DC_TYPE.getDT(sw);//列表
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DC_TYPE_Model m = new DC_TYPE_Model();
                m.DCTYPEID = dt.Rows[i]["DCTYPEID"].ToString();
                m.DCTYPETOPID = dt.Rows[i]["DCTYPETOPID"].ToString();
                m.DCTYPENAME = dt.Rows[i]["DCTYPENAME"].ToString();
                m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
                m.DCTYPEFLAG = dt.Rows[i]["DCTYPEFLAG"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }

        #endregion                                  

        #region 数据中心树形菜单
        /// <summary>
        /// 组合数据中心树形菜单
        /// </summary>
        /// <param name="dtTYPE">数据中心-类别DataTable</param>
        /// <param name="dctypetopid">父序号</param>
        /// <returns></returns>
        public static JArray getDC_TYPEChild(DataTable dtTYPE, string dctypetopid)
        {
            JArray childobjArray = new JArray();
            DataRow[] drTYPE = dtTYPE.Select("DCTYPETOPID = '" + dctypetopid + "'", "");
            for (int i = 0; i < drTYPE.Length; i++)
            {
                JObject root = new JObject
                        {
                        {"id", drTYPE[i]["DCTYPEID"].ToString()},
                        {"text",drTYPE[i]["DCTYPENAME"].ToString()}
                        };
                root.Add("children", getDC_TYPEChild(dtTYPE, drTYPE[i]["DCTYPEID"].ToString()));
                childobjArray.Add(root);
            }
            return childobjArray;
        }
        #endregion
        #region 组合队伍的树形菜单
        /// <summary>
        /// 组合队伍的树形菜单
        /// </summary>
        /// <returns></returns>
        public static string getArmytree()
        {
            JArray childobjArray = new JArray();
            JObject root = new JObject
                        {
                            {"text","专职人员"},
                        };
            DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });
            DataTable dtUser = BaseDT.DC_FULLTIMEUSER.getDT(new DC_FULLTIMEUSER_SW { });
            if (dtOrg.Rows.Count> 0)
            {
                    root.Add("children", DC_FULLTIMEUSERCls.getFULLTIMEUSERORGChild(dtOrg, dtUser));
                    childobjArray.Add(root);
             }
            DataRow[] drOrg = dtOrg.Select("", "ORGNO");
            JObject root1 = new JObject
                        {
                            {"text","护林员"},
                        };
            DataTable dtFRUser = BaseDT.T_IPSFR_USER.getDT(new T_IPSFR_USER_SW { });
            if (drOrg.Length > 0)
            {
                root1.Add("children", T_IPSFR_USERCls.getDatacenterTreeChild(dtOrg, dtFRUser, drOrg[0]["ORGNO"].ToString()));
                childobjArray.Add(root1);
            }

            JObject root2 = new JObject
                        {
                            {"text","专业队"},
                        };
            DataTable dtTYPE = BaseDT.DC_TYPE.getDT(new DC_TYPE_SW { DCTYPEFLAG = "Army" });
            DataTable dtPROTEAM = BaseDT.DC_PROTEAM.getDT(new DC_PROTEAM_SW { });
            string dctypetopid = BaseDT.DC_TYPE.getID(new DC_TYPE_SW { DCTYPEFLAG = "Army", DCTYPETOPID = "0" });
            root2.Add("children", DC_PROTEAMCls.getPROTEAMchild(dtTYPE, dtPROTEAM, dctypetopid));
            childobjArray.Add(root2);
            JObject root3 = new JObject
                        {
                            {"text","瞭望"},
                        };
            //childobjArray.Add(root3);

            DataTable dtWATCHTOWER = BaseDT.DC_WATCHTOWER.getDT(new DC_WATCHTOWER_SW { });
            if (drOrg.Length > 0)
            {
                root3.Add("children", DC_WATCHTOWERCls.getWATCHTOWERRORGChild(dtOrg, dtWATCHTOWER));
                childobjArray.Add(root3);
            }
            dtFRUser.Clear();
            dtFRUser.Dispose();
            dtOrg.Clear();
            dtOrg.Dispose();
            dtUser.Clear();
            dtUser.Dispose();
            return JsonConvert.SerializeObject(childobjArray);
        }
        #endregion
        #region 设施树形菜单
        /// <summary>
        /// 设施树形子菜单
        /// </summary>
        ///  <param name="dtFACILITY">设施表</param>
        /// <param name="dtTYPE">数据中心类别表</param>
        /// <param name="dctypetopid">父序号</param>
        /// <returns></returns>
        public static JArray getDC_typeFACILITYChild(DataTable dtTYPE, DataTable dtFACILITY, string dctypetopid)
        {
            JArray childobjArray = new JArray();
            DataRow[] drTYPE = dtTYPE.Select("DCTYPETOPID = '" + dctypetopid + "'", "");
            for (int i = 0; i < drTYPE.Length; i++)
            {
                JObject root = new JObject
                        {
                        
                        {"id", drTYPE[i]["DCTYPEID"].ToString()},
                        {"text",drTYPE[i]["DCTYPENAME"].ToString()},
                        {"flag","0"}
                        };
                root.Add("children", getDC_typeFACILITYChild(dtTYPE, dtFACILITY, drTYPE[i]["DCTYPEID"].ToString()));
                childobjArray.Add(root);

            }
            DataRow[] drfacility = dtFACILITY.Select("TYPEID = '" + dctypetopid + "'");
            for (int i = 0; i < drfacility.Length; i++)
            {
                JObject root = new JObject                
                    {
                        {"id",drfacility[i]["DC_FACILITYID"].ToString()},
                        {"text",drfacility[i]["FACINAME"].ToString()},
                        {"flag","1"}
                    };

                childobjArray.Add(root);
            }
            return childobjArray;
        }
        /// <summary>
        /// 组成设施树形菜单
        /// </summary>
        /// <returns></returns>
        public static string getFACILITYTree(DC_TYPE_SW sw)
        {
            DataTable dtTYPE = BaseDT.DC_TYPE.getDT(new DC_TYPE_SW { DCTYPEFLAG = "FACILITY" });
            string dctypetopid = BaseDT.DC_TYPE.getID(new DC_TYPE_SW { DCTYPEFLAG = "FACILITY", DCTYPETOPID = "0" });
            DataTable dtFACILITY = BaseDT.DC_FACILITY.getDT(new DC_FACILITY_SW { });
            JArray FACILITYT = getDC_typeFACILITYChild(dtTYPE, dtFACILITY, dctypetopid);
            dtTYPE.Clear();
            dtTYPE.Dispose();
            return JsonConvert.SerializeObject(FACILITYT);
        }
        #endregion
        #region 资源树形菜单
        /// <summary>
        /// 资源树形子菜单
        /// </summary>
        /// <param name="dtTYPE">数据中心类别表</param>
        /// <param name="dtRESOURCE">资源表</param>
        /// <param name="dctypetopid">父序号</param>
        /// <returns></returns>
        public static JArray getDC_typeRESOURCEChild(DataTable dtTYPE, DataTable dtRESOURCE, string dctypetopid)
        {
            JArray childobjArray = new JArray();
            DataRow[] drTYPE = dtTYPE.Select("DCTYPETOPID = '" + dctypetopid + "'", "");
            for (int i = 0; i < drTYPE.Length; i++)
            {
                JObject root = new JObject
                        {
                        {"id", drTYPE[i]["DCTYPEID"].ToString()},
                        {"text",drTYPE[i]["DCTYPENAME"].ToString()},
                        {"flag","0"}
                        };
                root.Add("children", getDC_typeRESOURCEChild(dtTYPE, dtRESOURCE, drTYPE[i]["DCTYPEID"].ToString()));
                childobjArray.Add(root);
            }
            DataRow[] drresource = dtRESOURCE.Select("TYPEID = '" + dctypetopid + "'");
            for (int i = 0; i < drresource.Length; i++)
            {
                JObject root = new JObject                
                    {
                        {"id",drresource[i]["DC_RESOURCEID"].ToString()},
                        {"text",drresource[i]["FACINAME"].ToString()},
                        {"flag","1"}
                    };
                childobjArray.Add(root);
            }
            return childobjArray;
        }
        /// <summary>
        /// 组成资源树形菜单
        /// </summary>
        /// <returns></returns>
        public static string getRESOURCETree(DC_TYPE_SW sw)
        {
            DataTable dtTYPE = BaseDT.DC_TYPE.getDT(new DC_TYPE_SW { DCTYPEFLAG = "RESOURCE" });
            string dctypetopid = BaseDT.DC_TYPE.getID(new DC_TYPE_SW { DCTYPEFLAG = "RESOURCE", DCTYPETOPID = "0" });
            DataTable dtRESOURCE = BaseDT.DC_RESOURCE.getDT(new DC_RESOURCE_SW { });
            JArray RESOURCE = getDC_typeRESOURCEChild(dtTYPE, dtRESOURCE, dctypetopid);
            dtTYPE.Clear();
            dtTYPE.Dispose();
            return JsonConvert.SerializeObject(RESOURCE);
        }
        #endregion
        #region 装备树形菜单
        /// <summary>
        /// 装备树形子菜单
        /// </summary>
        /// <param name="dtTYPE">数据中心类别表</param>
        /// <param name="dtEQUIP">装备表</param>
        /// <param name="dctypetopid">父序号</param>
        /// <returns></returns>
        public static JArray getDC_typeEQUIPChild(DataTable dtTYPE, DataTable dtEQUIP, string dctypetopid)
        {
            JArray childobjArray = new JArray();
            DataRow[] drTYPE = dtTYPE.Select("DCTYPETOPID = '" + dctypetopid + "'", "");
            for (int i = 0; i < drTYPE.Length; i++)
            {
                JObject root = new JObject
                        {
                        {"id", drTYPE[i]["DCTYPEID"].ToString()},
                        {"text",drTYPE[i]["DCTYPENAME"].ToString()},
                        {"flag","0"}
                        };
                root.Add("children", getDC_typeEQUIPChild(dtTYPE, dtEQUIP, drTYPE[i]["DCTYPEID"].ToString()));
                childobjArray.Add(root);
            }
            DataRow[] drequip = dtEQUIP.Select("TYPEID = '" + dctypetopid + "'");
            for (int i = 0; i < drequip.Length; i++)
            {
                JObject root = new JObject                
                    {
                        {"id",drequip[i]["DC_EQUIPID"].ToString()},
                        {"text",drequip[i]["FACINAME"].ToString()},
                        {"flag","1"}
                    };
                childobjArray.Add(root);
            }
            return childobjArray;
        }
        /// <summary>
        /// 组成装备树形菜单
        /// </summary>
        /// <returns></returns>
        public static string getEQUIPTree(DC_TYPE_SW sw)
        {
            DataTable dtTYPE = BaseDT.DC_TYPE.getDT(new DC_TYPE_SW { DCTYPEFLAG = "EQUIP" });
            string dctypetopid = BaseDT.DC_TYPE.getID(new DC_TYPE_SW { DCTYPEFLAG = "EQUIP", DCTYPETOPID = "0" });
            DataTable dtEQUIP = BaseDT.DC_EQUIP.getDT(new DC_EQUIP_SW { });
            JArray EQUIP = getDC_typeEQUIPChild(dtTYPE, dtEQUIP, dctypetopid);
            dtTYPE.Clear();
            dtTYPE.Dispose();
            return JsonConvert.SerializeObject(EQUIP);
        }
        #endregion
        #region 档案树形菜单
        /// <summary>
        /// 档案树形子菜单
        /// </summary>
        /// <param name="dtDICT">数据字典表</param>
        /// <param name="dicttypeid">数据字典序号</param>
        /// <returns></returns>
        public static JArray getARCHIVESChild( DataTable dtDICT,string dicttypeid)
        {
            JArray childobjArray = new JArray();
            DataRow[] drDICT = dtDICT.Select("DICTTYPEID = '" + dicttypeid + "'", "");
            for (int i = 0; i < drDICT.Length; i++)
            {
                var dictvalue = drDICT[i]["DICTVALUE"].ToString();
                JObject root = new JObject
                        {                          
                         {"id", drDICT[i]["DICTID"].ToString()},
                          {"text",drDICT[i]["DICTNAME"].ToString()},
                          {"type",dictvalue},
                          {"flag","0"}
                         };
                root.Add("children", getARCHIVESTree(dictvalue));
                childobjArray.Add(root);
            }
            return childobjArray;
        }
        
        /// <summary>
        /// 先取出市级单位
        /// </summary>
        /// <param name="dictvalue"></param>
        /// <returns></returns>
        public static JArray getARCHIVESTree(string dictvalue)
        {
            JArray jObjects = new JArray();
            DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });
            DataTable dtFire = BaseDT.JC_FIRE.GetDTFireProp(new JC_FIRE_SW { ISOUTFIRE = "1" });
            DataRow[] drOrg = dtOrg.Select("", "ORGNO");
            if (drOrg.Length > 0)
            {

                JObject root = new JObject
                     {
                         {"id",drOrg[0]["ORGNO"].ToString()},//ORGNO
                         {"text",drOrg[0]["ORGNAME"].ToString()+"["+BaseDT.JC_FIRE.getFireCountByOrgLevel(dtFire,drOrg[0]["ORGNO"].ToString(),dictvalue)+"]"},
                         {"type",dictvalue},
                         {"flag","1"}
                     };
                root.Add("children", getARCHIVESTreeChild(dtFire, dtOrg, drOrg[0]["ORGNO"].ToString(),dictvalue));
                jObjects.Add(root);
            }
            dtOrg.Clear();
            dtOrg.Dispose();
            dtFire.Clear();
            dtFire.Dispose();
            return jObjects;
        }
        /// <summary>
        /// 组织机构子菜单
        /// </summary>
        /// <param name="dtFire">火情表</param>
        /// <param name="dtOrg">组织机构表</param>
        /// <param name="orgNo">组织机构码</param>
        /// <param name="dictvalue">火情级别</param>
        /// <returns></returns>
        public static JArray getARCHIVESTreeChild(DataTable dtFire, DataTable dtOrg, string orgNo, string dictvalue)
        {
            JArray childArray = new JArray();
            if (orgNo.Substring(4, 5) == "00000")//获取所有市下属的县
            {
                DataRow[] drOrg = dtOrg.Select("SUBSTRING(ORGNO,1,4) = '" + orgNo.Substring(0, 4) + "' AND ORGNO<>'" + orgNo + "' and SUBSTRING(ORGNO,7,3)='000'", "");//获取所有县且〈〉市的
                for (int i = 0; i < drOrg.Length; i++)
                {
                    JObject root = new JObject
                     {
                         {"id",drOrg[i]["ORGNO"].ToString()},//ORGNO
                         {"text",drOrg[i]["ORGNAME"].ToString()+"["+BaseDT.JC_FIRE.getFireCountByOrgLevel(dtFire,drOrg[i]["ORGNO"].ToString(),dictvalue)+"]"},
                         {"type",dictvalue},
                         {"flag","1"}
                     };
                    //root.Add("children", getARCHIVESTreeChild(dtOrg, drOrg[i]["ORGNO"].ToString()));//继续获取镇
                    childArray.Add(root);
                }
                return childArray;
            }
            //else if (orgNo.Substring(6, 3) == "000")//获取所有县下属的乡
            //{
            //    DataRow[] drOrg = dtOrg.Select("SUBSTRING(ORGNO,1,6) = '" + orgNo.Substring(0, 6) + "' AND ORGNO<>'" + orgNo + "'", "");//获取所有县且〈〉市的
            //    for (int i = 0; i < drOrg.Length; i++)
            //    {

            //        JObject root = new JObject
            //         {
            //             {"id",""},//ORGNO
            //             {"text",drOrg[i]["ORGNAME"].ToString()},
            //             {"flag","1"}
            //         };
            //        root.Add("children", getARCHIVESTreeChild(dtOrg, drOrg[i]["ORGNO"].ToString()));
            //        childArray.Add(root);
            //    }
            //    return childArray;
            //}
            return childArray;
        }
        /// <summary>
        /// 组成档案树形菜单
        /// </summary>
        /// <returns></returns>
        public static string getARCHIVESTree(T_SYS_DICTSW sw)
        {
            DataTable dtDICT = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { });
            string dicttypeid = "22";
            JArray ARCHIVES = getARCHIVESChild(dtDICT, dicttypeid);
            dtDICT.Clear();
            dtDICT.Dispose();
            return JsonConvert.SerializeObject(ARCHIVES);
        }
        #endregion
        #region 机构首页中第三个仓库菜单
        /// <summary>
        /// 仓库树形菜单
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getdepotTree(DC_TYPE_SW sw)
        {
            DataTable dtTYPE = BaseDT.DC_TYPE.getDT(new DC_TYPE_SW { DCTYPEFLAG = "EQUIP" });
            string dctypetopid = BaseDT.DC_TYPE.getID(new DC_TYPE_SW { DCTYPEFLAG = "EQUIP", DCTYPETOPID = "0" });
            JArray DEPOT = getDC_TYPEChild(dtTYPE,dctypetopid);
            dtTYPE.Clear();
            dtTYPE.Dispose();
            return JsonConvert.SerializeObject(DEPOT);
        }
        #endregion
        #region 机构首页中出入库弹出框仓库树形菜单
        /// <summary>
        /// 仓库树形菜单
        /// </summary>
        /// <param name="dtTYPE">数据中心表</param>
        /// <param name="dtREPOSITORY">仓库表</param>
        /// <param name="dctypetopid">参数</param>
        /// <returns></returns>
        public static JArray getDC_typeREPOSITORYChild(DataTable dtTYPE, DataTable dtREPOSITORY, string dctypetopid)
        {
            JArray childobjArray = new JArray();
            DataRow[] drTYPE = dtTYPE.Select("DCTYPETOPID = '" + dctypetopid + "'", "");
            for (int i = 0; i < drTYPE.Length; i++)
            {
                JObject root = new JObject
                        {
                        {"id", drTYPE[i]["DCTYPEID"].ToString()},
                        {"text",drTYPE[i]["DCTYPENAME"].ToString()},
                        };
                root.Add("children", getDC_typeREPOSITORYChild(dtTYPE, dtREPOSITORY, drTYPE[i]["DCTYPEID"].ToString()));
                childobjArray.Add(root);
            }
            DataRow[] drREPOSITORY = dtREPOSITORY.Select("REPTYPEID = '" + dctypetopid + "'");
            for (int i = 0; i < drREPOSITORY.Length; i++)
            {
                JObject root = new JObject                
                    {
                        {"id",drREPOSITORY[i]["DCREPOSITORYID"].ToString()},
                        {"text",drREPOSITORY[i]["NAME"].ToString()},
                    };
                childobjArray.Add(root);
            }
            return childobjArray;
        }
        /// <summary>
        /// 仓库树形菜单
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getREPOSITORYTree(DC_TYPE_SW sw)
        {
            DataTable dtTYPE = BaseDT.DC_TYPE.getDT(new DC_TYPE_SW { DCTYPEFLAG = "REPOSITORY" });
            string dctypetopid = BaseDT.DC_TYPE.getID(new DC_TYPE_SW { DCTYPEFLAG = "REPOSITORY", DCTYPETOPID = "0" });
            DataTable dtREPOSITORY = BaseDT.DC_REPOSITORY.getDT(new DC_REPOSITORY_SW { });
            JArray REPOSITORY = getDC_typeREPOSITORYChild(dtTYPE, dtREPOSITORY, dctypetopid);
            dtTYPE.Clear();
            dtTYPE.Dispose();
            return JsonConvert.SerializeObject(REPOSITORY);
        }
        #endregion
    }
}
