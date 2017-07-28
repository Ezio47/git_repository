using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace PublicClassLibrary
{
    /// <summary>
    /// JSON操作类
    /// </summary>
    public class ClsJson
    {
        /// <summary>
        /// 将实体转化为json
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>
        /// 返回值
        /// 成功：json
        /// 失败：null
        /// </returns>
        public static string EntityToJSON(object entity)
        {
            string vret = null;
            try
            {
                JsonSerializerSettings setting = new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };
                vret = JsonConvert.SerializeObject(entity, setting);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return vret;
        }

        /// <summary>
        /// 将Json转化为实体
        /// </summary>
        /// <param name="json">实体</param>
        /// <returns>
        /// 返回值
        /// 成功：json
        /// 失败：null
        /// </returns>
        public static T JSONToEntity<T>(string json)
        {
            T vret = default(T);
            try
            {
                JavaScriptSerializer jsonconvet = new JavaScriptSerializer();
                jsonconvet.MaxJsonLength = Int32.MaxValue;//设置最大值
                vret = jsonconvet.Deserialize<T>(json);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return vret;
        }
    }
}
