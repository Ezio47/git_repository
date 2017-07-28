using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Springer.EntityModel
{
    /// <summary>
    /// T_IPSCOL_DATADETAIL:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class T_IPSCOL_DATADETAILModel
    {
        public T_IPSCOL_DATADETAILModel()
        { }
        #region Model
        private long _collectdetailid;
        private long _collectid;
        private decimal? _longitude;
        private decimal? _latitude;
        private decimal? _height;
        private decimal? _direction;
        private DateTime? _collecttime;
        /// <summary>
        /// 
        /// </summary>
        public long COLLECTDETAILID
        {
            set { _collectdetailid = value; }
            get { return _collectdetailid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long COLLECTID
        {
            set { _collectid = value; }
            get { return _collectid; }
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
        public DateTime? COLLECTTIME
        {
            set { _collecttime = value; }
            get { return _collecttime; }
        }
        #endregion Model

    }
}
