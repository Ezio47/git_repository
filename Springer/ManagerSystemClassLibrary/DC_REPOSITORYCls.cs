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
    /// 数据中心_仓库表
    /// </summary>
    public class DC_REPOSITORYCls
    {
        #region 获取仓库下拉框
        /// <summary>
        /// 获取仓库下拉框
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getSelectOption(DC_REPOSITORY_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = BaseDT.DC_REPOSITORY.getDT(sw);
            if (sw.Other == "1")
                sb.AppendFormat("<option value=\"\">{0}</option>", "其他");
                sb.AppendFormat("<option value=\"办公室\">{0}</option>", "办公室");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string name = dt.Rows[i]["NAME"].ToString();
                string dcrepid = dt.Rows[i]["DCREPOSITORYID"].ToString();
                sb.AppendFormat("<option value=\"{0}\">{1}</option>", dcrepid,name);
            }
            dt.Clear();
            dt.Dispose();
            return sb.ToString();
        }
        #endregion

        #region 获取单条记录
        /// <summary>
        /// 单条记录
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DC_REPOSITORY_Model getModel(DC_REPOSITORY_SW sw) 
        {
            DataTable dt = BaseDT.DC_REPOSITORY.getDT(sw);//列表
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });
            DC_REPOSITORY_Model m = new DC_REPOSITORY_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.DCREPOSITORYID = dt.Rows[i]["DCREPOSITORYID"].ToString();
                m.NAME = dt.Rows[i]["NAME"].ToString();
                m.ADDRESS = dt.Rows[i]["ADDRESS"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.ORGName = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                m.RESPONSIBLEMAN = dt.Rows[i]["RESPONSIBLEMAN"].ToString();
                m.LINKWAY = dt.Rows[i]["LINKWAY"].ToString();
                m.REPTYPEID = dt.Rows[i]["REPTYPEID"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
            }
            dt.Clear();
            dt.Dispose();
            return m;
        }
        #endregion

        #region 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<DC_REPOSITORY_Model> getModelList(DC_REPOSITORY_SW sw)
        {
            var result = new List<DC_REPOSITORY_Model>();
            DataTable dt = BaseDT.DC_REPOSITORY.getDT(sw);//列表
             DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DC_REPOSITORY_Model m = new DC_REPOSITORY_Model();
                m.DCREPOSITORYID = dt.Rows[i]["DCREPOSITORYID"].ToString();
                m.NAME = dt.Rows[i]["NAME"].ToString();
                m.ADDRESS = dt.Rows[i]["ADDRESS"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.ORGName = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                //string orgname = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                m.COMNAME = "[" + m.ORGName + "]" + m.NAME + "";
                //m.COMNAME = "[" + orgname + "]" + m.NAME + "";
                m.RESPONSIBLEMAN = dt.Rows[i]["RESPONSIBLEMAN"].ToString();
                m.LINKWAY = dt.Rows[i]["LINKWAY"].ToString();
                m.REPTYPEID = dt.Rows[i]["REPTYPEID"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            return result;
        }
        #endregion

        #region 通过仓库id获取仓库名称
        /// <summary>
        /// 仓库id获取仓库名称
        /// </summary>
        /// <param name="dpid"></param>
        /// <returns></returns>
        public static string getdepotname(string dpid)
        {
            DC_REPOSITORY_Model m = getModel(new DC_REPOSITORY_SW { DCREPOSITORYID = dpid });
            return m.NAME;
        }
        #endregion 

        #region 通过仓库id获取仓库负责人
        /// <summary>
        /// 仓库id获取仓库名称仓库负责人
        /// </summary>
        /// <param name="dpid"></param>
        /// <returns></returns>
        public static string getdepotman(string dpid)
        {
            DC_REPOSITORY_Model m = getModel(new DC_REPOSITORY_SW { DCREPOSITORYID = dpid });
            return m.RESPONSIBLEMAN;
        }
        #endregion 

        #region 通过仓库id获取仓库组织机构码
        /// <summary>
        /// 仓库id获取仓库名称仓库组织机构码
        /// </summary>
        /// <param name="dpid"></param>
        /// <returns></returns>
        public static string getdepOrg(string dpid)
        {
            DC_REPOSITORY_Model m = getModel(new DC_REPOSITORY_SW { DCREPOSITORYID = dpid });
            return m.BYORGNO;
        }
        #endregion 

        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(DC_REPOSITORY_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msgUser = BaseDT.DC_REPOSITORY.Add(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
            }
            if (m.opMethod == "Mdy")
            {
                Message msgUser = BaseDT.DC_REPOSITORY.Mdy(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);

            }
            if (m.opMethod == "Del")
            {
                Message msgUser = BaseDT.DC_REPOSITORY.Del(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
            }
            return new Message(false, "无效操作", "");
        }
        #endregion

        #region 获取分页
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <param name="total">记录总数</param>
        /// <returns>参见模型</returns>
        public static IEnumerable<DC_REPOSITORY_Model> getModelList(DC_REPOSITORY_SW sw, out int total)
        {
            var result = new List<DC_REPOSITORY_Model>();
            DataTable dt = BaseDT.DC_REPOSITORY.getDT(sw, out total);//列表
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DC_REPOSITORY_Model m = new DC_REPOSITORY_Model();
                m.DCREPOSITORYID = dt.Rows[i]["DCREPOSITORYID"].ToString();
                m.NAME = dt.Rows[i]["NAME"].ToString();
                m.ADDRESS = dt.Rows[i]["ADDRESS"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                if (m.BYORGNO.Substring(6, 3) != "000" && m.BYORGNO.Substring(9, 6) == "000000")
                {
                    m.ORGName = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);

                }
                m.ORGXSName = BaseDT.T_SYS_ORG.getSXName(dtORG, m.BYORGNO);
                //m.ORGName = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                //string orgname = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                m.COMNAME = "[" + m.ORGName + "]" + m.NAME + "";
                m.RESPONSIBLEMAN = dt.Rows[i]["RESPONSIBLEMAN"].ToString();
                m.LINKWAY = dt.Rows[i]["LINKWAY"].ToString();
                m.REPTYPEID = dt.Rows[i]["REPTYPEID"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            return result;
        }
        #endregion

        #region 统计当前用户下的仓库的记录数量
        /// <summary>
        ///统计当前用户下的仓库的记录数量
        /// </summary>
        /// <returns></returns>
        public static string getCount()
        {
            var Count = "";
            Count = BaseDT.DC_REPOSITORY.getNum(new DC_REPOSITORY_SW { BYORGNO = SystemCls.getCurUserOrgNo() });
            return Count;
        }
        #endregion
    }
}
