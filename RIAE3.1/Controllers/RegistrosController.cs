using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32;
using RIAE3._1.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using RIAE3._1.Controllers;
using RIAE3._1.Context;
using System.Linq;
using RIAE3._1.Models.Request;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RIAE3._1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrosController : ControllerBase
    {
        public readonly AplicationDbContext _context;
        public RegistrosController(AplicationDbContext context)
        {
            _context = context;
            //Configuration = configuration;
        }
        //public IConfiguration Configuration { get; }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                //var lista =  _context.Registros.Include(p => p.Categoria).ToList();
                var lista = _context.Registros.ToList();
                //var lista = _context.Registros.ToList();

                /*foreach (var modelregistro in lista)
                {
                    if (modelregistro.IdRegistro=)
                    {

                    }
                }*/
                //var lista= _context.Boletas.ToList();
                //getlist.Add(getlist);

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
            /*
            string query = @"
                            select dbo.Registros.IdRegistro, IdParametroTipo, case IdParametroTipo when 1 then 'Ingresos Propios'
                            when 2 then 'Fondo Rotatorio' end as 'NombreTipo', IdParametroSubtipo,
							case IdParametroSubtipo when 3 then 'Recaudación'
                            when 4 then 'Penalidad' when 5 then 'Factura'
                            when 6 then 'Protocolo' when 7 then 'Detracción'
                            when 8 then 'Otros servicios' when 9 then 'Otros ingresos'
                            when 10 then 'Ingresos diversos' when 11 then 'Recaudación por efectivo de caja'
                            when 12 then 'Pago de facturas - Cheque' when 13 then 'Pago de facturas - Nota de Abono'
                            when 14 then 'Otros pagos' end as 'NombreSubtipo', NroRecibo,
                            convert(varchar(10), Fecha, 103) as Fecha, ImporteTotalBoleta, Igv, MontoIgv,
                            NombreEmpresa,NotaInformativa, NombreFactura, convert(varchar(10), FechaGlosa, 103)
                            as FechaGlosa, ImporteDeposito, ImporteTotalTipoIP, ImporteTotalTipoFR, NroVoucher,
                            MontoVoucher, NroCheque, MontoCheque, NroNotaAbono, MontoNotaAbono, NombreBanco,
                            TextoGlosa, UsuarioCreacion, convert(varchar(10), FechaCreacion, 103) as FechaCreacion,
                            UsuarioModificacion, convert(varchar(10), FechaModificacion, 103) as FechaModificacion,
                            IdBoleta, IdParametro, ImporteUnitarioClasificador
                            from dbo.Registros, dbo.Boletas
							WHERE dbo.Registros.IdRegistro = dbo.Boletas.IdRegistro;
                            ";
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(Configuration.GetConnectionString("RiaeAppConex")))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);*/
        }

        [HttpPost]
        public IActionResult Post(RegistrosRequest registros)
        {
            try
            {
                var registro = new Registros();
                registro.IdParametroTipo = registros.IdParametroTipo;
                registro.IdParametroSubtipo = registros.IdParametroSubtipo;
                registro.NroRecibo = registros.NroRecibo;
                registro.Fecha = registros.Fecha;
                registro.ImporteTotalBoleta = registros.ImporteTotalBoleta;
                registro.Igv = registros.Igv;
                registro.MontoIgv = registros.MontoIgv;
                registro.NombreEmpresa = registros.NombreEmpresa;
                registro.NotaInformativa = registros.NotaInformativa;
                registro.NombreFactura = registros.NombreFactura;
                registro.FechaGlosa = registros.FechaGlosa;
                registro.ImporteDeposito = registros.ImporteDeposito;
                registro.ImporteTotalTipoIP = registros.ImporteTotalTipoIP;
                registro.ImporteTotalTipoFR = registros.ImporteTotalTipoFR;
                registro.NroVoucher = registros.NroVoucher;
                registro.MontoVoucher = registros.MontoVoucher;
                registro.NroCheque = registros.NroCheque;
                registro.MontoCheque = registros.MontoCheque;
                registro.NroNotaAbono = registros.NroNotaAbono;
                registro.MontoNotaAbono = registros.MontoNotaAbono;
                registro.NroVoucher = registros.NroVoucher;
                registro.NombreBanco = registros.NombreBanco;
                registro.TextoGlosa = registros.TextoGlosa;
                registro.UsuarioCreacion = registros.UsuarioCreacion;
                registro.FechaCreacion = registros.FechaCreacion;
                registro.UsuarioModificacion = registros.UsuarioModificacion;
                registro.FechaModificacion = registros.FechaModificacion;
                _context.Registros.Add(registro);
                _context.SaveChanges();
                foreach (var modelBoleta in registros.listBoletas)
                {
                    var boleta = new Models.Boletas();
                    boleta.IdRegistro = registro.IdRegistro;
                    boleta.IdParametro = modelBoleta.IdParametro;
                    boleta.ImporteUnitarioClasificador = modelBoleta.ImporteUnitarioClasificador;
                    _context.Boletas.Add(boleta);
                    _context.SaveChanges();
                }
                return Ok("registro con boletas guardado");
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
    }
}
