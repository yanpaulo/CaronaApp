namespace CaronaApp.Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Vagas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PassageiroCaronas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CaronaId = c.Int(nullable: false),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Caronas", t => t.CaronaId, cascadeDelete: true)
                .Index(t => t.CaronaId);
            
            AddColumn("dbo.Caronas", "Nome", c => c.String());
            AddColumn("dbo.Caronas", "Quantidade", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PassageiroCaronas", "CaronaId", "dbo.Caronas");
            DropIndex("dbo.PassageiroCaronas", new[] { "CaronaId" });
            DropColumn("dbo.Caronas", "Quantidade");
            DropColumn("dbo.Caronas", "Nome");
            DropTable("dbo.PassageiroCaronas");
        }
    }
}
