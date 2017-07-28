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
    /// 用户_通讯录表
    /// </summary>
    public class T_SYS_ADDREDDBOOKCls
    {
        #region 增、删、改

        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(T_SYS_ADDREDDBOOK_Model m)
        {
            if (m.opMethod == "Add")
            {
                SystemCls.LogSave("3", "通讯录:" + m.ADNAME, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.T_SYS_ADDREDDBOOK.Add(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            if (m.opMethod == "Mdy")
            {
                SystemCls.LogSave("4", "通讯录:" + m.ADNAME, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.T_SYS_ADDREDDBOOK.Mdy(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);

            }
            if (m.opMethod == "Del")
            {
                SystemCls.LogSave("5", "通讯录:" + m.ADNAME, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.T_SYS_ADDREDDBOOK.Del(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            return new Message(false, "无效操作", "");


        }
        #endregion

        #region  根据查询条件获取某一条记录

        /// <summary>
        /// 根据查询条件获取某一条信息记录
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static T_SYS_ADDREDDBOOK_Model getModel(T_SYS_ADDREDDBOOK_SW sw)
        {
            DataTable dt = BaseDT.T_SYS_ADDREDDBOOK.getDT(sw);
            T_SYS_ADDREDDBOOK_Model m = new T_SYS_ADDREDDBOOK_Model();
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.ADID = dt.Rows[i]["ADID"].ToString();
                m.ATID = dt.Rows[i]["ATID"].ToString();
                m.ORGNO = dt.Rows[i]["ORGNO"].ToString();
                m.ADNAME = dt.Rows[i]["ADNAME"].ToString();
                m.USERJOB = dt.Rows[i]["USERJOB"].ToString();
                m.PHONE = dt.Rows[i]["PHONE"].ToString();
                m.Tell = dt.Rows[i]["Tell"].ToString();
                m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
                m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtORG, dt.Rows[i]["ORGNO"].ToString());

                DataTable dtType = BaseDT.T_SYS_ADDREDDTYPE.getDT(new T_SYS_ADDREDDTYPE_SW { ATID = m.ATID });
                m.ATName = BaseDT.T_SYS_ADDREDDTYPE.getName(dtType, m.ATID);
                dtType.Clear();
                dtType.Dispose();
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

        public static IEnumerable<T_SYS_ADDREDDBOOK_Model> getListModel(T_SYS_ADDREDDBOOK_SW sw)
        {
            // DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dt = BaseDT.T_SYS_ADDREDDBOOK.getDT(sw);//列表
            DataTable dtType = BaseDT.T_SYS_ADDREDDTYPE.getDT(new T_SYS_ADDREDDTYPE_SW { });
            var result = new List<T_SYS_ADDREDDBOOK_Model>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T_SYS_ADDREDDBOOK_Model m = new T_SYS_ADDREDDBOOK_Model();
                m.ADID = dt.Rows[i]["ADID"].ToString();
                m.ATID = dt.Rows[i]["ATID"].ToString();
                m.ORGNO = dt.Rows[i]["ORGNO"].ToString();
                m.ADNAME = dt.Rows[i]["ADNAME"].ToString();
                m.USERJOB = dt.Rows[i]["USERJOB"].ToString();
                m.PHONE = dt.Rows[i]["PHONE"].ToString();
                m.Tell = dt.Rows[i]["Tell"].ToString();
                m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
                //m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtORG, dt.Rows[i]["ORGNO"].ToString());
                m.ATName = BaseDT.T_SYS_ADDREDDTYPE.getName(dtType, m.ATID);
                result.Add(m);
            }
            dtType.Clear();
            dtType.Dispose();
            dt.Clear();
            dt.Dispose();
            return result;
        }

        #endregion

        #region 获取列表（分页）
        /// <summary>
        /// 获取用户列表（分页）
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <param name="total">记录总数</param>
        /// <returns>参见模型</returns>
        public static IEnumerable<T_SYS_ADDREDDBOOK_Model> getListModelPhotoPager(T_SYS_ADDREDDBOOK_SW sw, out int total)
        {
            var result = new List<T_SYS_ADDREDDBOOK_Model>();

            DataTable dt = BaseDT.T_SYS_ADDREDDBOOK.getDT(sw, out total);//用户列表
            DataTable dtType = BaseDT.T_SYS_ADDREDDTYPE.getDT(new T_SYS_ADDREDDTYPE_SW { });

            //DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                T_SYS_ADDREDDBOOK_Model m = new T_SYS_ADDREDDBOOK_Model();
                m.ADID = dt.Rows[i]["ADID"].ToString();
                m.ATID = dt.Rows[i]["ATID"].ToString();
                m.ORGNO = dt.Rows[i]["ORGNO"].ToString();
                m.ADNAME = dt.Rows[i]["ADNAME"].ToString();
                m.USERJOB = dt.Rows[i]["USERJOB"].ToString();
                m.PHONE = dt.Rows[i]["PHONE"].ToString();
                m.Tell = dt.Rows[i]["Tell"].ToString();
                m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
                //m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtORG, dt.Rows[i]["ORGNO"].ToString());
                m.ATName = BaseDT.T_SYS_ADDREDDTYPE.getName(dtType, m.ATID);
                result.Add(m);
            }
            dtType.Clear();
            dtType.Dispose();
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion


        #region 树形菜单
        /// <summary>
        /// 类别树形菜单
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>string</returns>
        public static string getTypeTree(T_SYS_ADDREDDTYPE_SW sw)
        {
            JArray childobjArray = new JArray();
            DataTable dt = BaseDT.T_SYS_ADDREDDTYPE.getDT(sw);
            if (string.IsNullOrEmpty(sw.RATID))
                sw.RATID = "0";
            if (sw.isTreeOpen == "0")
                sw.isTreeOpen = "closed";
            else
                sw.isTreeOpen = "open";
            JArray JA = getTreeChild(sw, dt, sw.RATID);//, dctypetopid);
            dt.Clear();
            dt.Dispose();
            return JsonConvert.SerializeObject(JA);
        }
        private static JArray getTreeUser(JArray childobjArray, T_SYS_ADDREDDTYPE_SW sw, string atid)
        {

            DataTable dtU = BaseDT.T_SYS_ADDREDDBOOK.getDT(new T_SYS_ADDREDDBOOK_SW { ATID = atid });
            for (int k = 0; k < dtU.Rows.Count; k++)
            {//{ADID}{PHONE}{ADNAME}{USERJOB}{ORDERBY}
                if (string.IsNullOrEmpty(sw.treeIDShowUserType))
                    sw.treeIDShowUserType = "{ADID}";
                if (string.IsNullOrEmpty(sw.treeNameShowUserType))
                    sw.treeNameShowUserType = "{ADNAME}[{USERJOB}]";
                string id = sw.treeIDShowUserType
                    .Replace("{ADID}", dtU.Rows[k]["ADID"].ToString())
                    .Replace("{PHONE}", dtU.Rows[k]["PHONE"].ToString())
                    .Replace("{ADNAME}", dtU.Rows[k]["ADNAME"].ToString())
                    .Replace("{USERJOB}", dtU.Rows[k]["USERJOB"].ToString())
                    .Replace("{ORDERBY}", dtU.Rows[k]["ORDERBY"].ToString());
                string name = sw.treeNameShowUserType
                    .Replace("{ADID}", dtU.Rows[k]["ADID"].ToString())
                    .Replace("{PHONE}", dtU.Rows[k]["PHONE"].ToString())
                    .Replace("{ADNAME}", dtU.Rows[k]["ADNAME"].ToString())
                    .Replace("{USERJOB}", dtU.Rows[k]["USERJOB"].ToString())
                    .Replace("{ORDERBY}", dtU.Rows[k]["ORDERBY"].ToString());
                id = id.Replace("<>", "").Replace("[]", "");
                name = name.Replace("<>", "").Replace("[]", "");
                JObject root1 = new JObject
                        {
                        {"id", id}
                        ,{"text",name}
                        ,{"rid",""}
                        ,{"flag","user"}
                        };
                childobjArray.Add(root1);

            }
            dtU.Clear();
            dtU.Dispose();
            return childobjArray;
        }
        /// <summary>
        /// 获取类别子菜单
        /// </summary>
        /// <param name="sw">sw</param>
        /// <param name="dt">dt</param>
        /// <param name="RATID">RATID</param>
        /// <returns></returns>
        private static JArray getTreeChild(T_SYS_ADDREDDTYPE_SW sw, DataTable dt, string RATID)
        {

            JArray childobjArray = new JArray();
            DataRow[] dr = dt.Select("RATID = " + RATID + "", "ORDERBY");


            getTreeUser(childobjArray, sw, RATID);
            for (int i = 0; i < dr.Length; i++)
            {
                string atid = "typeid" + dr[i]["ATID"].ToString();
                if (sw.treeIsShowTypeID == "0")
                    atid = "";
                JObject root = new JObject
                        {
                        {"id",atid}
                        ,{"text",dr[i]["RTNAME"].ToString()}
                        ,{"rid",dr[i]["RATID"].ToString()}
                        ,{"flag","type"}
                        ,{"state", sw.isTreeOpen},
                        };
                root.Add("children", getTreeChild(sw, dt, dr[i]["ATID"].ToString()));
                childobjArray.Add(root);
            }
            return childobjArray;
        }
        /// <summary>
        /// 类别树形菜单
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>string</returns>
        public static string getTypeTreeYU(T_SYS_ADDREDDTYPE_SW sw)
        {
            JArray childobjArray = new JArray();
            DataTable dt = BaseDT.T_SYS_ADDREDDTYPE.getDT(sw);
            if (string.IsNullOrEmpty(sw.RATID))
                sw.RATID = "0";
            if (sw.isTreeOpen == "0")
                sw.isTreeOpen = "closed";
            else
                sw.isTreeOpen = "open";
            JArray JA = getTreeChild(sw, dt, sw.RATID);//, dctypetopid);


            DataRow[] dr = dt.Select("", "ORDERBY");

            getTreeUser(childobjArray, sw, sw.RATID);
            for (int i = 0; i < dr.Length; i++)
            {
                string atid = "typeid" + dr[i]["ATID"].ToString();
                if (sw.treeIsShowTypeID == "0")
                    atid = "";
                JObject root = new JObject
                        {
                        {"id",atid}
                        ,{"text",dr[i]["RTNAME"].ToString()}
                        ,{"rid",dr[i]["RATID"].ToString()}
                        ,{"flag","type"}
                        ,{"state", sw.isTreeOpen},
                        };
                //root.Add("children", getTreeChild(sw, dt, dr[i]["ATID"].ToString()));
                childobjArray.Add(root);
            }

            dt.Clear();
            dt.Dispose();
            return JsonConvert.SerializeObject(childobjArray);
        }

        #endregion

    }
}
