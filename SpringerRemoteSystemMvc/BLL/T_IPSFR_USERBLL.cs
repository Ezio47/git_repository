using Springer.DAL;
using Springer.EntityModel;
using Springer.EntityModel.Enum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Springer.BLL
{
    public partial class T_IPSFR_USERBLL
    {
        private readonly T_IPSFR_USERDAL dal = new T_IPSFR_USERDAL();
        public T_IPSFR_USERBLL()
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
        public bool Exists(int HID)
        {
            return dal.Exists(HID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(T_IPSFR_USERModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(T_IPSFR_USERModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int HID)
        {

            return dal.Delete(HID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string HIDlist)
        {
            return dal.DeleteList(Maticsoft.Common.PageValidate.SafeLongFilter(HIDlist, 0));
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public T_IPSFR_USERModel GetModel(int HID)
        {

            return dal.GetModel(HID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public T_IPSFR_USERModel GetModelByCache(int HID)
        {

            string CacheKey = "T_IPSFR_USERModel-" + HID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(HID);
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (T_IPSFR_USERModel)objModel;
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
        public List<T_IPSFR_USERModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<T_IPSFR_USERModel> DataTableToList(DataTable dt)
        {
            List<T_IPSFR_USERModel> modelList = new List<T_IPSFR_USERModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                T_IPSFR_USERModel model;
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
        /// 注册护林员
        /// </summary>
        /// <param name="sn"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        public bool RegisterHUser(string sn, string phone)
        {
            return dal.RegisterHUser(sn, phone);
        }

        /// <summary>
        /// 注册护林员(string)
        /// </summary>
        /// <param name="sn"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        public string RegisterHUserStr(string sn, string phone)
        {
            return dal.RegisterHUserStr(sn, phone);
        }

        /// <summary>
        /// 返回号码状态
        /// </summary>
        /// <param name="sn"></param>
        /// <returns></returns>
        public string ReturnPhoneStatus(string sn)
        {
            return dal.ReturnPhoneStatus(sn);
        }

        /// <summary>
        /// 判断手机号码是否存在
        /// </summary>
        /// <param name="sn"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        public bool Exist(string sn, string phone) 
        {
            return dal.ExistHUser(sn, phone);
        }

        /// <summary>
        /// 根据手机号码获取护林员HID
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public string getHID(string phone) 
        {
            string HID = dal.GetModelByPhone(phone).HID.ToString();
            return HID;
        }
        #endregion  ExtensionMethod
    }
}
