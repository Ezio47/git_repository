using System;
namespace Springer.EntityModel
{
    /// <summary>
    /// T_SYS_ADDREDDBOOK:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class T_SYS_ADDREDDBOOK
    {
        public T_SYS_ADDREDDBOOK()
        { }
        #region Model
        private int _adid;
        private int? _atid;
        private string _orgno;
        private string _adname;
        private string _userjob;
        private string _phone;
        private string _tell;
        private int _orderby = 0;
        /// <summary>
        /// 
        /// </summary>
        public int ADID
        {
            set { _adid = value; }
            get { return _adid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ATID
        {
            set { _atid = value; }
            get { return _atid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ORGNO
        {
            set { _orgno = value; }
            get { return _orgno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ADNAME
        {
            set { _adname = value; }
            get { return _adname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string USERJOB
        {
            set { _userjob = value; }
            get { return _userjob; }
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
        public string Tell
        {
            set { _tell = value; }
            get { return _tell; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ORDERBY
        {
            set { _orderby = value; }
            get { return _orderby; }
        }
        #endregion Model

    }
}

