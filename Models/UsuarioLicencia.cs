using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models {
    public class UsuarioLicencia {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long LicenciaId { get; set; }
        public DateTime Fecha { get; set; }
    }
}
