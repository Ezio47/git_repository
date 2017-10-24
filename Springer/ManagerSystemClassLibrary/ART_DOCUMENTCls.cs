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
    /// 文档表
    /// </summary>
    public class ART_DOCUMENTCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(ART_DOCUMENT_Model m)
        {
            if (m.opMethod == "Add")
            {
                //SystemCls.LogSave("3", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.ART_DOCUMENT.Add(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            if (m.opMethod == "Mdy")
            {
                //SystemCls.LogSave("4", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.ART_DOCUMENT.Mdy(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);

            }
            if (m.opMethod == "Del")
            {
                //SystemCls.LogSave("5", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.ART_DOCUMENT.Del(m);
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
        public static ART_DOCUMENT_Model getModel(ART_DOCUMENT_SW sw)
        {
            var result = new List<ART_DOCUMENT_Model>();

            DataTable dt = BaseDT.ART_DOCUMENT.getDT(sw);//列表

            ART_DOCUMENT_Model m = new ART_DOCUMENT_Model();

            DataTable dtUser = BaseDT.T_SYSSEC_USER.getDT(new T_SYSSEC_IPSUSER_SW { });
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.ARTID = dt.Rows[i]["ARTID"].ToString();
                m.ARTTYPEID = dt.Rows[i]["ARTTYPEID"].ToString();
                m.ARTTITLE = dt.Rows[i]["ARTTITLE"].ToString();
                m.ARTTAG = dt.Rows[i]["ARTTAG"].ToString();
                m.ARTTIME = PublicClassLibrary.ClsSwitch.SwitTM(dt.Rows[i]["ARTTIME"].ToString());
                m.ARTCHECKSTATUS = dt.Rows[i]["ARTCHECKSTATUS"].ToString();
                m.ARTADDUSERID = dt.Rows[i]["ARTADDUSERID"].ToString();
                m.ARTCHECKTIME = PublicClassLibrary.ClsSwitch.SwitTM(dt.Rows[i]["ARTCHECKTIME"].ToString());
                m.ARTCHECKUSERID = dt.Rows[i]["ARTCHECKUSERID"].ToString();
                m.ARTCONTENT = dt.Rows[i]["ARTCONTENT"].ToString();
                DataTable dtType = BaseDT.ART_TYPE.getDT(new ART_TYPE_SW { ARTTYPEID=m.ARTTYPEID });
                m.ARTTYPENAME = BaseDT.ART_TYPE.getName(dtType, m.ARTTYPEID);
                m.ARTADDUSERName = BaseDT.T_SYSSEC_USER.getNameByUserList(dtUser, m.ARTADDUSERID);
                m.ARTCHECKUSERIDName = BaseDT.T_SYSSEC_USER.getNameByUserList(dtUser, m.ARTCHECKUSERID);
                m.PLANFILENAME = dt.Rows[i]["PLANFILENAME"].ToString();
                dtType.Clear();
                dtType.Dispose();
            }
            dt.Clear();
            dt.Dispose();
            dtUser.Clear();
            dtUser.Dispose();
            return m;
        }

        #endregion

        #region 获取文档列表
        /// <summary>
        /// 获取文档列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<ART_DOCUMENT_Model>  getModelList(ART_DOCUMENT_SW sw)
        {
            var result = new List<ART_DOCUMENT_Model>();

            DataTable dtType = BaseDT.ART_TYPE.getDT(new ART_TYPE_SW { });
            DataTable dt = BaseDT.ART_DOCUMENT.getDT(sw);//列表

            DataTable dtUser = BaseDT.T_SYSSEC_USER.getDT(new T_SYSSEC_IPSUSER_SW { });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ART_DOCUMENT_Model m = new ART_DOCUMENT_Model();
                m.ARTID = dt.Rows[i]["ARTID"].ToString();
                m.ARTTYPEID = dt.Rows[i]["ARTTYPEID"].ToString();
                m.ARTTITLE = dt.Rows[i]["ARTTITLE"].ToString();
                m.ARTTAG = dt.Rows[i]["ARTTAG"].ToString();
                m.ARTTIME = PublicClassLibrary.ClsSwitch.SwitTM(dt.Rows[i]["ARTTIME"].ToString());
                m.ARTCHECKSTATUS = dt.Rows[i]["ARTCHECKSTATUS"].ToString();
                m.ARTADDUSERID = dt.Rows[i]["ARTADDUSERID"].ToString();
                m.ARTCHECKTIME = PublicClassLibrary.ClsSwitch.SwitTM(dt.Rows[i]["ARTCHECKTIME"].ToString());
                m.ARTCHECKUSERID = dt.Rows[i]["ARTCHECKUSERID"].ToString();
                m.PLANFILENAME = dt.Rows[i]["PLANFILENAME"].ToString();

                m.ARTTYPENAME = BaseDT.ART_TYPE.getName(dtType, dt.Rows[i]["ARTTYPEID"].ToString());
                m.ARTADDUSERName = BaseDT.T_SYSSEC_USER.getNameByUserList(dtUser, m.ARTADDUSERID);
                m.ARTCHECKUSERIDName = BaseDT.T_SYSSEC_USER.getNameByUserList(dtUser, m.ARTCHECKUSERID);
                result.Add(m);
            }
            dtType.Clear();
            dtType.Dispose();
            dt.Clear();
            dt.Dispose();
            dtUser.Clear();
            dtUser.Dispose();
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
        public static IEnumerable<ART_DOCUMENT_Model> getModelList(ART_DOCUMENT_SW sw, out int total)
        {
            var result = new List<ART_DOCUMENT_Model>();
            DataTable dt = BaseDT.ART_DOCUMENT.getDT(sw, out total);//列表
            DataTable dtUser = BaseDT.T_SYSSEC_USER.getDT(new T_SYSSEC_IPSUSER_SW { });
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            string typeid = "";
            if (sw.ARTBigTYPEID == "013") typeid = "47";
            if (sw.ARTBigTYPEID == "014") typeid = "48";
            if (sw.ARTBigTYPEID == "015") typeid = "49";
            DataTable dtType = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = typeid });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ART_DOCUMENT_Model m = new ART_DOCUMENT_Model();
                m.ARTID = dt.Rows[i]["ARTID"].ToString();
                m.ARTTYPEID = dt.Rows[i]["ARTTYPEID"].ToString();
                m.ARTTITLE = dt.Rows[i]["ARTTITLE"].ToString();
                m.ARTTAG = dt.Rows[i]["ARTTAG"].ToString();
                m.ARTTIME =PublicClassLibrary.ClsSwitch.SwitTM( dt.Rows[i]["ARTTIME"].ToString());
                m.ARTCHECKSTATUS = dt.Rows[i]["ARTCHECKSTATUS"].ToString();
                m.ARTADDUSERID = dt.Rows[i]["ARTADDUSERID"].ToString();
                m.ARTCHECKTIME = PublicClassLibrary.ClsSwitch.SwitTM(dt.Rows[i]["ARTCHECKTIME"].ToString());
                m.ARTCHECKUSERID = dt.Rows[i]["ARTCHECKUSERID"].ToString();
                m.PLANFILENAME = dt.Rows[i]["PLANFILENAME"].ToString();

                m.ARTTYPENAME = BaseDT.T_SYS_DICT.getName(dtType, m.ARTTYPEID);// BaseDT.ART_TYPE.getName(dtType, dt.Rows[i]["ARTTYPEID"].ToString());
                m.ARTADDUSERName = BaseDT.T_SYSSEC_USER.getNameByUserList(dtUser, m.ARTADDUSERID);
                m.ARTCHECKUSERIDName = BaseDT.T_SYSSEC_USER.getNameByUserList(dtUser, m.ARTCHECKUSERID);
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.orgName = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                result.Add(m);
                //ARTID, ARTTYPEID, ARTTITLE, ARTTAG, ARTTIME,  ARTCHECKSTATUS, ARTADDUSERID,ARTCHECKTIME, ARTCHECKUSERID
            }
            dtORG.Clear();
            dtORG.Dispose();
            dtType.Clear();
            dtType.Dispose();
            dt.Clear();
            dt.Dispose();
            dtUser.Clear();
            dtUser.Dispose();
            return result;
        }
        #endregion
    }
}
