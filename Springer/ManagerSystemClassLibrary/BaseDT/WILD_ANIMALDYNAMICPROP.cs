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
    /// 野生动物动态属性表
    /// </summary>
   public class WILD_ANIMALDYNAMICPROP
    {
        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
       public static Message Save(WILD_ANIMALDYNAMICPROP_Model m)
        {
            List<string> sqllist = new List<string>();
            string[] arrDYNAMICPROPCODE = m.DYNAMICPROPCODE.Split(',');
            string[] arrDYNAMICPROPCONTENT = m.DYNAMICPROPCONTENT.Split(',');
            if (arrDYNAMICPROPCODE.Length - 1 > 0)
            {
                #region 先删除
                StringBuilder sbDelete = new StringBuilder();
                sbDelete.AppendFormat("delete from WILD_ANIMALDYNAMICPROP where 1=1");
                sbDelete.AppendFormat(" and BIOLOGICALTYPECODE='{0}'", m.BIOLOGICALTYPECODE);
                DataBaseClass.ExeSql(sbDelete.ToString());
                #endregion

                #region 再更新
                StringBuilder sbInsert = new StringBuilder();
                sbInsert.AppendFormat("INSERT INTO WILD_ANIMALDYNAMICPROP(BIOLOGICALTYPECODE, DYNAMICPROPCODE, DYNAMICPROPCONTENT)");
                for (int i = 0; i < arrDYNAMICPROPCODE.Length - 1; i++)
                {
                    #region 更新
                    if (isExists(new WILD_ANIMALDYNAMICPROP_SW { BIOLOGICALTYPECODE = m.BIOLOGICALTYPECODE, DYNAMICPROPCODE = arrDYNAMICPROPCODE[i] }))
                    {
                        StringBuilder sbUpdate = new StringBuilder();
                        sbUpdate.AppendFormat("UPDATE WILD_ANIMALDYNAMICPROP SET ");
                        sbUpdate.AppendFormat(" DYNAMICPROPCONTENT={0},", ClsSql.saveNullField(arrDYNAMICPROPCONTENT[i]));
                        sbUpdate.AppendFormat(" where BIOLOGICALTYPECODE= '{0}'", ClsSql.EncodeSql(m.BIOLOGICALTYPECODE));
                        sbUpdate.AppendFormat(" and DYNAMICPROPCODE= '{0}'", ClsSql.EncodeSql(arrDYNAMICPROPCODE[i]));
                        sqllist.Add(sbUpdate.ToString());
                    }
                    #endregion

                    #region 添加
                    else
                    {
                        sbInsert.AppendFormat(" select '{0}'", ClsSql.EncodeSql(m.BIOLOGICALTYPECODE));
                        sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(arrDYNAMICPROPCODE[i]));
                        sbInsert.AppendFormat(",{0}", ClsSql.saveNullField(arrDYNAMICPROPCONTENT[i]));
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
       public static DataTable getDT(WILD_ANIMALDYNAMICPROP_SW sw)
       {
           StringBuilder sb = new StringBuilder();
           sb.AppendFormat("SELECT * FROM WILD_ANIMALDYNAMICPROP WHERE 1=1");
           if (!string.IsNullOrEmpty(sw.BIOLOGICALTYPECODE))
               sb.AppendFormat(" AND BIOLOGICALTYPECODE = '{0}'", ClsSql.EncodeSql(sw.BIOLOGICALTYPECODE));
           if (!string.IsNullOrEmpty(sw.DYNAMICPROPCODE))
               sb.AppendFormat(" AND DYNAMICPROPCODE = '{0}'", ClsSql.EncodeSql(sw.DYNAMICPROPCODE));
           DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
           return ds.Tables[0];
       }
       #endregion

        #region 判断记录是否存在
        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>true存在 false不存在 </returns>
       public static bool isExists(WILD_ANIMALDYNAMICPROP_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from WILD_ANIMALDYNAMICPROP where 1=1");
            if (string.IsNullOrEmpty(sw.BIOLOGICALTYPECODE) == false)
                sb.AppendFormat(" and BIOLOGICALTYPECODE='{0}'", ClsSql.EncodeSql(sw.BIOLOGICALTYPECODE));
            if (string.IsNullOrEmpty(sw.DYNAMICPROPCODE) == false)
                sb.AppendFormat(" and DYNAMICPROPCODE='{0}'", ClsSql.EncodeSql(sw.DYNAMICPROPCODE));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }
        #endregion
    }
}
