using DataBaseClassLibrary;
using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
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
    /// 预警响应相关工作表
    /// </summary>
    public class YJ_XY_WORKCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(YJ_XY_WORK_Model m)
        {
            if (string.IsNullOrEmpty(m.YJXYCONTENT)==false)
                m.YJXYCONTENT = m.YJXYCONTENT.Replace("\r\n", "<br>").Replace("\r", "<br>").Replace("\n", "<br>");//.Replace(@"\\n", "<br>").Replace(@"\r", "<br>").Replace(@"\n", "<br>").Replace(@"\r\n", "<br>");
            if (m.opMethod == "Add")
            {
                //SystemCls.LogSave("3", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.YJ_XY_WORK.Add(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            if (m.opMethod == "Mdy")
            {
                //SystemCls.LogSave("4", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.YJ_XY_WORK.Mdy(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);

            }
            if (m.opMethod == "Del")
            {
                //SystemCls.LogSave("5", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.YJ_XY_WORK.Del(m);
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
        public static YJ_XY_WORK_Model getModel(YJ_XY_WORK_SW sw)
        {
            var result = new List<YJ_XY_WORK_Model>();

            DataTable dt = BaseDT.YJ_XY_WORK.getDT(sw);//列表

            YJ_XY_WORK_Model m = new YJ_XY_WORK_Model();

            DataTable dt24 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "24" });//火情预警等级
            DataTable dt25 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "25" });//预警响应部门
            
                m.DANGERCLASS = sw.DANGERCLASS;
                m.YJXYDEPT = sw.YJXYDEPT;
                m.YJXYID = BaseDT.YJ_XY_WORK.getID(dt, m.DANGERCLASS, m.YJXYDEPT);
                m.YJXYCONTENT = BaseDT.YJ_XY_WORK.getContent(dt, m.DANGERCLASS, m.YJXYDEPT).Replace("<br>", "\n");
                m.DANGERCLASSName = BaseDT.T_SYS_DICT.getName(dt24, m.DANGERCLASS);
                m.YJXYDEPTName = BaseDT.T_SYS_DICT.getName(dt25, m.YJXYDEPT);
                result.Add(m);
           
            dt24.Clear();
            dt24.Dispose();
            dt25.Clear();
            dt25.Dispose();
            dt.Clear();
            dt.Dispose();
            return m;
        }

        #endregion

        #region 获取响应列表
        /// <summary>
        /// 获取文档列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<YJ_XY_WORK_Model> getModelList(YJ_XY_WORK_SW sw)
        {
            var result = new List<YJ_XY_WORK_Model>();

            DataTable dt = BaseDT.YJ_XY_WORK.getDT(sw);//列表

            DataTable dt24 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "24" });//火情预警等级
            DataTable dt25 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "25" });//预警响应部门
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                YJ_XY_WORK_Model m = new YJ_XY_WORK_Model();
                m.YJXYID = dt.Rows[i]["YJXYID"].ToString();
                m.DANGERCLASS = dt.Rows[i]["DANGERCLASS"].ToString();
                m.YJXYDEPT = dt.Rows[i]["YJXYDEPT"].ToString();
                m.YJXYCONTENT = dt.Rows[i]["YJXYCONTENT"].ToString();
                m.DANGERCLASSName = BaseDT.T_SYS_DICT.getName(dt24, m.DANGERCLASS);
                m.YJXYDEPTName = BaseDT.T_SYS_DICT.getName(dt24, m.YJXYDEPT);
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dt24.Clear();
            dt24.Dispose();
            dt25.Clear();
            dt25.Dispose();
            return result;
        }

        #endregion

        #region 获取所有响应列表 含未录入内容的
        /// <summary>
        /// 获取所有响应列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<YJ_XY_WORK_Model> getModelListMan(YJ_XY_WORK_SW sw)
        {
            var result = new List<YJ_XY_WORK_Model>();

            DataTable dt = BaseDT.YJ_XY_WORK.getDT(sw);//列表

            DataTable dt24 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "24"});//火情预警等级
            DataTable dt25 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "25" });//预警响应部门
            for (int i = 0; i < dt24.Rows.Count; i++)
            {
                for (int k = 0; k < dt25.Rows.Count; k++)
                {

                    YJ_XY_WORK_Model m = new YJ_XY_WORK_Model();
                    m.DANGERCLASS = dt24.Rows[i]["DICTVALUE"].ToString();
                    m.YJXYDEPT = dt25.Rows[k]["DICTVALUE"].ToString();
                    m.YJXYID = BaseDT.YJ_XY_WORK.getID(dt, m.DANGERCLASS, m.YJXYDEPT);
                    m.YJXYCONTENT = BaseDT.YJ_XY_WORK.getContent(dt, m.DANGERCLASS, m.YJXYDEPT);
                    m.DANGERCLASSName = dt24.Rows[i]["DICTNAME"].ToString();// BaseDT.T_SYS_DICT.getName(dt24, m.DANGERCLASS);
                    m.YJXYDEPTName = dt25.Rows[k]["DICTNAME"].ToString();
                    T_SYS_DICTModel dm = new T_SYS_DICTModel();
                    dm.STANDBY1 = dt24.Rows[i]["STANDBY1"].ToString();
                    m.dicModel = dm;
                    result.Add(m);
                }
            }
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    YJ_XY_WORK_Model m = new YJ_XY_WORK_Model();
                //    m.YJXYID = dt.Rows[i]["YJXYID"].ToString();
                //    m.DANGERCLASS = dt.Rows[i]["DANGERCLASS"].ToString();
                //    m.YJXYDEPT = dt.Rows[i]["YJXYDEPT"].ToString();
                //    m.YJXYCONTENT = dt.Rows[i]["YJXYCONTENT"].ToString();
                //    m.DANGERCLASSName = BaseDT.T_SYS_DICT.getName(dt24, m.DANGERCLASS);
                //    m.YJXYDEPTName = BaseDT.T_SYS_DICT.getName(dt24, m.YJXYDEPT);
                //    result.Add(m);
                //}
            dt.Clear();
            dt.Dispose();
            dt24.Clear();
            dt24.Dispose();
            dt25.Clear();
            dt25.Dispose();
            return result;
        }

        #endregion
    }
}
