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
    /// 数据中心_设施_防火通道
    /// </summary>
    public class DC_UTILITY_FIRECHANNELCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(DC_UTILITY_FIRECHANNEL_Model m)
        {
            if (m.opMethod == "Add")
            {
                //SystemCls.LogSave("3", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.DC_UTILITY_FIRECHANNEL.Add(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
            }
            if (m.opMethod == "Mdy")
            {
                //SystemCls.LogSave("4", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.DC_UTILITY_FIRECHANNEL.Mdy(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);

            }
            if (m.opMethod == "MdyJWD")
            {
                //SystemCls.LogSave("4", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.DC_UTILITY_FIRECHANNEL.MdyJWD(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);

            }
            if (m.opMethod == "Del")
            {
                //SystemCls.LogSave("5", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.DC_UTILITY_FIRECHANNEL.Del(m);
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
        public static DC_UTILITY_FIRECHANNEL_Model getModel(DC_UTILITY_FIRECHANNEL_SW sw)
        {
            var result = new List<DC_UTILITY_FIRECHANNEL_Model>();

            DataTable dt = BaseDT.DC_UTILITY_FIRECHANNEL.getDT(sw);//列表

            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dt36 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "36" });//使用现状
            DataTable dt37 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "37" });//维护类型
            DataTable dt38 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "38" });//防火通道等级类型
            DataTable dt39 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "39" });//防火通道使用性质
            DC_UTILITY_FIRECHANNEL_Model m = new DC_UTILITY_FIRECHANNEL_Model();

            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.DC_UTILITY_FIRECHANNEL_ID = dt.Rows[i]["DC_UTILITY_FIRECHANNEL_ID"].ToString();
                m.NUMBER = dt.Rows[i]["NUMBER"].ToString();
                m.NAME = dt.Rows[i]["NAME"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.BUILDDATE =PublicClassLibrary.ClsSwitch.SwitDate( dt.Rows[i]["BUILDDATE"].ToString());
                m.BUILDDATEBEGIN = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUILDDATEBEGIN"].ToString());
                m.BUILDDATEEND = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUILDDATEEND"].ToString());
                m.USESTATE = dt.Rows[i]["USESTATE"].ToString();
                m.USESTATEName = BaseDT.T_SYS_DICT.getName(dt36, m.USESTATE);
                m.MANAGERSTATE = dt.Rows[i]["MANAGERSTATE"].ToString();
                m.MANAGERSTATEName = BaseDT.T_SYS_DICT.getName(dt37, m.MANAGERSTATE);
                m.FIRECHANNELLEVELTYPE = dt.Rows[i]["FIRECHANNELLEVELTYPE"].ToString();
                m.FIRECHANNELLEVELTYPEName = BaseDT.T_SYS_DICT.getName(dt38, m.FIRECHANNELLEVELTYPE);
                m.FIRECHANNELUSERTYPE = dt.Rows[i]["FIRECHANNELUSERTYPE"].ToString();
                m.FIRECHANNELUSERTYPEName = BaseDT.T_SYS_DICT.getName(dt39, m.FIRECHANNELUSERTYPE);
                m.LENGTH = dt.Rows[i]["LENGTH"].ToString();
                m.JDBEGIN = dt.Rows[i]["JDBEGIN"].ToString();
                m.WDBEGIN = dt.Rows[i]["WDBEGIN"].ToString();
                m.JDEND = dt.Rows[i]["JDEND"].ToString();
                m.WDEND = dt.Rows[i]["WDEND"].ToString();
                m.JWDLIST = dt.Rows[i]["JWDLIST"].ToString();
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
            dt38.Clear();
            dt38.Dispose();
            dt39.Clear();
            dt39.Dispose();
            return m;
        }

        #endregion

        #region 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<DC_UTILITY_FIRECHANNEL_Model> getModelList(DC_UTILITY_FIRECHANNEL_SW sw)
        {
            var result = new List<DC_UTILITY_FIRECHANNEL_Model>();

            DataTable dt = BaseDT.DC_UTILITY_FIRECHANNEL.getDT(sw);//列表

            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dt36 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "36" });//使用现状
            DataTable dt37 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "37" });//维护类型
            DataTable dt38 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "38" });//防火通道等级类型
            DataTable dt39 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "39" });//防火通道使用性质
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DC_UTILITY_FIRECHANNEL_Model m = new DC_UTILITY_FIRECHANNEL_Model();
                m.DC_UTILITY_FIRECHANNEL_ID = dt.Rows[i]["DC_UTILITY_FIRECHANNEL_ID"].ToString();
                m.NUMBER = dt.Rows[i]["NUMBER"].ToString();
                m.NAME = dt.Rows[i]["NAME"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.BUILDDATE =PublicClassLibrary.ClsSwitch.SwitDate( dt.Rows[i]["BUILDDATE"].ToString());
                m.BUILDDATEBEGIN = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUILDDATEBEGIN"].ToString());
                m.BUILDDATEEND = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUILDDATEEND"].ToString());
                m.USESTATE = dt.Rows[i]["USESTATE"].ToString();
                m.USESTATEName = BaseDT.T_SYS_DICT.getName(dt36, m.USESTATE);
                m.MANAGERSTATE = dt.Rows[i]["MANAGERSTATE"].ToString();
                m.MANAGERSTATEName = BaseDT.T_SYS_DICT.getName(dt37, m.MANAGERSTATE);
                m.FIRECHANNELLEVELTYPE = dt.Rows[i]["FIRECHANNELLEVELTYPE"].ToString();
                m.FIRECHANNELLEVELTYPEName = BaseDT.T_SYS_DICT.getName(dt38, m.FIRECHANNELLEVELTYPE);
                m.FIRECHANNELUSERTYPE = dt.Rows[i]["FIRECHANNELUSERTYPE"].ToString();
                m.FIRECHANNELUSERTYPEName = BaseDT.T_SYS_DICT.getName(dt39, m.FIRECHANNELUSERTYPE);
                m.LENGTH = dt.Rows[i]["LENGTH"].ToString();
                m.JDBEGIN = dt.Rows[i]["JDBEGIN"].ToString();
                m.WDBEGIN = dt.Rows[i]["WDBEGIN"].ToString();
                m.JDEND = dt.Rows[i]["JDEND"].ToString();
                m.WDEND = dt.Rows[i]["WDEND"].ToString();
                m.JWDLIST = dt.Rows[i]["JWDLIST"].ToString();
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
            dt38.Clear();
            dt38.Dispose();
            dt39.Clear();
            dt39.Dispose();
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
        public static IEnumerable<DC_UTILITY_FIRECHANNEL_Model> getModelList(DC_UTILITY_FIRECHANNEL_SW sw, out int total)
        {
            var result = new List<DC_UTILITY_FIRECHANNEL_Model>();

            DataTable dt = BaseDT.DC_UTILITY_FIRECHANNEL.getDT(sw, out total);//列表

            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dt36 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "36" });//使用现状
            DataTable dt37 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "37" });//维护类型
            DataTable dt38 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "38" });//防火通道等级类型
            DataTable dt39 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "39" });//防火通道使用性质

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DC_UTILITY_FIRECHANNEL_Model m = new DC_UTILITY_FIRECHANNEL_Model();
                m.DC_UTILITY_FIRECHANNEL_ID = dt.Rows[i]["DC_UTILITY_FIRECHANNEL_ID"].ToString();
                m.NUMBER = dt.Rows[i]["NUMBER"].ToString();
                m.NAME = dt.Rows[i]["NAME"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.BUILDDATE = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUILDDATE"].ToString());
                m.BUILDDATEBEGIN = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUILDDATEBEGIN"].ToString());
                m.BUILDDATEEND = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUILDDATEEND"].ToString());
                m.USESTATE = dt.Rows[i]["USESTATE"].ToString();
                m.USESTATEName = BaseDT.T_SYS_DICT.getName(dt36, m.USESTATE);
                m.MANAGERSTATE = dt.Rows[i]["MANAGERSTATE"].ToString();
                m.MANAGERSTATEName = BaseDT.T_SYS_DICT.getName(dt37, m.MANAGERSTATE);
                m.FIRECHANNELLEVELTYPE = dt.Rows[i]["FIRECHANNELLEVELTYPE"].ToString();
                m.FIRECHANNELLEVELTYPEName = BaseDT.T_SYS_DICT.getName(dt38, m.FIRECHANNELLEVELTYPE);
                m.FIRECHANNELUSERTYPE = dt.Rows[i]["FIRECHANNELUSERTYPE"].ToString();
                m.FIRECHANNELUSERTYPEName = BaseDT.T_SYS_DICT.getName(dt39, m.FIRECHANNELUSERTYPE);
                m.LENGTH = dt.Rows[i]["LENGTH"].ToString();
                m.JDBEGIN = dt.Rows[i]["JDBEGIN"].ToString();
                m.WDBEGIN = dt.Rows[i]["WDBEGIN"].ToString();
                m.JWDLIST = dt.Rows[i]["JWDLIST"].ToString();
                m.JDEND = dt.Rows[i]["JDEND"].ToString();
                m.WDEND = dt.Rows[i]["WDEND"].ToString();
                m.WORTH = dt.Rows[i]["WORTH"].ToString();
                if (m.BYORGNO.Substring(6, 3) != "000")
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
            dt38.Clear();
            dt38.Dispose();
            dt39.Clear();
            dt39.Dispose();
            return result;
        }
        #endregion

        #region 防火通道上传
        /// <summary>
        /// 护林员上传
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static void FIRECHANNELUpload(string filePath)
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
                string[] arr = new string[10];
                for (int k = 0; k < arr.Length; k++)
                {
                    if (k != 8)
                        arr[k] = row.GetCell(k) == null ? "" : row.GetCell(k).ToString();//循环获取每一单元格内容
                    else
                        arr[k] = row.GetCell(k).DateCellValue.ToString("yyyy-MM-dd");
                }

                DC_UTILITY_FIRECHANNEL_Model m = new DC_UTILITY_FIRECHANNEL_Model();
                //单位	名称  长度	编号 使用现状	维护管理类型 防火通道等级 防火通道使用性质 建设日期 价值 
                if (string.IsNullOrEmpty(arr[0]) || string.IsNullOrEmpty(arr[2]))
                {
                    continue;
                }
                m.BYORGNO = BaseDT.T_SYS_ORG.getCodeByName(arr[0]);
                m.NAME = arr[1];
                m.LENGTH = arr[2];
                m.NUMBER = arr[3];
                m.BUILDDATE = arr[8];
                if (m.BUILDDATE == "9999-12-31")
                    m.BUILDDATE = "1900-01-01";
                m.WORTH = arr[9];       
                if (arr[4].Trim() == "在用")//使用现状
                {
                    m.USESTATE = "1";
                }
                else if (arr[4].Trim() == "储存")
                {
                    m.USESTATE = "2";
                }
                else if (arr[4].Trim() == "报废")
                {
                    m.USESTATE = "3";
                }
                else
                {
                    m.USESTATE = "1";
                }
                if (arr[5].Trim() == "未维护")//维护管理类型
                {
                    m.MANAGERSTATE = "1";
                }
                else if (arr[5].Trim() == "维护")
                {
                    m.MANAGERSTATE = "2";
                }
                else
                {
                    m.MANAGERSTATE = "1";
                }
                if (arr[6].Trim() == "便道")//防火通道等级类型
                {
                    m.FIRECHANNELLEVELTYPE = "1";
                }
                else if (arr[6].Trim() == "林区道路")
                {
                    m.FIRECHANNELLEVELTYPE = "2";
                }
                else
                {
                    m.FIRECHANNELLEVELTYPE = "1";
                }
                if (arr[7].Trim() == "人行道")//防火通道性质类型
                {
                    m.FIRECHANNELUSERTYPE = "1";
                }
                else if (arr[7].Trim() == "车行道")
                {
                    m.FIRECHANNELUSERTYPE = "2";
                }
                else
                {
                    m.FIRECHANNELUSERTYPE = "1";
                }
                BaseDT.DC_UTILITY_FIRECHANNEL.Add(m);
                string a = row.GetCell(0).ToString();
                string a1 = row.GetCell(1).ToString();
                string a2 = row.GetCell(2).ToString();

            }

        }
        #endregion

        #region 统计当前用户下的防火通道的记录数量
        /// <summary>
        ///统计当前用户下的防火通道的记录数量
        /// </summary>
        /// <returns></returns>
        public static string getCount()
        {
            var Count = "";
            Count = BaseDT.DC_UTILITY_FIRECHANNEL.getNum(new DC_UTILITY_FIRECHANNEL_SW { BYORGNO = SystemCls.getCurUserOrgNo() });
            return Count;
        }
        #endregion
    }
}
