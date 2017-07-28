using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerSystem.MVC.Models
{
    public class BaiDuApiAddressModel
    {
        public int status { get; set; }

        public AdressModel result { get; set; }
    }

    public class AdressModel
    {
        public LocationModel location { get; set; }
        public string formatted_address { get; set; }
        public string business { get; set; }
        public AddressComponentModel addressComponent { get; set; }
    }
    /// <summary>
    /// 经纬度
    /// </summary>
    public class LocationModel
    {
        public decimal lng { get; set; }
        public decimal lat { get; set; }

    }

    public class AddressComponentModel
    {
        public string city { get; set; }
        public string country { get; set; }
        public string direction { get; set; }
        public string distance { get; set; }
        public string district { get; set; }
        public string province { get; set; }
        public string street { get; set; }
        public string street_number { get; set; }
        public string country_code { get; set; }

    }
}