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
    /// 预警监测-红外相机
    /// </summary>
    public class JC_INFRAREDCAMERACls
    {
        #region 相机基本信息增、删、改

        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(JC_INFRAREDCAMERA_BASICINFO_Model m)
        {
            if (m.opMethod == "Add")
            {
                SystemCls.LogSave("3", "红外相机:" + m.INFRAREDCAMERANAME, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.JC_INFRAREDCAMERA_BASICINFO.Add(m);
                if (msgUser.Success == true)
                {
                    Message msgUser1 = BaseDT.JC_INFRAREDCAMERA_BASICINFO.AddHONGWAIXIANGJI(m, msgUser.Url);//添加三维库
                    return new Message(msgUser1.Success, msgUser1.Msg, m.returnUrl);
                }
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            if (m.opMethod == "Mdy")
            {
                SystemCls.LogSave("4", "红外相机:" + m.INFRAREDCAMERANAME, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.JC_INFRAREDCAMERA_BASICINFO.Mdy(m);
                if (msgUser.Success == true)
                {
                    Message msgUser1 = BaseDT.JC_INFRAREDCAMERA_BASICINFO.MdyHONGWAIXIANGJI(m);//添加三维库
                    return new Message(msgUser1.Success, msgUser1.Msg, m.returnUrl);
                }
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);

            }
            if (m.opMethod == "Del")
            {
                SystemCls.LogSave("5", "红外相机:" + m.INFRAREDCAMERANAME, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.JC_INFRAREDCAMERA_BASICINFO.Del(m);
                if (msgUser.Success == true)
                {
                    Message msgUser1 = BaseDT.JC_INFRAREDCAMERA_BASICINFO.DelHONGWAIXIANGJI(m);//添加三维库
                    return new Message(msgUser1.Success, msgUser1.Msg, m.returnUrl);
                }
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            return new Message(false, "无效操作", "");


        }
        #endregion

        #region  根据查询条件获取某一条相机信息记录

        /// <summary>
        /// 根据查询条件获取某一条信息记录
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static JC_INFRAREDCAMERA_BASICINFO_Model getModel(JC_INFRAREDCAMERA_BASICINFO_SW sw)
        {
            DataTable dt = BaseDT.JC_INFRAREDCAMERA_BASICINFO.getDT(sw);
            JC_INFRAREDCAMERA_BASICINFO_Model m = new JC_INFRAREDCAMERA_BASICINFO_Model();
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.INFRAREDCAMERAID = dt.Rows[i]["INFRAREDCAMERAID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.PHONE = dt.Rows[i]["PHONE"].ToString();
                m.INFRAREDCAMERANAME = dt.Rows[i]["INFRAREDCAMERANAME"].ToString();
                m.PHONE = dt.Rows[i]["PHONE"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.GC = dt.Rows[i]["GC"].ToString();
                m.ADDRESS = dt.Rows[i]["ADDRESS"].ToString();
                m.PHONE = dt.Rows[i]["PHONE"].ToString();
                m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtORG, dt.Rows[i]["BYORGNO"].ToString());
            }

            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            return m;
        }
        #endregion

        #region 获取相机列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>

        public static IEnumerable<JC_INFRAREDCAMERA_BASICINFO_Model> getListModel(JC_INFRAREDCAMERA_BASICINFO_SW sw)
        {
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dt = BaseDT.JC_INFRAREDCAMERA_BASICINFO.getDT(sw);//列表
            var result = new List<JC_INFRAREDCAMERA_BASICINFO_Model>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                JC_INFRAREDCAMERA_BASICINFO_Model m = new JC_INFRAREDCAMERA_BASICINFO_Model();
                m.INFRAREDCAMERAID = dt.Rows[i]["INFRAREDCAMERAID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.PHONE = dt.Rows[i]["PHONE"].ToString();
                m.INFRAREDCAMERANAME = dt.Rows[i]["INFRAREDCAMERANAME"].ToString();
                m.PHONE = dt.Rows[i]["PHONE"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.GC = dt.Rows[i]["GC"].ToString();
                m.ADDRESS = dt.Rows[i]["ADDRESS"].ToString();
                m.PHONE = dt.Rows[i]["PHONE"].ToString();
                m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtORG, dt.Rows[i]["BYORGNO"].ToString());
                result.Add(m);
            }
            dtORG.Clear();
            dtORG.Dispose();
            dt.Clear();
            dt.Dispose();
            return result;
        }

        #endregion

        #region 获取相机列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>

        public static IEnumerable<JC_INFRAREDCAMERA_BASICINFO_Model> getListModel(JC_INFRAREDCAMERA_BASICINFO_SW sw, out int total)
        {
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dt = BaseDT.JC_INFRAREDCAMERA_BASICINFO.getDT(sw, out total);//列表
            var result = new List<JC_INFRAREDCAMERA_BASICINFO_Model>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                JC_INFRAREDCAMERA_BASICINFO_Model m = new JC_INFRAREDCAMERA_BASICINFO_Model();
                m.INFRAREDCAMERAID = dt.Rows[i]["INFRAREDCAMERAID"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.PHONE = dt.Rows[i]["PHONE"].ToString();
                m.INFRAREDCAMERANAME = dt.Rows[i]["INFRAREDCAMERANAME"].ToString();
                m.PHONE = dt.Rows[i]["PHONE"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.GC = dt.Rows[i]["GC"].ToString();
                m.ADDRESS = dt.Rows[i]["ADDRESS"].ToString();
                m.PHONE = dt.Rows[i]["PHONE"].ToString();
                m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtORG, dt.Rows[i]["BYORGNO"].ToString());
                result.Add(m);
            }
            dtORG.Clear();
            dtORG.Dispose();
            dt.Clear();
            dt.Dispose();
            return result;
        }

        #endregion

        #region 彩信图片增、删、改、管理

        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message ManagerPhoto(JC_INFRAREDCAMERA_PHOTO_Model m)
        {

            if (m.opMethod == "Del")
            {
                SystemCls.LogSave("5", "红外相机图片:" + m.smid, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.JC_INFRAREDCAMERA_PHOTO.Del(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }

            if (m.opMethod == "Man")
            {
                SystemCls.LogSave("5", "红外相机图片:" + m.smid, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.JC_INFRAREDCAMERA_PHOTO.Man(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            return new Message(false, "无效操作", "");

        }
        #endregion

        #region  根据查询条件获取某一条图片信息记录

        /// <summary>
        /// 根据查询条件获取某一条信息记录
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static JC_INFRAREDCAMERA_PHOTO_Model getModelPhoto(JC_INFRAREDCAMERA_PHOTO_SW sw)
        {
            DataTable dt = BaseDT.JC_INFRAREDCAMERA_PHOTO.getDT(sw);
            JC_INFRAREDCAMERA_PHOTO_Model m = new JC_INFRAREDCAMERA_PHOTO_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.smid = dt.Rows[i]["smid"].ToString();
                m.tpa = dt.Rows[i]["tpa"].ToString();
                m.recvdatetime = ClsSwitch.SwitTM(dt.Rows[i]["recvdatetime"].ToString());
                m.filename = dt.Rows[i]["filename"].ToString();
                m.MANSTATE = dt.Rows[i]["MANSTATE"].ToString();
                m.MANRESULT = dt.Rows[i]["MANRESULT"].ToString();
                m.MANTIME = ClsSwitch.SwitTM(dt.Rows[i]["MANTIME"].ToString());
                m.MANUSERID = dt.Rows[i]["MANUSERID"].ToString();
                if (!string.IsNullOrEmpty(m.MANUSERID))
                {
                    DataTable dtUser = BaseDT.T_SYSSEC_USER.getDT(new T_SYSSEC_IPSUSER_SW { USERID = m.MANUSERID });
                    DataRow[] drUser = dtUser.Select("USERID='" + m.MANUSERID + "'");
                    if (drUser.Length > 0)
                    {
                        m.ManUserName = drUser[0]["USERNAME"].ToString();
                    }
                    dtUser.Clear();
                    dtUser.Dispose();

                }
                m.BasicInfoModel = getModel(new JC_INFRAREDCAMERA_BASICINFO_SW { PHONE = m.tpa });
            }
            dt.Clear();
            dt.Dispose();
            return m;
        }
        #endregion

        #region  根据查询条件获取某一条图片信息记录

        /// <summary>
        /// 根据查询条件获取某一条信息记录
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static JC_INFRAREDCAMERA_PHOTO_Model getModelNewPhoto(JC_INFRAREDCAMERA_PHOTO_SW sw)
        {
            DataTable dt = BaseDT.JC_INFRAREDCAMERA_PHOTO.getNewDT(sw);
            JC_INFRAREDCAMERA_PHOTO_Model m = new JC_INFRAREDCAMERA_PHOTO_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.JC_INFRAREDCAMERA_PHOTOID = dt.Rows[i]["JC_INFRAREDCAMERA_PHOTOID"].ToString();
                m.INFRAREDCAMERAID = dt.Rows[i]["INFRAREDCAMERAID"].ToString();
                m.PHOTOTIME = dt.Rows[i]["PHOTOTIME"].ToString();
                m.PHOTOTITLE = dt.Rows[i]["PHOTOTITLE"].ToString();
            }
            dt.Clear();
            dt.Dispose();
            return m;
        }
        #endregion

        #region 获取图片列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>

        public static IEnumerable<JC_INFRAREDCAMERA_PHOTO_Model> getListNewModelPhoto(JC_INFRAREDCAMERA_PHOTO_SW sw)
        {
            DataTable dt = BaseDT.JC_INFRAREDCAMERA_PHOTO.getNewDT(sw);//列表
            var result = new List<JC_INFRAREDCAMERA_PHOTO_Model>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                JC_INFRAREDCAMERA_PHOTO_Model m = new JC_INFRAREDCAMERA_PHOTO_Model();
                m.JC_INFRAREDCAMERA_PHOTOID = dt.Rows[i]["JC_INFRAREDCAMERA_PHOTOID"].ToString();
                m.INFRAREDCAMERAID = dt.Rows[i]["INFRAREDCAMERAID"].ToString();
                m.PHOTOTIME = dt.Rows[i]["PHOTOTIME"].ToString();
                m.PHOTOTITLE = dt.Rows[i]["PHOTOTITLE"].ToString();
                result.Add(m);
            }

            dt.Clear();
            dt.Dispose();
            return result;
        }

        #endregion

        #region 获取图片列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>

        public static IEnumerable<JC_INFRAREDCAMERA_PHOTO_Model> getListModelPhoto(JC_INFRAREDCAMERA_PHOTO_SW sw)
        {
            DataTable dt = BaseDT.JC_INFRAREDCAMERA_PHOTO.getDT(sw);//列表
            var result = new List<JC_INFRAREDCAMERA_PHOTO_Model>();

            DataTable dtUser = BaseDT.T_SYSSEC_USER.getDT(new T_SYSSEC_IPSUSER_SW { });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                JC_INFRAREDCAMERA_PHOTO_Model m = new JC_INFRAREDCAMERA_PHOTO_Model();
                m.smid = dt.Rows[i]["smid"].ToString();
                m.tpa = dt.Rows[i]["tpa"].ToString();
                m.recvdatetime = ClsSwitch.SwitTM(dt.Rows[i]["recvdatetime"].ToString());
                m.filename = dt.Rows[i]["filename"].ToString();
                m.MANSTATE = dt.Rows[i]["MANSTATE"].ToString();
                m.MANRESULT = dt.Rows[i]["MANRESULT"].ToString();
                m.MANTIME = ClsSwitch.SwitTM(dt.Rows[i]["MANTIME"].ToString());
                m.MANUSERID = dt.Rows[i]["MANUSERID"].ToString();
                if (!string.IsNullOrEmpty(m.MANUSERID))
                {
                    DataRow[] drUser = dtUser.Select("USERID='" + m.MANUSERID + "'");
                    if (drUser.Length > 0)
                    {
                        m.ManUserName = drUser[0]["USERNAME"].ToString();
                    }

                }
                m.BasicInfoModel = getModel(new JC_INFRAREDCAMERA_BASICINFO_SW { PHONE = m.tpa });
                result.Add(m);
            }
            dtUser.Clear();
            dtUser.Dispose();
            dt.Clear();
            dt.Dispose();
            return result;
        }

        #endregion

        #region 获取最新图片列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw">参见模型 sw.TopCount 默认为10 sw.MANSTATE 默认取所有</param>
        /// <returns>参见模型</returns>

        public static IEnumerable<JC_INFRAREDCAMERA_PHOTO_Model> getListModelTopPhoto(JC_INFRAREDCAMERA_PHOTO_SW sw)
        {
            DataTable dt = BaseDT.JC_INFRAREDCAMERA_PHOTO.getTopDT(sw);//列表
            var result = new List<JC_INFRAREDCAMERA_PHOTO_Model>();

            DataTable dtUser = BaseDT.T_SYSSEC_USER.getDT(new T_SYSSEC_IPSUSER_SW { });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                JC_INFRAREDCAMERA_PHOTO_Model m = new JC_INFRAREDCAMERA_PHOTO_Model();
                m.smid = dt.Rows[i]["smid"].ToString();
                m.tpa = dt.Rows[i]["tpa"].ToString();
                m.recvdatetime = ClsSwitch.SwitTM(dt.Rows[i]["recvdatetime"].ToString());
                m.filename = dt.Rows[i]["filename"].ToString();
                m.MANSTATE = dt.Rows[i]["MANSTATE"].ToString();
                m.MANRESULT = dt.Rows[i]["MANRESULT"].ToString();
                m.MANTIME = ClsSwitch.SwitTM(dt.Rows[i]["MANTIME"].ToString());
                m.MANUSERID = dt.Rows[i]["MANUSERID"].ToString();
                if (!string.IsNullOrEmpty(m.MANUSERID))
                {
                    DataRow[] drUser = dtUser.Select("USERID='" + m.MANUSERID + "'");
                    if (drUser.Length > 0)
                    {
                        m.ManUserName = drUser[0]["USERNAME"].ToString();
                    }
                }
                m.BasicInfoModel = getModel(new JC_INFRAREDCAMERA_BASICINFO_SW { PHONE = m.tpa });
                result.Add(m);
            }
            dtUser.Clear();
            dtUser.Dispose();
            dt.Clear();
            dt.Dispose();
            return result;
        }

        #endregion

        #region 获取列表（分页）
        /// <summary>
        /// 获取用户列表（分页）
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <param name="total">记录总数</param>
        /// <returns>参见模型</returns>
        public static IEnumerable<JC_INFRAREDCAMERA_PHOTO_Model> getListModelPhotoPager(JC_INFRAREDCAMERA_PHOTO_SW sw, out int total)
        {
            var result = new List<JC_INFRAREDCAMERA_PHOTO_Model>();

            DataTable dt = BaseDT.JC_INFRAREDCAMERA_PHOTO.getDT(sw, out total);//用户列表

            DataTable dtUser = BaseDT.T_SYSSEC_USER.getDT(new T_SYSSEC_IPSUSER_SW { });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                JC_INFRAREDCAMERA_PHOTO_Model m = new JC_INFRAREDCAMERA_PHOTO_Model();
                m.smid = dt.Rows[i]["smid"].ToString();
                m.tpa = dt.Rows[i]["tpa"].ToString();
                m.recvdatetime = ClsSwitch.SwitTM(dt.Rows[i]["recvdatetime"].ToString());
                m.filename = dt.Rows[i]["filename"].ToString();
                m.MANSTATE = dt.Rows[i]["MANSTATE"].ToString();
                m.MANRESULT = dt.Rows[i]["MANRESULT"].ToString();
                m.MANTIME = ClsSwitch.SwitTM(dt.Rows[i]["MANTIME"].ToString());
                m.MANUSERID = dt.Rows[i]["MANUSERID"].ToString();
                if (!string.IsNullOrEmpty(m.MANUSERID))
                {
                    DataRow[] drUser = dtUser.Select("USERID='" + m.MANUSERID + "'");
                    if (drUser.Length > 0)
                    {
                        m.ManUserName = drUser[0]["USERNAME"].ToString();
                    }

                }
                m.BasicInfoModel = getModel(new JC_INFRAREDCAMERA_BASICINFO_SW { PHONE = m.tpa });
                result.Add(m);
            }
            dtUser.Clear();
            dtUser.Dispose();
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion

        #region 获取视频列表
        /// <summary>
        /// 获取视频列表异步树
        /// </summary>
        /// <param name="OrgNo"></param>
        /// <param name="eid"></param>
        /// <returns></returns>
        public static string getTree(string OrgNo, string eid)
        {
            JArray jObjects = new JArray();
            string curUserOrg = SystemCls.getCurUserOrgNo();//获取当前登录用户的机构编码
            if (string.IsNullOrEmpty(OrgNo) == false)
                curUserOrg = OrgNo;
            DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { TopORGNO = curUserOrg, SYSFLAG = ConfigCls.getSystemFlag() });//获取当前登录用户有权限查看的单位
            DataTable dtFRUser = BaseDT.JC_MONITOR_BASICINFO.getDT(new JC_MONITOR_BASICINFO_SW { BYORGNO = curUserOrg, EMID = eid });//获取所有有权限的监控摄像
            #region 市级用户
            if (PublicCls.OrgIsShi(curUserOrg))
            {
                DataRow[] drOrg = dtOrg.Select("", "ORGNO");
                if (drOrg.Length > 0)
                {
                    JObject root = new JObject
                     {
                         {"id",drOrg[0]["ORGNO"].ToString()},//ORGNO
                         {"text",drOrg[0]["ORGNAME"].ToString()} ,
                         {"treeType","org"} ,
                     };
                    JArray childArray = new JArray();
                    DataRow[] drOrgC = dtOrg.Select("SUBSTRING(ORGNO,1,4) = '" + curUserOrg.Substring(0, 4) + "' AND ORGNO<>'" + curUserOrg + "' and SUBSTRING(ORGNO,7,3)='000'", "");//获取所有县且〈〉市的
                    for (int i = 0; i < drOrgC.Length; i++)
                    {
                        JObject rootC = new JObject
                     {
                         {"id",drOrgC[i]["ORGNO"].ToString()},//ORGNO
                         {"text",drOrgC[i]["ORGNAME"].ToString()},
                         {"state","closed"},
                         {"treeType","org"} 
                     };
                        childArray.Add(rootC);
                    }
                    root.Add("children", childArray);
                    jObjects.Add(root);
                }
            }
            #endregion

            #region 县级用户
            else if (PublicCls.OrgIsXian(curUserOrg))
            {
                DataRow[] drOrg = dtOrg.Select("", "ORGNO");
                if (drOrg.Length > 0)
                {
                    JObject root = new JObject
                     {
                         {"id",drOrg[0]["ORGNO"].ToString()},//ORGNO
                         {"text",drOrg[0]["ORGNAME"].ToString()}, 
                         {"state","closed"} ,
                         {"treeType","org"} 
                     };
                    JArray jObjectsC = new JArray();
                    DataRow[] drOrgC = dtOrg.Select("SUBSTRING(ORGNO,1,6) = '" + curUserOrg.Substring(0, 6) + "' AND ORGNO<>'" + curUserOrg + "'", ""); //获取所有县且〈〉市的
                    for (int i = 0; i < drOrgC.Length; i++)
                    {
                        JObject rootC = new JObject
                        {
                         {"id",drOrgC[i]["ORGNO"].ToString()},//ORGNO
                         {"text",drOrgC[i]["ORGNAME"].ToString()},
                         {"state","closed"} ,
                         {"treeType","org"} 
                       };
                        //root.Add("children", getTreeChild(dtOrg, dtFRUser, dtRealTmp, drOrg[i]["ORGNO"].ToString()));//继续获取护林员
                        jObjectsC.Add(rootC);
                        if (string.IsNullOrEmpty(OrgNo) == false)//异步加载，不显示县名
                        {
                            jObjects.Add(rootC);
                        }
                    }
                    if (string.IsNullOrEmpty(OrgNo))//县级用户登录
                    {
                        jObjects.Add(root);
                        root.Add("children", jObjectsC);
                    }
                }
            }
            #endregion

            #region 乡镇用户
            else
            {
                DataRow[] drOrg = dtOrg.Select("", "ORGNO");
                if (drOrg.Length > 0)
                {
                    JObject root = new JObject
                     {
                         {"id",drOrg[0]["ORGNO"].ToString()},//ORGNO
                         {"text",drOrg[0]["ORGNAME"].ToString()},
                         {"state","closed"} ,
                         {"treeType","org"} 
                     };
                    DataRow[] drFRUser = dtFRUser.Select("BYORGNO = '" + curUserOrg + "'", "");//获取所有县且〈〉市的
                    JArray jObjectsC = new JArray();
                    for (int i = 0; i < drFRUser.Length; i++)
                    {
                        StringBuilder sb = new StringBuilder();
                        JObject rootC = new JObject { { "id", drFRUser[i]["EMID"].ToString() }, { "text", drFRUser[i]["EMNAME"].ToString() }, { "objId", drFRUser[i]["OBJID"].ToString() }, { "templateId", drFRUser[i]["TEMPLATEDID"].ToString() }, { "ip", drFRUser[i]["ip"].ToString() }, { "port", drFRUser[i]["PORT"].ToString() }, { "isVideo", true } };
                        jObjectsC.Add(rootC);
                        if (string.IsNullOrEmpty(OrgNo) == false)//异步加载，不显示乡镇名
                        {
                            jObjects.Add(rootC);
                        }
                    }
                    if (string.IsNullOrEmpty(OrgNo))//乡镇级用户登录
                    {
                        jObjects.Add(root);
                        root.Add("children", jObjectsC);
                    }
                }
            }
            #endregion

            dtOrg.Clear();
            dtOrg.Dispose();
            return JsonConvert.SerializeObject(jObjects);
        }

        /// <summary>
        /// 获取视频列表
        /// </summary>
        /// <param name="orgno"></param>
        /// <param name="eid"></param>
        /// <returns></returns>
        public static string getSynTree(string orgno, string eid)
        {
            JArray jObjects = new JArray();
            string curUserOrg = SystemCls.getCurUserOrgNo();//获取当前登录用户的机构编码
            if (string.IsNullOrEmpty(orgno) == false)
                curUserOrg = orgno;
            if (string.IsNullOrEmpty(orgno))
            {
                var str = "";
                if (PublicCls.OrgIsShi(curUserOrg))
                {
                    str = "州";
                }
                else if (PublicCls.OrgIsXian(curUserOrg))
                {
                    str = PublicCls.GetOrgNameByOrgNO(curUserOrg);
                }
                else
                {
                    str = PublicCls.GetOrgNameByOrgNO(curUserOrg.Substring(0, 6) + "000000000");
                }
                JObject root = new JObject
                     {
                         {"id","1111"},//ORGNO
                         {"text",str+"视频监控列表"}  
                     };

                var resultlist = JC_MONITORCls.getListModel(new JC_MONITOR_BASICINFO_SW { BYORGNO = curUserOrg }); //获取所有有权限的监控摄像
                if (resultlist.Any())
                {
                    var devOrgSXList = resultlist.Select(p => p.BYORGNO.Substring(0, 6) + "00000000").Distinct();
                    if (devOrgSXList.Any())
                    {
                        JArray devSXArrary = new JArray();
                        foreach (var orgsx in devOrgSXList)
                        {
                            JObject rootSX = new JObject
                            {
                                {"id",orgsx},//ORGNO
                                {"text", PublicCls.GetOrgNameByOrgNO(orgsx)}
                                //{"state","closed"} 
                            };
                            var devOrgXZList = resultlist.Select(p => new { p.BYORGNO, p.ORGNAME }).Distinct()
                                .Where(p => p.BYORGNO.Substring(0, 6) + "00000000" == orgsx);
                            if (devOrgXZList.Any())
                            {
                                JArray devXZArrary = new JArray();
                                foreach (var orgxz in devOrgXZList)
                                {
                                    JObject rootXZ = new JObject
                                {
                                   {"id",orgxz.BYORGNO},//ORGNO
                                   {"text", orgxz.ORGNAME}
                                  // {"state","closed"} 
                                 };
                                    var devlist = resultlist.Where(p => p.BYORGNO == orgxz.BYORGNO);
                                    if (devlist.Any())
                                    {
                                        JArray devArrary = new JArray();
                                        foreach (var dev in devlist)
                                        {
                                            JObject rootDevice = new JObject 
                                      {
                                             { "id", dev.EMID }, { "text", dev.EMNAME },
                                              { "objId", dev.OBJID },   { "templateId", dev.TEMPLATEDID }, { "ip", dev.IP }, 
                                              { "port", dev.PORT }, { "type", dev.TYPE },{ "isVideo", true }
                                          };
                                            devArrary.Add(rootDevice);
                                        }
                                        rootXZ.Add("children", devArrary);
                                        devXZArrary.Add(rootXZ);
                                    }

                                }
                                rootSX.Add("children", devXZArrary);
                                devSXArrary.Add(rootSX);
                            }
                        }
                        root.Add("children", devSXArrary);
                    }
                }
                jObjects.Add(root);
            }
            else
            {
                var str = PublicCls.GetOrgNameByOrgNO(curUserOrg);
                JObject root = new JObject
                     {
                         {"id","1111"},//ORGNO
                         {"text",str+"视频监控列表"}  
                     };
                var devlist = JC_MONITORCls.getListModel(new JC_MONITOR_BASICINFO_SW { BYORGNO = curUserOrg, EMID = eid }); //获取所有有权限的监控摄像
                if (devlist.Any())
                {
                    JArray devArrary = new JArray();
                    foreach (var dev in devlist)
                    {
                        JObject rootDevice = new JObject 
                                      {
                                             { "id", dev.EMID }, { "text", dev.EMNAME },
                                              { "objId", dev.OBJID },   { "templateId", dev.TEMPLATEDID }, { "ip", dev.IP }, 
                                              { "port", dev.PORT }, { "type", dev.TYPE },{ "isVideo", true }
                                        };
                        devArrary.Add(rootDevice);
                    }
                    root.Add("children", devArrary);
                    jObjects.Add(root);
                }
            }
            return JsonConvert.SerializeObject(jObjects);
        }

        #endregion
    }
}
