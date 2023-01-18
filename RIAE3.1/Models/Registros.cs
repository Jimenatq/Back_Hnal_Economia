using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RIAE3._1.Models
{
    public class Registros
    {
        [Key]
        public int IdRegistro { get; set; }
        public int IdParametroTipo { get; set; }
        public int IdParametroSubtipo { get; set; }
        public decimal NroRecibo { get; set; }
        public DateTime Fecha { get; set; }
        public decimal ImporteTotalBoleta { get; set; }
        public decimal? Igv { get; set; }
        public decimal? MontoIgv { get; set; }
        public string NombreEmpresa { get; set; }
        public string NotaInformativa { get; set; }
        public string NombreFactura { get; set; }
        public DateTime? FechaGlosa { get; set; }
        public decimal? ImporteDeposito { get; set; }
        public decimal? ImporteTotalTipoIP { get; set; }
        public decimal? ImporteTotalTipoFR { get; set; }
        public int? NroVoucher { get; set; }
        public decimal? MontoVoucher { get; set; }
        public int? NroCheque { get; set; }
        public decimal? MontoCheque { get; set; }
        public int? NroNotaAbono { get; set; }
        public decimal? MontoNotaAbono { get; set; }
        public string NombreBanco { get; set; }
        public string TextoGlosa { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        //public virtual Boletas Categoria { get; set; }
    }
}
