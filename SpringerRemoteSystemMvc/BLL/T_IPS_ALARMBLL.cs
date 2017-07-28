using log4net;
using Springer.Common;
using Springer.Common.Utils;
using Springer.DAL;
using Springer.EntityModel;
using Springer.EntityModel.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Springer.BLL
{
    /// <summary>
    /// T_IPS_ALARM
    /// </summary>
    public partial class T_IPS_ALARMBLL
    {
        private readonly T_IPS_ALARMDAL dal = new T_IPS_ALARMDAL();
        private readonly ILog logs = LogHelper.GetInstance();
        public T_IPS_ALARMBLL()
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
        public bool Exists(int ALARMID)
        {
            return dal.Exists(ALARMID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(T_IPS_ALARMModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(T_IPS_ALARMModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ALARMID)
        {

            return dal.Delete(ALARMID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string ALARMIDlist)
        {
            return dal.DeleteList(Maticsoft.Common.PageValidate.SafeLongFilter(ALARMIDlist, 0));
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public T_IPS_ALARMModel GetModel(int ALARMID)
        {

            return dal.GetModel(ALARMID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public T_IPS_ALARMModel GetModelByCache(int ALARMID)
        {

            string CacheKey = "T_IPS_ALARMModel-" + ALARMID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ALARMID);
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (T_IPS_ALARMModel)objModel;
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
        public List<T_IPS_ALARMModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<T_IPS_ALARMModel> DataTableToList(DataTable dt)
        {
            List<T_IPS_ALARMModel> modelList = new List<T_IPS_ALARMModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                T_IPS_ALARMModel model;
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

        public string GetAddress(decimal lng, decimal lat)
        {
            var com = new HttpCommon();
            BaiDuApiAddressModel model = new BaiDuApiAddressModel();
            //string para="name="+name + "&ak=" + ak + "&is_published=" + is_published + "&geotype=" + geotype;
            //string url = "http://api.map.baidu.com/geocoder/v2/?ak=wYCjPb9rxUueQP8xcNwqGLFw&callback=renderReverselocation=39.983424,116.322987&output=json&pois=1";
            //http://api.map.baidu.com/geocoder/v2/?ak=E4805d16520de693a3fe707cdc962045&callback=renderReverse&location=39.983424,116.322987&output=json&pois=0
            string url = "http://api.map.baidu.com/geocoder/v2/";
            string postdata = "ak=wYCjPb9rxUueQP8xcNwqGLFw&location=" + lat + "," + lng + "&output=json&pois=0";
            try
            {
                string str = com.HttpGet(url, postdata);
                model = JsonHelper.JsonDeserialize<BaiDuApiAddressModel>(str);
            }
            catch (Exception ex)
            {
                logs.ErrorFormat("经度{0},纬度{1}地址转换失败", lat, lng);
                logs.Error(ex.Message);
            }
            return model.result.formatted_address;
        }

        //public string Test(decimal lng, decimal lat)
        //{
        //    //string url = "http://api.map.baidu.com/geocoder/v2/?ak=wYCjPb9rxUueQP8xcNwqGLFw&callback=renderReverselocation=39.983424,116.322987&output=json&pois=1";
        //    //http://api.map.baidu.com/geocoder/v2/?ak=E4805d16520de693a3fe707cdc962045&callback=renderReverse&location=39.983424,116.322987&output=json&pois=0
        //    string url = "http://api.map.baidu.com/geocoder/v2/";
        //    string apiKey = "wYCjPb9rxUueQP8xcNwqGLFw";
        //    string output = "json";
        //    string callback = "renderReverse";
        //    IDictionary<string, string> param = new Dictionary<string, string>();
        //    param.Add("ak", apiKey);
        //    param.Add("output", output);
        //    param.Add("callback", callback);
        //    param.Add("location", "39.983424,116.322987");//location=39.983424,116.322987
        //    param.Add("pois", "0");
        //    string str = HttpUtils.DoGet(url, param);
        //    return str;
        //}

        #endregion  ExtensionMethod
    }
}
