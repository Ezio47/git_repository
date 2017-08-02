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

namespace ManagerSystemClassLibrary
{

    /// <summary>
    /// 数据中心_队伍
    /// </summary>
    public class DC_ARMYCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(DC_ARMY_Model m)
        {
            if (m.opMethod == "Add")
            {
                //SystemCls.LogSave("3", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.DC_ARMY.Add(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
            }
            if (m.opMethod == "Mdy")
            {
                //SystemCls.LogSave("4", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.DC_ARMY.Mdy(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);

            }
            if (m.opMethod == "MdyJWD")
            {
                //SystemCls.LogSave("4", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.DC_ARMY.MdyJWD(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);

            }
            if (m.opMethod == "Del")
            {
                //SystemCls.LogSave("5", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.DC_ARMY.Del(m);
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
        public static DC_ARMY_Model getModel(DC_ARMY_SW sw)
        {
            var result = new List<DC_ARMY_Model>();
            DataTable dt = BaseDT.DC_ARMY.getDT(sw);//列表
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dt26 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "26" });//数据中心队伍类型
            DC_ARMY_Model m = new DC_ARMY_Model();

            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.DC_ARMY_ID = dt.Rows[i]["DC_ARMY_ID"].ToString();
                m.ARMYTYPE = dt.Rows[i]["ARMYTYPE"].ToString();
                m.NUMBER = dt.Rows[i]["NUMBER"].ToString();
                m.NAME = dt.Rows[i]["NAME"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.ARMYMEMBERCOUNT = dt.Rows[i]["ARMYMEMBERCOUNT"].ToString();
                m.ARMYLEADER = dt.Rows[i]["ARMYLEADER"].ToString();
                m.CONTACTS = dt.Rows[i]["CONTACTS"].ToString();
                m.ARMYCHARACTER = dt.Rows[i]["ARMYCHARACTER"].ToString();
                m.ARMYEQUIP = dt.Rows[i]["ARMYEQUIP"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.ORGName = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                m.ARMYTYPEName = BaseDT.T_SYS_DICT.getName(dt26, m.ARMYTYPE);
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            dt26.Clear();
            dt26.Dispose();
            return m;
        }

        #endregion

        #region 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<DC_ARMY_Model> getModelList(DC_ARMY_SW sw)
        {
            var result = new List<DC_ARMY_Model>();
             
            DataTable dt = BaseDT.DC_ARMY.getDT(sw);//列表

            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dt26 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "26" });//数据中心队伍类型
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DC_ARMY_Model m = new DC_ARMY_Model();

                m.DC_ARMY_ID = dt.Rows[i]["DC_ARMY_ID"].ToString();
                m.ARMYTYPE = dt.Rows[i]["ARMYTYPE"].ToString();
                m.NUMBER = dt.Rows[i]["NUMBER"].ToString();
                m.NAME = dt.Rows[i]["NAME"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.ARMYMEMBERCOUNT = dt.Rows[i]["ARMYMEMBERCOUNT"].ToString();
                m.ARMYLEADER = dt.Rows[i]["ARMYLEADER"].ToString();
                m.CONTACTS = dt.Rows[i]["CONTACTS"].ToString();
                m.ARMYCHARACTER = dt.Rows[i]["ARMYCHARACTER"].ToString();
                m.ARMYEQUIP = dt.Rows[i]["ARMYEQUIP"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.ORGName = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                m.ARMYTYPEName = BaseDT.T_SYS_DICT.getName(dt26, m.ARMYTYPE);
                result.Add(m);
            } 
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            dt26.Clear();
            dt26.Dispose();
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
        public static IEnumerable<DC_ARMY_Model> getModelList(DC_ARMY_SW sw, out int total)
        {
            var result = new List<DC_ARMY_Model>();
             
            DataTable dt = BaseDT.DC_ARMY.getDT(sw, out total);//列表

            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dt26 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "26" });//数据中心队伍类型

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DC_ARMY_Model m = new DC_ARMY_Model();

                m.DC_ARMY_ID = dt.Rows[i]["DC_ARMY_ID"].ToString();
                m.ARMYTYPE = dt.Rows[i]["ARMYTYPE"].ToString();
                m.NUMBER = dt.Rows[i]["NUMBER"].ToString();
                m.NAME = dt.Rows[i]["NAME"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.ARMYMEMBERCOUNT = dt.Rows[i]["ARMYMEMBERCOUNT"].ToString();
                m.ARMYLEADER = dt.Rows[i]["ARMYLEADER"].ToString();
                m.CONTACTS = dt.Rows[i]["CONTACTS"].ToString();
                m.ARMYCHARACTER = dt.Rows[i]["ARMYCHARACTER"].ToString();
                m.ARMYEQUIP = dt.Rows[i]["ARMYEQUIP"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                if (m.BYORGNO.Substring(6,9)!="000000000")
                {
                    m.ORGName = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                    
                }
                m.ORGXSName = BaseDT.T_SYS_ORG.getSXName(dtORG, m.BYORGNO);
                m.ARMYTYPEName = BaseDT.T_SYS_DICT.getName(dt26, m.ARMYTYPE);
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            dt26.Clear();
            dt26.Dispose();
            return result;
        }
        #endregion

        #region 队伍上传
        /// <summary>
        /// 护林员上传
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static void ARMYUpload(string filePath)
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
                string[] arr = new string[9];
                for (int k = 0; k < arr.Length; k++)
                {        
                        arr[k] = row.GetCell(k) == null ? "" : row.GetCell(k).ToString();//循环获取每一单元格内容
                }
                DC_ARMY_Model m = new DC_ARMY_Model();
                //单位	队伍类型	名称	编号	人数	队长	联系方式
                if (string.IsNullOrEmpty(arr[0]) || string.IsNullOrEmpty(arr[1]) || string.IsNullOrEmpty(arr[2]))
                {
                    continue;
                }
                m.BYORGNO = BaseDT.T_SYS_ORG.getCodeByName(arr[0]);
                m.NAME = arr[2];
                m.NUMBER = arr[3];
                m.ARMYMEMBERCOUNT = arr[4];
                m.ARMYLEADER = arr[5];
                m.CONTACTS = arr[6];
                string jd = arr[7];
                string wd = arr[8];
                if (string.IsNullOrEmpty(jd) == false && string.IsNullOrEmpty(wd) == false)
                {
                    double[] brr = ClsPositionTrans.GpsTransform(double.Parse(wd), double.Parse(jd), "1");
                    m.JD = brr[1].ToString();
                    m.WD = brr[0].ToString();
                }
                if (arr[1].Trim() == "专业队伍")//性别
                {
                    m.ARMYTYPE = "1";
                }
                else if (arr[1].Trim() == "半专业队伍")
                {
                    m.ARMYTYPE = "2";
                }
                else if (arr[1].Trim() == "应急队伍")
                {
                    m.ARMYTYPE = "3";
                }
                else if (arr[1].Trim() == "群众队伍")
                {
                    m.ARMYTYPE = "4";
                }
                else
                {
                    m.ARMYTYPE = "1";
                } 
                var ms = BaseDT.DC_ARMY.Add(m);
                if (string.IsNullOrEmpty(jd) == false && string.IsNullOrEmpty(wd) == false)
                {
                    Firedepartment_Model m1 = new Firedepartment_Model();
                    m1.OBJECTID = ms.Url;
                    m1.NAME = m.NAME;
                    m1.TYPE = m.ARMYTYPE;
                    m1.JD = jd;
                    m1.WD = wd;
                    m1.Shape = "geometry::STGeomFromText('POINT(" + m1.JD + " " + m1.WD + ")',4326)";
                    BaseDT.Firedepartment.Add(m1);
                }                
                string a = row.GetCell(0).ToString();
                string a1 = row.GetCell(1).ToString();
                string a2 = row.GetCell(2).ToString();


                //DataRow dataRow = table.NewRow();

                //if (row != null)
                //{
                //    for (int j = row.FirstCellNum; j < cellCount; j++)
                //    {
                //        //if (row.GetCell(j) != null)
                //        //dataRow[j] = GetCellValue(row.GetCell(j));
                //    }
                //}

                //table.Rows.Add(dataRow);

            }
            // return table;

        }
        #endregion

        #region 统计当前用户下的队伍数量
        /// <summary>
        ///统计当前用户下的队伍数量
        /// </summary>
        /// <returns></returns>
        public static string getCount()
        {
            var Count = "";
            Count = BaseDT.DC_ARMY.getNum(new DC_ARMY_SW {BYORGNO = SystemCls.getCurUserOrgNo()});
            return Count;
        }
        #endregion
    }
}
