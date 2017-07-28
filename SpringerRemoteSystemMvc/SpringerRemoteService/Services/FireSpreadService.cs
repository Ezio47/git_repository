using Springer.EntityModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TLW.Project.Springer.SpringerRemoteDataWcfService.Interfaces;
using TLW.Project.Springer.SpringerRemoteDataWcfService.LogicClass;

namespace TLW.Project.Springer.SpringerRemoteDataWcfService.Services
{
    public partial class SpringerRemoteSystemService : IFireSpreadService
    {
        private const int CONST_VELOCITY_FACTOR = 1;//【一】初速度加速因子【一】
        private const int CONST_INVALIDVALUE_FOREST = 1;//【一】该类通用的无效值（InvalidValue）【一】
        private const int CONST_INVALIDVALUE_SLOPE = 0;//【一】该类通用的无效值（InvalidValue）【一】
        private const int CONST_INVALIDVALUE_ASPECT = -1;//【一】该类通用的无效值（InvalidValue）【一】
        private readonly double READONLY_ARC_P = Math.PI / 180;//【一】角度转弧度的参数【一】

        #region 属性
        private ESRI.ArcGIS.DataSourcesRaster.IRaster2 m_pForestRaster = null;
        /// <summary>
        /// 【一】森林资源栅格数据（相元值：存放的数据是不同林种每小时的燃烧速度）【一】
        /// </summary>
        public ESRI.ArcGIS.DataSourcesRaster.IRaster2 ForestRaster
        {
            get { return m_pForestRaster; }
            set { m_pForestRaster = value; }
        }

        private int m_ForestRasterBand = 0;
        /// <summary>
        /// 【一】森林资源栅格数据读取波段【一】
        /// </summary>
        public int ForestRasterBand
        {
            get { return m_ForestRasterBand; }
            set { m_ForestRasterBand = value; }
        }

        private ESRI.ArcGIS.DataSourcesRaster.IRaster2 m_pSlopeRaster = null;
        /// <summary>
        /// 【一】Slope栅格数据【一】
        /// </summary>
        public ESRI.ArcGIS.DataSourcesRaster.IRaster2 SlopeRaster
        {
            get { return m_pSlopeRaster; }
            set { m_pSlopeRaster = value; }
        }

        private int m_SlopeRasterBand = 0;
        /// <summary>
        /// 【一】Slope栅格数据读取波段【一】
        /// </summary>
        public int SlopeRasterBand
        {
            get { return m_SlopeRasterBand; }
            set { m_SlopeRasterBand = value; }
        }

        private ESRI.ArcGIS.DataSourcesRaster.IRaster2 m_pAspectRaster = null;
        /// <summary>
        /// 【一】Aspect栅格数据【一】
        /// </summary>
        public ESRI.ArcGIS.DataSourcesRaster.IRaster2 AspectRaster
        {
            get { return m_pAspectRaster; }
            set { m_pAspectRaster = value; }
        }

        private int m_AspectRasterBand = 0;
        /// <summary>
        /// 【一】Aspect栅格数据读取波段【一】
        /// </summary>
        public int AspectRasterBand
        {
            get { return m_AspectRasterBand; }
            set { m_AspectRasterBand = value; }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public void FireSpreadExClass() { }

        /// <summary>
        /// 【一】 构造函数 【一】
        /// </summary>
        /// <param name="pForestRaster">森林资源栅格数据</param>
        /// <param name="iForestRasterBand">森林资源栅格数据读取波段</param>
        /// <param name="pSlopeRaster">坡度栅格数据</param>
        /// <param name="iSlopeRasterBand">坡度栅格数据读取波段</param>
        /// <param name="pAspectRaster">坡向栅格数据</param>
        /// <param name="iAspectRasterBand">坡向栅格数据读取波段</param>
        public void FireSpreadExClass(
            ESRI.ArcGIS.DataSourcesRaster.IRaster2 pForestRaster,//【M_P】森林资源栅格数据
            int iForestRasterBand,//【M_P】森林资源栅格数据读取波段
            ESRI.ArcGIS.DataSourcesRaster.IRaster2 pSlopeRaster,//【M_P】坡度栅格数据
            int iSlopeRasterBand,//【M_P】坡度栅格数据读取波段
            ESRI.ArcGIS.DataSourcesRaster.IRaster2 pAspectRaster,//【M_P】坡向栅格数据
            int iAspectRasterBand//【M_P】坡向栅格数据读取波段
            )
        {
            this.m_pForestRaster = pForestRaster;
            this.m_ForestRasterBand = iForestRasterBand;
            this.m_pSlopeRaster = pSlopeRaster;
            this.m_SlopeRasterBand = iSlopeRasterBand;
            this.m_pAspectRaster = pAspectRaster;
            this.m_AspectRasterBand = iAspectRasterBand;
        }
        #endregion

        #region IFireSpreadParameterFilter（时期：【一】）
        /// <summary>
        /// 【一】风力过滤（即：风力大于某值后不会发生林火燃烧现象,返回True表示可以燃烧）（可以通过重写来修改）【一】
        /// </summary>
        /// <param name="dWindPower">风力</param>
        /// <returns>返回True表示可以燃烧</returns>
        public virtual bool WindPowerFilter(
           double dWindPower//风力
           )//【一】风力过滤（即：风力大于某值后不会发生林火燃烧现象,返回True表示可以燃烧）（可以通过重写来修改）【一】
        {
            return dWindPower >= 0 && dWindPower < AKSON.SystemX.Web.CityWeather.ConvertWindPower(AKSON.SystemX.Web.WindClassificationStyle.eTen);
        }

        /// <summary>
        /// 【一】湿度过滤（即：湿度高于某值后不会发生林火燃烧现象,返回True表示可以燃烧）（可以通过重写来修改）【一】
        /// </summary>
        /// <param name="dHumidity">湿度</param>
        /// <returns>返回True表示可以燃烧</returns>
        public virtual bool HumidityFilter(
            double dHumidity//湿度
            )//【一】湿度过滤（即：湿度高于某值后不会发生林火燃烧现象,返回True表示可以燃烧）（可以通过重写来修改）【一】
        {
            //http://www.kmslfh.gov.cn/show.asp?dfdso56754121=1&id=61284072830&579470%C0%A5%C3%F7%C9%AD%C1%D6%B7%C0%BB%F0%CD%F8&%DVSGH1464
            //相对湿度，是指大气中水蒸气饱和程度的百分比。
            //空气完全饱和的相对湿度为百分之百，这时水蒸气就会凝成雨、雾、露水等形式。
            //据调查，月平均相对湿度在75%以上，不发生林火；55%-70%时，可能发生火灾；
            //55%以内时，就可能发生大的火灾；特大火灾多发生在相对湿度10%-30%。
            //但这也不是绝对的，自然因子往往是综合影响森林火灾的，如相对湿度和温度都低时，
            //也不容易发生特大火灾。因此，考虑气象因子影响时，不能只抓住单一气象因子，应该考虑综合因子的作用。
            return dHumidity >= 0 && dHumidity <= 0.8;
        }

        /// <summary>
        /// 【一】温度过滤（即：温度低于某值后不会发生林火燃烧现象,返回True表示可以燃烧）（可以通过重写来修改）【一】
        /// </summary>
        /// <param name="dTemperature">温度</param>
        /// <returns>返回True表示可以燃烧</returns>
        public virtual bool TemperatureFilter(
            double dTemperature//温度
            )//【一】温度过滤（即：温度低于某值后不会发生林火燃烧现象,返回True表示可以燃烧）（可以通过重写来修改）【一】
        {
            return dTemperature > -10 && dTemperature <= 60;
        }
        #endregion

        #region IFireSpreadParameterFilter2（时期：【一】）
        /// <summary>
        /// 【一】火点过滤（即：火点是否在不可燃烧地带[如：湖泊、河流、防林带]）（可以通过重写来修改）【一】
        /// </summary>
        /// <param name="pCenterPoint">火点</param>
        /// <param name="pPolygon">无效地区</param>
        /// <returns>返回True表示可以燃烧</returns>
        public virtual bool FirePointFilter(
             ESRI.ArcGIS.Geometry.IPoint pCenterPoint,//火点
             ESRI.ArcGIS.Geometry.IPolygon pPolygon//无效地区
             )//【一】火点过滤（即：火点是否在不可燃烧地带[如：湖泊、河流、防林带]）（可以通过重写来修改）【一】
        {
            if (pCenterPoint == null || pCenterPoint.IsEmpty) return true;
            if (pPolygon == null || pPolygon.IsEmpty) return true;
            if (pCenterPoint.SpatialReference == null) return true;
            ESRI.ArcGIS.Geometry.IGeometry pGeometry = pCenterPoint;
            if (pCenterPoint.SpatialReference != pPolygon.SpatialReference)
            {
                pGeometry = (ESRI.ArcGIS.Geometry.IPoint)(((ESRI.ArcGIS.esriSystem.IClone)pGeometry).Clone());
                pGeometry.Project(pPolygon.SpatialReference);
            }
            return ((ESRI.ArcGIS.Geometry.IRelationalOperator)pPolygon).Contains(pGeometry);
        }

        /// <summary>
        /// 【一】火点过滤（即：火点是否在不可燃烧地带[如：湖泊、河流、防林带]）（可以通过重写来修改）【一】
        /// </summary>
        /// <param name="pCenterPoint">火点</param>
        /// <param name="pFeatureClass">无效地区要素集合（面状要素）</param>
        /// <returns>返回True表示可以燃烧</returns>
        public virtual bool FirePointFilter(
            ESRI.ArcGIS.Geometry.IPoint pCenterPoint,//火点
            ESRI.ArcGIS.Geodatabase.IFeatureClass pFeatureClass//无效地区要素集合（面状要素）
             )//【一】火点过滤（即：火点是否在不可燃烧地带[如：湖泊、河流、防林带]）（可以通过重写来修改）【一】
        {
            if (pFeatureClass == null) return true;
            ESRI.ArcGIS.Geometry.IGeometry pGeometry = pCenterPoint;
            ESRI.ArcGIS.Geodatabase.IGeoDataset pGeoDataset = (ESRI.ArcGIS.Geodatabase.IGeoDataset)pFeatureClass;
            if (pCenterPoint.SpatialReference != pGeoDataset.SpatialReference)
            {
                pGeometry = (ESRI.ArcGIS.Geometry.IPoint)(((ESRI.ArcGIS.esriSystem.IClone)pGeometry).Clone());
                pGeometry.Project(pGeoDataset.SpatialReference);
            }
            ESRI.ArcGIS.Geodatabase.ISpatialFilter pSpatialFilter = new ESRI.ArcGIS.Geodatabase.SpatialFilterClass();
            pSpatialFilter.GeometryField = "SHAPE";
            pSpatialFilter.Geometry = pGeometry;
            pSpatialFilter.SpatialRel = ESRI.ArcGIS.Geodatabase.esriSpatialRelEnum.esriSpatialRelIntersects;
            return pFeatureClass.FeatureCount(pSpatialFilter) <= 0;
        }
        #endregion

        #region IFireSpreadEx（时期：【一】）
        /// <summary>
        /// 【一】 获取初速度 【一】
        /// </summary>
        /// <param name="dWindSpeed">风力（单位：米/每秒）</param>
        /// <param name="dHumidity">湿度值</param>
        /// <param name="dTemperature">温度值</param>
        /// <returns>初速度</returns>
        public virtual double GetVelocity(
             double dWindSpeed,//风力（单位：米/每秒）
             double dHumidity,//湿度值
             double dTemperature//温度值
            )
        {
            return (0.0299 * dTemperature + 0.047 * (int)AKSON.SystemX.Web.CityWeather.ConvertWindPower(dWindSpeed) + 0.009 * (100 - dHumidity) - 0.304) * CONST_VELOCITY_FACTOR;
        }

        /// <summary>
        /// 【一】 获取格网宽度 【一】
        /// </summary>
        /// <param name="dV">初速度</param>
        /// <param name="dTime">分段值（即：相对速度的时间值）（单位：分钟）</param>
        /// <returns></returns>
        public virtual double GetGridWidth(
             double dV,//初速度
             double dTime//分段值（即：相对速度的时间值）（单位：分钟）
            )
        {
            return dTime * dV / 10;
        }


        private void FireSpread_DG(IList<Cell> cellList, CellMatrix cellMatrix, Cell originalCell, double dTime, double R0, double V, double dWindDirection)//【一】递归【一】
        {
            List<Cell> cellList_temp = new List<Cell>();
            //
            foreach (Cell one in cellList)
            {
                this.FireSpread(cellMatrix, one, dTime, R0, V, dWindDirection, cellList_temp);
            }
            //
            if (cellList_temp.Count > 0) this.FireSpread_DG(cellList_temp, cellMatrix, originalCell, dTime, R0, V, dWindDirection);
        }
        private void FireSpread(CellMatrix cellMatrix, Cell originalCell, double dTime, double R0, double V, double dWindDirection, IList<Cell> cellList)//【一】某点向四周扩散【一】
        {
            double dTemp = 0;
            Cell cellYP = null;
            Cell cellXP = null;
            Cell cellZP = null;
            Cell cellSP = null;
            Cell cellFX = null;
            //
            double dAspect = originalCell.Aspect;
            //double dAspect = this.GetRasterCellValue_Aspect(originalCell.Point, this.m_pAspectRaster, this.AspectRasterBand);
            if (dAspect < 0)
            {
                //cellYP = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eEast));
                //cellYP.Angle = 90;
                //cellXP = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eSouth));
                //cellXP.Angle = 180;
                //cellZP = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eWest));
                //cellZP.Angle = 270;
                //cellSP = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eNorth));
                //cellSP.Angle = 0;
                ////
                //if ((dWindDirection >= 0 && dWindDirection < 22.5) || (dWindDirection > 337.5 && dWindDirection <= 360))//风方向与上坡方向重合
                //{ cellFX = cellSP; }
                //else if (dWindDirection >= 22.5 && dWindDirection < 67.5)//东北
                //{ cellFX = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eEastNorth)); cellFX.Angle = 45; }
                //else if (dWindDirection >= 67.5 && dWindDirection < 112.5)//风方向与右平坡方向重合
                //{ cellFX = cellYP; }
                //else if (dWindDirection >= 112.5 && dWindDirection < 157.5)//东南
                //{ cellFX = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eEastSouth)); cellFX.Angle = 135; }
                //else if (dWindDirection >= 157.5 && dWindDirection < 202.5)//风方向与下坡方向重合
                //{ cellFX = cellXP; }
                //else if (dWindDirection >= 202.5 && dWindDirection < 247.5)//西南
                //{ cellFX = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eWestSouth)); cellFX.Angle = 225; }
                //else if (dWindDirection >= 247.5 && dWindDirection < 292.5)//风方向与左平坡方向重合
                //{ cellFX = cellZP; }
                //else if (dWindDirection >= 292.5 && dWindDirection < 337.5)//西北
                //{ cellFX = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eWestNorth)); cellFX.Angle = 315; }
                //
                #region 无坡度信息
                if ((dWindDirection >= 0 && dWindDirection < 22.5) || (dWindDirection > 337.5 && dWindDirection <= 360))//南
                {
                    cellYP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastNorth));//
                    cellYP.Angle = 45;
                    cellXP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastSouth));
                    cellXP.Angle = 135;
                    cellZP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestSouth));
                    cellZP.Angle = 225;
                    cellSP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestNorth));
                    cellSP.Angle = 315;
                    //
                    cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eNorth));
                    cellFX.Angle = 180;
                }
                else if (dWindDirection >= 22.5 && dWindDirection < 67.5)//西南
                {
                    cellYP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eNorth));
                    cellYP.Angle = 0;
                    cellXP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEast));
                    cellXP.Angle = 90;
                    cellZP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eSouth));
                    cellZP.Angle = 180;
                    cellSP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWest));
                    cellSP.Angle = 270;
                    //
                    cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastNorth));
                    cellFX.Angle = 45;
                }
                else if (dWindDirection >= 67.5 && dWindDirection < 112.5)//西
                {
                    cellYP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestNorth));
                    cellYP.Angle = 315;
                    cellXP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastNorth));
                    cellXP.Angle = 45;
                    cellZP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastSouth));
                    cellZP.Angle = 135;
                    cellSP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestSouth));
                    cellSP.Angle = 225;
                    //
                    cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEast));
                    cellFX.Angle = 90;
                }
                else if (dWindDirection >= 112.5 && dWindDirection < 157.5)//西北
                {
                    cellYP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWest));
                    cellYP.Angle = 270;
                    cellXP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eNorth));
                    cellXP.Angle = 0;
                    cellZP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEast));
                    cellZP.Angle = 90;
                    cellSP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eSouth));
                    cellSP.Angle = 180;
                    //
                    cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastSouth));
                    cellFX.Angle = 135;
                }
                else if (dWindDirection >= 157.5 && dWindDirection < 202.5)//北
                {
                    cellYP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestSouth));
                    cellYP.Angle = 225;
                    cellXP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestNorth));
                    cellXP.Angle = 315;
                    cellZP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastNorth));
                    cellZP.Angle = 45;
                    cellSP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastSouth));
                    cellSP.Angle = 135;
                    //
                    cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eSouth));
                    cellFX.Angle = 180;
                }
                else if (dWindDirection >= 202.5 && dWindDirection < 247.5)//东北
                {
                    cellYP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eSouth));
                    cellYP.Angle = 180;
                    cellXP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWest));
                    cellXP.Angle = 270;
                    cellZP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eNorth));
                    cellZP.Angle = 0;
                    cellSP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEast));
                    cellSP.Angle = 90;
                    //
                    cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestSouth));
                    cellFX.Angle = 225;
                }
                else if (dWindDirection >= 247.5 && dWindDirection < 292.5)//东
                {
                    cellYP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastSouth));
                    cellYP.Angle = 135;
                    cellXP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestSouth));
                    cellXP.Angle = 225;
                    cellZP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestNorth));
                    cellZP.Angle = 315;
                    cellSP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastNorth));
                    cellSP.Angle = 45;
                    //
                    cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWest));
                    cellFX.Angle = 270;
                }
                else if (dWindDirection >= 292.5 && dWindDirection < 337.5)//东南
                {//
                    cellYP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEast));
                    cellYP.Angle = 90;
                    cellXP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eSouth));
                    cellXP.Angle = 180;
                    cellZP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWest));
                    cellZP.Angle = 270;
                    cellSP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eNorth));
                    cellSP.Angle = 0;
                    //
                    cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestNorth));
                    cellFX.Angle = 315;
                }
                else
                {
                    cellYP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEast));
                    cellYP.Angle = 90;
                    cellXP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eSouth));
                    cellXP.Angle = 180;
                    cellZP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWest));
                    cellZP.Angle = 270;
                    cellSP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eNorth));
                    cellSP.Angle = 0;
                }
                #endregion
                //
                #region 无坡度信息（已抛弃）
                //if ((dWindDirection >= 0 && dWindDirection < 22.5) || (dWindDirection > 337.5 && dWindDirection <= 360))//南
                //{
                //    cellYP = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eEast));//
                //    cellYP.Angle = 90;
                //    cellXP = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eSouth));
                //    cellXP.Angle = 180;
                //    cellZP = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eWest));
                //    cellZP.Angle = 270;
                //    cellSP = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eNorth));
                //    cellSP.Angle = 0;
                //    //
                //    cellFX = cellSP;
                //}
                //else if (dWindDirection >= 22.5 && dWindDirection < 67.5)//西南
                //{
                //    cellYP = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eEastSouth));
                //    cellYP.Angle = 135;
                //    cellXP = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eWestSouth));
                //    cellXP.Angle = 225;
                //    cellZP = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eEastNorth));
                //    cellZP.Angle = 315;
                //    cellSP = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eEastNorth));
                //    cellSP.Angle = 45;
                //    //
                //    cellFX = cellSP;
                //}
                //else if (dWindDirection >= 67.5 && dWindDirection < 112.5)//西
                //{
                //    cellYP = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eSouth));
                //    cellYP.Angle = 180;
                //    cellXP = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eWest));
                //    cellXP.Angle = 270;
                //    cellZP = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eNorth));
                //    cellZP.Angle = 0;
                //    cellSP = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eEast));
                //    cellSP.Angle = 90;
                //    //
                //    cellFX = cellSP;
                //}
                //else if (dWindDirection >= 112.5 && dWindDirection < 157.5)//西北
                //{
                //    cellYP = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eWestSouth));
                //    cellYP.Angle = 225;
                //    cellXP = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eWestNorth));
                //    cellXP.Angle = 315;
                //    cellZP = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eEastNorth));
                //    cellZP.Angle = 0;
                //    cellSP = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eEastSouth));
                //    cellSP.Angle = 135;
                //    //
                //    cellFX = cellSP;
                //}
                //else if (dWindDirection >= 157.5 && dWindDirection < 202.5)//北
                //{
                //    cellYP = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eWest));
                //    cellYP.Angle = 270;
                //    cellXP = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eNorth));
                //    cellXP.Angle = 0;
                //    cellZP = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eEast));
                //    cellZP.Angle = 90;
                //    cellSP = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eSouth));
                //    cellSP.Angle = 180;
                //    //
                //    cellFX = cellSP;
                //}
                //else if (dWindDirection >= 202.5 && dWindDirection < 247.5)//东北
                //{
                //    cellYP = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eWestNorth));
                //    cellYP.Angle = 315;
                //    cellXP = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eEastNorth));
                //    cellXP.Angle = 45;
                //    cellZP = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eEastSouth));
                //    cellZP.Angle = 135;
                //    cellSP = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eWestSouth));
                //    cellSP.Angle = 225;
                //    //
                //    cellFX = cellSP;
                //}
                //else if (dWindDirection >= 247.5 && dWindDirection < 292.5)//东
                //{
                //    cellYP = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eNorth));
                //    cellYP.Angle = 0;
                //    cellXP = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eEast));
                //    cellXP.Angle = 90;
                //    cellZP = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eSouth));
                //    cellZP.Angle = 180;
                //    cellSP = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eWest));
                //    cellSP.Angle = 270;
                //    //
                //    cellFX = cellSP;
                //}
                //else if (dWindDirection >= 292.5 && dWindDirection < 337.5)//东南
                //{//
                //    cellYP = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eEastNorth));
                //    cellYP.Angle = 45;
                //    cellXP = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eEastSouth));
                //    cellXP.Angle = 135;
                //    cellZP = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eWestSouth));
                //    cellZP.Angle = 225;
                //    cellSP = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eWestNorth));
                //    cellSP.Angle = 315;
                //    //
                //    cellFX = cellSP;
                //}
                //else
                //{
                //    cellYP = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eEast));
                //    cellYP.Angle = 90;
                //    cellXP = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eSouth));
                //    cellXP.Angle = 180;
                //    cellZP = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eWest));
                //    cellZP.Angle = 270;
                //    cellSP = cellMatrix.GetCell(originalCell.GetNestCell(DirectionStyle.eNorth));
                //    cellSP.Angle = 0;
                //}
                #endregion
            }
            else
            {
                #region 有坡度信息（反方向 - 已抛弃）
                //double dQ = this.Q(dAspect, dWindDirection);
                //if ((dAspect >= 0 && dAspect < 22.5) || (dAspect > 337.5 && dAspect <= 360))//北
                //{
                //    cellYP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEast));
                //    cellYP.Angle = 90;
                //    cellXP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eSouth));
                //    cellXP.Angle = 180;
                //    cellZP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWest));
                //    cellZP.Angle = 270;
                //    cellSP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eNorth));
                //    cellSP.Angle = 0;
                //    //
                //    if ((dQ >= 0 && dQ < 22.5) || (dQ > 337.5 && dQ <= 360))//风方向与上坡方向重合
                //    { cellFX = cellSP; }
                //    else if (dQ >= 22.5 && dQ < 67.5)//东北
                //    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastNorth)); cellFX.Angle = 45; }
                //    else if (dQ >= 67.5 && dQ < 112.5)//风方向与右平坡方向重合
                //    { cellFX = cellYP; }
                //    else if (dQ >= 112.5 && dQ < 157.5)//东南
                //    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastSouth)); cellFX.Angle = 135; }
                //    else if (dQ >= 157.5 && dQ < 202.5)//风方向与下坡方向重合
                //    { cellFX = cellXP; }
                //    else if (dQ >= 202.5 && dQ < 247.5)//西南
                //    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestSouth)); cellFX.Angle = 225; }
                //    else if (dQ >= 247.5 && dQ < 292.5)//风方向与左平坡方向重合
                //    { cellFX = cellZP; }
                //    else if (dQ >= 292.5 && dQ < 337.5)//西北
                //    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestNorth)); cellFX.Angle = 315; }
                //}
                //else if (dAspect >= 22.5 && dAspect < 67.5)//东北
                //{
                //    cellYP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastSouth));
                //    cellYP.Angle = 135;
                //    cellXP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestSouth));
                //    cellXP.Angle = 225;
                //    cellZP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestNorth));
                //    cellZP.Angle = 315;
                //    cellSP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastNorth));
                //    cellSP.Angle = 45;
                //    //
                //    if ((dQ >= 0 && dQ < 22.5) || (dQ > 337.5 && dQ <= 360))//风方向与上坡方向重合
                //    { cellFX = cellSP; }
                //    else if (dQ >= 22.5 && dQ < 67.5)//东
                //    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEast)); cellFX.Angle = 90; }
                //    else if (dQ >= 67.5 && dQ < 112.5)//风方向与右平坡方向重合
                //    { cellFX = cellYP; }
                //    else if (dQ >= 112.5 && dQ < 157.5)//南
                //    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eSouth)); cellFX.Angle = 180; }
                //    else if (dQ >= 157.5 && dQ < 202.5)//风方向与下坡方向重合
                //    { cellFX = cellXP; }
                //    else if (dQ >= 202.5 && dQ < 247.5)//西
                //    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWest)); cellFX.Angle = 270; }
                //    else if (dQ >= 247.5 && dQ < 292.5)//风方向与左平坡方向重合
                //    { cellFX = cellZP; }
                //    else if (dQ >= 292.5 && dQ < 337.5)//北
                //    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eNorth)); cellFX.Angle = 0; }
                //}
                //else if (dAspect >= 67.5 && dAspect < 112.5)//东
                //{
                //    cellYP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eSouth));
                //    cellYP.Angle = 180;
                //    cellXP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWest));
                //    cellXP.Angle = 270;
                //    cellZP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eNorth));
                //    cellZP.Angle = 0;
                //    cellSP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEast));
                //    cellSP.Angle = 90;
                //    //
                //    if ((dQ >= 0 && dQ < 22.5) || (dQ > 337.5 && dQ <= 360))//风方向与上坡方向重合
                //    { cellFX = cellSP; }
                //    else if (dQ >= 22.5 && dQ < 67.5)//东南
                //    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastSouth)); cellFX.Angle = 135; }
                //    else if (dQ >= 67.5 && dQ < 112.5)//风方向与右平坡方向重合
                //    { cellFX = cellYP; }
                //    else if (dQ >= 112.5 && dQ < 157.5)//西南
                //    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestSouth)); cellFX.Angle = 225; }
                //    else if (dQ >= 157.5 && dQ < 202.5)//风方向与下坡方向重合
                //    { cellFX = cellXP; }
                //    else if (dQ >= 202.5 && dQ < 247.5)//西北
                //    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestNorth)); cellFX.Angle = 315; }
                //    else if (dQ >= 247.5 && dQ < 292.5)//风方向与左平坡方向重合
                //    { cellFX = cellZP; }
                //    else if (dQ >= 292.5 && dQ < 337.5)//东北
                //    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastNorth)); cellFX.Angle = 45; }
                //}
                //else if (dAspect >= 112.5 && dAspect < 157.5)//东南
                //{
                //    cellYP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestSouth));
                //    cellYP.Angle = 225;
                //    cellXP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestNorth));
                //    cellXP.Angle = 315;
                //    cellZP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastNorth));
                //    cellZP.Angle = 45;
                //    cellSP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastSouth));
                //    cellSP.Angle = 135;
                //    //
                //    if ((dQ >= 0 && dQ < 22.5) || (dQ > 337.5 && dQ <= 360))//风方向与上坡方向重合
                //    { cellFX = cellSP; }
                //    else if (dQ >= 22.5 && dQ < 67.5)//南
                //    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eSouth)); cellFX.Angle = 180; }
                //    else if (dQ >= 67.5 && dQ < 112.5)//风方向与右平坡方向重合
                //    { cellFX = cellYP; }
                //    else if (dQ >= 112.5 && dQ < 157.5)//西
                //    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWest)); cellFX.Angle = 270; }
                //    else if (dQ >= 157.5 && dQ < 202.5)//风方向与下坡方向重合
                //    { cellFX = cellXP; }
                //    else if (dQ >= 202.5 && dQ < 247.5)//北
                //    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eNorth)); cellFX.Angle = 0; }
                //    else if (dQ >= 247.5 && dQ < 292.5)//风方向与左平坡方向重合
                //    { cellFX = cellZP; }
                //    else if (dQ >= 292.5 && dQ < 337.5)//东
                //    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEast)); cellFX.Angle = 90; }
                //}
                //else if (dAspect >= 157.5 && dAspect < 202.5)//南
                //{
                //    cellYP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWest));
                //    cellYP.Angle = 270;
                //    cellXP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eNorth));
                //    cellXP.Angle = 0;
                //    cellZP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEast));
                //    cellZP.Angle = 90;
                //    cellSP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eSouth));
                //    cellSP.Angle = 180;
                //    //
                //    if ((dQ >= 0 && dQ < 22.5) || (dQ > 337.5 && dQ <= 360))//风方向与上坡方向重合
                //    { cellFX = cellSP; }
                //    else if (dQ >= 22.5 && dQ < 67.5)//西南
                //    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestSouth)); cellFX.Angle = 225; }
                //    else if (dQ >= 67.5 && dQ < 112.5)//风方向与右平坡方向重合
                //    { cellFX = cellYP; }
                //    else if (dQ >= 112.5 && dQ < 157.5)//西北
                //    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestNorth)); cellFX.Angle = 315; }
                //    else if (dQ >= 157.5 && dQ < 202.5)//风方向与下坡方向重合
                //    { cellFX = cellXP; }
                //    else if (dQ >= 202.5 && dQ < 247.5)//东北
                //    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastNorth)); cellFX.Angle = 45; }
                //    else if (dQ >= 247.5 && dQ < 292.5)//风方向与左平坡方向重合
                //    { cellFX = cellZP; }
                //    else if (dQ >= 292.5 && dQ < 337.5)//东南
                //    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastSouth)); cellFX.Angle = 135; }
                //}
                //else if (dAspect >= 202.5 && dAspect < 247.5)//西南
                //{
                //    cellYP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestNorth));
                //    cellYP.Angle = 315;
                //    cellXP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastNorth));
                //    cellXP.Angle = 45;
                //    cellZP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastSouth));
                //    cellZP.Angle = 135;
                //    cellSP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestSouth));
                //    cellSP.Angle = 225;
                //    //
                //    if ((dQ >= 0 && dQ < 22.5) || (dQ > 337.5 && dQ <= 360))//风方向与上坡方向重合
                //    { cellFX = cellSP; }
                //    else if (dQ >= 22.5 && dQ < 67.5)//西
                //    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWest)); cellFX.Angle = 270; }
                //    else if (dQ >= 67.5 && dQ < 112.5)//风方向与右平坡方向重合
                //    { cellFX = cellYP; }
                //    else if (dQ >= 112.5 && dQ < 157.5)//北
                //    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eNorth)); cellFX.Angle = 0; }
                //    else if (dQ >= 157.5 && dQ < 202.5)//风方向与下坡方向重合
                //    { cellFX = cellXP; }
                //    else if (dQ >= 202.5 && dQ < 247.5)//东
                //    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEast)); cellFX.Angle = 90; }
                //    else if (dQ >= 247.5 && dQ < 292.5)//风方向与左平坡方向重合
                //    { cellFX = cellZP; }
                //    else if (dQ >= 292.5 && dQ < 337.5)//南
                //    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eSouth)); cellFX.Angle = 180; }
                //}
                //else if (dAspect >= 247.5 && dAspect < 292.5)//西
                //{
                //    cellYP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eNorth));
                //    cellYP.Angle = 0;
                //    cellXP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEast));
                //    cellXP.Angle = 90;
                //    cellZP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eSouth));
                //    cellZP.Angle = 180;
                //    cellSP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWest));
                //    cellSP.Angle = 270;
                //    //
                //    if ((dQ >= 0 && dQ < 22.5) || (dQ > 337.5 && dQ <= 360))//风方向与上坡方向重合
                //    { cellFX = cellSP; }
                //    else if (dQ >= 22.5 && dQ < 67.5)//西北
                //    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestNorth)); cellFX.Angle = 315; }
                //    else if (dQ >= 67.5 && dQ < 112.5)//风方向与右平坡方向重合
                //    { cellFX = cellYP; }
                //    else if (dQ >= 112.5 && dQ < 157.5)//东北
                //    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastNorth)); cellFX.Angle = 45; }
                //    else if (dQ >= 157.5 && dQ < 202.5)//风方向与下坡方向重合
                //    { cellFX = cellXP; }
                //    else if (dQ >= 202.5 && dQ < 247.5)//东南
                //    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastSouth)); cellFX.Angle = 135; }
                //    else if (dQ >= 247.5 && dQ < 292.5)//风方向与左平坡方向重合
                //    { cellFX = cellZP; }
                //    else if (dQ >= 292.5 && dQ < 337.5)//西南
                //    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestSouth)); cellFX.Angle = 225; }
                //}
                //else if (dAspect >= 292.5 && dAspect < 337.5)//西北
                //{
                //    cellYP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastNorth));
                //    cellYP.Angle = 45;
                //    cellXP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastSouth));
                //    cellXP.Angle = 135;
                //    cellZP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestSouth));
                //    cellZP.Angle = 225;
                //    cellSP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestNorth));
                //    cellSP.Angle = 315;
                //    //
                //    if ((dQ >= 0 && dQ < 22.5) || (dQ > 337.5 && dQ <= 360))//风方向与上坡方向重合
                //    { cellFX = cellSP; }
                //    else if (dQ >= 22.5 && dQ < 67.5)//北
                //    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eNorth)); cellFX.Angle = 0; }
                //    else if (dQ >= 67.5 && dQ < 112.5)//风方向与右平坡方向重合
                //    { cellFX = cellYP; }
                //    else if (dQ >= 112.5 && dQ < 157.5)//东
                //    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEast)); cellFX.Angle = 90; }
                //    else if (dQ >= 157.5 && dQ < 202.5)//风方向与下坡方向重合
                //    { cellFX = cellXP; }
                //    else if (dQ >= 202.5 && dQ < 247.5)//南
                //    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eSouth)); cellFX.Angle = 180; }
                //    else if (dQ >= 247.5 && dQ < 292.5)//风方向与左平坡方向重合
                //    { cellFX = cellZP; }
                //    else if (dQ >= 292.5 && dQ < 337.5)//西
                //    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWest)); cellFX.Angle = 270; }
                //}
                //else
                //{
                //    return;
                //}
                #endregion
                #region 有坡度信息
                double dQ = this.Q(dAspect, dWindDirection);
                if ((dAspect >= 0 && dAspect < 22.5) || (dAspect > 337.5 && dAspect <= 360))//北
                {
                    cellYP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWest));
                    cellYP.Angle = 270;
                    cellXP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eNorth));
                    cellXP.Angle = 0;
                    cellZP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEast));
                    cellZP.Angle = 90;
                    cellSP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eSouth));
                    cellSP.Angle = 180;
                    //
                    if ((dQ >= 0 && dQ < 22.5) || (dQ > 337.5 && dQ <= 360))//风方向与上坡方向重合
                    { cellFX = cellSP; }
                    else if (dQ >= 22.5 && dQ < 67.5)//西南
                    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestSouth)); cellFX.Angle = 225; }
                    else if (dQ >= 67.5 && dQ < 112.5)//风方向与右平坡方向重合
                    { cellFX = cellYP; }
                    else if (dQ >= 112.5 && dQ < 157.5)//西北
                    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestNorth)); cellFX.Angle = 315; }
                    else if (dQ >= 157.5 && dQ < 202.5)//风方向与下坡方向重合
                    { cellFX = cellXP; }
                    else if (dQ >= 202.5 && dQ < 247.5)//东北
                    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastNorth)); cellFX.Angle = 45; }
                    else if (dQ >= 247.5 && dQ < 292.5)//风方向与左平坡方向重合
                    { cellFX = cellZP; }
                    else if (dQ >= 292.5 && dQ < 337.5)//东南
                    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastSouth)); cellFX.Angle = 135; }
                }
                else if (dAspect >= 22.5 && dAspect < 67.5)//东北
                {
                    cellYP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestNorth));
                    cellYP.Angle = 315;
                    cellXP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastNorth));
                    cellXP.Angle = 45;
                    cellZP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastSouth));
                    cellZP.Angle = 135;
                    cellSP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestSouth));
                    cellSP.Angle = 225;
                    //
                    if ((dQ >= 0 && dQ < 22.5) || (dQ > 337.5 && dQ <= 360))//风方向与上坡方向重合
                    { cellFX = cellSP; }
                    else if (dQ >= 22.5 && dQ < 67.5)//西
                    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWest)); cellFX.Angle = 270; }
                    else if (dQ >= 67.5 && dQ < 112.5)//风方向与右平坡方向重合
                    { cellFX = cellYP; }
                    else if (dQ >= 112.5 && dQ < 157.5)//北
                    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eNorth)); cellFX.Angle = 0; }
                    else if (dQ >= 157.5 && dQ < 202.5)//风方向与下坡方向重合
                    { cellFX = cellXP; }
                    else if (dQ >= 202.5 && dQ < 247.5)//东
                    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEast)); cellFX.Angle = 90; }
                    else if (dQ >= 247.5 && dQ < 292.5)//风方向与左平坡方向重合
                    { cellFX = cellZP; }
                    else if (dQ >= 292.5 && dQ < 337.5)//南
                    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eSouth)); cellFX.Angle = 180; }
                }
                else if (dAspect >= 67.5 && dAspect < 112.5)//东
                {
                    cellYP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eNorth));
                    cellYP.Angle = 0;
                    cellXP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEast));
                    cellXP.Angle = 90;
                    cellZP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eSouth));
                    cellZP.Angle = 180;
                    cellSP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWest));
                    cellSP.Angle = 270;
                    //
                    if ((dQ >= 0 && dQ < 22.5) || (dQ > 337.5 && dQ <= 360))//风方向与上坡方向重合
                    { cellFX = cellSP; }
                    else if (dQ >= 22.5 && dQ < 67.5)//西北
                    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestNorth)); cellFX.Angle = 315; }
                    else if (dQ >= 67.5 && dQ < 112.5)//风方向与右平坡方向重合
                    { cellFX = cellYP; }
                    else if (dQ >= 112.5 && dQ < 157.5)//东北
                    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastNorth)); cellFX.Angle = 45; }
                    else if (dQ >= 157.5 && dQ < 202.5)//风方向与下坡方向重合
                    { cellFX = cellXP; }
                    else if (dQ >= 202.5 && dQ < 247.5)//东南
                    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastSouth)); cellFX.Angle = 135; }
                    else if (dQ >= 247.5 && dQ < 292.5)//风方向与左平坡方向重合
                    { cellFX = cellZP; }
                    else if (dQ >= 292.5 && dQ < 337.5)//西南
                    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestSouth)); cellFX.Angle = 225; }
                }
                else if (dAspect >= 112.5 && dAspect < 157.5)//东南
                {
                    cellYP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastNorth));
                    cellYP.Angle = 45;
                    cellXP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastSouth));
                    cellXP.Angle = 135;
                    cellZP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestSouth));
                    cellZP.Angle = 225;
                    cellSP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestNorth));
                    cellSP.Angle = 315;
                    //
                    if ((dQ >= 0 && dQ < 22.5) || (dQ > 337.5 && dQ <= 360))//风方向与上坡方向重合
                    { cellFX = cellSP; }
                    else if (dQ >= 22.5 && dQ < 67.5)//北
                    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eNorth)); cellFX.Angle = 0; }
                    else if (dQ >= 67.5 && dQ < 112.5)//风方向与右平坡方向重合
                    { cellFX = cellYP; }
                    else if (dQ >= 112.5 && dQ < 157.5)//东
                    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEast)); cellFX.Angle = 90; }
                    else if (dQ >= 157.5 && dQ < 202.5)//风方向与下坡方向重合
                    { cellFX = cellXP; }
                    else if (dQ >= 202.5 && dQ < 247.5)//南
                    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eSouth)); cellFX.Angle = 180; }
                    else if (dQ >= 247.5 && dQ < 292.5)//风方向与左平坡方向重合
                    { cellFX = cellZP; }
                    else if (dQ >= 292.5 && dQ < 337.5)//西
                    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWest)); cellFX.Angle = 270; }
                }
                else if (dAspect >= 157.5 && dAspect < 202.5)//南
                {
                    cellYP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEast));
                    cellYP.Angle = 90;
                    cellXP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eSouth));
                    cellXP.Angle = 180;
                    cellZP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWest));
                    cellZP.Angle = 270;
                    cellSP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eNorth));
                    cellSP.Angle = 0;
                    //
                    if ((dQ >= 0 && dQ < 22.5) || (dQ > 337.5 && dQ <= 360))//风方向与上坡方向重合
                    { cellFX = cellSP; }
                    else if (dQ >= 22.5 && dQ < 67.5)//东北
                    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastNorth)); cellFX.Angle = 45; }
                    else if (dQ >= 67.5 && dQ < 112.5)//风方向与右平坡方向重合
                    { cellFX = cellYP; }
                    else if (dQ >= 112.5 && dQ < 157.5)//东南
                    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastSouth)); cellFX.Angle = 135; }
                    else if (dQ >= 157.5 && dQ < 202.5)//风方向与下坡方向重合
                    { cellFX = cellXP; }
                    else if (dQ >= 202.5 && dQ < 247.5)//西南
                    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestSouth)); cellFX.Angle = 225; }
                    else if (dQ >= 247.5 && dQ < 292.5)//风方向与左平坡方向重合
                    { cellFX = cellZP; }
                    else if (dQ >= 292.5 && dQ < 337.5)//西北
                    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestNorth)); cellFX.Angle = 315; }
                }
                else if (dAspect >= 202.5 && dAspect < 247.5)//西南
                {
                    cellYP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastSouth));
                    cellYP.Angle = 135;
                    cellXP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestSouth));
                    cellXP.Angle = 225;
                    cellZP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestNorth));
                    cellZP.Angle = 315;
                    cellSP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastNorth));
                    cellSP.Angle = 45;
                    //
                    if ((dQ >= 0 && dQ < 22.5) || (dQ > 337.5 && dQ <= 360))//风方向与上坡方向重合
                    { cellFX = cellSP; }
                    else if (dQ >= 22.5 && dQ < 67.5)//东
                    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEast)); cellFX.Angle = 90; }
                    else if (dQ >= 67.5 && dQ < 112.5)//风方向与右平坡方向重合
                    { cellFX = cellYP; }
                    else if (dQ >= 112.5 && dQ < 157.5)//南
                    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eSouth)); cellFX.Angle = 180; }
                    else if (dQ >= 157.5 && dQ < 202.5)//风方向与下坡方向重合
                    { cellFX = cellXP; }
                    else if (dQ >= 202.5 && dQ < 247.5)//西
                    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWest)); cellFX.Angle = 270; }
                    else if (dQ >= 247.5 && dQ < 292.5)//风方向与左平坡方向重合
                    { cellFX = cellZP; }
                    else if (dQ >= 292.5 && dQ < 337.5)//北
                    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eNorth)); cellFX.Angle = 0; }
                }
                else if (dAspect >= 247.5 && dAspect < 292.5)//西
                {
                    cellYP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eSouth));
                    cellYP.Angle = 180;
                    cellXP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWest));
                    cellXP.Angle = 270;
                    cellZP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eNorth));
                    cellZP.Angle = 0;
                    cellSP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEast));
                    cellSP.Angle = 90;
                    //
                    if ((dQ >= 0 && dQ < 22.5) || (dQ > 337.5 && dQ <= 360))//风方向与上坡方向重合
                    { cellFX = cellSP; }
                    else if (dQ >= 22.5 && dQ < 67.5)//东南
                    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastSouth)); cellFX.Angle = 135; }
                    else if (dQ >= 67.5 && dQ < 112.5)//风方向与右平坡方向重合
                    { cellFX = cellYP; }
                    else if (dQ >= 112.5 && dQ < 157.5)//西南
                    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestSouth)); cellFX.Angle = 225; }
                    else if (dQ >= 157.5 && dQ < 202.5)//风方向与下坡方向重合
                    { cellFX = cellXP; }
                    else if (dQ >= 202.5 && dQ < 247.5)//西北
                    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestNorth)); cellFX.Angle = 315; }
                    else if (dQ >= 247.5 && dQ < 292.5)//风方向与左平坡方向重合
                    { cellFX = cellZP; }
                    else if (dQ >= 292.5 && dQ < 337.5)//东北
                    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastNorth)); cellFX.Angle = 45; }
                }
                else if (dAspect >= 292.5 && dAspect < 337.5)//西北
                {
                    cellYP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestSouth));
                    cellYP.Angle = 225;
                    cellXP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWestNorth));
                    cellXP.Angle = 315;
                    cellZP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastNorth));
                    cellZP.Angle = 45;
                    cellSP = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEastSouth));
                    cellSP.Angle = 135;
                    //
                    if ((dQ >= 0 && dQ < 22.5) || (dQ > 337.5 && dQ <= 360))//风方向与上坡方向重合
                    { cellFX = cellSP; }
                    else if (dQ >= 22.5 && dQ < 67.5)//南
                    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eSouth)); cellFX.Angle = 180; }
                    else if (dQ >= 67.5 && dQ < 112.5)//风方向与右平坡方向重合
                    { cellFX = cellYP; }
                    else if (dQ >= 112.5 && dQ < 157.5)//西
                    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eWest)); cellFX.Angle = 270; }
                    else if (dQ >= 157.5 && dQ < 202.5)//风方向与下坡方向重合
                    { cellFX = cellXP; }
                    else if (dQ >= 202.5 && dQ < 247.5)//北
                    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eNorth)); cellFX.Angle = 0; }
                    else if (dQ >= 247.5 && dQ < 292.5)//风方向与左平坡方向重合
                    { cellFX = cellZP; }
                    else if (dQ >= 292.5 && dQ < 337.5)//东
                    { cellFX = cellMatrix.GetCell(originalCell.GetNestCell(AKSON.SystemX.Web.DirectionStyle.eEast)); cellFX.Angle = 90; }
                }
                else
                {
                    return;
                }
                #endregion
            }
            //
            #region 计算
            if (cellFX != null)//存在独立的风方向
            {
                if (cellFX.RowID != originalCell.PreRowID || cellFX.ColumnID != originalCell.PreColumnID)
                {
                    cellFX.R0 = this.FXValueR(cellFX, R0, V, dWindDirection);
                    dTemp = originalCell.Value + this.Len(originalCell, cellFX) / cellFX.R0;
                    if (dTime < dTemp)
                    {
                        cellFX.IsEnd = true;
                    }
                    else if (cellFX.Value > dTemp)//
                    {
                        cellFX.Value = dTemp;
                        cellFX.IsEnd = false;
                        cellFX.SetPreID(originalCell.RowID, originalCell.ColumnID);
                        if (!cellList.Contains(cellFX)) cellList.Add(cellFX);
                    }
                    //Console.WriteLine("FX:" + cellFX.RowID + " - " + cellFX.ColumnID + " - " + cellFX.Value + " - " + dTemp.ToString() + " - " + cellFX.R0.ToString());
                }
            }
            //
            if (cellYP.RowID != originalCell.PreRowID || cellYP.ColumnID != originalCell.PreColumnID)
            {
                cellYP.R0 = this.YPValueR(cellYP, R0, V, dWindDirection);
                dTemp = originalCell.Value + this.Len(originalCell, cellYP) / cellYP.R0;
                if (dTime < dTemp)
                {
                    cellYP.IsEnd = true;
                }
                else if (cellYP.Value > dTemp)//
                {
                    cellYP.Value = dTemp;
                    cellYP.IsEnd = false;
                    cellYP.SetPreID(originalCell.RowID, originalCell.ColumnID);
                    if (!cellList.Contains(cellYP)) cellList.Add(cellYP);
                }
                //Console.WriteLine("YP:" + cellYP.RowID + " - " + cellYP.ColumnID + " - " + cellYP.Value + " - " + dTemp.ToString() + " - " + cellYP.R0.ToString());
            }
            if (cellXP.RowID != originalCell.PreRowID || cellXP.ColumnID != originalCell.PreColumnID)
            {
                cellXP.R0 = this.XPValueR(cellXP, R0, V, dWindDirection);
                dTemp = originalCell.Value + this.Len(originalCell, cellXP) / cellXP.R0;
                if (dTime < dTemp)
                {
                    cellXP.IsEnd = true;
                }
                else if (cellXP.Value > dTemp)//
                {
                    cellXP.Value = dTemp;
                    cellXP.IsEnd = false;
                    cellXP.SetPreID(originalCell.RowID, originalCell.ColumnID);
                    if (!cellList.Contains(cellXP)) cellList.Add(cellXP);
                }
                //Console.WriteLine("XP:" + cellXP.RowID + " - " + cellXP.ColumnID + " - " + cellXP.Value + " - " + dTemp.ToString() + " - " + cellXP.R0.ToString());
            }
            if (cellZP.RowID != originalCell.PreRowID || cellZP.ColumnID != originalCell.PreColumnID)
            {
                cellZP.R0 = this.ZPValueR(cellZP, R0, V, dWindDirection);
                dTemp = originalCell.Value + this.Len(originalCell, cellZP) / cellZP.R0;
                if (dTime < dTemp)
                {
                    cellZP.IsEnd = true;
                }
                else if (cellZP.Value > dTemp)//
                {
                    cellZP.Value = dTemp;
                    cellZP.IsEnd = false;
                    cellZP.SetPreID(originalCell.RowID, originalCell.ColumnID);
                    if (!cellList.Contains(cellZP)) cellList.Add(cellZP);
                }
                //Console.WriteLine("ZP:" + cellZP.RowID + " - " + cellZP.ColumnID + " - " + cellZP.Value + " - " + dTemp.ToString() + " - " + cellZP.R0.ToString());
            }
            if (cellSP.RowID != originalCell.PreRowID || cellSP.ColumnID != originalCell.PreColumnID)
            {
                cellSP.R0 = this.SPValueR(cellSP, R0, V, dWindDirection);
                dTemp = originalCell.Value + this.Len(originalCell, cellSP) / cellSP.R0;
                if (dTime < dTemp)
                {
                    cellSP.IsEnd = true;
                }
                else if (cellSP.Value > dTemp)//
                {
                    cellSP.Value = dTemp;
                    cellSP.IsEnd = false;
                    cellSP.SetPreID(originalCell.RowID, originalCell.ColumnID);
                    if (!cellList.Contains(cellSP)) cellList.Add(cellSP);
                }
                //Console.WriteLine("SP:" + cellSP.RowID + " - " + cellSP.ColumnID + " - " + cellSP.Value + " - " + dTemp.ToString() + " - " + cellSP.R0.ToString());
            }
            //
            //Console.WriteLine("-----------------------------------------------------------");
            #endregion
        }
        private double SPValueR(Cell cell, double R0, double V, double dWindDirection)//【一】上坡【一】
        {
            if (cell.Aspect == Cell.CONST_NULLVALUE_ASF) cell.Aspect = this.GetRasterCellValue_Aspect(cell.Point, this.AspectRaster, this.AspectRasterBand);
            if (cell.Forest == Cell.CONST_NULLVALUE_ASF) cell.Forest = this.GetRasterCellValue_Forest(cell.Point, this.ForestRaster, this.ForestRasterBand);
            if (cell.Aspect < 0)//只要没有坡向，坡度必然不存在。
            {
                //return R0 * cell.Forest;
                //double dQ2 = this.Q2(cell.Angle, dWindDirection);
                //if (dQ2 > 90)
                //{
                //    return R0 * cell.Forest *
                //        Math.Exp(
                //             -0.1783 * V * Math.Cos(
                //                               READONLY_ARC_P * (180 - dQ2)));
                //}
                return R0 * cell.Forest *
                    Math.Exp(
                         0.1783 * V * Math.Cos(
                                           READONLY_ARC_P * this.Q2(cell.Angle, dWindDirection)));
            }
            //
            return R0 * cell.Forest *
                Math.Exp(
                     3.533 * Math.Pow(
                                  Math.Tan(
                                       READONLY_ARC_P * this.GetRasterCellValue_Slope(cell.Point, this.SlopeRaster, this.SlopeRasterBand)), 1.2)) *
                Math.Exp(
                     0.178 * V * Math.Cos(
                                      READONLY_ARC_P * this.Q(cell.Aspect, dWindDirection)));
        }
        private double XPValueR(Cell cell, double R0, double V, double dWindDirection)//【一】下坡【一】
        {
            if (cell.Aspect == Cell.CONST_NULLVALUE_ASF) cell.Aspect = this.GetRasterCellValue_Aspect(cell.Point, this.AspectRaster, this.AspectRasterBand);
            if (cell.Forest == Cell.CONST_NULLVALUE_ASF) cell.Forest = this.GetRasterCellValue_Forest(cell.Point, this.ForestRaster, this.ForestRasterBand);
            if (cell.Aspect < 0)//只要没有坡向，坡度必然不存在。
            {
                //return R0 * cell.Forest;
                //double dQ2 = this.Q2(cell.Angle, dWindDirection);
                //if (dQ2 > 90)
                //{
                //    return R0 * cell.Forest *
                //        Math.Exp(
                //             -0.1783 * V * Math.Cos(
                //                               READONLY_ARC_P * (180 - dQ2)));
                //}
                return R0 * cell.Forest *
                    Math.Exp(
                         0.1783 * V * Math.Cos(
                                           READONLY_ARC_P * this.Q2(cell.Angle, dWindDirection)));
            }
            //
            return R0 * cell.Forest *
                Math.Exp(
                     -3.533 * Math.Pow(
                                  Math.Tan(
                                       READONLY_ARC_P * this.GetRasterCellValue_Slope(cell.Point, this.SlopeRaster, this.SlopeRasterBand)), 1.2)) *
                Math.Exp(
                     0.178 * V * Math.Cos(
                                      READONLY_ARC_P * (180 - this.Q(cell.Aspect, dWindDirection))));
        }
        private double ZPValueR(Cell cell, double R0, double V, double dWindDirection)//【一】左坡【一】
        {
            if (cell.Aspect == Cell.CONST_NULLVALUE_ASF) cell.Aspect = this.GetRasterCellValue_Aspect(cell.Point, this.AspectRaster, this.AspectRasterBand);
            if (cell.Forest == Cell.CONST_NULLVALUE_ASF) cell.Forest = this.GetRasterCellValue_Forest(cell.Point, this.ForestRaster, this.ForestRasterBand);
            if (cell.Aspect < 0)//只要没有坡向，坡度必然不存在。
            {
                //return R0 * cell.Forest;
                //double dQ2 = this.Q2(cell.Angle, dWindDirection);
                //if (dQ2 > 90)
                //{
                //    return R0 * cell.Forest *
                //        Math.Exp(
                //             -0.1783 * V * Math.Cos(
                //                               READONLY_ARC_P * (180 - dQ2)));
                //}
                return R0 * cell.Forest *
                    Math.Exp(
                         0.1783 * V * Math.Cos(
                                           READONLY_ARC_P * this.Q2(cell.Angle, dWindDirection)));
            }
            //
            return R0 * cell.Forest *
                Math.Exp(
                     0.1783 * V * Math.Cos(
                                       READONLY_ARC_P * (this.Q(cell.Aspect, dWindDirection) + 90)));
        }
        private double YPValueR(Cell cell, double R0, double V, double dWindDirection)//【一】右坡【一】
        {
            if (cell.Aspect == Cell.CONST_NULLVALUE_ASF) cell.Aspect = this.GetRasterCellValue_Aspect(cell.Point, this.AspectRaster, this.AspectRasterBand);
            if (cell.Forest == Cell.CONST_NULLVALUE_ASF) cell.Forest = this.GetRasterCellValue_Forest(cell.Point, this.ForestRaster, this.ForestRasterBand);
            if (cell.Aspect < 0)//只要没有坡向，坡度必然不存在。
            {
                //return R0 * cell.Forest;
                //double dQ2 = this.Q2(cell.Angle, dWindDirection);
                //if (dQ2 > 90)
                //{
                //    return R0 * cell.Forest *
                //        Math.Exp(
                //             -0.1783 * V * Math.Cos(
                //                               READONLY_ARC_P * (180 - dQ2)));
                //}
                return R0 * cell.Forest *
                    Math.Exp(
                         0.1783 * V * Math.Cos(
                                           READONLY_ARC_P * this.Q2(cell.Angle, dWindDirection)));
            }
            //
            return R0 * cell.Forest *
                Math.Exp(
                     0.1783 * V * Math.Cos(
                                       READONLY_ARC_P * (this.Q(cell.Aspect, dWindDirection) - 90)));
        }
        private double FXValueR(Cell cell, double R0, double V, double dWindDirection)//【一】风向【一】
        {
            if (cell.Aspect == Cell.CONST_NULLVALUE_ASF) cell.Aspect = this.GetRasterCellValue_Aspect(cell.Point, this.AspectRaster, this.AspectRasterBand);
            if (cell.Slope == Cell.CONST_NULLVALUE_ASF) cell.Slope = this.GetRasterCellValue_Slope(cell.Point, this.SlopeRaster, this.SlopeRasterBand);
            if (cell.Forest == Cell.CONST_NULLVALUE_ASF) cell.Forest = this.GetRasterCellValue_Forest(cell.Point, this.ForestRaster, this.ForestRasterBand);
            if (cell.Aspect < 0)//只要没有坡向，坡度必然不存在。
            {
                return R0 * cell.Forest * Math.Exp(0.1783 * V);
            }
            //
            double dQ = this.Q(cell.Aspect, dWindDirection);
            if (dQ > 90 && dQ < 270)
            {
                return R0 * cell.Forest *
                    Math.Exp(
                         -3.533 * Math.Pow(
                                      Math.Tan(
                                           READONLY_ARC_P * (cell.Slope * Math.Cos(READONLY_ARC_P * (180 - dQ)))), 1.2)) *
                    Math.Exp(0.1783 * V);
            }
            else
            {
                return R0 * cell.Forest *
                    Math.Exp(
                         3.533 * Math.Pow(
                                      Math.Tan(
                                           READONLY_ARC_P * (cell.Slope * Math.Cos(READONLY_ARC_P * dQ))), 1.2)) *
                    Math.Exp(0.1783 * V);
            }
        }
        private double Q(double dAspect, double dWindDirection)//【一】坡向与风向顺时针夹角参数【一】
        {
            double dA = dWindDirection - dAspect;
            if (dA < 0) dA += 360;
            return dA;
        }
        private double Q2(double dAngle, double dWindDirection)//【一】单元临时角度与风向夹角参数【一】
        {
            double dA = dWindDirection - dAngle;
            if (dA < 0) dA += 360;
            //Console.WriteLine(dAngle + " - " + dWindDirection + " - " + (dA > 180 ? 360 - dA : dA).ToString());
            return dA > 180 ? 360 - dA : dA;
        }
        private double Len(Cell cell1, Cell cell2)//【一】两个矩阵的距离【一】
        {
            //Console.WriteLine(Math.Sqrt(Math.Pow(cell2.Point.X - cell1.Point.X, 2) + Math.Pow(cell2.Point.Y - cell1.Point.Y, 2)));
            return Math.Sqrt(Math.Pow(cell2.Point.X - cell1.Point.X, 2) + Math.Pow(cell2.Point.Y - cell1.Point.Y, 2));
        }
        private double GetRasterCellValue_Forest(
            ESRI.ArcGIS.Geometry.IPoint pPoint,//查询点
            ESRI.ArcGIS.DataSourcesRaster.IRaster2 pRaster2,//栅格数据
            int iBand//查询波段对象
            )//【一】获取某坐标下的相元值【一】
        {
            if (pRaster2 == null) return CONST_INVALIDVALUE_FOREST;
            //
            int iCol = pRaster2.ToPixelColumn(pPoint.X);
            int iRow = pRaster2.ToPixelRow(pPoint.Y);
            //
            object objPixelValue = pRaster2.GetPixelValue(iBand, iCol, iRow);
            if (objPixelValue == null) return CONST_INVALIDVALUE_FOREST;
            return Convert.ToDouble(objPixelValue);
            //double dRasterValue = CONST_INVALIDVALUE_FOREST;
            //double.TryParse(objPixelValue.ToString(), out dRasterValue);
            //return dRasterValue;
        }
        private double GetRasterCellValue_Slope(
            ESRI.ArcGIS.Geometry.IPoint pPoint,//查询点
            ESRI.ArcGIS.DataSourcesRaster.IRaster2 pRaster2,//栅格数据
            int iBand//查询波段对象
            )//【一】获取某坐标下的相元值【一】
        {
            if (pRaster2 == null) return CONST_INVALIDVALUE_SLOPE;
            //
            int iCol = pRaster2.ToPixelColumn(pPoint.X);
            int iRow = pRaster2.ToPixelRow(pPoint.Y);
            //
            object objPixelValue = pRaster2.GetPixelValue(iBand, iCol, iRow);
            if (objPixelValue == null) return CONST_INVALIDVALUE_SLOPE;
            return Convert.ToDouble(objPixelValue);
            //double dRasterValue = CONST_INVALIDVALUE;
            //double.TryParse(objPixelValue.ToString(), out dRasterValue);
            //return dRasterValue;
        }
        private double GetRasterCellValue_Aspect(
            ESRI.ArcGIS.Geometry.IPoint pPoint,//查询点
            ESRI.ArcGIS.DataSourcesRaster.IRaster2 pRaster2,//栅格数据
            int iBand//查询波段对象
            )//【一】获取某坐标下的相元值【一】
        {
            if (pRaster2 == null) return CONST_INVALIDVALUE_ASPECT;
            //
            int iCol = pRaster2.ToPixelColumn(pPoint.X);
            int iRow = pRaster2.ToPixelRow(pPoint.Y);
            //
            object objPixelValue = pRaster2.GetPixelValue(iBand, iCol, iRow);
            if (objPixelValue == null) return CONST_INVALIDVALUE_ASPECT;
            return Convert.ToDouble(objPixelValue);
            //double dRasterValue = CONST_INVALIDVALUE;
            //double.TryParse(objPixelValue.ToString(), out dRasterValue);
            //return dRasterValue;
        }
        private bool Homogeneity(
            Cell originalCell,//辐散中心点
            CellMatrix cellMatrix//矩阵
            )//【一】是否在均值平原上【一】
        {
            int iNum = 0;
            //
            if (originalCell == null) return true;
            //
            if (originalCell.Aspect < 0)
            {
                foreach (Cell one in cellMatrix)
                {
                    if (one.Aspect < 0 && one.Forest == originalCell.Forest) iNum++;
                }
            }
            //
            return cellMatrix.Count <= 0 ? true : (double)iNum / (double)cellMatrix.Count >= 0.5;
        }
        private bool Homogeneity(
            Cell originalCell,//辐散中心点
            IList<Cell> cellList//矩阵
            )//【一】矩阵边界是否在均值平原上【一】
        {
            int iNum = 0;
            //
            if (originalCell == null) return true;
            //
            if (originalCell.Aspect < 0)
            {
                foreach (Cell one in cellList)
                {
                    if (one.Aspect < 0 && one.Forest == originalCell.Forest) iNum++;
                }
            }
            //
            return cellList.Count <= 0 ? true : (double)iNum / (double)cellList.Count >= 0.5;
        }


        #endregion


        #region 火情蔓延
        /// <summary>
        /// 【一】林火蔓延【一】
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
        public List<Points> FireSpread(
             string jd,//经度
             string wd,//纬度
             double dWindDirection,//风向角度值（北风：180，东风：270，南风：0，西风：45以此类推，即：方向与北方向的顺时针夹角，切记有效值为0至360）
             double dWindSpeed,//风力（单位：米/每秒）
             double dHumidity,//湿度值
             double dTemperature,//温度值
             double dW,//网格宽度（小于0时，系统自动分配）
             double dTime,//分段值（即：相对速度的时间值）（单位：分钟）
             bool bConvexHull//凸多边形
            )
        {
            //System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            //stopwatch.Start();
            ////
            //ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop);
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);
            //将经纬度坐标转换成投影坐标
            var help = new EarthCom();
            ESRI.ArcGIS.Geometry.IPoint CenterPoint = new ESRI.ArcGIS.Geometry.PointClass();
            CenterPoint.X = Convert.ToDouble(jd);
            CenterPoint.Y = Convert.ToDouble(wd);
            CenterPoint.PutCoords(CenterPoint.X, CenterPoint.Y);
            var pointTY = help.GCStoPRJ(CenterPoint);

            ESRI.ArcGIS.Geometry.IPoint pCenterPoint = new ESRI.ArcGIS.Geometry.PointClass();
            pCenterPoint.PutCoords(Convert.ToDouble(pointTY.X.ToString()), Convert.ToDouble(pointTY.Y.ToString()));

            if (dWindSpeed <= 0) dWindDirection = -360;
            else if (dWindDirection < 0 || dWindDirection > 360) dWindSpeed = 0;
            //
            double dV = this.GetVelocity(dWindSpeed, dHumidity, dTemperature);
            //

            #region 扩散矩阵
            CellMatrix cellMatrix = new CellMatrix();
            //
            Cell originalCell = cellMatrix.GetCell(Cell.OriginalCell(pCenterPoint, 0, 0, dW > 0 ? dW : this.GetGridWidth(dV, dTime)));
            originalCell.Value = 0;
            originalCell.R0 = dV;
            originalCell.Aspect = this.GetRasterCellValue_Aspect(originalCell.Point, this.m_pAspectRaster, this.AspectRasterBand);
            //
            IList<Cell> cellList = new List<Cell>();
            cellList.Add(originalCell);
            this.FireSpread_DG(
                cellList,
                cellMatrix,
                originalCell,
                dTime,
                originalCell.R0,
                dWindSpeed,
                dWindDirection);
            #endregion
            //
            #region 获取面
            cellList = cellMatrix.GetMatrixOutLine();
            IList<Cell> cellList2 = cellMatrix.GetConvexHull(cellList);//获取边线凸壳
            if (bConvexHull)//凸壳
            {
                cellList = cellList2;
            }
            else //当边界点在平原上时（即：cell.Aspect < 0），移除该点
            {
                Cell cell;
                for (int i = cellList.Count - 1; i >= 0; i--)
                {
                    cell = cellList[i];
                    if (cellList2.Contains(cell)) continue;
                    //
                    if (cell.Aspect < 0) cellList.RemoveAt(i);
                }
                //
                cellMatrix.RemoveStep(cellList, 2 * originalCell.Width);//移除栅格阶梯，最大阶梯长度是2倍Width
                cellMatrix.RemoveCanine(cellList, Math.PI / 2, 2 * originalCell.Width);//移除栅格尖齿，夹角为Math.PI/2，最大阶梯长度是2倍Width
            }

            //    object obj = Type.Missing;
            var list = new List<Points>();//投影坐标集合

            for (int i = 0; i < cellList.Count; i++)
            {
                var point = new Points();
                point.JD = cellList[i].Point.X.ToString();
                point.WD = cellList[i].Point.Y.ToString();
                var record = help.PRJtoGCS(Convert.ToDouble(point.JD), Convert.ToDouble(point.WD));
                list.Add(new Points { JD = record.X.ToString(), WD = record.Y.ToString() });
            }
            return list;
            #endregion
        }
        #endregion
    }
}
