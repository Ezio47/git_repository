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
using System.Linq;
using System.Text;

namespace Springer.BLL
{
    /// <summary>
    /// T_IPSCOL_COLLECTDATA
    /// </summary>
    public partial class T_IPSCOL_COLLECTDATABLL
    {
        private readonly T_IPSCOL_COLLECTDATADAL dal = new T_IPSCOL_COLLECTDATADAL();//采集数据
        private readonly T_IPSCOL_DATADETAILDAL datadetaildal = new T_IPSCOL_DATADETAILDAL();//采集数据明细
        private readonly T_IPSCOL_DATAUPLOADDAL uploaddal = new T_IPSCOL_DATAUPLOADDAL();//采集数据上传明细
        private readonly T_IPSFR_USERDAL huserdal = new T_IPSFR_USERDAL();//护林员
        private readonly T_IPS_REALDATATEMPORARYDAL realdatatempdal = new T_IPS_REALDATATEMPORARYDAL();//实时数据中间表
        private readonly ILog logs = LogHelper.GetInstance();

        public T_IPSCOL_COLLECTDATABLL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int HID, long COLLECTID)
        {
            return dal.Exists(HID, COLLECTID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(T_IPSCOL_COLLECTDATAModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(T_IPSCOL_COLLECTDATAModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long COLLECTID)
        {

            return dal.Delete(COLLECTID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int HID, long COLLECTID)
        {

            return dal.Delete(HID, COLLECTID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string COLLECTIDlist)
        {
            return dal.DeleteList(Maticsoft.Common.PageValidate.SafeLongFilter(COLLECTIDlist, 0));
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public T_IPSCOL_COLLECTDATAModel GetModel(long COLLECTID)
        {

            return dal.GetModel(COLLECTID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public T_IPSCOL_COLLECTDATAModel GetModelByCache(long COLLECTID)
        {

            string CacheKey = "T_IPSCOL_COLLECTDATAModel-" + COLLECTID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(COLLECTID);
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (T_IPSCOL_COLLECTDATAModel)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<T_IPSCOL_COLLECTDATAModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<T_IPSCOL_COLLECTDATAModel> DataTableToList(DataTable dt)
        {
            List<T_IPSCOL_COLLECTDATAModel> modelList = new List<T_IPSCOL_COLLECTDATAModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                T_IPSCOL_COLLECTDATAModel model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
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
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod
        #region  ExtensionMethod
        /// <summary>
        /// 采集数据Add
        /// </summary>
        /// <param name="datalist">采集数据详细</param>
        /// <param name="infotype">采集信息类型</param>
        /// <param name="title">上报标题</param>
        /// <param name="phone">上报电话</param>
        /// <param name="fileupload">采集图片或视频</param>
        /// <returns></returns>
        public bool AddCollectData(IList<DataInfoModel> datalist, string infotype, string title, string phone, FileUploadMessageModel fileupload)
        {
            string strwhere = string.Empty;
            if (string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(infotype) || datalist.Count == 0)
            {
                return false;
            }
            try
            {
                strwhere = " PHONE='" + phone.Trim() + "'";
                var reportdata = new T_IPSCOL_COLLECTDATAModel();
                reportdata.COLLECTNAME = title;//采集标题
                reportdata.COLLECTTIME = DateTime.Now;//时间
                reportdata.SYSTYPEVALUE = infotype;//类型值
                var userlist = huserdal.GetHuserDataList(1, strwhere, "");
                if (userlist.Count < 0)
                {
                    return false;
                }
                reportdata.HID = userlist[0].HID;//护林员id

                var i = dal.Add(reportdata);//追加采集数据
                if (i > 0)
                {
                    if (datalist.Count > 0)
                    {
                        foreach (var data in datalist)
                        {
                            T_IPSCOL_DATADETAILModel model = new T_IPSCOL_DATADETAILModel();
                            model.LONGITUDE = data.lon;
                            model.LATITUDE = data.lat;
                            model.HEIGHT = data.height;
                            model.COLLECTTIME = Convert.ToDateTime(data.cjtime);
                            model.DIRECTION = data.dir;
                            model.COLLECTID = i;
                            datadetaildal.Add(model);//追加上报详细表
                        }
                    }
                    if (fileupload != null)//上传图片或者视频
                    {
                        UpLoadCommon com = new UpLoadCommon();
                        string uploadFolder = ConfigurationManager.AppSettings["SpringercollectImagePath"];
                        if (fileupload.FileType == "1")//上传类型为视频
                        {
                            uploadFolder = ConfigurationManager.AppSettings["SpringercollectVideoPath"];
                            if (string.IsNullOrEmpty(uploadFolder))
                            {
                                uploadFolder = "~/SpringerCollectVideo/";
                            }
                        }
                        else//上传为图片
                        {
                            if (string.IsNullOrEmpty(uploadFolder))
                            {
                                uploadFolder = "~/SpringerCollectImage/";
                            }
                        }
                        string filePath = "";//文件相对路径
                        filePath = com.UploadFile(fileupload, uploadFolder);
                        if (!string.IsNullOrEmpty(filePath))
                        {
                            T_IPSCOL_DATAUPLOADModel model = new T_IPSCOL_DATAUPLOADModel();//上传文件表
                            model.COLLECTID = i;
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
        /// 采集数据AddCollectDataStr
        /// </summary>
        /// <param name="datalistJsonstr">经纬度json字符串</param>
        /// <param name="infotype">采集数据类型</param>
        /// <param name="describe">采集描述</param>
        /// <param name="cjtime">采集时间</param>
        /// <param name="phone">电话号码</param>
        /// <param name="base64str">加密文件</param>
        /// <returns></returns>
        public bool AddCollectDataStr(string datalistJsonstr, string infotype, string describe, string cjtime, string phone, string base64str, string type = "1")
        {
            string strwhere = string.Empty;
            if (string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(datalistJsonstr) || string.IsNullOrEmpty(infotype) || string.IsNullOrEmpty(cjtime))
            {
                return false;
            }
            try
            {
                var datalist = JsonHelper.JsonToList<DataInfoModel>(datalistJsonstr);
                strwhere = " PHONE='" + phone.Trim() + "'";
                var collectdata = new T_IPSCOL_COLLECTDATAModel();
                collectdata.COLLECTNAME = describe;//采集标题
                collectdata.COLLECTTIME = Convert.ToDateTime(cjtime);//时间
                collectdata.SYSTYPEVALUE = infotype;//类型值

                var userlist = huserdal.GetHuserDataList(1, strwhere, "");
                if (userlist.Count < 0)
                {
                    return false;
                }
                collectdata.HID = userlist[0].HID;//护林员id
                if (datalist.Any())
                {
                    var alarmbll = new T_IPS_ALARMBLL();
                    if (datalist[0].lon == 0 || datalist[0].lat == 0)//若经纬度为0 获取实时经纬度中间表的经纬度数据
                    {
                        try
                        {
                            string swhere = string.Format(" USERID='{0}' and SBDATE='{1}'", collectdata.HID, DateTime.Now.ToString("yyyy-MM-dd"));//" USERID='13455' and SBDATE='2016-11-17'"; 
                            var record = realdatatempdal.GetRealDataTmpList(1, swhere, "REALDATAID").FirstOrDefault();
                            if (record != null)
                            {
                                datalist[0].lon = record.LONGITUDE;
                                datalist[0].lat = record.LATITUDE;
                            }
                        }
                        catch (Exception ex)
                        {
                            logs.ErrorFormat("电话号码{0}，采集数据获取最新实时经纬度中间表经纬度时间失败", phone);
                            logs.Error(ex.Message);
                        }
                    }
                    var address = alarmbll.GetAddress(datalist[0].lon, datalist[0].lat);
                    collectdata.ADDRESS = address;//采集地址
                }

                var i = dal.Add(collectdata);//追加采集数据
                if (i > 0)
                {
                    //JsonHelper
                    if (!string.IsNullOrEmpty(datalistJsonstr))
                    {
                        //var model = JsonHelper.JsonDeserialize<DataInfoModel>(datalistJsonstr);
                        if (datalist.Any())
                        {
                            foreach (var data in datalist)
                            {
                                T_IPSCOL_DATADETAILModel model = new T_IPSCOL_DATADETAILModel();
                                model.LONGITUDE = data.lon;
                                model.LATITUDE = data.lat;
                                model.HEIGHT = data.height;
                                model.COLLECTTIME = Convert.ToDateTime(data.cjtime);
                                model.DIRECTION = data.dir;
                                model.COLLECTID = i;
                                datadetaildal.Add(model);//追加上报详细表
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(base64str))//上传图片或者视频、音频
                    {
                        string fileVirtualPath = "";//转化视频音频路径
                        UpLoadCommon com = new UpLoadCommon();
                        string uploadFolder = ConfigurationManager.AppSettings["SpringercollectImagePath"];
                        string dirFolder = ConfigurationManager.AppSettings["DirPath"];

                        if (type == "2")//采集数据上传类型为视频
                        {
                            uploadFolder = ConfigurationManager.AppSettings["SpringercollectVideoPath"];
                            if (string.IsNullOrEmpty(uploadFolder))
                            {
                                uploadFolder = "~/SpringerCollectVideo/";
                            }
                            fileVirtualPath = ConfigurationManager.AppSettings["SpringercollectCovertVideoPath"];
                        }
                        else if (type == "3")//采集数据上传为音频
                        {
                            uploadFolder = ConfigurationManager.AppSettings["SpringercollectAudioPath"];
                            if (string.IsNullOrEmpty(uploadFolder))
                            {
                                uploadFolder = "~/SpringerCollectAudio/";
                            }
                            fileVirtualPath = ConfigurationManager.AppSettings["SpringercollectConvertAudioPath"];
                        }
                        string filePath = "";//文件相对路径
                        byte[] imageBytes = Convert.FromBase64String(base64str);
                        filePath = com.UploadFileByType(imageBytes, type, uploadFolder, dirFolder, fileVirtualPath);
                        if (!string.IsNullOrEmpty(filePath))
                        {
                            T_IPSCOL_DATAUPLOADModel model = new T_IPSCOL_DATAUPLOADModel();//上传文件表
                            model.COLLECTID = i;
                            //  model.UPLOADNAME = fileupload.FileName;
                            model.UPLOADURL = filePath;
                            model.UPLOADTYPE = type;
                            // model.UPLOADDESCRIBE = fileupload.FileDescripe;
                            uploaddal.Add(model);
                        }
                    }
                    return true;
                }
                else
                {
                    logs.ErrorFormat("{0}采集数据失败,类型值{1}", phone, infotype);
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
