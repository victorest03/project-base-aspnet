using BaseProject.Model;

namespace BaseProject.Frontend.ViewModel
{
    public class UsuarioViewModel
    {
        public int pkUsuario { get; set; }
        public string cUsuario { get; set; }
        public string cPassword { get; set; }

        public static implicit operator UsuarioViewModel(Usuario usuario)
        {
            return new UsuarioViewModel
            {
                pkUsuario = usuario.pkUsuario,
                cUsuario = usuario.cUsuario,
                cPassword = usuario.cPassword
            };
        }

        public static implicit operator Usuario(UsuarioViewModel vm)
        {
            return new Usuario
            {
                pkUsuario = vm.pkUsuario,
                cUsuario = vm.cUsuario,
                cPassword = vm.cPassword
            };
        }
    }
}
