namespace Models {
    public class Persona {
        public long Id { get; set; }
        public string? Nombres { get; set; }    
        public string? ApellidoPaterno { get; set; }    
        public string? ApellidoMaterno { get; set; }    
        public string? Documento { get; set; } 
        public string NombreCompleo { get =>
            $"{ApellidoPaterno} {ApellidoMaterno}, {Nombres}";
        }
    }
}
