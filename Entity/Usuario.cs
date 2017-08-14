using System;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class Usuario : Auditoria
    {
        public int pk_eUsuario { get; set; }
        public string cDocIdentidad { get; set; }
        public string cEmail { get; set; }
        public string cNombres { get; set; }
        public string cApellido { get; set; }
        public int fk_eRol { get; set; }
        public DateTime? ffechaUltimaSesion { get; set; }
        public int fk_eEmpresa { get; set; }
    }

    public class Login
    {
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Correo electrónico")]
        [EmailAddress]
        public string cEmail { get; set; }

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
    
}
