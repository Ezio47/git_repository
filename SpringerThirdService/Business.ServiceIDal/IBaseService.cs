using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLW.AH.Business.ServiceIDal
{
    /// <summary>
    /// 提供通用的基础增删改查
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseService<T> where T : class
    {
        /// <summary>
        /// 根据id查询实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Find(int id);

        /// <summary>
        /// 一次性查出全部数据
        /// </summary>
        /// <returns></returns>
        List<T> FindAll();

        /// <summary>
        /// 提供对单表的查询
        /// </summary>
        /// <returns></returns>
        IQueryable<T> Set();

        /// <summary>
        /// 新增数据，即时Commit
        /// </summary>
        /// <param name="t"></param>
        T Insert(T t);

        /// <summary>
        /// 更新数据，即时Commit
        /// </summary>
        /// <param name="t"></param>
        void Update(T t);

        /// <summary>
        /// 删除数据，即时Commit
        /// </summary>
        /// <param name="t"></param>
        void Delete(T t);

        /// <summary>
        /// 根据主键删除数据，即时Commit
        /// </summary>
        /// <param name="t"></param>
        void Delete(int Id);

        /// <summary>
        /// 立即保存全部修改
        /// </summary>
        void Commit();

        /// <summary>
        /// 实现对数据库的查询  --简单查询
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        IQueryable<T> LoadEntities(Func<T, bool> whereLambda);

        /// <summary>
        /// 实现对数据的分页查询
        /// </summary>
        /// <typeparam name="S">按照某个类进行排序</typeparam>
        /// <param name="pageIndex">当前第几页</param>
        /// <param name="pageSize">一页显示多少条数据</param>
        /// <param name="total">总条数</param>
        /// <param name="whereLambda">取得排序的条件</param>
        /// <param name="isAsc">如何排序，根据倒叙还是升序</param>
        /// <param name="orderByLambda">根据那个字段进行排序</param>
        /// <returns></returns>

        IQueryable<T> LoadPageEntities<S>(int pageIndex, int pageSize, out int total, Func<T, bool> whereLambda, bool isAsc, Func<T, S> orderByLambda);

        /// <summary>
        /// 执行sql 返回集合
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IQueryable<T> ExcuteQuery(string sql, SqlParameter[] parameters);

        /// <summary>
        /// 执行sql，无返回
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        void Excute(string sql, SqlParameter[] parameters);
    }
}
