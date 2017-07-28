using GeoAPI.Geometries;
using log4net;
using NetTopologySuite.Geometries;
using Newtonsoft.Json;
using Springer.BLL;
using Springer.Common;
using Springer.Common.Utils;
using Springer.EntityModel;
using Springer.EntityModel.Entity;
using Springer.EntityModel.Enum;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.Text;
using TLW.Project.Springer.SpringerRemoteDataWcfService.Interfaces;

namespace TLW.Project.Springer.SpringerRemoteDataWcfService.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public partial class SpringerRemoteSystemService : IHUserWcfService
    {
        //Mutex m = new Mutex(false);
        //  m.WaitOne();
        //finally
        //  {
        //      m.ReleaseMutex();
        //  }
        private readonly T_IPSFR_USERBLL huserbll = new T_IPSFR_USERBLL();//护林员 
        private readonly T_SYS_PARAMETERBLL paramentsbll = new T_SYS_PARAMETERBLL();//系统参数 
        private readonly T_IPS_REALDATABLL realdatabll = new T_IPS_REALDATABLL();//实时数据 
        private readonly T_IPSRPT_REPORTDATABLL reportbll = new T_IPSRPT_REPORTDATABLL();//上报数据
        private readonly T_SYS_DICTBLL dictbll = new T_SYS_DICTBLL();//字典
        private readonly T_IPS_ALARMBLL alarmbll = new T_IPS_ALARMBLL();//报警信息
        private readonly T_IPSCOL_COLLECTDATABLL collectdatabll = new T_IPSCOL_COLLECTDATABLL();//采集数据信息
        private readonly T_SYS_ADDREDDBOOLL txlbll = new T_SYS_ADDREDDBOOLL();//通讯录
        private readonly T_IPSFR_ROUTERAIL_RAILBLL railbll = new T_IPSFR_ROUTERAIL_RAILBLL();//围栏
        private readonly TASK_FEEDBACKBLL tfbbll = new TASK_FEEDBACKBLL();//任务管理 
        private readonly TASK_INFOBLL tibbll = new TASK_INFOBLL();//任务管理
        private readonly ClsJson cj = new ClsJson();

        private readonly ILog logs = LogHelper.GetInstance();//log 

        #region Method


        #region Huser 护林员
        /// <summary>
        /// 返回手机号码
        /// </summary>
        /// <param name="sn">设备号</param>
        /// <returns>返回手机号码</returns>
        public string ReturnPhoneStatusWcf(string sn)
        {
            return huserbll.ReturnPhoneStatus(sn);
        }

        /// <summary>
        /// 注册护林员
        /// </summary>
        /// <param name="sn">设备号</param>
        /// <param name="phone">手机号码</param>
        /// <returns>注册是否成功</returns>
        public bool RegisterHUserWcf(string sn, string phone)
        {
            return huserbll.RegisterHUser(sn, phone);
        }

        /// <summary>
        ///  注册护林员(string)
        /// </summary>
        /// <param name="sn">设备</param>
        /// <param name="phone">手机号码</param>
        /// <returns>返回枚举类型</returns>
        public string RegisterHUserWcfStr(string sn, string phone)
        {
            return huserbll.RegisterHUserStr(sn, phone);
        }

        /// <summary>
        /// 注册护林员[成功==>返回该护林员所设置的参数]
        /// </summary>
        /// <param name="sn">设备</param>
        /// <param name="phone">手机号码</param>
        /// <returns></returns>
        public string RegisterHUserWcfStrReturnParams(string sn, string phone)
        {
            var str = huserbll.RegisterHUserStr(sn, phone);
            if (str == StringEnum.Success.ToString())
            {
                str = GetJsonSysParamentsBy("Springer", "", phone);//护林员参数设置
            }
            return str;
        }


        /// <summary>
        /// 获取护林员通讯录
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <returns>获取通讯录</returns>
        public List<HLYTXLModel> GetHLYTXLModelList(string phone)
        {
            var result = new List<HLYTXLModel>();
            var strwhere1 = " PHONE ='" + phone.Trim() + "'";
            var orgno = huserbll.GetModelList(strwhere1).FirstOrDefault().BYORGNO;

            var strwhere2 = " BYORGNO ='" + orgno.Trim() + "'";
            var list = huserbll.GetModelList(strwhere2);
            if (list.Any())
            {
                foreach (var item in list)
                {
                    if (!string.IsNullOrEmpty(item.PHONE))
                    {
                        var info = new HLYTXLModel();
                        info.HID = info.HID;
                        info.HNAME = item.HNAME;
                        info.PHONE = item.PHONE;
                        info.ORGNO = item.BYORGNO;
                        result.Add(info);
                    }

                }
            }
            return result;
        }

        #endregion

        #region 系统参数

        /// <summary>
        /// 获取系统参数
        /// </summary>
        /// <param name="sysflag">系统标识</param>
        /// <returns>系统配置字典</returns>
        public Dictionary<string, string> GetDicSysParaments(string sysflag)
        {

            return paramentsbll.GetDicSysParaments(sysflag);
        }

        /// <summary>
        /// 获取系统参数jsonstr
        /// </summary>
        /// <param name="sysflag">系统标识</param>
        /// <param name="paramflag">参数类型</param>
        /// <returns>json字符串</returns>
        public string GetJsonSysParaments(string sysflag, string paramflag)
        {
            string str = "";
            var diclist = paramentsbll.GetDicSysParaments(sysflag, paramflag);
            str = JsonConvert.SerializeObject(diclist);
            //str = (new System.Web.Script.Serialization.JavaScriptSerializer()).Serialize(diclist);
            return str;
        }

        /// <summary>
        /// 获取系统参数jsonstr
        /// </summary>
        /// <param name="sysflag">系统标识</param>
        /// <param name="paramflag">参数类型</param>
        /// <param name="phone">电话</param>
        /// <returns>json字符串</returns>
        public string GetJsonSysParamentsBy(string sysflag, string paramflag, string phone)
        {
            string str = "";
            var diclist = paramentsbll.GetDicSysParaments(sysflag, paramflag, phone);
            str = JsonConvert.SerializeObject(diclist);
            return str;
        }
        #endregion

        #region 实时数据上报
        /// <summary>
        /// 上报实时数据
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <param name="lon">经度</param>
        /// <param name="lat">纬度</param>
        /// <param name="elec">电量</param>
        /// <param name="cjtime">采集时间</param>
        /// <returns>是否上传成功</returns>
        public bool AddRealData(string phone, string lon, string lat, string elec, string cjtime)
        {
            if (string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(lon) || string.IsNullOrEmpty(lat) || string.IsNullOrEmpty(cjtime))
            {
                return false;
            }
            var lonfloat = double.Parse(lon);//经度
            var latfloat = double.Parse(lat);//纬度
            //去除非中国坐标
            if (lonfloat < 72.004 || lonfloat > 137.8347 || latfloat < 0.8293 || latfloat > 55.8271)
            {
                logs.ErrorFormat("电话：{0}，错误坐标：经度：{1},纬度：{2}", phone, lonfloat, latfloat);
                return false;
            }


            var model = new T_IPS_REALDATAModel();
            model.PHONE = phone.Trim();
            model.LONGITUDE = decimal.Parse(lon);
            model.LATITUDE = decimal.Parse(lat);
            model.ELECTRIC = decimal.Parse(elec);
            model.SBTIME = DateTime.Parse(cjtime);
            model.ISOUTRAIL = 0;//默认值0 不出围
            var bo = ISFence(phone, lon, lat);
            if (bo)
            {
                model.ISOUTRAIL = 1;//出围
            }

            try
            {
                var i = realdatabll.Add(model);
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    logs.Error(string.Format("实时数据上报错误，电话号码{0}，经度{1}纬度{2}", model.PHONE, model.LATITUDE, model.LONGITUDE));
                    return false;
                }
            }
            catch (Exception ex)
            {
                logs.Error(ex.Message);
                return false;
            }


        }

        /// <summary>
        /// 判断是否出围
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <param name="lon">经度</param>
        /// <param name="lat">纬度</param>
        /// <returns>是否出围</returns>
        private bool ISFence(string phone, string lon, string lat)
        {
            var usermodel = railbll.GetHUserModel(phone);
            if (usermodel != null)
            {
                var bo = railbll.ExistROUTERAIL(usermodel.HID, 1);//判断是否存在围栏数据
                if (bo)
                {
                    //where HID=5 and ROADTYPE =1 order by  ORDERBY desc
                    string strwhere = " HID=" + usermodel.HID + " AND ROADTYPE = 1";
                    string orderstr = " ORDERBY desc ";
                    var raillist = railbll.GetROUTERAILDataList(0, strwhere, orderstr);//选出围栏数据
                    if (raillist.Any())
                    {
                        List<Coordinate> coArr = new List<Coordinate>();
                        foreach (var item in raillist)
                        {
                            Coordinate co = new Coordinate() { X = Convert.ToDouble(item.LONGITUDE), Y = Convert.ToDouble(item.LATITUDE) };
                            coArr.Add(co);
                        }
                        Coordinate cotest = new Coordinate() { X = Convert.ToDouble(lon), Y = Convert.ToDouble(lat) };
                        IPoint point = new Point(cotest);
                        try
                        {
                            var fencebol = railbll.Fence(coArr, point);//true 表示出围，false 表示未出围
                            if (fencebol)
                            {
                                //T_IPSFR_ROUTERAIL_RAILModel info = new T_IPSFR_ROUTERAIL_RAILModel();//出围数据模型
                                //info.HID = usermodel.HID;
                                //info.LONGITUDE = decimal.Parse(lon);
                                //info.LONGITUDE = decimal.Parse(lat);
                                //info.SBTIME = DateTime.Parse(cjtime);
                                //railbll.Add(info);
                                logs.InfoFormat("{0}判断出围数据出围", phone);
                                return true;//出围

                            }
                            else
                            {
                                return false;//未出围
                            }
                        }
                        catch (Exception ex)
                        {
                            logs.Error(ex.Message);
                            logs.ErrorFormat("{0}判断出围数据错误", phone);
                        }
                    }
                }
            }
            return false;
        }
        #endregion

        #region 实时数据上报
        /// <summary>
        /// 上报实时数据1
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <param name="lon">经度</param>
        /// <param name="lat">经度</param>
        /// <param name="elec">电量</param>
        /// <param name="cjtime">采集时间</param>
        /// <returns>是否成功</returns>
        public bool AddRealDataTest(string phone, string lon, string lat, string elec, string cjtime)
        {
            if (string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(lon) || string.IsNullOrEmpty(lat))
            {
                return false;
            }
            var model = new T_IPS_REALDATAModel();
            model.PHONE = phone.Trim();
            model.LONGITUDE = decimal.Parse(lon);
            model.LATITUDE = decimal.Parse(lat);
            model.ELECTRIC = decimal.Parse(elec);
            model.SBTIME = DateTime.Parse(cjtime);
            var i = realdatabll.Add(model);
            if (i > 0)
            {
                return true;
            }
            else
            {
                logs.Error(string.Format("实时数据上报错误，电话号码{0}，经度{1}纬度{2}", model.PHONE, model.LATITUDE, model.LONGITUDE));
                return false;
            }

        }
        #endregion

        #region 上报数据
        /// <summary>
        /// 上报数据Add
        /// </summary>
        /// <param name="datalist">上报数据详细</param>
        /// <param name="infotype">上报信息类型</param>
        /// <param name="title">上报标题</param>
        /// <param name="phone">上报手机号码</param>
        /// <param name="fileupload">上报图片或视频</param>
        /// <returns></returns>
        public bool AddReportData(IList<DataInfoModel> datalist, string infotype, string title, string phone, FileUploadMessageModel fileupload)
        {
            return reportbll.AddReportData(datalist, infotype, title, phone, fileupload);
        }

        /// <summary>
        /// 上报数据
        /// </summary>
        /// <param name="infotype">上报信息类型</param>
        /// <param name="reportdescribe">上报描述</param>
        /// <param name="phone">手机号码</param>
        /// <param name="lon">经度</param>
        /// <param name="lat">纬度</param>
        /// <param name="cjtime">上报时间</param>
        /// <param name="base64str">Base64加密图片</param>
        /// <returns></returns>
        public bool AddReportDataStr(string infotype, string reportdescribe, string phone, string lon, string lat, string cjtime, string base64str)
        {
            return reportbll.AddReportDataStr(infotype, reportdescribe, phone, lon, lat, cjtime, base64str);
        }

        /// <summary>
        /// 上报数据
        /// </summary>
        /// <param name="infotype">上报信息类型</param>
        /// <param name="reportdescribe">上报描述</param>
        /// <param name="phone">手机号码</param>
        /// <param name="lon">经度</param>
        /// <param name="lat">纬度</param>
        /// <param name="cjtime">上报时间</param>
        /// <param name="type">上传文件类型（1 为照片 2 为视频 3 为音频）</param>
        /// <param name="base64str">Base64加密</param>
        /// <returns></returns>
        public bool AddReportDataStr(string infotype, string reportdescribe, string phone, string lon, string lat, string cjtime, string type, string base64str)
        {
            return reportbll.AddReportDataStr(infotype, reportdescribe, phone, lon, lat, cjtime, base64str, type);
        }

        #endregion

        #region 数据字典
        /// <summary>
        /// 根据DICTFLAG获取数据字典类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<T_SYS_DICTModel> GetModelList(string type)
        {
            string strWhere = " DICTFLAG='" + type.Trim() + "'";
            return dictbll.GetModelList(strWhere);
        }


        /// <summary>
        /// 获取数据列表（字典类型）
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetModelListDic(string type)
        {
            string strWhere = " A.DICTTYPEID='" + type.Trim() + "'";
            return dictbll.GetModelListDic(strWhere);
        }

        /// <summary>
        /// 获取下拉类型 jsonstr
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetJsonModelListDic(string type)
        {
            string str = "";
            string strWhere = " A.DICTTYPEID='" + type.Trim() + "'";
            var diclist = dictbll.GetModelListDicValueList(strWhere);
            str = JsonConvert.SerializeObject(diclist);
            return str;
        }
        #endregion

        #region 报警信息

        /// <summary>
        /// 增加报警信息
        /// </summary>
        /// <param name="lon">纬度</param>
        /// <param name="lat">经度</param>
        /// <param name="height">高度</param>
        /// <param name="phone">手机号码</param>
        /// <param name="alarmtime">报警时间</param>
        /// <returns>是否成功</returns>
        public bool AddAlarmInfo(string lon, string lat, string height, string phone, string alarmtime)
        {
            //string para="name="+name + "&ak=" + ak + "&is_published=" + is_published + "&geotype=" + geotype;
            //string url = "http://api.map.baidu.com/geocoder/v2/?ak=wYCjPb9rxUueQP8xcNwqGLFw&callback=renderReverse&location=39.983424,116.322987&output=json&pois=1";
            T_IPS_ALARMModel model = new T_IPS_ALARMModel();
            model.PHONE = phone.Trim();
            if (string.IsNullOrEmpty(lon))
            {
                lon = "0";
            }
            model.LONGITUDE = decimal.Parse(lon);
            if (string.IsNullOrEmpty(lat))
            {
                lat = "0";
            }
            model.LATITUDE = decimal.Parse(lat);
            if (string.IsNullOrEmpty(height))
            {
                height = "0";
            }
            model.HEIGHT = decimal.Parse(height);
            model.ALARMTIME = Convert.ToDateTime(alarmtime);
            model.ADDRESS = this.GetAddreass(model.LONGITUDE.Value, model.LATITUDE.Value);//由经纬度算出实际地址
            var i = alarmbll.Add(model);
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 获取地址
        /// </summary>
        /// <param name="lng">经度</param>
        /// <param name="lat">纬度</param>
        /// <returns></returns>
        public string GetAddreass(decimal lng, decimal lat)
        {
            string str = alarmbll.GetAddress(lng, lat);
            return str;
        }
        #endregion

        #region 采集数据
        /// <summary>
        /// 采集数据Add
        /// </summary>
        /// <param name="datalist">采集数据详细</param>
        /// <param name="infotype">采集信息类型</param>
        /// <param name="title">上报标题</param>
        /// <param name="phone">上报手机号码</param>
        /// <param name="fileupload">上报图片或视频</param>
        /// <returns>是否成功</returns>
        public bool AddCollectData(IList<DataInfoModel> datalist, string infotype, string title, string phone, FileUploadMessageModel fileupload)
        {
            return collectdatabll.AddCollectData(datalist, infotype, title, phone, fileupload);
        }


        /// <summary>
        /// 采集数据Addstr
        /// </summary>
        /// <param name="datalistJsonstr">json字符串经纬度</param>
        /// <param name="infotype">采集类型</param>
        /// <param name="describe">采集描述</param>
        /// <param name="cjtime">采集时间</param>
        /// <param name="phone">手机电话</param>
        /// <param name="base64str">Base64加密</param>
        /// <returns></returns>
        public bool AddCollectDataStr(string datalistJsonstr, string infotype, string describe, string cjtime, string phone, string base64str)
        {
            return collectdatabll.AddCollectDataStr(datalistJsonstr, infotype, describe, cjtime, phone, base64str);

        }



        /// <summary>
        /// 采集数据Addstr
        /// </summary>
        /// <param name="datalistJsonstr">json字符串经纬度</param>
        /// <param name="infotype">采集类型</param>
        /// <param name="describe">采集描述</param>
        /// <param name="cjtime">采集时间</param>
        /// <param name="phone">手机电话</param>
        /// <param name="type">上传文件类型（1 为照片 2 为视频 3 为音频）</param>
        /// <param name="base64str">Base64加密</param>
        /// <returns></returns>
        public bool AddCollectDataStr(string datalistJsonstr, string infotype, string describe, string cjtime, string phone, string type, string base64str)
        {
            return collectdatabll.AddCollectDataStr(datalistJsonstr, infotype, describe, cjtime, phone, base64str, type);

        }
        #endregion

        #region FeatureWcfPhone
        /// <summary>
        /// Feature手机对接
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public bool FeaturePhoneUploadGPS(string data)
        {
            bool bb = false;
            var arr = data.Split(',');//获取参数
            if (arr.Count() > 0)
            {
                string phoneMac = arr[0];//机器设备号码
                string phoneTime = arr[1];//时间
                string lon = arr[2];//经度
                string lat = arr[3];//纬度
                string type = arr[4];//类型
                if (type == "0")
                {
                    string phoneNo = ReturnPhoneStatusWcf(phoneMac);//手机号码
                    if (!string.IsNullOrEmpty(phoneNo))
                    {
                        bb = AddRealData(phoneNo, lon, lat, "100", phoneTime);
                        if (bb == false)
                        {
                            logs.InfoFormat("{0}Feature手机实时坐标上传失败", phoneNo);
                        }
                    }
                }

            }
            return bb;
        }

        #endregion

        #region 通讯录
        /// <summary>
        /// 获取通讯录
        /// </summary>
        /// <param name="top">个数 空值全选</param>
        /// <returns></returns>
        public List<TXLModel> GetTxlModelList(string top)
        {
            var list = txlbll.GetTXLModelList(top);
            return list;
        }

        #endregion

        #region 任务管理

        /// <summary>
        /// 获取任务列表
        /// </summary>
        /// <param name="sn">设备号</param>
        /// <param name="phonenumber">手机号码</param>
        /// <param name="starttime">任务开始时间</param>
        /// <param name="endtime">任务结束时间</param>
        /// <param name="status">任务状态</param>
        /// <returns></returns>
        public string GetTaskList(string sn,string phonenumber, string starttime, string endtime, int status) 
        {
            Message ms = null;
            if (string.IsNullOrEmpty(phonenumber))
            {
                ms = new Message(false, "手机号码为空！", "");
            }
            else 
            {
                bool isExist = huserbll.Exist(sn, phonenumber);
                if (isExist)
                {
                    string hid = huserbll.getHID(phonenumber);//获得护林员id
                    if (!string.IsNullOrEmpty(hid))
                    {
                        var result = tfbbll.getModelList(hid); //根据hid 反馈表 获取 任务ID列表 "15319"
                        if (result.Count > 0)
                        {
                           string str="";
                            var taskinfomodel = new List<TASK_INFO_Model>();
                            foreach (var item in result)
                            {
                                TASK_INFO_Model m = new TASK_INFO_Model();
                                var list = tibbll.getModelList(item.TASK_INFOID, starttime, endtime, status);//再根据 开始时间、结束时间、状态、任务ID 任务表 得到任务列表  

                                foreach (var li in list)
                                {
                                    m.BYORGNO = li.BYORGNO;
                                    m.TASK_INFOID = li.TASK_INFOID;
                                    m.TASKBEGINTIME = li.TASKBEGINTIME;
                                    m.TASKENDTIME = li.TASKENDTIME;
                                    m.TASKLEVEL = li.TASKLEVEL;
                                    m.TASKTYPE = li.TASKTYPE;
                                    m.TASKTITLE = li.TASKTITLE;
                                    m.TASKCONTENT = li.TASKCONTENT;
                                    m.TASKSTAUTS = li.TASKSTAUTS;
                                    m.TASKSTARTTIME = li.TASKSTARTTIME;
                                    taskinfomodel.Add(m);
                                }                               
                            }
                            //var str1 = ClsJson.EntityToJSON(taskinfomodel);
                            str = JsonConvert.SerializeObject(taskinfomodel);
                            ms = new Message(true, str, "");
                        }
                        else {
                            ms = new Message(false, "该护林员没有任务！", "");
                        }
                    }
                    else
                    {
                        ms = new Message(false, "未查找到该手机号码的护林员！", "");
                    }
                }
                else
                {
                    ms = new Message(false, "手机号码不存在！", "");
                }
            }
         
            return JsonConvert.SerializeObject(ms);
        }

        /// <summary>
        /// 护林员接受任务 
        /// </summary>
        /// <param name="phonenumber">手机号码</param>
        /// <param name="taskinfoid">任务ID</param>
        /// <param name="accepttime">接受时间</param>
        /// <returns>jsonstr</returns>
        public string HlyAccepttask(string phonenumber, string taskinfoid,string accepttime) 
        {
            Message ms = null;
            if (string.IsNullOrEmpty(phonenumber) || string.IsNullOrEmpty(taskinfoid) || string.IsNullOrEmpty(accepttime))
            {
                ms = new Message(false, "传递的参数有空值！", "");
            }
            else
            { 
                bool isExist = huserbll.Exist("", phonenumber);
                if (isExist)
                {
                    string hid = huserbll.getHID(phonenumber);//获得护林员id
                    if (!string.IsNullOrEmpty(hid))
                    {
                        TASK_FEEDBACK_Model m = new TASK_FEEDBACK_Model();
                        m.HID = hid;
                        m.TASK_INFOID = taskinfoid;
                        m.ACCEPTTIME = accepttime;
                        m.FEEDBACKSTATUS = "1";
                        bool bln = tfbbll.UpdateFeedback(m);//修改任务表中任务状态(0:未接受 1:已接受 2:已反馈)和接受时间
                        if (bln)
                            ms = new Message(true, "接受成功！", "");
                        else
                            ms = new Message(false, "接受失败！", "");
                    }
                    else
                    {
                        ms = new Message(false, "未查找到该手机号码的护林员！", "");
                    } 
                    
                }
                else
                {
                    ms = new Message(false, "手机号码不存在！", "");
                }   
            }
            return JsonConvert.SerializeObject(ms);
        }

        /// <summary>
        /// 护林员反馈信息
        /// </summary>
        /// <param name="phonenumber">手机号码</param>
        /// <param name="taskinfoid">任务ID</param>
        /// <param name="feedbacktime">反馈时间</param>
        /// <param name="feedbackcontent">反馈内容</param>
        /// <returns></returns>
        public string HlyFeedback(string phonenumber, string taskinfoid, string feedbacktime, string feedbackcontent)
        {
            Message ms = null;
            if (string.IsNullOrEmpty(phonenumber) || string.IsNullOrEmpty(taskinfoid) || string.IsNullOrEmpty(feedbacktime) || string.IsNullOrEmpty(feedbackcontent))
            {
                ms = new Message(false, "传递的参数有空值！", "");
            }
            else
            {
                bool isExist = huserbll.Exist("", phonenumber);
                if (isExist)
                {
                    string hid = huserbll.getHID(phonenumber);//获得护林员id
                    if (!string.IsNullOrEmpty(hid))
                    {
                        TASK_FEEDBACK_Model m = new TASK_FEEDBACK_Model();
                        m.HID = hid;
                        m.TASK_INFOID = taskinfoid;
                        m.FEEDBACKTIME = feedbacktime;
                        m.FEEDBACKCONTENT = feedbackcontent;
                        m.FEEDBACKSTATUS = "2";//已反馈
                        bool bln = tfbbll.UpdateFeedback(m);
                        if(bln)
                            ms = new Message(true, "反馈成功！", "");
                        else
                            ms = new Message(false, "反馈失败！", "");
                    }
                    else
                    {
                        ms = new Message(false, "未查找到该手机号码的护林员！", "");
                    } 
                }
                else
                {
                    ms = new Message(false, "手机号码不存在！", "");
                }   
                
            }
            return JsonConvert.SerializeObject(ms);
        }
        #endregion

        #endregion
    }
}
