namespace CaronaApp.Server.Models.Entities
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class CaronaContext : DbContext
    {
        // Your context has been configured to use a 'CaronaContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'CaronaApp.Server.Models.Entities.CaronaContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'CaronaContext' 
        // connection string in the application configuration file.
        public CaronaContext()
            : base("name=CaronaContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Carona> Caronas { get; set; }
    }

}