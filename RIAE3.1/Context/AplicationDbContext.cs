﻿using Microsoft.EntityFrameworkCore;
using RIAE3._1.Models;
using RIAE3._1.Authentication;

namespace RIAE3._1.Context
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Registros> Registros { get; set; }
        public DbSet<Boletas> Boletas { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        //public DbSet<Roles> Roles { get; set; }
        //public DbSet<UsuarioRoles> UsuarioRoles { get; set; }
    }
}
