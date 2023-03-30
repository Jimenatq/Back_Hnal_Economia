
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RIAE3._1.Models;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using RIAE3._1.Context;
using System.Linq;
using RIAE3._1.Models.Request;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Collections.Generic;

namespace RIAE3._1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrosController : ControllerBase
    {
        public readonly AplicationDbContext _context;
        public RegistrosController(AplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            ListaRegistros prueba = new ListaRegistros();
            var listaRegistros = await _context.Registros.OrderByDescending(x => x.IdRegistro).ToListAsync();
            foreach (var registros in listaRegistros)
            {
                Registros registro = new Registros();
                registro.IdRegistro = registros.IdRegistro;
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
                registro.listBoletas = await _context.Boletas.Where(p => p.IdRegistro == registro.IdRegistro).ToListAsync();
                prueba.registros.Add(registro);
            }
            return Ok(prueba);
        }

        [HttpPost]
        [Route("ingpropios")]
        public async Task<IActionResult> GetIPropios(listaRegistrosPorAnio regis)
        {
            int anioSolicitado = regis.anio;
            ListaRegistros listaRegistrosIngresosPropios = new ListaRegistros();
            var listaRegistros = await _context.Registros.OrderByDescending(x => x.NroRecibo).Where(x => x.Fecha.Year == anioSolicitado && x.IdParametroTipo==1).ToListAsync();
            foreach (var registros in listaRegistros)
            {
                Registros registro = new Registros();
                registro.IdRegistro = registros.IdRegistro;
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
                registro.Anulado = registros.Anulado;
                registro.listBoletas = await _context.Boletas.Where(p => p.IdRegistro == registro.IdRegistro).ToListAsync();
                listaRegistrosIngresosPropios.registros.Add(registro);
            }
            return Ok(listaRegistrosIngresosPropios.registros);
        }

        [HttpPost]
        [Route("fondorotatorio")]
        public async Task<IActionResult> GetFondoRotatorio(listaRegistrosPorAnio regis)
        {
            int anioSolicitado = regis.anio;
            ListaRegistros listaRegistrosFondoRotatorio = new ListaRegistros();
            var listaRegistros = await _context.Registros.OrderByDescending(x => x.NroRecibo).Where(x => x.Fecha.Year == anioSolicitado && x.IdParametroTipo==2).ToListAsync();
            foreach (var registros in listaRegistros)
            {
                Registros registro = new Registros();
                registro.IdRegistro = registros.IdRegistro;
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
                registro.Anulado = registros.Anulado;
                registro.listBoletas = await _context.Boletas.Where(p => p.IdRegistro == registro.IdRegistro).ToListAsync();
                listaRegistrosFondoRotatorio.registros.Add(registro);
            }
            return Ok(listaRegistrosFondoRotatorio.registros);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Getid(int id)
        {
            var listaRegistros = await _context.Registros.ToListAsync();
            foreach (var registros in listaRegistros)
            {
                if (registros.IdRegistro == id)
                {
                    Registros registro = new Registros();
                    registro.IdRegistro = registros.IdRegistro;
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
                    registro.Anulado = registros.Anulado;
                    registro.listBoletas = await _context.Boletas.Where(p => p.IdRegistro == id).ToListAsync();
                    /*ListaRegistros prueba = new ListaRegistros();
                    prueba.registro.Add(_context.Registros.FirstOrDefault(i=>i.IdRegistro==id));
                    prueba.boletas = _context.Boletas.Where(p => p.IdRegistro == id).ToList();
                    return Ok(prueba);*/
                    return Ok(registro);
                }
            }
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> GuardarRegistro(RegistrosRequest registros)
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
                registro.Anulado = false;
                _context.Registros.Add(registro);
                await _context.SaveChangesAsync();
                foreach (var modelBoleta in registros.listBoletas)
                {
                    var boleta = new Models.Boletas();
                    boleta.IdRegistro = registro.IdRegistro;
                    boleta.IdParametro = modelBoleta.IdParametro;
                    boleta.ImporteUnitarioClasificador = modelBoleta.ImporteUnitarioClasificador;
                    _context.Boletas.Add(boleta);
                    await _context.SaveChangesAsync();
                }
                return Ok("registro con boletas guardado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> modificarRegistro(RegistrosRequest registros)
        {
            try
            {
                var registro = new Registros();
                registro.IdRegistro = registros.IdRegistro;
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
                registro.Anulado = registros.Anulado;
                _context.Entry(registro).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                foreach (var modelBoleta in registros.listBoletas)
                {
                    var boleta = new Models.Boletas();
                    //validamos si han ingresado nuevos clasificadores, los nuevos clasificadores estaran
                    //con IdBoleta = 0
                    //si solo modificaron los campos de un clasificador ya existente:
                    if (modelBoleta.IdBoleta != 0)
                    {
                        boleta.IdBoleta = modelBoleta.IdBoleta;
                        boleta.IdRegistro = registro.IdRegistro;
                        boleta.IdParametro = modelBoleta.IdParametro;
                        boleta.ImporteUnitarioClasificador = modelBoleta.ImporteUnitarioClasificador;
                        _context.Entry(boleta).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }
                    //si el IdBoleta = 0 entonces es nuevo y debe agregarse a la base de datos
                    else
                    {
                        boleta.IdRegistro = registro.IdRegistro;
                        boleta.IdParametro = modelBoleta.IdParametro;
                        boleta.ImporteUnitarioClasificador = modelBoleta.ImporteUnitarioClasificador;
                        _context.Boletas.Add(boleta);
                        await _context.SaveChangesAsync();
                    }
                    //si eliminan un clasificador sera en DeleteDetalle()
                }
                return Ok("Se modificó el registro correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("anular")]
        public JsonResult AnularRegistro(Registros registros)
        {
            string query = @"
                            update dbo.Registros set
                            dbo.Registros.Anulado = @Anulado
                            where dbo.Registros.IdRegistro = @IdRegistro
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = Configuration.GetConnectionString("RiaeAppConex");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@IdRegistro", registros.IdRegistro);
                    myCommand.Parameters.AddWithValue("@Anulado", registros.Anulado);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            if (registros.Anulado == true)
            {
                return new JsonResult("Registro anulado");
            }
            else
            {
                return new JsonResult("Registro activado");
            }
        }

        [HttpPut]
        [Route("importetotal")]
        public JsonResult ModificarImporteTotal (Registros registros)
        {
            string query = @"
                            update dbo.Registros set
                            dbo.Registros.ImporteTotalBoleta = @ImporteTotalBoleta
                            where dbo.Registros.IdRegistro = @IdRegistro
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = Configuration.GetConnectionString("RiaeAppConex");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@IdRegistro", registros.IdRegistro);
                    myCommand.Parameters.AddWithValue("@ImporteTotalBoleta", registros.ImporteTotalBoleta);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("El total ha sido actualizado.");
        }
        /*Esta opcion se dio de baja debido a que se eliminan completamente los registros de la
        base de datos y solo debe cambiar de estado de 0 a 1*/
        /*[HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var listaBoletas = await _context.Boletas.ToListAsync();
                
                foreach (var boletaAEliminar in listaBoletas)
                {
                    if (boletaAEliminar.IdRegistro == id)
                    {
                        string query = @"
                            delete from dbo.Boletas
                            where IdBoleta = @IdBoleta
                            ";
                        string sqlDataSource = Configuration.GetConnectionString("RiaeAppConex");
                        SqlDataReader myReader;
                        using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                        {
                            myCon.Open();
                            using (SqlCommand myCommand = new SqlCommand(query, myCon))
                            {
                                myCommand.Parameters.AddWithValue("@IdBoleta", boletaAEliminar.IdBoleta);

                                myReader = myCommand.ExecuteReader();
                                myReader.Close();
                                myCon.Close();
                            }
                        }
                    }
                }
                var registroAEliminar = await _context.Registros.FindAsync(id);
                _context.Remove(registroAEliminar);
                await _context.SaveChangesAsync();
                return Ok("registro con boletas eliminado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex + "hola");
            }
        }
    }*/

        [HttpDelete("{id}")]
        public JsonResult DeleteDetalle(int id)
        {
            string query = @"
                    select * from dbo.Boletas
                    where IdBoleta = @IdBoleta
                    ";
            DataTable table = new DataTable();
            DataTable tableEliminar = new DataTable();
            string sqlDataSource = Configuration.GetConnectionString("RiaeAppConex");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@IdBoleta", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    if (table.Rows.Count == 0)
                    {
                        return new JsonResult("El clasificador ya no existe en el registro. Vuelva a cargar la " +
                            "página por favor. ");
                    }
                    else
                    {
                        string queryEliminar = @"
                            delete from dbo.Boletas
                            where IdBoleta = @IdBoleta
                            ";
                        using (SqlCommand myCommand1 = new SqlCommand(queryEliminar, myCon))
                        {
                            myCommand1.Parameters.AddWithValue("@IdBoleta", id);
                            myReader = myCommand1.ExecuteReader();
                            tableEliminar.Load(myReader);
                            myReader.Close();
                            myCon.Close();
                        }
                        return new JsonResult("El clasificador ha sido eliminado del registro. ");
                    }
                }
            }
        }
    }
    //var boletaAEliminar = _context.Boletas.FindAsync(boleta.IdBoleta);
    /*if (registroAEliminar == null)
    {
        return NotFound();
    }
    _context.Remove(registroAEliminar);
    await _context.SaveChangesAsync();*/
}