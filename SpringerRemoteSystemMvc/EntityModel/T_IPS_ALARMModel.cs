using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Springer.EntityModel
{
    /// <summary>
    /// T_IPS_ALARM:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class T_IPS_ALARMModel
    {
        public T_IPS_ALARMModel()
        { }
        #region Model
        private int _alarmid;
        private decimal? _longitude;
        private decimal? _latitude;
        private decimal? _height;
        private string _phone;
        private string _address;
        private DateTime? _alarmtime;
        private string _alarmcontent;
        private int? _alarmstate;
        private string _alarmresult;
        private int? _alarmuserid;
        /// <summary>
        /// 
        /// </summary>
        public int ALARMID
        {
            set { _alarmid = value; }
            get { return _alarmid; }
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
        public string PHONE
        {
            set { _phone = value; }
            get { return _phone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ADDRESS
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ALARMTIME
        {
            set { _alarmtime = value; }
            get { return _alarmtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ALARMCONTENT
        {
            set { _alarmcontent = value; }
            get { return _alarmcontent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ALARMSTATE
        {
            set { _alarmstate = value; }
            get { return _alarmstate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ALARMRESULT
        {
            set { _alarmresult = value; }
            get { return _alarmresult; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ALARMUSERID
        {
            set { _alarmuserid = value; }
            get { return _alarmuserid; }
        }
        #endregion Model

    }
}
