using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PublicClassLibrary.PublicCom
{
    /// <summary>
    /// 
    /// </summary>
    public class PublicTools
    {
        /// <summary>
        /// list to datable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(IEnumerable<T> collection)
        {
            var props = typeof(T).GetProperties();
            var dt = new DataTable();
            dt.Columns.AddRange(props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray());
            if (collection.Count() > 0)
            {
                for (int i = 0; i < collection.Count(); i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in props)
                    {
                        object obj = pi.GetValue(collection.ElementAt(i), null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    dt.LoadDataRow(array, true);
                }
            }
            return dt;
        }

        /// <summary>
        /// datable to list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static IList<T> ConvertTo<T>(DataTable table)
        {
            if (table == null)
            {
                return null;
            }
            List<DataRow> rows = new List<DataRow>();

            foreach (DataRow row in table.Rows)
            {
                rows.Add(row);
            }
            return ConvertTo<T>(rows);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static IList<T> ConvertTo<T>(IList<DataRow> rows)
        {
            IList<T> list = null;

            if (rows != null)
            {
                list = new List<T>();
                foreach (DataRow row in rows)
                {
                    T item = CreateItem<T>(row);
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <returns></returns>
        public static T CreateItem<T>(DataRow row)
        {
            T obj = default(T);
            if (row != null)
            {
                obj = Activator.CreateInstance<T>();

                foreach (DataColumn column in row.Table.Columns)
                {
                    PropertyInfo prop = obj.GetType().GetProperty(column.ColumnName);
                    try
                    {
                        //Get value for the column
                        object value = (row[column.ColumnName].GetType() == typeof(DBNull))
                        ? null : row[column.ColumnName];
                        //Set property value
                        if (prop.CanWrite)    //判断其是否可写
                            prop.SetValue(obj, value == null ? null : value.ToString(), null);
                        //object value = row[column.ColumnName];
                        // prop.SetValue(obj, value, null);
                    }
                    catch (Exception)
                    {  
                        //You can log something here     
                        //throw;     
                    }
                    //try
                    //{
                    //    object value = row[column.ColumnName];
                    //    prop.SetValue(obj, value null);
                    //}
                    //catch
                    //{  //You can log something here     
                    //    //throw;    
                    //}
                }
            }
            return obj;
        }
    }

}
