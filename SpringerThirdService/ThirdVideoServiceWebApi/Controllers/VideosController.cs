using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ThirdVideoServiceWebApi.Models;
using Omu.ValueInjecter;
using TLW.AH.Common;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using TLW.AH.Application.Interfance;
using TLW.AH.Business.DBUtility;

namespace TLW.Project.ThirdVideoServiceWebApi.Controllers
{
    /// <summary>
    /// 视频接口对接
    /// </summary>
    [RoutePrefix("api/Videos")]
    public class VideosController : ApiController
    {
        private readonly ILog logs = LogHelper.GetInstance();//log 
        #region Identity
        private readonly IJCFireApplicationService _iJCFireApplicationService = null;
        private readonly IVideoOriginalApplicationService _iVideoOriginalApplicationService = null;

        public VideosController(IJCFireApplicationService iJCFireApplicationService, IVideoOriginalApplicationService iVideoOriginalApplicationService)
        {
            this._iJCFireApplicationService = iJCFireApplicationService;
            this._iVideoOriginalApplicationService = iVideoOriginalApplicationService;
        }
        //[Dependency]
        //public IJCFireApplicationService _iJCFireApplicationService { get; set; }//火情服务

        //[Dependency]
        //public IVideoOriginalApplicationService _iVideoOriginalApplicationService { get; set; }//视频原始记录服务
        #endregion Identity


        /// <summary>
        /// 推送火情信息
        /// </summary>
        /// <param name="model">post 数据</param>
        /// <returns>布尔值</returns>
        [HttpPost, Route("PushFireFromVideo")]
        public bool PushFireFromVideo([FromBody]ExPushAlarmInfo model)
        {
            var ss = JsonConvert.SerializeObject(model);
            logs.InfoFormat("初始化数据为{0}", ss);

            if (model.Params != null)
            {
                if (string.IsNullOrEmpty(model.Params.LATITUE) || string.IsNullOrEmpty(model.Params.LONGTITUE))
                {
                    logs.Error("错误的经纬度数据或者为空");
                    return false;
                }
                if (string.IsNullOrEmpty(model.Params.DEVICEID))
                {
                    logs.Error("视频设备id为空");
                    return false;
                }
                var info = new T_VIDEO_ORIGINAL();
                info.InjectFrom(model.Params);
                // logs.InfoFormat("info==={0}", JsonConvert.SerializeObject(info));
                var bo = _iVideoOriginalApplicationService.AddVideoOriginalData(info);
                if (bo)
                {
                    var equipModel = _iJCFireApplicationService.GetMonitorBasicInfoData(model.Params.DEVICEID.Trim());//视频设备信息
                    if (equipModel != null)
                    {
                        var fire = new JC_FIRE();
                        fire.BYORGNO = equipModel.BYORGNO;
                        fire.FIREFROM = "4";//4 表示视频监控
                        var orgMolde = _iJCFireApplicationService.GetSysOrgByOrgNOData(equipModel.BYORGNO.Trim());//组织机构
                        string orgname = "";
                        if (orgMolde != null)
                        {
                            orgname = orgMolde.ORGNAME;
                        }
                        fire.FIRENAME = DateTime.Now.ToString("yyyy-MM-dd") + orgname + "视频监控火点";
                        fire.FIRETIME = DateTime.Now;//起火时间
                        fire.ZQWZ = equipModel.ADDRESS;//火灾发生地
                        fire.RECEIVETIME = DateTime.Now;//接收时间
                        fire.JD = double.Parse(model.Params.LONGTITUE);
                        fire.WD = double.Parse(model.Params.LATITUE);
                        var bb = _iJCFireApplicationService.AddJcFireData(fire);
                        if (bb)
                        {
                            logs.InfoFormat("视频报警成功，视频设备devid:{0}。", model.Params.DEVICEID);
                            return true;
                        }
                        else
                        {
                            logs.ErrorFormat("视频报警失败，视频设备devid:{0}。", model.Params.DEVICEID);
                            return false;
                        }
                    }
                    else
                    {
                        logs.ErrorFormat("视频设备id{0}未在系统录入，没有检索到。", model.Params.DEVICEID);
                        return false;
                    }
                }
                else
                {
                    logs.ErrorFormat("增加视频原始火情数据失败,数据{0}", JsonConvert.SerializeObject(model));
                    return false;
                }

            }
            else
            {
                logs.Error("参数传值为空，错误");
                return false;
            }

        }



        #region Test
        public class DemoClass
        {
            public string str { get; set; }
        }


        [HttpPost, Route("PushTest")]
        public string PushTest([FromBody]DemoClass demo)
        {
            logs.InfoFormat("初始化数据为{0}", demo.str);
            return demo.str;
        }

        [HttpGet]
        public string GetStr(string txt)
        {
            if (string.IsNullOrEmpty(txt))
            {
                txt = "测试";
            }
            //return txt;
            try
            {
                var equipModel = _iJCFireApplicationService.GetMonitorBasicInfoData(txt);
                return equipModel.ADDRESS;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        [HttpGet, Route("GetBo")]
        public bool GetBo()
        {
            return true;
        }

        [HttpPost, Route("PostBo")]
        public bool PostBo()
        {
            return true;
        }
        #endregion
    }
}
