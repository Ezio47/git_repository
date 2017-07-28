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
    /// T_SYS_DICT
    /// </summary>
    public partial class T_SYS_DICTBLL
    {
        private readonly T_SYS_DICTDAL dal = new T_SYS_DICTDAL();
        public T_SYS_DICTBLL()
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
        public bool Exists(int DICTID)
        {
            return dal.Exists(DICTID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(T_SYS_DICTModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(T_SYS_DICTModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int DICTID)
        {

            return dal.Delete(DICTID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string DICTIDlist)
        {
            return dal.DeleteList(Maticsoft.Common.PageValidate.SafeLongFilter(DICTIDlist, 0));
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public T_SYS_DICTModel GetModel(int DICTID)
        {

            return dal.GetModel(DICTID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public T_SYS_DICTModel GetModelByCache(int DICTID)
        {

            string CacheKey = "T_SYS_DICTModel-" + DICTID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(DICTID);
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (T_SYS_DICTModel)objModel;
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
        public List<T_SYS_DICTModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }


        /// <summary>
        /// 获取数据列表（字典类型）
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetModelListDic(string strWhere)
        {
            var dic = new Dictionary<string, string>();
            var list = this.GetModelList(strWhere);
            if (list.Any())
            {
                foreach (var item in list)
                {
                    dic.Add(item.DICTNAME, item.DICTVALUE);
                }
            }
            return dic;
        }

        /// <summary>
        /// 获取数据列表（字典类型）
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<DicValueModel> GetModelListDicValueList(string strWhere)
        {
            var result = new List<DicValueModel>();
            var list = this.GetModelList(strWhere);
            if (list.Any())
            {
                foreach (var item in list)
                {
                    var info = new DicValueModel();
                   // String[] values = { item.DICTVALUE, item.STANDBY1 };
                    info.typename = item.DICTNAME;
                    info.typeid = item.DICTVALUE;
                    info.spatialtype = item.STANDBY1;
                    result.Add(info);
                }
            }
            return result;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<T_SYS_DICTModel> DataTableToList(DataTable dt)
        {
            List<T_SYS_DICTModel> modelList = new List<T_SYS_DICTModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                T_SYS_DICTModel model;
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

        #endregion  ExtensionMethod
    }
}
