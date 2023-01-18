using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using RIAE3._1.Models;
using Microsoft.Win32;

namespace RIAE3._1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoletasController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public BoletasController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select IdBoleta, IdRegistro, IdParametro, ImporteUnitarioClasificador 
                            from dbo.Boletas
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("RiaeAppConex");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
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

            return new JsonResult(table);
        }
        
        [HttpPost]
        public JsonResult Post(Boletas boletas)
        /*{
            guardarBoleta(boletas);
            return new JsonResult("Boleta agregada con exito");
            return _boletasRepository.registrarBoleta(boletas);
        }
        private void guardarBoleta(Boletas boletas)*/
        {
            string query = @"
                            insert into dbo.Boletas
                            values (@IdRegistro, @IdParametro, @ImporteUnitarioClasificador)
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("RiaeAppConex");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@IdRegistro", boletas.IdRegistro);
                    myCommand.Parameters.AddWithValue("@IdParametro", boletas.IdParametro);
                    myCommand.Parameters.AddWithValue("@ImporteUnitarioClasificador", boletas.ImporteUnitarioClasificador);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Boleta agregada con exito");
        }

        [HttpPut]
        public JsonResult Put(Boletas boletas)
        {
            string query = @"
                            update dbo.Boletas
                            set
                            IdRegistro = @IdRegistro,
                            IdParametro = @IdParametro, 
                            ImporteUnitarioClasificador = @ImporteUnitarioClasificador
                            where IdBoleta = @IdBoleta
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("RiaeAppConex");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    //myCommand.Parameters.AddWithValue("@IdBoleta", boletas.IdBoleta);
                    myCommand.Parameters.AddWithValue("@IdRegistro", boletas.IdRegistro);
                    myCommand.Parameters.AddWithValue("@IdParametro", boletas.IdParametro);
                    myCommand.Parameters.AddWithValue("@ImporteUnitarioClasificador", boletas.ImporteUnitarioClasificador);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Boleta actualizada con exito");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                            delete from dbo.Boletas
                            where IdBoleta = @IdBoleta
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("RiaeAppConex");
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
                    myCon.Close();
                }
            }

            return new JsonResult("Boleta eliminada con exito");
        }
    }
}
