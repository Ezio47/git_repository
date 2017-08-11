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
    /// 系统_图层表
    /// </summary>
    public class T_SYS_LAYERCls
    {
        /// <summary>
        /// 获取树形三维图层控制菜单
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getTree(T_SYS_LAYER_SW sw)
        {
            JArray jObjects = new JArray();
            DataTable dtLayer = BaseDT.T_SYS_LAYER.getDT(sw);
            if (dtLayer != null && dtLayer.Rows.Count > 0)
            {
                DataRow[] drOrg = dtLayer.Select("Len(LAYERCODE) = '2'");
                if (drOrg.Length > 0)
                {
                    JObject root = new JObject { { "id", drOrg[0]["LAYERCODE"].ToString() }, { "text", drOrg[0]["LAYERNAME"].ToString() } };
                    JArray childArray = new JArray();
                    DataRow[] drOrg1 = dtLayer.Select("Len(LAYERCODE) = '4' AND SUBSTRING(LAYERCODE,1,3)='010'");//点，线，面
                    for (int i = 0; i < drOrg1.Length; i++)
                    {
                        JObject root1 = new JObject { { "id", drOrg1[i]["LAYERCODE"].ToString() }, { "text", drOrg1[i]["LAYERNAME"].ToString() } };
                        JArray childArray1 = new JArray();
                        DataRow[] drOrg2 = dtLayer.Select("(Len(LAYERCODE) = '6' AND SUBSTRING(LAYERCODE,1,5)='" + drOrg1[i]["LAYERCODE"].ToString() + "0" + "') OR (Len(LAYERCODE) = '6' AND SUBSTRING(LAYERCODE,1,5)='" + drOrg1[i]["LAYERCODE"].ToString() + "1" + "')");
                        for (int j = 0; j < drOrg2.Length; j++)
                        {

                            JObject root2 = new JObject { { "id", drOrg2[j]["LAYERCODE"].ToString() }, { "text", drOrg2[j]["LAYERNAME"].ToString() }, { "state", "closed" } };
                            JArray childArray2 = new JArray();
                            DataRow[] drOrg3 = dtLayer.Select("Len(LAYERCODE) = '8' AND SUBSTRING(LAYERCODE,1,7)='" + drOrg2[j]["LAYERCODE"].ToString() + "0" + "'");
                            for (int k = 0; k < drOrg3.Length; k++)
                            {

                                JObject root3 = new JObject { { "id", drOrg3[k]["LAYERCODE"].ToString() }, { "text", drOrg3[k]["LAYERNAME"].ToString() } };
                                if (drOrg3[k]["ISDEFAULTCH"].ToString() == "1")
                                {
                                    root3 = new JObject { { "id", drOrg3[k]["LAYERCODE"].ToString() }, { "text", drOrg3[k]["LAYERNAME"].ToString() }, { "checked", true } };
                                }
                                JArray childArray3 = new JArray();
                                DataRow[] drOrg4 = dtLayer.Select("Len(LAYERCODE) = '10' AND SUBSTRING(LAYERCODE,1,9)='" + drOrg3[k]["LAYERCODE"].ToString() + "0" + "'");
                                for (int m = 0; m < drOrg4.Length; m++)
                                {
                                    JObject root4 = new JObject { { "id", drOrg4[m]["LAYERCODE"].ToString() }, { "text", drOrg4[m]["LAYERNAME"].ToString() } };
                                    if (drOrg4[m]["ISDEFAULTCH"].ToString() == "1")
                                    {
                                        root4 = new JObject { { "id", drOrg4[m]["LAYERCODE"].ToString() }, { "text", drOrg4[m]["LAYERNAME"].ToString() }, { "checked", true } };
                                    }
                                    childArray3.Add(root4);
                                }
                                root3.Add("children", childArray3);
                                childArray2.Add(root3);
                            }
                            if (drOrg3.Count() > 0)
                            {
                                root2.Add("children", childArray2);
                            }
                            else
                                root2 = new JObject { { "id", drOrg2[j]["LAYERCODE"].ToString() }, { "text", drOrg2[j]["LAYERNAME"].ToString() } };
                            if (drOrg2[j]["ISDEFAULTCH"].ToString() == "1")
                            {
                                root2 = new JObject { { "id", drOrg2[j]["LAYERCODE"].ToString() }, { "text", drOrg2[j]["LAYERNAME"].ToString() }, { "checked", true } };
                            }

                            childArray1.Add(root2);
                        }
                        root1.Add("children", childArray1);
                        childArray.Add(root1);
                    }
                    root.Add("children", childArray);
                    jObjects.Add(root);
                }
            }
            dtLayer.Clear();
            dtLayer.Dispose();
            return JsonConvert.SerializeObject(jObjects);
        }

        /// <summary>
        /// 使用递归法获取三维图层
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getTckzTree(T_SYS_LAYER_SW sw)
        {
            JArray jObjects = new JArray();
            DataTable dtLayer = BaseDT.T_SYS_LAYER.getDT(sw);
            DataRow[] drLayer = dtLayer.Select("", "LAYERCODE");
            if (drLayer.Length > 0)
            {
                if (drLayer[0]["LAYERCODE"].ToString().Length == 2)
                {
                    JObject root = new JObject { { "id", drLayer[0]["LAYERCODE"].ToString() }, { "text", drLayer[0]["LAYERNAME"].ToString() } };
                    root.Add("children", getTckzTreeChild(dtLayer, drLayer[0]["LAYERCODE"].ToString()));
                    jObjects.Add(root);
                }
                else
                {
                    JObject root = new JObject { { "id", drLayer[0]["LAYERCODE"].ToString() }, { "text", drLayer[0]["LAYERNAME"].ToString() } };
                    jObjects.Add(root);
                }
            }
            dtLayer.Clear();
            dtLayer.Dispose();
            return JsonConvert.SerializeObject(jObjects);
        }

        /// <summary>
        /// 三维图层递归法children
        /// </summary>
        /// <param name="dtLayer"></param>
        /// <param name="layerCode"></param>
        /// <returns></returns>
        /// 
        private static JArray getTckzTreeChild(DataTable dtLayer, string layerCode)
        {
            JArray childArray = new JArray();
            if (layerCode.Length == 2)
            {
                DataRow[] drLayer = dtLayer.Select("Len(LAYERCODE) = '4' AND SUBSTRING(LAYERCODE,1,3)='010'");//点，线，面
                if (drLayer.Length > 0)
                {
                    for (int i = 0; i < drLayer.Length; i++)
                    {
                        string layerCode1 = drLayer[i]["LAYERCODE"].ToString();
                        JObject root1 = new JObject { { "id", drLayer[i]["LAYERCODE"].ToString() }, { "text", drLayer[i]["LAYERNAME"].ToString() } };
                        root1.Add("children", getTckzTreeChild(dtLayer, layerCode1));
                        childArray.Add(root1);
                    }
                    return childArray;
                }
            }
            if (layerCode.Length == 4)
            {
                DataRow[] drLayer = dtLayer.Select("(Len(LAYERCODE) = '6' AND SUBSTRING(LAYERCODE,1,5)='" + layerCode + "0" + "') OR (Len(LAYERCODE) = '6' AND SUBSTRING(LAYERCODE,1,5)='" + layerCode + "1" + "')");
                if (drLayer.Length > 0)
                {
                    for (int i = 0; i < drLayer.Length; i++)
                    {
                        string layerCode1 = drLayer[i]["LAYERCODE"].ToString();
                        JObject root1 = new JObject { { "id", drLayer[i]["LAYERCODE"].ToString() }, { "text", drLayer[i]["LAYERNAME"].ToString() } };
                        var NextCount = dtLayer.Select("LAYERCODE LIKE '" + layerCode1 + "%'");
                        if (NextCount.Count() > 2)
                        {
                            root1 = new JObject { { "id", drLayer[i]["LAYERCODE"].ToString() }, { "text", drLayer[i]["LAYERNAME"].ToString() }, { "state", "closed" } };
                            if (drLayer[i]["ISDEFAULTCH"].ToString() == "1")
                            {
                                root1 = new JObject { { "id", drLayer[i]["LAYERCODE"].ToString() }, { "text", drLayer[i]["LAYERNAME"].ToString() }, { "state", "closed" }, { "checked", true } };
                            }
                        }
                        root1.Add("children", getTckzTreeChild(dtLayer, layerCode1));
                        if (drLayer[i]["ISDEFAULTCH"].ToString() == "1")
                        {
                            root1 = new JObject { { "id", drLayer[i]["LAYERCODE"].ToString() }, { "text", drLayer[i]["LAYERNAME"].ToString() }, { "checked", true } };
                        }
                        childArray.Add(root1);
                    }
                    return childArray;
                }
            }
            if (layerCode.Length == 6)
            {
                DataRow[] drLayer = dtLayer.Select("Len(LAYERCODE) = '8' AND SUBSTRING(LAYERCODE,1,7)='" + layerCode + "0" + "'");
                if (drLayer.Length > 0)
                {
                    for (int i = 0; i < drLayer.Length; i++)
                    {
                        string layerCode1 = drLayer[i]["LAYERCODE"].ToString();
                        JObject root1 = new JObject { { "id", drLayer[i]["LAYERCODE"].ToString() }, { "text", drLayer[i]["LAYERNAME"].ToString() } };
                        var NextCount = dtLayer.Select("LAYERCODE LIKE '" + layerCode1 + "%'");
                        if (NextCount.Count() > 2)
                        {
                            root1 = new JObject { { "id", drLayer[i]["LAYERCODE"].ToString() }, { "text", drLayer[i]["LAYERNAME"].ToString() }, { "state", "closed" } };
                            if (drLayer[i]["ISDEFAULTCH"].ToString() == "1")
                            {
                                root1 = new JObject { { "id", drLayer[i]["LAYERCODE"].ToString() }, { "text", drLayer[i]["LAYERNAME"].ToString() }, { "state", "closed" }, { "checked", true } };
                            }
                        }
                        root1.Add("children", getTckzTreeChild(dtLayer, layerCode1));
                        if (drLayer[i]["ISDEFAULTCH"].ToString() == "1")
                        {
                            root1 = new JObject { { "id", drLayer[i]["LAYERCODE"].ToString() }, { "text", drLayer[i]["LAYERNAME"].ToString() }, { "checked", true } };
                        }
                        childArray.Add(root1);
                    }
                    return childArray;
                }
            }
            if (layerCode.Length == 8)
            {
                DataRow[] drLayer = dtLayer.Select("Len(LAYERCODE) = '10' AND SUBSTRING(LAYERCODE,1,9)='" + layerCode + "0" + "'");
                if (drLayer.Length > 0)
                {
                    for (int i = 0; i < drLayer.Length; i++)
                    {
                        string layerCode1 = drLayer[i]["LAYERCODE"].ToString();
                        JObject root1 = new JObject { { "id", drLayer[i]["LAYERCODE"].ToString() }, { "text", drLayer[i]["LAYERNAME"].ToString() } };
                        var NextCount = dtLayer.Select("LAYERCODE LIKE '" + layerCode1 + "%'");
                        if (NextCount.Count() > 2)
                        {
                            root1 = new JObject { { "id", drLayer[i]["LAYERCODE"].ToString() }, { "text", drLayer[i]["LAYERNAME"].ToString() }, { "state", "closed" } };
                            if (drLayer[i]["ISDEFAULTCH"].ToString() == "1")
                            {
                                root1 = new JObject { { "id", drLayer[i]["LAYERCODE"].ToString() }, { "text", drLayer[i]["LAYERNAME"].ToString() }, { "state", "closed" }, { "checked", true } };
                            }
                        }
                        root1.Add("children", getTckzTreeChild(dtLayer, layerCode1));
                        if (drLayer[i]["ISDEFAULTCH"].ToString() == "1")
                        {
                            root1 = new JObject { { "id", drLayer[i]["LAYERCODE"].ToString() }, { "text", drLayer[i]["LAYERNAME"].ToString() }, { "checked", true } };
                        }
                        childArray.Add(root1);
                    }
                    return childArray;
                }
            }
            return childArray;
        }

        /// <summary>
        /// 获取树形三维图层控制菜单(所有节点默认为false)
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getTckzTreeChecked(T_SYS_LAYER_SW sw)
        {
            JArray jObjects = new JArray();
            DataTable dtLayer = BaseDT.T_SYS_LAYER.getDT(sw);
            DataRow[] drLayer = dtLayer.Select("", "LAYERCODE");
            if (drLayer.Length > 0)
            {
                if (drLayer[0]["LAYERCODE"].ToString().Length == 2)
                {
                    JObject root = new JObject { { "id", drLayer[0]["LAYERCODE"].ToString() }, { "text", drLayer[0]["LAYERNAME"].ToString() } };
                    root.Add("children", getTckzTreeChildChecked(dtLayer, drLayer[0]["LAYERCODE"].ToString()));
                    jObjects.Add(root);
                }
                else
                {
                    JObject root = new JObject { { "id", drLayer[0]["LAYERCODE"].ToString() }, { "text", drLayer[0]["LAYERNAME"].ToString() } };
                    jObjects.Add(root);
                }
            }
            dtLayer.Clear();
            dtLayer.Dispose();
            return JsonConvert.SerializeObject(jObjects);
        }

        /// <summary>
        /// 获取树形三维图层控制菜单Child(所有节点默认为false)
        /// </summary>
        /// <param name="dtLayer"></param>
        /// <param name="layerCode"></param>
        /// <returns></returns>
        /// 
        private static JArray getTckzTreeChildChecked(DataTable dtLayer, string layerCode)
        {
            JArray childArray = new JArray();
            if (layerCode.Length == 2)
            {
                DataRow[] drLayer = dtLayer.Select("Len(LAYERCODE) = '4' AND SUBSTRING(LAYERCODE,1,3)='010'");//点，线，面
                if (drLayer.Length > 0)
                {
                    for (int i = 0; i < drLayer.Length; i++)
                    {
                        string layerCode1 = drLayer[i]["LAYERCODE"].ToString();
                        JObject root1 = new JObject { { "id", drLayer[i]["LAYERCODE"].ToString() }, { "text", drLayer[i]["LAYERNAME"].ToString() } };
                        root1.Add("children", getTckzTreeChildChecked(dtLayer, layerCode1));
                        childArray.Add(root1);
                    }
                    return childArray;
                }
            }
            if (layerCode.Length == 4)
            {
                DataRow[] drLayer = dtLayer.Select("(Len(LAYERCODE) = '6' AND SUBSTRING(LAYERCODE,1,5)='" + layerCode + "0" + "') OR (Len(LAYERCODE) = '6' AND SUBSTRING(LAYERCODE,1,5)='" + layerCode + "1" + "')");
                if (drLayer.Length > 0)
                {
                    for (int i = 0; i < drLayer.Length; i++)
                    {
                        string layerCode1 = drLayer[i]["LAYERCODE"].ToString();
                        JObject root1 = new JObject { { "id", drLayer[i]["LAYERCODE"].ToString() }, { "text", drLayer[i]["LAYERNAME"].ToString() } };
                        var NextCount = dtLayer.Select("LAYERCODE LIKE '" + layerCode1 + "%'");
                        if (NextCount.Count() > 2)
                        {
                            root1 = new JObject { { "id", drLayer[i]["LAYERCODE"].ToString() }, { "text", drLayer[i]["LAYERNAME"].ToString() }, { "state", "closed" } };
                        }
                        root1.Add("children", getTckzTreeChildChecked(dtLayer, layerCode1));
                        childArray.Add(root1);
                    }
                    return childArray;
                }
            }
            if (layerCode.Length == 6)
            {
                DataRow[] drLayer = dtLayer.Select("Len(LAYERCODE) = '8' AND SUBSTRING(LAYERCODE,1,7)='" + layerCode + "0" + "'");
                if (drLayer.Length > 0)
                {
                    for (int i = 0; i < drLayer.Length; i++)
                    {
                        string layerCode1 = drLayer[i]["LAYERCODE"].ToString();
                        JObject root1 = new JObject { { "id", drLayer[i]["LAYERCODE"].ToString() }, { "text", drLayer[i]["LAYERNAME"].ToString() } };
                        var NextCount = dtLayer.Select("LAYERCODE LIKE '" + layerCode1 + "%'");
                        if (NextCount.Count() > 2)
                        {
                            root1 = new JObject { { "id", drLayer[i]["LAYERCODE"].ToString() }, { "text", drLayer[i]["LAYERNAME"].ToString() }, { "state", "closed" } };
                        }
                        root1.Add("children", getTckzTreeChildChecked(dtLayer, layerCode1));
                        childArray.Add(root1);
                    }
                    return childArray;
                }
            }
            if (layerCode.Length == 8)
            {
                DataRow[] drLayer = dtLayer.Select("Len(LAYERCODE) = '10' AND SUBSTRING(LAYERCODE,1,9)='" + layerCode + "0" + "'");
                if (drLayer.Length > 0)
                {
                    for (int i = 0; i < drLayer.Length; i++)
                    {
                        string layerCode1 = drLayer[i]["LAYERCODE"].ToString();
                        JObject root1 = new JObject { { "id", drLayer[i]["LAYERCODE"].ToString() }, { "text", drLayer[i]["LAYERNAME"].ToString() } };
                        var NextCount = dtLayer.Select("LAYERCODE LIKE '" + layerCode1 + "%'");
                        if (NextCount.Count() > 2)
                        {
                            root1 = new JObject { { "id", drLayer[i]["LAYERCODE"].ToString() }, { "text", drLayer[i]["LAYERNAME"].ToString() }, { "state", "closed" } };
                        }
                        root1.Add("children", getTckzTreeChildChecked(dtLayer, layerCode1));
                        childArray.Add(root1);
                    }
                    return childArray;
                }
            }
            return childArray;
        }

        /// <summary>
        /// 获取树形图层用于火点周边查询
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getTreeFireQuery(T_SYS_LAYER_SW sw)
        {
            JArray jObjects = new JArray();
            DataTable dtLayer = BaseDT.T_SYS_LAYER.getDT(sw);
            if (dtLayer != null && dtLayer.Rows.Count > 0)
            {
                DataRow[] drOrg = dtLayer.Select("Len(LAYERCODE) = '2'");
                if (drOrg.Length > 0)
                {
                    JObject root = new JObject { { "id", drOrg[0]["LAYERID"].ToString() }, { "text", drOrg[0]["LAYERNAME"].ToString() } };
                    JArray childArray = new JArray();
                    DataRow[] drOrg1 = dtLayer.Select("Len(LAYERCODE) = '4' AND SUBSTRING(LAYERCODE,1,3)='010'");//点，线，面
                    for (int i = 0; i < drOrg1.Length; i++)
                    {
                        JObject root1 = new JObject { { "id", drOrg1[i]["LAYERID"].ToString() }, { "text", drOrg1[i]["LAYERNAME"].ToString() } };
                        JArray childArray1 = new JArray();
                        DataRow[] drOrg2 = dtLayer.Select("(ISFIREROUNDDEFAULT='1' AND SUBSTRING(LAYERCODE,1,5)='" + drOrg1[i]["LAYERCODE"].ToString() + "0" + "') OR (ISFIREROUNDDEFAULT='1' AND SUBSTRING(LAYERCODE,1,5)='" + drOrg1[i]["LAYERCODE"].ToString() + "1" + "')");
                        for (int j = 0; j < drOrg2.Length; j++)
                        {
                            JObject root2 = new JObject { { "id", drOrg2[j]["LAYERID"].ToString() }, { "text", drOrg2[j]["LAYERNAME"].ToString() } };
                            childArray1.Add(root2);
                        }
                        root1.Add("children", childArray1);
                        childArray.Add(root1);
                    }
                    root.Add("children", childArray);
                    jObjects.Add(root);
                }
            }
            dtLayer.Clear();
            dtLayer.Dispose();
            return JsonConvert.SerializeObject(jObjects);
        }

        /// <summary>
        /// 获取三维图层LAYERID用于火点周边查询
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getLayerFireLAYERID(T_SYS_LAYER_SW sw)
        {
            DataTable dt = BaseDT.T_SYS_LAYER.getDT(sw);
            string LAYERID = "";
            DataRow[] drOrg = dt.Select("ISFIREROUNDDEFAULT = '1'");
            for (int i = 0; i < drOrg.Length; i++)
            {
                if (i != drOrg.Length - 1)
                    LAYERID += drOrg[i]["LAYERID"].ToString() + ",";
                else
                    LAYERID += drOrg[i]["LAYERID"].ToString();
            }
            return LAYERID;
        }

        /// <summary>
        /// 获取三维图层LAYERID用于护林员周边查询
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getLayerHuLinYuanLAYERID(T_SYS_LAYER_SW sw)
        {
            DataTable dt = BaseDT.T_SYS_LAYER.getDT(sw);
            string LAYERID = "";
            DataRow[] drOrg = dt.Select("ISFUROUNDDEFAULT = '1'");
            for (int i = 0; i < drOrg.Length; i++)
            {
                if (i != drOrg.Length - 1)
                    LAYERID += drOrg[i]["LAYERID"].ToString() + ",";
                else
                    LAYERID += drOrg[i]["LAYERID"].ToString();
            }
            return LAYERID;
        }

        /// <summary>
        /// 获取三维图层控制LAYERNAME
        /// </summary>
        public static string getLayerNameStr(T_SYS_LAYER_SW sw)
        {
            DataTable dt = BaseDT.T_SYS_LAYER.getDT(sw);
            string LAYERNAME = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i != dt.Rows.Count - 1)
                    LAYERNAME += dt.Rows[i]["LAYERNAME"].ToString() + ",";
                else
                    LAYERNAME += dt.Rows[i]["LAYERNAME"].ToString();
            }
            return LAYERNAME;
        }

        /// <summary>
        /// 获取三维图层控制ISDEFAULTCH
        /// </summary>
        public static string getLayerDEFAULTCHStr(T_SYS_LAYER_SW sw)
        {
            DataTable dt = BaseDT.T_SYS_LAYER.getDT(sw);
            string ISDEFAULTCH = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i != dt.Rows.Count - 1)
                    ISDEFAULTCH += dt.Rows[i]["ISDEFAULTCH"].ToString() + ",";
                else
                    ISDEFAULTCH += dt.Rows[i]["ISDEFAULTCH"].ToString();
            }
            return ISDEFAULTCH;
        }

        /// <summary>
        /// 获取三维图层控制LAYERCODE
        /// </summary>
        public static string getLayerLAYERCODEStr(T_SYS_LAYER_SW sw)
        {
            DataTable dt = BaseDT.T_SYS_LAYER.getDT(sw);
            string LAYERCODE = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i != dt.Rows.Count - 1)
                    LAYERCODE += dt.Rows[i]["LAYERCODE"].ToString() + ",";
                else
                    LAYERCODE += dt.Rows[i]["LAYERCODE"].ToString();
            }
            return LAYERCODE;
        }

        /// <summary>
        /// 获取三维图层所有名称用于初始化隐藏
        /// </summary>
        public static string getLayerAllNAME()
        {
            DataTable dt = BaseDT.T_SYS_LAYER.getALLDT();
            string LAYERNAME = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i != dt.Rows.Count - 1)
                    LAYERNAME += dt.Rows[i]["LAYERNAME"].ToString() + ",";
                else
                    LAYERNAME += dt.Rows[i]["LAYERNAME"].ToString();
            }
            return LAYERNAME;
        }

        /// <summary>
        /// 获取空间库火情档案
        /// </summary>
        public static string getLayerYEAR()
        {
            DataTable dt = BaseDT.T_SYS_LAYER.getHQDADT();
            string YEAR = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i != dt.Rows.Count - 1)
                    YEAR += dt.Rows[i]["YEAR"].ToString() + ",";
                else
                    YEAR += dt.Rows[i]["YEAR"].ToString();
            }
            return YEAR;
        }

        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static T_SYS_LAYER_Model getModel(T_SYS_LAYER_SW sw)
        {
            DataTable dt = BaseDT.T_SYS_LAYER.getDT2(sw);
            T_SYS_LAYER_Model m = new T_SYS_LAYER_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                //数据库表字段
                m.LAYERCODE = dt.Rows[i]["LAYERCODE"].ToString();
                m.LAYERNAME = dt.Rows[i]["LAYERNAME"].ToString();
                m.LAYERID = dt.Rows[i]["LAYERID"].ToString();
                m.ISACTION = dt.Rows[i]["ISACTION"].ToString();
                m.LAYERRIGHTID = dt.Rows[i]["LAYERRIGHTID"].ToString();
                m.ISDEFAULTCH = dt.Rows[i]["ISDEFAULTCH"].ToString();
                m.ISFIREROUNDDEFAULT = dt.Rows[i]["ISFIREROUNDDEFAULT"].ToString();
                m.ISFUROUNDDEFAULT = dt.Rows[i]["ISFUROUNDDEFAULT"].ToString();
                m.LAYERPICNAME = dt.Rows[i]["LAYERPICNAME"].ToString();
                m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
                //扩充字段
            }
            dt.Clear();
            dt.Dispose();
            return m;
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw">参见模型PEST_REPORT_HAPPEN_SW</param>
        /// <returns>参见模型PEST_REPORT_HAPPEN_Model</returns>
        public static IEnumerable<T_SYS_LAYER_Model> getListModel(T_SYS_LAYER_SW sw)
        {
            var result = new List<T_SYS_LAYER_Model>();
            DataTable dt = BaseDT.T_SYS_LAYER.getDT2(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T_SYS_LAYER_Model m = new T_SYS_LAYER_Model();
                m.LAYERCODE = dt.Rows[i]["LAYERCODE"].ToString();
                m.LAYERNAME = dt.Rows[i]["LAYERNAME"].ToString();
                m.LAYERID = dt.Rows[i]["LAYERID"].ToString();
                m.ISACTION = dt.Rows[i]["ISACTION"].ToString();
                m.LAYERRIGHTID = dt.Rows[i]["LAYERRIGHTID"].ToString();
                m.ISDEFAULTCH = dt.Rows[i]["ISDEFAULTCH"].ToString();
                m.ISFIREROUNDDEFAULT = dt.Rows[i]["ISFIREROUNDDEFAULT"].ToString();
                m.ISFUROUNDDEFAULT = dt.Rows[i]["ISFUROUNDDEFAULT"].ToString();
                m.LAYERPICNAME = dt.Rows[i]["LAYERPICNAME"].ToString();
                m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }

        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(T_SYS_LAYER_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msg = BaseDT.T_SYS_LAYER.Add(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Mdy")
            {
                Message msg = BaseDT.T_SYS_LAYER.Mdy(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Del")
            {
                Message msg = BaseDT.T_SYS_LAYER.Del(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "PLMdy")
            {
                Message msg = BaseDT.T_SYS_LAYER.PLMdy(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            return new Message(false, "无效操作", "");
        }

    }
}
