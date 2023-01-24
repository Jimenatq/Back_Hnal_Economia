
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
            var listaRegistros = await _context.Registros.ToListAsync();
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
                    registro.listBoletas = await _context.Boletas.Where(p=>p.IdRegistro==id).ToListAsync();
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
        public async Task<IActionResult> Post(RegistrosRequest registros)
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
        public async Task<IActionResult> Put(RegistrosRequest registros)
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
                _context.Entry(registro).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                foreach (var modelBoleta in registros.listBoletas)
                {
                    var boleta = new Models.Boletas();
                    boleta.IdBoleta = modelBoleta.IdBoleta;
                    boleta.IdRegistro = registro.IdRegistro;
                    boleta.IdParametro = modelBoleta.IdParametro;
                    boleta.ImporteUnitarioClasificador = modelBoleta.ImporteUnitarioClasificador;
                    _context.Entry(boleta).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                return Ok("registro con boletas modificado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpDelete("{id}")]
        //[HttpGet("{id}")]
        public async Task<IActionResult> Delete(int id)
        //public IActionResult Delete(int id)
        {
            try
            {
                var listaBoletas = await _context.Boletas.ToListAsync();
                
                //return Ok(registroAEliminar);
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
                    //return Ok("registro con boletas eliminado");
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
    }
    //var boletaAEliminar = _context.Boletas.FindAsync(boleta.IdBoleta);
    /*if (registroAEliminar == null)
    {
        return NotFound();
    }
    _context.Remove(registroAEliminar);
    await _context.SaveChangesAsync();*/
}
//como pasar objeto parecido al que se pasa en FindAsync para eliminar
/*ObjectFind objectFind = new ObjectFind();
                        {
                            objectFind.IsCompleted = false;
                            objectFind.IsCompletedSuccessfully = false;
                            objectFind.IsFaulted = false;
                            objectFind.IsCanceled = false;
                            objectFind.Result = new ResultModel()
                            {
                                IdBoleta = boletaAEliminar.IdBoleta,
                                IdRegistro = boletaAEliminar.IdRegistro,
                                IdParametro = boletaAEliminar.IdParametro,
                                ImporteUnitarioClasificador = boletaAEliminar.ImporteUnitarioClasificador
                            };

                        }
                        
                        return Ok(objectFind);
                        _context.Remove(objectFind);
                        await _context.SaveChangesAsync();
                        */


//////////////////////////////////////////////////////
//para sacar reportes con recibos ordenados: Nota: solo faltaria  una condicional para q sea solo de tipo 1 o 2
//var c = _context.Registros.OrderBy(p => p.NroRecibo);
//return Ok(c);