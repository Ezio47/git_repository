using DataBaseClassLibrary;
using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using PublicClassLibrary;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 野生动物分布
    /// </summary>
    public class WILD_ANIMALDISTRIBUTECls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(WILD_ANIMALDISTRIBUTE_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msgUser = BaseDT.WILD_ANIMALDISTRIBUTE.Add(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
            }
            if (m.opMethod == "Mdy")
            {
                Message msgUser = BaseDT.WILD_ANIMALDISTRIBUTE.Mdy(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);

            }
            //if (m.opMethod == "MdyJWD")
            //{
            //    //SystemCls.LogSave("4", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
            //    Message msgUser = BaseDT.WILD_ANIMALDISTRIBUTE.MdyJWD(m);
            //    return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);

            //}
            if (m.opMethod == "Del")
            {
                Message msgUser = BaseDT.WILD_ANIMALDISTRIBUTE.Del(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            return new Message(false, "无效操作", "");


        }

        #endregion

        #region 获取单条
        /// <summary>
        /// 根据查询条件获取某一条用户信息记录，用于修改、删除、用户登录验证
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static WILD_ANIMALDISTRIBUTE_Model getModel(WILD_ANIMALDISTRIBUTE_SW sw)
        {
            DataTable dt = BaseDT.WILD_ANIMALDISTRIBUTE.getDT(sw);//列表
            DataTable dt55 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "55" });//数据中心族群
            WILD_ANIMALDISTRIBUTE_Model m = new WILD_ANIMALDISTRIBUTE_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.WILD_ANIMALDISTRIBUTEID = dt.Rows[i]["WILD_ANIMALDISTRIBUTEID"].ToString();
                m.BIOLOGICALTYPECODE = dt.Rows[i]["BIOLOGICALTYPECODE"].ToString();
                m.BIOLOGICALTYPEName = T_SYS_BIOLOGICALTYPECls.getName(m.BIOLOGICALTYPECODE);
                m.POPULATIONTYPE = dt.Rows[i]["POPULATIONTYPE"].ToString();
                //m.POPULATIONTYPEName = T_SYS_BIOLOGICALTYPECls.getName(m.POPULATIONTYPE);
                m.POPULATIONTYPEName = BaseDT.T_SYS_DICT.getName(dt55, m.POPULATIONTYPE);
                m.WD = dt.Rows[i]["WD"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.JWDLIST = dt.Rows[i]["JWDLIST"].ToString();
                m.ANIMALCOUNT = dt.Rows[i]["ANIMALCOUNT"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
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
        public static IEnumerable<WILD_ANIMALDISTRIBUTE_Model> getListModel(WILD_ANIMALDISTRIBUTE_SW sw)
        {
            var result = new List<WILD_ANIMALDISTRIBUTE_Model>();
            DataTable dt = BaseDT.WILD_ANIMALDISTRIBUTE.getDT(sw);
            DataTable dt55 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "55" });//数据中心族群
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                WILD_ANIMALDISTRIBUTE_Model m = new WILD_ANIMALDISTRIBUTE_Model();
                m.WILD_ANIMALDISTRIBUTEID = dt.Rows[i]["WILD_ANIMALDISTRIBUTEID"].ToString();
                m.BIOLOGICALTYPECODE = dt.Rows[i]["BIOLOGICALTYPECODE"].ToString();
                m.BIOLOGICALTYPEName = T_SYS_BIOLOGICALTYPECls.getName(m.BIOLOGICALTYPECODE);
                m.POPULATIONTYPE = dt.Rows[i]["POPULATIONTYPE"].ToString();
                m.POPULATIONTYPEName = BaseDT.T_SYS_DICT.getName(dt55, m.POPULATIONTYPE);
                m.WD = dt.Rows[i]["WD"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.JWDLIST = dt.Rows[i]["JWDLIST"].ToString();
                m.ANIMALCOUNT = dt.Rows[i]["ANIMALCOUNT"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
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
        public static IEnumerable<WILD_ANIMALDISTRIBUTE_Model> getModelList(WILD_ANIMALDISTRIBUTE_SW sw, out int total)
        {
            var result = new List<WILD_ANIMALDISTRIBUTE_Model>();

            DataTable dt = BaseDT.WILD_ANIMALDISTRIBUTE.getDT(sw, out total);//列表
            DataTable dt55 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "55" });//数据中心族群
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                WILD_ANIMALDISTRIBUTE_Model m = new WILD_ANIMALDISTRIBUTE_Model();
                m.WILD_ANIMALDISTRIBUTEID = dt.Rows[i]["WILD_ANIMALDISTRIBUTEID"].ToString();
                m.BIOLOGICALTYPECODE = dt.Rows[i]["BIOLOGICALTYPECODE"].ToString();
                m.BIOLOGICALTYPEName = T_SYS_BIOLOGICALTYPECls.getName(m.BIOLOGICALTYPECODE);
                m.POPULATIONTYPE = dt.Rows[i]["POPULATIONTYPE"].ToString();
                m.POPULATIONTYPEName = BaseDT.T_SYS_DICT.getName(dt55, m.POPULATIONTYPE);
                m.WD = dt.Rows[i]["WD"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.JWDLIST = dt.Rows[i]["JWDLIST"].ToString();
                m.ANIMALCOUNT = dt.Rows[i]["ANIMALCOUNT"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion

        #region 树形菜单
        /// <summary>
        /// 类别树形菜单
        /// </summary>
        /// <param name="sw">idcode</param>
        /// <returns></returns>
        public static string GetTypeTree(WILD_LOCALANIMAL_SW sw)
        {
            DataTable dt = BaseDT.WILD_LOCALANIMAL.getDT(sw);
            JArray JA = GetTreeChild(dt, sw.BIOLOGICALTYPECODE);
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
                    dr = dt.Select("SUBSTRING(BIOLOGICALTYPECODE,11,2) <> '00' AND SUBSTRING(BIOLOGICALTYPECODE,13,2)='00'");
                else
                {
                    string str = "";
                    if (PublicCls.BioCodeIsKe(code))
                    {
                        str = PublicCls.GetKeBioCode(code);
                        dr = dt.Select("SUBSTRING(BIOLOGICALTYPECODE,1,10) = '" + str + "' AND SUBSTRING(BIOLOGICALTYPECODE,11,2)<>'00' AND SUBSTRING(BIOLOGICALTYPECODE,13,2)='00'");
                    }
                    else if (PublicCls.BioCodeIsShu(code))
                    {
                        str = PublicCls.GetShuBioCode(code);
                        dr = dt.Select("SUBSTRING(BIOLOGICALTYPECODE,1,12) = '" + str + "' AND SUBSTRING(BIOLOGICALTYPECODE,13,2)<>'00'");
                    }
                    else
                    {
                        str = PublicCls.GetZhongBioCode(code);
                        dr = dt.Select("SUBSTRING(BIOLOGICALTYPECODE,1,14) = '" + str + "' AND LEN(BIOLOGICALTYPECODE)>'" + code.Length + "'");
                    }
                }
                if (dr != null && dr.Count() > 0)
                {
                    for (int i = 0; i < dr.Count(); i++)
                    {
                        code = dr[i]["BIOLOGICALTYPECODE"].ToString();
                        JObject root = new JObject 
                        { 
                            { "id", dr[i]["BIOLOGICALTYPECODE"].ToString() }, 
                            { "text", dr[i]["BIOLOGICALTYPECODENAME"].ToString()},
                            { "state","closed" } 
                        };
                        root.Add("children", GetTreeChild(dt, code));
                        childobjArray.Add(root);
                    }
                }
            }
            return childobjArray;
        }
        #endregion

        #region 获取本地化野生动物的下拉框
        /// <summary>
        /// 获取本地化野生动物的下拉框
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static string getSelectOption(WILD_LOCALANIMAL_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = BaseDT.WILD_LOCALANIMAL.getDISDT(sw);

            if (sw.isShowAll == "1")
                sb.AppendFormat("<option value=\"\">{0}</option>", "--所有--");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (sw.BIOLOGICALTYPECODE == dt.Rows[i]["BIOLOGICALTYPECODE"].ToString())
                    sb.AppendFormat("<option value=\"{0}\" selected>{1}</option>", dt.Rows[i]["BIOLOGICALTYPECODE"].ToString(), T_SYS_BIOLOGICALTYPECls.getName(dt.Rows[i]["BIOLOGICALTYPECODE"].ToString()));
                else
                    sb.AppendFormat("<option value=\"{0}\">{1}</option>", dt.Rows[i]["BIOLOGICALTYPECODE"].ToString(), T_SYS_BIOLOGICALTYPECls.getName(dt.Rows[i]["BIOLOGICALTYPECODE"].ToString()));
            }
            dt.Clear();
            dt.Dispose();
            return sb.ToString();
        }
        #endregion

        #region 获取动物的记录数量
        /// <summary>
        ///获取植物的记录数量
        /// </summary>
        /// <returns></returns>
        public static string getCount()
        {
            var Count = "";
            Count = BaseDT.WILD_ANIMALDISTRIBUTE.getNum(new WILD_ANIMALDISTRIBUTE_SW { });
            return Count;
        }
        #endregion
    }
}
