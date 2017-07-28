using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLW.AH.Business.DBUtility;

namespace TLW.AH.Business.ServiceIDal
{
    /// <summary>
    /// 视频原始数据
    /// </summary>
    public interface IVideoOriginalService : IBaseService<T_VIDEO_ORIGINAL>
    {
        /// <summary>
        /// 增加视频原始记录
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        T_VIDEO_ORIGINAL AddVideoOriginal(T_VIDEO_ORIGINAL info);
    }
}
