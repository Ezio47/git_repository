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
    /// 有害生物_附件表
    /// </summary>
    public class PEST_PESTINFOFILE
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(PEST_PESTINFOFILE_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  PEST_PESTINFOFILE(BIOLOGICALTYPECODE, PESTFILETITLE, PESTFILETYPE, PESTFILENAME, UPLOADTIME, UID) VALUES( ");
            sb.AppendFormat(" '{0}'", ClsSql.EncodeSql(m.BIOLOGICALTYPECODE));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.PESTFILETITLE));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.PESTFILETYPE));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.PESTFILENAME));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.UPLOADTIME));
            sb.AppendFormat(",{0})", ClsSql.saveNullField(m.UID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "上传成功!", "");
            else
                return new Message(false, "上传失败!", "");
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Mdy(PEST_PESTINFOFILE_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE PEST_PESTINFOFILE SET ");
            sb.AppendFormat(" PESTFILETITLE={0}", ClsSql.saveNullField(m.PESTFILETITLE));
            sb.AppendFormat(",PESTFILETYPE={0}", ClsSql.saveNullField(m.PESTFILETYPE));
            sb.AppendFormat(",PESTFILENAME={0}", ClsSql.saveNullField(m.PESTFILENAME));
            sb.AppendFormat(",UPLOADTIME={0}", ClsSql.saveNullField(m.UPLOADTIME));
            sb.AppendFormat(",UID={0}", ClsSql.saveNullField(m.UID));
            sb.AppendFormat(" WHERE PESTFILEID= '{0}'", ClsSql.EncodeSql(m.PESTFILEID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功!", "");
            else
                return new Message(false, "修改失败!", "");
        }
        #endregion

        #region 修改但不涉及到图片上传
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message MdyTP(PEST_PESTINFOFILE_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE PEST_PESTINFOFILE SET ");
            sb.AppendFormat(" PESTFILETITLE={0}", ClsSql.saveNullField(m.PESTFILETITLE));
            sb.AppendFormat(",PESTFILETYPE={0}", ClsSql.saveNullField(m.PESTFILETYPE));
            sb.AppendFormat(",UID={0}", ClsSql.saveNullField(m.UID));
            sb.AppendFormat(" WHERE PESTFILEID= '{0}'", ClsSql.EncodeSql(m.PESTFILEID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功!", "");
            else
                return new Message(false, "修改失败!", "");
        }

        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(PEST_PESTINFOFILE_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" DELETE FROM PEST_PESTINFOFILE");
            sb.AppendFormat(" WHERE PESTFILEID= '{0}'", ClsSql.EncodeSql(m.PESTFILEID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功!", "");
            else
                return new Message(false, "删除失败!", "");
        }
        #endregion

        #region 获取数据
        /// <summary>
        ///  获取数据
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(PEST_PESTINFOFILE_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM PEST_PESTINFOFILE  WHERE 1=1");

            #region 查询条件
            //生物物种编码
            if (string.IsNullOrEmpty(sw.BIOLOGICALTYPECODE) == false)
                sb.AppendFormat(" AND BIOLOGICALTYPECODE = '{0}'", ClsSql.EncodeSql(sw.BIOLOGICALTYPECODE));
            //附近名称
            if (string.IsNullOrEmpty(sw.PESTFILETITLE) == false)
                sb.AppendFormat(" AND PESTFILETITLE = '{0}'", ClsSql.EncodeSql(sw.PESTFILETITLE));
            //附件类别
            if (string.IsNullOrEmpty(sw.PESTFILETYPE) == false)
                sb.AppendFormat(" AND PESTFILETYPE = '{0}'", ClsSql.EncodeSql(sw.PESTFILETYPE));
            #endregion

            string sql = "SELECT  PESTFILEID, BIOLOGICALTYPECODE, PESTFILETITLE, PESTFILETYPE, PESTFILENAME, UPLOADTIME, UID " + sb.ToString() + " ORDER BY UPLOADTIME ";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }
        #endregion
    }
}
