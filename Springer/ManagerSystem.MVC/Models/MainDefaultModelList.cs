using ManagerSystemModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerSystem.MVC.Models
{
    public class MainDefaultModelList
    {
        /// <summary>
        /// 首页通知
        /// </summary>
        public List<ART_DOCUMENT_Model> NoticeDataList { get; set; }

        /// <summary>
        /// 火险预报
        /// </summary>
        public List<ART_DOCUMENT_Model> FireYBDataList { get; set; }

        /// <summary>
        /// 防火信息
        /// </summary>
        public List<ART_DOCUMENT_Model> FireInfoDataList { get; set; }

        /// <summary>
        /// 热点个数情况
        /// </summary>
        public List<HotInfoModel> HotInfoSum { get; set; }
    }
}