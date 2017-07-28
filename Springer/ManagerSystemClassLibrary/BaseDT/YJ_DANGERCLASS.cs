using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel;
using DataBaseClassLibrary;
using PublicClassLibrary;
using NPOI.HSSF.UserModel;
using System.IO;
using NPOI.SS.UserModel;

namespace ManagerSystemClassLibrary.BaseDT
{

    /// <summary>
    /// 预警_火险等级表
    /// </summary>
    public class YJ_DANGERCLASS
    {
        #region 增加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(YJ_DANGERCLASS_Model m)
        {
            StringBuilder sb = new StringBuilder();
            if (DataBaseClass.JudgeRecordExists("select 1 from YJ_DANGERCLASS where DCDATE='" + m.DCDATE + "' and TOWNNAME='" + m.TOWNNAME + "'"))
            {
                //return new Message(false, "添加失败，该日期已经添加！", "");
                sb.AppendFormat("UPDATE YJ_DANGERCLASS SET ");
                sb.AppendFormat(" WEATHER= '{0}',", ClsSql.EncodeSql(m.WEATHER));
                sb.AppendFormat(" TEMPREATURE='{0}',", ClsSql.EncodeSql(m.TEMPREATURE));
                sb.AppendFormat(" WINDYSPEED= '{0}',", ClsSql.EncodeSql(m.WINDYSPEED));
                sb.AppendFormat(" DANGERCLASS= '{0}'", ClsSql.EncodeSql(m.DANGERCLASS));
                sb.AppendFormat(" where DCDATE= '{0}'", ClsSql.EncodeSql(m.DCDATE));
                sb.AppendFormat(" and TOWNNAME= '{0}'", ClsSql.EncodeSql(m.TOWNNAME));
            }
            else
            {
                sb.AppendFormat("INSERT INTO  YJ_DANGERCLASS(DCDATE, BYORGNO, TOWNNAME, JD, WD, DANGERCLASS, TOPTOWNNAME, WEATHER, TEMPREATURE, WINDYSPEED)");
                sb.AppendFormat("VALUES(");
                sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.DCDATE));
                sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.BYORGNO));
                sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.TOWNNAME));
                sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.JD));
                sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.WD));
                sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.DANGERCLASS));
                sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.TOPTOWNNAME));
                sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.WEATHER));
                sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.TEMPREATURE));
                sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.WINDYSPEED));
                sb.AppendFormat(")");
            }
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "保存成功!", "");
            else
                return new Message(false, "保存失败，请检查各输入框是否正确!", "");
        }

        /// <summary>
        /// 批量添加更新
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message PLAdd(YJ_DANGERCLASS_Model m)
        {
            List<string> sqllist = new List<string>();
            StringBuilder sb = new StringBuilder();
            string insertstr = "";

            var arrBYORGNAME = m.TOWNNAME.Split(',');
            var arrBYORGNO = m.BYORGNO.Split(',');
            var arrWEATHER = m.WEATHER.Split(',');
            var arrTEMPREATURE = m.TEMPREATURE.Split(',');
            var arrWINDYSPEED = m.WINDYSPEED.Split(',');
            var arrDANGERCLASS = m.DANGERCLASS.Split(',');
            var arrDCDATE = m.DCDATE.Split(',');

            for (int i = 0; i < arrBYORGNAME.Length - 1; i++)
            {
                if (DataBaseClass.JudgeRecordExists("select 1 from YJ_DANGERCLASS where DCDATE='" + arrDCDATE[i] + "' and BYORGNO='" + arrBYORGNO[i] + "' and TOWNNAME='" + arrBYORGNAME[i] + "'"))
                {
                    string orgno = arrBYORGNO[i].ToString().Substring(0, 6) + "%";
                    sb.AppendFormat("UPDATE YJ_DANGERCLASS SET ");
                    sb.AppendFormat(" WEATHER= '{0}',", ClsSql.EncodeSql(arrWEATHER[i]));
                    sb.AppendFormat(" TEMPREATURE='{0}',", ClsSql.EncodeSql(arrTEMPREATURE[i]));
                    sb.AppendFormat(" WINDYSPEED= '{0}',", ClsSql.EncodeSql(arrWINDYSPEED[i]));
                    sb.AppendFormat(" DANGERCLASS= '{0}'", ClsSql.EncodeSql(arrDANGERCLASS[i]));
                    sb.AppendFormat(" where DCDATE= '{0}'", ClsSql.EncodeSql(arrDCDATE[i]));
                    sb.AppendFormat(" and BYORGNO like '{0}'", ClsSql.EncodeSql(orgno));
                    sqllist.Add(sb.ToString());
                    sb.Remove(0, sb.Length);
                }
                else
                {
                    sb.AppendFormat("INSERT INTO  YJ_DANGERCLASS(DCDATE, BYORGNO, TOWNNAME, JD, WD, DANGERCLASS, TOPTOWNNAME, WEATHER, TEMPREATURE, WINDYSPEED)");
                    var list = T_ALL_AREACls.getListModel(new T_ALL_AREA_SW { SubAREACODE = arrBYORGNO[i] }).ToList();//乡镇
                    var a = T_ALL_AREACls.getModel(new T_ALL_AREA_SW { AREACODE = arrBYORGNO[i] });//市县
                    list.Add(a);
                    foreach (var item in list)
                    {
                        sb.AppendFormat(" select '{0}'", ClsSql.EncodeSql(arrDCDATE[i]));
                        sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(item.AREACODE));
                        sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(item.AREAJC));
                        sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.JD));
                        sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.WD));
                        sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(arrDANGERCLASS[i]));
                        sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(arrBYORGNAME[i]));
                        sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(arrWEATHER[i]));
                        sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(arrTEMPREATURE[i]));
                        sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(arrWINDYSPEED[i]));
                        sb.AppendFormat(" UNION ALL ");
                    }
                    insertstr += sb.ToString();
                    string str = insertstr.Substring(0, insertstr.Length - 10);
                    insertstr = "";
                    sb.Remove(0, sb.Length);
                    sqllist.Add(str);
                }
            }
            var j = DataBaseClass.ExecuteSqlTran(sqllist);
            if (j > 0)
                return new Message(true, "保存成功!", "");
            else
                return new Message(false, "保存失败，事物回滚机制!", "");
        }

        /// <summary>
        /// 批量添加更新
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message PLAdd2(YJ_DANGERCLASS_Model m)
        {
            List<string> sqllist = new List<string>();
            
            var arrTOWNNAME = m.TOWNNAME.Split(',');
            var arrBYORGNO = m.BYORGNO.Split(',');
            var arrWEATHER = m.WEATHER.Split(',');
            var arrTEMPREATURE = m.TEMPREATURE.Split(',');
            var arrWINDYSPEED = m.WINDYSPEED.Split(',');
            var arrDANGERCLASS = m.DANGERCLASS.Split(',');
            var arrDCDATE = m.DCDATE.Split(',');

            if (arrBYORGNO.Length - 1 > 0)
            {
                StringBuilder sbInsert = new StringBuilder();
                sbInsert.AppendFormat("INSERT INTO  YJ_DANGERCLASS(DCDATE, BYORGNO, TOWNNAME, JD, WD, DANGERCLASS, TOPTOWNNAME, WEATHER, TEMPREATURE, WINDYSPEED)");
                for (int i = 0; i < arrBYORGNO.Length - 1; i++)
                {
                    #region 更新
                    if (DataBaseClass.JudgeRecordExists("select 1 from YJ_DANGERCLASS where DCDATE='" + arrDCDATE[i] + "' and BYORGNO='" + arrBYORGNO[i] + "' and TOWNNAME='" + arrTOWNNAME[i] + "'"))
                    {
                        StringBuilder sbUpdate = new StringBuilder();
                        sbUpdate.AppendFormat("UPDATE YJ_DANGERCLASS SET ");
                        sbUpdate.AppendFormat(" WEATHER= {0},", ClsSql.saveNullField(arrWEATHER[i]));
                        sbUpdate.AppendFormat(" TEMPREATURE={0},", ClsSql.saveNullField(arrTEMPREATURE[i]));
                        sbUpdate.AppendFormat(" WINDYSPEED= {0},", ClsSql.saveNullField(arrWINDYSPEED[i]));
                        sbUpdate.AppendFormat(" DANGERCLASS= '{0}'", ClsSql.EncodeSql(arrDANGERCLASS[i]));
                        sbUpdate.AppendFormat(" where DCDATE= '{0}'", ClsSql.EncodeSql(arrDCDATE[i]));
                        sbUpdate.AppendFormat(" and BYORGNO= '{0}'", ClsSql.EncodeSql(arrBYORGNO[i]));
                        sqllist.Add(sbUpdate.ToString());
                    }
                    #endregion

                    #region 添加
                    else
                    {
                        sbInsert.AppendFormat(" select '{0}'", ClsSql.EncodeSql(arrDCDATE[i]));
                        sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(arrBYORGNO[i]));
                        sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(arrTOWNNAME[i]));
                        sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.JD));
                        sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.WD));
                        sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(arrDANGERCLASS[i]));
                        sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.TOPTOWNNAME));
                        sbInsert.AppendFormat(",{0}", ClsSql.saveNullField(arrWEATHER[i]));
                        sbInsert.AppendFormat(",{0}", ClsSql.saveNullField(arrTEMPREATURE[i]));
                        sbInsert.AppendFormat(",{0}", ClsSql.saveNullField(arrWINDYSPEED[i]));
                        sbInsert.AppendFormat(" UNION ALL ");
                    }
                    #endregion
                }
                string insertStr = sbInsert.ToString();
                if (insertStr.Contains(" UNION ALL "))
                {
                    insertStr = insertStr.Substring(0, insertStr.Length - 10);
                    sqllist.Add(insertStr);
                }
            }

            var y = DataBaseClass.ExecuteSqlTran(sqllist);
            if (y >= 0)
                return new Message(true, "保存成功!", "");
            else
                return new Message(false, "保存失败，事物回滚机制!", "");
        }

        /// <summary>
        /// 根据等级时间删除
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message DeleteByDCDATE(YJ_DANGERCLASS_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" DELETE   FROM  YJ_DANGERCLASS  where dcdate='" + m.DCDATE + "'");
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功！", "");
            else
                return new Message(false, "删除失败，请检查各输入框是否正确！", "");
        }

        /// <summary>
        /// 获取火险等级记录
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(YJ_DANGERCLASS_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" SELECT DANGERID, DCDATE, BYORGNO, TOWNNAME, JD, WD, DANGERCLASS, TOPTOWNNAME, WEATHER, TEMPREATURE, WINDYSPEED");
            sb.AppendFormat(" FROM YJ_DANGERCLASS Where 1=1 ");
            if (!string.IsNullOrEmpty(sw.BYORGNO))
            {
                sb.AppendFormat(" And BYORGNO='{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            }
            if (!string.IsNullOrEmpty(sw.DANGERCLASS) && sw.DANGERCLASS != "0")
            {
                sb.AppendFormat(" And DANGERCLASS='{0}'", ClsSql.EncodeSql(sw.DANGERCLASS));
            }
            if (!string.IsNullOrEmpty(sw.DCDATE))
            {
                sb.AppendFormat(" And convert(char(10), DCDATE,120)='{0}'", ClsSwitch.SwitDate(sw.DCDATE));
            }
            string sql = sb.ToString()
                + " order by BYORGNO DESC";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取数据库中最新的一条火险等级记录
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getTopDT(YJ_DANGERCLASS_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" SELECT top 1 DANGERID, DCDATE, BYORGNO, TOWNNAME, JD, WD, DANGERCLASS, TOPTOWNNAME, WEATHER, TEMPREATURE, WINDYSPEED");
            sb.AppendFormat(" FROM YJ_DANGERCLASS Where 1=1 ");
            if (!string.IsNullOrEmpty(sw.BYORGNO))
            {
                sb.AppendFormat(" And BYORGNO='{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            }
            if (!string.IsNullOrEmpty(sw.DANGERCLASS) && sw.DANGERCLASS != "0")
            {
                sb.AppendFormat(" And DANGERCLASS='{0}'", ClsSql.EncodeSql(sw.DANGERCLASS));
            }
            if (!string.IsNullOrEmpty(sw.DCDATE))
            {
                sb.AppendFormat(" And convert(char(10), DCDATE,120)='{0}'", ClsSwitch.SwitDate(sw.DCDATE));
            }
            string sql = sb.ToString()
                + " order by DCDATE DESC";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }

        /// <summary>
        /// 空间火险等级更新
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message UpdateAceHuoXianDengJi(YJ_DANGERCLASS_Model m)
        {
            Message ms = null;
            StringBuilder sb1 = new StringBuilder();
            sb1.AppendFormat(" Update HUOXIANDENGJI set DValue={0}", ClsSql.EncodeSql(m.DValue));
            sb1.AppendFormat(" Where NAME='{0}'", ClsSql.EncodeSql(m.Name));
            List<string> sqllist = new List<string>();
            sqllist.Add(sb1.ToString());
            var i = SDEDataBaseClass.ExecuteSqlTran(sqllist);
            if (i > 0)
            {
                ms = new Message(true, "处理成功！", "");
            }
            else
            {
                ms = new Message(false, "处理失败，事物回滚机制！", "");
            }
            return ms;
        }

        /// <summary>
        /// 空间库火险等级更新 根据县代码更新
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message UpdateAceHuoXianDengJiByXian(YJ_DANGERCLASS_Model m)
        {
            List<string> sqllist = new List<string>();
            var arrBYORGNO = m.BYORGNO.Split(',');
            var arrDANGERCLASS = m.DANGERCLASS.Split(',');
            Message ms = null;
            StringBuilder sb1 = new StringBuilder();
            for (int i = 0; i < arrBYORGNO.Length - 1; i++)
            {
                string xiandaima = arrBYORGNO[i].Substring(0, 6);
                sb1.AppendFormat(" Update HUOXIANDENGJI set DValue={0}", ClsSql.EncodeSql(arrDANGERCLASS[i]));
                sb1.AppendFormat(" Where 县代码='{0}'", ClsSql.EncodeSql(xiandaima));
                sqllist.Add(sb1.ToString());
                sb1.Remove(0, sb1.Length);
            }
            var j = SDEDataBaseClass.ExecuteSqlTran(sqllist);
            if (j > 0)
            {
                ms = new Message(true, "保存成功！", "");
            }
            else
            {
                ms = new Message(false, "保存失败，事物回滚！", "");
            }
            return ms;
        }

        /// <summary>
        /// 空间库火险等级更新 根据QH_CODE更新
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message UpdateAceHuoXianDengJiByQHCODE(YJ_DANGERCLASS_Model m)
        {
            List<string> sqllist = new List<string>();
            var arrBYORGNO = m.BYORGNO.Split(',');
            var arrDANGERCLASS = m.DANGERCLASS.Split(',');
            Message ms = null;
            StringBuilder sb1 = new StringBuilder();
            for (int i = 0; i < arrBYORGNO.Length - 1; i++)
            {
                if (ClsSql.EncodeSql(arrDANGERCLASS[i]) != "")
                {
                    string QHCODE = arrBYORGNO[i] + "000";
                    sb1.AppendFormat(" Update HUOXIANDENGJI set DValue={0}", ClsSql.EncodeSql(arrDANGERCLASS[i]));
                    sb1.AppendFormat(" Where QH_CODE='{0}'", ClsSql.EncodeSql(QHCODE));
                    sqllist.Add(sb1.ToString());
                    sb1.Remove(0, sb1.Length);
                }
            }
            var j = SDEDataBaseClass.ExecuteSqlTran(sqllist);
            if (j >= 0)
            {
                ms = new Message(true, "保存成功！", "");
            }
            else
            {
                ms = new Message(false, "保存失败，事物回滚！", "");
            }
            return ms;
        }

        /// <summary>
        /// 火险等级导入
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message AddExport(YJ_DANGERCLASS_Model m)
        {
            Message ms = null;
            StringBuilder sb1 = new StringBuilder();
            sb1.AppendFormat("INSERT INTO  YJ_DANGERCLASS(DCDATE, BYORGNO, TOWNNAME, JD, WD, DANGERCLASS, TOPTOWNNAME)");
            sb1.AppendFormat("VALUES(");
            sb1.AppendFormat("'{0}'", ClsSql.EncodeSql(m.DCDATE));
            sb1.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.BYORGNO));
            sb1.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.TOWNNAME));
            sb1.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.JD));
            sb1.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.WD));
            sb1.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.DANGERCLASS));
            sb1.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.TOPTOWNNAME));
            sb1.AppendFormat(")");
            List<string> sqllist = new List<string>();
            sqllist.Add(sb1.ToString());
            var i = DataBaseClass.ExecuteSqlTran(sqllist);
            if (i > 0)
            {
                ms = new Message(true, "处理成功！", "");
            }
            else
            {
                ms = new Message(false, "处理失败，事物回滚机制！", "");
            }
            return ms;

        }
        #endregion

        #region 获取最新数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getNewDT(YJ_DANGERCLASS_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" SELECT DANGERID, DCDATE, BYORGNO, TOWNNAME, JD, WD, DANGERCLASS, TOPTOWNNAME, WEATHER, TEMPREATURE, WINDYSPEED");
            sb.AppendFormat(" FROM YJ_DANGERCLASS");
            sb.AppendFormat(" where DCDATE=(select max( DCDATE) from YJ_DANGERCLASS)");
            string sql = sb.ToString()
                + " order by BYORGNO DESC";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }

        #endregion

        #region 获取某一区域数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getNewArea(YJ_DANGERCLASS_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" SELECT top 1 DANGERID, DCDATE, BYORGNO, TOWNNAME, JD, WD, DANGERCLASS, TOPTOWNNAME, WEATHER, TEMPREATURE, WINDYSPEED");
            sb.AppendFormat(" FROM YJ_DANGERCLASS Where 1=1 ");
            if (!string.IsNullOrEmpty(sw.DCDATE) && !string.IsNullOrEmpty(sw.TOWNNAME))
            {
                sb.AppendFormat(" AND DCDATE='" + sw.DCDATE + "' and (TOWNNAME='" + sw.TOWNNAME + "')");
            }
            else
            {
                sb.AppendFormat(" AND TOWNNAME='" + sw.TOWNNAME + "'");
                //sb.AppendFormat(" AND (DCDATE=(select max(DCDATE) from YJ_DANGERCLASS) or DCDATE=(select max(DCDATE) from YJ_DANGERCLASS where DCDATE!=(select max(DCDATE) from YJ_DANGERCLASS) and TOWNNAME!='" + sw.TOWNNAME + "')) and (TOWNNAME='" + sw.TOWNNAME + "')");
                //sb.AppendFormat(" AND DCDATE=(select max( DCDATE) from YJ_DANGERCLASS)  and (TOWNNAME='" + sw.TOWNNAME + "')");
            }
            string sql = sb.ToString()
                + " order by DCDATE DESC";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }

        #endregion

        #region 火险等级上传
        /// <summary>
        /// 火险等级上传
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static void HXDJUpload(string filePath)
        {
            HSSFWorkbook hssfworkbook;
            try
            {
                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    hssfworkbook = new HSSFWorkbook(file);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheetAt(0);

            int rowCount = sheet.LastRowNum;//LastRowNum = PhysicalNumberOfRows - 1

            for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
            {

                IRow row = sheet.GetRow(i);
                string[] arr = new string[3];
                for (int k = 0; k < arr.Length; k++)
                {
                    if (k != 2)
                        arr[k] = row.GetCell(k) == null ? "" : row.GetCell(k).ToString();//循环获取每一单元格内容
                    else
                        arr[k] = row.GetCell(k).DateCellValue.ToString("yyyy-MM-dd");
                }
                YJ_DANGERCLASS_Model m = new YJ_DANGERCLASS_Model();
                //地区  火险等级  日期
                if (string.IsNullOrEmpty(arr[0]) || string.IsNullOrEmpty(arr[1]) || string.IsNullOrEmpty(arr[2]))
                {
                    continue;
                }
                m.TOWNNAME = arr[0];
                m.DANGERCLASS = arr[1];
                m.DCDATE = arr[2];

                m.opMethod = "Add";

                YJ_DANGERCLASSCls.Manager(m);
            }

        }
        #endregion
    }
}
