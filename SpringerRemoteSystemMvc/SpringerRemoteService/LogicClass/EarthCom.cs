using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TLW.Project.Springer.SpringerRemoteDataWcfService.LogicClass
{
    public class EarthCom
    {
        private IPoint GCStoPRJ(IPoint pPoint, int GCSType, int PRJType)
        {
            ISpatialReferenceFactory pSRF = new SpatialReferenceEnvironmentClass();
            pPoint.SpatialReference = pSRF.CreateGeographicCoordinateSystem(GCSType);
            pPoint.Project(pSRF.CreateProjectedCoordinateSystem(PRJType));

            return pPoint;
        }

        public IPoint PRJtoGCS(double x, double y)
        {
            IPoint pPoint = new PointClass();
            pPoint.PutCoords(x, y);
            ISpatialReferenceFactory pSRF = new SpatialReferenceEnvironmentClass();
            pPoint.SpatialReference = pSRF.CreateProjectedCoordinateSystem(2331);
            pPoint.Project(pSRF.CreateGeographicCoordinateSystem((int)esriSRGeoCSType.esriSRGeoCS_Beijing1954));
            return pPoint;
        }

        public IPoint GCStoPRJ(IPoint pPoint)
        {
            ISpatialReferenceFactory pSpatialReferenceFactory = new SpatialReferenceEnvironment();
            pPoint.SpatialReference = pSpatialReferenceFactory.CreateGeographicCoordinateSystem((int)esriSRGeoCSType.esriSRGeoCS_Krasovsky1940);
            IProjectedCoordinateSystem pProjectCoodinateSys = pSpatialReferenceFactory.CreateProjectedCoordinateSystem((int)esriSRProjCS4Type.esriSRProjCS_Xian1980_GK_Zone_17);
            ISpatialReference pSpatialReference = (ISpatialReference)pProjectCoodinateSys;
            pSpatialReference.SetDomain(17352988.066800, 18230892.557100, 2326007.173500, 3237311.062300);
            pPoint.Project(pSpatialReference);
            return pPoint;
        }

        /// 将平面坐标转换为经纬度。
        public IPoint GetProject(double x, double y)
        {
            ISpatialReferenceFactory pfactory = new SpatialReferenceEnvironmentClass();
            var flatref = pfactory.CreateProjectedCoordinateSystem(54013);
            var earthref = pfactory.CreateGeographicCoordinateSystem((int)esriSRGeoCSType.esriSRGeoCS_Beijing1954);
            IPoint pt = new ESRI.ArcGIS.Geometry.PointClass();

            pt.PutCoords(x, y);

            IGeometry geo = (IGeometry)pt;
            geo.SpatialReference = earthref;
            geo.Project(flatref);


            return pt;
        }




        private IPoint GetProject(IActiveView pActiveView, double x, double y)
        {
            try
            {
                IMap pMap = pActiveView.FocusMap;
                IPoint pt = new PointClass();
                ISpatialReferenceFactory pfactory = new SpatialReferenceEnvironmentClass();
                ISpatialReference flatref = pMap.SpatialReference;
                ISpatialReference earthref = pfactory.CreateGeographicCoordinateSystem((int)esriSRGeoCSType.esriSRGeoCS_Beijing1954);
                pt.PutCoords(x, y);
                IGeometry geo = (IGeometry)pt;
                geo.SpatialReference = earthref;
                geo.Project(flatref);
                return pt;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return null;

            }
        }
        // 将平面坐标转换为经纬度。
        private IPoint GetGeo(IActiveView pActiveView, double x, double y)
        {
            try
            {
                IMap pMap = pActiveView.FocusMap;
                IPoint pt = new PointClass();
                ISpatialReferenceFactory pfactory = new SpatialReferenceEnvironmentClass();
                ISpatialReference flatref = pMap.SpatialReference;
                ISpatialReference earthref = pfactory.CreateGeographicCoordinateSystem((int)esriSRGeoCSType.esriSRGeoCS_Beijing1954);
                pt.PutCoords(x, y);

                IGeometry geo = (IGeometry)pt;
                geo.SpatialReference = flatref;
                geo.Project(earthref);
                double xx = pt.X;
                return pt;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return null;
            }
        }
    }
}
