using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BaseProject.Common.Model;
using NLog;
using NPoco;

namespace BaseProject.DataAccess.Repository
{
    public abstract class Repository<TEntity> where TEntity : class
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
                    result = db.Fetch<TEntity>("where isDelete != 1");
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
                    var entity = Get(id) as BaseEntity;
                    if (entity != null)
                    {
                        entity.fFechaEdita = DateTime.UtcNow;
                        entity.isDelete = true;
                        db.Update(entity, id, new[] { "isDelete", "fFechaEdita" });
                    }
                    else
                    {
                        return new Result { Message = "Registro no encontrado!!!" };
                    }
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
                    var castEntity = entity as BaseEntity;
                    castEntity.fFechaCrea = DateTime.UtcNow;
                    castEntity.fFechaEdita = null;
                    castEntity.fkUsuarioEdita = null;
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
                    var castEntity = entity as BaseEntity;
                    castEntity.fFechaEdita = DateTime.UtcNow;

                    db.Update(entity, entity.GetType().GetProperties().Select(s => s.Name).Where(w => w != "fkUsuarioCrea" && w != "fFechaCrea").ToArray());
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
