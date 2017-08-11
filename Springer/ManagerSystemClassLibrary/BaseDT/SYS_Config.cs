using DataBaseClassLibrary;
using ManagerSystemModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary.BaseDT
{
    /// <summary>
    /// 系统配置
    /// </summary>
    public class SYS_Config
    {
        /// <summary>
        /// 更新数据库
        /// </summary>
        /// <param name="sql">以英文逗号分割的sql语句</param>
        /// <returns></returns>
        public static Message UpdateDataBase(string sql)
        {
            List<string> sqllist = new List<string>();
            string[] arrSql = sql.Split('\n');
            for (int i = 0; i < arrSql.Length; i++)
            {
                sqllist.Add(arrSql[i]);
            }
            var y = DataBaseClass.ExecuteSqlTran(sqllist);
            if (y > 0)
                return new Message(true, "更新成功!", "");
            else
                return new Message(false, "更新失败,事物回滚机制!", "");
        }
    }
}
