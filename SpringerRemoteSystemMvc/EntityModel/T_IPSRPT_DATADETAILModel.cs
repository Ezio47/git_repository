using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Springer.EntityModel
{
    /// <summary>
    /// T_IPSRPT_DATADETAIL:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class T_IPSRPT_DATADETAILModel
    {
        public T_IPSRPT_DATADETAILModel()
        { }
        #region Model
        private long _reportdetailid;
        private long _reportid;
        private decimal? _longitude;
        private decimal? _latitude;
        private decimal? _height;
        private decimal? _direction;
        private DateTime? _sbtime;
        /// <summary>
        /// 
        /// </summary>
        public long REPORTDETAILID
        {
            set { _reportdetailid = value; }
            get { return _reportdetailid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long REPORTID
        {
            set { _reportid = value; }
            get { return _reportid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? LONGITUDE
        {
            set { _longitude = value; }
            get { return _longitude; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? LATITUDE
        {
            set { _latitude = value; }
            get { return _latitude; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? HEIGHT
        {
            set { _height = value; }
            get { return _height; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? DIRECTION
        {
            set { _direction = value; }
            get { return _direction; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? SBTIME
        {
            set { _sbtime = value; }
            get { return _sbtime; }
        }
        #endregion Model

    }
}
