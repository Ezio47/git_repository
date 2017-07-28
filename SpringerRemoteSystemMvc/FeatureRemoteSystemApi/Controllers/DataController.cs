using log4net;
using Springer.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;
using TLW.Project.Springer.SpringerRemoteDataWcfService.Services;

namespace FeatureRemoteSystemApi.Controllers
{
    [RoutePrefix("api/Data")]
    public class DataController : ApiController
    {
        private readonly ILog logs = LogHelper.GetInstance();//log  

        #region FeatureWcfPhone
        /// <summary>
        /// Feature手机对接
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns>
        ///1)	找不到设备串号
        ///2)	设备串号未授权
        ///3)	时间格式不正确
        ///4)	经度、纬度格式不正确
        ///5)	图片不是Base64编码      
        ///6)	ok
        /// </returns>
        [HttpGet, Route("FeaturePhoneUploadGPSStr")]
        public string FeaturePhoneUploadGPSStr(string data)
        {
            bool bb = false;
            var arr = data.Split(',');//获取参数
            if (arr.Count() > 4)
            {
                string phoneMac = arr[0];//机器设备号码
                string phoneTime = arr[1];//时间
                if (string.IsNullOrEmpty(phoneTime))
                {
                    return "3";
                }
                string lon = arr[3];//经度
                string lat = arr[2];//纬度
                if (string.IsNullOrEmpty(lon) || string.IsNullOrEmpty(lat))
                {
                    return "4";
                }
                string type = arr[4];//类型
                if (type == "0")
                {
                    var service = new SpringerRemoteSystemService();
                    string phoneNo = service.ReturnPhoneStatusWcf(phoneMac.Trim());//手机号码
                    if (!string.IsNullOrEmpty(phoneNo))
                    {
                        bb = service.AddRealData(phoneNo, lon, lat, "100", phoneTime);
                        if (bb == false)
                        {
                            logs.InfoFormat("{0}Feature手机实时坐标上传失败", phoneNo);
                        }
                    }
                    else
                    {
                        return "1";
                    }
                }
            }
            if (bb)
            {
                return "ok";
            }
            else
            {
                return "error";
            }
        }

        /// <summary>
        /// 返回string
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpGet, Route("FeaturePhoneUploadGPS")]
        public HttpResponseMessage FeaturePhoneUploadGPS(string data)
        {
            //data=设备串号,时间,经度,纬度,多媒体类型,多媒体内容,多媒体标题,多媒体描述，超界标志，报警类型，电量
            //data=设备串号,时间,经度,纬度,多媒体类型,多媒体内容,多媒体描述，报警类型，电量
            logs.InfoFormat("FeaturePhoneUploadGPS接收数据{0}", data);
            bool bb = false;
            string str = "";
            string reportdescribe = "";//多媒体描述
            string phoneNo = "";//电话号码
            var arr = data.Split(',');//获取参数
            if (arr.Count() > 4)
            {
                string phoneMac = arr[0];//机器设备号码
                if (string.IsNullOrEmpty(phoneMac))
                {
                    str = "1";
                    logs.Error("机器设备号码为空");
                    return ToGetStr(str);
                }
                string phoneTime = arr[1];//时间
                //logs.InfoFormat("编码时间{0}", phoneTime);
                if (string.IsNullOrEmpty(phoneTime))
                {
                    str = "3";
                    logs.Error("时间格式不正确，为空");
                    return ToGetStr(str);
                }
                string lon = arr[2];//经度
                string lat = arr[3];//纬度
                string elec = arr[8];//电量
                if (string.IsNullOrEmpty(lon) || string.IsNullOrEmpty(lat))
                {
                    str = "4";
                    logs.Error("经纬度为空");
                    return ToGetStr(str);
                }
                string type = arr[4];//类型 多媒体类型：0-纯GPS，1-图片，2-视频；
                var service = new SpringerRemoteSystemService();
                phoneNo = service.ReturnPhoneStatusWcf(phoneMac.Trim());//手机号码
                if (string.IsNullOrEmpty(phoneNo))
                {
                    str = "1";
                    logs.ErrorFormat("{0}找不到设备串号", phoneMac);
                    return ToGetStr(str);
                }
                if (type == "0")//实时数据上报 纯GPS传
                {
                    bb = service.AddRealData(phoneNo, lon, lat, elec, phoneTime);//上报实时数据
                    if (bb == false)
                    {
                        logs.InfoFormat("{0}Feature手机实时坐标上传失败", phoneNo);
                    }
                }
                else if (type == "1")//图片 数据上报
                {
                    string base64strContent = arr[5].Trim();//多媒体内容：纯GPS传"",图片用Base64位编码，视频待定；
                    string base64reportdescribe = arr[6].Trim();//多媒体描述：用Base64编码，纯GPS传""；
                    string alarmtype = arr[7].Trim();//报警类型
                    if (string.IsNullOrEmpty(base64strContent))
                    {
                        str = "5";
                        logs.ErrorFormat("{0}图片为空", phoneNo);
                        return ToGetStr(str);
                    }
                    if (string.IsNullOrEmpty(alarmtype))
                    {
                        str = "6";
                        logs.ErrorFormat("{0}多媒体类型为1-图片，报警类型不可为空", phoneNo);
                        return ToGetStr(str);
                    }
                    if (!string.IsNullOrEmpty(base64reportdescribe))
                    {
                        reportdescribe = Base64Decode(base64reportdescribe);//多媒体描述解密
                    }
                    bb = service.AddReportDataStr(alarmtype, reportdescribe, phoneNo, lon, lat, phoneTime, base64strContent);
                    if (bb == false)
                    {
                        logs.InfoFormat("{0}Feature手机图片数据上传失败", phoneNo);
                    }
                }
            }
            if (bb)
            {
                str = "ok";
                return ToGetStr(str);
            }
            else
            {
                str = "error";
                logs.ErrorFormat("{0}上传实时数据库失败", phoneNo);
                return ToGetStr(str);
            }
        }


        /// <summary>
        /// 返回string
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost, Route("FeaturePhoneUploadGPSPost")]
        public HttpResponseMessage FeaturePhoneUploadGPSPost([FromBody] DataPost datapost)
        {
            //data=设备串号,时间,经度,纬度,多媒体类型,多媒体内容,多媒体标题,多媒体描述，超界标志，报警类型，电量
            //data=设备串号,时间,经度,纬度,多媒体类型,多媒体内容,多媒体描述，报警类型，电量
            logs.InfoFormat("FeaturePhoneUploadGPSPost接收数据{0}", datapost.data);
            bool bb = false;
            string str = "";
            string reportdescribe = "";//多媒体描述
            string phoneNo = "";//电话号码
            if (string.IsNullOrEmpty(datapost.data))
            {
                str = "error";
                logs.ErrorFormat("传参数为空");
                return ToGetStr(str);
            }
            var arr = datapost.data.Split(',');//获取参数
            if (arr.Count() > 4)
            {
                string phoneMac = arr[0];//机器设备号码
                string phoneTime = arr[1];//时间
                //logs.InfoFormat("编码时间{0}", phoneTime);
                if (string.IsNullOrEmpty(phoneTime))
                {
                    str = "3";
                    logs.Error("时间格式不正确，为空");
                    return ToGetStr(str);
                }
                string lon = arr[2];//经度
                string lat = arr[3];//纬度
                string elec = arr[8];//电量
                if (string.IsNullOrEmpty(lon) || string.IsNullOrEmpty(lat))
                {
                    str = "4";
                    logs.Error("经纬度为空");
                    return ToGetStr(str);
                }
                string type = arr[4];//类型 多媒体类型：0-纯GPS，1-图片，2-视频；
                var service = new SpringerRemoteSystemService();
                phoneNo = service.ReturnPhoneStatusWcf(phoneMac.Trim());//手机号码
                if (string.IsNullOrEmpty(phoneNo))
                {
                    str = "1";
                    logs.ErrorFormat("{0}找不到设备串号", phoneMac);
                    return ToGetStr(str);
                }
                if (type == "0")//实时数据上报 纯GPS传
                {
                    bb = service.AddRealData(phoneNo, lon, lat, elec, phoneTime);//上报实时数据
                    if (bb == false)
                    {
                        logs.InfoFormat("{0}Feature手机实时坐标上传失败", phoneNo);
                    }
                }
                else if (type == "1")//图片 数据上报
                {
                    string base64strContent = arr[5].Trim();//多媒体内容：纯GPS传"",图片用Base64位编码，视频待定；
                    string base64reportdescribe = arr[6].Trim();//多媒体描述：用Base64编码，纯GPS传""；
                    string alarmtype = arr[7].Trim();//报警类型
                    if (string.IsNullOrEmpty(base64strContent))
                    {
                        str = "5";
                        logs.ErrorFormat("{0}图片为空", phoneNo);
                        return ToGetStr(str);
                    }
                    if (string.IsNullOrEmpty(alarmtype))
                    {
                        str = "6";
                        logs.ErrorFormat("{0}多媒体类型为1-图片，报警类型不可为空", phoneNo);
                        return ToGetStr(str);
                    }
                    if (!string.IsNullOrEmpty(base64reportdescribe))
                    {
                        reportdescribe = Base64Decode(base64reportdescribe);//多媒体描述解密
                    }
                    bb = service.AddReportDataStr(alarmtype, reportdescribe, phoneNo, lon, lat, phoneTime, type, base64strContent);
                    if (bb == false)
                    {
                        logs.InfoFormat("{0}Feature手机图片数据上传失败", phoneNo);
                    }
                }
            }
            if (bb)
            {
                str = "ok";
                return ToGetStr(str);
            }
            else
            {
                str = "error";
                logs.ErrorFormat("{0}上传数据库失败", phoneNo);
                return ToGetStr(str);
            }
        }

        /// <summary>
        /// 测试数据
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetStr")]
        public string GetStr()
        {
            return "111";
        }

        public HttpResponseMessage Get()
        {
            logs.InfoFormat("{0}访问", DateTime.Now.ToString());
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent("hello", Encoding.UTF8);
            return response;
        }



        #endregion

        #region Private

        private HttpResponseMessage ToGetStr(string str)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(str, Encoding.UTF8);
            return response;
        }
        /// <summary>  
        /// Base64加密  
        /// </summary>  
        /// <param name="Message"></param>  
        /// <returns></returns>  
        private string Base64Code(string Message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(Message);
            return Convert.ToBase64String(bytes);
        }
        /// <summary>  
        /// Base64解密  
        /// </summary>  
        /// <param name="Message"></param>  
        /// <returns></returns>  
        private string Base64Decode(string Message)
        {
            byte[] bytes = Convert.FromBase64String(Message);
            return Encoding.UTF8.GetString(bytes);
        }
        #endregion


        #region Model
        /// <summary>
        /// post提交数据模型
        /// </summary>
        public class DataPost
        {
            public string data { get; set; }
        }
        #endregion
    }
}
