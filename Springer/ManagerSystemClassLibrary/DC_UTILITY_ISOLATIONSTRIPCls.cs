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
    /// 数据中心_设施_隔离带
    /// </summary>
    public class DC_UTILITY_ISOLATIONSTRIPCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(DC_UTILITY_ISOLATIONSTRIP_Model m)
        {
            if (m.opMethod == "Add")
            {
                //SystemCls.LogSave("3", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.DC_UTILITY_ISOLATIONSTRIP.Add(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
            }
            if (m.opMethod == "Mdy")
            {
                //SystemCls.LogSave("4", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.DC_UTILITY_ISOLATIONSTRIP.Mdy(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);

            }
            if (m.opMethod == "MdyJWD")
            {
                //SystemCls.LogSave("4", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.DC_UTILITY_ISOLATIONSTRIP.MdyJWD(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);

            }
            if (m.opMethod == "Del")
            {
                //SystemCls.LogSave("5", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.DC_UTILITY_ISOLATIONSTRIP.Del(m);
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
        public static DC_UTILITY_ISOLATIONSTRIP_Model getModel(DC_UTILITY_ISOLATIONSTRIP_SW sw)
        {
            var result = new List<DC_UTILITY_ISOLATIONSTRIP_Model>();
            DataTable dt = BaseDT.DC_UTILITY_ISOLATIONSTRIP.getDT(sw);//列表
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dt35 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "35" });//防火隔离带类型
            DataTable dt36 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "36" });//使用现状
            DataTable dt37 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "37" });//维护类型
            DataTable dt52 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "52" });//树种
            DataTable dt53 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "53" });//规划生物隔离带位置
            DC_UTILITY_ISOLATIONSTRIP_Model m = new DC_UTILITY_ISOLATIONSTRIP_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.DC_UTILITY_ISOLATIONSTRIP_ID = dt.Rows[i]["DC_UTILITY_ISOLATIONSTRIP_ID"].ToString();
                m.ISOLATIONTYPE = dt.Rows[i]["ISOLATIONTYPE"].ToString();
                m.ISOLATIONTYPEName = BaseDT.T_SYS_DICT.getName(dt35, m.ISOLATIONTYPE);
                m.NUMBER = dt.Rows[i]["NUMBER"].ToString();
                m.NAME = dt.Rows[i]["NAME"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.BUILDDATE = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUILDDATE"].ToString());
                m.ENTRYTIME = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["ENTRYTIME"].ToString());
                //m.ENTRYTIME = ClsSwitch.SwitTM(dt.Rows[i]["ENTRYTIME"].ToString());
                m.USESTATE = dt.Rows[i]["USESTATE"].ToString();
                m.USESTATEName = BaseDT.T_SYS_DICT.getName(dt36, m.USESTATE);
                m.MANAGERSTATE = dt.Rows[i]["MANAGERSTATE"].ToString();
                m.MANAGERSTATEName = BaseDT.T_SYS_DICT.getName(dt37, m.MANAGERSTATE);
                m.WIDTH = dt.Rows[i]["WIDTH"].ToString();
                m.LENGTH = dt.Rows[i]["LENGTH"].ToString();
                m.JDBEGIN = dt.Rows[i]["JDBEGIN"].ToString();
                m.WDBEGIN = dt.Rows[i]["WDBEGIN"].ToString();
                m.JDEND = dt.Rows[i]["JDEND"].ToString();
                m.WDEND = dt.Rows[i]["WDEND"].ToString();
                m.JWDLIST = dt.Rows[i]["JWDLIST"].ToString();
                m.PLANAREA = dt.Rows[i]["PLANAREA"].ToString();
                m.REALAREA = dt.Rows[i]["REALAREA"].ToString();
                m.WORTH = dt.Rows[i]["WORTH"].ToString();
                m.KINDTYPE = dt.Rows[i]["KINDTYPE"].ToString();
                m.TREETYPE = dt.Rows[i]["TREETYPE"].ToString();
                m.TREETYPEName = BaseDT.T_SYS_DICT.getName(dt52, m.TREETYPE);
                m.Position =dt.Rows[i]["POSITION"].ToString();
                m.PositionName = BaseDT.T_SYS_DICT.getName(dt53, m.Position);
                m.Price = dt.Rows[i]["PRICE"].ToString();
                m.AlleywayWideth = dt.Rows[i]["ALLEYWAYWIDETH"].ToString();
                m.ORGName = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                m.BUILDDATEBEGIN = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUILDDATEBEGIN"].ToString());
                m.BUILDDATEEND = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUILDDATEEND"].ToString());
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            dt35.Clear();
            dt35.Dispose();
            dt36.Clear();
            dt36.Dispose();
            dt37.Clear();
            dt37.Dispose();
            return m;
        }

        #endregion

        #region 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<DC_UTILITY_ISOLATIONSTRIP_Model> getModelList(DC_UTILITY_ISOLATIONSTRIP_SW sw)
        {
            var result = new List<DC_UTILITY_ISOLATIONSTRIP_Model>();

            DataTable dt = BaseDT.DC_UTILITY_ISOLATIONSTRIP.getDT(sw);//列表

            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dt35 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "35" });//防火隔离带类型
            DataTable dt36 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "36" });//使用现状
            DataTable dt37 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "37" });//维护类型
            DataTable dt52 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "52" });//树种
            DataTable dt53 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "53" });//规划生物隔离带位置
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DC_UTILITY_ISOLATIONSTRIP_Model m = new DC_UTILITY_ISOLATIONSTRIP_Model();
                m.DC_UTILITY_ISOLATIONSTRIP_ID = dt.Rows[i]["DC_UTILITY_ISOLATIONSTRIP_ID"].ToString();
                m.ISOLATIONTYPE = dt.Rows[i]["ISOLATIONTYPE"].ToString();
                m.ISOLATIONTYPEName = BaseDT.T_SYS_DICT.getName(dt35, m.ISOLATIONTYPE);
                m.NUMBER = dt.Rows[i]["NUMBER"].ToString();
                m.NAME = dt.Rows[i]["NAME"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.BUILDDATE = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUILDDATE"].ToString());
                m.ENTRYTIME = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["ENTRYTIME"].ToString());
                m.USESTATE = dt.Rows[i]["USESTATE"].ToString();
                m.USESTATEName = BaseDT.T_SYS_DICT.getName(dt36, m.USESTATE);
                m.MANAGERSTATE = dt.Rows[i]["MANAGERSTATE"].ToString();
                m.MANAGERSTATEName = BaseDT.T_SYS_DICT.getName(dt37, m.MANAGERSTATE);
                m.WIDTH = dt.Rows[i]["WIDTH"].ToString();
                m.LENGTH = dt.Rows[i]["LENGTH"].ToString();
                m.JDBEGIN = dt.Rows[i]["JDBEGIN"].ToString();
                m.WDBEGIN = dt.Rows[i]["WDBEGIN"].ToString();
                m.JDEND = dt.Rows[i]["JDEND"].ToString();
                m.WDEND = dt.Rows[i]["WDEND"].ToString();
                m.JWDLIST = dt.Rows[i]["JWDLIST"].ToString();
                m.PLANAREA = dt.Rows[i]["PLANAREA"].ToString();
                m.REALAREA = dt.Rows[i]["REALAREA"].ToString();
                m.WORTH = dt.Rows[i]["WORTH"].ToString();
                m.KINDTYPE = dt.Rows[i]["KINDTYPE"].ToString();
                m.TREETYPE = dt.Rows[i]["TREETYPE"].ToString();
                m.TREETYPEName = BaseDT.T_SYS_DICT.getName(dt52, m.TREETYPE);
                m.ORGName = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                m.Position = dt.Rows[i]["POSITION"].ToString();
                m.PositionName = BaseDT.T_SYS_DICT.getName(dt53, m.Position);
                m.Price = dt.Rows[i]["PRICE"].ToString();
                m.AlleywayWideth = dt.Rows[i]["ALLEYWAYWIDETH"].ToString();
                m.BUILDDATEBEGIN = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUILDDATEBEGIN"].ToString());
                m.BUILDDATEEND = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUILDDATEEND"].ToString());
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            dt35.Clear();
            dt35.Dispose();
            dt36.Clear();
            dt36.Dispose();
            dt37.Clear();
            dt37.Dispose();
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
        public static IEnumerable<DC_UTILITY_ISOLATIONSTRIP_Model> getModelList(DC_UTILITY_ISOLATIONSTRIP_SW sw, out int total)
        {
            var result = new List<DC_UTILITY_ISOLATIONSTRIP_Model>();

            DataTable dt = BaseDT.DC_UTILITY_ISOLATIONSTRIP.getDT(sw, out total);//列表

            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dt35 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "35" });//防火隔离带类型
            DataTable dt36 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "36" });//使用现状
            DataTable dt37 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "37" });//维护类型
            DataTable dt52 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "52" });//树种
            DataTable dt53 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "53" });//规划生物隔离带位置
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DC_UTILITY_ISOLATIONSTRIP_Model m = new DC_UTILITY_ISOLATIONSTRIP_Model();
                m.DC_UTILITY_ISOLATIONSTRIP_ID = dt.Rows[i]["DC_UTILITY_ISOLATIONSTRIP_ID"].ToString();
                m.ISOLATIONTYPE = dt.Rows[i]["ISOLATIONTYPE"].ToString();
                m.ISOLATIONTYPEName = BaseDT.T_SYS_DICT.getName(dt35, m.ISOLATIONTYPE);
                m.NUMBER = dt.Rows[i]["NUMBER"].ToString();
                m.NAME = dt.Rows[i]["NAME"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.BUILDDATE = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUILDDATE"].ToString());
                m.ENTRYTIME = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["ENTRYTIME"].ToString());
                m.USESTATE = dt.Rows[i]["USESTATE"].ToString();
                m.USESTATEName = BaseDT.T_SYS_DICT.getName(dt36, m.USESTATE);
                m.MANAGERSTATE = dt.Rows[i]["MANAGERSTATE"].ToString();
                m.MANAGERSTATEName = BaseDT.T_SYS_DICT.getName(dt37, m.MANAGERSTATE);
                m.WIDTH = dt.Rows[i]["WIDTH"].ToString();
                m.LENGTH = dt.Rows[i]["LENGTH"].ToString();
                m.JDBEGIN = dt.Rows[i]["JDBEGIN"].ToString();
                m.WDBEGIN = dt.Rows[i]["WDBEGIN"].ToString();
                m.JDEND = dt.Rows[i]["JDEND"].ToString();
                m.WDEND = dt.Rows[i]["WDEND"].ToString();
                m.JWDLIST = dt.Rows[i]["JWDLIST"].ToString();
                m.PLANAREA = dt.Rows[i]["PLANAREA"].ToString();
                m.REALAREA = dt.Rows[i]["REALAREA"].ToString();
                m.WORTH = dt.Rows[i]["WORTH"].ToString();
                m.KINDTYPE = dt.Rows[i]["KINDTYPE"].ToString();
                m.TREETYPE = dt.Rows[i]["TREETYPE"].ToString();
                m.BUILDDATEBEGIN = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUILDDATEBEGIN"].ToString());
                m.BUILDDATEEND = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUILDDATEEND"].ToString());
                m.TREETYPEName = BaseDT.T_SYS_DICT.getName(dt52, m.TREETYPE);
                if (m.BYORGNO.Substring(6, 3) != "000")
                {
                    m.ORGName = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);

                }
                m.ORGXSName = BaseDT.T_SYS_ORG.getSXName(dtORG, m.BYORGNO);
                m.Position = dt.Rows[i]["POSITION"].ToString();
                m.PositionName = BaseDT.T_SYS_DICT.getName(dt53, m.Position);
                m.Price = dt.Rows[i]["PRICE"].ToString();
                m.AlleywayWideth = dt.Rows[i]["ALLEYWAYWIDETH"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            dt35.Clear();
            dt35.Dispose();
            dt36.Clear();
            dt36.Dispose();
            dt37.Clear();
            dt37.Dispose();
            return result;
        }
        #endregion

        #region 隔离带上传
        /// <summary>
        /// 护林员上传
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static void ISOLATIONSTRIPUpload(string filePath)
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
                        arr[k] = row.GetCell(k) == null ? "" : row.GetCell(k).ToString();//循环获取每一单元格内容
                }
                DC_UTILITY_ISOLATIONSTRIP_Model m = new DC_UTILITY_ISOLATIONSTRIP_Model();
                //单位	隔离带类型	名称	编号	使用现状 维护类型	宽度 长度 计划面积 实际面积 价值 树种
                if (string.IsNullOrEmpty(arr[0]) || string.IsNullOrEmpty(arr[2]))
                {
                    continue;
                }
                m.BYORGNO = BaseDT.T_SYS_ORG.getCodeByName(arr[0]);
                m.NAME = arr[2];
                m.NUMBER = arr[3];
                m.WIDTH = arr[6];
                m.LENGTH = arr[7];
                m.PLANAREA = arr[8];
                m.REALAREA = arr[9];
                m.WORTH = arr[11];
                m.KINDTYPE = arr[10];
                if (arr[1].Trim() == "生物")//隔离带类型
                {
                    m.ISOLATIONTYPE = "1";
                }
                else if (arr[1].Trim() == "生土")
                {
                    m.ISOLATIONTYPE = "2";
                }
                else if (arr[1].Trim() == "火烧线")
                {
                    m.ISOLATIONTYPE = "3";
                }
                else if (arr[1].Trim() == "计划烧除")
                {
                    m.ISOLATIONTYPE = "4";
                }
                else
                {
                    m.ISOLATIONTYPE = "1";
                }
                if (arr[4].Trim() == "在用")//使用类型
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
                if (arr[5].Trim() == "维护")//使用类型
                {
                    m.MANAGERSTATE = "2";
                }
                else if (arr[5].Trim() == "未维护")
                {
                    m.MANAGERSTATE = "1";
                }
                else
                {
                    m.MANAGERSTATE = "1";
                }
                BaseDT.DC_UTILITY_ISOLATIONSTRIP.Add(m);
                string a = row.GetCell(0).ToString();
                string a1 = row.GetCell(1).ToString();
                string a2 = row.GetCell(2).ToString();

            }

        }
        #endregion

        #region 统计当前用户下的隔离带的记录数量
        /// <summary>
        ///统计当前用户下的隔离带的记录数量
        /// </summary>
        /// <returns></returns>
        public static string getCount()
        {
            var Count = "";
            Count = BaseDT.DC_UTILITY_ISOLATIONSTRIP.getNum(new DC_UTILITY_ISOLATIONSTRIP_SW { BYORGNO = SystemCls.getCurUserOrgNo() });
            return Count;
        }
        #endregion
    }
}
