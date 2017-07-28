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
    /// 数据采集 点操作类Nl
    /// </summary>
    public class T_COLLECTPOINTSCls
    {
        #region  处理
        /// <summary>
        /// 数据采集点处理
        /// </summary>
        /// <param name="m">参见模型T_COLLECTPOINTS_Model</param>
        /// <returns>参见模型Message</returns>
        public static Message Manager(T_COLLECTPOINTS_Model m)
        {
            if (m.opMethod == "ADD")
            {
                T_COLLECTPOINTS_SW sw = new T_COLLECTPOINTS_SW();
                var id = BaseDT.T_COLLECTPOINTS.GetPointMaxObjID();
                sw.OBJECTID = Convert.ToInt32(id) + 1;
                sw.NAME = m.NAME;
                sw.TypeId = m.TypeId;
                sw.Shape = m.Shape;
                SystemCls.LogSave("10", "空间点:" + m.NAME, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.T_COLLECTPOINTS.AddPoints(sw);
                return new Message(msgUser.Success, msgUser.Msg, "");
            }

            return new Message(false, "无效操作", "");
        }
        #endregion
    }
}
