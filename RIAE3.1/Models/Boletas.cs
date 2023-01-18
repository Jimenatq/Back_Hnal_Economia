using System.ComponentModel.DataAnnotations;

namespace RIAE3._1.Models
{
    public class Boletas
    {
        [Key]public int IdBoleta { get; set; }
        public int IdRegistro { get; set; }
        public int IdParametro { get; set; }
        public decimal ImporteUnitarioClasificador { get; set; }
    }
}
