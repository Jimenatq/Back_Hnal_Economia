using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RIAE3._1.Authentication
{
    public class Usuarios
    {
        [Key]
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string NombresCompletos { get; set; }
        public DateTime FechaPrimerLogin { get; set; }
        public bool esActivo { get; set; }
        public List<ListaRoles> listRoles { get; set; }
        public Usuarios()
        {
            this.listRoles = new List<ListaRoles>();
        }
            
        
    }
    public class ListaRoles
    {
        [Key]
        public int IdUsuarioRol { get; set; }
        public string Descripcion { get; set; }
    }

    public class UserAD
    {
        public string Usuario { get; set; }
        public string Clave { get; set; }
    }
    public class ListaUsuariosConRoles
    {
        public List<Usuarios> usuarios = new List<Usuarios>();
    }
    /*public class UsuarioRoles
    {
        [Key]
        public int IdUsuarioRol { get; set; }
        public int IdRol { get; set; }
        public int IdUsuario { get; set; }
    }
    public class Roles
    {
        [Key]
        public int IdRol { get; set; }
        public string Descripcion { get; set; }
    }*/
}
