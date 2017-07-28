using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicClassLibrary.PublicCom
{
    /// <summary>
    /// 公共逻辑计算类
    /// </summary>
    public class LogicalCalcTools
    {
        /// <summary>
        /// 计算两点地图坐标的距离
        /// </summary>
        /// <param name="X1"></param>
        /// <param name="Y1"></param>
        /// <param name="X2"></param>
        /// <param name="Y2"></param>
        /// <returns></returns>
        public static double CalcMil(double X1, double Y1, double X2, double Y2)
        {
            double PI = 3.1415926535898;
            double EARTH_RADIUS = 6378137;  //地球半径 米 

            double CurRadLong = 0;	//两点经纬度的弧度
            double CurRadLat = 0;
            double PreRadLong = 0;
            double PreRadLat = 0;
            double a = 0, b = 0;   //经纬度弧度差
            double MilValue = 0;

            //将经纬度换算成弧度
            CurRadLong = (double)(X1);
            CurRadLong = CurRadLong * PI / 180.0;

            PreRadLong = (double)(X2);
            PreRadLong = PreRadLong * PI / 180.0;

            CurRadLat = (double)(Y1);
            CurRadLat = CurRadLat * PI / 180.0f;

            PreRadLat = (double)(Y2);
            PreRadLat = PreRadLat * PI / 180.0f;

            //计算经纬度差值
            if (CurRadLat > PreRadLat)
            {
                a = CurRadLat - PreRadLat;
            }
            else
            {
                a = PreRadLat - CurRadLat;
            }

            if (CurRadLong > PreRadLong)
            {
                b = CurRadLong - PreRadLong;
            }
            else
            {
                b = PreRadLong - CurRadLong;
            }

            MilValue = 2 * Math.Asin(Math.Sqrt(Math.Sin(a / 2.0) * Math.Sin(a / 2.0) + Math.Cos(CurRadLat) * Math.Cos(PreRadLat) * Math.Sin(b / 2.0) * Math.Sin(b / 2.0)));
            MilValue = (double)(EARTH_RADIUS * MilValue);
            return MilValue;
        }
    }
}
