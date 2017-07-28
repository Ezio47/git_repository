using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel.ExtenAttribute
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Event | AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Class)]
    public class DicTypeAttribute : Attribute
    {
        private string _displayName;
        //public static readonly DisplayNameAttribute Default = new DisplayNameAttribute();

        /// <summary>
        /// 
        /// </summary>
        public DicTypeAttribute()
            : this(string.Empty)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="displayName"></param>
        public DicTypeAttribute(string displayName)
        {
            this._displayName = displayName;
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual string DisplayName
        {
            get
            {
                return this.DisplayNameValue;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        protected string DisplayNameValue
        {
            get
            {
                return this._displayName;
            }
            set
            {
                this._displayName = value;
            }
        }
    }
}
