using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerSystem.MVC.Models
{
    public class EHCurFireMode
    {

        /// <summary>
        /// 主键
        /// </summary>
        public string JCFID { get; set; }
        /// <summary>
        ///火险名
        /// </summary>
        public string FIRENAME { get; set; }

        /// <summary>
        /// 所属机构
        /// </summary>
        public string ORGNO { get; set; }
        /// <summary>
        /// 发生区域
        /// </summary>
        public string ADDRESSS { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public string JD { get; set; }


        /// <summary>
        /// 纬度
        /// </summary>
        public string WD { get; set; }

        /// <summary>
        /// 火险等级
        /// </summary>
        public string FIRELEVEL { get; set; }
    }
}