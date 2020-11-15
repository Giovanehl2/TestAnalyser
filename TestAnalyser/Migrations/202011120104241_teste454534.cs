namespace TestAnalyser.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teste454534 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Provas", "NomeTurma", c => c.String(nullable: false, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Provas", "NomeTurma");
        }
    }
}
