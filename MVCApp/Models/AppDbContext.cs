using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Models
{
    // Heredamos de la clase DbContext
    public class AppDbContext: DbContext
    {
        // Instanciar indicando el tipo objeto (constructor)
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //
        }

        // Propiedad que nos permitira jugar con las entidades del modelo, en este caso amigo
        // Sirve para consultar y guardar instancias de la clase amigo
        public DbSet<Amigo> Amigos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Amigo>().HasData(new Amigo
            {
                Id =1,
                Nombre ="Elon",
                Ciudad =Provincia.Madrid,
                Email ="pepe@gmail.com"
            },
            
            new Amigo {
                Id = 2,
                Nombre = "Jose",
                Ciudad = Provincia.Valladolid,
                Email = "jose@gmail.com"

            },

            new Amigo
            {
                Id = 3,
                Nombre = "Sergio",
                Ciudad = Provincia.Bizkaia,
                Email = "sr4@gmail.com"

            }


            );
        }
    }
}
