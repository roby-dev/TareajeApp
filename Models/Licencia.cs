namespace Models {
    public class Licencia {
        public long Id { get; set; }
        public string Codigo { get; set; } = null!;
        public int Estado { get; set; }

        public static Licencia Create(
            string codigo,
            int estado) {
            return new Licencia { Codigo= codigo,Estado=estado};
        }
    }
}
