namespace TLW.AH.Business.DBUtility
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_VIDEO_ORIGINAL
    {
        public long ID { get; set; }

        [StringLength(500)]
        public string DEVICEID { get; set; }

        [StringLength(500)]
        public string ALARMID { get; set; }

        [StringLength(500)]
        public string HOR { get; set; }

        [StringLength(500)]
        public string PIT { get; set; }

        [StringLength(500)]
        public string VIEW { get; set; }

        [StringLength(500)]
        public string LONGTITUE { get; set; }

        [StringLength(500)]
        public string LATITUE { get; set; }

        [StringLength(500)]
        public string ELEVATION { get; set; }

        [StringLength(500)]
        public string BAK { get; set; }
    }
}
