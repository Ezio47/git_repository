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
    /// 监测_火情表
    /// </summary>
    public class JC_FIRECls
    {
        #region 火情增加[转为火情]

        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(JC_FIRE_Model m)
        {
            if (m.opMethod == "Add")
            {
                SystemCls.LogSave("3", "转为火情:" + m.FIRENAME, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.JC_FIRE.Add(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            if (m.opMethod == "Mdy")
            {
                Message msgUser = BaseDT.JC_FIRE.Mdy(m);
                return new Message(msgUser.Success, msgUser.Msg, "");
            }
            if (m.opMethod == "AddWX")
            {
                Message msgUser = BaseDT.JC_FIRE.Add(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            if (m.opMethod == "PLAdd")
            {
                Message msgUser = BaseDT.JC_FIRE.PLAdd(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            if (m.opMethod == "PLEnd")
            {
                Message msgUser = BaseDT.JC_FIRE.PLEnd(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            return new Message(false, "无效操作", "");
        }

        /// <summary>
        /// 更新监测火情表状态
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message MdyJCFireState(JC_FIRE_Model m)
        {
            Message msgUser = BaseDT.JC_FIRE.Mdy(m);
            return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
        }
        /// <summary>
        /// 更新火情结束
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message MdyJCFireOver(JC_FIRE_Model m)
        {
            Message msgUser = BaseDT.JC_FIRE.MdyFireOver(m);
            return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
        }

        /// <summary>
        /// 获取监测火情信息
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<JC_FIRE_Model> GetListModel(JC_FIRE_SW sw)
        {
            DataTable dt = BaseDT.JC_FIRE.GetDT(sw);//列表
            var result = new List<JC_FIRE_Model>();
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                JC_FIRE_Model m = new JC_FIRE_Model();
                m.JCFID = dt.Rows[i]["JCFID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.FIRENAME = dt.Rows[i]["FIRENAME"].ToString();
                m.FIREFROM = dt.Rows[i]["FIREFROM"].ToString();//火情来源
                m.FIREFROMID = dt.Rows[i]["FIREFROMID"].ToString();//原始记录序号
                m.FIRETIME = ClsSwitch.SwitTM(dt.Rows[i]["FIRETIME"].ToString());//起火时间
                m.FIREENDTIME = ClsSwitch.SwitTM(dt.Rows[i]["FIREENDTIME"].ToString());//起火结束时间
                m.ISOUTFIRE = dt.Rows[i]["ISOUTFIRE"].ToString();//是否已灭
                m.MARK = dt.Rows[i]["MARK"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.ZQWZ = dt.Rows[i]["ZQWZ"].ToString();
                m.WXBH = dt.Rows[i]["WXBH"].ToString();
                m.DQRDBH = dt.Rows[i]["DQRDBH"].ToString();
                m.RSMJ = dt.Rows[i]["RSMJ"].ToString();
                m.DL = dt.Rows[i]["DL"].ToString();
                m.YY = dt.Rows[i]["YY"].ToString();
                m.JXHQSJ = dt.Rows[i]["JXHQSJ"].ToString();
                m.OWERJCFID = dt.Rows[i]["OWERJCFID"].ToString();//所属主火情id
                m.PFUSERID = dt.Rows[i]["PFUSERID"].ToString();//派发人
                m.PFORGNO = dt.Rows[i]["PFORGNO"].ToString();// 派发单位
                m.PFTIME = dt.Rows[i]["PFTIME"].ToString();// 派发时间
                m.RECEIVETIME = ClsSwitch.SwitTM(dt.Rows[i]["RECEIVETIME"].ToString());
                m.PFFLAG = dt.Rows[i]["PFFLAG"].ToString();// 派发标志 1 为市局单位处理 2 为县 3 为乡镇 0 非本单位处理
                m.ISSUEDTIME = ClsSwitch.SwitTM(dt.Rows[i]["ISSUEDTIME"].ToString());
                m.MANSTATE = dt.Rows[i]["MANSTATE"].ToString();
                m.ISSUEDTIME = ClsSwitch.SwitTM(dt.Rows[i]["LASTPROCESSTIME"].ToString());
                m.FIREFROMWEATHER = dt.Rows[i]["FIREFROMWEATHER"] == null ? "" : dt.Rows[i]["FIREFROMWEATHER"].ToString();// 来源

                m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }

        /// <summary>
        /// 获取监测火情信息——优化后
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<JC_FIRE_Model> GetListModelYH(JC_FIRE_SW sw)
        {
            DataTable dt = BaseDT.JC_FIRE.GetDTYH(sw);//列表
            var result = new List<JC_FIRE_Model>();
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                JC_FIRE_Model m = new JC_FIRE_Model();
                m.JCFID = dt.Rows[i]["JCFID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.FIRENAME = dt.Rows[i]["FIRENAME"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.ZQWZ = dt.Rows[i]["ZQWZ"].ToString();
                m.FIRELEVEL = dt.Rows[i]["FIRELEVEL"].ToString();
                m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }

        /// <summary>
        /// 获取监测火情信息和数量
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="total"></param>
        /// <param name="flag">0 分页 1 不分页</param>
        /// <returns></returns>
        public static IEnumerable<JC_FIRE_Model> GetListModelAndCount(JC_FIRE_SW sw, out int total, string flag = "0")
        {
            DataTable dt = BaseDT.JC_FIRE.GetDTAndTotal(sw, out total, flag);//列表
            var result = new List<JC_FIRE_Model>();
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                JC_FIRE_Model m = new JC_FIRE_Model();
                m.JCFID = dt.Rows[i]["JCFID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.FIRENAME = dt.Rows[i]["FIRENAME"].ToString();
                m.FIREFROM = dt.Rows[i]["FIREFROM"].ToString();//火情来源
                m.FIREFROMID = dt.Rows[i]["FIREFROMID"].ToString();//原始记录序号
                m.FIRETIME = ClsSwitch.SwitTM(dt.Rows[i]["FIRETIME"].ToString());//起火时间
                m.MARK = dt.Rows[i]["MARK"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.ZQWZ = dt.Rows[i]["ZQWZ"].ToString();
                m.WXBH = dt.Rows[i]["WXBH"].ToString();
                m.DQRDBH = dt.Rows[i]["DQRDBH"].ToString();
                m.RSMJ = dt.Rows[i]["RSMJ"].ToString();
                m.DL = dt.Rows[i]["DL"].ToString();
                m.YY = dt.Rows[i]["YY"].ToString();
                m.JXHQSJ = dt.Rows[i]["JXHQSJ"].ToString();
                m.RECEIVETIME = ClsSwitch.SwitTM(dt.Rows[i]["RECEIVETIME"].ToString());
                m.ISSUEDTIME = ClsSwitch.SwitTM(dt.Rows[i]["ISSUEDTIME"].ToString());
                m.MANSTATE = dt.Rows[i]["MANSTATE"].ToString();
                m.ISSUEDTIME = ClsSwitch.SwitTM(dt.Rows[i]["LASTPROCESSTIME"].ToString());

                m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }

        /// <summary>
        ///  获取签收反馈个数
        /// </summary>
        /// <param name="value"></param>
        /// <param name="ftype"></param>
        /// <param name="orgnostr"></param>
        /// <param name="eqtype"></param>
        /// <returns></returns>
        public static int GetCount(string value, string ftype, string orgnostr, string eqtype)
        {
            return BaseDT.JC_FIRE.GetCount(value, ftype, orgnostr, eqtype);
        }
        #endregion

        #region 签收（反馈）火情事务
        /// <summary>
        /// 签收
        /// </summary>
        /// <param name="jcfire"></param>
        /// <param name="jcfirefk"></param>
        /// <returns></returns>
        public static Message QSFireTrans(JC_FIRE_Model jcfire, JC_FIRETICKLING_SW jcfirefk)
        {
            return BaseDT.JC_FIRE.QSFire(jcfire, jcfirefk);
        }
        #endregion

        #region 获取火灾及火灾等级数据
        /// <summary>
        /// 获取火灾及火灾等级数据
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public IEnumerable<JC_FIRE_Model> GetDTFireProp(JC_FIRE_SW sw)
        {
            DataTable dt = BaseDT.JC_FIRE.GetDTFireProp(sw);//列表
            var result = new List<JC_FIRE_Model>();
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                JC_FIRE_Model m = new JC_FIRE_Model();
                m.JCFID = dt.Rows[i]["JCFID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.FIRENAME = dt.Rows[i]["FIRENAME"].ToString();
                m.FIREFROM = dt.Rows[i]["FIREFROM"].ToString();//火情来源
                m.FIREFROMID = dt.Rows[i]["FIREFROMID"].ToString();//原始记录序号
                m.FIRETIME = ClsSwitch.SwitTM(dt.Rows[i]["FIRETIME"].ToString());//起火时间
                m.MARK = dt.Rows[i]["MARK"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.ZQWZ = dt.Rows[i]["ZQWZ"].ToString();
                m.WXBH = dt.Rows[i]["WXBH"].ToString();
                m.DQRDBH = dt.Rows[i]["DQRDBH"].ToString();
                m.RSMJ = dt.Rows[i]["RSMJ"].ToString();
                m.DL = dt.Rows[i]["DL"].ToString();
                m.YY = dt.Rows[i]["YY"].ToString();
                m.JXHQSJ = dt.Rows[i]["JXHQSJ"].ToString();
                m.RECEIVETIME = ClsSwitch.SwitTM(dt.Rows[i]["RECEIVETIME"].ToString());
                m.ISSUEDTIME = ClsSwitch.SwitTM(dt.Rows[i]["ISSUEDTIME"].ToString());
                m.MANSTATE = dt.Rows[i]["MANSTATE"].ToString();
                m.ISSUEDTIME = ClsSwitch.SwitTM(dt.Rows[i]["LASTPROCESSTIME"].ToString());
                m.FIRELEVEL = dt.Rows[i]["FIRELEVEL"].ToString();
                m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                m.FirePropModel = null;//需要再扩充
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;

        }

        #endregion

        #region 获取火灾等级单条记录
        /// <summary>
        /// 获取火灾等级单条记录
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static JC_FIRE_Model getModel(JC_FIRE_SW sw)
        {
            DataTable dt = BaseDT.JC_FIRE.GetDT(sw);
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dtUser = BaseDT.T_SYSSEC_USER.getDT(new T_SYSSEC_IPSUSER_SW { });
            JC_FIRE_Model m = new JC_FIRE_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.JCFID = dt.Rows[i]["JCFID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.FIRENAME = dt.Rows[i]["FIRENAME"].ToString();
                m.FIREFROM = dt.Rows[i]["FIREFROM"].ToString();//火情来源
                //          红外相机 = 1,
                //卫星监控 = 2,
                //电话报警 = 3,
                //电子监控 = 4,
                //瞭望护林员 = 5,
                //无人机巡护 = 6
                if (m.FIREFROM == "1")
                    m.FIREFROMName = "红外相机";
                if (m.FIREFROM == "2")
                    m.FIREFROMName = "卫星热点";
                if (m.FIREFROM == "3")
                    m.FIREFROMName = "电话报警";
                if (m.FIREFROM == "4")
                    m.FIREFROMName = "电子监控";
                if (m.FIREFROM == "5")
                    m.FIREFROMName = "瞭望护林员";
                if (m.FIREFROM == "6")
                    m.FIREFROMName = "无人机巡护";
                if (m.FIREFROM == "50")
                    m.FIREFROMName = "历史补录";
                m.FIREFROMID = dt.Rows[i]["FIREFROMID"].ToString();//原始记录序号
                m.FIRETIME = ClsSwitch.SwitTM(dt.Rows[i]["FIRETIME"].ToString());//起火时间
                m.MARK = dt.Rows[i]["MARK"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.ZQWZ = dt.Rows[i]["ZQWZ"].ToString();
                m.WXBH = dt.Rows[i]["WXBH"].ToString();
                m.DQRDBH = dt.Rows[i]["DQRDBH"].ToString();
                m.RSMJ = dt.Rows[i]["RSMJ"].ToString();
                m.DL = dt.Rows[i]["DL"].ToString();
                m.YY = dt.Rows[i]["YY"].ToString();
                m.JXHQSJ = dt.Rows[i]["JXHQSJ"].ToString();
                m.ISOUTFIRE = dt.Rows[i]["ISOUTFIRE"].ToString();
                m.RECEIVETIME = ClsSwitch.SwitTM(dt.Rows[i]["RECEIVETIME"].ToString());
                m.ISSUEDTIME = ClsSwitch.SwitTM(dt.Rows[i]["ISSUEDTIME"].ToString());
                m.PFUSERID = dt.Rows[i]["PFUSERID"].ToString();
                //m.FIRELEVEL = dt.Rows[i]["FIRELEVEL"].ToString();
                m.MANSTATE = dt.Rows[i]["MANSTATE"].ToString();
                m.FIREENDTIME = ClsSwitch.SwitTM(dt.Rows[i]["FIREENDTIME"].ToString());
                m.PFNAME = BaseDT.T_SYSSEC_USER.getName(dtUser, m.PFUSERID);
                m.PFORGNO = dt.Rows[i]["PFORGNO"].ToString();
                m.PFORGNOName = BaseDT.T_SYS_ORG.getName(dtORG, dt.Rows[i]["PFORGNO"].ToString());
                m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                m.FirePropModel = null;//需要再扩充
            }
            dt.Clear();
            dt.Dispose();
            dtUser.Clear();
            dtUser.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            return m;
        }
        #endregion

        #region 获取火灾等级分页
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <param name="total">记录总数</param>
        /// <returns>参见模型</returns>
        public static IEnumerable<JC_FIRE_Model> getModelList(JC_FIRE_SW sw, out int total)
        {
            var result = new List<JC_FIRE_Model>();
            DataTable dtprop = BaseDT.JC_FIRE_PROP.getDT(new JC_FIRE_PROP_SW { });
            DataTable dt = BaseDT.JC_FIRE.GetDCDT(sw, out total);//列表
            DataTable dtUser = BaseDT.T_SYSSEC_USER.getDT(new T_SYSSEC_IPSUSER_SW { });
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位    
            DataTable dt99 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "99" });//火情来源
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                JC_FIRE_Model m = new JC_FIRE_Model();
                m.JCFID = dt.Rows[i]["JCFID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.FIRENAME = dt.Rows[i]["FIRENAME"].ToString();
                m.FIRELEVEL = BaseDT.JC_FIRE_PROP.getfirelevel(dtprop, dt.Rows[i]["JCFID"].ToString());
                m.FIREFROM = dt.Rows[i]["FIREFROM"].ToString();//火情来源
                m.FIREFROMName = BaseDT.T_SYS_DICT.getName(dt99, m.FIREFROM); //火情来源名称
                m.FIREFROMID = dt.Rows[i]["FIREFROMID"].ToString();//原始记录序号
                m.FIRETIME = ClsSwitch.SwitTM(dt.Rows[i]["FIRETIME"].ToString());//起火时间
                m.MARK = dt.Rows[i]["MARK"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.ZQWZ = dt.Rows[i]["ZQWZ"].ToString();
                m.WXBH = dt.Rows[i]["WXBH"].ToString();
                m.DQRDBH = dt.Rows[i]["DQRDBH"].ToString();
                m.RSMJ = dt.Rows[i]["RSMJ"].ToString();
                m.DL = dt.Rows[i]["DL"].ToString();
                m.YY = dt.Rows[i]["YY"].ToString();
                m.ISOUTFIRE = dt.Rows[i]["ISOUTFIRE"].ToString();
                m.JXHQSJ = dt.Rows[i]["JXHQSJ"].ToString();
                m.RECEIVETIME = ClsSwitch.SwitTM(dt.Rows[i]["RECEIVETIME"].ToString());
                m.FIREENDTIME = ClsSwitch.SwitTM(dt.Rows[i]["FIREENDTIME"].ToString());
                m.ISSUEDTIME = ClsSwitch.SwitTM(dt.Rows[i]["ISSUEDTIME"].ToString());
                m.MANSTATE = dt.Rows[i]["MANSTATE"].ToString();
                //m.ISSUEDTIME = ClsSwitch.SwitTM(dt.Rows[i]["LASTPROCESSTIME"].ToString());
                //m.FIRELEVEL = dt.Rows[i]["FIRELEVEL"].ToString();
                m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                m.PFUSERID = dt.Rows[i]["PFUSERID"].ToString();
                m.PFNAME = BaseDT.T_SYSSEC_USER.getName(dtUser, m.PFUSERID);
                m.FirePropModel = null;//需要再扩充
                m.HOTTYPE = dt.Rows[i]["HOTTYPE"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtUser.Clear();
            dtUser.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            dt99.Clear();
            dt99.Dispose();
            return result;
        }
        #endregion

        #region 获取档案统计列表
        /// <summary>
        /// 获取档案统计列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static IEnumerable<JC_FIRE_Model> getList(JC_FIRE_SW sw)
        {
            var result = new List<JC_FIRE_Model>();
            var dt = new DataTable();
            DataTable dtprop = BaseDT.JC_FIRE_PROP.getDT(new JC_FIRE_PROP_SW { });
            if (sw.TYPE == "3")
                dt = BaseDT.JC_FIRE.GetArchivalDT(sw);
            if (sw.TYPE == "1")
                dt = BaseDT.JC_FIRE.GetDTHottype(sw);
            if (sw.TYPE == "2")
                dt = BaseDT.JC_FIRE.GetDTFirelevel(sw);
            DataTable dtUser = BaseDT.T_SYSSEC_USER.getDT(new T_SYSSEC_IPSUSER_SW { });
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dt10 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "10" });//热点类别
            DataTable dt22 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "22" });//火灾等级
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                JC_FIRE_Model m = new JC_FIRE_Model();
                m.JCFID = dt.Rows[i]["JCFID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtORG, dt.Rows[i]["BYORGNO"].ToString());
                m.FIRENAME = dt.Rows[i]["FIRENAME"].ToString();
                //m.FIRELEVEL = BaseDT.JC_FIRE_PROP.getfirelevel(dtprop, dt.Rows[i]["JCFID"].ToString());
                m.FIRELEVEL = dt.Rows[i]["FIRELEVEL"].ToString();
                m.FIRELEVELName = BaseDT.T_SYS_DICT.getName(dt22, m.FIRELEVEL);
                m.FIREFROM = dt.Rows[i]["FIREFROM"].ToString();//火情来源
                if (m.FIREFROM == "1")
                    m.FIREFROMName = "红外相机";
                if (m.FIREFROM == "2")
                    m.FIREFROMName = "卫星监控";
                if (m.FIREFROM == "3")
                    m.FIREFROMName = "电话报警";
                if (m.FIREFROM == "4")
                    m.FIREFROMName = "电子监控";
                if (m.FIREFROM == "5")
                    m.FIREFROMName = "瞭望护林员";
                if (m.FIREFROM == "6")
                    m.FIREFROMName = "无人机巡护";
                //DataTable dtfireticking = BaseDT.JC_FIRETICKLING.Latestfeedback(new JC_FIRETICKLING_SW { JCFID = dt.Rows[i]["JCFID"].ToString()});
                //m.HOTTYPE = BaseDT.JC_FIRETICKLING.gethotetype(dtfireticking, dt.Rows[i]["JCFID"].ToString());
                m.HOTTYPE = dt.Rows[i]["HOTTYPE"].ToString();
                m.HOTTYPEName = BaseDT.T_SYS_DICT.getName(dt10, m.HOTTYPE);
                m.FIREFROMID = dt.Rows[i]["FIREFROMID"].ToString();//原始记录序号
                m.FIRETIME = ClsSwitch.SwitTM(dt.Rows[i]["FIRETIME"].ToString());//起火时间
                m.MARK = dt.Rows[i]["MARK"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.ZQWZ = dt.Rows[i]["ZQWZ"].ToString();
                m.WXBH = dt.Rows[i]["WXBH"].ToString();
                m.DQRDBH = dt.Rows[i]["DQRDBH"].ToString();
                m.RSMJ = dt.Rows[i]["RSMJ"].ToString();
                m.DL = dt.Rows[i]["DL"].ToString();
                m.YY = dt.Rows[i]["YY"].ToString();
                m.ISOUTFIRE = dt.Rows[i]["ISOUTFIRE"].ToString();
                m.JXHQSJ = dt.Rows[i]["JXHQSJ"].ToString();
                m.RECEIVETIME = ClsSwitch.SwitTM(dt.Rows[i]["RECEIVETIME"].ToString());
                m.FIREENDTIME = ClsSwitch.SwitTM(dt.Rows[i]["FIREENDTIME"].ToString());
                m.ISSUEDTIME = ClsSwitch.SwitTM(dt.Rows[i]["ISSUEDTIME"].ToString());
                m.MANSTATE = dt.Rows[i]["MANSTATE"].ToString();
                //m.ISSUEDTIME = ClsSwitch.SwitTM(dt.Rows[i]["LASTPROCESSTIME"].ToString());
                //m.FIRELEVEL = dt.Rows[i]["FIRELEVEL"].ToString();
                m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                m.PFUSERID = dt.Rows[i]["PFUSERID"].ToString();
                m.PFNAME = BaseDT.T_SYSSEC_USER.getName(dtUser, m.PFUSERID);
                m.FirePropModel = null;//需要再扩充
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtUser.Clear();
            dtUser.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            return result;
        }
        #endregion

        #region 获取列表分页
        /// <summary>
        /// 获取列表分页
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static IEnumerable<JC_FIRE_Model> getModelPager(JC_FIRE_SW sw, out int total)
        {
            var result = new List<JC_FIRE_Model>();
            DataTable dt = BaseDT.JC_FIRE.getDT(sw, out total);//列表
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var m = GetModel(dt, i);
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion

        #region Private
        /// <summary>
        /// 获取公益林模型
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private static JC_FIRE_Model GetModel(DataTable dt, int i)
        {
            JC_FIRE_Model m = new JC_FIRE_Model();
            m.JCFID = dt.Rows[i]["JCFID"].ToString();
            m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
            m.FIRENAME = dt.Rows[i]["FIRENAME"].ToString();
            m.FIREFROM = dt.Rows[i]["FIREFROM"].ToString();//火情来源
            if (m.FIREFROM == "1")
                m.FIREFROMName = "红外相机";
            if (m.FIREFROM == "2")
                m.FIREFROMName = "电话报警";
            if (m.FIREFROM == "3")
                m.FIREFROMName = "卫星热点";
            if (m.FIREFROM == "4")
                m.FIREFROMName = "电子报警";
            if (m.FIREFROM == "5")
                m.FIREFROMName = "护林员火情";
            if (m.FIREFROM == "6")
                m.FIREFROMName = "飞机巡护";
            m.FIREFROMID = dt.Rows[i]["FIREFROMID"].ToString();//原始记录序号
            m.FIRETIME = ClsSwitch.SwitTM(dt.Rows[i]["FIRETIME"].ToString());//起火时间
            m.MARK = dt.Rows[i]["MARK"].ToString();
            m.JD = dt.Rows[i]["JD"].ToString();
            m.WD = dt.Rows[i]["WD"].ToString();
            m.ZQWZ = dt.Rows[i]["ZQWZ"].ToString();
            m.WXBH = dt.Rows[i]["WXBH"].ToString();
            m.DQRDBH = dt.Rows[i]["DQRDBH"].ToString();
            m.RSMJ = dt.Rows[i]["RSMJ"].ToString();
            m.DL = dt.Rows[i]["DL"].ToString();
            m.YY = dt.Rows[i]["YY"].ToString();
            m.JXHQSJ = dt.Rows[i]["JXHQSJ"].ToString();
            m.ISOUTFIRE = dt.Rows[i]["ISOUTFIRE"].ToString();
            m.RECEIVETIME = ClsSwitch.SwitTM(dt.Rows[i]["RECEIVETIME"].ToString());
            m.ISSUEDTIME = ClsSwitch.SwitTM(dt.Rows[i]["ISSUEDTIME"].ToString());
            m.PFUSERID = dt.Rows[i]["PFUSERID"].ToString();
            //m.FIRELEVEL = dt.Rows[i]["FIRELEVEL"].ToString();
            m.MANSTATE = dt.Rows[i]["MANSTATE"].ToString();
            m.FIREENDTIME = ClsSwitch.SwitTM(dt.Rows[i]["FIREENDTIME"].ToString());
            //  m.PFNAME = BaseDT.T_SYSSEC_USER.getName(dtUser, m.PFUSERID);
            m.PFORGNO = dt.Rows[i]["PFORGNO"].ToString();
            // m.PFORGNOName = BaseDT.T_SYS_ORG.getName(dtORG, dt.Rows[i]["PFORGNO"].ToString());
            //m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
            m.FirePropModel = null;//需要再扩充
            return m;
        }
        #endregion

        #region 统计当前用户下的火情的记录数量
        /// <summary>
        ///统计当前用户下的火情的记录数量
        /// </summary>
        /// <returns></returns>
        public static string getCount()
        {
            var Count = "";
            Count = BaseDT.JC_FIRE.getNum(new JC_FIRE_SW { BYORGNO = SystemCls.getCurUserOrgNo() });
            return Count;
        }
        #endregion

        #region  根据JCFID获取BYORGNO的值
        /// <summary>
        /// 根据JCFID获取BYORGNO的值
        /// </summary>
        /// <param name="sw">JC_FIRE_SW</param>
        /// <returns></returns>
        public static string getByorgno(JC_FIRE_SW sw)
        {
            DataTable dt = BaseDT.JC_FIRE.getBYORGNO(sw);
            JC_FIRE_Model m = new JC_FIRE_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
            }
            dt.Clear();
            dt.Dispose();
            return m.BYORGNO;

        }
        #endregion
    }
}
