using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Springer.EntityModel
{
    /// <summary>
    /// 围栏（道路）表
    /// </summary>
    public class T_IPSFR_ROUTERAILModel
    {
        public int HID { get; set; }
        public decimal LONGITUDE { get; set; }
        public decimal LATITUDE { get; set; }
        public decimal HEIGHT { get; set; }
        public int ORDERBY { get; set; }
        public int ROADTYPE { get; set; }
    }
}
