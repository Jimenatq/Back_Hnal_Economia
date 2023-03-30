using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RIAE3._1.Authentication;
using System;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using RIAE3._1.Context;
using Microsoft.EntityFrameworkCore;
using static RIAE3._1.Authentication.Usuarios;
using RIAE3._1.Models.Request;
using RIAE3._1.Models;

namespace RIAE3._1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        public readonly AplicationDbContext _context;
        public UsuariosController(AplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public static String NombreCompleto, NTusername;

        [HttpGet]
        public async Task<IActionResult> GetUsuariosConRoles(ListaUsuariosConRoles listaUsuariosConRoles)
        {
            var listaUsersConRoles = await _context.Registros.ToListAsync();
            /*foreach (var users in listaUsersConRoles)
            {
                Usuarios user = new Usuarios();
                user.IdUsuario = users.IdUsuario;
                user.Usuario = users.Usuario;
                user.NombresCompletos = users.NombresCompletos;
                user.FechaPrimerLogin = users.FechaPrimerLogin;
                user.esActivo = users.esActivo;
                UsuarioRoles usuarioRoles = new UsuarioRoles();
                Roles roles = new Roles();
                var listaUsuarioRoles = await _context.UsuarioRoles.Where(x => x.IdUsuario == users.IdUsuario).ToListAsync();
                ListaRoles lista = new ListaRoles();
                foreach (var usuarioroles in listaUsuarioRoles)
                {
                    var listaRoles = await _context.Roles.Where(x => x.IdRol == usuarioroles.IdRol).ToListAsync();
                    lista.IdUsuarioRol = usuarioRoles.IdUsuarioRol;
                    lista.Descripcion = listaRoles.ToString();
                }
                user.listRoles.Add(lista);
                listaUsuariosConRoles.usuarios.Add(user);
            }*/
            return Ok(listaUsersConRoles);
        }

        /*public async Task GuardarUsuarioenBD(string NombreCompleto, string NTusername)
        {
            
                string query = @"
                            SELECT * FROM dbo.Usuarios where Usuario = @Usuario
                            ";
                DataTable table = new DataTable();
                string sqlDataSource = Configuration.GetConnectionString("RiaeAppConex");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@Usuario", NTusername);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();

                        if (table.Rows.Count != 0)
                        {
                            myCon.Close();
                        }
                        else
                        {
                            var user = new Usuarios();
                            user.NombresCompletos = NombreCompleto;
                            user.Usuario = NTusername;
                            user.FechaPrimerLogin = DateTime.Now;
                            user.esActivo = true;
                            _context.Usuarios.Add(user);
                            await _context.SaveChangesAsync();
                            myCon.Close();
                        }
                    }
                }
        }*/
        private string GetCurrentDomainPath()
        {
            DirectoryEntry de = new DirectoryEntry("LDAP://RootDSE");
            //LDAP://na.miempresa.com
            return "LDAP://" + de.Properties["defaultNamingContext"][0].ToString();
        }

       
            /*public JsonResult ObtenerUsuarios()
            {
                string query = @"
                                SELECT * FROM dbo.Usuarios
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
            //////////
            DataTable table = new DataTable();
                string query = @"
                                select dbo.usuarioroles.IdUsuarioRol,dbo.Roles.Descripcion 
                                from dbo.UsuarioRoles inner join dbo.Roles on 
                                dbo.UsuarioRoles.IdRol= dbo.Roles.IdRol where 
                                dbo.UsuarioRoles.IdUsuario = @IdUsuario
                                ";
                string sqlDataSource = _configuration.GetConnectionString("RiaeAppConex");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@IdUsuario", users.IdUsuario);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        User usuarioRol = new User();
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            usuarioRol.IdUsuarioRol = (int)table.Rows[i]["IdUsuarioRol"];
                            usuarioRol.Descripcion =  table.Rows[i]["Descripcion"].ToString();
                        }
                        user.listRoles.Add(usuarioRol);
                        myCon.Close();
                        listaUsuariosConRoles.usuarios.Add(user);
                    }
                }
            }
            return Ok(listaUsuariosConRoles.usuarios);
             */

        [HttpPost]
        public async Task<JsonResult> LoginAsync(UserAD userAD)
        {
            bool ret;
            try
            {
                DirectoryEntry de = new DirectoryEntry(GetCurrentDomainPath(), userAD.Usuario, userAD.Clave);
                DirectorySearcher dsearch = new DirectorySearcher(de);
                dsearch.Filter = "sAMAccountName=" + userAD.Usuario + "";
                SearchResult results = null;

                results = dsearch.FindOne();

                NombreCompleto = results.GetDirectoryEntry().Properties["DisplayName"].Value.ToString();
                NTusername = results.GetDirectoryEntry().Properties["sAMAccountName"].Value.ToString();
                ret = true;
            }
            catch
            {
                ret = false;
            }
            if (ret)
            {
                string query = @"
                            SELECT * FROM dbo.Usuarios where Usuario = @Usuario
                            ";
                DataTable table = new DataTable();
                string sqlDataSource = Configuration.GetConnectionString("RiaeAppConex");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@Usuario", NTusername);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();

                        if (table.Rows.Count != 0)
                        {
                            myCon.Close();
                        }
                        else
                        {
                            var user = new Usuarios();
                            user.NombresCompletos = NombreCompleto;
                            user.Usuario = NTusername;
                            user.FechaPrimerLogin = DateTime.Now;
                            user.esActivo = true;
                            _context.Usuarios.Add(user);
                            await _context.SaveChangesAsync();
                            myCon.Close();
                        }
                    }
                }
                return new JsonResult(null);
            }
            else
            {
                return new JsonResult("El usuario o clave son invalidas. Por favor intente de nuevo");
            }
        }


    }

}