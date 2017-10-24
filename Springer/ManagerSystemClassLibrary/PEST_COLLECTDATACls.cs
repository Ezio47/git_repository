using ManagerSystemClassLibrary.BaseDT.SDE;
using ManagerSystemModel;
using ManagerSystemModel.SDEModel;
using ManagerSystemSearchWhereModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 有害生物_采集数据表
    /// </summary>
    public class PEST_COLLECTDATACls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型PEST_COLLECTDATA_Model</param>
        /// <returns>参见模型Message</returns>
        public static Message Manager(PEST_COLLECTDATA_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msg = BaseDT.PEST_COLLECTDATA.Add(m);
                if (msg.Success == false)
                    return new Message(msg.Success, msg.Msg, "");
                msg.Msg = msg.Msg + "," + m.KID;
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Mdy")
            {
                Message msg = BaseDT.PEST_COLLECTDATA.Mdy(m);
                if (msg.Success == false)
                    return new Message(msg.Success, msg.Msg, "");
                msg.Msg = msg.Msg + "," + m.KID;
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Del")
            {
                Message msg = BaseDT.PEST_COLLECTDATA.Del(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            return new Message(false, "无效操作!", m.returnUrl);
        }

        #endregion

        #region 获取单条数据
        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static PEST_COLLECTDATA_Model getModel(PEST_COLLECTDATA_SW sw)
        {
            DataTable dt = BaseDT.PEST_COLLECTDATA.getDT(sw);//列表
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dtBiolo = BaseDT.T_SYS_BIOLOGICALTYPE.getDT(new T_SYS_BIOLOGICALTYPE_SW());//生物类别
            DataTable dt55 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "55" });//危害部位
            DataTable dt56 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "56" });//危害程度
            DataTable dt57 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "57" });//处理状态
            DataTable dt123 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "123" });//调查类型
            PEST_COLLECTDATA_Model m = new PEST_COLLECTDATA_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.PESTCOLLDATAID = dt.Rows[i]["PESTCOLLDATAID"].ToString();
                m.HID = dt.Rows[i]["HID"].ToString();
                m.COLLECTRESOURCE = dt.Rows[i]["COLLECTRESOURCE"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.BYORGNONAME = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                m.VILLAGENAME = dt.Rows[i]["VILLAGENAME"].ToString();
                m.SMALLADDRESS = dt.Rows[i]["SMALLADDRESS"].ToString();
                m.SMALLCLASSCODE = dt.Rows[i]["SMALLCLASSCODE"].ToString();
                m.SMALLCLASSAREA = dt.Rows[i]["SMALLCLASSAREA"].ToString();
                m.HOSTTREESPECIESCODE = dt.Rows[i]["HOSTTREESPECIESCODE"].ToString();
                m.HOSTTREESPECIESNAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.HOSTTREESPECIESCODE);
                m.SEARCHTYPE = dt.Rows[i]["SEARCHTYPE"].ToString();
                m.SEARCHTYPENAME = BaseDT.T_SYS_DICT.getName(dt123, m.SEARCHTYPE);
                m.COLLECTPESTCODE = dt.Rows[i]["COLLECTPESTCODE"].ToString();
                m.COLLECTPESTNAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.COLLECTPESTCODE);
                m.HARMPOSITION = dt.Rows[i]["HARMPOSITION"].ToString();
                m.HARMPOSITIONNAME = BaseDT.T_SYS_DICT.getName(dt55, m.HARMPOSITION);
                m.HARMLEVEL = dt.Rows[i]["HARMLEVEL"].ToString();
                m.HARMLEVELNAME = BaseDT.T_SYS_DICT.getName(dt56, m.HARMPOSITION);
                m.DEADCOUNT = dt.Rows[i]["DEADCOUNT"].ToString();
                m.UNKNOWNDIEOFFCOUNT = dt.Rows[i]["UNKNOWNDIEOFFCOUNT"].ToString();
                m.ELSEDIEOFFCOUNT = dt.Rows[i]["ELSEDIEOFFCOUNT"].ToString();
                m.SAMPLECOUNT = dt.Rows[i]["SAMPLECOUNT"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
                m.UPLOADTIME = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["UPLOADTIME"].ToString());
                m.MANSTATE = dt.Rows[i]["MANSTATE"].ToString();
                m.MANSTATENAME = BaseDT.T_SYS_DICT.getName(dt57, m.MANSTATE);
                m.MANRESULT = dt.Rows[i]["MANRESULT"].ToString();
                m.MANTIME = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["MANTIME"].ToString());
                m.MANUSERID = dt.Rows[i]["MANUSERID"].ToString();
                m.KID = dt.Rows[i]["KID"].ToString();
                m.JWDLIST = dt.Rows[i]["JWDLIST"].ToString();
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            dtBiolo.Clear();
            dtBiolo.Dispose();
            dt55.Clear();
            dt55.Dispose();
            dt56.Clear();
            dt56.Dispose();
            dt57.Clear();
            dt57.Dispose();
            return m;
        }
        #endregion

        #region 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static IEnumerable<PEST_COLLECTDATA_Model> getModeList(PEST_COLLECTDATA_SW sw)
        {
            var result = new List<PEST_COLLECTDATA_Model>();
            DataTable dt = BaseDT.PEST_COLLECTDATA.getDT(sw);
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dtBiolo = BaseDT.T_SYS_BIOLOGICALTYPE.getDT(new T_SYS_BIOLOGICALTYPE_SW());//生物类别
            DataTable dt102 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "102" });//危害部位
            DataTable dt103 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "103" });//危害程度
            DataTable dt104 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "104" });//处理状态
            DataTable dt123 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "123" });//调查类型
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PEST_COLLECTDATA_Model m = new PEST_COLLECTDATA_Model();
                m.PESTCOLLDATAID = dt.Rows[i]["PESTCOLLDATAID"].ToString();
                m.HID = dt.Rows[i]["HID"].ToString();
                m.COLLECTRESOURCE = dt.Rows[i]["COLLECTRESOURCE"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.BYORGNONAME = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                m.VILLAGENAME = dt.Rows[i]["VILLAGENAME"].ToString();
                m.SMALLADDRESS = dt.Rows[i]["SMALLADDRESS"].ToString();
                m.SMALLCLASSCODE = dt.Rows[i]["SMALLCLASSCODE"].ToString();
                m.SMALLCLASSAREA = dt.Rows[i]["SMALLCLASSAREA"].ToString();
                m.HOSTTREESPECIESCODE = dt.Rows[i]["HOSTTREESPECIESCODE"].ToString();
                m.HOSTTREESPECIESNAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.HOSTTREESPECIESCODE);
                m.SEARCHTYPE = dt.Rows[i]["SEARCHTYPE"].ToString();
                m.SEARCHTYPENAME = BaseDT.T_SYS_DICT.getName(dt123, m.SEARCHTYPE);
                m.COLLECTPESTCODE = dt.Rows[i]["COLLECTPESTCODE"].ToString();
                m.COLLECTPESTNAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.COLLECTPESTCODE);
                m.HARMPOSITION = dt.Rows[i]["HARMPOSITION"].ToString();
                m.HARMPOSITIONNAME = BaseDT.T_SYS_DICT.getName(dt102, m.HARMPOSITION);
                m.HARMLEVEL = dt.Rows[i]["HARMLEVEL"].ToString();
                m.HARMLEVELNAME = BaseDT.T_SYS_DICT.getName(dt103, m.HARMPOSITION);
                m.DEADCOUNT = dt.Rows[i]["DEADCOUNT"].ToString();
                m.UNKNOWNDIEOFFCOUNT = dt.Rows[i]["UNKNOWNDIEOFFCOUNT"].ToString();
                m.ELSEDIEOFFCOUNT = dt.Rows[i]["ELSEDIEOFFCOUNT"].ToString();
                m.SAMPLECOUNT = dt.Rows[i]["SAMPLECOUNT"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
                m.UPLOADTIME = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["UPLOADTIME"].ToString());
                m.MANSTATE = dt.Rows[i]["MANSTATE"].ToString();
                m.MANSTATENAME = BaseDT.T_SYS_DICT.getName(dt104, m.MANSTATE);
                m.MANRESULT = dt.Rows[i]["MANRESULT"].ToString();
                m.MANTIME = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["MANTIME"].ToString());
                m.MANUSERID = dt.Rows[i]["MANUSERID"].ToString();
                m.KID = dt.Rows[i]["KID"].ToString();
                m.JWDLIST = dt.Rows[i]["JWDLIST"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            dtBiolo.Clear();
            dtBiolo.Dispose();
            dt102.Clear();
            dt102.Dispose();
            dt103.Clear();
            dt103.Dispose();
            dt104.Clear();
            dt104.Dispose();
            dt123.Clear();
            dt123.Dispose();
            return result;
        }

        /// <summary>
        /// 分页获取用户列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <param name="total">记录总数</param>
        /// <returns>参见模型</returns>
        public static IEnumerable<PEST_COLLECTDATA_Model> getModeList(PEST_COLLECTDATA_SW sw, out int total)
        {
            var result = new List<PEST_COLLECTDATA_Model>();
            DataTable dt = BaseDT.PEST_COLLECTDATA.getDT(sw, out total);
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dtBiolo = BaseDT.T_SYS_BIOLOGICALTYPE.getDT(new T_SYS_BIOLOGICALTYPE_SW());//生物类别
            DataTable dt102 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "102" });//危害部位
            DataTable dt103 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "103" });//危害程度
            DataTable dt104 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "104" });//处理状态
            DataTable dt123 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "123" });//调查类型
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PEST_COLLECTDATA_Model m = new PEST_COLLECTDATA_Model();
                m.PESTCOLLDATAID = dt.Rows[i]["PESTCOLLDATAID"].ToString();
                m.HID = dt.Rows[i]["HID"].ToString();
                m.COLLECTRESOURCE = dt.Rows[i]["COLLECTRESOURCE"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.BYORGNONAME = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                m.VILLAGENAME = dt.Rows[i]["VILLAGENAME"].ToString();
                m.SMALLADDRESS = dt.Rows[i]["SMALLADDRESS"].ToString();
                m.SMALLCLASSCODE = dt.Rows[i]["SMALLCLASSCODE"].ToString();
                m.SMALLCLASSAREA = dt.Rows[i]["SMALLCLASSAREA"].ToString();
                m.HOSTTREESPECIESCODE = dt.Rows[i]["HOSTTREESPECIESCODE"].ToString();
                m.HOSTTREESPECIESNAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.HOSTTREESPECIESCODE);
                m.SEARCHTYPE = dt.Rows[i]["SEARCHTYPE"].ToString();
                m.SEARCHTYPENAME = BaseDT.T_SYS_DICT.getName(dt123, m.SEARCHTYPE);
                m.COLLECTPESTCODE = dt.Rows[i]["COLLECTPESTCODE"].ToString();
                m.COLLECTPESTNAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.COLLECTPESTCODE);
                m.HARMPOSITION = dt.Rows[i]["HARMPOSITION"].ToString();
                m.HARMPOSITIONNAME = BaseDT.T_SYS_DICT.getName(dt102, m.HARMPOSITION);
                m.HARMLEVEL = dt.Rows[i]["HARMLEVEL"].ToString();
                m.HARMLEVELNAME = BaseDT.T_SYS_DICT.getName(dt103, m.HARMPOSITION);
                m.DEADCOUNT = dt.Rows[i]["DEADCOUNT"].ToString();
                m.UNKNOWNDIEOFFCOUNT = dt.Rows[i]["UNKNOWNDIEOFFCOUNT"].ToString();
                m.ELSEDIEOFFCOUNT = dt.Rows[i]["ELSEDIEOFFCOUNT"].ToString();
                m.SAMPLECOUNT = dt.Rows[i]["SAMPLECOUNT"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
                m.UPLOADTIME = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["UPLOADTIME"].ToString());
                m.MANSTATE = dt.Rows[i]["MANSTATE"].ToString();
                m.MANSTATENAME = BaseDT.T_SYS_DICT.getName(dt104, m.MANSTATE);
                m.MANRESULT = dt.Rows[i]["MANRESULT"].ToString();
                m.MANTIME = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["MANTIME"].ToString());
                m.MANUSERID = dt.Rows[i]["MANUSERID"].ToString();
                m.KID = dt.Rows[i]["KID"].ToString();
                m.JWDLIST = dt.Rows[i]["JWDLIST"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            dtBiolo.Clear();
            dtBiolo.Dispose();
            dt102.Clear();
            dt102.Dispose();
            dt103.Clear();
            dt103.Dispose();
            dt104.Clear();
            dt104.Dispose();
            dt123.Clear();
            dt123.Dispose();
            return result;
        }
        #endregion
    }
}
