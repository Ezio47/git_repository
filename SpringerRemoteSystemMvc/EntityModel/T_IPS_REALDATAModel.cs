using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Springer.EntityModel
{
    /// <summary>
    /// T_IPS_REALDATA:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class T_IPS_REALDATAModel
    {
        public T_IPS_REALDATAModel()
        { }
        #region Model
        private long _realdataid;
        private string _phone;
        private decimal? _longitude;
        private decimal? _latitude;
        private decimal? _height;
        private decimal? _electric;
        private decimal? _speed;
        private decimal? _direction;
        private DateTime? _sbtime;
        private string _note;
        private int _isoutrail;
        /// <summary>
        /// 
        /// </summary>
        public long REALDATAID
        {
            set { _realdataid = value; }
            get { return _realdataid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PHONE
        {
            set { _phone = value; }
            get { return _phone; }
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
        public decimal? ELECTRIC
        {
            set { _electric = value; }
            get { return _electric; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? SPEED
        {
            set { _speed = value; }
            get { return _speed; }
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
        /// <summary>
        /// 
        /// </summary>
        public string NOTE
        {
            set { _note = value; }
            get { return _note; }
        }

        public int ISOUTRAIL
        {
            set { _isoutrail = value; }
            get { return _isoutrail; }
        }
        #endregion Model
    }
}
