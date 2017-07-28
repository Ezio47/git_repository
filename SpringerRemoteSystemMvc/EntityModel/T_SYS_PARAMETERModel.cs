using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Springer.EntityModel
{
    /// <summary>
    /// T_SYS_PARAMETER:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class T_SYS_PARAMETERModel
    {
        public T_SYS_PARAMETERModel()
        { }

        #region Model
        private int _paramid;
        private string _paramflag;
        private string _paramname;
        private string _paramvalue;
        private string _parammark;
        private string _sysflag;
        /// <summary>
        /// 
        /// </summary>
        public int PARAMID
        {
            set { _paramid = value; }
            get { return _paramid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PARAMFLAG
        {
            set { _paramflag = value; }
            get { return _paramflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PARAMNAME
        {
            set { _paramname = value; }
            get { return _paramname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PARAMVALUE
        {
            set { _paramvalue = value; }
            get { return _paramvalue; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PARAMMARK
        {
            set { _parammark = value; }
            get { return _parammark; }
        }
        /// <summary>
        /// 为0表示共用的
        /// </summary>
        public string SYSFLAG
        {
            set { _sysflag = value; }
            get { return _sysflag; }
        }
        #endregion Model
    }
}
