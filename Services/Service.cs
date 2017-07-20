using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Teage.Common;
using Teage.Entity;

namespace Services
{
    public abstract class Service
    {
        protected Entity ContextDb()
        {
            return new Entity();
        }
        /// <summary>
        /// 数据操作(无返回值)
        /// </summary>
        /// <param name="action"></param>
        protected void Try(Action<Entity> action)
        {
            try
            {
                using (var context = ContextDb())
                {
                    action(context);
                }
            }
            catch (Exception ex)
            {
                Log.Error("执行数据库错误。", ex);
                throw ex;
            }
        }
        /// <summary>
        /// 数据操作(返回dynamic)
        /// </summary>
        /// <param name="func"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        protected dynamic Try(Func<Entity, dynamic> func, dynamic def = null)
        {
            try
            {
                using (var context = ContextDb())
                {
                    var result = func(context);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error("执行数据库错误。", ex);
                return def ?? default(dynamic);
            }
        }
        /// <summary>
        /// 事务操作(返回dynamic)
        /// </summary>
        /// <param name="func"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        protected dynamic Try(Func<Entity, TransactionScope, dynamic> func, dynamic def = null)
        {
            using (var tran = new TransactionScope())
            {
                try
                {
                    using (var context = ContextDb())
                    {
                        var result = func(context, tran);
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("执行数据库错误。", ex);
                    return def ?? default(dynamic);
                }
                finally
                {
                    tran.Dispose();
                }
            }
        }
    }

    public abstract class Service<TContext> where TContext : DbContext, new()
    {
        protected TContext ContextDb()
        {
            return new TContext();
        }
        /// <summary>
        /// 数据操作(无返回值)
        /// </summary>
        /// <param name="action"></param>
        protected void Try(Action<TContext> action)
        {
            try
            {
                using (var context = ContextDb())
                {
                    action(context);
                }
            }
            catch (Exception ex)
            {
                Log.Error("执行数据库错误。", ex);
                throw ex;
            }
        }
        /// <summary>
        /// 数据操作(返回dynamic)
        /// </summary>
        /// <param name="func"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        protected dynamic Try(Func<TContext, dynamic> func, dynamic def = null)
        {
            try
            {
                using (var context = ContextDb())
                {
                    var result = func(context);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error("执行数据库错误。", ex);
                return def ?? default(dynamic);
            }
        }
        /// <summary>
        /// 事务操作(返回dynamic)
        /// </summary>
        /// <param name="func"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        protected dynamic Try(Func<TContext, TransactionScope, dynamic> func, dynamic def = null)
        {
            using (var tran = new TransactionScope())
            {
                try
                {
                    using (var context = ContextDb())
                    {
                        var result = func(context, tran);
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("执行数据库错误。", ex);
                    return def ?? default(dynamic);
                }
                finally
                {
                    tran.Dispose();
                }
            }
        }
    }
}
