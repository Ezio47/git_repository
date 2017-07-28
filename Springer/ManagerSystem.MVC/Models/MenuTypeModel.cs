using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerSystem.MVC.Models
{
    public class MenuTypeModel
    {
        /// <summary>
        /// 菜单名
        /// </summary>
        public string MENUNAME { get; set; }
        /// <summary>
        /// 菜单Class
        /// </summary>
        public string LICLASS { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string DICTTYPEID { get; set; }
        /// <summary>
        /// 类型值
        /// </summary>
        public string DICTVALUE { get; set; }
    }
}