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
    /// 有害生物照片表
    /// </summary>
    public class PEST_PHOTO
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(PEST_PHOTO_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  PEST_PHOTO(PHOTOTITLE, PHOTOFILENAME, PHOTOEXPLAIN, PHOTOTIME, PHOTOTYPE, PRID) VALUES( ");
            sb.AppendFormat(" {0}", ClsSql.saveNullField(m.PHOTOTITLE));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.PHOTOFILENAME));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.PHOTOEXPLAIN));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.PHOTOTIME));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.PHOTOTYPE));
            sb.AppendFormat(",{0})", ClsSql.saveNullField(m.PRID));
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
        public static Message Mdy(PEST_PHOTO_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE PEST_PHOTO SET ");
            sb.AppendFormat(" PHOTOTITLE={0}", ClsSql.saveNullField(m.PHOTOTITLE));
            sb.AppendFormat(",PHOTOFILENAME={0}", ClsSql.saveNullField(m.PHOTOFILENAME));
            sb.AppendFormat(",PHOTOEXPLAIN={0}", ClsSql.saveNullField(m.PHOTOEXPLAIN));
            sb.AppendFormat(",PHOTOTIME={0}", ClsSql.saveNullField(m.PHOTOTIME));
            sb.AppendFormat(",PHOTOTYPE={0}", ClsSql.saveNullField(m.PHOTOTYPE));
            sb.AppendFormat(",PRID={0}", ClsSql.saveNullField(m.PRID));
            sb.AppendFormat(" WHERE PEST_PHOTOID= '{0}'", ClsSql.EncodeSql(m.PEST_PHOTOID));
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
        public static Message MdyTP(PEST_PHOTO_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE PEST_PHOTO SET");
            sb.AppendFormat(" PHOTOTITLE={0}", ClsSql.saveNullField(m.PHOTOTITLE));
            sb.AppendFormat(",PHOTOEXPLAIN={0}", ClsSql.saveNullField(m.PHOTOEXPLAIN));
            sb.AppendFormat(",PHOTOTIME={0}", ClsSql.saveNullField(m.PHOTOTIME));
            sb.AppendFormat(",PHOTOTYPE={0}", ClsSql.saveNullField(m.PHOTOTYPE));
            sb.AppendFormat(",PRID={0}", ClsSql.saveNullField(m.PRID));
            sb.AppendFormat(" WHERE PEST_PHOTOID= '{0}'", ClsSql.EncodeSql(m.PEST_PHOTOID));
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
        public static Message Del(PEST_PHOTO_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" DELETE PEST_PHOTO");
            sb.AppendFormat(" WHERE PEST_PHOTOID= '{0}'", ClsSql.EncodeSql(m.PEST_PHOTOID));
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
        public static DataTable getDT(PEST_PHOTO_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" FROM PEST_PHOTO  WHERE 1=1");

            #region 查询条件
            //根据表名查询
            if (string.IsNullOrEmpty(sw.PHOTOTYPE) == false)
                sb.AppendFormat(" AND PHOTOTYPE = '{0}'", ClsSql.EncodeSql(sw.PHOTOTYPE));
            //根据所属序号查询
            if (string.IsNullOrEmpty(sw.PRID) == false)
                sb.AppendFormat(" AND PRID = '{0}'", ClsSql.EncodeSql(sw.PRID));
            //根据照片标题查询
            if (string.IsNullOrEmpty(sw.PHOTOTITLE) == false)
                sb.AppendFormat(" AND PHOTOTITLE = '{0}'", ClsSql.EncodeSql(sw.PHOTOTITLE));
            //根据照片描述查询
            if (string.IsNullOrEmpty(sw.PHOTOEXPLAIN) == false)
                sb.AppendFormat(" AND PHOTOEXPLAIN like  '%{0}%'", ClsSql.EncodeSql(sw.PHOTOEXPLAIN));
            #endregion

            string sql = "SELECT PEST_PHOTOID, PHOTOTITLE, PHOTOFILENAME, PHOTOEXPLAIN, PHOTOTIME, PHOTOTYPE, PRID " + sb.ToString() + " ORDER BY PEST_PHOTOID, PHOTOTIME ";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }
        #endregion
    }
}
