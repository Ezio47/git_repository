using ManagerSystemModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 系统配置
    /// </summary>
    public class SYS_ConfigCls
    {
        #region 增删改
        /// <summary>
        /// 增删改
        /// </summary>
        /// <param name="sql">以英文逗号分割的sql语句</param>
        /// <returns>参见模型Message</returns>
        public static Message UpdateDataBase(string sql)
        {
            Message msg = BaseDT.SYS_Config.UpdateDataBase(sql);
            return new Message(msg.Success, msg.Msg, msg.Url);
        }
        #endregion
    }
}
