using BaseProject.Model.Persistence;
using FluentValidation;

namespace BaseProject.Model
{
    public class Usuario : BaseEntity
    {
        public int pkUsuario { get; set; }
        public string cUsuario { get; set; }
        public string cPassword { get; set; }
    }

    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        public UsuarioValidator()
        {
            RuleFor(usuario => usuario.cUsuario).NotEmpty().WithMessage("Ingresar un Nombre de Usuario!!!");
            RuleFor(usuario => usuario.cPassword).NotEmpty().WithMessage("Ingresar una Contraseña!!!");

        }
    }
}
