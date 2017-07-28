using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TLW.Project.Springer.SpringerRemoteDataWcfService.Services
{
    class CellMatrix : IEnumerable
    {
        List<Cell> m_CellList = new List<Cell>();

        /// <summary>
        /// 单元个数
        /// </summary>
        public int Count { get { return this.m_CellList.Count; } }

        /// <summary>
        /// 查找单元
        /// </summary>
        /// <param name="iRowID"></param>
        /// <param name="iColumnID"></param>
        /// <returns></returns>
        public Cell FindCell(int iRowID, int iColumnID)
        {
            Cell cell;
            for (int i = this.m_CellList.Count - 1; i >= 0; i--)
            {
                cell = this.m_CellList[i];
                if (cell.RowID == iRowID && cell.ColumnID == iColumnID)
                {
                    return cell;
                }
            }
            return null;
        }

        /// <summary>
        /// 尝试获取一个单元
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public Cell GetCell(Cell cell)
        {
            Cell cellTemp;
            for (int i = this.m_CellList.Count - 1; i >= 0; i--)
            {
                cellTemp = this.m_CellList[i];
                if (cellTemp.RowID == cell.RowID && cellTemp.ColumnID == cell.ColumnID)
                {
                    return cellTemp;
                }
            }
            this.m_CellList.Add(cell);
            return cell;
        }

        /// <summary>
        /// 最大和最小行列值
        /// </summary>
        /// <param name="iMinColumnID"></param>
        /// <param name="iMaxColumnID"></param>
        /// <param name="iMinRowID"></param>
        /// <param name="iMaxRowID"></param>
        public void GetMatrixExtent(out int iMinColumnID, out int iMaxColumnID, out int iMinRowID, out int iMaxRowID)
        {
            iMinColumnID = 0;
            iMaxColumnID = 0;
            iMinRowID = 0;
            iMaxRowID = 0;
            //
            foreach (Cell one in this.m_CellList)
            {
                if (one.ColumnID < iMinColumnID) iMinColumnID = one.ColumnID;
                if (one.ColumnID > iMaxColumnID) iMaxColumnID = one.ColumnID;
                if (one.RowID < iMinRowID) iMinRowID = one.RowID;
                if (one.RowID > iMaxRowID) iMaxRowID = one.RowID;
            }
        }

        /// <summary>
        /// 获取外轮廓
        /// </summary>
        /// <returns></returns>
        public IList<Cell> GetMatrixOutLine()
        {
            int iMinColumnID, iMaxColumnID, iMinRowID, iMaxRowID;
            this.GetMatrixExtent(out iMinColumnID, out iMaxColumnID, out iMinRowID, out  iMaxRowID);
            //
            #region 获取矩阵边框节点
            IList<Cell> cellList = new List<Cell>();
            Cell cell = null;
            for (int i = iMinColumnID; i < iMaxColumnID; i++)
            {
                for (int j = iMaxRowID; j >= iMinRowID; j--)
                {
                    cell = this.FindCell(j, i);
                    if (cell != null) break;
                }
                //
                if (cell == null || cellList.Contains(cell)) continue;
                cellList.Add(cell);
            }
            //
            if (cellList.Count > 0) cell = cellList[cellList.Count - 1];
            for (int j = (cell == null ? iMaxRowID : cell.RowID); j > iMinRowID; j--)
            {
                for (int i = iMaxColumnID; i >= iMinColumnID; i--)
                {
                    cell = this.FindCell(j, i);
                    if (cell != null) break;
                }
                //
                if (cell == null || cellList.Contains(cell)) continue;
                cellList.Add(cell);
            }
            //
            if (cellList.Count > 0) cell = cellList[cellList.Count - 1];
            for (int i = (cell == null ? iMaxColumnID : cell.ColumnID); i > iMinColumnID; i--)
            {
                for (int j = iMinRowID; j <= iMaxRowID; j++)
                {
                    cell = this.FindCell(j, i);
                    if (cell != null) break;
                }
                //
                if (cell == null || cellList.Contains(cell)) continue;
                cellList.Add(cell);
            }
            //
            if (cellList.Count > 0)
            {
                cell = cellList[cellList.Count - 1];
                iMaxRowID = cellList[0].RowID;
            }
            for (int j = (cell == null ? iMinRowID : cell.RowID); j < iMaxRowID; j++)
            {
                for (int i = iMinColumnID; i <= iMaxColumnID; i++)
                {
                    cell = this.FindCell(j, i);
                    if (cell != null) break;
                }
                //
                if (cell == null || cellList.Contains(cell)) continue;
                cellList.Add(cell);
            }
            #endregion
            //
            #region 移除在同一直线上的节点
            double dStep = 0.1;
            Cell cell2 = cellList[0];
            double dCS = cell2.Point.X - cell.Point.X;
            double dTan = dCS == 0 ? -999999999 : (cell2.Point.Y - cell.Point.Y) / dCS;
            for (int i = 1; i < cellList.Count; )
            {
                cell = cellList[i - 1];
                cell2 = cellList[i];
                double dCS_temp = cell2.Point.X - cell.Point.X;
                double dTan_temp = dCS_temp == 0 ? -999999999 : (cell2.Point.Y - cell.Point.Y) / dCS_temp;
                //Console.WriteLine(Math.Abs(dTan - dTan_temp));
                if (Math.Abs(dTan - dTan_temp) <= dStep)
                {
                    cellList.RemoveAt(i - 1);
                }
                else
                {
                    dTan = dTan_temp;
                    i++;
                }
            }
            if (cellList.Count >= 3)
            {
                cell = cellList[cellList.Count - 1];
                cell2 = cellList[0];
                double dCS_temp = cell2.Point.X - cell.Point.X;
                double dTan_temp = dCS_temp == 0 ? -999999999 : (cell2.Point.Y - cell.Point.Y) / dCS_temp;
                //Console.WriteLine(Math.Abs(dTan - dTan_temp));
                if (Math.Abs(dTan - dTan_temp) <= dStep)
                {
                    cellList.RemoveAt(cellList.Count - 1);
                }
                else
                {
                    dTan = dTan_temp;
                }
            }
            #endregion
            //
            return cellList;
        }

        /// <summary>
        /// 获取凸壳
        /// </summary>
        /// <param name="cellList"></param>
        /// <returns></returns>
        public IList<Cell> GetConvexHull(IList<Cell> cellList)
        {
            IList<Cell> cellList2 = new List<Cell>();
            //
            if (cellList.Count >= 3)
            {
                Cell cellBottom = cellList[0];
                Cell cellTop = cellList[0];
                foreach (Cell one in cellList)
                {
                    if (cellBottom.Point.Y > one.Point.Y) cellBottom = one;
                    else if (cellBottom.Point.Y == one.Point.Y && cellBottom.Point.X < one.Point.X) cellBottom = one;
                    //
                    if (cellTop.Point.Y < one.Point.Y) cellTop = one;
                    else if (cellTop.Point.Y == one.Point.Y && cellTop.Point.X > one.Point.X) cellTop = one;
                }
                //
                cellList2.Insert(0, cellBottom);
                this.ConvexHull_DG_Left(cellList, cellBottom, cellList2, cellTop);
                cellList2.Insert(0, cellTop);
                this.ConvexHull_DG_Right(cellList, cellTop, cellList2, cellBottom);
            }
            //
            return cellList2;
        }
        private void ConvexHull_DG_Left(IEnumerable pEnumerable, Cell cell, IList<Cell> cellList2, Cell cellTop)
        {
            double dLen = 0;
            Cell cell3 = null;
            double dCos = -1;
            foreach (Cell one in pEnumerable)
            {
                if (cell.RowID == one.RowID && cell.ColumnID == one.ColumnID) continue;
                //
                double dX_temp = one.Point.X - cell.Point.X;
                double dY_temp = one.Point.Y - cell.Point.Y;
                if (dY_temp < 0) continue;
                double dLen_temp = Math.Sqrt(dX_temp * dX_temp + dY_temp * dY_temp);
                if (dLen_temp == 0) continue;
                double dCos_temp = dX_temp / dLen_temp;
                //
                if (dCos < dCos_temp)
                {
                    dCos = dCos_temp;
                    dLen = dLen_temp;
                    cell3 = one;
                }
                else if (dCos == dCos_temp && dLen_temp >= dLen)
                {
                    dCos = dCos_temp;
                    dLen = dLen_temp;
                    cell3 = one;
                }
            }
            //
            if (cell3 == null || (cell3.RowID == cellTop.RowID && cell3.ColumnID == cellTop.ColumnID)) return;
            //
            cellList2.Insert(0, cell3);
            this.ConvexHull_DG_Left(pEnumerable, cell3, cellList2, cellTop);
        }
        private void ConvexHull_DG_Right(IEnumerable pEnumerable, Cell cell, IList<Cell> cellList2, Cell cellBottom)
        {
            double dLen = 0;
            Cell cell3 = null;
            double dCos = 1;
            foreach (Cell one in pEnumerable)
            {
                if (cell.RowID == one.RowID && cell.ColumnID == one.ColumnID) continue;
                //
                //
                double dX_temp = one.Point.X - cell.Point.X;
                double dY_temp = one.Point.Y - cell.Point.Y;
                if (dY_temp > 0) continue;
                double dLen_temp = Math.Sqrt(dX_temp * dX_temp + dY_temp * dY_temp);
                if (dLen_temp == 0) continue;
                double dCos_temp = dX_temp / dLen_temp;
                //
                if (dCos > dCos_temp)
                {
                    dCos = dCos_temp;
                    dLen = dLen_temp;
                    cell3 = one;
                }
                else if (dCos == dCos_temp && dLen_temp >= dLen)
                {
                    dCos = dCos_temp;
                    dLen = dLen_temp;
                    cell3 = one;
                }
            }
            //
            if (cell3 == null || cell3 == cellBottom) return;
            //
            cellList2.Insert(0, cell3);
            this.ConvexHull_DG_Right(pEnumerable, cell3, cellList2, cellBottom);
        }

        /// <summary>
        /// 移除阶梯
        /// </summary>
        /// <param name="cellList"></param>
        /// <param name="dStepMaxLen"></param>
        public void RemoveStep(IList<Cell> cellList, double dStepMaxLen)
        {
            if (cellList.Count > 4)
            {
                double dStepTan = 0.1;
                double dStepLen = cellList[0].Width;
                //
                for (int i = 3; i < cellList.Count; )
                {
                    if (this.RemoveStep_DG(cellList[i - 3], cellList[i - 2], cellList[i - 1], cellList[i], cellList, dStepMaxLen, dStepTan, dStepLen)) continue;
                    i++;
                }
                //
                if (cellList.Count >= 4)
                {
                    this.RemoveStep_DG(cellList[cellList.Count - 3], cellList[cellList.Count - 2], cellList[cellList.Count - 1], cellList[0], cellList, dStepMaxLen, dStepTan, dStepLen);
                    if (cellList.Count >= 4)
                    {
                        this.RemoveStep_DG(cellList[cellList.Count - 2], cellList[cellList.Count - 1], cellList[0], cellList[1], cellList, dStepMaxLen, dStepTan, dStepLen);
                        if (cellList.Count >= 4)
                        {
                            this.RemoveStep_DG(cellList[cellList.Count - 1], cellList[0], cellList[1], cellList[2], cellList, dStepMaxLen, dStepTan, dStepLen);
                        }
                    }
                }
            }
        }
        private bool RemoveStep_DG(Cell cell, Cell cell2, Cell cell3, Cell cell4, IList<Cell> cellList, double dStepMaxLen, double dStepTan, double dStepLen)
        {
            double dBCS_12;
            double dCS_12;
            double dTan_12;
            double dBCS_34;
            double dCS_34;
            double dTan_34;
            double dLen23;
            double dLen34;
            //
            dBCS_12 = cell2.Point.Y - cell.Point.Y;
            dCS_12 = cell2.Point.X - cell.Point.X;
            dTan_12 = dCS_12 == 0 ? -999999999 : dBCS_12 / dCS_12;
            //
            dBCS_34 = cell4.Point.Y - cell3.Point.Y;
            dCS_34 = cell4.Point.X - cell3.Point.X;
            dTan_34 = dCS_34 == 0 ? -999999999 : dBCS_34 / dCS_34;
            if (Math.Abs(dTan_12 - dTan_34) <= dStepTan)
            {
                dLen23 = Math.Abs(Math.Sqrt(Math.Pow(cell3.Point.X - cell2.Point.X, 2) + Math.Pow(cell3.Point.Y - cell2.Point.Y, 2)));
                if (dLen23 <= dStepMaxLen)
                {
                    dLen34 = Math.Sqrt(dCS_34 * dCS_34 + dBCS_34 * dBCS_34);
                    //Console.WriteLine((dTan_12 - dTan_34).ToString() + " - " + dLen23 + " - " + dLen34);
                    if (Math.Abs(dLen23 - dLen34) <= dStepLen)
                    {
                        cellList.Remove(cell3);
                        return true;
                    }
                }
            }
            //
            return false;
        }

        /// <summary>
        /// 去除尖齿
        /// </summary>
        /// <param name="cellList"></param>
        /// <param name="dAngle"></param>
        /// <param name="dAngleLen"></param>
        public void RemoveCanine(IList<Cell> cellList, double dAngle, double dAngleLen)
        {
            if (cellList.Count >= 3)
            {
                double dPI2 = 2 * Math.PI;
                //
                for (int i = 2; i < cellList.Count; )
                {
                    if (this.RemoveCanine_DG(cellList[i - 2], cellList[i - 1], cellList[i], cellList, dAngle, dAngleLen, dPI2)) continue;
                    i++;
                }
                //
                if (cellList.Count >= 3)
                {
                    this.RemoveCanine_DG(cellList[cellList.Count - 2], cellList[cellList.Count - 1], cellList[0], cellList, dAngle, dAngleLen, dPI2);
                    if (cellList.Count >= 3)
                    {
                        this.RemoveCanine_DG(cellList[cellList.Count - 1], cellList[0], cellList[1], cellList, dAngle, dAngleLen, dPI2);
                    }
                }
            }
        }
        private bool RemoveCanine_DG(Cell cell, Cell cell2, Cell cell3, IList<Cell> cellList, double dAngle, double dAngleLen, double dPI2)
        {
            double dH_12;
            double dW_12;
            double dAngle_12;
            double dH_23;
            double dW_23;
            double dAngle_23;
            double dLen12;
            double dLen23;
            //
            dH_12 = cell.Point.Y - cell2.Point.Y;
            dW_12 = cell.Point.X - cell2.Point.X;
            dLen12 = Math.Sqrt(dW_12 * dW_12 + dH_12 * dH_12);
            //
            dH_23 = cell3.Point.Y - cell2.Point.Y;
            dW_23 = cell3.Point.X - cell2.Point.X;
            dLen23 = Math.Sqrt(dW_23 * dW_23 + dH_23 * dH_23);
            //
            if (dLen12 <= dAngleLen || dLen23 <= dAngleLen)
            {
                dAngle_12 = Math.Acos(dW_12 / dLen12);
                if (dH_12 < 0) dAngle_12 = dPI2 - dAngle_12;
                dAngle_23 = Math.Acos(dW_23 / dLen23);
                if (dH_23 < 0) dAngle_23 = dPI2 - dAngle_23;
                //
                double dA = Math.Abs(dAngle_12 - dAngle_23);
                if (dA > Math.PI) dA = dPI2 - dA;
                //Console.WriteLine("Angle : " + dAngle_12 + " - " + dAngle_23 + " - " + dA);
                if (dA <= dAngleLen)
                {
                    //Console.WriteLine("Angle : " + dAngle_12 + " - " + dAngle_23 + " - " + dA);
                    if (dLen12 < dLen23)
                    {
                        cellList.Remove(cell);
                    }
                    else
                    {
                        cellList.Remove(cell3);
                    }
                    return true;
                }
            }
            //
            return false;
        }

        #region IEnumerable
        public IEnumerator GetEnumerator()
        {
            return this.m_CellList.GetEnumerator();
        }
        #endregion
    }
}
