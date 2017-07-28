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
    /// 火情报告
    /// </summary>
    public class JC_FIRE_REPORTCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(JC_FIRE_REPORT_SW m)
        {
            if (m.opMethod == "Add")
            {
                //SystemCls.LogSave("3", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msg = BaseDT.JC_FIRE_REPORT.Add(m);
                return new Message(msg.Success, msg.Msg, "");
            }
            //if (m.opMethod == "Mdy")
            //{
            //    //SystemCls.LogSave("4", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
            //    Message msgUser = BaseDT.JC_FIRE_REPORT.Mdy(m);
            //    return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);

            //}
            //if (m.opMethod == "Del")
            //{
            //    //SystemCls.LogSave("5", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
            //    Message msgUser = BaseDT.JC_FIRE_REPORT.Del(m);
            //    return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            //}
            return new Message(false, "无效操作", "");


        }

        #endregion

        #region 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<JC_FIRE_REPORT_Model> getModelList(JC_FIRE_REPORT_SW sw)
        {
            var result = new List<JC_FIRE_REPORT_Model>();
            DataTable dt = BaseDT.JC_FIRE_REPORT.getDT(sw);//列表
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                JC_FIRE_REPORT_Model m = new JC_FIRE_REPORT_Model();
                m.ID = dt.Rows[i]["ID"].ToString();
                m.OWERJCFID = dt.Rows[i]["OWERJCFID"].ToString();
                m.FILENAME = dt.Rows[i]["FILENAME"].ToString();
                m.FILESIZE = dt.Rows[i]["FILESIZE"].ToString();
                m.FILEURL = dt.Rows[i]["FILEURL"].ToString();
                //m.UPLOADTIME = dt.Rows[i]["UPLOADTIME"].ToString();
                m.UPLOADTIME = ClsSwitch.SwitTM(dt.Rows[i]["UPLOADTIME"].ToString());
                m.UPLOADUSERID = dt.Rows[i]["UPLOADUSERID"].ToString();
                m.UPLOADORGNO = dt.Rows[i]["UPLOADORGNO"].ToString();
                m.UPLOANAME = T_SYSSEC_IPSUSERCls.getModel(new T_SYSSEC_IPSUSER_SW() { USERID = m.UPLOADUSERID }).USERNAME;
                m.UPLOADORGNAME = BaseDT.T_SYS_ORG.getName(dtORG, m.UPLOADORGNO);
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            return result;
        }
        #endregion
    }
}
