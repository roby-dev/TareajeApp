using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Models {
    public class Usuario {
        public long Id { get; set; }
        public string? User { get; set; }
        public string Password { get; set; } = "";
        public long PersonaId { get; set; }
    }
}
