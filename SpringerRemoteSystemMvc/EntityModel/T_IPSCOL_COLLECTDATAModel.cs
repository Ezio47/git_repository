using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Springer.EntityModel
{

    /// <summary>
    /// T_IPSCOL_COLLECTDATA:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class T_IPSCOL_COLLECTDATAModel
    {
        public T_IPSCOL_COLLECTDATAModel()
        { }
        #region Model
        private long _collectid;
        private int _hid;
        private string _systypevalue;
        private string _address;
        private DateTime? _collecttime;
        private string _collectname;
        private int? _isdeal;
        private string _note;
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
        public int HID
        {
            set { _hid = value; }
            get { return _hid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SYSTYPEVALUE
        {
            set { _systypevalue = value; }
            get { return _systypevalue; }
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
        public DateTime? COLLECTTIME
        {
            set { _collecttime = value; }
            get { return _collecttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string COLLECTNAME
        {
            set { _collectname = value; }
            get { return _collectname; }
        }
        /// <summary>
        /// 0为未处理1为已处理
        /// </summary>
        public int? ISDEAL
        {
            set { _isdeal = value; }
            get { return _isdeal; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NOTE
        {
            set { _note = value; }
            get { return _note; }
        }
        #endregion Model

    }
}
