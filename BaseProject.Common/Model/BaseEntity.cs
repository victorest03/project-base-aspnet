using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Model.Persistence
{
    public class BaseEntity
    {
        public int fkUsuarioCrea { get; set; }
        public DateTime fFechaCrea { get; set; }
        public int fkUsuarioEdita { get; set; }
        public DateTime fFechaEdita { get; set; }
        public bool isDelete { get; set; }
    }
}
