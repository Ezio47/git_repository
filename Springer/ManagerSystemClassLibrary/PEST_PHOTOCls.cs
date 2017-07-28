using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using PublicClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 有害生物照片表
    /// </summary>
    public class PEST_PHOTOCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型Message</returns>
        public static Message Manager(PEST_PHOTO_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msg = BaseDT.PEST_PHOTO.Add(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Mdy")
            {
                Message msg = BaseDT.PEST_PHOTO.Mdy(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "MdyTP")
            {
                Message msg = BaseDT.PEST_PHOTO.MdyTP(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Del")
            {
                Message msg = BaseDT.PEST_PHOTO.Del(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            return new Message(false, "无效操作", m.returnUrl);
        }
        #endregion

        #region 获取单条记录
        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static PEST_PHOTO_Model getModel(PEST_PHOTO_SW sw)
        {
            PEST_PHOTO_Model m = new PEST_PHOTO_Model();
            DataTable dt = BaseDT.PEST_PHOTO.getDT(sw);//列表
            if (dt.Rows.Count > 0)
            {
                int i = 0;              
                m.PEST_PHOTOID = dt.Rows[i]["PEST_PHOTOID"].ToString();
                m.PHOTOTITLE = dt.Rows[i]["PHOTOTITLE"].ToString();
                m.PHOTOFILENAME = dt.Rows[i]["PHOTOFILENAME"].ToString();
                m.PHOTOEXPLAIN = dt.Rows[i]["PHOTOEXPLAIN"].ToString();
                m.PHOTOTIME = ClsSwitch.SwitTM(dt.Rows[i]["PHOTOTIME"].ToString());
                m.PHOTOTYPE = dt.Rows[i]["PHOTOTYPE"].ToString();
                m.PRID = dt.Rows[i]["PRID"].ToString();
            }
            dt.Clear();
            dt.Dispose();
            return m;
        }
        #endregion

        #region 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<PEST_PHOTO_Model> getModelList(PEST_PHOTO_SW sw)
        {
            var result = new List<PEST_PHOTO_Model>();
            DataTable dt = BaseDT.PEST_PHOTO.getDT(sw);//列表
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PEST_PHOTO_Model m = new PEST_PHOTO_Model();
                m.PEST_PHOTOID = dt.Rows[i]["PEST_PHOTOID"].ToString();
                m.PHOTOTITLE = dt.Rows[i]["PHOTOTITLE"].ToString();
                m.PHOTOFILENAME = dt.Rows[i]["PHOTOFILENAME"].ToString();
                m.PHOTOEXPLAIN = dt.Rows[i]["PHOTOEXPLAIN"].ToString();
                m.PHOTOTIME = ClsSwitch.SwitTM(dt.Rows[i]["PHOTOTIME"].ToString());
                m.PHOTOTYPE = dt.Rows[i]["PHOTOTYPE"].ToString();
                m.PRID = dt.Rows[i]["PRID"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion
    }
}
