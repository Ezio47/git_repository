using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Springer.EntityModel
{
    /// <summary>
    /// 实时数据中间表
    /// </summary>
    public class T_IPS_REALDATATEMPORARYModel
    {
        /// <summary>
        /// REALDATAIDT
        /// </summary>		
        private long _realdataid;
        public long REALDATAID
        {
            get { return _realdataid; }
            set { _realdataid = value; }
        }
        /// <summary>
        /// USERID
        /// </summary>		
        private int _userid;
        public int USERID
        {
            get { return _userid; }
            set { _userid = value; }
        }
        /// <summary>
        /// 经度
        /// </summary>		
        private decimal _longitude;
        public decimal LONGITUDE
        {
            get { return _longitude; }
            set { _longitude = value; }
        }
        /// <summary>
        /// 纬度
        /// </summary>		
        private decimal _latitude;
        public decimal LATITUDE
        {
            get { return _latitude; }
            set { _latitude = value; }
        }
        /// <summary>
        /// 高度
        /// </summary>		
        private decimal _height;
        public decimal HEIGHT
        {
            get { return _height; }
            set { _height = value; }
        }
        /// <summary>
        /// 电量
        /// </summary>		
        private decimal _electric;
        public decimal ELECTRIC
        {
            get { return _electric; }
            set { _electric = value; }
        }
        /// <summary>
        /// 速度
        /// </summary>		
        private decimal _speed;
        public decimal SPEED
        {
            get { return _speed; }
            set { _speed = value; }
        }
        /// <summary>
        /// 方位
        /// </summary>		
        private decimal _direction;
        public decimal DIRECTION
        {
            get { return _direction; }
            set { _direction = value; }
        }
        /// <summary>
        /// 上报时间
        /// </summary>		
        private DateTime _sbtime;
        public DateTime SBTIME
        {
            get { return _sbtime; }
            set { _sbtime = value; }
        }
        /// <summary>
        /// 备注
        /// </summary>		
        private string _note;
        public string NOTE
        {
            get { return _note; }
            set { _note = value; }
        }
        /// <summary>
        /// ORGNO
        /// </summary>		
        private string _orgno;
        public string ORGNO
        {
            get { return _orgno; }
            set { _orgno = value; }
        }
        /// <summary>
        /// 上报日期
        /// </summary>		
        private DateTime _sbdate;
        public DateTime SBDATE
        {
            get { return _sbdate; }
            set { _sbdate = value; }
        }
        /// <summary>
        /// 本日上报开始时间
        /// </summary>		
        private DateTime _sbtimebegin;
        public DateTime SBTIMEBEGIN
        {
            get { return _sbtimebegin; }
            set { _sbtimebegin = value; }
        }
        /// <summary>
        /// 本日已巡检长度
        /// </summary>		
        private decimal _patrollength;
        public decimal PATROLLENGTH
        {
            get { return _patrollength; }
            set { _patrollength = value; }
        }
        /// <summary>
        /// 是否超围栏
        /// </summary>		
        private int _isoutrail;
        public int ISOUTRAIL
        {
            get { return _isoutrail; }
            set { _isoutrail = value; }
        }
    }
}
