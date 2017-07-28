using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLW.AH.Business.DBUtility;

namespace TLW.AH.Application.Interfance
{
    public interface IVideoOriginalApplicationService
    {
        /// <summary>
        /// 视频原始数据增加
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        bool AddVideoOriginalData(T_VIDEO_ORIGINAL info);
    }
}
