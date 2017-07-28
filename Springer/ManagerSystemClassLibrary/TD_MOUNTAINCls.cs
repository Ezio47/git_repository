using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel.SDEModel;
using ManagerSystemModel;
using PublicClassLibrary;
using ManagerSystemClassLibrary.BaseDT.SDE;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 三维山数据
    /// </summary>
    public class TD_MOUNTAINCls
    {
        #region 山数据管理
        /// <summary>
        /// 山数据管理
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message Manager(TD_MOUNTAIN_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msgUser = BaseDT.SDE.TD_MOUNTAIN.Add(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
            }
            if (m.opMethod == "Mdy")
            {
                Message msgUser = BaseDT.SDE.TD_MOUNTAIN.Mdy(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
            }

            if (m.opMethod == "Del")
            {
                Message msgUser = BaseDT.SDE.TD_MOUNTAIN.Del(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
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
        public static TD_MOUNTAIN_Model getModel(TD_MOUNTAIN_SW sw)
        {
            var result = new List<TD_MOUNTAIN_Model>();

            DataTable dt = BaseDT.SDE.TD_MOUNTAIN.getDT(sw);//列表
            DataTable dt51 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "51" });//类型
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位

            TD_MOUNTAIN_Model m = new TD_MOUNTAIN_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.OBJECTID = dt.Rows[i]["OBJECTID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.BYORGNOXS = dt.Rows[i]["BYORGNOXS"].ToString();
                m.ORGXSNAME = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNOXS);
                m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                m.NAME = dt.Rows[i]["NAME"].ToString();
                string jd = dt.Rows[i]["DISPLAY_X"].ToString();
                string wd = dt.Rows[i]["DISPLAY_Y"].ToString();
                m.JD = Convert.ToDouble(jd).ToString("F6");
                m.WD = Convert.ToDouble(wd).ToString("F6");
                m.VILLAGE = dt.Rows[i]["VILLAGE"].ToString();
                m.Shape = dt.Rows[i]["Shape"].ToString();
                m.TYPE = dt.Rows[i]["TYPE"].ToString();
                m.TYPENAME = BaseDT.T_SYS_DICT.getName(dt51, m.TYPE);
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
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<TD_MOUNTAIN_Model> getModelList(TD_MOUNTAIN_SW sw)
        {
            var result = new List<TD_MOUNTAIN_Model>();

            DataTable dt = BaseDT.SDE.TD_MOUNTAIN.getDT(sw);//列表
            DataTable dt51 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "51" });//类型
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TD_MOUNTAIN_Model m = new TD_MOUNTAIN_Model();
                m.OBJECTID = dt.Rows[i]["OBJECTID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                m.BYORGNOXS = dt.Rows[i]["BYORGNOXS"].ToString();
                m.ORGXSNAME = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNOXS);
                m.NAME = dt.Rows[i]["NAME"].ToString();
                string jd = dt.Rows[i]["DISPLAY_X"].ToString();
                string wd = dt.Rows[i]["DISPLAY_Y"].ToString();
                m.JD = Convert.ToDouble(jd).ToString("F6");
                m.WD = Convert.ToDouble(wd).ToString("F6");
                m.VILLAGE = dt.Rows[i]["VILLAGE"].ToString();
                m.Shape = dt.Rows[i]["Shape"].ToString();
                m.TYPE = dt.Rows[i]["TYPE"].ToString();
                m.TYPENAME = BaseDT.T_SYS_DICT.getName(dt51, m.TYPE);
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            return result;
        }

        #endregion

        #region 获取分页列表
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="sw">sw</param>
        /// <param name="total">total</param>
        /// <returns></returns>
        public static IEnumerable<TD_MOUNTAIN_Model> getModelList(TD_MOUNTAIN_SW sw, out int total)
        {
            var result = new List<TD_MOUNTAIN_Model>();

            DataTable dt = BaseDT.SDE.TD_MOUNTAIN.getDT(sw, out total);//列表
            DataTable dt51 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "51" });//类型
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TD_MOUNTAIN_Model m = new TD_MOUNTAIN_Model();
                m.OBJECTID = dt.Rows[i]["OBJECTID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                m.BYORGNOXS = dt.Rows[i]["BYORGNOXS"].ToString();
                m.ORGXSNAME = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNOXS);
                m.NAME = dt.Rows[i]["NAME"].ToString();
                string jd = dt.Rows[i]["DISPLAY_X"].ToString();
                string wd = dt.Rows[i]["DISPLAY_Y"].ToString();
                m.JD = Convert.ToDouble(jd).ToString("F6");
                m.WD = Convert.ToDouble(wd).ToString("F6");
                m.VILLAGE = dt.Rows[i]["VILLAGE"].ToString();
                m.Shape = dt.Rows[i]["Shape"].ToString();
                m.TYPE = dt.Rows[i]["TYPE"].ToString();
                m.TYPENAME = BaseDT.T_SYS_DICT.getName(dt51,m.TYPE);
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            return result;
        }

        #endregion

        #region 统计当前用户下的自定义数据的数量
        /// <summary>
        ///统计当前用户下的自定义数据的数量
        /// </summary>
        /// <returns></returns>
        public static string getCount()
        {
            var Count = "";
            Count = BaseDT.SDE.TD_MOUNTAIN.getNum(SystemCls.getCurUserOrgNo());
            return Count;
        }
        #endregion
    }
}
