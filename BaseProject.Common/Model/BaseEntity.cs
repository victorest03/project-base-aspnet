using System;

namespace BaseProject.Common.Model
{
    public class BaseEntity
    {
        public int? fkUsuarioCrea { get; set; }
        public DateTime? fFechaCrea { get; set; }
        public int? fkUsuarioEdita { get; set; }
        public DateTime? fFechaEdita { get; set; }
        public bool isDelete { get; set; }
    }
}
