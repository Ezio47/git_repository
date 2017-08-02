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

using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using ManagerSystemModel.SDEModel;

namespace ManagerSystemClassLibrary
{

    /// <summary>
    /// 数据中心_装备_新
    /// </summary>
    public class DC_EQUIP_NEWCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(DC_EQUIP_NEW_Model m)
        {
            if (m.opMethod == "Add")
            {
                //SystemCls.LogSave("3", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.DC_EQUIP_NEW.Add(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
            }
            if (m.opMethod == "Mdy")
            {
                //SystemCls.LogSave("4", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.DC_EQUIP_NEW.Mdy(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);

            }
            if (m.opMethod == "MdyCount")
            {
                //SystemCls.LogSave("4", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.DC_EQUIP_NEW.MdyCount(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);

            }
            if (m.opMethod == "Del")
            {
                //SystemCls.LogSave("5", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.DC_EQUIP_NEW.Del(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
            }
            if (m.opMethod == "Del")
            {
                //SystemCls.LogSave("5", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.DC_EQUIP_NEW.Del(m);
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
        public static DC_EQUIP_NEW_Model getModel(DC_EQUIP_NEW_SW sw)
        {
            var result = new List<DC_EQUIP_NEW_Model>();

            DataTable dt = BaseDT.DC_EQUIP_NEW.getDT(sw);//列表

            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dt32 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "32" });//数据中心装备类型
            DataTable dt36 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "36" });//使用现状
            DC_EQUIP_NEW_Model m = new DC_EQUIP_NEW_Model();

            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.DC_EQUIP_NEW_ID = dt.Rows[i]["DC_EQUIP_NEW_ID"].ToString();
                m.EQUIPTYPE = dt.Rows[i]["EQUIPTYPE"].ToString();
                m.EQUIPTYPEName = BaseDT.T_SYS_DICT.getName(dt32, m.EQUIPTYPE);
                m.NUMBER = dt.Rows[i]["NUMBER"].ToString();
                m.NAME = dt.Rows[i]["NAME"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.ORGName = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                m.MODEL = dt.Rows[i]["MODEL"].ToString();
                m.BUYYEAR = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUYYEAR"].ToString());
                m.USESTATE = dt.Rows[i]["USESTATE"].ToString();
                m.USESTATEName = BaseDT.T_SYS_DICT.getName(dt36, m.USESTATE);
                m.STOREADDR = dt.Rows[i]["STOREADDR"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.WORTH = dt.Rows[i]["WORTH"].ToString();
                m.EQUIPAMOUNT = dt.Rows[i]["EQUIPAMOUNT"].ToString();
                m.REPID = dt.Rows[i]["REPID"].ToString();
                m.DCSUPPROPUNIT = dt.Rows[i]["DCSUPPROPUNIT"].ToString();
                m.PRICE = dt.Rows[i]["PRICE"].ToString();
                if (string.IsNullOrEmpty(m.REPID) == false)
                {
                    m.REPNAME = DC_REPOSITORYCls.getdepotname(m.REPID);
                }
                else
                {
                    m.REPNAME = "";
                }
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            dt32.Clear();
            dt32.Dispose();
            dt36.Clear();
            dt36.Dispose();
            return m;
        }

        #endregion

        #region 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<DC_EQUIP_NEW_Model> getModelList(DC_EQUIP_NEW_SW sw)
        {
            var result = new List<DC_EQUIP_NEW_Model>();

            DataTable dt = BaseDT.DC_EQUIP_NEW.getDT(sw);//列表

            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dt32 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "32" });//数据中心装备类型
            DataTable dt36 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "36" });//使用现状
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DC_EQUIP_NEW_Model m = new DC_EQUIP_NEW_Model();

                m.DC_EQUIP_NEW_ID = dt.Rows[i]["DC_EQUIP_NEW_ID"].ToString();
                m.EQUIPTYPE = dt.Rows[i]["EQUIPTYPE"].ToString();
                m.EQUIPTYPEName = BaseDT.T_SYS_DICT.getName(dt32, m.EQUIPTYPE);
                m.NUMBER = dt.Rows[i]["NUMBER"].ToString();
                m.NAME = dt.Rows[i]["NAME"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.ORGName = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                m.MODEL = dt.Rows[i]["MODEL"].ToString();
                m.BUYYEAR = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUYYEAR"].ToString());
                m.USESTATE = dt.Rows[i]["USESTATE"].ToString();
                m.USESTATEName = BaseDT.T_SYS_DICT.getName(dt36, m.USESTATE);
                m.STOREADDR = dt.Rows[i]["STOREADDR"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.WORTH = dt.Rows[i]["WORTH"].ToString();
                m.EQUIPAMOUNT = dt.Rows[i]["EQUIPAMOUNT"].ToString();
                m.REPID = dt.Rows[i]["REPID"].ToString();
                m.DCSUPPROPUNIT = dt.Rows[i]["DCSUPPROPUNIT"].ToString();
                m.PRICE = dt.Rows[i]["PRICE"].ToString();
                if (string.IsNullOrEmpty(m.REPID) == false)
                {
                    m.REPNAME = DC_REPOSITORYCls.getdepotname(m.REPID);
                }
                else
                {
                    m.REPNAME = "";
                }
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            dt32.Clear();
            dt32.Dispose();
            dt36.Clear();
            dt36.Dispose();
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
        public static IEnumerable<DC_EQUIP_NEW_Model> getModelList(DC_EQUIP_NEW_SW sw, out int total)
        {
            var result = new List<DC_EQUIP_NEW_Model>();

            DataTable dt = BaseDT.DC_EQUIP_NEW.getDT(sw, out total);//列表
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dt32 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "32" });//数据中心装备类型
            DataTable dt36 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "36" });//使用现状

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DC_EQUIP_NEW_Model m = new DC_EQUIP_NEW_Model();

                m.DC_EQUIP_NEW_ID = dt.Rows[i]["DC_EQUIP_NEW_ID"].ToString();
                m.EQUIPTYPE = dt.Rows[i]["EQUIPTYPE"].ToString();
                m.EQUIPTYPEName = BaseDT.T_SYS_DICT.getName(dt32, m.EQUIPTYPE);
                m.NUMBER = dt.Rows[i]["NUMBER"].ToString();
                m.NAME = dt.Rows[i]["NAME"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                //m.ORGName = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                if (m.BYORGNO.Substring(6, 3) != "000" && m.BYORGNO.Substring(9, 6) == "000000")
                {
                    m.ORGName = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);

                }
                m.ORGXSName = BaseDT.T_SYS_ORG.getSXName(dtORG, m.BYORGNO);
                m.MODEL = dt.Rows[i]["MODEL"].ToString();
                m.BUYYEAR = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUYYEAR"].ToString());
                m.USESTATE = dt.Rows[i]["USESTATE"].ToString();
                m.USESTATEName = BaseDT.T_SYS_DICT.getName(dt36, m.USESTATE);
                m.STOREADDR = dt.Rows[i]["STOREADDR"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.WORTH = dt.Rows[i]["WORTH"].ToString();
                m.EQUIPAMOUNT = dt.Rows[i]["EQUIPAMOUNT"].ToString();
                m.REPID = dt.Rows[i]["REPID"].ToString();
                m.DCSUPPROPUNIT = dt.Rows[i]["DCSUPPROPUNIT"].ToString();
                m.PRICE = dt.Rows[i]["PRICE"].ToString();
                if (string.IsNullOrEmpty(m.REPID) == false)
                {
                    m.REPNAME = DC_REPOSITORYCls.getdepotname(m.REPID);
                }
                else
                {
                    m.REPNAME = "";
                }
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            dt32.Clear();
            dt32.Dispose();
            dt36.Clear();
            dt36.Dispose();
            return result;
        }
        #endregion

        #region 装备上传
        /// <summary>
        /// 护林员上传
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static void EQUIPUpload(string filePath)
        {
            HSSFWorkbook hssfworkbook;
            try
            {
                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    hssfworkbook = new HSSFWorkbook(file);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheetAt(0);
            int rowCount = sheet.LastRowNum;
            for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
            {

                IRow row = sheet.GetRow(i);
                string[] arr = new string[12];
                for (int k = 0; k < arr.Length; k++)
                {
                    if (k != 6)
                        arr[k] = row.GetCell(k) == null ? "" : row.GetCell(k).ToString();//循环获取每一单元格内容
                    else
                        arr[k] = row.GetCell(k).DateCellValue.ToString("yyyy-MM-dd");
                }
                DC_EQUIP_NEW_Model m = new DC_EQUIP_NEW_Model();
                //单位	装备类型	名称	编号	型号	使用现状	购买年份 存储地点 数量 价值 经度 纬度
                if (string.IsNullOrEmpty(arr[0]) || string.IsNullOrEmpty(arr[2]))
                {
                    continue;
                }
                m.BYORGNO = BaseDT.T_SYS_ORG.getCodeByName(arr[0]);
                m.NAME = arr[2];
                m.NUMBER = arr[3];
                m.MODEL = arr[4];
                m.BUYYEAR = arr[6];
                if (m.BUYYEAR == "9999-12-31")
                    m.BUYYEAR = "1900-01-01";
                m.STOREADDR = arr[7];
                m.EQUIPAMOUNT = arr[8];
                m.WORTH = arr[9];
                string jd = arr[10];
                string wd = arr[11];
                if (string.IsNullOrEmpty(jd) == false && string.IsNullOrEmpty(wd) == false)
                {
                    double[] brr = ClsPositionTrans.GpsTransform(double.Parse(wd), double.Parse(jd), "1");
                    m.JD = brr[1].ToString();
                    m.WD = brr[0].ToString();
                }
                if (arr[1].Trim() == "扑救类")//装备类型
                {
                    m.EQUIPTYPE = "1";
                }
                else if (arr[1].Trim() == "阻隔类")
                {
                    m.EQUIPTYPE = "2";
                }
                else if (arr[1].Trim() == "防护类")
                {
                    m.EQUIPTYPE = "3";
                }
                else if (arr[1].Trim() == "通讯类")
                {
                    m.EQUIPTYPE = "4";
                }
                else if (arr[1].Trim() == "户外类")
                {
                    m.EQUIPTYPE = "5";
                }
                else if (arr[1].Trim() == "运输类")
                {
                    m.EQUIPTYPE = "6";
                }
                else
                {
                    m.EQUIPTYPE = "1";
                }
                if (arr[5].Trim() == "在用")//使用类型
                {
                    m.USESTATE = "1";
                }
                else if (arr[5].Trim() == "储存")
                {
                    m.USESTATE = "2";
                }
                else if (arr[5].Trim() == "报废")
                {
                    m.USESTATE = "3";
                }
                else
                {
                    m.USESTATE = "1";
                }
                var ms = BaseDT.DC_EQUIP_NEW.Add(m);
                if (string.IsNullOrEmpty(jd) == false && string.IsNullOrEmpty(wd) == false)
                {
                    TD_EQUIP_Model m1 = new TD_EQUIP_Model();
                    m1.OBJECTID = ms.Url;
                    m1.NAME = m.NAME;
                    m1.TYPE = m.EQUIPTYPE;
                    m1.JD = jd;
                    m1.WD = wd;
                    m1.Shape = "geometry::STGeomFromText('POINT(" + m1.JD + " " + m1.WD + ")',4326)";
                    BaseDT.SDE.TD_EQUIP.Add(m1);
                }
                string a = row.GetCell(0).ToString();
                string a1 = row.GetCell(1).ToString();
                string a2 = row.GetCell(2).ToString();

            }

        }
        #endregion

        #region 通过id获取装备类型
        /// <summary>
        /// 通过id获取装备类型
        /// </summary>
        /// <param name="supid"></param>
        /// <returns></returns>
        public static string getEQUIPTYPEName(string supid)
        {
            DC_EQUIP_NEW_Model m = getModel(new DC_EQUIP_NEW_SW { DC_EQUIP_NEW_ID = supid });
            return m.EQUIPTYPEName;
        }
        #endregion

        #region 统计当前用户下的装备的记录数量
        /// <summary>
        ///统计当前用户下的装备的记录数量
        /// </summary>
        /// <returns></returns>
        public static string getCount()
        {
            var Count = "";
            Count = BaseDT.DC_EQUIP_NEW.getNum(new DC_EQUIP_NEW_SW { BYORGNO = SystemCls.getCurUserOrgNo() });
            return Count;
        }
        #endregion
    }
}
