using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// YJ_DCSMS_SENDCls
    /// </summary>
    public class YJ_DCSMS_SENDCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(YJ_DCSMS_SEND_SW m)
        {
            Message ms = null;
            if (m.opMethod == "Add")
            {
                // SystemCls.LogSave("3", "系统用户:" + m.LOGINUSERNAME, ClsStr.getModelContent(m));
                ms = BaseDT.YJ_DCSMS_SEND.Add(m);//增加发送短信
            }
            return ms;
        }
        #endregion
    }
}
