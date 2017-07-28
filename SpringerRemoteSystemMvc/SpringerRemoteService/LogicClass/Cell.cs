using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TLW.Project.Springer.SpringerRemoteDataWcfService.Services
{
    class Cell
    {
        /// <summary>
        /// 标识 坡向 坡度 森林资源 是否被赋值
        /// </summary>
        public const double CONST_NULLVALUE_ASF = -999999999;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iPreRowID">派生者行号</param>
        /// <param name="iPreColumnID">派生者列号</param>
        /// <param name="pPoint">原点</param>
        /// <param name="iRowID">行号</param>
        /// <param name="iColumnID">列号</param>
        /// <param name="dWidth">单元宽</param>
        private Cell(int iPreRowID, int iPreColumnID, ESRI.ArcGIS.Geometry.IPoint pPoint, int iRowID, int iColumnID, double dWidth)
        {
            this.m_iPreRowID = iPreRowID;
            this.m_iPreColumnID = iPreColumnID;
            this.m_pPoint = pPoint;
            this.m_iRowID = iRowID;
            this.m_iColumnID = iColumnID;
            this.m_dWidth = dWidth;
        }

        ESRI.ArcGIS.Geometry.IPoint m_pPoint;
        /// <summary>
        /// 单元对应的点坐标
        /// </summary>
        public ESRI.ArcGIS.Geometry.IPoint Point
        {
            get { return m_pPoint; }
        }

        int m_iRowID = 0;
        /// <summary>
        /// 行号
        /// </summary>
        public int RowID
        {
            get { return m_iRowID; }
        }

        int m_iColumnID = 0;
        /// <summary>
        /// 列号
        /// </summary>
        public int ColumnID
        {
            get { return m_iColumnID; }
        }

        double m_dWidth = 1;
        /// <summary>
        /// 单元宽
        /// </summary>
        public double Width
        {
            get { return m_dWidth; }
        }

        double m_dAspect = CONST_NULLVALUE_ASF;
        /// <summary>
        /// 坡向值
        /// </summary>
        public double Aspect
        {
            get { return m_dAspect; }
            set { m_dAspect = value; }
        }

        double m_dSlope = CONST_NULLVALUE_ASF;
        /// <summary>
        /// 坡度值
        /// </summary>
        public double Slope
        {
            get { return m_dSlope; }
            set { m_dSlope = value; }
        }

        double m_dForest = CONST_NULLVALUE_ASF;
        /// <summary>
        /// 森林资源
        /// </summary>
        public double Forest
        {
            get { return m_dForest; }
            set { m_dForest = value; }
        }

        double m_dR0 = 0;
        /// <summary>
        /// 该单元燃烧的速度
        /// </summary>
        public double R0
        {
            get { return m_dR0; }
            set { m_dR0 = value; }
        }

        double m_dValue = 999999999;
        /// <summary>
        /// 该单元的值
        /// </summary>
        public double Value
        {
            get { return m_dValue; }
            set
            {
                m_dValue = value;
            }
        }

        bool m_IsEnd = false;
        /// <summary>
        /// 标识该单元是否已完成燃烧，不再使用
        /// </summary>
        public bool IsEnd
        {
            get { return m_IsEnd; }
            set { m_IsEnd = value; }
        }

        int m_iPreRowID = -1;
        /// <summary>
        /// 派生者行号（它是变化的，用于计算，不做长久有效）
        /// </summary>
        public int PreRowID
        {
            get { return m_iPreRowID; }
        }

        int m_iPreColumnID = -1;
        /// <summary>
        /// 派生者列号（它是变化的，用于计算，不做长久有效）
        /// </summary>
        public int PreColumnID
        {
            get { return m_iPreColumnID; }
        }

        double m_dAngle = 0;
        /// <summary>
        /// 临时方向角，即：与派生者的方向关系（它是变化的，用于计算，不做长久有效）
        /// </summary>
        public double Angle
        {
            get { return m_dAngle; }
            set { m_dAngle = value; }
        }

        /// <summary>
        /// 设置派生者
        /// </summary>
        /// <param name="iPreRowID">派生者行号</param>
        /// <param name="iPreColumnID">派生者列号</param>
        public void SetPreID(int iPreRowID, int iPreColumnID)
        {
            this.m_iPreRowID = iPreRowID;
            this.m_iPreColumnID = iPreColumnID;
        }

        /// <summary>
        /// 获取下一个单元的行列值
        /// </summary>
        /// <param name="eDirectionStyle">方向</param>
        /// <param name="iRowID">行</param>
        /// <param name="iColumnID">列</param>
        public void GetNestID(AKSON.SystemX.Web.DirectionStyle eDirectionStyle, ref int iRowID, ref int iColumnID)
        {
            switch (eDirectionStyle)
            {
                case AKSON.SystemX.Web.DirectionStyle.eEast:
                    iColumnID++;
                    break;
                case AKSON.SystemX.Web.DirectionStyle.eEastSouth:
                    iRowID--;
                    iColumnID++;
                    break;
                case AKSON.SystemX.Web.DirectionStyle.eSouth:
                    iRowID--;
                    break;
                case AKSON.SystemX.Web.DirectionStyle.eWestSouth:
                    iRowID--;
                    iColumnID--;
                    break;
                case AKSON.SystemX.Web.DirectionStyle.eWest:
                    iColumnID--;
                    break;
                case AKSON.SystemX.Web.DirectionStyle.eWestNorth:
                    iRowID++;
                    iColumnID--;
                    break;
                case AKSON.SystemX.Web.DirectionStyle.eNorth:
                    iRowID++;
                    break;
                case AKSON.SystemX.Web.DirectionStyle.eEastNorth:
                    iRowID++;
                    iColumnID++;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 获取下一个单元
        /// </summary>
        /// <param name="eDirectionStyle">方向</param>
        /// <returns>返回下一个单元</returns>
        public Cell GetNestCell(AKSON.SystemX.Web.DirectionStyle eDirectionStyle)
        {
            switch (eDirectionStyle)
            {
                case AKSON.SystemX.Web.DirectionStyle.eEast:
                    return new Cell(RowID, ColumnID, new ESRI.ArcGIS.Geometry.PointClass() { X = Point.X + Width, Y = Point.Y }, RowID, ColumnID + 1, this.Width);
                case AKSON.SystemX.Web.DirectionStyle.eEastSouth:
                    return new Cell(RowID, ColumnID, new ESRI.ArcGIS.Geometry.PointClass() { X = Point.X + Width, Y = Point.Y - Width }, RowID - 1, ColumnID + 1, this.Width);
                case AKSON.SystemX.Web.DirectionStyle.eSouth:
                    return new Cell(RowID, ColumnID, new ESRI.ArcGIS.Geometry.PointClass() { X = Point.X, Y = Point.Y - Width }, RowID - 1, ColumnID, this.Width);
                case AKSON.SystemX.Web.DirectionStyle.eWestSouth:
                    return new Cell(RowID, ColumnID, new ESRI.ArcGIS.Geometry.PointClass() { X = Point.X - Width, Y = Point.Y - Width }, RowID - 1, ColumnID - 1, this.Width);
                case AKSON.SystemX.Web.DirectionStyle.eWest:
                    return new Cell(RowID, ColumnID, new ESRI.ArcGIS.Geometry.PointClass() { X = Point.X - Width, Y = Point.Y }, RowID, ColumnID - 1, this.Width);
                case AKSON.SystemX.Web.DirectionStyle.eWestNorth:
                    return new Cell(RowID, ColumnID, new ESRI.ArcGIS.Geometry.PointClass() { X = Point.X - Width, Y = Point.Y + Width }, RowID + 1, ColumnID - 1, this.Width);
                case AKSON.SystemX.Web.DirectionStyle.eNorth:
                    return new Cell(RowID, ColumnID, new ESRI.ArcGIS.Geometry.PointClass() { X = Point.X, Y = Point.Y + Width }, RowID + 1, ColumnID, this.Width);
                case AKSON.SystemX.Web.DirectionStyle.eEastNorth:
                    return new Cell(RowID, ColumnID, new ESRI.ArcGIS.Geometry.PointClass() { X = Point.X + Width, Y = Point.Y + Width }, RowID + 1, ColumnID + 1, this.Width);
                default:
                    return null;
            }
        }

        //
        //
        //

        /// <summary>
        /// 创建一个出发的原点
        /// </summary>
        /// <param name="pPoint">原点</param>
        /// <param name="iRowID">行号</param>
        /// <param name="iColumnID">列号</param>
        /// <param name="dWidth">单元宽</param>
        /// <returns></returns>
        public static Cell OriginalCell(ESRI.ArcGIS.Geometry.IPoint pPoint, int iRowID, int iColumnID, double dWidth)
        {
            //ESRI.ArcGIS.Geometry.IPoint pPoint
            return new Cell(99999999, 99999999, pPoint, iRowID, iColumnID, dWidth);
        }
    }
}
