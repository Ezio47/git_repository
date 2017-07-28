using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{

    #region 系统配置信息
    ///// <summary>
    ///// 系统配置信息
    ///// </summary>
    //public class ConfigModel
    //{
    //    /// <summary>
    //    /// 系统名称
    //    /// </summary>
    //    public string SystemName { get; set; }
    //    /// <summary>
    //    /// 系统标识符
    //    /// </summary>
    //    public string SystemFlag { get; set; }
    //}
    #endregion

    #region 用户Cookie保存信息
    /// <summary>
    /// 用户Cookie保存信息
    /// </summary>
    public class CookieModel
    {
        /// <summary>
        /// 用户序号
        /// </summary>
        public string UID { get; set; }
        /// <summary>
        /// 登录用户名
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string trueName { get; set; }
        /// <summary>
        /// 登录状态
        /// </summary>
        public string SaveType { get; set; }
    }
    #endregion


}
