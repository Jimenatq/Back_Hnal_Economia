using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace RIAE3._1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParametrosController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ParametrosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("Tipos")]
        public JsonResult GetTipos()
        {
            string query = @"
                            select IdParametro, Codigo, Descripcion
                            from dbo.ParametroTesoreria
                            where IdTipoParametro = 1;";
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

        [HttpGet]
        [Route("SubtiposIP")]
        public JsonResult GetSubtiposIP()
        {
            string query = @"
                            select IdParametro, Codigo, Descripcion
                            from dbo.ParametroTesoreria
                            where IdTipoParametro = 2;";
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

        [HttpGet]
        [Route("SubtiposFR")]
        public JsonResult GetSubtiposFR()
        {
            string query = @"
                            select IdParametro, Codigo, Descripcion
                            from dbo.ParametroTesoreria
                            where IdTipoParametro = 3;";
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

        [HttpGet]
        [Route("Clasificadores")]
        public JsonResult GetClasificadores()
        {
            string query = @"
                            select IdParametro, Codigo, CodClasificadorArea, CodClasificadorExterno, Descripcion 
                            from dbo.ParametroTesoreria
                            where IdTipoParametro = 4 or  IdTipoParametro= 5;";
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
    }
}
