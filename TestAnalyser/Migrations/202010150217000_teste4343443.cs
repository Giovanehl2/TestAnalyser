namespace TestAnalyser.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teste4343443 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Provas", "DataProvaInicio", c => c.DateTime(nullable: false));
            AddColumn("dbo.Provas", "DataProvaFim", c => c.DateTime(nullable: false));
            DropColumn("dbo.Provas", "DataProva");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Provas", "DataProva", c => c.DateTime(nullable: false));
            DropColumn("dbo.Provas", "DataProvaFim");
            DropColumn("dbo.Provas", "DataProvaInicio");
        }
    }
}
