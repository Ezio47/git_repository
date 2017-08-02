using DataBaseClassLibrary;
using ManagerSystemModel;
using PublicClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary.BaseDT
{
    /// <summary>
    ///  防火系统部门与OA系统部门关联表
    /// </summary>
    public class T_SysDept_OADept
    {
        #region 防火系统部门与OA系统部门关联
        /// <summary>
        /// 防火系统部门与OA系统部门关联
        /// </summary>
        /// <param name="m">m</param>
        /// <returns></returns>
        public static Message DeptMap(T_SysDept_OADept_Model m)
        {
            string[] SysDeptArray = m.SysDeptID.Split(',');
            string[] OADeptArray = m.OADeptID.Split(',');
            List<string> sqllist = new List<string>();
            StringBuilder sbInsert = new StringBuilder();
            sbInsert.AppendFormat("Insert into T_SysDept_OADept(SysORGNO,SysDeptID,OADeptID)");
            for (int i = 0; i < OADeptArray.Length; i++)
            {
                #region 更新
                if (DataBaseClass.JudgeRecordExists("select 1 from T_SysDept_OADept where SysORGNO='" + m.SysORGNO + "' and SysDeptID='" + SysDeptArray[i] + "'"))
                {
                    StringBuilder sbUpdate = new StringBuilder();
                    if (OADeptArray[i] != "")
                    {
                        sbUpdate.AppendFormat("Update T_SysDept_OADept SET ");
                        sbUpdate.AppendFormat(" SysORGNO= '{0}',", ClsSql.EncodeSql(m.SysORGNO));
                        sbUpdate.AppendFormat(" SysDeptID='{0}',", ClsSql.EncodeSql(SysDeptArray[i]));
                        sbUpdate.AppendFormat(" OADeptID= '{0}'", ClsSql.EncodeSql(OADeptArray[i]));
                        sbUpdate.AppendFormat(" where SysORGNO= '{0}'", ClsSql.EncodeSql(m.SysORGNO));
                        sbUpdate.AppendFormat(" and SysDeptID= '{0}'", ClsSql.EncodeSql(SysDeptArray[i]));
                        sqllist.Add(sbUpdate.ToString());
                    }
                    else
                    {
                        sbUpdate.AppendFormat("Delete  from  T_SysDept_OADept  where  SysORGNO='" + m.SysORGNO + "'  and SysDeptID='" + SysDeptArray[i] + "'");
                        sqllist.Add(sbUpdate.ToString());
                    }
                }
                #endregion

                #region 添加
                else
                {
                    sbInsert.AppendFormat(" select '{0}'", ClsSql.EncodeSql(m.SysORGNO));
                    sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(SysDeptArray[i]));
                    sbInsert.AppendFormat(",'{0}'", ClsSql.EncodeSql(OADeptArray[i]));
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

            var y = DataBaseClass.ExecuteSqlTran(sqllist);
            if (y > 0)
            {
                return new Message(true, "保存成功!", "");
            }
            else
            {
                return new Message(false, "保存失败!", "");
            }
        }
        #endregion

        #region  获取OA部门编号集合
        /// <summary>
        /// 获取OA部门编号集合
        /// </summary>
        /// <param name="sysORGNO">防火系统组织机构ID</param>
        /// <param name="sysDeptIdList">防火系统部门ID集合,以英文逗号分隔</param>
        /// <returns>以英文逗号分隔的OA部门编码,若没有则用'无'代替</returns>
        public static string FindOADeptBySysDept(string sysORGNO, string sysDeptIdList)
        {
            string[] sysDeptId = sysDeptIdList.Split(',');
            string OADeptId = "";
            for (int i = 0; i < sysDeptId.Length; i++)
            {
                string sql = "select OADEPTID from T_SysDept_OADept where SysORGNO='" + sysORGNO + "' and SysDeptID='" + sysDeptId[i] + "'";
                string ss = DataBaseClass.ReturnSqlField(sql);
                OADeptId = OADeptId + ss + ",";
            }
            if (OADeptId.Length > 1)
                OADeptId = OADeptId.Substring(0, OADeptId.Length - 1);
            return OADeptId;
        }
        #endregion

        #region 获取OA部门编号
        /// <summary>
        /// 获取OA部门编号
        /// </summary>
        /// <param name="sysORGNO">防火系统组织机构ID</param>
        /// <param name="sysDeptId">防火系统部门ID</param>
        /// <returns></returns>
        public static string GetDeptID(string sysORGNO, string sysDeptId)
        {
            string sql = "select OADEPTID from T_SysDept_OADept where SysORGNO='" + sysORGNO + "' and SysDeptID='" + sysDeptId + "'";
            return DataBaseClass.ReturnSqlField(sql);
        }
        #endregion
    }
}
