namespace CaronaApp.Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Carona_Passageiros : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Caronas", "QuantidadeVagas", c => c.Int(nullable: false, defaultValue: 1));
            DropColumn("dbo.Caronas", "Quantidade");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Caronas", "Quantidade", c => c.Int(nullable: false));
            DropColumn("dbo.Caronas", "QuantidadeVagas");
        }
    }
}
