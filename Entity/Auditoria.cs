using System;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class Auditoria
    {
        public DateTime? fFechaCrea { get; set; }

        public DateTime? fFechaEdita { get; set; }

        public int? fk_eUsuarioCrea { get; set; }

        public int? fk_eUsuarioEdita { get; set; }

        public bool isDeleted { get; set; }

        [Display(Name = "Desactivar")]
        public bool isDisabled { get; set; }
    }
}
