using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using PublicClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 数据采集类
    /// </summary>
    public class T_COLLECTLINESCls
    {

        #region  处理
        /// <summary>
        /// 数据采集处理
        /// </summary>
        /// <param name="m">参见模型T_COLLECTLINES_Model</param>
        /// <returns>参见模型Message</returns>
        public static Message Manager(T_COLLECTLINES_Model m)
        {
            if (m.opMethod == "ADD")
            {
                T_COLLECTLINES_SW sw = new T_COLLECTLINES_SW();
                var id = BaseDT.T_COLLECTLINES.GetLineMaxObjID();
                sw.OBJECTID = Convert.ToInt32(id) + 1;
                sw.NAME = m.NAME;
                sw.TypeId = m.TypeId;
                sw.Shape = m.Shape;
                SystemCls.LogSave("11", "空间线:" + m.NAME, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.T_COLLECTLINES.AddLines(sw);
                return new Message(msgUser.Success, msgUser.Msg, "");
            }

            return new Message(false, "无效操作", "");
        }
        #endregion
    }
}
