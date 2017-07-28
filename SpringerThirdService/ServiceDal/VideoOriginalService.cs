using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLW.AH.Business.DBUtility;
using TLW.AH.Business.ServiceIDal;

namespace TLW.AH.Business.ServiceDal
{
    /// <summary>
    /// 视频原始数据
    /// </summary>
    public class VideoOriginalService : BaseService<T_VIDEO_ORIGINAL>, IVideoOriginalService
    {
        #region Identity
        private DbSet<T_VIDEO_ORIGINAL> VideoOriginalDb;
        public VideoOriginalService(DbContext jdContext)
            : base(jdContext)
        {
            this.VideoOriginalDb = base.Context.Set<T_VIDEO_ORIGINAL>();
        }
        #endregion Identity


        #region 方法主体

        /// <summary>
        /// 增加视频原始记录
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public T_VIDEO_ORIGINAL AddVideoOriginal(T_VIDEO_ORIGINAL info)
        {
            var model = this.Insert(info);
            return model;

        }
        #endregion
    }
}
