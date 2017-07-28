namespace TLW.AH.Business.DBUtility
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class JC_MONITOR_BASICINFO
    {
        [Key]
        public int EMID { get; set; }

        [StringLength(50)]
        public string TTBH { get; set; }

        [StringLength(50)]
        public string EMNAME { get; set; }

        [StringLength(15)]
        public string BYORGNO { get; set; }

        public double? JD { get; set; }

        public double? WD { get; set; }

        public double? GC { get; set; }

        [StringLength(500)]
        public string ADDRESS { get; set; }

        [StringLength(15)]
        public string IP { get; set; }

        [StringLength(15)]
        public string LOGINUSERNAME { get; set; }

        [StringLength(50)]
        public string USERPWD { get; set; }

        [StringLength(50)]
        public string XH { get; set; }

        [StringLength(50)]
        public string PP { get; set; }

        public double? GD { get; set; }

        public double? JCJL { get; set; }
    }
}
