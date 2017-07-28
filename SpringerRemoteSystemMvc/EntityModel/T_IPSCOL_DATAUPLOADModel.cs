using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Springer.EntityModel
{/// <summary>
    /// T_IPSCOL_DATAUPLOAD:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class T_IPSCOL_DATAUPLOADModel
    {
        public T_IPSCOL_DATAUPLOADModel()
        { }
        #region Model
        private int _collectuploadid;
        private long _collectid;
        private string _uploadurl;
        private string _uploadname;
        private string _uploaddescribe;
        private string _uploadtype;
        /// <summary>
        /// 
        /// </summary>
        public int COLLECTUPLOADID
        {
            set { _collectuploadid = value; }
            get { return _collectuploadid; }
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
