using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace PublicClassLibrary
{
    /// <summary>
    /// 坐标偏移操作类
    /// </summary>
    public class ClsCoordinateRectify
    {
        static double pi = 3.14159265358979324;
        static double a = 6378245.0;
        static double ee = 0.00669342162296594323;

        /// <summary>
        /// 偏移
        /// </summary>
        /// <param name="wgLat">经度</param>
        /// <param name="wgLon">纬度</param>
        /// <param name="latlng">数组</param>
        public static void transform(double wgLat, double wgLon, double[] latlng)
        {
            if (outOfChina(wgLat, wgLon))
            {
                latlng[0] = wgLat;
                latlng[1] = wgLon;
                return;
            }
            double dLat = transformLat(wgLon - 105.0, wgLat - 35.0);
            double dLon = transformLon(wgLon - 105.0, wgLat - 35.0);
            double radLat = wgLat / 180.0 * pi;
            double magic = Math.Sin(radLat);
            magic = 1 - ee * magic * magic;
            double sqrtMagic = Math.Sqrt(magic);
            dLat = (dLat * 180.0) / ((a * (1 - ee)) / (magic * sqrtMagic) * pi);
            dLon = (dLon * 180.0) / (a / sqrtMagic * Math.Cos(radLat) * pi);
            latlng[0] = wgLat + dLat;
            latlng[1] = wgLon + dLon;
        }

        /// <summary>
        /// 偏移计算
        /// </summary>
        /// <param name="wgLat">经度</param>
        /// <param name="wgLon">纬度</param>
        /// <returns>偏移计算后的经纬度</returns>
        public static double[] transform2(double wgLat, double wgLon)
        {
            // <!--是否经纬度偏移量转换 0 表示不需要 1表示需要-->
            string changeType = ConfigurationManager.AppSettings["lonLatChange"].ToString();
            double[] latlng = new double[2];
            if (changeType == "1")//1表示需要
            {
                if (outOfChina(wgLat, wgLon))
                {
                    latlng[0] = wgLat;
                    latlng[1] = wgLon;
                    return latlng;
                }
                double dLat = transformLat(wgLon - 105.0, wgLat - 35.0);
                double dLon = transformLon(wgLon - 105.0, wgLat - 35.0);
                double radLat = wgLat / 180.0 * pi;
                double magic = Math.Sin(radLat);
                magic = 1 - ee * magic * magic;
                double sqrtMagic = Math.Sqrt(magic);
                dLat = (dLat * 180.0) / ((a * (1 - ee)) / (magic * sqrtMagic) * pi);
                dLon = (dLon * 180.0) / (a / sqrtMagic * Math.Cos(radLat) * pi);
                latlng[0] = wgLat + dLat;
                latlng[1] = wgLon + dLon;
            }
            else//无坐标偏移量
            {
                latlng[0] = wgLat;
                latlng[1] = wgLon;

            }
            return latlng;
        }

        /// <summary>
        /// 备用 
        /// </summary>
        /// <param name="lat">经度</param>
        /// <param name="lon">纬度</param>
        /// <returns>bool型</returns>
        private static bool outOfChina(double lat, double lon)
        {
            if (lon < 72.004 || lon > 137.8347)
                return true;
            if (lat < 0.8293 || lat > 55.8271)
                return true;
            return false;
        }

        /// <summary>
        /// 备用
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="y">t</param>
        /// <returns>double型</returns>
        private static double transformLat(double x, double y)
        {
            double ret = -100.0 + 2.0 * x + 3.0 * y + 0.2 * y * y + 0.1 * x * y + 0.2 * Math.Sqrt(Math.Abs(x));
            ret += (20.0 * Math.Sin(6.0 * x * pi) + 20.0 * Math.Sin(2.0 * x * pi)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(y * pi) + 40.0 * Math.Sin(y / 3.0 * pi)) * 2.0 / 3.0;
            ret += (160.0 * Math.Sin(y / 12.0 * pi) + 320 * Math.Sin(y * pi / 30.0)) * 2.0 / 3.0;
            return ret;
        }

        /// <summary>
        /// 备用 
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="y">t</param>
        /// <returns>double型</returns>
        private static double transformLon(double x, double y)
        {
            double ret = 300.0 + x + 2.0 * y + 0.1 * x * x + 0.1 * x * y + 0.1 * Math.Sqrt(Math.Abs(x));
            ret += (20.0 * Math.Sin(6.0 * x * pi) + 20.0 * Math.Sin(2.0 * x * pi)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(x * pi) + 40.0 * Math.Sin(x / 3.0 * pi)) * 2.0 / 3.0;
            ret += (150.0 * Math.Sin(x / 12.0 * pi) + 300.0 * Math.Sin(x / 30.0 * pi)) * 2.0 / 3.0;
            return ret;
        }
    }
}
