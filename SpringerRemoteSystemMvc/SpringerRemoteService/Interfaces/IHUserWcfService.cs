using Springer.EntityModel;
using Springer.EntityModel.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace TLW.Project.Springer.SpringerRemoteDataWcfService.Interfaces
{
    [ServiceContract]
    public interface IHUserWcfService
    {
        /// <summary>
        /// 返回电话号码
        /// </summary>
        /// <param name="sn"></param>
        /// <returns></returns>
        [OperationContract]
        string ReturnPhoneStatusWcf(string sn);

        /// <summary>
        /// 注册护林员
        /// </summary>
        /// <param name="sn"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        [OperationContract]
        bool RegisterHUserWcf(string sn, string phone);

        /// <summary>
        ///  注册护林员(string)
        /// </summary>
        /// <param name="sn"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        [OperationContract]
        string RegisterHUserWcfStr(string sn, string phone);

        /// <summary>
        /// 注册护林员[成功==>返回该护林员所设置的参数]
        /// </summary>
        /// <param name="sn">设备</param>
        /// <param name="phone">手机号码</param>
        /// <returns></returns>
        [OperationContract]
        string RegisterHUserWcfStrReturnParams(string sn, string phone);
        /// <summary>
        /// 获取系统参数
        /// </summary>
        /// <param name="sysflag"></param>
        /// <returns></returns>
        [OperationContract]
        Dictionary<string, string> GetDicSysParaments(string sysflag);


        /// <summary>
        /// 获取系统参数
        /// </summary>
        /// <param name="sysflag"></param>
        /// <param name="paramflag"></param>
        /// <returns></returns>
        [OperationContract]
        // [WebInvoke(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string GetJsonSysParaments(string sysflag, string paramflag);

        /// <summary>
        /// 获取系统参数jsonstr
        /// </summary>
        /// <param name="sysflag">系统标识</param>
        /// <param name="paramflag">参数类型</param>
        /// <param name="phone">电话</param>
        /// <returns>json字符串</returns>
        [OperationContract]
        string GetJsonSysParamentsBy(string sysflag, string paramflag, string phone);

        /// <summary>
        /// 上报实时数据
        /// </summary>
        /// <param name="phone">电话</param>
        /// <param name="lon">经度</param>
        /// <param name="lat">经度</param>
        /// <param name="elec">电量</param>
        /// <param name="cjtime">采集时间</param>
        /// <returns></returns>
        [OperationContract]
        //bool AddRealData(string phone, decimal lon, decimal lat, decimal elec, DateTime cjtime);
        bool AddRealData(string phone, string lon, string lat, string elec, string cjtime);

        /// <summary>
        /// 上报实时数据1
        /// </summary>
        /// <param name="phone">电话</param>
        /// <param name="lon">经度</param>
        /// <param name="lat">经度</param>
        /// <param name="elec">电量</param>
        /// <param name="cjtime">采集时间</param>
        /// <returns></returns>
        [OperationContract]
        bool AddRealDataTest(string phone, string lon, string lat, string elec, string cjtime);

        /// <summary>
        /// 增加报警信息
        /// </summary>
        /// <param name="lon">纬度</param>
        /// <param name="lat">经度</param>
        /// <param name="height">高度</param>
        /// <param name="phone">电话</param>
        /// <param name="alarmtime">报警时间</param>
        /// <returns></returns>
        [OperationContract]
        bool AddAlarmInfo(string lon, string lat, string height, string phone, string alarmtime);

        /// <summary>
        /// 上报数据Add
        /// </summary>
        /// <param name="datalist">上报数据详细</param>
        /// <param name="infotype">上报信息类型</param>
        /// <param name="title">上报标题</param>
        /// <param name="phone">上报电话</param>
        /// <param name="fileupload">上报图片或视频</param>
        /// <returns></returns>
        [OperationContract]
        bool AddReportData(IList<DataInfoModel> datalist, string infotype, string title, string phone, FileUploadMessageModel fileupload);

        /// <summary>
        /// 上报数据
        /// </summary>
        /// <param name="infotype">上报类型</param>
        /// <param name="reportdescribe">上报描述</param>
        /// <param name="phone">电话号码</param>
        /// <param name="lon">经度</param>
        /// <param name="lat">纬度</param>
        /// <param name="cjtime">上报时间</param>
        /// <param name="base64str">加密文件</param>
        /// <returns></returns>
        [OperationContract]
        bool AddReportDataStr(string infotype, string reportdescribe, string phone, string lon, string lat, string cjtime, string base64str);

        /// <summary>
        /// 上报数据
        /// </summary>
        /// <param name="infotype">上报类型</param>
        /// <param name="reportdescribe">上报描述</param>
        /// <param name="phone">电话号码</param>
        /// <param name="lon">经度</param>
        /// <param name="lat">纬度</param>
        /// <param name="cjtime">上报时间</param>
        /// <param name="type">上报类型 1 为图片 2 为视频 3 为音频</param>
        /// <param name="base64str">加密文件</param>
        /// <returns></returns>
        [OperationContract(Name = "AddReportDataStrByType")]
        bool AddReportDataStr(string infotype, string reportdescribe, string phone, string lon, string lat, string cjtime, string type, string base64str);
        /// <summary>
        /// 采集数据Add
        /// </summary>
        /// <param name="datalist">采集数据详细</param>
        /// <param name="infotype">采集信息类型</param>
        /// <param name="title">上报标题</param>
        /// <param name="phone">上报电话</param>
        /// <param name="fileupload">采集图片或视频</param>
        /// <returns></returns>
        [OperationContract]
        bool AddCollectData(IList<DataInfoModel> datalist, string infotype, string title, string phone, FileUploadMessageModel fileupload);


        /// <summary>
        /// 采集数据Addstr
        /// </summary>
        /// <param name="datalistJsonstr">经纬度json字符串</param>
        /// <param name="infotype">采集类型</param>
        /// <param name="describe">采集描述</param>
        /// <param name="cjtime">采集时间</param>
        /// <param name="phone">电话号码</param>
        /// <param name="base64str">加密文件</param>
        /// <returns></returns>
        [OperationContract]
        bool AddCollectDataStr(string datalistJsonstr, string infotype, string describe, string cjtime, string phone, string base64str);

        /// <summary>
        /// 采集数据Addstr
        /// </summary>
        /// <param name="datalistJsonstr">经纬度json字符串</param>
        /// <param name="infotype">采集类型</param>
        /// <param name="describe">采集描述</param>
        /// <param name="cjtime">采集时间</param>
        /// <param name="phone">电话号码</param>
        /// <param name="type">上报类型 1 为图片 2 为视频 3 为音频</param>
        /// <param name="base64str">加密文件</param>
        /// <returns></returns>
        [OperationContract(Name = "AddCollectDataStrByType")]
        bool AddCollectDataStr(string datalistJsonstr, string infotype, string describe, string cjtime, string phone, string type, string base64str);

        /// <summary>
        /// 根据DICTFLAG获取数据字典类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [OperationContract]
        List<T_SYS_DICTModel> GetModelList(string type);

        /// <summary>
        /// 获取数据列表（字典类型）
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        [OperationContract]
        Dictionary<string, string> GetModelListDic(string type);

        /// <summary>
        /// 获取下拉类型 jsonstr
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [OperationContract]
        string GetJsonModelListDic(string type);

        /// <summary>
        /// 获取通讯录
        /// </summary>
        /// <param name="top">个数 空值全选</param>
        /// <returns></returns>
        [OperationContract]
        List<TXLModel> GetTxlModelList(string top);


        /// <summary>
        /// 获取护林员通讯录
        /// </summary>
        /// <param name="phone">电话号码</param>
        /// <returns></returns>
        [OperationContract]
        List<HLYTXLModel> GetHLYTXLModelList(string phone);

        /// <summary>
        /// Feature手机对接
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        [OperationContract]
        bool FeaturePhoneUploadGPS(string data);

        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="lng"></param>
        /// <param name="lat"></param>
        /// <returns></returns>
        [OperationContract]
        string GetAddreass(decimal lng, decimal lat);


        /// <summary>
        /// 获取任务列表
        /// </summary>
        /// <param name="sn">设备号</param>
        /// <param name="phonenumber">手机号码 </param>
        /// <param name="starttime">任务开始时间</param>
        /// <param name="endtime">任务结束时间</param>
        /// <param name="status">任务状态</param>
        /// <returns>jsonstr</returns>
        [OperationContract]
        string GetTaskList(string sn,string phonenumber,string starttime,string endtime,int status);

        /// <summary>
        /// 护林员接受任务
        /// </summary>
        /// <param name="phonenumber">手机号码</param>
        /// <param name="taskinfoid">任务ID</param>
        /// <param name="accepttime">接受时间</param>
        /// <returns>jsonstr</returns>
        [OperationContract]
        string HlyAccepttask(string phonenumber, string taskinfoid, string accepttime);

        /// <summary>
        /// 护林员反馈信息
        /// </summary>
        /// <param name="phonenumber">手机号码</param>
        /// <param name="taskinfoid">任务序号</param>
        /// <param name="feedbacktime">反馈时间</param>
        /// <param name="feedbackcontent">反馈内容</param>
        /// <returns></returns>
        [OperationContract]
        string HlyFeedback(string phonenumber, string taskinfoid, string feedbacktime, string feedbackcontent);
        //#region 上传流

        //[MessageContract]
        //public class FileUploadMessageModel
        //{
        //    /// <summary>
        //    /// 上传文件名
        //    /// </summary>
        //    [MessageHeader(MustUnderstand = true)]
        //    public string FileName;

        //    /// <summary>
        //    /// 上传文件描述
        //    /// </summary>
        //    [MessageHeader(MustUnderstand = true)]
        //    public string FileDescripe;

        //    /// <summary>
        //    ///  上传文件类型
        //    /// </summary>
        //    [MessageHeader(MustUnderstand = true)]
        //    public string FileType;

        //    /// <summary>
        //    /// 上传文件流
        //    /// </summary>
        //    [MessageBodyMember(Order = 1)]
        //    public Stream FileData;

        //}

        //#endregion
    }
}
