namespace TLW.AH.Business.DBUtility
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_SYS_ORG
    {
        [Key]
        [StringLength(15)]
        public string ORGNO { get; set; }

        [StringLength(20)]
        public string ORGNAME { get; set; }

        [Column(TypeName = "text")]
        public string ORGDUTY { get; set; }

        [StringLength(20)]
        public string LEADER { get; set; }

        [StringLength(15)]
        public string AREACODE { get; set; }

        [StringLength(20)]
        public string ORGJC { get; set; }

        [StringLength(20)]
        public string WXJC { get; set; }

        [StringLength(20)]
        public string SYSFLAG { get; set; }

        public double? JD { get; set; }

        public double? WD { get; set; }

        [StringLength(50)]
        public string COMMANDNAME { get; set; }

        [StringLength(20)]
        public string CITYID { get; set; }

        [StringLength(20)]
        public string WEATHERJC { get; set; }

        [StringLength(20)]
        public string POSTCODE { get; set; }

        [StringLength(50)]
        public string DUTYTELL { get; set; }

        [StringLength(50)]
        public string FAX { get; set; }

        [StringLength(2000)]
        public string MOBILEPARAMLIST { get; set; }

        [StringLength(500)]
        public string ADDRESS { get; set; }
    }
}
