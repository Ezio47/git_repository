using DataBaseClassLibrary;
using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using PublicClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary.BaseDT
{
    /// <summary>
    /// 有害生物_本地化树种信息表
    /// </summary>
    public class PEST_LOCALTREESPECIES
    {
        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Save(PEST_LOCALTREESPECIES_Model m)
        {
            List<string> sqllist = new List<string>();
            string[] arrTSPCODE = m.TSPCODE.Split(',');
            string[] arrTSPAREA = m.TSPAREA.Split(',');
            if (arrTSPCODE.Length > 0)
            {
                #region 先删除
                string sql = "delete from PEST_LOCALTREESPECIES where BYORGNO='" + m.BYORGNO + "'";
                DataBaseClass.ExeSql(sql);
                #endregion

                #region 再更新
                StringBuilder sbInsert = new StringBuilder();
                sbInsert.AppendFormat("INSERT INTO PEST_LOCALTREESPECIES(BYORGNO,TSPCODE,TSPAREA)");
                for (int i = 0; i < arrTSPCODE.Length; i++)
                {
                    sbInsert.AppendFormat(" select '{0}'", ClsSql.EncodeSql(m.BYORGNO));
                    sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(arrTSPCODE[i]));
                    sbInsert.AppendFormat(",{0}", ClsSql.saveNullField(arrTSPAREA[i]));
                    if (i != arrTSPCODE.Length - 1)
                        sbInsert.AppendFormat(" UNION ALL ");
                }
                sqllist.Add(sbInsert.ToString());
                #endregion
            }
            var y = DataBaseClass.ExecuteSqlTran(sqllist);
            if (y >= 0)
                return new Message(true, "保存成功!", "");
            else
                return new Message(false, "保存失败，事物回滚机制!", "");
        }
        #endregion

        #region 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static DataTable getDT(PEST_LOCALTREESPECIES_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT * FROM PEST_LOCALTREESPECIES WHERE 1=1");
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
            {
                if (sw.BYORGNO.Substring(4, 11) == "00000000000")  //获取所有市的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,4) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 4)));
                else if (sw.BYORGNO.Substring(6, 9) == "000000000" && sw.BYORGNO.Substring(4, 11) != "00000000000") //获取所有县的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,6) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 6)));
                else if (sw.BYORGNO.Substring(9, 6) == "000000" && sw.BYORGNO.Substring(6, 9) != "000000000")   //获取所有乡镇的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,9) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 9)));
                else if (sw.BYORGNO.Substring(9, 6) != "000000")   //获取所有村的
                    sb.AppendFormat(" AND (SUBSTRING(BYORGNO,1,12) = '{0}')", ClsSql.EncodeSql(sw.BYORGNO.Substring(0, 12)));
                else
                    sb.AppendFormat(" AND BYORGNO = '{0}'", ClsSql.EncodeSql(sw.BYORGNO));
            }
            if (!string.IsNullOrEmpty(sw.TSPCODE))
                sb.AppendFormat(" AND TSPCODE = '{0}'", ClsSql.EncodeSql(sw.TSPCODE));
            if (!string.IsNullOrEmpty(sw.TSPAREA))
                sb.AppendFormat(" AND TSPAREA = '{0}'", ClsSql.EncodeSql(sw.TSPAREA));
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        #endregion

        #region 获取有害生物面积列表
        /// <summary>
        /// 获取面积列表
        /// </summary>
        /// <param name="PESTCODE">有害生物编码</param>
        /// <returns></returns>
        public static DataTable getAREADT(string PESTCODE)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT BYORGNO,TSPCODE,TSPAREA FROM PEST_LOCALTREESPECIES WHERE 1=1");
            if (!string.IsNullOrEmpty(PESTCODE))
                sb.AppendFormat(" AND TSPCODE in (SELECT TSPCODE  from T_SYS_TREESPECIES_PEST where  PESTCODE='{0}')", ClsSql.EncodeSql(PESTCODE));
            sb.AppendFormat(" ORDER BY BYORGNO");
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        #endregion
    }
}
