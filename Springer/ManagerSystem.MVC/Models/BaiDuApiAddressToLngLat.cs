using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerSystem.MVC.Models
{
    public class BaiDuApiAddressToLngLat
    {
        public int status { get; set; }

        public LngLatModel result { get; set; }
    }

    public class LngLatModel
    {
        public LngLatLocationModel location { get; set; }
        public string precise { get; set; }
        public string confidence { get; set; }

    }
    /// <summary>
    /// 经纬度
    /// </summary>
    public class LngLatLocationModel
    {
        public decimal lng { get; set; }
        public decimal lat { get; set; }

    }
}