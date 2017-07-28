using Springer.EntityModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace TLW.Project.Springer.SpringerRemoteDataWcfService.Interfaces
{
    [ServiceContract]
    //[ServiceKnownType(typeof(Points))]
    public interface IFireSpreadService
    {
        /// <summary>
        /// 获取初速度
        /// </summary>
        /// <param name="dWindSpeed">风力（单位：米/每秒）</param>
        /// <param name="dHumidity">湿度值</param>
        /// <param name="dTemperature">温度值</param>
        /// <returns>初速度</returns>
        double GetVelocity(
             double dWindSpeed,//风力（单位：米/每秒）
             double dHumidity,//湿度值
             double dTemperature//温度值
            );

        /// <summary>
        /// 获取格网宽度
        /// </summary>
        /// <param name="dV">初速度</param>
        /// <param name="dTime">分段值（即：相对速度的时间值）（单位：分钟）</param>
        /// <returns></returns>
        double GetGridWidth(
             double dV,//初速度
             double dTime//分段值（即：相对速度的时间值）（单位：分钟）
            );

        /// <summary>
        /// 风力过滤（即：风力大于某值后不会发生林火燃烧现象,返回True表示可以燃烧）（可以通过重写来修改）
        /// </summary>
        /// <param name="dWindPower">风力</param>
        /// <returns>返回True表示可以燃烧</returns>
        bool WindPowerFilter(
           double dWindPower//风力
           );//风力过滤（即：风力大于某值后不会发生林火燃烧现象,返回True表示可以燃烧）（可以通过重写来修改）

        /// <summary>
        /// 湿度过滤（即：湿度高于某值后不会发生林火燃烧现象,返回True表示可以燃烧）（可以通过重写来修改）
        /// </summary>
        /// <param name="dHumidity">湿度</param>
        /// <returns>返回True表示可以燃烧</returns>
        bool HumidityFilter(
            double dHumidity//湿度
            );//湿度过滤（即：湿度高于某值后不会发生林火燃烧现象,返回True表示可以燃烧）（可以通过重写来修改）

        /// <summary>
        /// 温度过滤（即：温度低于某值后不会发生林火燃烧现象,返回True表示可以燃烧）（可以通过重写来修改）
        /// </summary>
        /// <param name="dTemperature">温度</param>
        /// <returns>返回True表示可以燃烧</returns>
        bool TemperatureFilter(
            double dTemperature//温度
            );//温度过滤（即：温度低于某值后不会发生林火燃烧现象,返回True表示可以燃烧）（可以通过重写来修改）

        /// <summary>
        /// 辐散中心点
        /// </summary>
        /// <param name="pCenterPoint">辐散中心点</param>
        /// <param name="dWindDirection">风向角度值（北风：180，东风：270，南风：0，西风：45以此类推，即：方向与北方向的顺时针夹角，切记有效值为0至360）</param>
        /// <param name="dWindSpeed">风力（单位：米/每秒）</param>
        /// <param name="dHumidity">湿度值</param>
        /// <param name="dTemperature">温度值</param>
        /// <param name="dW">网格宽度（小于0时，系统自动分配）</param>
        /// <param name="dTime">分段值（即：相对速度的时间值）（单位：分钟）</param>
        /// <param name="bConvexHull">凸多边形</param>
        /// <returns>燃烧面积</returns>
        [OperationContract]
        //[ServiceKnownType(typeof(Points))]
        List<Points> FireSpread(
             string jd,//经度
             string wd,//纬度
             double dWindDirection,//风向角度值（北风：180，东风：270，南风：0，西风：45以此类推，即：方向与北方向的顺时针夹角，切记有效值为0至360）
             double dWindSpeed,//风力（单位：米/每秒）
             double dHumidity,//湿度值
             double dTemperature,//温度值
             double dW,//网格宽度（小于0时，系统自动分配）
             double dTime,//分段值（即：相对速度的时间值）（单位：分钟）
             bool bConvexHull//凸多边形
             );


    }
}
