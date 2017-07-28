namespace TLW.AH.Business.DBUtility
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class JC_FIRE
    {
        [Key]
        public int JCFID { get; set; }

        [StringLength(200)]
        public string FIRENAME { get; set; }

        [StringLength(15)]
        public string BYORGNO { get; set; }

        [StringLength(20)]
        public string FIREFROM { get; set; }

        public int? FIREFROMID { get; set; }

        public DateTime? FIRETIME { get; set; }

        public DateTime? FIREENDTIME { get; set; }

        public int? ISOUTFIRE { get; set; }

        [StringLength(2000)]
        public string MARK { get; set; }

        public double? JD { get; set; }

        public double? WD { get; set; }

        [StringLength(1000)]
        public string ZQWZ { get; set; }

        [StringLength(100)]
        public string WXBH { get; set; }

        [StringLength(100)]
        public string DQRDBH { get; set; }

        public double? RSMJ { get; set; }

        [StringLength(100)]
        public string DL { get; set; }

        [StringLength(100)]
        public string YY { get; set; }

        [StringLength(100)]
        public string JXHQSJ { get; set; }

        public int? OWERJCFID { get; set; }

        [StringLength(50)]
        public string PFUSERID { get; set; }

        [StringLength(15)]
        public string PFORGNO { get; set; }

        public DateTime? PFTIME { get; set; }

        public int? PFFLAG { get; set; }

        public DateTime? RECEIVETIME { get; set; }

        public DateTime? ISSUEDTIME { get; set; }

        public DateTime? LASTPROCESSTIME { get; set; }

        public int MANSTATE { get; set; }

        [StringLength(20)]
        public string FIREFROMWEATHER { get; set; }
    }
}
