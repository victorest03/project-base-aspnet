using System.ComponentModel.DataAnnotations;
using BaseProject.Model;

namespace BaseProject.Frontend.ViewModel
{
    public class UsuarioViewModel
    {
        public int pkUsuario { get; set; }
        public string cUsuario { get; set; }
        public string cPassword { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese un {0}!!!")]
        [Display(Name = "Nombres")]
        public string cNombres { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese un {0}!!!")]
        [Display(Name = "Apellidos")]
        public string cApellidos { get; set; }

        public bool isFirstSession { get; set; }

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

    public class Login
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese un {0}!!!")]
        [Display(Name = "Usuario")]
        public string cUsuario { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string cPassword { get; set; }

        [Display(Name = "¿Recordar cuenta?")]
        public bool isRememberMe { get; set; }
    }

    public class ResetPassword
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string cOldPassword { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva Contraseña")]
        public string cNewPassword { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
        public string cConfirmPassword { get; set; }
    }

    public class ResetPasswordFirstSession
    {

        [Required(AllowEmptyStrings = false)]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva Contraseña")]
        public string cNewPassword { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("cNewPassword", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
        public string cConfirmPassword { get; set; }
    }
}
