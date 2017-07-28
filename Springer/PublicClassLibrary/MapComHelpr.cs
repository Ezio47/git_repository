using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicClassLibrary
{
    /// <summary>
    /// 地图公共方法
    /// </summary>
    public class MapComHelpr
    {

        private const double EARTH_RADIUS = 6378.137;//地球半径
        private static double rad(double d)
        {
            return d * Math.PI / 180.0;
        }

        /// <summary>
        /// 计算两个经纬度之间的距离
        /// </summary>
        /// <param name="lat1">纬度1</param>
        /// <param name="lng1">经度1</param>
        /// <param name="lat2">纬度2</param>
        /// <param name="lng2">经度2</param>
        /// <returns></returns>
        public static double GetDistance(double lat1, double lng1, double lat2, double lng2)
        {
            double radLat1 = rad(lat1);
            double radLat2 = rad(lat2);
            double a = radLat1 - radLat2;
            double b = rad(lng1) - rad(lng2);

            double s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) +
             Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2)));
            s = s * EARTH_RADIUS;
            s = Math.Round(s * 10000) / 10000;
            return s;
        }

        /// <summary>
        /// 以米为单位
        /// </summary>
        /// <param name="lng1"></param>
        /// <param name="lat1"></param>
        /// <param name="lng2"></param>
        /// <param name="lat2"></param>
        /// <param name="gs"></param>
        /// <returns></returns>
        public static double DistanceOfTwoPoints(double lng1, double lat1, double lng2, double lat2, GaussSphere gs)
        {
            double radLat1 = Rad(lat1);
            double radLat2 = Rad(lat2);
            double a = radLat1 - radLat2;
            double b = Rad(lng1) - Rad(lng2);
            double s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) +
             Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2)));
            s = s * (gs == GaussSphere.WGS84 ? 6378137.0 : (gs == GaussSphere.Xian80 ? 6378140.0 : 6378245.0));
            s = Math.Round(s * 10000) / 10000;
            return s;
        }

        private static double Rad(double d)
        {
            return d * Math.PI / 180.0;
        }

        #region 经纬度转换方法

        /// <summary>
        /// 数字经纬度和度分秒经纬度转换 (Digital degree of latitude and longitude and vehicle to latitude and longitude conversion)
        /// </summary>
        /// <param name="digitalLati_Longi">数字经纬度</param>
        /// <return>度分秒经纬度</return>
        public static string ConvertDigitalToDegrees(string digitalLati_Longi)
        {
            double digitalDegree = Convert.ToDouble(digitalLati_Longi);
            return ConvertDigitalToDegrees(digitalDegree);
        }

        /// <summary>
        /// 数字经纬度和度分秒经纬度转换 (Digital degree of latitude and longitude and vehicle to latitude and longitude conversion)
        /// </summary>
        /// <param name="digitalDegree">数字经纬度</param>
        /// <return>度分秒经纬度</return>
        public static string ConvertDigitalToDegrees(double digitalDegree)
        {
            const double num = 60;
            int degree = (int)digitalDegree;
            double tmp = (digitalDegree - degree) * num;
            int minute = (int)tmp;
            double second = (tmp - minute) * num;
            string degrees = "" + degree + "°" + minute + "′" + second + "″";
            return degrees;
        }


        /// <summary>
        /// 度分秒经纬度(必须含有'°')和数字经纬度转换
        /// </summary>
        /// <param name="degrees">度分秒经纬度</param>
        /// <return>数字经纬度</return>
        public static double ConvertDegreesToDigital(string degrees)
        {
            const double num = 60;
            double digitalDegree = 0.0;
            int d = degrees.IndexOf('°');           //度的符号对应的 Unicode 代码为：00B0[1]（六十进制），显示为°。
            if (d < 0)
            {
                return digitalDegree;
            }
            string degree = degrees.Substring(0, d);
            digitalDegree += Convert.ToDouble(degree);

            int m = degrees.IndexOf('′');           //分的符号对应的 Unicode 代码为：2032[1]（六十进制），显示为′。
            if (m < 0)
            {
                return digitalDegree;
            }
            string minute = degrees.Substring(d + 1, m - d - 1);
            digitalDegree += ((Convert.ToDouble(minute)) / num);

            int s = degrees.IndexOf('″');           //秒的符号对应的 Unicode 代码为：2033[1]（六十进制），显示为″。
            if (s < 0)
            {
                return digitalDegree;
            }
            string second = degrees.Substring(m + 1, s - m - 1);
            digitalDegree += (Convert.ToDouble(second) / (num * num));

            return digitalDegree;
        }


        /// <summary>
        /// 度分秒经纬度(必须含有'/')和数字经纬度转换
        /// </summary>
        /// <param name="degrees">度分秒经纬度</param>
        /// <return>数字经纬度</return>
        public static double ConvertDegreesToDigital_default(string degrees)
        {
            char ch = '/';
            return ConvertDegreesToDigital(degrees, ch);
        }

        /// <summary>
        /// 度分秒经纬度和数字经纬度转换
        /// </summary>
        /// <param name="degrees">度分秒经纬度</param>
        /// <param name="cflag">分隔符</param>
        /// <return>数字经纬度</return>
        public static double ConvertDegreesToDigital(string degrees, char cflag)
        {
            const double num = 60;
            double digitalDegree = 0.0;
            int d = degrees.IndexOf(cflag);
            if (d < 0)
            {
                return digitalDegree;
            }
            string degree = degrees.Substring(0, d);
            digitalDegree += Convert.ToDouble(degree);

            int m = degrees.IndexOf(cflag, d + 1);
            if (m < 0)
            {
                return digitalDegree;
            }
            string minute = degrees.Substring(d + 1, m - d - 1);
            digitalDegree += ((Convert.ToDouble(minute)) / num);

            int s = degrees.Length;
            if (s < 0)
            {
                return digitalDegree;
            }
            string second = degrees.Substring(m + 1, s - m - 1);
            digitalDegree += (Convert.ToDouble(second) / (num * num));

            return digitalDegree;
        }

        #endregion
    }
}
