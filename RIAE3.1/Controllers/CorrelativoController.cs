using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RIAE3._1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RIAE3._1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CorrelativoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public CorrelativoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                        select IdCorrelativo, IdParametro, NombreCorrelativo, NroCorrelativo, Ano
                        from dbo.Correlativo";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("RiaeAppConex");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myComand = new SqlCommand(query, myCon))
                {
                    myReader = myComand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Correlativo correlativo)
        {
            string query = @"
                            insert into dbo.Registros
                            values (@IdParametro, @NombreCorrelativo, @NroCorrelativo, @Ano)
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("RiaeAppConex");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@IdParametro", correlativo.IdParametro);
                    myCommand.Parameters.AddWithValue("@NombreCorrelativo", correlativo.NombreCorrelativo);
                    myCommand.Parameters.AddWithValue("@NroCorrelativo", correlativo.NroCorrelativo);
                    myCommand.Parameters.AddWithValue("@Ano", correlativo.Ano);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Correlativo agregado con exito");
        }

        [HttpPut]
        public JsonResult Put(Correlativo correlativo)
        {
            string query = @"
                            update dbo.Correlativo
                            set IdParametro = @IdParametro,
                            NombreCorrelativo = @NombreCorrelativo,
                            NroCorrelativo = @NroCorrelativo,
                            Ano = @Ano
                            where IdCorrelativo = @IdCorrelativo
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("RiaeAppConex");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@IdCorrelativo", correlativo.IdCorrelativo);
                    myCommand.Parameters.AddWithValue("@IdParametro", correlativo.IdParametro);
                    myCommand.Parameters.AddWithValue("@NombreCorrelativo", correlativo.NombreCorrelativo);
                    myCommand.Parameters.AddWithValue("@NroCorrelativo", correlativo.NroCorrelativo);
                    myCommand.Parameters.AddWithValue("@Ano", correlativo.Ano);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Correlativo actualizado con exito");
        }
    }
}
