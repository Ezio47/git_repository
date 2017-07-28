using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLW.AH.Application.Interfance;
using TLW.AH.Business.DBUtility;
using TLW.AH.Business.ServiceIDal;

namespace TLW.AH.Application.Service
{
    public class VideoOriginalApplicationService : IVideoOriginalApplicationService
    {
        #region Identity
        [Dependency]
        public IVideoOriginalService iVideoOriginalService { get; set; }//视频原始信息 
        #endregion Identity

        #region 方法主体

        /// <summary>
        /// 视频原始数据增加
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool AddVideoOriginalData(T_VIDEO_ORIGINAL info)
        {
            var model = iVideoOriginalService.AddVideoOriginal(info);
            if (model != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
