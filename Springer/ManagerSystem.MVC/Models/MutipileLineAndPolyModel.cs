using ManagerSystemModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerSystem.MVC.Models
{
    public class MutipileLineAndPolyModel
    {
          
        /// <summary>
        /// 线或者面点集合
        /// </summary>
        public IEnumerable<IEnumerable<T_IPSFR_ROUTERAIL_Model>> DataList { get; set; }
    }
}