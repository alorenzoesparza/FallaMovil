namespace FallaMovil.Domain
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {
        }

        public DbSet<Act> Acts { get; set; }

        public DbSet<ActAssistance> ActAssistances { get; set; }

        //public DbSet<Representante> Representantes { get; set; }

        public DbSet<Component> Components { get; set; }

        //public DbSet<Supporter> Supporters { get; set; }

        //public DbSet<ActSupporter> ActSupporters { get; set; }

        //public DbSet<Noticia> Noticias { get; set; }

        //public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
