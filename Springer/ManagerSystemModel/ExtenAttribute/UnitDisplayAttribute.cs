using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel.ExtenAttribute
{
    /// <summary>
    /// 单位
    /// </summary>
    [AttributeUsage(AttributeTargets.Event | AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Class)]
    public class UnitDisplayAttribute : Attribute
    {
        private string _description;//描述
        private string _unit;//单位

        /// <summary>
        /// 单位显示属性
        /// </summary>
        public UnitDisplayAttribute()
        {
        }

        /// <summary>
        /// 单位显示属性
        /// </summary>
        /// <param name="unit">单位</param>
        /// <param name="description">描述</param>
        public UnitDisplayAttribute(string unit, string description)
        {
            this._unit = unit;
            this._description = description;
        }

        /// <summary>
        /// 单位
        /// </summary>
        public virtual string Unit
        {
            get
            {
                return this.UnitValue;
            }
        }

        /// <summary>
        /// 单位值
        /// </summary>
        protected string UnitValue
        {
            get
            {
                return this._unit;
            }
            set
            {
                this._unit = value;
            }
        }

        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description
        {
            get
            {
                return this.DescriptionValue;
            }
        }

        /// <summary>
        /// 描述值
        /// </summary>
        protected string DescriptionValue
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
            }
        }
    }
}
