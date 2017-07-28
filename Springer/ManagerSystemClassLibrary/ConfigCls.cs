using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagerSystemClassLibrary;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel;
using DataBaseClassLibrary;
using PublicClassLibrary;
using System.Configuration;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 系统配置类
    /// </summary>
    public class ConfigCls
    {
        /// <summary>
        /// 系统名称
        /// </summary>
        /// <returns>系统名称</returns>
        public static string getSystemName()
        {
            return ConfigurationManager.AppSettings["getSystemName"].ToString(); //"智能巡护系统";
        }

        /// <summary>
        /// 系统标识符
        /// </summary>
        /// <returns>系统标识符</returns>
        public static string getSystemFlag()
        {
            return ConfigurationManager.AppSettings["getSystemFlag"].ToString(); // "Springer";
        }

        /// <summary>
        /// 用于获取顶级区划编码，如只取合肥市，则为3401
        /// </summary>
        /// <returns>顶级区划编码</returns>
        public static string getTopAreaCode()
        {
            return ConfigurationManager.AppSettings["getTopAreaCode"].ToString(); // "3401";
        }

        /// <summary>
        /// 获取配置文件值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string getConfigValue(string name)
        {
            return ConfigurationManager.AppSettings[name].ToString();
        }

        /// <summary>
        /// 护林员刷新时间间隔 毫秒
        /// </summary>
        /// <returns>毫秒</returns>
        public static int HRRefreshInterval()
        {
            return Convert.ToInt32(T_SYS_PARAMETERCls.getValueByFlag(new T_SYS_PARAMETER_SW { SYSFLAG = ConfigCls.getSystemFlag(), PARAMFLAG = "HRRefreshInterval" }));
        }

        /// <summary>
        /// 判断护林员是否在线时间间隔 分
        /// </summary>
        /// <returns>分</returns>
        public static int inLineTimeInterval()
        {
            return Convert.ToInt32(T_SYS_PARAMETERCls.getValueByFlag(new T_SYS_PARAMETER_SW { SYSFLAG = ConfigCls.getSystemFlag(), PARAMFLAG = "inLineTimeInterval" }));
        }

        /// <summary>
        /// 巡检距离误差（用于判断怠工 单位米（m））
        /// </summary>
        /// <returns>米（m）</returns>
        public static float getPatrolLengthError()
        {
            return float.Parse(T_SYS_PARAMETERCls.getValueByFlag(new T_SYS_PARAMETER_SW { SYSFLAG = ConfigCls.getSystemFlag(), PARAMFLAG = "getPatrolLengthError" }));
        }

        /// <summary>
        /// 顶部菜单提醒数量刷新时间设置 分钟
        /// </summary>
        /// <returns>分钟</returns>
        public static string noticeRefreshTimeInterval()
        {
            return T_SYS_PARAMETERCls.getValueByFlag(new T_SYS_PARAMETER_SW { SYSFLAG = ConfigCls.getSystemFlag(), PARAMFLAG = "noticeRefreshTimeInterval" });
        }

        /// <summary>
        /// 护林员树形菜单在线提醒颜色
        /// </summary>
        /// <returns>在线提醒颜色</returns>
        public static string getInLineColor()
        {
            return ConfigurationManager.AppSettings["InLineColor"].ToString();
        }

        /// <summary>
        /// 护林员树形菜单离线提醒颜色
        /// </summary>
        /// <returns>离线提醒颜色</returns>
        public static string getOutLineColor()
        {
            return ConfigurationManager.AppSettings["OutLineColor"].ToString();
        }

        /// <summary>
        /// 护林员树形菜单出围（责任区）提醒颜色
        /// </summary>
        /// <returns>出围（责任区）提醒颜色</returns>
        public static string getOutRailColor()
        {
            return ConfigurationManager.AppSettings["OutRailColor"].ToString();
        }

        /// <summary>
        /// 菜单显示方式
        /// </summary>
        /// <returns> 1 护林员 2 仅显示页面且页面平铺显示</returns>
        public static string getMenuShowMode()
        {
            return ConfigurationManager.AppSettings["MenuShowMode"].ToString();
        }

        /// <summary>
        /// 登录后跳转显示页面　即登录成功后跳转的页面
        /// </summary>
        /// <returns>/Home/Index</returns>
        public static string getLoginRedirectUrl()
        {
            //从config里读取首页
            if (ConfigurationManager.AppSettings["LoginRedirectUrlType"].ToString() == "0")
            {
                return ConfigurationManager.AppSettings["LoginRedirectUrl"].ToString();
            }
            //获取有权限的所有菜单
            var result = T_SYS_MENUCls.getT_SYS_MENUModel(new T_SYS_MENU_SW { UID = SystemCls.getUserID(), SYSFLAG = ConfigCls.getSystemFlag() });
            var v = result.Where(p => p.ISTOPMENU == "1" && p.MENUCODE.Length == 3).FirstOrDefault();//获取顶部菜单
            var vv = v.subMenuModel.Where(p => p.MENUCODE.Length == 6).FirstOrDefault();
            string url = vv.MENUURL;
            return url;
        }
        public static string getLoginRedirectUrl(string uid)
        {
            //从config里读取首页
            if (ConfigurationManager.AppSettings["LoginRedirectUrlType"].ToString() == "0")
            {
                return ConfigurationManager.AppSettings["LoginRedirectUrl"].ToString();
            }
            //获取有权限的所有菜单
            var result = T_SYS_MENUCls.getT_SYS_MENUModel(new T_SYS_MENU_SW { UID = uid, SYSFLAG = ConfigCls.getSystemFlag() });
            var v = result.Where(p => p.ISTOPMENU == "1" && p.MENUCODE.Length == 3).FirstOrDefault();//获取顶部菜单
            var vv = v.subMenuModel.Where(p => p.MENUCODE.Length == 6).FirstOrDefault();
            string url = vv.MENUURL;
            return url;
        }

        /// <summary>
        /// 单位下拉框默认选中项
        /// </summary>
        /// <returns>单位编码</returns>
        public static string getTopOrgCode()
        {
            return ConfigurationManager.AppSettings["getTopOrgCode"].ToString();
        }

        /// <summary>
        /// 用户最后操作时间 系统用户在线时间间隔（分钟）
        /// </summary>
        /// <returns> 0为不判断</returns>
        public static string getIsSaveLastOpTime()
        {
            string str = ConfigurationManager.AppSettings["IsSaveLastOpTime"].ToString();
            if (string.IsNullOrEmpty(str))
                str = "0";
            return str;
        }

        /// <summary>
        /// 用户最后操作时间 系统用户在线时间间隔（分钟）
        /// </summary>
        /// <returns> 0为不判断</returns>
        public static string getTopNewsTopCount()
        {
            string str = ConfigurationManager.AppSettings["TopNewsTopCount"].ToString();
            if (string.IsNullOrEmpty(str))
                str = "4";
            return str;
        }

        /// <summary>
        /// 获取是否自动发送火险等级短信设置 1 自动发送 0 为手动发送
        /// </summary>
        /// <returns></returns>
        public static string getIsAutoSendFireLevelMsg()
        {
            string str = ConfigurationManager.AppSettings["IsAutoSendFireLevelMsg"].ToString();
            if (string.IsNullOrEmpty(str))
                str = "1";
            return str;
        }

        /// <summary>
        /// 表格类默认每页数
        /// </summary>
        /// <returns></returns>
        public static string getTableDefaultPageSize()
        {
            string str = ConfigurationManager.AppSettings["TableDefaultPageSize"].ToString();
            if (string.IsNullOrEmpty(str))
                str = "10";
            return str;
        }

        /// <summary>
        /// 空间库（队伍）是否写入 0 不写入 1 为写入
        /// </summary>
        /// <returns></returns>
        public static string getSDEDBTeam()
        {
            string str = ConfigurationManager.AppSettings["SDEDBTeam"].ToString();
            if (string.IsNullOrEmpty(str))
                str = "1";
            return str;
        }

        /// <summary>
        /// 坐标系之间转换 1表示84 to 火星坐标系 2表示火星坐标转wgs84 3表示火星坐标转换成百度坐标 4表示百度坐标转换成火星坐标 5表示百度坐标系转换成84
        /// </summary>
        /// <returns></returns>
        public static string getSDELonLatTransform()
        {
            string str = ConfigurationManager.AppSettings["SDELonLatTransform"].ToString();
            if (string.IsNullOrEmpty(str))
                str = "1";
            return str;
        }

        /// <summary>
        /// 是否显示OA集成信息   考勤  待办  拟办  短信
        /// </summary>
        /// <returns></returns>
        public static string getOAShowMethod()
        {
            string str = ConfigurationManager.AppSettings["getOAShowMethod"].ToString();
            if (string.IsNullOrEmpty(str))
                str = "0";
            return str;
        }

        /// <summary>
        /// 是否同步OA
        /// </summary>
        /// <returns></returns>
        public static string getIsTongBuOA()
        {
            string str = ConfigurationManager.AppSettings["IsTongBuOA"].ToString();
            if (string.IsNullOrEmpty(str))
                str = "0";
            return str;
        }

        /// <summary>
        /// 获取OA部署地址
        /// </summary>
        /// <returns></returns>
        public static string getOAAddress()
        {
            return ConfigurationManager.AppSettings["OAAddress"].ToString();
        }
      
        /// <summary>
        /// 获取OA初始密码
        /// </summary>
        /// <returns></returns>
        public static string getOAPwd()
        {
            return ConfigurationManager.AppSettings["OAPWd"].ToString();
        }

        /// <summary>
        /// 获取OA服务地址
        /// </summary>
        /// <returns></returns>
        public static string getOAWebServiseAddress()
        {
            return ConfigurationManager.AppSettings["OAWebServise"].ToString();
        }
        /// <summary>
        /// 获取州府所在地
        /// </summary>
        /// <returns></returns>
        public static string getProvincialCapital()
        {
            return ConfigurationManager.AppSettings["ProvincialCapital"].ToString();

        }
    }
}
