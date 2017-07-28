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

namespace ManagerSystemClassLibrary.BaseDT
{
    /// <summary>
    /// 附件
    /// </summary>
    public class E_FILE
    {
        //#region 附件保存 多个附件 草稿/发送时需要保存
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="BYEMAILID"></param>
        ///// <param name="FileList"></param>
        ///// <returns></returns>
        //public static Message Save(string BYEMAILID, string FileList)
        //{
        //    Del(new E_FILE_Model { BYEMAILID = BYEMAILID });//多附件时，先删除，后加

        //    string[] arrFile = FileList.Split('|');//附件之间用|
        //    for (int i = 0; i < arrFile.Length; i++)
        //    {
        //        if (string.IsNullOrEmpty(arrFile[i]) == false)
        //        {
        //            string[] arr = arrFile[i].Split(',');//附件内容之间用,分隔
        //            for (int k = 0; k < arr.Length; k++)
        //            {
        //                //保存附件
        //                Add(new E_FILE_Model
        //                {
        //                    BYEMAILID = BYEMAILID,
        //                    EMAILFILETITLE = arr[0],
        //                    EMAILFILESIZE = arr[1],
        //                    EMAILFILENAME = arr[2]
        //                });

        //            }
        //        }
        //    }

        //    return new Message(true, "保存成功！", "");
        //}
        //#endregion
        #region 增加
        /// <summary>
        /// 增加附件-返回主键值
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static string AddReturn(E_FILE_Model m)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  E_FILE(BYEMAILID,EMAILFILETITLE,EMAILFILESIZE, EMAILFILENAME)");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.BYEMAILID));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.EMAILFILETITLE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.EMAILFILESIZE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.EMAILFILENAME));
            sb.AppendFormat(") select @@identity");
            return DataBaseClass.ReturnSqlField(sb.ToString());
        }

        #endregion
        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(E_FILE_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete E_FILE");
            sb.AppendFormat(" where 1=1");
            if (string.IsNullOrEmpty(m.EFID) == false)
                sb.AppendFormat(" and EFID= '{0}'", ClsSql.EncodeSql(m.EFID));
            if (string.IsNullOrEmpty(m.BYEMAILID) == false)
                sb.AppendFormat(" and BYEMAILID= '{0}'", ClsSql.EncodeSql(m.BYEMAILID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "", "");
            else
                return new Message(false, "", "");
        }
        #endregion


        #region 获取数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(E_File_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM      E_File a");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.EFID) == false)
                sb.AppendFormat(" AND EFID = '{0}'", ClsSql.EncodeSql(sw.EFID));
            if (string.IsNullOrEmpty(sw.BYEMAILID) == false)
                sb.AppendFormat(" AND BYEMAILID = '{0}'", ClsSql.EncodeSql(sw.BYEMAILID));
            string sql = "SELECT     EFID, BYEMAILID, EMAILFILETITLE, EMAILFILESIZE, EMAILFILENAME"
                + sb.ToString()
                + " order by EFID";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }
        #endregion

    }
}
