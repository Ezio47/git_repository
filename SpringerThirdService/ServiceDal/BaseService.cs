using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLW.AH.Business.ServiceIDal;

namespace TLW.AH.Business.ServiceDal
{
    /// <summary>
    /// 提供通用的基础增删改查实现
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseService<T> : IBaseService<T> where T : class
    {
        #region Identity
        public DbContext Context { get; set; }
        private DbSet<T> TDbSet;

        public BaseService(DbContext context)
        {
            this.Context = context;
            this.TDbSet = this.Context.Set<T>();
        }
        #endregion Identity


        public T Insert(T t)
        {
            this.TDbSet.Add(t);
            this.Commit();
            return t;
        }

        public T Find(int id)
        {
            return this.TDbSet.Find(id);
        }

        public List<T> FindAll()
        {
            return this.TDbSet == null ? null : TDbSet.ToList();
        }

        public IQueryable<T> Set()
        {
            return this.TDbSet;
        }

        public void Update(T t)
        {
            if (t == null) throw new Exception("t is null");
            this.Commit();
        }

        public void Delete(T t)
        {
            if (t == null) throw new Exception("t is null");
            this.TDbSet.Remove(t);
            this.Commit();
        }

        public void Delete(int Id)
        {
            T t = this.Find(Id);
            if (t == null) throw new Exception("t is null");
            this.TDbSet.Remove(t);
            this.Commit();
        }

        public void Commit()
        {
            this.Context.SaveChanges();
        }


        /// <summary>
        /// 实现对数据库的查询  --简单查询
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public IQueryable<T> LoadEntities(Func<T, bool> whereLambda)
        {
            return this.TDbSet.Where<T>(whereLambda).AsQueryable();
        }


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
        public IQueryable<T> LoadPageEntities<S>(int pageIndex, int pageSize, out  int total, Func<T, bool> whereLambda,
            bool isAsc, Func<T, S> orderByLambda)
        {
            var temp = this.TDbSet.Where<T>(whereLambda);

            total = temp.Count(); //得到总的条数
            //排序,获取当前页的数据
            if (isAsc)
            {
                temp = temp.OrderBy<T, S>(orderByLambda)

                     .Skip<T>(pageSize * (pageIndex - 1)) //越过多少条

                     .Take<T>(pageSize).AsQueryable(); //取出多少条
            }
            else
            {
                temp = temp.OrderByDescending<T, S>(orderByLambda)

                    .Skip<T>(pageSize * (pageIndex - 1)) //越过多少条

                    .Take<T>(pageSize).AsQueryable(); //取出多少条
            }
            return temp.AsQueryable();

        }



        /// <summary>
        /// 执行sql返回泛型
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IQueryable<T> ExcuteQuery(string sql, SqlParameter[] parameters)
        {
            return this.Context.Database.SqlQuery<T>(sql, parameters).AsQueryable();
        }

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        public void Excute(string sql, SqlParameter[] parameters)
        {
            DbContextTransaction trans = null;
            try
            {
                trans = this.Context.Database.BeginTransaction();
                Context.Database.ExecuteSqlCommand(sql, parameters);
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                throw ex;
            }
        }

    }
}
