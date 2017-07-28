using DataBaseClassLibrary;
using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary.BaseDT
{
    /// <summary>
    /// 数据采集 点公共类
    /// </summary>
    public class T_COLLECTPOINTS
    {

        /// <summary>
        /// 增加点
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>

        public static Message AddPoints(T_COLLECTPOINTS_SW m)
        {
            StringBuilder sb = new StringBuilder();
            ////([OBJECTID],[TYPEID],[NAME],[SHAPE])
            sb.AppendFormat(" Insert  INTO  T_COLLECTPOINTS (OBJECTID,TYPEID,NAME,SHAPE)");
            sb.AppendFormat(" values({0},{1},'{2}','{3}') ", m.OBJECTID, m.TypeId, m.NAME, m.Shape);
            bool bln = SDEDataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "空间数据点处理成功！", "");
            else
                return new Message(false, "空间数据点处理失败！", "");
        }

        /// <summary>
        /// 获取最大主键ID
        /// </summary>
        /// <returns>参见模型</returns>
        public static string GetPointMaxObjID()
        {
            string sql = " SELECT max(OBJECTID) FROM  T_COLLECTPOINTS ";
            return SDEDataBaseClass.ReturnSqlField(sql);
        }
    }
}
