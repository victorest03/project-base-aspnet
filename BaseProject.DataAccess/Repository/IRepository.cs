using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BaseProject.Common.Model;

namespace BaseProject.DataAccess.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        List<TEntity> GetAll();

        List<TEntity> Query(Expression<Func<TEntity, bool>> expression);

        TEntity Get(int id);

        Result Delete(int id);

        Result Save(TEntity entity);
    }
}
