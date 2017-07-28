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
    /// 数据中心_专职人员
    /// </summary>
    public class DC_FULLTIMEUSERCls
    {
        #region 专职人员树形子菜单-市县平级
        /// <summary>
        /// 专职人员树形菜单
        /// </summary>
        /// <param name="dtOrg">组织机构表</param>
        /// <param name="dtUser">专职人员表</param>
        /// <returns></returns>
        public static JArray getFULLTIMEUSERORGChild(DataTable dtOrg, DataTable dtUser)
        {
            JArray childArray = new JArray();
            for (int i = 0; i < dtOrg.Rows.Count; i++)
            {
                if (dtOrg.Rows[i]["ORGNO"].ToString().Substring(6, 3) == "000")
                {
                    JObject root = new JObject
                {
                    {"id",dtOrg.Rows[i]["ORGNO"].ToString()},
                    {"text",dtOrg.Rows[i]["ORGNAME"].ToString()},
                    {"type","1"},
                    {"flag","0"}
                };
                    var user = dtUser.Select("BYORGNO='" + dtOrg.Rows[i]["ORGNO"].ToString() + "'", "");
                    root.Add("children", getFULLTIMEUSERChild(user));
                    childArray.Add(root);
                }
            }
            return childArray;
        }
        /// <summary>
        /// 专职人员树形子菜单
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static JArray getFULLTIMEUSERChild(DataRow[] user)
        {
            JArray childArray = new JArray();
            for (int i = 0; i < user.Length; i++)
            {
                JObject root = new JObject
                {
                    {"id",user[i]["DC_FULLTIMEUSERID"].ToString()},
                    {"text",user[i]["FTNAME"].ToString()},
                    {"type","1"},
                    {"flag","1"}
                };
                childArray.Add(root);
            }

            return childArray;
        }
        #endregion
        //#region 专职人员树形菜单
        //public static JArray getFULLTIMEUSERTree()
        //{
        //    JArray jObjects = new JArray();
        //    DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });
        //    DataTable dtUser = BaseDT.DC_FULLTIMEUSER.getDT(new DC_FULLTIMEUSER_SW { });
        //    DataRow[] drOrg = dtOrg.Select("", "ORGNO");
        //    if (drOrg.Length > 0)
        //    {

        //        JObject root = new JObject
        //             {
        //                 {"id",""},//ORGNO
        //                 {"text",drOrg[0]["ORGNAME"].ToString()} 
        //             };
        //        root.Add("children", getFULLTIMEUSERChild(dtOrg, dtUser, drOrg[0]["ORGNO"].ToString()));
        //        jObjects.Add(root);
        //    }
        //    return jObjects;
        //}
        //public static JArray getFULLTIMEUSERChild(DataTable dtOrg, DataTable dtUser, string orgNo)
        //{
        //    JArray childArray = new JArray();
        //    if (orgNo.Substring(4, 5) == "00000")//获取所有市下属的人员和县
        //    {
        //        DataRow[] drUser = dtUser.Select("BYORGNO = '" + orgNo + "'", "");
        //        for (int i = 0; i < drUser.Length; i++)
        //        {
        //            JObject root = new JObject
        //             {
        //                 {"id",drUser[i]["DC_FULLTIMEUSERID"].ToString()} ,
        //                 {"text",drUser[i]["FTNAME"].ToString()} 
        //             };
        //            childArray.Add(root);
        //        }
        //        DataRow[] drOrg = dtOrg.Select("SUBSTRING(ORGNO,1,4) = '" + orgNo.Substring(0, 4) + "' AND ORGNO<>'" + orgNo + "' and SUBSTRING(ORGNO,7,3)='000'", "");//获取所有县且〈〉市的
        //        for (int i = 0; i < drOrg.Length; i++)
        //        {
        //            JObject root = new JObject
        //             {
        //                 {"id",""},//ORGNO
        //                 {"text",drOrg[i]["ORGNAME"].ToString()}
        //             };
        //            root.Add("children", getFULLTIMEUSERChild(dtOrg, dtUser, drOrg[i]["ORGNO"].ToString()));//继续获取镇
        //            childArray.Add(root);
        //        }
        //        return childArray;
        //    }
        //    else if (orgNo.Substring(6, 3) == "000")//获取所有县的人员
        //    {
        //        DataRow[] drUser = dtUser.Select("BYORGNO = '" + orgNo + "'", "");
        //        for (int i = 0; i < drUser.Length; i++)
        //        {
        //            JObject root = new JObject
        //             {
        //                 {"id",drUser[i]["DC_FULLTIMEUSERID"].ToString()} ,
        //                 {"text",drUser[i]["FTNAME"].ToString()} 
        //             };
        //            childArray.Add(root);
        //        }
        //        return childArray;
        //    }
        //    return childArray;
        //}
        //#endregion
        #region 获取单条专职人员
        /// <summary>
        /// 单条专职人员
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DC_FULLTIMEUSER_Model getModel(DC_FULLTIMEUSER_SW sw ) 
        {
            DataTable dt = BaseDT.DC_FULLTIMEUSER.getDT(sw);
            DC_FULLTIMEUSER_Model m = new DC_FULLTIMEUSER_Model();
            if (dt.Rows.Count>0)
            {
                int i = 0;
                m.DC_FULLTIMEUSERID = dt.Rows[i]["DC_FULLTIMEUSERID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.FTNAME = dt.Rows[i]["FTNAME"].ToString();
                m.BIRTH = dt.Rows[i]["BIRTH"].ToString();
                m.SEX = dt.Rows[i]["SEX"].ToString();
                m.NATION = dt.Rows[i]["NATION"].ToString();
                m.USERJOB = dt.Rows[i]["USERJOB"].ToString();
                m.LINKWAY = dt.Rows[i]["LINKWAY"].ToString();
                m.PHOTO = dt.Rows[i]["PHOTO"].ToString();
                m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
            }
            dt.Dispose();
            dt.Clear();
            return m;
        }
        #endregion
        #region 获取专职人员列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<DC_FULLTIMEUSER_Model> getListModel(DC_FULLTIMEUSER_SW sw)
        {
            DataTable dt = BaseDT.DC_FULLTIMEUSER.getDT(sw);
            var result = new List<DC_FULLTIMEUSER_Model>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DC_FULLTIMEUSER_Model m = new DC_FULLTIMEUSER_Model();
                m.DC_FULLTIMEUSERID = dt.Rows[i]["DC_FULLTIMEUSERID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.FTNAME = dt.Rows[i]["FTNAME"].ToString();
                m.BIRTH = dt.Rows[i]["BIRTH"].ToString();
                m.SEX = dt.Rows[i]["SEX"].ToString();
                m.NATION = dt.Rows[i]["NATION"].ToString();
                m.USERJOB = dt.Rows[i]["USERJOB"].ToString();
                m.PHOTO = dt.Rows[i]["PHOTO"].ToString();
                m.LINKWAY = dt.Rows[i]["LINKWAY"].ToString();
                m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion
    }
}
