using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RIAE3._1.Context;
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
    public class ReportController : ControllerBase
    {
        public ReportController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        [HttpPost]
        [Route("ingpropios/pormesyanio")]
        public JsonResult IngresosPropiosPorMesAnio(Reporte reporte)
        {
            string query = @"
                            SELECT * FROM dbo.Registros WHERE 
                            dbo.Registros.IdParametroTipo = 1 
                            and MONTH(dbo.Registros.Fecha) = @Mes
                            AND YEAR(dbo.Registros.Fecha) = @Anio
                            order by dbo.Registros.NroRecibo
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = Configuration.GetConnectionString("RiaeAppConex");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Mes", reporte.Mes);
                    myCommand.Parameters.AddWithValue("@Anio", reporte.Anio);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }
        [HttpPost]
        [Route("ingpropios/poranio")]
        public JsonResult IngresosPropiosPorAnio(Reporte reporte)
        {
            string query = @"
                            SELECT * FROM dbo.Registros WHERE
                            dbo.Registros.IdParametroTipo = 1 and
                            YEAR(dbo.Registros.Fecha) = @Anio
                            order by dbo.Registros.NroRecibo
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = Configuration.GetConnectionString("RiaeAppConex");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Anio", reporte.Anio);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }
        [HttpPost]
        [Route("fondorotatorio/pormesyanio")]
        public JsonResult FondoRotatorioPorMesAnio(Reporte reporte)
        {
            string query = @"
                            SELECT * FROM dbo.Registros WHERE 
                            dbo.Registros.IdParametroTipo = 2 
                            and MONTH(dbo.Registros.Fecha) = @Mes
                            AND YEAR(dbo.Registros.Fecha) = @Anio
                            order by dbo.Registros.NroRecibo
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = Configuration.GetConnectionString("RiaeAppConex");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Mes", reporte.Mes);
                    myCommand.Parameters.AddWithValue("@Anio", reporte.Anio);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }
        [HttpPost]
        [Route("fondorotatorio/poranio")]
        public JsonResult FondoRotatorioPorAnio(Reporte reporte)
        {
            string query = @"
                            SELECT * FROM dbo.Registros WHERE
                            dbo.Registros.IdParametroTipo = 2 and
                            YEAR(dbo.Registros.Fecha) = @Anio
                            order by dbo.Registros.NroRecibo
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = Configuration.GetConnectionString("RiaeAppConex");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Anio", reporte.Anio);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }
    }
}
