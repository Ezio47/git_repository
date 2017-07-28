using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace ManagerSystem.MVC.HelpCom
{
    /// <summary>
    /// 修改webconfig
    /// </summary>
    public class ConfigCom
    {
        #region 修改config文件
        /// <summary>  
        /// 修改config文件(AppSetting节点)  
        /// </summary>  
        /// <param name="key">键</param>  
        /// <param name="value">要修改成的值</param>  
        public static void UpdateAppSetting(string key, string value)
        {
            XmlDocument doc = new XmlDocument();
            //获得配置文件的全路径   
            string strFileName = AppDomain.CurrentDomain.BaseDirectory.ToString() + "Web.config";
            doc.Load(strFileName);
            //找出名称为“add”的所有元素   
            XmlNodeList nodes = doc.GetElementsByTagName("add");
            for (int i = 0; i < nodes.Count; i++)
            {
                //获得将当前元素的key属性   
                XmlAttribute _key = nodes[i].Attributes["key"];
                //根据元素的第一个属性来判断当前的元素是不是目标元素   
                if (_key != null)
                {
                    if (_key.Value == key)
                    {
                        //对目标元素中的第二个属性赋值   
                        _key = nodes[i].Attributes["value"];

                        _key.Value = value;
                        break;
                    }
                }
            }
            //保存上面的修改   
            doc.Save(strFileName);
        }

        /// <summary>  
        /// 修改config文件(ConnectionString节点)  
        /// </summary>  
        /// <param name="name">键</param>  
        /// <param name="value">要修改成的值</param>  
        public static void UpdateConnectionString(string name, string value)
        {
            XmlDocument doc = new XmlDocument();
            //获得配置文件的全路径   
            string strFileName = AppDomain.CurrentDomain.BaseDirectory.ToString() + "Web.config";
            doc.Load(strFileName);
            //找出名称为“add”的所有元素   
            XmlNodeList nodes = doc.GetElementsByTagName("add");
            for (int i = 0; i < nodes.Count; i++)
            {
                //获得将当前元素的key属性   
                XmlAttribute _name = nodes[i].Attributes["name"];
                //根据元素的第一个属性来判断当前的元素是不是目标元素   
                if (_name != null)
                {
                    if (_name.Value == name)
                    {
                        //对目标元素中的第二个属性赋值   
                        _name = nodes[i].Attributes["connectionString"];

                        _name.Value = value;
                        break;
                    }
                }
            }
            //保存上面的修改   
            doc.Save(strFileName);
        }
        #endregion
    }
}