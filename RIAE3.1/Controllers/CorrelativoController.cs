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
                        select * from dbo.Correlativo order by dbo.Correlativo.Ano, dbo.Correlativo.IdParametro";
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
            string queryConsultar = @"
                            select * from dbo.Correlativo 
                            where IdParametro = @IdParametro and ano=@Anio
                            ";
            string queryInsertar = @"
                            insert into dbo.Correlativo
                            values (@IdParametro, @NombreCorrelativo, @NroCorrelativo, @Ano)
                            ";
            DataTable table = new DataTable();
            DataTable table1 = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("RiaeAppConex");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(queryConsultar, myCon))
                {
                    myCommand.Parameters.AddWithValue("@IdParametro", correlativo.IdParametro);
                    myCommand.Parameters.AddWithValue("@Anio", correlativo.Ano);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();

                    if (table.Rows.Count != 0)
                    {
                        return new JsonResult("Ya existe un correlativo igual, ingrese de nuevo" +
                            " el correlativo correcto");
                    }
                    else
                    {
                        using (SqlCommand myCommand1 = new SqlCommand(queryInsertar, myCon))
                        {
                            myCommand1.Parameters.AddWithValue("@IdParametro", correlativo.IdParametro);
                            myCommand1.Parameters.AddWithValue("@NombreCorrelativo", correlativo.NombreCorrelativo);
                            myCommand1.Parameters.AddWithValue("@NroCorrelativo", 0);
                            myCommand1.Parameters.AddWithValue("@Ano", correlativo.Ano);
                            myReader = myCommand1.ExecuteReader();
                            table1.Load(myReader);
                            myReader.Close();
                            myCon.Close();
                        }
                        return new JsonResult("Correlativo agregado con exito");
                    }
                }    
            }
        }

        [HttpPut]
        public JsonResult Put(Correlativo correlativo)
        {
            string query = @"
                            update dbo.Correlativo
                            set 
                            NroCorrelativo = @NroCorrelativo
                            where IdCorrelativo = @IdCorrelativo
                            ";
            DataTable table = new DataTable();
            //Riae => registro de ingresos del area de economia
            string sqlDataSource = _configuration.GetConnectionString("RiaeAppConex");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@IdCorrelativo", correlativo.IdCorrelativo);
                    myCommand.Parameters.AddWithValue("@NroCorrelativo", correlativo.NroCorrelativo);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("El N° de doc ha sido modificado con éxito");
        }

        [HttpPost]
        [Route("consulta")]
        public JsonResult ObtenerCorrelativo(Correlativo correlativo)
        {
            //////////////////////////////////////
            //int anioActual = @DateTime.Now.Year;
            //int anioActual =2024;
            string query = @"
                            select * from dbo.Correlativo 
                            where IdParametro = @IdParametro and ano=@Anio
                            ";
            DataTable table = new DataTable();
            DataTable table1 = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("RiaeAppConex");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@IdParametro", correlativo.IdParametro);
                    myCommand.Parameters.AddWithValue("@Anio", correlativo.Ano);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();

                    if (table.Rows.Count != 0)
                    {
                        string obtenerNroCorrelativo = @"
                                                    select NroCorrelativo from dbo.Correlativo 
                                                    where IdParametro = @IdParametro and ano = @Anio";
                        using (SqlCommand myCommand1 = new SqlCommand(obtenerNroCorrelativo, myCon))
                        {
                            myCommand1.Parameters.AddWithValue("@IdParametro", correlativo.IdParametro);
                            myCommand1.Parameters.AddWithValue("@Anio", correlativo.Ano);
                            myReader = myCommand1.ExecuteReader();
                            table1.Load(myReader);
                            myReader.Close();
                        }
                        int NroRecibo = 0;
                        foreach (DataRow fila in table1.Rows)
                        {
                            NroRecibo = Convert.ToInt16(fila["NroCorrelativo"].ToString());
                        }
                        myCon.Close();
                        return new JsonResult(NroRecibo + 1);
                    }
                    else
                    {
                        if (correlativo.IdParametro == 1)
                        {
                            string queryInsertar = @"
                            insert into dbo.Correlativo
                            values (@IdParametro, 'Ingresos Propios', 0 , @Anio)
                            ";
                            using (SqlCommand myCommand1 = new SqlCommand(queryInsertar, myCon))
                            {
                                myCommand1.Parameters.AddWithValue("@IdParametro", correlativo.IdParametro);
                                myCommand1.Parameters.AddWithValue("@Anio", correlativo.Ano);
                                myReader = myCommand1.ExecuteReader();
                                table.Load(myReader);
                                myReader.Close();
                            }
                            correlativo.NroCorrelativo = 0;
                        }
                        else if (correlativo.IdParametro == 2)
                        {
                            string queryInsertar = @"
                            insert into dbo.Correlativo
                            values (@IdParametro, 'Fondo Rotatorio', 0 , @Anio)
                            ";
                            using (SqlCommand myCommand1 = new SqlCommand(queryInsertar, myCon))
                            {
                                myCommand1.Parameters.AddWithValue("@IdParametro", correlativo.IdParametro);
                                myCommand1.Parameters.AddWithValue("@Anio", correlativo.Ano);
                                myReader = myCommand1.ExecuteReader();
                                table.Load(myReader);
                                myReader.Close();
                            }
                            correlativo.NroCorrelativo = 0;
                        }
                        myCon.Close();
                        return new JsonResult(correlativo.NroCorrelativo + 1);
                    }
                }
                //return new JsonResult("hola");
            }
        }
        [HttpPut]
        [Route("modificar")]
        public JsonResult modificarCorrelativo(Correlativo correlativo)
        {
            //int anioActual = @DateTime.Now.Year;
            //int anioActual =2024;
            string query = @"
                            select * from dbo.Correlativo 
                            where IdParametro = @IdParametro and ano=@Anio
                            ";
            DataTable table = new DataTable();
            DataTable table1 = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("RiaeAppConex");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@IdParametro", correlativo.IdParametro);
                    myCommand.Parameters.AddWithValue("@Anio", correlativo.Ano);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();

                    if (table.Rows.Count != 0)
                    {
                        string obtenerNroCorrelativo = @"
                                                    select NroCorrelativo from dbo.Correlativo 
                                                    where IdParametro = @IdParametro and ano = @Anio";
                        string queryActualizar = @"
                            update dbo.Correlativo
                            set
                            NroCorrelativo = @NroCorrelativo
                            where IdParametro = @IdParametro and
                            Ano= @Anio
                            ";
                        using (SqlCommand myCommand1 = new SqlCommand(obtenerNroCorrelativo, myCon))
                        {
                            myCommand1.Parameters.AddWithValue("@IdParametro", correlativo.IdParametro);
                            myCommand1.Parameters.AddWithValue("@Anio", correlativo.Ano);
                            myReader = myCommand1.ExecuteReader();
                            table1.Load(myReader);
                            myReader.Close();
                        }
                        int NroRecibo=0;
                        foreach(DataRow fila in table1.Rows)
                        {
                           NroRecibo = Convert.ToInt16(fila["NroCorrelativo"].ToString());
                        }
                        using (SqlCommand myCommand2 = new SqlCommand(queryActualizar, myCon))
                        {
                            myCommand2.Parameters.AddWithValue("@IdParametro", correlativo.IdParametro);
                            myCommand2.Parameters.AddWithValue("@NroCorrelativo", NroRecibo+1);
                            myCommand2.Parameters.AddWithValue("@Anio", correlativo.Ano);
                            myReader = myCommand2.ExecuteReader();
                            table.Load(myReader);
                            myReader.Close();
                        }
                        myCon.Close();
                        return new JsonResult(NroRecibo + 1);
                    }
                    else
                    {
                        if (correlativo.IdParametro == 1)
                        {
                            string queryInsertar = @"
                            insert into dbo.Correlativo
                            values (@IdParametro, 'Ingresos Propios', 0 , @Anio)
                            ";
                            using (SqlCommand myCommand1 = new SqlCommand(queryInsertar, myCon))
                            {
                                myCommand1.Parameters.AddWithValue("@IdParametro", correlativo.IdParametro);
                                myCommand1.Parameters.AddWithValue("@Anio", correlativo.Ano);
                                myReader = myCommand1.ExecuteReader();
                                table.Load(myReader);
                                myReader.Close();
                            }
                            correlativo.NroCorrelativo = 0;
                        }
                        else if (correlativo.IdParametro == 2)
                        {
                            string queryInsertar = @"
                            insert into dbo.Correlativo
                            values (@IdParametro, 'Fondo Rotatorio', 0 , @Anio)
                            ";
                            using (SqlCommand myCommand1 = new SqlCommand(queryInsertar, myCon))
                            {
                                myCommand1.Parameters.AddWithValue("@IdParametro", correlativo.IdParametro);
                                myCommand1.Parameters.AddWithValue("@Anio", correlativo.Ano);
                                myReader = myCommand1.ExecuteReader();
                                table.Load(myReader);
                                myReader.Close();
                            }
                            correlativo.NroCorrelativo = 0;
                        }
                        myCon.Close();
                        return new JsonResult(correlativo.NroCorrelativo+1);
                    }
                }
            }
        }
    }
}
