using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Springer.EntityModel
{

    /// <summary>
    /// T_IPSRPT_DATAUPLOAD:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class T_IPSRPT_DATAUPLOADModel
    {
        public T_IPSRPT_DATAUPLOADModel()
        { }
        #region Model
        private int _reportuploadid;
        private long _reportid;
        private string _uploadurl;
        private string _uploadname;
        private string _uploaddescribe;
        private string _uploadtype;

        /// <summary>
        /// 
        /// </summary>
        public int REPORTUPLOADID
        {
            set { _reportuploadid = value; }
            get { return _reportuploadid; }
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
        public string UPLOADURL
        {
            set { _uploadurl = value; }
            get { return _uploadurl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UPLOADNAME
        {
            set { _uploadname = value; }
            get { return _uploadname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UPLOADDESCRIBE
        {
            set { _uploaddescribe = value; }
            get { return _uploaddescribe; }
        }

        public string UPLOADTYPE
        {
            set { _uploadtype = value; }
            get { return _uploadtype; }
        }
        #endregion Model

    }
}
