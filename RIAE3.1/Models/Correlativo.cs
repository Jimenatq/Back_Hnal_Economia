using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RIAE3._1.Models
{
    public class Correlativo
    {
        public int IdCorrelativo { get; set; }
        public int IdParametro { get; set; }
        public string NombreCorrelativo { get; set; }
        public int NroCorrelativo { get; set; }
        public int Ano { get; set; }
    }
}
