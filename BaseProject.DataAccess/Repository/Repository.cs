using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BaseProject.Common.Model;
using BaseProject.Model.Persistence;
using NLog;
using NPoco;

namespace BaseProject.DataAccess.Repository
{
    public class Repository<TEntity> where TEntity : class
    {
        public static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public virtual List<TEntity> GetAll()
        {
            var result = new List<TEntity>();

            try
            {
                using (IDatabase db = DbContext.GetInstance())
                {
                    #region Select All
                    result = db.Fetch<TEntity>("where isDelete = 1");
                    #endregion
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }

            return result;
        }

        public virtual List<TEntity> Query(Expression<Func<TEntity, bool>> expression)
        {
            var result = new List<TEntity>();

            try
            {
                using (IDatabase db = DbContext.GetInstance())
                {
                    #region Select Where

                    result = db.Query<TEntity>().Where(expression).ToList();

                    #endregion
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }

            return result;
        }

        public virtual TEntity Get(int id)
        {
            TEntity result = null;

            try
            {
                using (IDatabase db = DbContext.GetInstance())
                {
                    result = db.SingleById<TEntity>(id);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }

            return result;
        }

        public virtual Result Delete(int id)
        {
            var result = new Result();
            try
            {
                using (IDatabase db = DbContext.GetInstance())
                {
                    db.Update(new BaseEntity {isDelete = true, fFechaEdita = DateTime.UtcNow}, id,new []{ "isDelete", "fFechaEdita" });
                }

                result.Success = true;
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                result.Message = e.Message;
            }

            return result;
        }

        public virtual Result Insert(TEntity entity)
        {
            var result = new Result();
            try
            {
                using (IDatabase db = DbContext.GetInstance())
                {
                    db.Insert(entity);
                }

                result.Success = true;
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                result.Message = e.Message;
            }

            return result;
        }

        public virtual Result Update(TEntity entity)
        {
            var result = new Result();
            try
            {
                using (IDatabase db = DbContext.GetInstance())
                {
                    db.Update(entity);
                }

                result.Success = true;
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                result.Message = e.Message;
            }

            return result;
        }
    }
}
