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
    /// 数据中心_资源_新
    /// </summary>
   public  class DC_RESOURCE_NEWCls
   {
       #region 增、删、改
       /// <summary>
       /// 增、删、改
       /// </summary>
       /// <param name="m">参见模型</param>
       /// <returns>参见模型</returns>
       public static Message Manager(DC_RESOURCE_NEW_Model m)
       {
           if (m.opMethod == "Add")
           {
               //SystemCls.LogSave("3", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
               Message msgUser = BaseDT.DC_RESOURCE_NEW.Add(m);
               return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
           }
           if (m.opMethod == "Mdy")
           {
               //SystemCls.LogSave("4", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
               Message msgUser = BaseDT.DC_RESOURCE_NEW.Mdy(m);
               return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);

           }
           if (m.opMethod == "MdyJWD")
           {
               //SystemCls.LogSave("4", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
               Message msgUser = BaseDT.DC_RESOURCE_NEW.MdyJWD(m);
               return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);

           }
           if (m.opMethod == "Del")
           {
               //SystemCls.LogSave("5", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
               Message msgUser = BaseDT.DC_RESOURCE_NEW.Del(m);
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
       public static DC_RESOURCE_NEW_Model getModel(DC_RESOURCE_NEW_SW sw)
       {
           var result = new List<DC_RESOURCE_NEW_Model>();

           DataTable dt = BaseDT.DC_RESOURCE_NEW.getDT(sw);//列表
           DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
           DataTable dt27 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "27" });//林龄类型
           DataTable dt28 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "28" });//数据中心资源类型
           DataTable dt29 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "29" });//数据中心资源起源类型
           DataTable dt30 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "30" });//数据中心资源可燃类型
           DataTable dt31 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "31" });//数据中心资源林木类型
           DC_RESOURCE_NEW_Model m = new DC_RESOURCE_NEW_Model();

           if (dt.Rows.Count > 0)
           {
               int i = 0;
               m.DC_RESOURCE_NEW_ID = dt.Rows[i]["DC_RESOURCE_NEW_ID"].ToString();
               m.RESOURCETYPE = dt.Rows[i]["RESOURCETYPE"].ToString();
               m.RESOURCETYPEName = BaseDT.T_SYS_DICT.getName(dt28, m.RESOURCETYPE);
               m.NUMBER = dt.Rows[i]["NUMBER"].ToString();
               m.NAME = dt.Rows[i]["NAME"].ToString();
               m.ORGNOS = dt.Rows[i]["ORGNOS"].ToString();
               m.ORGNOSName = BaseDT.T_SYS_ORG.getNames(dtORG, m.ORGNOS);
               m.KINDTYPE = dt.Rows[i]["KINDTYPE"].ToString();
               m.AGETYPE = dt.Rows[i]["AGETYPE"].ToString();
               m.AGETYPEName = BaseDT.T_SYS_DICT.getName(dt27, m.AGETYPE);
               m.ORIGINTYPE = dt.Rows[i]["ORIGINTYPE"].ToString();
               m.ORIGINTYPEName = BaseDT.T_SYS_DICT.getName(dt29, m.AGETYPE);
               m.AREA = dt.Rows[i]["AREA"].ToString();
               m.BURNTYPE = dt.Rows[i]["BURNTYPE"].ToString();
               m.BURNTYPEName = BaseDT.T_SYS_DICT.getName(dt30, m.BURNTYPE);
               m.TREETYPE = dt.Rows[i]["TREETYPE"].ToString();
               m.TREETYPEName = BaseDT.T_SYS_DICT.getName(dt31, m.TREETYPE);
               m.ASPECT = dt.Rows[i]["ASPECT"].ToString();
               m.ANGLE = dt.Rows[i]["ANGLE"].ToString();
               m.MARK = dt.Rows[i]["MARK"].ToString();
               m.JD = dt.Rows[i]["JD"].ToString();
               m.WD = dt.Rows[i]["WD"].ToString();
               m.POTHOOKLEADER = dt.Rows[i]["POTHOOKLEADER"].ToString();
               m.POTHOOKLEADERJOB = dt.Rows[i]["POTHOOKLEADERJOB"].ToString();
               m.POTHOOKLEADERTLEE = dt.Rows[i]["POTHOOKLEADERTLEE"].ToString();
               m.DUTYPERSON = dt.Rows[i]["DUTYPERSON"].ToString();
               m.DUTYPERSONTELL = dt.Rows[i]["DUTYPERSONTELL"].ToString();
               m.JWDLIST = dt.Rows[i]["JWDLIST"].ToString();
           }
           dt.Clear();
           dt.Dispose();
           dt27.Clear();
           dt27.Dispose();
           dt28.Clear();
           dt28.Dispose();
           dt29.Clear();
           dt29.Dispose();
           dt30.Clear();
           dt30.Dispose();
           dt31.Clear();
           dt31.Dispose();
           dtORG.Clear();
           dtORG.Dispose();
           return m;
       }

       #endregion

       #region 获取列表
       /// <summary>
       /// 获取列表
       /// </summary>
       /// <param name="sw"></param>
       /// <returns></returns>
       public static IEnumerable<DC_RESOURCE_NEW_Model> getModelList(DC_RESOURCE_NEW_SW sw)
       {
           var result = new List<DC_RESOURCE_NEW_Model>();

           DataTable dt = BaseDT.DC_RESOURCE_NEW.getDT(sw);//列表

           DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
           DataTable dt27 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "27" });//林龄类型
           DataTable dt28 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "28" });//数据中心资源类型
           DataTable dt29 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "29" });//数据中心资源起源类型
           DataTable dt30 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "30" });//数据中心资源可燃类型
           DataTable dt31 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "31" });//数据中心资源林木类型
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               DC_RESOURCE_NEW_Model m = new DC_RESOURCE_NEW_Model();

               m.DC_RESOURCE_NEW_ID = dt.Rows[i]["DC_RESOURCE_NEW_ID"].ToString();
               m.RESOURCETYPE = dt.Rows[i]["RESOURCETYPE"].ToString();
               m.RESOURCETYPEName = BaseDT.T_SYS_DICT.getName(dt28, m.RESOURCETYPE);
               m.NUMBER = dt.Rows[i]["NUMBER"].ToString();
               m.NAME = dt.Rows[i]["NAME"].ToString();
               m.ORGNOS = dt.Rows[i]["ORGNOS"].ToString();
               m.ORGNOSName = BaseDT.T_SYS_ORG.getNames(dtORG, m.ORGNOS);
               m.KINDTYPE = dt.Rows[i]["KINDTYPE"].ToString();
               m.AGETYPE = dt.Rows[i]["AGETYPE"].ToString();
               m.AGETYPEName = BaseDT.T_SYS_DICT.getName(dt27, m.AGETYPE);
               m.ORIGINTYPE = dt.Rows[i]["ORIGINTYPE"].ToString();
               m.ORIGINTYPEName = BaseDT.T_SYS_DICT.getName(dt29, m.ORIGINTYPE);
               m.AREA = dt.Rows[i]["AREA"].ToString();
               m.BURNTYPE = dt.Rows[i]["BURNTYPE"].ToString();
               m.BURNTYPEName = BaseDT.T_SYS_DICT.getName(dt30, m.BURNTYPE);
               m.TREETYPE = dt.Rows[i]["TREETYPE"].ToString();
               m.TREETYPEName = BaseDT.T_SYS_DICT.getName(dt31, m.TREETYPE);
               m.ASPECT = dt.Rows[i]["ASPECT"].ToString();
               m.ANGLE = dt.Rows[i]["ANGLE"].ToString();
               m.MARK = dt.Rows[i]["MARK"].ToString();
               m.JD = dt.Rows[i]["JD"].ToString();
               m.WD = dt.Rows[i]["WD"].ToString();
               m.POTHOOKLEADER = dt.Rows[i]["POTHOOKLEADER"].ToString();
               m.POTHOOKLEADERJOB = dt.Rows[i]["POTHOOKLEADERJOB"].ToString();
               m.POTHOOKLEADERTLEE = dt.Rows[i]["POTHOOKLEADERTLEE"].ToString();
               m.DUTYPERSON = dt.Rows[i]["DUTYPERSON"].ToString();
               m.DUTYPERSONTELL = dt.Rows[i]["DUTYPERSONTELL"].ToString();
               m.JWDLIST = dt.Rows[i]["JWDLIST"].ToString();
               result.Add(m);
           }
           dt.Clear();
           dt.Dispose();
           dt27.Clear();
           dt27.Dispose();
           dt28.Clear();
           dt28.Dispose();
           dt29.Clear();
           dt29.Dispose();
           dt30.Clear();
           dt30.Dispose();
           dt31.Clear();
           dt31.Dispose();
           dtORG.Clear();
           dtORG.Dispose();
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
       public static IEnumerable<DC_RESOURCE_NEW_Model> getModelList(DC_RESOURCE_NEW_SW sw, out int total)
       {
           var result = new List<DC_RESOURCE_NEW_Model>();

           DataTable dt = BaseDT.DC_RESOURCE_NEW.getDT(sw, out total);//列表

           DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
           DataTable dt27 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "27" });//林龄类型
           DataTable dt28 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "28" });//数据中心资源类型
           DataTable dt29 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "29" });//数据中心资源起源类型
           DataTable dt30 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "30" });//数据中心资源可燃类型
           DataTable dt31 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "31" });//数据中心资源林木类型
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               DC_RESOURCE_NEW_Model m = new DC_RESOURCE_NEW_Model();

               m.DC_RESOURCE_NEW_ID = dt.Rows[i]["DC_RESOURCE_NEW_ID"].ToString();
               m.RESOURCETYPE = dt.Rows[i]["RESOURCETYPE"].ToString();
               m.RESOURCETYPEName = BaseDT.T_SYS_DICT.getName(dt28, m.RESOURCETYPE);
               m.NUMBER = dt.Rows[i]["NUMBER"].ToString();
               m.NAME = dt.Rows[i]["NAME"].ToString();
               m.ORGNOS = dt.Rows[i]["ORGNOS"].ToString();
               //m.ORGNOSName = BaseDT.T_SYS_ORG.getNames(dtORG, m.ORGNOS);
               if (m.ORGNOS.Substring(6, 3) != "000" && m.ORGNOS.Substring(9, 6) == "000000")
               {
                   m.ORGNOSName = BaseDT.T_SYS_ORG.getName(dtORG, m.ORGNOS);

               }
               m.ORGXSName = BaseDT.T_SYS_ORG.getSXName(dtORG, m.ORGNOS);
               m.KINDTYPE = dt.Rows[i]["KINDTYPE"].ToString();
               m.AGETYPE = dt.Rows[i]["AGETYPE"].ToString();
               m.AGETYPEName = BaseDT.T_SYS_DICT.getName(dt27, m.AGETYPE);
               m.ORIGINTYPE = dt.Rows[i]["ORIGINTYPE"].ToString();
               m.ORIGINTYPEName = BaseDT.T_SYS_DICT.getName(dt29, m.ORIGINTYPE);
               m.AREA = dt.Rows[i]["AREA"].ToString();
               m.BURNTYPE = dt.Rows[i]["BURNTYPE"].ToString();
               m.BURNTYPEName = BaseDT.T_SYS_DICT.getName(dt30, m.BURNTYPE);
               m.TREETYPE = dt.Rows[i]["TREETYPE"].ToString();
               m.TREETYPEName = BaseDT.T_SYS_DICT.getName(dt31, m.TREETYPE);
               m.ASPECT = dt.Rows[i]["ASPECT"].ToString();
               m.ANGLE = dt.Rows[i]["ANGLE"].ToString();
               m.MARK = dt.Rows[i]["MARK"].ToString();
               m.JD = dt.Rows[i]["JD"].ToString();
               m.WD = dt.Rows[i]["WD"].ToString();
               m.POTHOOKLEADER = dt.Rows[i]["POTHOOKLEADER"].ToString();
               m.POTHOOKLEADERJOB = dt.Rows[i]["POTHOOKLEADERJOB"].ToString();
               m.POTHOOKLEADERTLEE = dt.Rows[i]["POTHOOKLEADERTLEE"].ToString();
               m.DUTYPERSON = dt.Rows[i]["DUTYPERSON"].ToString();
               m.DUTYPERSONTELL = dt.Rows[i]["DUTYPERSONTELL"].ToString();
               m.JWDLIST = dt.Rows[i]["JWDLIST"].ToString();
               result.Add(m);
           }
           dt.Clear();
           dt.Dispose();
           dt27.Clear();
           dt27.Dispose();
           dt28.Clear();
           dt28.Dispose();
           dt29.Clear();
           dt29.Dispose();
           dt30.Clear();
           dt30.Dispose();
           dt31.Clear();
           dt31.Dispose();
           dtORG.Clear();
           dtORG.Dispose();
           return result;
       }
       #endregion

       #region 资源上传
       /// <summary>
       /// 护林员上传
       /// </summary>
       /// <param name="filePath">文件路径</param>
       public static void RESOURCEUpload(string filePath)
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
               string[] arr = new string[17];
               for (int k = 0; k < arr.Length; k++)
               {
                   arr[k] = row.GetCell(k) == null ? "" : row.GetCell(k).ToString();//循环获取每一单元格内容
               }
               DC_RESOURCE_NEW_Model m = new DC_RESOURCE_NEW_Model();
               //单位 资源类型 名称 编号 林龄类型 起源类型 可燃类型 林木类型 树种 面积 坡向 坡度 挂钩领导 职务 领导电话 责任人 责任人电话
               if (string.IsNullOrEmpty(arr[0]) || string.IsNullOrEmpty(arr[2]))
               {
                   continue;
               }
               m.ORGNOS = BaseDT.T_SYS_ORG.getCodeByName(arr[0]);
               m.NAME = arr[2];
               m.NUMBER = arr[3];
               m.KINDTYPE = arr[8];
               m.AREA = arr[9];
               m.ASPECT = arr[10];
               m.ANGLE = arr[11];
               m.POTHOOKLEADER = arr[12];
               m.POTHOOKLEADERJOB = arr[13];
               m.POTHOOKLEADERTLEE = arr[14];
               m.DUTYPERSON = arr[15];
               m.DUTYPERSONTELL = arr[16];
               if (arr[1].Trim() == "重点林区")//资源类型
               {
                   m.RESOURCETYPE = "1";
               }
               else if (arr[1].Trim() == "有林地")
               {
                   m.RESOURCETYPE = "2";
               }
               else if (arr[1].Trim() == "荒山")
               {
                   m.RESOURCETYPE = "3";
               }
               else if (arr[1].Trim() == "灌木丛")
               {
                   m.RESOURCETYPE = "4";
               }
               else
               {
                   m.RESOURCETYPE = "1";
               }
               if (arr[4].Trim() == "幼龄林")//林龄类型
               {
                   m.AGETYPE = "1";
               }
               else if (arr[4].Trim() == "中龄林")
               {
                   m.AGETYPE = "2";
               }
               else if (arr[4].Trim() == "近熟林")
               {
                   m.AGETYPE = "3";
               }
               else if (arr[4].Trim() == "成熟林")
               {
                   m.AGETYPE = "4";
               }
               else if (arr[4] == "过熟林")
               {
                   m.AGETYPE = "5";
               }
               else
               {
                   m.AGETYPE = "1";
               }
               if (arr[5].Trim() == "天然")//起源类型
               {
                   m.ORIGINTYPE = "1";
               }
               else if (arr[5].Trim() == "人工")
               {
                   m.ORIGINTYPE = "2";
               }
               else
               {
                   m.ORIGINTYPE = "1";
               }
               if (arr[6].Trim() == "易燃")//可燃类型
               {
                   m.BURNTYPE = "1";
               }
               else if (arr[6].Trim() == "不易燃")
               {
                   m.BURNTYPE = "2";
               }
               else
               {
                   m.BURNTYPE = "1";
               }
               if (arr[7].Trim() == "针叶林")//林木类型
               {
                   m.TREETYPE = "1";
               }
               else if (arr[7].Trim() == "阔叶林")
               {
                   m.TREETYPE = "2";
               }
               else if (arr[7].Trim() == "混交林")
               {
                   m.TREETYPE = "3";
               }
               else
               {
                   m.TREETYPE = "1";
               }
               BaseDT.DC_RESOURCE_NEW.Add(m);
               string a = row.GetCell(0).ToString();
               string a1 = row.GetCell(1).ToString();
               string a2 = row.GetCell(2).ToString();
           }

       }
       #endregion

       #region 统计当前用户下的资源的记录数量
       /// <summary>
       ///统计当前用户下的资源的记录数量
       /// </summary>
       /// <returns></returns>
       public static string getCount()
       {
           var Count = "";
           Count = BaseDT.DC_RESOURCE_NEW.getNum(new DC_RESOURCE_NEW_SW { ORGNOS = SystemCls.getCurUserOrgNo() });
           return Count;
       }
       #endregion
    }
}
