using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tarea3.Models;


namespace Tarea3.DAL
{

    public class Contexto : DbContext
    {
        public DbSet<Persona> Persona { get; set; }
        public DbSet<Prestamo> Prestamo { get; set; }
        public DbSet<Moras> Moras { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = Data\Prestamos.db");
        }
    }
}
