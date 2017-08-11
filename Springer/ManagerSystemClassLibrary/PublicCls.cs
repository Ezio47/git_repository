using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel;
using DataBaseClassLibrary;
using PublicClassLibrary;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 公用类
    /// </summary>
    public class PublicCls
    {
        /// <summary>
        /// 经纬度坐标偏移量计算
        /// </summary>
        /// <param name="jd">经度</param>
        /// <param name="wd">纬度</param>
        /// <returns>经纬度</returns>
        public static string[] switJWD(string jd, string wd)
        {
            string[] arr = new string[2];
            double[] arrDbl = new double[2];
            if (string.IsNullOrEmpty(jd))
                return arr;
            if (string.IsNullOrEmpty(wd))
                return arr;
            arrDbl = ClsCoordinateRectify.transform2(Convert.ToDouble(jd), Convert.ToDouble(wd));
            arr[0] = arrDbl[0].ToString();
            arr[1] = arrDbl[1].ToString();
            return arr;
        }

        /// <summary>
        /// 根据组织机构获取该行应用的样式名称
        /// </summary>
        /// <param name="topOrgNo">顶级单位编码</param>
        /// <param name="orgCode">当前单位编码</param>
        /// <returns>样式名称</returns>
        public static string getOrgTrClass(string topOrgNo, string orgCode)
        {
            string str1 = "danger";
            string str2 = "warning";
            string str3 = "";
            if (PublicCls.OrgIsShi(topOrgNo))
            {
                str1 = "danger";
                str2 = "warning";
                str3 = "";
            }
            else if (PublicCls.OrgIsXian(topOrgNo))
            {
                str1 = "";
                str2 = "danger";
                str3 = "warning";
            }
            else if (PublicCls.OrgIsZhen(topOrgNo))
            {
                str1 = "";
                str2 = "";
                str3 = "danger";
            }
            if (PublicCls.OrgIsShi(orgCode))//市
            {
                return str1;
            }
            else if (PublicCls.OrgIsXian(orgCode))//县
            {
                return str2;
            }
            else
            {
                return str3;
            }
        }

        /// <summary>
        /// 获取组织机构渐进名称
        /// </summary>
        /// <param name="topOrgNo">顶级单位编码</param>
        /// <param name="orgCode">当前单位编码</param>
        /// <param name="orgName">单位名称</param>
        /// <returns>格式化后的单位名称（缩进）</returns>
        public static string getOrgTDName1(string topOrgNo, string orgCode, string orgName)
        {
            string str2 = "&nbsp;&nbsp;&nbsp;";
            string str3 = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            string str4 = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            if (PublicCls.OrgIsShi(topOrgNo))
            { }
            else if (PublicCls.OrgIsXian(topOrgNo))
            {
                str4 = str3;
                str3 = str2;
                str2 = "";
            }
            else if (PublicCls.OrgIsZhen(topOrgNo))
            {
                str4 = str3;
                str3 = "";
            }
            else if (PublicCls.OrgIsCun(topOrgNo))
            {
                str4 = "";
            }
            if (PublicCls.OrgIsShi(orgCode))//市
            {
                return orgName;
            }
            else if (PublicCls.OrgIsXian(orgCode))//县
            {
                return str2 + orgName;
            }
            else if (PublicCls.OrgIsZhen(orgCode))
            {
                return str3 + orgName;
            }
            else
            {
                return str4 + orgName;
            }
        }

        /// <summary>
        /// 获取组织机构渐进名称
        /// </summary>
        /// <param name="topOrgNo">顶级单位编码</param>
        /// <param name="orgCode">当前单位编码</param>
        /// <returns>组织机构渐进名称</returns>
        public static string getOrgTDNameClass(string topOrgNo, string orgCode)
        {
            string str1 = "padding-left:0px;";
            string str2 = "padding-left:20px;";
            string str3 = "padding-left:40px;";
            string str4 = "padding-left:60px;";
            if (PublicCls.OrgIsShi(topOrgNo))
            {
                str1 = "padding-left:0px;";
                str2 = "padding-left:20px;";
                str3 = "padding-left:40px;";
                str4 = "padding-left:60px;";
            }
            else if (PublicCls.OrgIsXian(topOrgNo))
            {
                str1 = "padding-left:0px;";
                str2 = "padding-left:0px;";
                str3 = "padding-left:20px;";
                str4 = "padding-left:40px;";
            }
            else if (PublicCls.OrgIsZhen(topOrgNo))
            {
                str1 = "padding-left:0px;";
                str2 = "padding-left:0px;";
                str3 = "padding-left:0px;";
                str4 = "padding-left:20px;";
            }
            else if (PublicCls.OrgIsCun(topOrgNo))
            {
                str1 = "padding-left:0px;";
                str2 = "padding-left:0px;";
                str3 = "padding-left:0px;";
                str4 = "padding-left:0px;";
            }
            if (PublicCls.OrgIsShi(orgCode))//市
            {
                return str1;
            }
            else if (PublicCls.OrgIsXian(orgCode))//县
            {
                return str2;
            }
            else if (PublicCls.OrgIsZhen(orgCode))
            {
                return str3;
            }
            else
            {
                return str4;
            }
        }

        /// <summary>
        /// 判断组织机构是否是市
        /// </summary>
        /// <param name="str">单位编码</param>
        /// <returns>true 市 false 否</returns>
        public static bool OrgIsShi(string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;
            if (str.Length != 15)
                return false;
            if (str.Substring(4, 11) == "00000000000")
                return true;
            return false;
        }

        /// <summary>
        /// 判断组织机构是否是县
        /// </summary>
        /// <param name="str">单位编码</param>
        /// <returns>true 县 false 否</returns>
        public static bool OrgIsXian(string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;
            if (str.Length != 15)
                return false;
            if (str.Substring(6, 9) == "000000000" && str.Substring(4, 11) != "00000000000")
                return true;
            return false;
        }

        /// <summary>
        /// 判断组织机构是否是镇（乡）
        /// </summary>
        /// <param name="str">单位编码</param>
        /// <returns>true 镇（乡） false 否</returns>
        public static bool OrgIsZhen(string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;
            if (str.Length != 15)
                return false;
            if (str.Substring(9, 6) == "000000" && str.Substring(6, 9) != "000000000")
                return true;
            return false;
        }

        /// <summary>
        /// 判断组织机构是否是村
        /// </summary>
        /// <param name="str">单位编码</param>
        /// <returns>true 村 false 否</returns>
        public static bool OrgIsCun(string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;
            if (str.Length != 15)
                return false;
            if (str.Substring(9, 6) != "000000")
                return true;
            return false;
        }

        /// <summary>
        /// 返回市、县、镇（乡）前面机构编码
        /// </summary>
        /// <param name="str">单位编码</param>
        /// <returns>返回市、县、镇（乡）前面机构编码</returns>
        public static string getShiIncOrgNo(string str)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            if (str.Length != 15)
                return "";
            return str.Substring(0, 4);
        }

        /// <summary>
        /// 返回县、镇（乡）前面机构编码
        /// </summary>
        /// <param name="str">单位编码</param>
        /// <returns>返回县、镇（乡）前面机构编码</returns>
        public static string getXianIncOrgNo(string str)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            if (str.Length != 15)
                return "";
            return str.Substring(0, 6);
        }

        /// <summary>
        /// 返回镇（乡）机构编码
        /// </summary>
        /// <param name="str">单位编码</param>
        /// <returns>返回镇（乡）机构编码</returns>
        public static string getZhenIncOrgNo(string str)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            if (str.Length != 15)
                return "";
            return str.Substring(0, 9);
        }

        /// <summary>
        /// 返回村机构编码
        /// </summary>
        /// <param name="str">单位编码</param>
        /// <returns>返回村机构编码</returns>
        public static string getCunIncOrgNo(string str)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            if (str.Length != 15)
                return "";
            return str.Substring(0, 12);
        }

        /// <summary>
        /// 获取树种渐进名称
        /// </summary>
        /// <param name="TSPCODE">树种编码</param>
        /// <returns></returns>
        public static string getTSPNameClass(string TSPCODE)
        {
            string str1 = "padding-left:0px;";
            string str2 = "padding-left:20px;";
            string str3 = "padding-left:40px;";
            if (TSPCODE.Length == 2)
                return str1;
            else if (TSPCODE.Length == 4)
                return str2;
            else if (TSPCODE.Length == 6)
                return str3;
            else
                return str3;
        }

        /// <summary>
        /// 获取图层渐进名称
        /// </summary>
        /// <param name="LAYERCODE">图层编码</param>
        /// <returns></returns>
        public static string getLAYERNameClass(string LAYERCODE)
        {
            int lenth = LAYERCODE.Length / 2;
            return "padding-left:" + ((lenth - 1) * 20).ToString() + "px;"; ;
        }

        /// <summary>
        /// 有机构码获取机构名
        /// </summary>
        /// <param name="orgno"></param>
        /// <returns></returns>
        public static string GetOrgNameByOrgNO(string orgno)
        {
            string str = "";
            DataTable dt = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW() { ORGNO = orgno });
            str = BaseDT.T_SYS_ORG.getName(dt, orgno);
            return str;
        }
    }
}
