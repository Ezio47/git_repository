using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 有害生物_采集数据上报上传表
    /// </summary>
    public class PEST_COLLECT_DATAUPLOADCls
    {
        #region 增删改
        /// <summary>
        /// 增删改
        /// </summary>
        /// <param name="m">参见模型PEST_COLLECT_DATAUPLOAD_Model</param>
        /// <returns>参见模型Message</returns>
        public static Message Manager(PEST_COLLECT_DATAUPLOAD_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msg = BaseDT.PEST_COLLECT_DATAUPLOAD.Add(m);
                if (msg.Success == false)
                    return new Message(msg.Success, msg.Msg, "");
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Mdy")
            {
                Message msg = BaseDT.PEST_COLLECT_DATAUPLOAD.Mdy(m);
                if (msg.Success == false)
                    return new Message(msg.Success, msg.Msg, "");
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "MdyTP")
            {
                Message msg = BaseDT.PEST_COLLECT_DATAUPLOAD.MdyTP(m);
                if (msg.Success == false)
                    return new Message(msg.Success, msg.Msg, "");
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Del")
            {
                return BaseDT.PEST_COLLECT_DATAUPLOAD.Del(m);
            }
            return new Message(false, "无效操作", m.returnUrl);
        }
        #endregion

        #region 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<PEST_COLLECT_DATAUPLOAD_Model> getModelList(PEST_COLLECT_DATAUPLOAD_SW sw)
        {
            var result = new List<PEST_COLLECT_DATAUPLOAD_Model>();
            DataTable dt = BaseDT.PEST_COLLECT_DATAUPLOAD.getDT(sw);//列表
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PEST_COLLECT_DATAUPLOAD_Model m = new PEST_COLLECT_DATAUPLOAD_Model();
                m.PESTCOLLDATAUPLOADID = dt.Rows[i]["PESTCOLLDATAUPLOADID"].ToString();
                m.PESTCOLLDATAID = dt.Rows[i]["PESTCOLLDATAID"].ToString();
                m.UPLOADNAME = dt.Rows[i]["UPLOADNAME"].ToString();
                m.UPLOADDESCRIBE = dt.Rows[i]["UPLOADDESCRIBE"].ToString();
                m.UPLOADURL = dt.Rows[i]["UPLOADURL"].ToString();
                m.UPLOADTYPE = dt.Rows[i]["UPLOADTYPE"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion
    }
}
