using Microsoft.EntityFrameworkCore;


namespace L01_2020ML603.Models
{
    public class pedidosContext: DbContext
    {

        public pedidosContext(DbContextOptions<pedidosContext> options) : base(options)
        {

        }


        //Se presentan las tablas
        public DbSet<pedidos> pedidos { get; set; }

        public DbSet<platos> platos { get; set; }
        public DbSet<motoristas> motoristas { get; set; }
    }
}

