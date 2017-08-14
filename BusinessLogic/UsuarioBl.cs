using DataAccess;
using Entity;

namespace BusinessLogic
{
    public class UsuarioBl
    {
        private readonly UsuarioDal _usuarioDal = new UsuarioDal();
        public Usuario Buscar(int pk_eUsuario)
        {
            return _usuarioDal.Buscar(pk_eUsuario);
        }
    }
}
