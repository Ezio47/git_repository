using GeoAPI.Geometries;
using NetTopologySuite.Geometries;
using Springer.DAL;
using Springer.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Springer.BLL
{
    public partial class T_IPSFR_ROUTERAIL_RAILBLL
    {

        private readonly T_IPSFR_ROUTERAIL_RAILDAL dal = new T_IPSFR_ROUTERAIL_RAILDAL();//出围表
        private readonly T_IPSFR_USERDAL userdal = new T_IPSFR_USERDAL();//护林员
        private readonly T_SYS_PARAMETERBLL parametebll = new T_SYS_PARAMETERBLL();//系统参数

        /// <summary>
        /// 增加一条出围数据
        /// </summary>
        public int Add(T_IPSFR_ROUTERAIL_RAILModel model)
        {
            return dal.Add(model);
        }


        /// <summary>
        /// 检索是否存在围栏数据 ROADTYPE =1 为围栏
        /// </summary>
        /// <param name="hid"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool ExistROUTERAIL(int hid, int type)
        {
            return dal.ExistROUTERAIL(hid, type);
        }

        /// <summary>
        /// 电话号码获取护林员
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public T_IPSFR_USERModel GetHUserModel(string phone)
        {

            return userdal.GetModelByPhone(phone);
        }

        /// <summary>
        /// 获取围栏数据
        /// </summary>
        /// <param name="top"></param>
        /// <param name="strwhere"></param>
        /// <param name="ordercloum"></param>
        /// <returns></returns>
        public List<T_IPSFR_ROUTERAILModel> GetROUTERAILDataList(int top, string strwhere, string ordercloum)
        {
            return dal.GetROUTERAILDataList(top, strwhere, ordercloum);
        }

        /// <summary>
        /// 是否出围
        /// </summary>
        /// <param name="coArr"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool Fence(List<Coordinate> coArr, IPoint point)
        {
            ILinearRing pLinrRing = new LinearRing(coArr.ToArray());
            Polygon poly = new Polygon(pLinrRing);
            string distance = "";
            var parameter = parametebll.GetDicSysParaments("Springer", "FrenceDistance");
            parameter.TryGetValue("FrenceDistance", out distance);
            if (string.IsNullOrEmpty(distance))
            {
                distance = System.Configuration.ConfigurationManager.AppSettings["FrenceDistance"];
            }
            if (poly.IsWithinDistance(point, Convert.ToDouble(distance)))//.Contains(point))
            {
                return false;//未出围
            }
            else
            {
                return true;//出围
            }
        }
    }
}
