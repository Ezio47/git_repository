using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicClassLibrary
{
    /** 
 * 各地图API坐标系统比较与转换; 
 * WGS84坐标系：即地球坐标系，国际上通用的坐标系。设备一般包含GPS芯片或者北斗芯片获取的经纬度为WGS84地理坐标系, 
 * 谷歌地图采用的是WGS84地理坐标系（中国范围除外）; 
 * GCJ02坐标系：即火星坐标系，是由中国国家测绘局制订的地理信息系统的坐标系统。由WGS84坐标系经加密后的坐标系。 
 * 谷歌中国地图和搜搜中国地图采用的是GCJ02地理坐标系; BD09坐标系：即百度坐标系，GCJ02坐标系经加密后的坐标系; 
 * 搜狗坐标系、图吧坐标系等，估计也是在GCJ02基础上加密而成的。 chenhua 
 */
    public class ClsPositionUtil
    {
        /// <summary>
        /// BAIDU_LBS_TYPE
        /// </summary>
        public static string BAIDU_LBS_TYPE = "bd09ll";
        /// <summary>
        /// pi
        /// </summary>
        public static double pi = 3.1415926535897932384626;
        /// <summary>
        /// 长半轴
        /// </summary>
        public static double a = 6378245.0;
        /// <summary>
        /// ee
        /// </summary>
        public static double ee = 0.00669342162296594323;

        /** 
         * 84 to 火星坐标系 (GCJ-02) World Geodetic System ==> Mars Geodetic System 
         *  
         * @param lat 
         * @param lon 
         * @return 
         */
        public static double[] gps84_To_Gcj02(double lat, double lon)
        {
            double[] latlon = new double[2];
            if (outOfChina(lat, lon))
            {
                latlon[0] = lat;
                latlon[1] = lon;
                return latlon;
            }
            double dLat = transformLat(lon - 105.0, lat - 35.0);
            double dLon = transformLon(lon - 105.0, lat - 35.0);
            double radLat = lat / 180.0 * pi;
            double magic = Math.Sin(radLat);
            magic = 1 - ee * magic * magic;
            double sqrtMagic = Math.Sqrt(magic);
            dLat = (dLat * 180.0) / ((a * (1 - ee)) / (magic * sqrtMagic) * pi);
            dLon = (dLon * 180.0) / (a / sqrtMagic * Math.Cos(radLat) * pi);
            latlon[0] = lat + dLat;
            latlon[1] = lon + dLon;
            return latlon;
        }

        /** 
         * * 火星坐标系 (GCJ-02) to 84 * * @param lon * @param lat * @return 
         * */
        public static double[] gcj_To_Gps84(double lat, double lon)
        {
            double[] latlon = transform(lat, lon);
            latlon[1] = lon * 2 - latlon[1];
            latlon[0] = lat * 2 - latlon[0];
            return latlon;
        }

        /** 
         * 火星坐标系 (GCJ-02) 与百度坐标系 (BD-09) 的转换算法 将 GCJ-02 坐标转换成 BD-09 坐标 
         *  
         * @param gg_lat 
         * @param gg_lon 
         */
        public static double[] gcj02_To_Bd09(double gg_lat, double gg_lon)
        {
            double[] latlon = new double[2];
            double x = gg_lon, y = gg_lat;
            double z = Math.Sqrt(x * x + y * y) + 0.00002 * Math.Sin(y * pi);
            double theta = Math.Atan2(y, x) + 0.000003 * Math.Cos(x * pi);
            latlon[1] = z * Math.Cos(theta) + 0.0065;
            latlon[0] = z * Math.Sin(theta) + 0.006;
            return latlon;
        }

        /** 
         * * 火星坐标系 (GCJ-02) 与百度坐标系 (BD-09) 的转换算法 * * 将 BD-09 坐标转换成GCJ-02 坐标 * * @param 
         * bd_lat * @param bd_lon * @return 
         */
        public static double[] bd09_To_Gcj02(double bd_lat, double bd_lon)
        {
            double[] latlon = new double[2];
            double x = bd_lon - 0.0065, y = bd_lat - 0.006;
            double z = Math.Sqrt(x * x + y * y) - 0.00002 * Math.Sin(y * pi);
            double theta = Math.Atan2(y, x) - 0.000003 * Math.Cos(x * pi);
            latlon[1] = z * Math.Cos(theta);
            latlon[0] = z * Math.Sin(theta);
            return latlon;
        }

        /** 
         * (BD-09)-->84 
         * @param bd_lat 
         * @param bd_lon 
         * @return 
         */
        public static double[] bd09_To_Gps84(double bd_lat, double bd_lon)
        {
            double[] gcj02 = ClsPositionUtil.bd09_To_Gcj02(bd_lat, bd_lon);
            double[] map84 = ClsPositionUtil.gcj_To_Gps84(gcj02[0],
                    gcj02[1]);
            return map84;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lat">lat</param>
        /// <param name="lon">lon</param>
        /// <returns></returns>
        public static bool outOfChina(double lat, double lon)
        {
            if (lon < 72.004 || lon > 137.8347)
                return true;
            if (lat < 0.8293 || lat > 55.8271)
                return true;
            return false;
        }

        /// <summary>
        /// 火星坐标转wgs84
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// <returns></returns>
        public static double[] transform(double lat, double lon)
        {
            double[] latlon = new double[2];
            if (outOfChina(lat, lon))
            {
                latlon[0] = lat;//纬度
                latlon[1] = lon;//经度
                return latlon;
            }
            double dLat = transformLat(lon - 105.0, lat - 35.0);
            double dLon = transformLon(lon - 105.0, lat - 35.0);
            double radLat = lat / 180.0 * pi;
            double magic = Math.Sin(radLat);
            magic = 1 - ee * magic * magic;
            double sqrtMagic = Math.Sqrt(magic);
            dLat = (dLat * 180.0) / ((a * (1 - ee)) / (magic * sqrtMagic) * pi);
            dLon = (dLon * 180.0) / (a / sqrtMagic * Math.Cos(radLat) * pi);
            latlon[0] = lat + dLat;
            latlon[1] = lon + dLon;
            return latlon;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <returns></returns>
        public static double transformLat(double x, double y)
        {
            double ret = -100.0 + 2.0 * x + 3.0 * y + 0.2 * y * y + 0.1 * x * y
                    + 0.2 * Math.Sqrt(Math.Abs(x));
            ret += (20.0 * Math.Sin(6.0 * x * pi) + 20.0 * Math.Sin(2.0 * x * pi)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(y * pi) + 40.0 * Math.Sin(y / 3.0 * pi)) * 2.0 / 3.0;
            ret += (160.0 * Math.Sin(y / 12.0 * pi) + 320 * Math.Sin(y * pi / 30.0)) * 2.0 / 3.0;
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <returns></returns>
        public static double transformLon(double x, double y)
        {
            double ret = 300.0 + x + 2.0 * y + 0.1 * x * x + 0.1 * x * y + 0.1
                    * Math.Sqrt(Math.Abs(x));
            ret += (20.0 * Math.Sin(6.0 * x * pi) + 20.0 * Math.Sin(2.0 * x * pi)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(x * pi) + 40.0 * Math.Sin(x / 3.0 * pi)) * 2.0 / 3.0;
            ret += (150.0 * Math.Sin(x / 12.0 * pi) + 300.0 * Math.Sin(x / 30.0
                    * pi)) * 2.0 / 3.0;
            return ret;
        }
    }
}
