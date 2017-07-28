using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PublicClassLibrary
{
    /// <summary>
    /// GaussSphere 为自定义枚举类型
    /// 高斯投影中所选用的参考椭球
    /// </summary>
    public enum GaussSphere
    {
        /// <summary>
        /// Beijing54
        /// </summary>
        Beijing54,
        /// <summary>
        ///  Xian80
        /// </summary>
        Xian80,
        /// <summary>
        /// WGS84
        /// </summary>
        WGS84,
    }
}
