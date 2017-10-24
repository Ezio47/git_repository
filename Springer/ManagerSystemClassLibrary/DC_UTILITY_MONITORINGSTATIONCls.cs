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
    /// 数据中心_设施_监测站
    /// </summary>
    public class DC_UTILITY_MONITORINGSTATIONCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(DC_UTILITY_MONITORINGSTATION_Model m)
        {
            if (m.opMethod == "Add")
            {
                //SystemCls.LogSave("3", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.DC_UTILITY_MONITORINGSTATION.Add(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
            }
            if (m.opMethod == "Mdy")
            {
                //SystemCls.LogSave("4", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.DC_UTILITY_MONITORINGSTATION.Mdy(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);

            }
            if (m.opMethod == "MdyJWD")
            {
                //SystemCls.LogSave("4", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.DC_UTILITY_MONITORINGSTATION.MdyJWD(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);

            }
            if (m.opMethod == "Del")
            {
                //SystemCls.LogSave("5", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.DC_UTILITY_MONITORINGSTATION.Del(m);
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
        public static DC_UTILITY_MONITORINGSTATION_Model getModel(DC_UTILITY_MONITORINGSTATION_SW sw)
        {
            var result = new List<DC_UTILITY_MONITORINGSTATION_Model>();

            DataTable dt = BaseDT.DC_UTILITY_MONITORINGSTATION.getDT(sw);//列表

            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dt36 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "36" });//使用现状
            DataTable dt37 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "37" });//维护类型
            DataTable dt42 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "42" });//无线电传输方式
            DC_UTILITY_MONITORINGSTATION_Model m = new DC_UTILITY_MONITORINGSTATION_Model();

            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.DC_UTILITY_MONITORINGSTATION_ID = dt.Rows[i]["DC_UTILITY_MONITORINGSTATION_ID"].ToString();
                m.NUMBER = dt.Rows[i]["NUMBER"].ToString();
                m.NAME = dt.Rows[i]["NAME"].ToString();
                m.ADDRESS = dt.Rows[i]["ADDRESS"].ToString();
                m.MODEL = dt.Rows[i]["MODEL"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.TRANSFERMODETYPE = dt.Rows[i]["TRANSFERMODETYPE"].ToString();
                m.TRANSFERMODETYPEName = BaseDT.T_SYS_DICT.getName(dt42, m.TRANSFERMODETYPE);
                m.MONICONTENT = dt.Rows[i]["MONICONTENT"].ToString();
                m.BUILDDATE = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUILDDATE"].ToString());
                m.BUILDDATEBEGIN = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUILDDATEBEGIN"].ToString());
                m.BUILDDATEEND = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUILDDATEEND"].ToString());
                m.USESTATE = dt.Rows[i]["USESTATE"].ToString();
                m.USESTATEName = BaseDT.T_SYS_DICT.getName(dt36, m.USESTATE);
                m.MANAGERSTATE = dt.Rows[i]["MANAGERSTATE"].ToString();
                m.MANAGERSTATEName = BaseDT.T_SYS_DICT.getName(dt37, m.MANAGERSTATE);
                m.MARK = dt.Rows[i]["MARK"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.WORTH = dt.Rows[i]["WORTH"].ToString();
                m.ORGName = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            dt36.Clear();
            dt36.Dispose();
            dt37.Clear();
            dt37.Dispose();
            dt42.Clear();
            dt42.Dispose();
            return m;
        }

        #endregion

        #region 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<DC_UTILITY_MONITORINGSTATION_Model> getModelList(DC_UTILITY_MONITORINGSTATION_SW sw)
        {
            var result = new List<DC_UTILITY_MONITORINGSTATION_Model>();

            DataTable dt = BaseDT.DC_UTILITY_MONITORINGSTATION.getDT(sw);//列表

            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dt36 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "36" });//使用现状
            DataTable dt37 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "37" });//维护类型
            DataTable dt42 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "42" });//无线电传输方式
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DC_UTILITY_MONITORINGSTATION_Model m = new DC_UTILITY_MONITORINGSTATION_Model();
                m.DC_UTILITY_MONITORINGSTATION_ID = dt.Rows[i]["DC_UTILITY_MONITORINGSTATION_ID"].ToString();
                m.NUMBER = dt.Rows[i]["NUMBER"].ToString();
                m.NAME = dt.Rows[i]["NAME"].ToString();
                m.ADDRESS = dt.Rows[i]["ADDRESS"].ToString();
                m.MODEL = dt.Rows[i]["MODEL"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.TRANSFERMODETYPE = dt.Rows[i]["TRANSFERMODETYPE"].ToString();
                m.TRANSFERMODETYPEName = BaseDT.T_SYS_DICT.getName(dt42, m.TRANSFERMODETYPE);
                m.MONICONTENT = dt.Rows[i]["MONICONTENT"].ToString();
                m.BUILDDATE = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUILDDATE"].ToString());
                m.BUILDDATEBEGIN = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUILDDATEBEGIN"].ToString());
                m.BUILDDATEEND = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUILDDATEEND"].ToString());
                m.USESTATE = dt.Rows[i]["USESTATE"].ToString();
                m.USESTATEName = BaseDT.T_SYS_DICT.getName(dt36, m.USESTATE);
                m.MANAGERSTATE = dt.Rows[i]["MANAGERSTATE"].ToString();
                m.MANAGERSTATEName = BaseDT.T_SYS_DICT.getName(dt37, m.MANAGERSTATE);
                m.MARK = dt.Rows[i]["MARK"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.WORTH = dt.Rows[i]["WORTH"].ToString();
                m.ORGName = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            dt36.Clear();
            dt36.Dispose();
            dt37.Clear();
            dt37.Dispose();
            dt42.Clear();
            dt42.Dispose();
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
        public static IEnumerable<DC_UTILITY_MONITORINGSTATION_Model> getModelList(DC_UTILITY_MONITORINGSTATION_SW sw, out int total)
        {
            var result = new List<DC_UTILITY_MONITORINGSTATION_Model>();

            DataTable dt = BaseDT.DC_UTILITY_MONITORINGSTATION.getDT(sw, out total);//列表

            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dt36 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "36" });//使用现状
            DataTable dt37 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "37" });//维护类型
            DataTable dt42 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "42" });//无线电传输方式

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DC_UTILITY_MONITORINGSTATION_Model m = new DC_UTILITY_MONITORINGSTATION_Model();
                m.DC_UTILITY_MONITORINGSTATION_ID = dt.Rows[i]["DC_UTILITY_MONITORINGSTATION_ID"].ToString();
                m.NUMBER = dt.Rows[i]["NUMBER"].ToString();
                m.NAME = dt.Rows[i]["NAME"].ToString();
                m.ADDRESS = dt.Rows[i]["ADDRESS"].ToString();
                m.MODEL = dt.Rows[i]["MODEL"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.TRANSFERMODETYPE = dt.Rows[i]["TRANSFERMODETYPE"].ToString();
                m.TRANSFERMODETYPEName = BaseDT.T_SYS_DICT.getName(dt42, m.TRANSFERMODETYPE);
                m.MONICONTENT = dt.Rows[i]["MONICONTENT"].ToString();
                m.BUILDDATE = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUILDDATE"].ToString());
                m.BUILDDATEBEGIN = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUILDDATEBEGIN"].ToString());
                m.BUILDDATEEND = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUILDDATEEND"].ToString());
                m.USESTATE = dt.Rows[i]["USESTATE"].ToString();
                m.USESTATEName = BaseDT.T_SYS_DICT.getName(dt36, m.USESTATE);
                m.MANAGERSTATE = dt.Rows[i]["MANAGERSTATE"].ToString();
                m.MANAGERSTATEName = BaseDT.T_SYS_DICT.getName(dt37, m.MANAGERSTATE);
                m.MARK = dt.Rows[i]["MARK"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.WORTH = dt.Rows[i]["WORTH"].ToString();
                if (m.BYORGNO.Substring(6, 3) != "000" && m.BYORGNO.Substring(9, 6) == "000000")
                {
                    m.ORGName = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                }
                m.ORGXSName = BaseDT.T_SYS_ORG.getSXName(dtORG, m.BYORGNO);
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            dt36.Clear();
            dt36.Dispose();
            dt37.Clear();
            dt37.Dispose();
            dt42.Clear();
            dt42.Dispose();
            return result;
        }
        #endregion

        #region 监测站上传
        /// <summary>
        /// 监测站上传
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static void MONITORINGSTATIONUpload(string filePath)
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
                string[] arr = new string[13];
                for (int k = 0; k < arr.Length; k++)
                {
                    if (k != 7)
                        arr[k] = row.GetCell(k) == null ? "" : row.GetCell(k).ToString();//循环获取每一单元格内容
                    else
                        arr[k] = row.GetCell(k).DateCellValue.ToString("yyyy-MM-dd");
                }
                DC_UTILITY_MONITORINGSTATION_Model m = new DC_UTILITY_MONITORINGSTATION_Model();
                //单位	无线电方式	名称	编号	型号	使用现状	维护管理类型 建设日期  监测内容 价值 地址 经度 纬度
                if (string.IsNullOrEmpty(arr[0]) || string.IsNullOrEmpty(arr[2]))
                {
                    continue;
                }
                m.BYORGNO = BaseDT.T_SYS_ORG.getCodeByName(arr[0]);
                m.NAME = arr[2];
                m.NUMBER = arr[3];
                m.MODEL = arr[4];
                m.BUILDDATE = arr[7];
                if (m.BUILDDATE == "9999-12-31")
                    m.BUILDDATE = "1900-01-01";
                m.MONICONTENT = arr[8];
                m.WORTH = arr[9];
                m.ADDRESS = arr[10];
                string jd = arr[11];
                string wd = arr[12];
                if (string.IsNullOrEmpty(jd) == false && string.IsNullOrEmpty(wd) == false)
                {
                    double[] brr = ClsPositionTrans.GpsTransform(double.Parse(wd), double.Parse(jd), "1");
                    m.JD = brr[1].ToString();
                    m.WD = brr[0].ToString();
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
                if (arr[6].Trim() == "未维护")//维护管理类型
                {
                    m.MANAGERSTATE = "1";
                }
                else if (arr[6].Trim() == "维护")
                {
                    m.MANAGERSTATE = "2";
                }
                else
                {
                    m.MANAGERSTATE = "1";
                }
                if (arr[1].Trim() == "有线")//无线电方式
                {
                    m.TRANSFERMODETYPE = "1";
                }
                else if (arr[1].Trim() == "无线")
                {
                    m.TRANSFERMODETYPE = "2";
                }
                else
                {
                    m.TRANSFERMODETYPE = "1";
                }
                var ms = BaseDT.DC_UTILITY_MONITORINGSTATION.Add(m);
                if (string.IsNullOrEmpty(jd) == false && string.IsNullOrEmpty(wd) == false)
                {
                    TD_MONITORINGSTATION_Model m1 = new TD_MONITORINGSTATION_Model();
                    m1.OBJECTID = ms.Url;
                    m1.NAME = m.NAME;
                    m1.TYPE = m.TRANSFERMODETYPE;
                    m1.JD = jd;
                    m1.WD = wd;
                    m1.Shape = "geometry::STGeomFromText('POINT(" + m1.JD + " " + m1.WD + ")',4326)";
                    BaseDT.SDE.TD_MONITORINGSTATION.Add(m1);
                }
                string a = row.GetCell(0).ToString();
                string a1 = row.GetCell(1).ToString();
                string a2 = row.GetCell(2).ToString();
            }
        }
        #endregion

        #region 统计当前用户下的监测站的记录数量
        /// <summary>
        ///统计当前用户下的监测站的记录数量
        /// </summary>
        /// <returns></returns>
        public static string getCount()
        {
            var Count = "";
            Count = BaseDT.DC_UTILITY_MONITORINGSTATION.getNum(new DC_UTILITY_MONITORINGSTATION_SW { BYORGNO = SystemCls.getCurUserOrgNo() });
            return Count;
        }
        #endregion
    }
}
