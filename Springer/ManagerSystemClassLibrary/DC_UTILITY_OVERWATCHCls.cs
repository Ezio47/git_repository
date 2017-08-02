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
    /// 数据中心_瞭望台
    /// </summary>
    public class DC_UTILITY_OVERWATCHCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(DC_UTILITY_OVERWATCH_Model m)
        {
            if (m.opMethod == "Add")
            {
                //SystemCls.LogSave("3", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.DC_UTILITY_OVERWATCH.Add(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
            }
            if (m.opMethod == "Mdy")
            {
                //SystemCls.LogSave("4", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.DC_UTILITY_OVERWATCH.Mdy(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);

            }
            if (m.opMethod == "MdyJWD")
            {
                //SystemCls.LogSave("4", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.DC_UTILITY_OVERWATCH.MdyJWD(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);

            }
            if (m.opMethod == "Del")
            {
                //SystemCls.LogSave("5", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.DC_UTILITY_OVERWATCH.Del(m);
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
        public static DC_UTILITY_OVERWATCH_Model getModel(DC_UTILITY_OVERWATCH_SW sw)
        {
            var result = new List<DC_UTILITY_OVERWATCH_Model>();

            DataTable dt = BaseDT.DC_UTILITY_OVERWATCH.getDT(sw);//列表

            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dt34 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "34" });//建筑物结构类型
            DC_UTILITY_OVERWATCH_Model m = new DC_UTILITY_OVERWATCH_Model();

            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.DC_UTILITY_OVERWATCH_ID = dt.Rows[i]["DC_UTILITY_OVERWATCH_ID"].ToString();
                m.NUMBER = dt.Rows[i]["NUMBER"].ToString();
                m.NAME = dt.Rows[i]["NAME"].ToString();
                m.ORGNOS = dt.Rows[i]["ORGNOS"].ToString();
                m.AREA = dt.Rows[i]["AREA"].ToString();
                m.BUILDDATE = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUILDDATE"].ToString());
                m.BUILDDATEBEGIN = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUILDDATEBEGIN"].ToString());
                m.BUILDDATEEND = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUILDDATEEND"].ToString());
                m.FLOOR = dt.Rows[i]["FLOOR"].ToString();
                m.STRUCTURETYPE = dt.Rows[i]["STRUCTURETYPE"].ToString();
                m.STRUCTURETYPEName = BaseDT.T_SYS_DICT.getName(dt34, m.STRUCTURETYPE);
                m.SUBFACILITIES = dt.Rows[i]["SUBFACILITIES"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.WORTH = dt.Rows[i]["WORTH"].ToString();
                m.ORGName = BaseDT.T_SYS_ORG.getNames(dtORG, m.ORGNOS);
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            dt34.Clear();
            dt34.Dispose();
            return m;
        }

        #endregion

        #region 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<DC_UTILITY_OVERWATCH_Model> getModelList(DC_UTILITY_OVERWATCH_SW sw)
        {
            var result = new List<DC_UTILITY_OVERWATCH_Model>();

            DataTable dt = BaseDT.DC_UTILITY_OVERWATCH.getDT(sw);//列表

            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dt34 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "34" });//建筑物结构类型
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DC_UTILITY_OVERWATCH_Model m = new DC_UTILITY_OVERWATCH_Model();
                m.DC_UTILITY_OVERWATCH_ID = dt.Rows[i]["DC_UTILITY_OVERWATCH_ID"].ToString();
                m.NUMBER = dt.Rows[i]["NUMBER"].ToString();
                m.NAME = dt.Rows[i]["NAME"].ToString();
                m.ORGNOS = dt.Rows[i]["ORGNOS"].ToString();
                m.AREA = dt.Rows[i]["AREA"].ToString();
                m.BUILDDATE = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUILDDATE"].ToString());
                m.BUILDDATEBEGIN = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUILDDATEBEGIN"].ToString());
                m.BUILDDATEEND = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUILDDATEEND"].ToString());
                m.FLOOR = dt.Rows[i]["FLOOR"].ToString();
                m.STRUCTURETYPE = dt.Rows[i]["STRUCTURETYPE"].ToString();
                m.STRUCTURETYPEName = BaseDT.T_SYS_DICT.getName(dt34, m.STRUCTURETYPE);
                m.SUBFACILITIES = dt.Rows[i]["SUBFACILITIES"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.WORTH = dt.Rows[i]["WORTH"].ToString();
                m.ORGName = BaseDT.T_SYS_ORG.getNames(dtORG, m.ORGNOS);
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            dt34.Clear();
            dt34.Dispose();
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
        public static IEnumerable<DC_UTILITY_OVERWATCH_Model> getModelList(DC_UTILITY_OVERWATCH_SW sw, out int total)
        {
            var result = new List<DC_UTILITY_OVERWATCH_Model>();

            DataTable dt = BaseDT.DC_UTILITY_OVERWATCH.getDT(sw, out total);//列表

            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dt34 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "34" });//建筑物结构类型

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DC_UTILITY_OVERWATCH_Model m = new DC_UTILITY_OVERWATCH_Model();
                m.DC_UTILITY_OVERWATCH_ID = dt.Rows[i]["DC_UTILITY_OVERWATCH_ID"].ToString();
                m.NUMBER = dt.Rows[i]["NUMBER"].ToString();
                m.NAME = dt.Rows[i]["NAME"].ToString();
                m.ORGNOS = dt.Rows[i]["ORGNOS"].ToString();
                m.AREA = dt.Rows[i]["AREA"].ToString();
                m.BUILDDATE = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUILDDATE"].ToString());
                m.BUILDDATEBEGIN = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUILDDATEBEGIN"].ToString());
                m.BUILDDATEEND = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUILDDATEEND"].ToString());
                m.FLOOR = dt.Rows[i]["FLOOR"].ToString();
                m.STRUCTURETYPE = dt.Rows[i]["STRUCTURETYPE"].ToString();
                m.STRUCTURETYPEName = BaseDT.T_SYS_DICT.getName(dt34, m.STRUCTURETYPE);
                m.SUBFACILITIES = dt.Rows[i]["SUBFACILITIES"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.WORTH = dt.Rows[i]["WORTH"].ToString();
                if (m.ORGNOS.Substring(6, 3) != "000" && m.ORGNOS.Substring(9, 6) == "000000")
                {
                    m.ORGName = BaseDT.T_SYS_ORG.getName(dtORG, m.ORGNOS);

                }
                m.ORGXSName = BaseDT.T_SYS_ORG.getSXName(dtORG, m.ORGNOS);
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            dt34.Clear();
            dt34.Dispose();
            return result;
        }
        #endregion

        #region 瞭望台上传
        /// <summary>
        /// 瞭望台上传
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static void OVERWATCHUpload(string filePath)
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
                string[] arr = new string[11];
                for (int k = 0; k < arr.Length; k++)
                {
                    if (k != 6)
                        arr[k] = row.GetCell(k) == null ? "" : row.GetCell(k).ToString();//循环获取每一单元格内容
                    else
                        arr[k] = row.GetCell(k).DateCellValue.ToString("yyyy-MM-dd");
                }
                DC_UTILITY_OVERWATCH_Model m = new DC_UTILITY_OVERWATCH_Model();
                //单位	结构类型	名称	编号	建筑面积	楼层	建设日期 附属设施  价值 经度 纬度
                if (string.IsNullOrEmpty(arr[0]) || string.IsNullOrEmpty(arr[2]))
                {
                    continue;
                }
                m.ORGNOS = BaseDT.T_SYS_ORG.getCodeByName(arr[0]);
                m.NAME = arr[2];
                m.NUMBER = arr[3];
                m.AREA = arr[4];
                m.FLOOR = arr[5];
                m.BUILDDATE = arr[6];
                if (m.BUILDDATE == "9999-12-31")
                    m.BUILDDATE = "1900-01-01";
                m.SUBFACILITIES = arr[7];
                m.WORTH = arr[8];
                string jd = arr[9];
                string wd = arr[10];
                if (string.IsNullOrEmpty(jd) == false && string.IsNullOrEmpty(wd) == false)
                {
                    double[] brr = ClsPositionTrans.GpsTransform(double.Parse(wd), double.Parse(jd), "1");
                    m.JD = brr[1].ToString();
                    m.WD = brr[0].ToString();
                }
                if (arr[1].Trim() == "钢构")//结构类型
                {
                    m.STRUCTURETYPE = "1";
                }
                else if (arr[1].Trim() == "砖混")
                {
                    m.STRUCTURETYPE = "2";
                }
                else if (arr[1].Trim() == "钢混")
                {
                    m.STRUCTURETYPE = "3";
                }  
                else
                {
                    m.STRUCTURETYPE = "1";
                }
                var ms = BaseDT.DC_UTILITY_OVERWATCH.Add(m);
                if (string.IsNullOrEmpty(jd) == false && string.IsNullOrEmpty(wd) == false)
                {
                    TD_OVERWATCH_Model m1 = new TD_OVERWATCH_Model();
                    m1.OBJECTID = ms.Url;
                    m1.NAME = m.NAME;
                    m1.TYPE = m.STRUCTURETYPE;
                    m1.JD = jd;
                    m1.WD = wd;
                    m1.Shape = "geometry::STGeomFromText('POINT(" + m1.JD + " " + m1.WD + ")',4326)";
                    BaseDT.SDE.TD_OVERWATCH.Add(m1);
                }
                string a = row.GetCell(0).ToString();
                string a1 = row.GetCell(1).ToString();
                string a2 = row.GetCell(2).ToString();

            }

        }
        #endregion

        #region 统计当前用户下的瞭望台的记录数量
        /// <summary>
        ///统计当前用户下的瞭望台的记录数量
        /// </summary>
        /// <returns></returns>
        public static string getCount()
        {
            var Count = "";
            Count = BaseDT.DC_UTILITY_OVERWATCH.getNum(new DC_UTILITY_OVERWATCH_SW { ORGNOS = SystemCls.getCurUserOrgNo() });
            return Count;
        }
        #endregion
    }
}
