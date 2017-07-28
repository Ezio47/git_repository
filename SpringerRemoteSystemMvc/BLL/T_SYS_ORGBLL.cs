using Springer.DAL;
using Springer.EntityModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Springer.BLL
{
    public class T_SYS_ORGBLL
    {


        private readonly T_SYS_ORGDAL dal = new T_SYS_ORGDAL();
        public T_SYS_ORGBLL()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string ORGNO)
        {
            return dal.Exists(ORGNO);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(T_SYS_ORGModel model)
        {
            dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(T_SYS_ORGModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string ORGNO)
        {

            return dal.Delete(ORGNO);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public T_SYS_ORGModel GetModel(string ORGNO)
        {

            return dal.GetModel(ORGNO);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public T_SYS_ORGModel GetModelByCache(string ORGNO)
        {

            string CacheKey = "T_SYS_ORGModel-" + ORGNO;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ORGNO);
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (T_SYS_ORGModel)objModel;
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
        public List<T_SYS_ORGModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<T_SYS_ORGModel> DataTableToList(DataTable dt)
        {
            List<T_SYS_ORGModel> modelList = new List<T_SYS_ORGModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                T_SYS_ORGModel model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new T_SYS_ORGModel();
                    model.ORGNO = dt.Rows[n]["ORGNO"].ToString();
                    if (dt.Rows[n]["WD"].ToString() != "")
                    {
                        model.WD = decimal.Parse(dt.Rows[n]["WD"].ToString());
                    }
                    model.COMMANDNAME = dt.Rows[n]["COMMANDNAME"].ToString();
                    model.CITYID = dt.Rows[n]["CITYID"].ToString();
                    model.WEATHERJC = dt.Rows[n]["WEATHERJC"].ToString();
                    model.POSTCODE = dt.Rows[n]["POSTCODE"].ToString();
                    model.DUTYTELL = dt.Rows[n]["DUTYTELL"].ToString();
                    model.FAX = dt.Rows[n]["FAX"].ToString();
                    model.MOBILEPARAMLIST = dt.Rows[n]["MOBILEPARAMLIST"].ToString();
                    model.ADDRESS = dt.Rows[n]["ADDRESS"].ToString();
                    model.ORGNAME = dt.Rows[n]["ORGNAME"].ToString();
                    model.ORGDUTY = dt.Rows[n]["ORGDUTY"].ToString();
                    model.LEADER = dt.Rows[n]["LEADER"].ToString();
                    model.AREACODE = dt.Rows[n]["AREACODE"].ToString();
                    model.ORGJC = dt.Rows[n]["ORGJC"].ToString();
                    model.WXJC = dt.Rows[n]["WXJC"].ToString();
                    model.SYSFLAG = dt.Rows[n]["SYSFLAG"].ToString();
                    if (dt.Rows[n]["JD"].ToString() != "")
                    {
                        model.JD = decimal.Parse(dt.Rows[n]["JD"].ToString());
                    }


                    modelList.Add(model);
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
        #endregion
    }
}
