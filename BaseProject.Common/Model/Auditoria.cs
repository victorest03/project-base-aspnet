using System;

namespace BaseProject.Common.Model
{
    public class Auditoria
    {
        public DateTime? fFechaCrea { get; set; }

        public DateTime? fFechaEdita { get; set; }

        public int? fkUsuarioCrea { get; set; }

        public int? fkUsuarioEdita { get; set; }

        public bool isDeleted { get; set; }

        public bool isDisabled { get; set; }
    }
}
