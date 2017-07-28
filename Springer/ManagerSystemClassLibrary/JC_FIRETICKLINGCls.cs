using ManagerSystemModel;
using ManagerSystemModel.LogicModel;
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
    /// 火情反馈
    /// </summary>
    public class JC_FIRETICKLINGCls
    {

        #region 火情反馈信息增、删、改

        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(JC_FIRETICKLING_SW m)
        {
            if (m.opMethod == "Add")
            {
                //SystemCls.LogSave("3", "红外相机:" + m.INFRAREDCAMERANAME, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.JC_FIRETICKLING.Add(m);
                return new Message(msgUser.Success, msgUser.Msg, "");
            }
            //if (m.opMethod == "Mdy")
            //{
            //    SystemCls.LogSave("4", "红外相机:" + m.INFRAREDCAMERANAME, ClsStr.getModelContent(m));
            //    Message msgUser = BaseDT.JC_INFRAREDCAMERA_BASICINFO.Mdy(m);
            //    return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);

            //}
            //if (m.opMethod == "Del")
            //{
            //    SystemCls.LogSave("5", "红外相机:" + m.INFRAREDCAMERANAME, ClsStr.getModelContent(m));
            //    Message msgUser = BaseDT.JC_INFRAREDCAMERA_BASICINFO.Del(m);
            //    return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            //}
            return new Message(false, "无效操作", "");


        }

        /// <summary>
        /// 获取反馈信息模型
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<JC_FIRETICKLING_Model> GetModelList(JC_FIRETICKLING_SW sw)
        {
            var result = new List<JC_FIRETICKLING_Model>();
            DataTable dt = BaseDT.JC_FIRETICKLING.GetDT(sw);//获取反馈信息记录
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var model = new JC_FIRETICKLING_Model();
                model.FKID = dt.Rows[i]["FKID"].ToString();
                model.JCFID = dt.Rows[i]["JCFID"].ToString();
                model.DL = dt.Rows[i]["DL"].ToString();
                model.FORESTNAME = dt.Rows[i]["FORESTNAME"].ToString();
                model.FORESTFIRETYPE = dt.Rows[i]["FORESTFIRETYPE"].ToString();
                model.FUELTYPE = dt.Rows[i]["FUELTYPE"].ToString();
                model.HOTTYPE = dt.Rows[i]["HOTTYPE"].ToString();
                model.CHECKTIME = dt.Rows[i]["CHECKTIME"] == null ? "" : ClsSwitch.SwitTM(dt.Rows[i]["CHECKTIME"].ToString());
                model.YY = dt.Rows[i]["YY"].ToString();
                model.JXHQSJ = dt.Rows[i]["JXHQSJ"].ToString();
                model.FIREBEGINTIME = dt.Rows[i]["FIREBEGINTIME"] == null ? "" : ClsSwitch.SwitTM(dt.Rows[i]["FIREBEGINTIME"].ToString());
                model.FIREENDTIME = dt.Rows[i]["FIREENDTIME"] == null ? "" : ClsSwitch.SwitTM(dt.Rows[i]["FIREENDTIME"].ToString());
                model.ISOUTFIRE = dt.Rows[i]["ISOUTFIRE"].ToString();
                model.BURNEDAREA = dt.Rows[i]["BURNEDAREA"].ToString();
                model.OVERDOAREA = dt.Rows[i]["OVERDOAREA"].ToString();
                model.LOSTFORESTAREA = dt.Rows[i]["LOSTFORESTAREA"].ToString();
                model.ELSELOSSINTRO = dt.Rows[i]["ELSELOSSINTRO"].ToString();
                model.FIREINTRO = dt.Rows[i]["FIREINTRO"].ToString();
                model.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                model.MANUSERID = dt.Rows[i]["MANUSERID"].ToString();
                model.MANTIME = dt.Rows[i]["MANTIME"].ToString();
                model.MANSTATE = dt.Rows[i]["MANSTATE"].ToString();
                result.Add(model);
            }
            return result;
        }
        #endregion

        #region 反馈火情数据事务

        /// <summary>
        /// 获取反馈火情相关数据
        /// </summary>
        /// <param name="jcfid">监测火情id</param>
        /// <returns></returns>
        public static JCFireFKInfoModel GetFKFireInfoData(string jcfid)
        {
            DataTable dt = BaseDT.JC_FIRETICKLING.GetFKDT(jcfid);//获取反馈信息记录
            JCFireFKInfoModel m = new JCFireFKInfoModel();
            if (dt.Rows.Count == 1)
            {
                #region 监测火情信息
                m.JC_FireData.JCFID = dt.Rows[0]["JCFID"].ToString();
                m.JC_FireData.WXBH = dt.Rows[0]["WXBH"].ToString();
                m.JC_FireData.DQRDBH = dt.Rows[0]["DQRDBH"].ToString();
                m.JC_FireData.RSMJ = dt.Rows[0]["RSMJ"].ToString();
                m.JC_FireData.JD = dt.Rows[0]["JCJD"].ToString();
                m.JC_FireData.WD = dt.Rows[0]["JCWD"].ToString();
                m.JC_FireData.RECEIVETIME = ClsSwitch.SwitTM(dt.Rows[0]["RECEIVETIME"].ToString());
                m.JC_FireData.ISSUEDTIME = ClsSwitch.SwitTM(dt.Rows[0]["ISSUEDTIME"].ToString());
                m.JC_FireData.ZQWZ = dt.Rows[0]["ZQWZ"].ToString();
                m.JC_FireData.JCMANSTATE = dt.Rows[0]["JCMANSTATE"].ToString();
                m.JC_FireData.FIRENAME = dt.Rows[0]["FIRENAME"].ToString();
                m.JC_FireData.FIREFROMID = dt.Rows[0]["FIREFROMID"].ToString();//火情原始id
                m.JC_FireData.FIREFROM = dt.Rows[0]["FIREFROM"].ToString();
                m.JC_FireData.BYORGNO = dt.Rows[0]["XFORGNO"].ToString();//下发单位
                m.JC_FireData.FIRETIME = ClsSwitch.SwitTM(dt.Rows[0]["FIRETIME"].ToString());
                m.JC_FireData.PFFLAG = dt.Rows[0]["PFFLAG"].ToString();
                #endregion

                #region 火情反馈信息
                m.JC_FireFKData.DL = dt.Rows[0]["DL"].ToString();
                m.JC_FireFKData.FORESTNAME = dt.Rows[0]["FORESTNAME"].ToString();
                m.JC_FireFKData.FORESTFIRETYPE = dt.Rows[0]["FORESTFIRETYPE"].ToString();
                m.JC_FireFKData.FUELTYPE = dt.Rows[0]["FUELTYPE"].ToString();
                m.JC_FireFKData.HOTTYPE = dt.Rows[0]["HOTTYPE"].ToString();
                m.JC_FireFKData.CHECKTIME = ClsSwitch.SwitTM(dt.Rows[0]["CHECKTIME"].ToString());
                m.JC_FireFKData.YY = dt.Rows[0]["YY"].ToString();
                m.JC_FireFKData.JXHQSJ = dt.Rows[0]["JXHQSJ"].ToString();
                m.JC_FireFKData.FIREBEGINTIME = ClsSwitch.SwitTM(dt.Rows[0]["FIREBEGINTIME"].ToString());
                m.JC_FireFKData.FIREENDTIME = ClsSwitch.SwitTM(dt.Rows[0]["FIREENDTIME"].ToString());

                m.JC_FireFKData.ISOUTFIRE = dt.Rows[0]["ISOUTFIRE"].ToString();
                m.JC_FireFKData.BURNEDAREA = dt.Rows[0]["BURNEDAREA"].ToString();
                m.JC_FireFKData.OVERDOAREA = dt.Rows[0]["OVERDOAREA"].ToString();
                m.JC_FireFKData.LOSTFORESTAREA = dt.Rows[0]["LOSTFORESTAREA"].ToString();
                m.JC_FireFKData.ELSELOSSINTRO = dt.Rows[0]["ELSELOSSINTRO"].ToString();
                m.JC_FireFKData.FIREINTRO = dt.Rows[0]["FIREINTRO"].ToString();
                m.JC_FireFKData.BYORGNO = dt.Rows[0]["BYORGNO"].ToString();
                m.JC_FireFKData.MANUSERID = dt.Rows[0]["MANUSERID"].ToString();
                m.JC_FireFKData.MANSTATE = dt.Rows[0]["MANSTATE"].ToString();
                m.JC_FireFKData.MANTIME = dt.Rows[0]["MANTIME"].ToString();
                m.JC_FireFKData.AUDITREASON = dt.Rows[0]["AUDITREASON"].ToString();//审核不通过理由
                m.JC_FireFKData.ADDRESS = dt.Rows[0]["ADDRESS"].ToString();//实际发生地
                //坐标偏移量计算  84坐标转火星坐标
                string jd = string.IsNullOrEmpty(dt.Rows[0]["JD"].ToString()) ? dt.Rows[0]["JCJD"].ToString() : dt.Rows[0]["JD"].ToString();//实际发生经度
                string wd = string.IsNullOrEmpty(dt.Rows[0]["WD"].ToString()) ? dt.Rows[0]["JCWD"].ToString() : dt.Rows[0]["WD"].ToString();//实际发生纬度
                if (!string.IsNullOrEmpty(jd) && !string.IsNullOrEmpty(wd))
                {
                    double[] drr = ClsPositionTrans.GpsTransform(double.Parse(wd), double.Parse(jd), "1");//中心点偏移
                    m.JC_FireFKData.JD = drr[1].ToString();
                    m.JC_FireFKData.WD = drr[0].ToString();
                }
                // m.JC_FireFKData.JD = string.IsNullOrEmpty(dt.Rows[0]["JD"].ToString()) ? dt.Rows[0]["JCJD"].ToString() : dt.Rows[0]["JD"].ToString();//实际发生经度
                //m.JC_FireFKData.WD = string.IsNullOrEmpty(dt.Rows[0]["WD"].ToString()) ? dt.Rows[0]["JCWD"].ToString() : dt.Rows[0]["WD"].ToString();//实际发生纬度
                #endregion

                //组织机构名
                if (string.IsNullOrEmpty(m.JC_FireFKData.BYORGNO))
                {
                    m.JC_FireFKData.BYORGNO = SystemCls.getCurUserOrgNo();
                }
                DataTable orgdt = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW() { ORGNO = m.JC_FireFKData.BYORGNO });
                m.OrgName = BaseDT.T_SYS_ORG.getName(orgdt, m.JC_FireFKData.BYORGNO);
                //用户名
                if (string.IsNullOrEmpty(m.JC_FireFKData.MANUSERID))
                {
                    m.JC_FireFKData.MANUSERID = SystemCls.getUserID();
                }
                m.UserName = T_SYSSEC_IPSUSERCls.getModel(new T_SYSSEC_IPSUSER_SW() { USERID = m.JC_FireFKData.MANUSERID }).USERNAME;
            }
            dt.Clear();
            dt.Dispose();
            return m;
        }
        #endregion
        #region 获取最新的火情反馈信息
        /// <summary>
        /// 获取最新的火情反馈信息
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static JC_FIRETICKLING_Model getLatestfeedback(JC_FIRETICKLING_SW sw)
        {
            DataTable dt = BaseDT.JC_FIRETICKLING.Latestfeedback(sw);
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dt7 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "7" });//数据中心地类类型
            DataTable dt8 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "8" });//数据中心林火类型
            DataTable dt9 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "9" });//数据中心可燃物类别
            DataTable dt10 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "10" });//热点类别           
            JC_FIRETICKLING_Model m = new JC_FIRETICKLING_Model();
            //JCFireFKInfoModel m = new JCFireFKInfoModel();
            if (dt.Rows.Count == 1)
            {
                m.DL = dt.Rows[0]["DL"].ToString();
                m.DLName = BaseDT.T_SYS_DICT.getName(dt7, m.DL);
                m.FORESTNAME = dt.Rows[0]["FORESTNAME"].ToString();
                m.FORESTFIRETYPE = dt.Rows[0]["FORESTFIRETYPE"].ToString();
                m.FORESTFIRETYPENAME = BaseDT.T_SYS_DICT.getName(dt8, m.FORESTFIRETYPE);
                m.FUELTYPE = dt.Rows[0]["FUELTYPE"].ToString();
                m.FUELTYPEName = BaseDT.T_SYS_DICT.getName(dt9, m.FUELTYPE);
                m.HOTTYPE = dt.Rows[0]["HOTTYPE"].ToString();
                m.HOTTYPEName = BaseDT.T_SYS_DICT.getName(dt10, m.HOTTYPE);
                m.CHECKTIME = ClsSwitch.SwitTM(dt.Rows[0]["CHECKTIME"].ToString());
                m.YY = dt.Rows[0]["YY"].ToString();
                m.JXHQSJ = dt.Rows[0]["JXHQSJ"].ToString();
                m.FIREBEGINTIME = ClsSwitch.SwitTM(dt.Rows[0]["FIREBEGINTIME"].ToString());
                m.FIREENDTIME = ClsSwitch.SwitTM(dt.Rows[0]["FIREENDTIME"].ToString());

                m.ISOUTFIRE = dt.Rows[0]["ISOUTFIRE"].ToString();
                m.BURNEDAREA = dt.Rows[0]["BURNEDAREA"].ToString();
                m.OVERDOAREA = dt.Rows[0]["OVERDOAREA"].ToString();
                m.LOSTFORESTAREA = dt.Rows[0]["LOSTFORESTAREA"].ToString();
                m.ELSELOSSINTRO = dt.Rows[0]["ELSELOSSINTRO"].ToString();
                m.FIREINTRO = dt.Rows[0]["FIREINTRO"].ToString();
                m.BYORGNO = dt.Rows[0]["BYORGNO"].ToString();
                m.MANUSERID = dt.Rows[0]["MANUSERID"].ToString();
                m.MANSTATE = dt.Rows[0]["MANSTATE"].ToString();
                m.MANTIME = dt.Rows[0]["MANTIME"].ToString();
                m.AUDITREASON = dt.Rows[0]["AUDITREASON"].ToString();//审核不通过理由
                m.ADDRESS = dt.Rows[0]["ADDRESS"].ToString();//实际发生地
                m.JD = dt.Rows[0]["JD"].ToString();
                m.WD = dt.Rows[0]["WD"].ToString();
                m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                //m.JC_FireFKData.JD = string.IsNullOrEmpty(dt.Rows[0]["JD"].ToString()) ? dt.Rows[0]["JCJD"].ToString() : dt.Rows[0]["JD"].ToString();//实际发生经度
                //m.JC_FireFKData.WD = string.IsNullOrEmpty(dt.Rows[0]["WD"].ToString()) ? dt.Rows[0]["JCWD"].ToString() : dt.Rows[0]["WD"].ToString();//实际发生纬度
            }
            dt.Clear();
            dt.Dispose();
            dt7.Clear();
            dt7.Dispose();
            dt8.Clear();
            dt8.Dispose();
            dt9.Clear();
            dt9.Dispose();
            dt10.Clear();
            dt10.Dispose();
            return m;
        }
        #endregion
    }
}
