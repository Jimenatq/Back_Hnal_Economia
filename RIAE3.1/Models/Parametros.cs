namespace RIAE3._1.Models
{
    public class Parametros
    {
        public int IdParametro { get; set; }
        public int IdTipoParametro { get; set; }
        public string Descripcion { get; set; }
        public int Codigo { get; set; }
        public int CodClasificadorArea { get; set; }
        public int CodClasificadorExterno { get; set; }
        public bool Estado { get; set; }
    }
}
