using Springer.DAL;
using Springer.EntityModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Springer.BLL
{
    /// <summary>
    /// T_SYS_PARAMETERBLL
    /// </summary>
    public partial class T_SYS_PARAMETERBLL
    {
        private readonly T_SYS_PARAMETERDAL dal = new T_SYS_PARAMETERDAL();
        private readonly T_SYS_ORGDAL orgdal = new T_SYS_ORGDAL();//机构
        private readonly T_IPSFR_USERDAL huserdal = new T_IPSFR_USERDAL();//护林员

        public T_SYS_PARAMETERBLL()
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
        public bool Exists(int PARAMID)
        {
            return dal.Exists(PARAMID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(T_SYS_PARAMETERModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(T_SYS_PARAMETERModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int PARAMID)
        {

            return dal.Delete(PARAMID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string PARAMIDlist)
        {
            return dal.DeleteList(Maticsoft.Common.PageValidate.SafeLongFilter(PARAMIDlist, 0));
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public T_SYS_PARAMETERModel GetModel(int PARAMID)
        {

            return dal.GetModel(PARAMID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public T_SYS_PARAMETERModel GetModelByCache(int PARAMID)
        {

            string CacheKey = "T_SYS_PARAMETERModel-" + PARAMID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(PARAMID);
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (T_SYS_PARAMETERModel)objModel;
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
        public List<T_SYS_PARAMETERModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<T_SYS_PARAMETERModel> DataTableToList(DataTable dt)
        {
            List<T_SYS_PARAMETERModel> modelList = new List<T_SYS_PARAMETERModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                T_SYS_PARAMETERModel model;
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
        /// 获取所有系统参数
        /// </summary>
        /// <param name="sysflg"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetDicSysParaments(string sysflg)
        {
            var dic = new Dictionary<string, string>();
            string strwhere = string.Empty;
            if (!string.IsNullOrEmpty(sysflg))
            {
                strwhere = " SYSFLAG= '" + sysflg.Trim() + "'";
            }
            var ds = this.GetList(strwhere);
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dic.Add(dr["PARAMNAME"].ToString(), dr["PARAMVALUE"].ToString());
                }
            }
            return dic;
        }
        /// <summary>
        /// 获取所有系统参数
        /// </summary>
        /// <param name="sysflg"></param>
        /// <param name="paramflag"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetDicSysParaments(string sysflg, string paramflag)
        {
            var dic = new Dictionary<string, string>();
            string strwhere = " 1 = 1 ";
            if (!string.IsNullOrEmpty(sysflg))
            {
                strwhere += " AND SYSFLAG= '" + sysflg.Trim() + "'";
            }
            if (!string.IsNullOrEmpty(paramflag))
            {
                strwhere += " AND PARAMFLAG= '" + paramflag.Trim() + "'";
            }
            var ds = this.GetList(strwhere);
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dic.Add(dr["PARAMFLAG"].ToString(), dr["PARAMVALUE"].ToString());
                }
            }
            return dic;
        }


        /// <summary>
        /// 电话号码获取参数设置
        /// 紧急呼救号码	SOS_TEL	1231345	
        ///数据回传频率	FQCY	180	单位(秒)
        ///每天开始回传时间	STATR_TIME	08:30	
        ///每天结束回传时间	END_TIME	19:30	
        ///WEB Service地址	WEB_SERVICE_URL
        ///数据回传有效日期	TransEanbleDate	1.1,5.1|6.1,12.31
        /// </summary>
        /// <param name="sysflg">系统标识</param>
        /// <param name="paramflag">key</param>
        /// <param name="phone">电话号码</param>
        /// <returns></returns>
        public Dictionary<string, string> GetDicSysParaments(string sysflg, string paramflag, string phone)
        {
            var keys = new string[] { "SOS_TEL", "FQCY", "STATR_TIME", "END_TIME", "WEB_SERVICE_URL", "TransEanbleDate" };
            var dic = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(phone))
            {
                dic = GetDicSysParaments(sysflg, paramflag);
            }
            else
            {
                var huser = huserdal.GetModelByPhone(phone);
                if (huser != null)
                {
                    if (string.IsNullOrEmpty(huser.MOBILEPARAMLIST))//护林员没有参数设置  则以机构参数为准 否则以默认参数
                    {
                        var org = orgdal.GetModel(huser.BYORGNO);
                        if (org != null)
                        {
                            if (!string.IsNullOrEmpty(org.MOBILEPARAMLIST))
                            {
                                var arrItems = org.MOBILEPARAMLIST.Split('$');//参数设置
                                if (arrItems.Length > 0)
                                {
                                    dic = GetDic(paramflag, keys, arrItems);
                                }
                            }
                            else
                            {
                                dic = GetDicSysParaments(sysflg, paramflag);
                            }
                        }
                    }
                    else
                    {
                        //1231345$180$08:30$19:30$http://36.7.68.79:88/SpringerRemoteSystemService.svc$1.1,5.1|6.1,12.31$$$$$$$$$$
                        var arrItems = huser.MOBILEPARAMLIST.Split('$');//护林员参数设置
                        if (arrItems.Length > 0)
                        {
                            dic = GetDic(paramflag, keys, arrItems);
                        }
                    }
                }
            }
            return dic;
        }
        #endregion  ExtensionMethod


        #region Private
        /// <summary>
        /// 获取keyvalue
        /// </summary>
        /// <param name="paramflag">指定key</param>
        /// <param name="Keys">获取的keys</param>
        /// <param name="arrItems">传值的value</param>
        /// <returns>返回指定的key 或者keys的字典</returns>
        private Dictionary<string, string> GetDic(string paramflag, string[] Keys, string[] arrItems)
        {
            var dic = new Dictionary<string, string>();
            if (arrItems.Length > 0)
            {
                int i = 0;
                foreach (var item in arrItems)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        dic.Add(Keys[i], item);
                    }
                    ++i;
                }
                if (!string.IsNullOrEmpty(paramflag))
                {
                    if (dic.ContainsKey(paramflag))
                    {
                        var value = dic[paramflag];
                        dic.Clear();
                        dic.Add(paramflag, value);
                    }
                    else
                    {
                        dic.Clear();
                    }
                }
            }
            return dic;
        }
        #endregion
    }
}
