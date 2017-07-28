using log4net;
using Springer.Common;
using Springer.Common.Utils;
using Springer.DAL;
using Springer.EntityModel;
using Springer.EntityModel.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Springer.BLL
{
    /// <summary>
    /// T_IPSRPT_REPORTDATA
    /// </summary>
    public partial class T_IPSRPT_REPORTDATABLL
    {
        private readonly T_IPSRPT_REPORTDATADAL reportdatadal = new T_IPSRPT_REPORTDATADAL();//上报数据
        private readonly T_IPSRPT_DATADETAILDAL datadetaildal = new T_IPSRPT_DATADETAILDAL();//上报数据明细
        private readonly T_IPSRPT_DATAUPLOADDAL uploaddal = new T_IPSRPT_DATAUPLOADDAL();//上报数据上传明细
        private readonly T_IPSFR_USERDAL huserdal = new T_IPSFR_USERDAL();//护林员
        private readonly T_IPS_REALDATATEMPORARYDAL realdatatempdal = new T_IPS_REALDATATEMPORARYDAL();//实时数据中间表
        private readonly ILog logs = LogHelper.GetInstance();

        public T_IPSRPT_REPORTDATABLL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return reportdatadal.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int HID, long REPORTID)
        {
            return reportdatadal.Exists(HID, REPORTID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(T_IPSRPT_REPORTDATAModel model)
        {
            return reportdatadal.Add(model);
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long REPORTID)
        {

            return reportdatadal.Delete(REPORTID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int HID, long REPORTID)
        {

            return reportdatadal.Delete(HID, REPORTID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string REPORTIDlist)
        {
            return reportdatadal.DeleteList(Maticsoft.Common.PageValidate.SafeLongFilter(REPORTIDlist, 0));
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public T_IPSRPT_REPORTDATAModel GetModel(long REPORTID)
        {

            return reportdatadal.GetModel(REPORTID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public T_IPSRPT_REPORTDATAModel GetModelByCache(long REPORTID)
        {

            string CacheKey = "T_IPSRPT_REPORTDATAModel-" + REPORTID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = reportdatadal.GetModel(REPORTID);
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (T_IPSRPT_REPORTDATAModel)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return reportdatadal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return reportdatadal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<T_IPSRPT_REPORTDATAModel> GetModelList(string strWhere)
        {
            DataSet ds = reportdatadal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<T_IPSRPT_REPORTDATAModel> DataTableToList(DataTable dt)
        {
            List<T_IPSRPT_REPORTDATAModel> modelList = new List<T_IPSRPT_REPORTDATAModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                T_IPSRPT_REPORTDATAModel model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = reportdatadal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return reportdatadal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return reportdatadal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return reportdatadal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod
        #region  ExtensionMethod

        /// <summary>
        /// 上报数据Add
        /// </summary>
        /// <param name="datalist">上报数据详细</param>
        /// <param name="infotype">上报信息类型</param>
        /// <param name="title">上报标题</param>
        /// <param name="phone">上报电话</param>
        /// <param name="fileupload">上报图片或视频</param>
        /// <returns></returns>
        public bool AddReportData(IList<DataInfoModel> datalist, string infotype, string title, string phone, FileUploadMessageModel fileupload)
        {
            string strwhere = string.Empty;
            if (string.IsNullOrEmpty(phone) || datalist.Count == 0 || string.IsNullOrEmpty(infotype))
            {
                return false;
            }
            try
            {
                strwhere = " PHONE='" + phone.Trim() + "'";
                var reportdata = new T_IPSRPT_REPORTDATAModel();
                reportdata.COLLECTNAME = title;//上报标题
                reportdata.REPORTTIME = DateTime.Now;//时间
                reportdata.SYSTYPEVALUE = infotype;//类型值
                var userlist = huserdal.GetHuserDataList(1, strwhere, "");
                if (userlist.Count < 0)
                {
                    return false;
                }
                reportdata.HID = userlist[0].HID;//护林员id
                var i = reportdatadal.Add(reportdata);//追加上报数据
                if (i > 0)
                {
                    if (datalist.Count > 0)
                    {
                        foreach (var data in datalist)
                        {
                            T_IPSRPT_DATADETAILModel model = new T_IPSRPT_DATADETAILModel();
                            model.LONGITUDE = data.lon;
                            model.LATITUDE = data.lat;
                            model.HEIGHT = data.height;
                            model.SBTIME = Convert.ToDateTime(data.cjtime);
                            model.DIRECTION = data.dir;
                            model.REPORTID = i;
                            datadetaildal.Add(model);//追加上报详细表
                        }
                    }
                    if (fileupload != null)//上传图片或者视频
                    {
                        UpLoadCommon com = new UpLoadCommon();
                        string uploadFolder = ConfigurationManager.AppSettings["SpringeruploadImagePath"];
                        if (fileupload.FileType == "1")//上传类型为视频
                        {
                            uploadFolder = ConfigurationManager.AppSettings["SpringeruploadVideoPath"];
                            if (string.IsNullOrEmpty(uploadFolder))
                            {
                                uploadFolder = "~/SpringerUploadVideo/";
                            }
                        }
                        else//上传为图片
                        {
                            if (string.IsNullOrEmpty(uploadFolder))
                            {
                                uploadFolder = "~/SpringerUploadImage/";
                            }
                        }
                        string filePath = "";//文件相对路径
                        filePath = com.UploadFile(fileupload, uploadFolder);
                        if (!string.IsNullOrEmpty(filePath))
                        {
                            T_IPSRPT_DATAUPLOADModel model = new T_IPSRPT_DATAUPLOADModel();//上传文件表
                            model.REPORTID = i;
                            model.UPLOADNAME = fileupload.FileName;
                            model.UPLOADURL = filePath;
                            model.UPLOADDESCRIBE = fileupload.FileDescripe;
                            uploaddal.Add(model);
                        }
                    }
                    return true;
                }
                else
                {
                    logs.ErrorFormat("{0}上报数据失败,类型值{1}", phone, infotype);
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
        /// 上报数据
        /// </summary>
        /// <param name="infotype">数据上报类型</param>
        /// <param name="reportdescribe">上报描述</param>
        /// <param name="phone">电话</param>
        /// <param name="lon">经度</param>
        /// <param name="lat">纬度</param>
        /// <param name="cjtime">上报时间</param>
        /// <param name="base64str">加密文件</param>
        /// <returns></returns>
        public bool AddReportDataStr(string infotype, string reportdescribe, string phone, string lon, string lat, string cjtime, string base64str, string type = "1")
        {
            //logs.Info("infotype==" + infotype + "reportdescribe==" + reportdescribe + "phone=" + phone + "lon=" + lon + "lat=" + lat + "cjtime=" + cjtime + "base64str=" + base64str);
            string strwhere = string.Empty;
            if (string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(infotype) || string.IsNullOrEmpty(lon) || string.IsNullOrEmpty(lat) || string.IsNullOrEmpty(base64str))
            {
                logs.Error(phone + "===上报数据有缺失,数据上报失败");
                return false;
            }
            try
            {
                strwhere = " PHONE='" + phone.Trim() + "'";
                var reportdata = new T_IPSRPT_REPORTDATAModel();
                reportdata.COLLECTNAME = reportdescribe;//上报描述
                if (string.IsNullOrEmpty(cjtime))
                {
                    reportdata.REPORTTIME = DateTime.Now;//时间
                }
                else
                {
                    reportdata.REPORTTIME = Convert.ToDateTime(cjtime);//DateTime.Now;//时间
                }
                reportdata.SYSTYPEVALUE = infotype;//类型值
                var userlist = huserdal.GetHuserDataList(1, strwhere, "");
                if (userlist.Count < 0)
                {
                    return false;
                }
                reportdata.HID = userlist[0].HID;//护林员id
                var alarmbll = new T_IPS_ALARMBLL();
                if (lon == "0" || lat == "0")//若经纬度为0 获取实时经纬度中间表的经纬度数据
                {
                    try
                    {
                        string swhere = string.Format(" USERID='{0}' and SBDATE='{1}'", reportdata.HID, DateTime.Now.ToString("yyyy-MM-dd"));//" USERID='13455' and SBDATE='2016-11-17'"; 
                        var record = realdatatempdal.GetRealDataTmpList(1, swhere, "REALDATAID").FirstOrDefault();
                        if (record != null)
                        {
                            lon = record.LONGITUDE.ToString();
                            lat = record.LATITUDE.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        logs.ErrorFormat("电话号码{0}，上报数据获取最新实时经纬度中间表经纬度时间失败", phone);
                        logs.Error(ex.Message);
                    }

                }
                var address = alarmbll.GetAddress(Convert.ToDecimal(lon), Convert.ToDecimal(lat));//上报地址
                reportdata.ADDRESS = address;//采集地址
                var i = reportdatadal.Add(reportdata);//追加上报数据
                if (i > 0)
                {

                    T_IPSRPT_DATADETAILModel model = new T_IPSRPT_DATADETAILModel();
                    model.LONGITUDE = Convert.ToDecimal(lon);
                    model.LATITUDE = Convert.ToDecimal(lat);
                    if (string.IsNullOrEmpty(cjtime))
                    {
                        model.SBTIME = DateTime.Now;//时间
                    }
                    else
                    {
                        model.SBTIME = Convert.ToDateTime(cjtime);//DateTime.Now;//时间
                    }
                    model.REPORTID = i;
                    // logs.Error("==(lon)" + model.LONGITUDE + "(lat)" + model.LATITUDE + "(i)" + i.ToString() + "==" + model.REPORTID.ToString());
                    datadetaildal.Add(model);//追加上报详细表
                    if (!string.IsNullOrEmpty(base64str))//上传图片或者视频或者音频
                    {
                        string fileVirtualPath = "";//转化视频音频路径
                        UpLoadCommon com = new UpLoadCommon();
                        string uploadFolder = ConfigurationManager.AppSettings["SpringeruploadImagePath"];
                        string dirFolder = ConfigurationManager.AppSettings["DirPath"];

                        if (type == "2")//上传类型为视频
                        {
                            uploadFolder = ConfigurationManager.AppSettings["SpringeruploadVideoPath"];
                            if (string.IsNullOrEmpty(uploadFolder))
                            {
                                uploadFolder = "~/SpringerUploadVideo/";
                            }
                            fileVirtualPath = ConfigurationManager.AppSettings["SpringeruploadCovertVideoPath"];
                        }
                        else if (type == "3")//上传为音频
                        {
                            uploadFolder = ConfigurationManager.AppSettings["SpringeruploadAudioPath"];
                            if (string.IsNullOrEmpty(uploadFolder))
                            {
                                uploadFolder = "~/SpringerUploadAudio/";
                            }
                            fileVirtualPath = ConfigurationManager.AppSettings["SpringeruploadConvertAudioPath"];
                        }
                        string filePath = "";//文件相对路径
                        byte[] imageBytes = Convert.FromBase64String(base64str);
                        filePath = com.UploadFileByType(imageBytes, type, uploadFolder, dirFolder, fileVirtualPath);
                        if (!string.IsNullOrEmpty(filePath))
                        {
                            T_IPSRPT_DATAUPLOADModel info = new T_IPSRPT_DATAUPLOADModel();//上传文件表
                            info.REPORTID = i;
                            // model.UPLOADNAME = fileupload.FileName;
                            info.UPLOADURL = filePath;
                            info.UPLOADTYPE = type;
                            //model.UPLOADDESCRIBE = fileupload.FileDescripe;
                            uploaddal.Add(info);
                        }
                        else
                        {
                            logs.ErrorFormat("{0}上报媒体信息失败,类型值{1}", phone, infotype);
                        }
                    }
                    return true;
                }
                else
                {
                    logs.ErrorFormat("{0}上报数据失败,类型值{1}", phone, infotype);
                    return false;
                }
            }
            catch (Exception ex)
            {
                logs.Error(ex.Message);
                return false;
            }
        }
        #endregion  ExtensionMethod
    }
}
