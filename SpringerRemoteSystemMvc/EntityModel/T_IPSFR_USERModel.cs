using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Springer.EntityModel
{
    [Serializable]
    public partial class T_IPSFR_USERModel
    {

        public T_IPSFR_USERModel()
        { }

        #region Model
        private int _hid;
        private string _hname;
        private string _sn;
        private string _phone;
        private int? _sex;
        private DateTime? _birth;
        private int? _onstate;
        private string _byorgno;
        private string _mobileparamlist;
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
        public string HNAME
        {
            set { _hname = value; }
            get { return _hname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SN
        {
            set { _sn = value; }
            get { return _sn; }
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
        /// 0为男 1为女 2为其他
        /// </summary>
        public int? SEX
        {
            set { _sex = value; }
            get { return _sex; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? BIRTH
        {
            set { _birth = value; }
            get { return _birth; }
        }
        /// <summary>
        /// 0为兼职1为固
        /// </summary>
        public int? ONSTATE
        {
            set { _onstate = value; }
            get { return _onstate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BYORGNO
        {
            set { _byorgno = value; }
            get { return _byorgno; }
        }

        /// <summary>
        /// 手机端参数
        /// </summary>
        public string MOBILEPARAMLIST
        {
            set { _mobileparamlist = value; }
            get { return _mobileparamlist; }
        }
        #endregion Model
    }
}
