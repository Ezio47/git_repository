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
    /// 有害生物_采集数据上报上传表
    /// </summary>
    public class PEST_COLLECT_DATAUPLOAD
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(PEST_COLLECT_DATAUPLOAD_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  PEST_COLLECT_DATAUPLOAD(PESTCOLLDATAID,UPLOADNAME, UPLOADDESCRIBE, UPLOADURL,UPLOADTYPE)");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.PESTCOLLDATAID));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.UPLOADNAME));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.UPLOADDESCRIBE));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.UPLOADURL));
            sb.AppendFormat(",{0})", ClsSql.saveNullField(m.UPLOADTYPE));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "添加成功!", "");
            else
                return new Message(false, "添加失败!", "");
        }

        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Mdy(PEST_COLLECT_DATAUPLOAD_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE PEST_COLLECT_DATAUPLOAD SET ");
            sb.AppendFormat(" UPLOADNAME='{0}'", ClsSql.EncodeSql(m.UPLOADNAME));
            sb.AppendFormat(",UPLOADDESCRIBE={0}", ClsSql.saveNullField(m.UPLOADDESCRIBE));
            sb.AppendFormat(",UPLOADURL={0}", ClsSql.saveNullField(m.UPLOADURL));
            sb.AppendFormat(",UPLOADTYPE={0}", ClsSql.saveNullField(m.UPLOADTYPE));
            sb.AppendFormat(" where PESTCOLLDATAUPLOADID= '{0}'", ClsSql.EncodeSql(m.PESTCOLLDATAUPLOADID));
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
        public static Message MdyTP(PEST_COLLECT_DATAUPLOAD_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE PEST_COLLECT_DATAUPLOAD SET ");
            sb.AppendFormat(" UPLOADNAME='{0}'", ClsSql.EncodeSql(m.UPLOADNAME));
            sb.AppendFormat(",UPLOADDESCRIBE={0}", ClsSql.saveNullField(m.UPLOADDESCRIBE));
            sb.AppendFormat(" where PESTCOLLDATAUPLOADID= '{0}'", ClsSql.EncodeSql(m.PESTCOLLDATAUPLOADID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功!", "");
            else
                return new Message(false, "修改失败，请检查各输入框是否正确!", "");
        }

        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(PEST_COLLECT_DATAUPLOAD_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete PEST_COLLECT_DATAUPLOAD");
            sb.AppendFormat(" where PESTCOLLDATAUPLOADID= '{0}'", ClsSql.EncodeSql(m.PESTCOLLDATAUPLOADID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功!", "");
            else
                return new Message(false, "删除失败，请检查各输入框是否正确!", "");
        }

        #endregion

        #region 获取数据
        /// <summary>
        ///  获取数据
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getDT(PEST_COLLECT_DATAUPLOAD_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM PEST_COLLECT_DATAUPLOAD  WHERE 1=1");

            #region 查询条件
            //根据文件名查询
            if (string.IsNullOrEmpty(sw.PESTCOLLDATAID) == false)
                sb.AppendFormat(" AND PESTCOLLDATAID = '{0}'", ClsSql.EncodeSql(sw.PESTCOLLDATAID));
            //根据文件名查询
            if (string.IsNullOrEmpty(sw.UPLOADNAME) == false)
                sb.AppendFormat(" AND UPLOADNAME like  '%{0}%'", ClsSql.EncodeSql(sw.UPLOADNAME));
            //根据文件描述查询
            if (string.IsNullOrEmpty(sw.UPLOADDESCRIBE) == false)
                sb.AppendFormat(" AND UPLOADDESCRIBE = '{0}'", ClsSql.EncodeSql(sw.UPLOADDESCRIBE));
            #endregion

            string sql = "SELECT * " + sb.ToString() + " order by PESTCOLLDATAUPLOADID,UPLOADTYPE ";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }
        #endregion
    }
}
