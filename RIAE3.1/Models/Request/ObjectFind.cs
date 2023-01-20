using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RIAE3._1.Models.Request
{
    public class ResultModel
    {
        public int IdBoleta { get; set; }
        public int IdRegistro { get; set; }
        public int IdParametro { get; set; }
        public decimal ImporteUnitarioClasificador { get; set; }
    }
    public class ObjectFind
    {
        public bool IsCompleted { get; set; }
        public bool IsCompletedSuccessfully { get; set; }
        public bool IsFaulted { get; set; }
        public bool IsCanceled { get; set; }
        public ResultModel Result { get; set; }
        
    }
}
