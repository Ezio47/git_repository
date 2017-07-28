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
    /// 公用_机构表_村委会
    /// </summary>
    public class T_SYS_ORG_CWHCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(T_SYS_ORG_CWH_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msgUser = BaseDT.T_SYS_ORG_CWH.Add(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            if (m.opMethod == "Mdy")
            {
                Message msgUser = BaseDT.T_SYS_ORG_CWH.Mdy(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            if (m.opMethod == "Del")
            {
                Message msgUser = BaseDT.T_SYS_ORG_CWH.Del(m);
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
        public static T_SYS_ORG_CWH_Model getModel(T_SYS_ORG_CWH_SW sw)
        {
            var result = new List<T_SYS_ORG_CWH_Model>();

            DataTable dt = BaseDT.T_SYS_ORG_CWH.getDT(sw);//列表

            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            T_SYS_ORG_CWH_Model m = new T_SYS_ORG_CWH_Model();

            if (dt.Rows.Count > 0)
            {
                int i = 0; m.CWHID = dt.Rows[i]["CWHID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.CWHNAME = dt.Rows[i]["CWHNAME"].ToString();
                m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
                m.ORGName = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
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
        public static IEnumerable<T_SYS_ORG_CWH_Model> getModelList(T_SYS_ORG_CWH_SW sw)
        {
            var result = new List<T_SYS_ORG_CWH_Model>();

            DataTable dt = BaseDT.T_SYS_ORG_CWH.getDT(sw);//列表

            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T_SYS_ORG_CWH_Model m = new T_SYS_ORG_CWH_Model();
                m.CWHID = dt.Rows[i]["CWHID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.CWHNAME = dt.Rows[i]["CWHNAME"].ToString();
                m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
                m.ORGName = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            return result;
        }

        #endregion

        #region 获取自然村列表
        /// <summary>
        /// 获取自然村列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<T_SYS_ORG_CWH_Model> getDCModelList(T_SYS_ORG_CWH_SW sw)
        {
            var result = new List<T_SYS_ORG_CWH_Model>();

            DataTable dt = BaseDT.T_SYS_ORG_CWH.getVillageDT(sw);//列表

            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T_SYS_ORG_CWH_Model m = new T_SYS_ORG_CWH_Model();
                m.CWHID = dt.Rows[i]["CWHID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.CWHNAME = dt.Rows[i]["CWHNAME"].ToString();
                m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
                m.ORGName = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                m.UNITNAME = dt.Rows[i]["UNITNAME"].ToString();
                m.ORGLINKTYPE = dt.Rows[i]["ORGLINKTYPE"].ToString();
                m.ORGNO = dt.Rows[i]["ORGNO"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            return result;
        }

        #endregion

        #region 村委会导入
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message CWHUploadAdd(OrgMapUpload_Model  m)
        {
            //第一步，根据县名称和乡名称获取单位编码
            string orgNo = BaseDT.T_SYS_ORG.getOrgNoByXianNameAndZhenName(m.县名称, m.乡镇名称);
            if (string.IsNullOrEmpty(orgNo))
                return new Message(false, "没有找到该乡镇", "");
            //第二步，判断村委会是否存在于表中,通过获取村委会ID来判断
            string cwhID = "";
            if (1 == 1)
            {
                DataTable dt = BaseDT.T_SYS_ORG_CWH.getDT(new T_SYS_ORG_CWH_SW { CWHNAME = m.村委会名称, BYORGNO = orgNo });
                if (dt.Rows.Count > 0)
                    cwhID = dt.Rows[0]["CWHID"].ToString();
                if (string.IsNullOrEmpty(cwhID))
                {//没有找到村委会，先插入 该村委会
                    T_SYS_ORG_CWH_Model mCWH = new T_SYS_ORG_CWH_Model();
                    mCWH.BYORGNO = orgNo;
                    mCWH.CWHNAME = m.村委会名称;
                    mCWH.ORDERBY = m.排序号;
                    mCWH.opMethod = "Add";
                    Manager(mCWH);
                }
                dt.Clear();
                dt.Dispose();
            }
            if(string.IsNullOrEmpty(cwhID))//如果是新添加的，重新获取村委会ID
            {
                DataTable dt = BaseDT.T_SYS_ORG_CWH.getDT(new T_SYS_ORG_CWH_SW { CWHNAME = m.村委会名称, BYORGNO = orgNo });
                if (dt.Rows.Count > 0)
                    cwhID = dt.Rows[0]["CWHID"].ToString();
                dt.Clear();
                dt.Dispose();
            }
            if (string.IsNullOrEmpty(cwhID))
                return new Message(false, "未找到该村委会", "");
            //第三步，插入到联系人表中
            T_SYS_ORG_LINK_Model ml = new T_SYS_ORG_LINK_Model();
            ml.BYORGNO = cwhID;
            if (string.IsNullOrEmpty(m.自然村名称))//村委会成员
                ml.ORGLINKTYPE = "1";
            else
                ml.ORGLINKTYPE = "2";//小组长
            ml.UNITNAME = m.自然村名称;
            ml.NAME = m.联系人;
            ml.USERJOB = m.职位;
            ml.PHONE = m.手机;
            ml.Tell = m.电话;
            ml.ORDERBY = m.排序号;
            ml.opMethod = "Add";
            return T_SYS_ORG_LINKCls.Manager(ml);
           
            //if(bln)
            //return new Message(false, "无效操作", "");


        }

        #endregion

        #region 获取自然村JsonString
        /// <summary>
        /// 获取组织机构json字符串
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getVillageJsonStr(T_SYS_ORG_CWH_SW sw)
        {
            DataTable dt = BaseDT.T_SYS_ORG_CWH.getVillageDT(sw);//列表
            char[] specialChars = new char[] { ',' };
            string JSONstring = "[";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string orgNo = dt.Rows[i]["ORGNO"].ToString();
                string UNITNAME = dt.Rows[i]["UNITNAME"].ToString();
                JSONstring += "{";
                JSONstring += "\"id\":\"" + UNITNAME + "\",";
                JSONstring += "\"text\":\"" + UNITNAME + "\"";
                JSONstring += "},";
            }
            JSONstring = JSONstring.TrimEnd(specialChars);
            JSONstring += "]";
            return JSONstring.ToString();
        }
        #endregion

        
    }
}
