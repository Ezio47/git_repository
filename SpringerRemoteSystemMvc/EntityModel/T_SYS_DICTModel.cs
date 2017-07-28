using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Springer.EntityModel
{
    /// <summary>
    /// T_SYS_DICT:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class T_SYS_DICTModel
    {
        public T_SYS_DICTModel()
        { }
        #region Model
        private int _dictid;
        private string _dictypeid;
        private string _dictname;
        private string _dictvalue;
        private string _sysflag;
        private string _standby1;
      
        /// <summary>
        /// 
        /// </summary>
        public int DICTID
        {
            set { _dictid = value; }
            get { return _dictid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DICTTYPEID
        {
            set { _dictypeid = value; }
            get { return _dictypeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DICTNAME
        {
            set { _dictname = value; }
            get { return _dictname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DICTVALUE
        {
            set { _dictvalue = value; }
            get { return _dictvalue; }
        }
        /// <summary>
        /// 为0表示共用的
        /// </summary>
        public string SYSFLAG
        {
            set { _sysflag = value; }
            get { return _sysflag; }
        }

        /// <summary>
        /// 备用1（0 点 1线 2面）
        /// </summary>
        public string STANDBY1
        {
            set { _standby1 = value; }
            get { return _standby1; }
        }
        #endregion Model
    }
}
