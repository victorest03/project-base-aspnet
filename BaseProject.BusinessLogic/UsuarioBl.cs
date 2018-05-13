using BaseProject.Common.Model;
using BaseProject.DataAccess;
using BaseProject.DataAccess.Repository;
using BaseProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BaseProject.BusinessLogic
{
    public class UsuarioBl : IRepository<Usuario>
    {
        private readonly UsuarioDa _da = new UsuarioDa();
        public List<Usuario> GetAll()
        {
            return _da.GetAll();
        }

        public List<Usuario> Query(Expression<Func<Usuario, bool>> expression)
        {
            return _da.Query(expression);
        }

        public Usuario Get(int id)
        {
            return _da.Get(id);
        }

        public Result Delete(int id)
        {
            return _da.Delete(id);
        }

        public Result Save(Usuario entity)
        {
            var result = new UsuarioValidator().Validate(entity);
            return result.IsValid ? entity.pkUsuario == 0 ? _da.Insert(entity):_da.Update(entity) : new Result { Errors = result.Errors.Select(s => new Error(s.PropertyName, s.ErrorMessage)) };
        }
    }
}
