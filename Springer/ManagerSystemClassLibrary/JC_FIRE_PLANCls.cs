using DataBaseClassLibrary;
using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using PublicClassLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 火灾级别预案表
    /// </summary>
    public class JC_FIRE_PLANCls
    {

        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(JC_FIRE_PLAN_Model m)
        {
            if (m.opMethod == "Add")
            {
                //SystemCls.LogSave("3", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.JC_FIRE_PLAN.Add(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            if (m.opMethod == "Mdy")
            {
                //SystemCls.LogSave("4", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.JC_FIRE_PLAN.Mdy(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);

            }
            if (m.opMethod == "Del")
            {
                //SystemCls.LogSave("5", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.JC_FIRE_PLAN.Del(m);
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
        public static JC_FIRE_PLAN_Model getModel(JC_FIRE_PLAN_SW sw)
        {
            var result = new List<JC_FIRE_PLAN_Model>();

            DataTable dt = BaseDT.JC_FIRE_PLAN.getDT(sw);//列表

            JC_FIRE_PLAN_Model m = new JC_FIRE_PLAN_Model();

            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.JC_FIRE_PLANID = dt.Rows[i]["JC_FIRE_PLANID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.FIRELEVEL = dt.Rows[i]["FIRELEVEL"].ToString();
                m.PLANTITLE = dt.Rows[i]["PLANTITLE"].ToString();
                m.PLANCONTENT = dt.Rows[i]["PLANCONTENT"].ToString();
                m.PLANFILENAME = dt.Rows[i]["PLANFILENAME"].ToString();

                DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
                m.BYORGNOName = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);//单位名称
                DataTable dtFIRELEVEL = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "22" });//预案
                m.FIRELEVELName = BaseDT.T_SYS_DICT.getName(dtFIRELEVEL, m.FIRELEVEL);

                dtFIRELEVEL.Clear();
                dtFIRELEVEL.Dispose();
                dtFIRELEVEL.Clear();
                dtFIRELEVEL.Dispose();
            }
            dt.Clear();
            dt.Dispose();
            return m;
        }

        #endregion

        #region 获取预案列表
       /// <summary>
        /// 获取预案列表
       /// </summary>
       /// <param name="sw">sw</param>
       /// <param name="org">org</param>
       /// <returns></returns>
        public static IEnumerable<JC_FIRE_PLAN_Model> getModelList(JC_FIRE_PLAN_SW sw, string org = "")
        {
            var result = new List<JC_FIRE_PLAN_Model>();

            DataTable dt = BaseDT.JC_FIRE_PLAN.getDT(sw);//列表

            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dtFIRELEVEL = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "22" });//火险等级
            if (dt != null)
            {
                ArrayList aList = new ArrayList();
                if (PublicCls.OrgIsZhen(org))//乡镇
                {
                    aList.Add(org);
                }
                aList.Add(PublicCls.getXianIncOrgNo(org) + "000");//县
                aList.Add(PublicCls.getShiIncOrgNo(org) + "00000");//市
                for (int i = 0; i < aList.Count; i++)
                {
                    DataRow[] arrayDR = dt.Select(string.Format("BYORGNO='{0}'", aList[i].ToString()));
                    var list = GetYAList(arrayDR, dtORG, dtFIRELEVEL, org);
                    result.AddRange(list);
                }
            }

            dt.Clear();
            dt.Dispose();
            dtFIRELEVEL.Clear();
            dtFIRELEVEL.Dispose();
            dtFIRELEVEL.Clear();
            dtFIRELEVEL.Dispose();
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
        public static IEnumerable<JC_FIRE_PLAN_Model> getModelList(JC_FIRE_PLAN_SW sw, out int total)
        {
            var result = new List<JC_FIRE_PLAN_Model>();

            DataTable dt = BaseDT.JC_FIRE_PLAN.getDT(sw, out total);//列表

            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dtFIRELEVEL = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "22" });//预案

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                JC_FIRE_PLAN_Model m = new JC_FIRE_PLAN_Model();
                m.JC_FIRE_PLANID = dt.Rows[i]["JC_FIRE_PLANID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.FIRELEVEL = dt.Rows[i]["FIRELEVEL"].ToString();
                m.PLANTITLE = dt.Rows[i]["PLANTITLE"].ToString();
                m.PLANCONTENT = dt.Rows[i]["PLANCONTENT"].ToString();
                m.PLANFILENAME = dt.Rows[i]["PLANFILENAME"].ToString();

                m.BYORGNOName = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);//单位名称
                m.FIRELEVELName = BaseDT.T_SYS_DICT.getName(dtFIRELEVEL, m.FIRELEVEL);

                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtFIRELEVEL.Clear();
            dtFIRELEVEL.Dispose();
            dtFIRELEVEL.Clear();
            dtFIRELEVEL.Dispose();
            return result;
        }
        #endregion

        #region Private
        /// <summary>
        /// 获取相应单位的火线等级预案
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="dtORG"></param>
        /// <param name="dtFIRELEVEL"></param>
        /// <param name="org"></param>
        /// <returns></returns>
        private static IEnumerable<JC_FIRE_PLAN_Model> GetYAList(DataRow[] dr, DataTable dtORG, DataTable dtFIRELEVEL, string org)
        {
            var result = new List<JC_FIRE_PLAN_Model>();
            for (int i = 0; i < dr.Length; i++)
            {
                JC_FIRE_PLAN_Model m = new JC_FIRE_PLAN_Model();
                m.JC_FIRE_PLANID = dr[i]["JC_FIRE_PLANID"].ToString();
                m.BYORGNO = dr[i]["BYORGNO"].ToString();
                m.FIRELEVEL = dr[i]["FIRELEVEL"].ToString();
                m.PLANTITLE = dr[i]["PLANTITLE"].ToString();
                m.PLANCONTENT = dr[i]["PLANCONTENT"].ToString();
                m.PLANFILENAME = dr[i]["PLANFILENAME"].ToString();
                m.BYORGNOName = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);//单位名称
                m.FIRELEVELName = BaseDT.T_SYS_DICT.getName(dtFIRELEVEL, m.FIRELEVEL);

                result.Add(m);
            }
            return result;
        }
        #endregion
    }
}
