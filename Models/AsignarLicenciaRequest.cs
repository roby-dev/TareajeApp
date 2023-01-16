using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models {
    public class AsignarLicenciaRequest {
        public string Key { get; set; } = null!;
        public long UsuarioId { get; set; }
    }
}
