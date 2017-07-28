using ManagerSystemClassLibrary;
using ManagerSystemSearchWhereModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace ManagerSystem.MVC.HelpCom
{
    /// <summary>
    /// 状态转换类
    /// </summary>
    public class StateSwitch
    {
        /// <summary>
        /// 签收状态//火情处理状态：1、市已签收2、县已签收3、县已反馈4、市已反馈
        /// </summary>
        /// <param name="state"></param>
        public static string QSState(string orgno, string state)
        {
            string str = "";
            bool bo = PublicCls.OrgIsShi(orgno);//是否为市
            if (bo)
            {
                if (state == "1")
                {
                    str = "下级未签收下发核查";
                }
                else if (state == "2")
                {
                    str = "下级未反馈";
                }
                else if (state == "3")
                {
                    str = "本级未审核";
                }
                else if (state == "4")
                {
                    str = "本级已上报";
                }
                else
                {
                    str = "本级未签收";
                }
            }
            else
            {
                bool bb = PublicCls.OrgIsXian(orgno);//是否为县
                if (bb)
                {
                    if (state == "1")
                    {
                        str = "本级未签收下发核查";
                    }
                    else if (state == "2")
                    {
                        str = "本级未反馈";
                    }
                    else if (state == "3")
                    {
                        str = "本级已反馈";
                    }
                    else if (state == "4")
                    {
                        str = "本级已反馈";
                    }
                    else
                    {
                        str = "本级未签收";
                    }
                }
                else
                {
                    var bx = PublicCls.OrgIsZhen(orgno);//是否为乡镇
                    if (bx)
                    {
                        if (state == "1")
                        {
                            str = "本级未签收下发核查";
                        }
                        else if (state == "2")
                        {
                            str = "本级未反馈";
                        }
                        else if (state == "3")
                        {
                            str = "本级已反馈";
                        }
                        else if (state == "4")
                        {
                            str = "本级已反馈";
                        }
                        else
                        {
                            str = "无此状态";
                        }
                    }
                }
            }
            return str;
        }

        /// <summary>
        /// 签收状态
        /// </summary>
        /// <param name="orgno"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public static string QSStateNew(string orgno, string state)
        {
            string str = "";
            bool bo = PublicCls.OrgIsShi(orgno);//是否为市 州：10~30
            if (bo)
            {
                if (state.Trim() == "")
                {
                    str = "未签收";
                }
                else if (state == "11" || state == "15")
                {
                    str = "未审核";
                }
                else if (state == "18" || state == "19")
                {
                    str = "已上报";
                }
                else if (state == "0" || Convert.ToInt32(state) >= 30)
                {
                    str = "已签收";
                }
                else
                {
                    str = "状态未定";
                }
            }
            else
            {
                bool bb = PublicCls.OrgIsXian(orgno);//是否为县 县：30~50
                if (bb)
                {
                    if (state.Trim() == "")
                    {
                        str = "未签收";
                    }
                    else if (state == "32")
                    {
                        str = "已签收并处理";
                    }
                    else if (state == "31")
                    {
                        str = "未审核";
                    }
                    else if (state == "11" || state == "18" || state == "15" || state == "19")
                    {
                        str = "已上报";
                    }
                    else if (state == "51")
                    {
                        str = "等待下级反馈";
                    }
                    else if (state == "33" || state == "34")
                    {
                        str = "上级审核不通过";
                    }
                    else if (state == "0" || Convert.ToInt32(state) >= 50)
                    {
                        str = "已签收下派";
                    }
                    else
                    {
                        str = "状态未定";
                    }

                }
                else
                {
                    bool bc = PublicCls.OrgIsZhen(orgno);//是否为乡镇 乡：50~60
                    if (bc)
                    {
                        if (state.Trim() == "")
                        {
                            str = "未签收";
                        }
                        else if (state == "0")
                        {
                            str = "已签收下派,请跟踪反馈";
                        }
                        else if (state == "51")
                        {
                            str = "上级审核不通过";
                        }
                        else if (Convert.ToInt32(state) > 0 && Convert.ToInt32(state) < 50)
                        {
                            str = "已上报";
                        }
                        else
                        {
                            str = "状态未定";
                        }
                    }
                }
            }
            return str;
        }


        /// <summary>
        /// 签收状态(仅限签收由于签收无顺序性 所以不能用最新时间的记录的状态来判断)
        /// </summary>
        /// <param name="orgno"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public static string QSStateNewList(string orgno, List<string> statelist)
        {
            string str = "";
            bool bo = PublicCls.OrgIsShi(orgno);//是否为市
            if (bo)
            {
                if (statelist.Contains("1") || statelist.Contains("4"))
                {
                    str = "已签收";
                }
                else
                {
                    str = "未签收";
                }
            }
            else
            {
                bool bb = PublicCls.OrgIsXian(orgno);//是否为县
                if (bb)
                {
                    if (statelist.Contains("2") || statelist.Contains("32"))
                    {
                        str = "已签收";
                    }
                    else
                    {
                        str = "未签收";
                    }
                }
                else
                {
                    bool bc = PublicCls.OrgIsZhen(orgno);//是否为乡镇
                    if (bc)
                    {
                        if (statelist.Contains("3"))
                        {
                            str = "已签收";
                        }
                        else
                        {
                            str = "未签收";
                        }
                    }
                }
            }
            return str;
        }


        /// <summary>
        /// 字典值转名称
        /// </summary>
        /// <param name="DicName">字典类别名称</param>
        /// <param name="value">字典值</param>
        /// <returns></returns>
        public static string DicStateName(string DicName, string value)
        {
            string str = "";
            DataTable dt = ManagerSystemClassLibrary.BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTFLAG = DicName });
            str = ManagerSystemClassLibrary.BaseDT.T_SYS_DICT.getName(dt, value); ;
            return str;
        }


        /// <summary>
        /// 字典值转名称
        /// </summary>
        /// <param name="DicName">字典类别ID</param>
        /// <param name="value">字典值</param>
        /// <returns></returns>
        public static string DicStateNameByDicTypeID(string DicTypeID, string value)
        {
            string str = "";
            DataTable dt = ManagerSystemClassLibrary.BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = DicTypeID });
            str = ManagerSystemClassLibrary.BaseDT.T_SYS_DICT.getName(dt, value); ;
            return str;
        }


        /// <summary>
        /// 获取字典列表
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetDicList(string value)
        {
            var result = new Dictionary<string, string>();
            var list = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = value });
            if (list.Any())
            {
                foreach (var item in list)
                {
                    result.Add(item.DICTVALUE, item.DICTNAME);
                }
            }
            return result;
        }

        /// <summary>
        /// 参数key获取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetParamenterByKey(string key)
        {
            var model = T_SYS_PARAMETERCls.getModel(new T_SYS_PARAMETER_SW { PARAMFLAG = key });
            return model.PARAMVALUE;
        }
        /// <summary>
        /// 有机构码获取机构名
        /// </summary>
        /// <param name="orgno"></param>
        /// <returns></returns>
        public static string GetOrgNameByOrgNO(string orgno)
        {
            string str = "";
            DataTable dt = ManagerSystemClassLibrary.BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW() { ORGNO = orgno });
            str = ManagerSystemClassLibrary.BaseDT.T_SYS_ORG.getName(dt, orgno);
            return str;
        }

        /// <summary>
        /// 由userid获取用户名
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static string GetUsrNameByUserid(string userid)
        {
            var model = T_SYSSEC_IPSUSERCls.getModel(new T_SYSSEC_IPSUSER_SW() { USERID = userid });
            return model.USERNAME;
        }

        /// <summary>
        /// 通过HttpWebRequest上传文件到服务器
        /// </summary>
        /// <param name="url">上传地址</param>
        /// <param name="file">文件所有位置</param>
        /// <param name="paramName">表单参数名</param>
        /// <param name="contentType">文件的contentType</param>
        /// <param name="nvc">其他参数集合</param>
        /// <returns></returns>
        public static string UploadFileByHttpWebRequest(string url, string file, string paramName, string contentType, NameValueCollection nvc)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";
            wr.KeepAlive = true;
            wr.Credentials = System.Net.CredentialCache.DefaultCredentials;

            Stream rs = wr.GetRequestStream();

            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            if (nvc != null)
            {
                foreach (string key in nvc.Keys)
                {
                    rs.Write(boundarybytes, 0, boundarybytes.Length);
                    string formitem = string.Format(formdataTemplate, key, nvc[key]);
                    byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                    rs.Write(formitembytes, 0, formitembytes.Length);
                }
            }
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
            string header = string.Format(headerTemplate, paramName, file, contentType);
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

            FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[4096];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }
            fileStream.Close();

            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);
            rs.Close();

            WebResponse wresp = null;
            var result = "";
            try
            {
                wresp = wr.GetResponse();
                Stream stream2 = wresp.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                //成功返回的結果
                result = reader2.ReadToEnd();
            }
            catch
            {
                if (wresp != null)
                {
                    wresp.Close();
                }
            }

            wr = null;
            return result;

        }
    }
}